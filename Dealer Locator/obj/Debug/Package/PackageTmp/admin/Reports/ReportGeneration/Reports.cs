using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Dealer_Locator.admin.Reports.ReportGeneration
{
    public class Reports
    {

        public static DataSet GenerateContractListByCategoryListStateListRepID(int categoryID, List<string> stateList, int territoryRepID)
        {
            string stateListString = "";

            foreach (string state in stateList)
            {
                if (stateListString.Length > 0)
                    stateListString += ",";

                stateListString += "'" + state + "'";
            }

            DataTable dtSource = DA.Report_da.GetContractListByCategoryListStateListRepID(categoryID.ToString(), stateListString, territoryRepID).Tables[0];

            return GenerateGroupData(dtSource, categoryID, stateListString, territoryRepID);
        }

        public static DataSet GenerateContractListByCategoryIDListDistributorIDList(int categoryID, string distributorID, ref List<string> stateList, int territoryRepID)
        {
            string stateListString = "";

           
            // loop all states and get list together here
            DataTable dtSource = DA.Report_da.GetContractListByCategoryListDistributorList(categoryID.ToString(), distributorID).Tables[0];

            foreach (DataRow dr in dtSource.Rows)
            {
                if (stateList.Contains(dr["FullName"].ToString()) == false)
                {
                    if (stateListString.Length > 0)
                        stateListString += ",";

                    stateListString += "'" + dr["FullName"].ToString() + "'";
                    stateList.Add(dr["FullName"].ToString());
                }
            }

            return GenerateGroupData(dtSource, categoryID, stateListString, territoryRepID);
        }

        public static DataSet GenerateOverview(int categoryID, ref List<string> stateList)
        {
            string stateListString = "";

           
            // loop all states and get list together here
            DataTable dtSource = DA.Report_da.GetOverviewReportData(categoryID.ToString()).Tables[0];

            foreach (DataRow dr in dtSource.Rows)
            {
                if (stateList.Contains(dr["FullName"].ToString()) == false)
                {
                    if (stateListString.Length > 0)
                        stateListString += ",";

                    stateListString += "'" + dr["FullName"].ToString() + "'";
                    stateList.Add(dr["FullName"].ToString());
                }
            }

            return GenerateGroupData(dtSource, categoryID, stateListString);
        }
        

        public static DataTable GetGroupDataTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "GroupCountyData";
            dt.Columns.Add("Name");
            dt.Columns.Add("GroupID");
            dt.Columns.Add("CountyID");
            dt.Columns.Add("Color");
            dt.Columns.Add("Abbreviation");
            dt.Columns.Add("CountyName");
            dt.Columns.Add("StateName");
            dt.Columns.Add("GroupStateName");
            dt.Columns.Add("GroupStateAbbreviation");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("Zip");

            dt.Columns.Add("IsSplit");
            dt.Columns.Add("SplitType");
            dt.Columns.Add("SplitLine");
            dt.Columns.Add("SplitCountyName");

            return dt;
        }


        public static DataTable GetSubGroupDataTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "SubGroupData";
            dt.Columns.Add("GroupID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("Abbreviation");
            dt.Columns.Add("StateName");
            dt.Columns.Add("Zip");
            dt.Columns.Add("ContractState");
            dt.Columns.Add("Lat");
            dt.Columns.Add("Lng");
            dt.Columns.Add("GeoLastUpdated");
            dt.Columns.Add("pk_DistributorID");
            
            return dt;
        }


        public static DataSet GenerateGroupData(DataTable dtSource, int categoryID, string stateListString = "", int territoryRepID = 0)
        {
            DataSet ds = new DataSet();
            DataTable dt = GetGroupDataTemplate();
            DataTable dtSubGroups = GetSubGroupDataTemplate();

            List<string> states = new List<string>();

            List<int> distIDs = new List<int>();

            DataSet dsSplit = DA.Report_da.GetSplitList();

            foreach (DataRow dr in dtSource.Rows)
            {

                string groupName = dr["DistName"].ToString();
                int groupID = Convert.ToInt32(dr["pk_DistributorID"].ToString());
                int countyID = Convert.ToInt32(dr["CountyID"].ToString());
                string color = "";
                string abbreviation = dr["Abbreviation"].ToString();
                string countyName = dr["CountyName"].ToString();
                string stateName = dr["FullName"].ToString();
                string groupStateAbbreviation = dr["GroupStateAbbreviation"].ToString();
                string groupStateName = dr["GroupStateName"].ToString();
                string city = dr["CityName"].ToString();
                string address = dr["ShippingAddress"].ToString();
                string zip = dr["fk_ZipID"].ToString();

                DataRow newRow = dt.NewRow();
                newRow["Name"] = groupName;
                newRow["GroupID"] = groupID;
                newRow["CountyID"] = countyID;
                newRow["Abbreviation"] = abbreviation;
                newRow["CountyName"] = countyName;
                newRow["StateName"] = stateName;
                newRow["GroupStateName"] = groupStateName;
                newRow["GroupStateAbbreviation"] = groupStateAbbreviation;
                newRow["Color"] = color;
                newRow["Address"] = address;
                newRow["City"] = city;
                newRow["Zip"] = zip;
                newRow["IsSplit"] = false;
                newRow["SplitType"] = "";
                newRow["SplitLine"] = "";
                newRow["SplitCountyName"] = "";

                foreach (DataRow drSplit in dsSplit.Tables[0].Rows)
                {
                    if (drSplit["fk_fakeCountyID"].ToString() == countyID.ToString())
                    {
                        newRow["IsSplit"] = true;
                        string splitType = "";
                        string splitLine = "";

                        if (drSplit["NorthSouth"].ToString() == "North")
                        {
                            splitType = "North";
                            splitLine = drSplit["latitude"].ToString();
                        }

                        if (drSplit["NorthSouth"].ToString() == "South")
                        {
                            splitType = "South";
                            splitLine = drSplit["latitude"].ToString();
                        }

                        if (drSplit["EastWest"].ToString() == "East")
                        {
                            splitType = "East";
                            splitLine = drSplit["longitude"].ToString();
                        }

                        if (drSplit["EastWest"].ToString() == "West")
                        {
                            splitType = "West";
                            splitLine = drSplit["longitude"].ToString();
                        }
                        newRow["SplitType"] = splitType;
                        newRow["SplitLine"] = splitLine;

                        newRow["SplitCountyName"] = drSplit["CountyName"].ToString();

                    }
                }


                dt.Rows.Add(newRow);


                if (distIDs.Contains(groupID) == false)
                {
                    distIDs.Add(groupID);
                    DataSet dsBranches;

                    if (territoryRepID==0)
                        dsBranches = DA.Report_da.GetDistributorBranchListWithContractStates(groupID, categoryID, stateListString);
                    else
                        dsBranches = DA.Report_da.GetDistributorBranchListWithContractStates(groupID, categoryID, stateListString, territoryRepID);

                    foreach (DataRow drSubGroup in dsBranches.Tables[0].Rows)
                    {
                        DataRow drNewSubGroup = dtSubGroups.NewRow();

                        drNewSubGroup["GroupID"] = groupID;
                        drNewSubGroup["Name"] = drSubGroup["DistName"].ToString();
                        drNewSubGroup["Address"] = drSubGroup["ShippingAddress"].ToString();
                        drNewSubGroup["City"] = drSubGroup["CityName"];
                        drNewSubGroup["Abbreviation"] = drSubGroup["Abbreviation"];
                        drNewSubGroup["StateName"] = drSubGroup["FullName"];
                        drNewSubGroup["Zip"] = drSubGroup["fk_ZipID"];
                        drNewSubGroup["ContractState"] = drSubGroup["ContractState"];
                        drNewSubGroup["Lat"] = drSubGroup["Lat"];
                        drNewSubGroup["Lng"] = drSubGroup["Lng"];
                        drNewSubGroup["GeoLastUpdated"] = drSubGroup["GeoLastUpdated"];
                        drNewSubGroup["pk_DistributorID"] = drSubGroup["pk_DistributorID"];

                        dtSubGroups.Rows.Add(drNewSubGroup);
                    }
                }

            }


            ds.Tables.Add(dt);
            ds.Tables.Add(dtSubGroups);
            return ds;

        }


    }
}