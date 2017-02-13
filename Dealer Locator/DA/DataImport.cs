using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Dealer_Locator.DA
{
    public class DataImport
    {
        public static bool ImportData(string AccessFilePath, ArrayList tableNames, string alternateAccessTableName, out System.Collections.Generic.List<string> errors)
        {
            DataSet ds = new DataSet();
            string sql;
            string values;
            errors = new System.Collections.Generic.List<string>();
            bool result = true;

            foreach (string tableName in tableNames)
            {
                string tempTableName = tableName;

                // get the data
                if (alternateAccessTableName != "")
                    tempTableName = alternateAccessTableName;

                try
                {

                    // import both into the "Category" table and the "DL.MainCategory" tables
                    // We need no special import for the Category table due to no disabling needing to be done
                    if (tempTableName == "Category")
                    {
                        ImportCategory(AccessFilePath, tempTableName);
                    }


                    ds = Dealer_Locator.DA.DataAccess.GetAllData_AccessDatabase(tempTableName, AccessFilePath);

                    // clear the table
                    sql = "DELETE FROM " + tableName;
                    DA.DataAccess.Update(sql);

                    int counter = 0;
                    sql = "";
                    string tempVal;
                    Hashtable valueTable;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        values = "";
                        valueTable = new Hashtable();
                        int arrayCounter = 0;

                        foreach (object myObj in dr.ItemArray)
                        {
                            if (values != "")
                                values = values + ",";

                            tempVal = (string)myObj.ToString();
                            tempVal = tempVal.Replace("'", "''");

                            if (tableName == "Category" )
                            {
                                if (tempVal == "False")
                                {
                                    tempVal = "0";
                                }
                                else if (tempVal == "True")
                                {
                                    tempVal = "1";
                                }
                                

                            }

                            valueTable.Add(ds.Tables[0].Columns[arrayCounter].ColumnName, "'" + tempVal + "'");

                            values = values + "'" + tempVal + "'";

                            arrayCounter += 1;

                        }

                        if (tableName == "[DL.ZipLookup]")
                        {

                            sql = sql + "INSERT INTO " + tableName + " VALUES (";

                            //ZipCode, City, State, AreaCode, CityAliasName, CityAliasAbbreviation, CityType, County, StateFips, CountyFips, TimeZone, DayLightSaving, Latitude, Longitude, Elevation

                            sql = sql + valueTable["ZipCode"] + ",";
                            sql = sql + valueTable["City"] + ",";
                            sql = sql + valueTable["State"] + ",";
                            sql = sql + valueTable["AreaCode"] + ",";
                            sql = sql + valueTable["CityAliasName"] + ",";
                            sql = sql + valueTable["CityAliasAbbreviation"] + ",";
                            sql = sql + valueTable["CityType"] + ",";
                            sql = sql + valueTable["County"] + ",";

                            sql = sql + valueTable["StateFIPS"] + ",";
                            sql = sql + valueTable["CountyFIPS"] + ",";
                            sql = sql + valueTable["TimeZone"] + ",";
                            sql = sql + valueTable["DayLightSaving"] + ",";
                            sql = sql + valueTable["Latitude"] + ",";
                            sql = sql + valueTable["Longitude"] + ",";
                            sql = sql + valueTable["Elevation"];

                            sql = sql + ");";

                        } else {
                            sql = sql + "INSERT INTO " + tableName + " VALUES (" + values + ");";
                        }

                        if (counter > 100)
                        {
                            string tempError = string.Empty;

                            tempError = DA.DataAccess.Update(sql);
                            sql = "";
                            counter = 0;

                            if (tempError.Length > 0)
                            {
                                result = false;
                                errors.Add(tempError);
                            }
                        }

                        counter = counter + 1;
                    }

                    if (sql != "")
                    {
                        string tempError = string.Empty;

                        tempError = DA.DataAccess.Update(sql);

                        if (tempError.Length > 0)
                        {
                            result = false;
                            errors.Add(tempError);
                        }
                    }

                }

                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                    return false;
                }
            }

            return result;
        }

        private static void ImportCategory(string AccessFilePath, string tempTableName)
        {
            DataSet ds = new DataSet();
            string sql;
            string values;
            ds = Dealer_Locator.DA.DataAccess.GetAllData_AccessDatabase(tempTableName, AccessFilePath);

            int rowCount;

            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter ta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
            DA.MainCategoryTDS.DL_MainCategoryDataTable dt = new MainCategoryTDS.DL_MainCategoryDataTable();

            // get main categories in SQL2005 database
            dt = ta.GetData();

            bool rowExists;
            int catID = 0;
            
            foreach (DataRow dr in dt.Rows)
            {

                catID = Convert.ToInt32(dr["pk_mainCatID"]);
                string catName = Convert.ToString(dr["CategoryName"]);

                rowExists = false;

                foreach (DataRow drAccess in ds.Tables[0].Rows)
                {
                    if (catID == Convert.ToInt32(drAccess["CategoryID"]))
                    {
                        rowExists = true;
                        string accessCatName = Convert.ToString(drAccess["CategoryName"]);
                        string allowTerritoryOverlap = Convert.ToString(drAccess["AllowTerritoryOverlap"]);
                        
                        bool allowOverlap = false;

                        if (allowTerritoryOverlap == "False")
                        {
                            allowOverlap = false;
                        }
                        else
                        {
                            allowOverlap = true;
                        }

                        // update name
                        if (catName != accessCatName)
                        {
                            ta.UpdateName(accessCatName, catID);
                        }

                        ta.UpdateAllowTerritoryOverlap(allowOverlap, catID);

                        
                    }
                }

                if (rowExists == false)
                {
                    ta.DisableEnableCategory(true, catID);
                }
            }

            string categoryName;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int ordinal = Convert.ToInt32(dr["Ordinal"]);
                bool allowTerritoryOverlap = Convert.ToBoolean(dr["AllowTerritoryOverlap"].ToString());

                catID = Convert.ToInt32(dr["CategoryID"]);  // Get the new category id
                categoryName = Convert.ToString(dr["CategoryName"]);

                rowCount = Convert.ToInt32(ta.CheckIfCategoryExists(catID));

                // Make sure we re-enable if the row is present
                if (rowCount > 0)
                {
                    ta.DisableEnableCategory(false, catID);
                    ta.UpdatePosition_2(ordinal, catID);
                }
                else
                {
                    int nextPosition = DA.DataAccess.GetNextID("[DL.MainCategory]", "position");
                    // Add the new category, setting it to enabled
                    ta.InsertCategory(catID, categoryName, false, ordinal, "","","", allowTerritoryOverlap);
                }

            }

        }
    }
}
