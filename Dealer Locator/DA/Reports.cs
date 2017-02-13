using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SagaraSoftware.ZipCodeUtil;
using System.Collections;

namespace Dealer_Locator.DA
{
    public class Reports
    {

        #region Distributor City Report

            
            public static DataSet CreateDistributorCityReport()
            {
                string sql;
                
                sql = "SELECT Distinct(CityName) FROM Distributor " + 
                    " EXCEPT " + 
                    " SELECT DISTINCT([CITY_ALIAS_NAME]) FROM [DL.ZipLookup], Distributor, State " + 
                    " WHERE [CITY_ALIAS_NAME] = CityName AND [STATE] = State.Abbreviation " + 
                    " AND State.StateID = Distributor.fk_StateID";

                DataSet dsTemp = new DataSet();
                dsTemp = DA.DataAccess.Read(sql);

                string CityNameList = "";

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (CityNameList != "")
                        CityNameList = CityNameList + ", ";

                    CityNameList = CityNameList + "'" + dr["CityName"].ToString() + "'";
                }
                
                sql = "SELECT DistName, BillingAddress, BillingCityName, fk_BillingZipID, ShippingAddress, CityName, fk_ZipID, MainDistributor FROM Distributor WHERE CityName NOT IN (SELECT CITY_ALIAS_NAME FROM [DL.ZipLookup]) " + 
                    " OR CityName IN (" + CityNameList + ")";

                DataSet ds = DA.DataAccess.Read(sql);

                return ds;
            }
            // WHERE CityName NOT IN (SELECT CITY_ALIAS_NAME FROM [DL.ZipLookup])

        public static DataTable CreateDistributorZipReport()
        {
           // DataSet dsCityList = GetCityList();

            DataTable returnDT = new DataTable();
            returnDT.Columns.Add("pk_DistributorID");
            returnDT.Columns.Add("Distributor Name");
            returnDT.Columns.Add("Shipping Address");
            returnDT.Columns.Add("Shipping City Name");
            returnDT.Columns.Add("State");
            returnDT.Columns.Add("Current Zip Code");
            returnDT.Columns.Add("Possible Zip Codes");

            string sql;

            sql = "SELECT pk_DistributorID FROM Distributor " +
            " EXCEPT " +
" SELECT pk_DistributorID FROM Distributor, [DL.ZipLookup] WHERE " + 
" ZIP_CODE = fk_ZipID " +
" AND [CITY_ALIAS_NAME] = [CityName]";


            DataSet dsCityList = DA.DataAccess.Read(sql);

            foreach (DataRow dr in dsCityList.Tables[0].Rows)
            {
                DataSet dsTemp = new DataSet();
                dsTemp = DDA.DataAccess.Distributor_da.GetDistributorInformation_DL(Convert.ToInt32(dr["pk_DistributorID"].ToString()));
                
                string currentStateAbbreviation = "";
                currentStateAbbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(dsTemp.Tables[0].Rows[0]["fk_StateID"].ToString()));

                sql = "SELECT [ZIP_CODE] FROM [DL.ZipLookup] WHERE [CITY_ALIAS_NAME] = '" + dsTemp.Tables[0].Rows[0]["CityName"].ToString() +
                        "' AND [STATE] = '" + currentStateAbbreviation + "'";

                DataSet dsTemp2 = DA.DataAccess.Read(sql);

                string zipTemp = "";

                foreach (DataRow drTemp in dsTemp2.Tables[0].Rows)
                {
                    if (zipTemp != "")
                        zipTemp = zipTemp + ", ";

                    zipTemp = zipTemp + drTemp["ZIP_CODE"].ToString();
                }

                DataRow drNew = returnDT.NewRow();

                drNew["pk_DistributorID"] = dsTemp.Tables[0].Rows[0]["pk_DistributorID"];
                drNew["Distributor Name"] = dsTemp.Tables[0].Rows[0]["DistName"];
                drNew["Shipping Address"] = dsTemp.Tables[0].Rows[0]["ShippingAddress"];
                drNew["Shipping City Name"] = dsTemp.Tables[0].Rows[0]["CityName"];
                drNew["State"] = currentStateAbbreviation;
                drNew["Current Zip Code"] = dsTemp.Tables[0].Rows[0]["fk_ZipID"];
                drNew["Possible Zip Codes"] = zipTemp;

                returnDT.Rows.Add(drNew);

            }


            //////sql = "SELECT [pk_DistributorID], [CityName], [fk_ZipID], [fk_StateID] FROM Distributor ORDER BY CityName";

            //////DataSet dsFullList = DA.DataAccess.Read(sql);

            //////int count = 0 ;
            //////bool IsMatchFound = false;
            //////ArrayList BadDistributors = new ArrayList();

            //////if (dsCityList.Tables[0].Rows.Count != dsFullList.Tables[0].Rows.Count)
            //////{

            //////    foreach (DataRow drFull in dsFullList.Tables[0].Rows)
            //////    {
            //////        count = 0;
            //////        IsMatchFound = false;
            //////        foreach (DataRow drCity in dsCityList.Tables[0].Rows)
            //////        {
            //////            if (drCity["CityName"] == drFull["CityName"] && drCity["fk_ZipID"] == drFull["fk_ZipID"])
            //////                IsMatchFound = true;

            //////        }

            //////        string currentStateAbbreviation = "";
            //////        currentStateAbbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(drFull["fk_StateID"].ToString()));

            //////        if (IsMatchFound == false)
            //////        {

            //////            BadDistributors.Add(drFull["pk_DistributorID"]);
            //////            sql = "SELECT [ZIP_CODE] FROM [DL.ZipLookup] WHERE [CITY_ALIAS_NAME] = '" + drFull["CityName"] +
            //////                    "' AND [STATE] = '" + currentStateAbbreviation + "'";

            //////            DataSet dsTemp = DA.DataAccess.Read(sql);

            //////            string zipTemp = "";

            //////            foreach (DataRow drTemp in dsTemp.Tables[0].Rows)
            //////            {
            //////                if (zipTemp != "")
            //////                    zipTemp = zipTemp + ", ";

            //////                zipTemp = zipTemp + drTemp["ZIP_CODE"].ToString();
            //////            }

            //////            DataRow drNew = returnDT.NewRow();

            //////            drNew["CityName"] = drFull["CityName"];
            //////            drNew["State"] = currentStateAbbreviation;
            //////            drNew["Current Zip Code"] = drFull["fk_ZipID"];
            //////            drNew["Possible Zip Codes"] = zipTemp;

            //////            returnDT.Rows.Add(drNew);
            //////        }

            //////    }
            //////}


            //string CityName = "", StateAbbreviation = "", ZipCode = "";

            //foreach (DataRow dr in dsCityList.Tables[0].Rows)
            //{
            //    CityName = "";
            //    StateAbbreviation = "";
            //    ZipCode = "";

            //    CityName = dr["CityName"].ToString();
            //    StateAbbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(dr["fk_StateID"].ToString()));
            //    ZipCode = dr["fk_ZipID"].ToString();

            //    // check if city/zip match
            //    try
            //    {
            //        Location tempLoc = ZipCodeUtil.LookupByCityState(CityName, StateAbbreviation);
            //    }
            //    catch
            //    {
            //        if (1 == 1)
            //        {
            //            DataRow drNew = returnDT.NewRow();

            //            drNew[0] = CityName;
            //            drNew[1] = ZipCode;
            //            drNew[2] = StateAbbreviation;

            //            returnDT.Rows.Add(drNew);
            //        }
            //    }
            //}

            return returnDT;

        }

        private static DataSet GetCityList()
        {
            string sql = "SELECT DistName, BillingAddress, BillingCityName, fk_BillingZipID, ShippingAddress, " +
            "CityName, fk_ZipID, fk_StateID, MainDistributor FROM Distributor";
            DataSet ds = DA.DataAccess.Read(sql);

            return ds;
        }

            public static DataSet GetCitiesInZipCode(int p_zipCode)
            {
                string sql = "SELECT CITY_ALIAS_NAME FROM [DL.ZipLookup] WHERE ZIP_CODE = '" + p_zipCode.ToString() + "'";
                DataSet ds = DA.DataAccess.Read(sql);

                return ds;

            }

        #endregion

    }
}
