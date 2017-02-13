using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Dealer_Locator.admin.Reports.ReportGeneration.DA
{
    public class Report_da
    {

        /// <summary>
        /// Gets list of contracts restricted by categoryID list, StateID list, and territoryRepID
        /// </summary>
        /// <param name="categoryIDList"></param>
        /// <param name="stateIDList"></param>
        /// <param name="territoryRepID"></param>
        /// <returns></returns>
        public static DataSet GetContractListByCategoryListStateListRepID(string categoryIDList, string stateIDList, int territoryRepID)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, " +
                    " CategoryName, AllowTerritoryOverlap, pk_DistributorID, Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, " +
                    " '' AS GroupStateName, '' AS GroupStateAbbreviation, Distributor.fk_StateID, dr.fk_TerritoryRepID " +
                    " FROM Contract, County, ContractCounty, ContractCategory cc, Category cat, Distributor, State s1, State s2, DistributorRepresentative dr " +
                    " WHERE Contract.ContractID = ContractCounty.fk_ContractID " +
                    " AND County.CountyID = ContractCounty.fk_CountyID " +
                    " AND County.CountyID IN ( " +
                            " SELECT DISTINCT CountyID FROM County WHERE fk_StateID IN ( SELECT StateID FROM [State] WHERE FullName in (" + stateIDList + ") ) " +
                    " ) " +
                    " AND cc.fk_ContractID = ContractID " +
                    " AND CategoryID = cc.fk_CategoryID " +
                    " AND CategoryID IN ( " + categoryIDList + " ) " +
                    " AND Distributor.pk_DistributorID LIKE MainDistributorID " +
                    " AND s1.StateID = County.fk_StateID " +
                    " AND dr.fk_TerritoryRepID = " + territoryRepID +
                    " GROUP BY ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap, pk_DistributorID, " +
                    " Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, Distributor.fk_StateID, dr.fk_TerritoryRepID ";


            ds = Dealer_Locator.DA.DataAccess.Read(sql);
            ds = FillMapReportStates(ds);

            return ds;
        }

        /// <summary>
        /// Gets list of all contracts restricted by a categoryID list and a distributorID list
        /// </summary>
        /// <param name="categoryList"></param>
        /// <param name="distributorList"></param>
        /// <returns></returns>
        public static DataSet GetContractListByCategoryListDistributorList(string categoryIDList, string distributorIDList)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, " +
                    " CategoryName, AllowTerritoryOverlap, pk_DistributorID, Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, " +
                    " '' AS GroupStateName, '' AS GroupStateAbbreviation, Distributor.fk_StateID " +
                    " FROM Contract, County, ContractCounty, ContractCategory cc, Category cat, Distributor, State s1, State s2, DistributorRepresentative dr " +
                    " WHERE Contract.ContractID = ContractCounty.fk_ContractID   " +
                    " AND County.CountyID = ContractCounty.fk_CountyID   " +
                    " AND cc.fk_ContractID = ContractID   " +
                    " AND CategoryID = cc.fk_CategoryID   " +
                    " AND CategoryID IN ( " + categoryIDList + " )   " +
                    " AND Distributor.pk_DistributorID LIKE MainDistributorID   " +
                    " AND s1.StateID = County.fk_StateID   " +
                    " AND Distributor.pk_DistributorID IN (" + distributorIDList + ") " +
                    " GROUP BY ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap, pk_DistributorID, " +
                    " Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, Distributor.fk_StateID ";


            ds = Dealer_Locator.DA.DataAccess.Read(sql);
            ds = FillMapReportStates(ds);

            return ds;
        }


        public static DataSet GetDistributorBranchListWithContractStates(int p_MainDistID, int categoryID, string stateListString = "", int territoryRepID = 0)
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;

                sql = "SELECT StateID FROM [State] WHERE FullName ";

                if (stateListString.Length > 0)
                    sql += " in ( " + stateListString + " ) ";
                else
                    sql += " = FullName ";


                DataSet ds2 = Dealer_Locator.DA.DataAccess.Read(sql);
                string stateIDList = "";
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    if (stateIDList.Length > 0)
                        stateIDList += ",";

                    stateIDList += dr["StateID"].ToString();
                }

                
                    SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();
                SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
                SqlCommand comm;

                if (territoryRepID == 0)
                {
                    comm = new SqlCommand("GetDistributorBranchListWithContractStates", sqlConn);
                }
                else
                {
                    comm = new SqlCommand("GetDistributorBranchListWithContractStatesTerritoryRepID", sqlConn);
                    comm.Parameters.AddWithValue("@TerritoryRepID", territoryRepID);
                }

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@MainDistributorID", p_MainDistID.ToString());
                comm.Parameters.AddWithValue("@CategoryID", categoryID.ToString());
                
                if (stateListString.Length > 0)
                    comm.Parameters.AddWithValue("@StateList", stateIDList.ToString());

                da2.SelectCommand = comm;
                da2.Fill(ds);


            }
            catch (Exception ex)
            {

            }

            ds.Tables[0].Columns.Add("CountyName");

            return ds;
        }

        public static DataSet GetOverviewReportData(string categoryID)
        {

            DataSet ds = new DataSet();

            try
            {
             
                SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();
                SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
                SqlCommand comm = new SqlCommand("OverviewReport", sqlConn);
                comm.CommandType = CommandType.StoredProcedure;
                
                comm.Parameters.AddWithValue("@CategoryIDList", categoryID.ToString());

                da2.SelectCommand = comm;
                da2.Fill(ds);

            }
            catch (Exception ex)
            {

            }

            //ds.Tables[0].Columns.Add("CountyName");

            return ds;
        }

        public static void UpdateDistributorGeo(int distributorID, double lat, double lng)
        {
            try
            {

                SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();
                SqlCommand comm = new SqlCommand("DistributorUpdateGeo", sqlConn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@DistributorID", distributorID.ToString());
                comm.Parameters.AddWithValue("@lat", lat.ToString());
                comm.Parameters.AddWithValue("@lng", lng.ToString());

                sqlConn.Open();
                comm.ExecuteNonQuery();
                sqlConn.Close();

            }
            catch (Exception ex)
            {

            }

        }

        public static DataSet GetSplitList()
        {

            string sql = "SELECT sc.pk_splitID, sc.fk_countyID, sc.fk_fakeCountyID, sc.latitude, sc.longitude, sc.NorthSouth, sc.EastWest, cty.CountyName " +
                            " FROM SplitCounty sc, County cty " +
                            " WHERE cty.CountyID = sc.fk_countyID";


            return Dealer_Locator.DA.DataAccess.Read(sql);

        }


        public static DataSet FillMapReportStates(DataSet ds)
        {
            DataSet dsStates = DDA.DataAccess.Location_da.GetStateList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drState in dsStates.Tables[0].Rows)
                {
                    if (drState["StateID"].ToString() == dr["fk_StateID"].ToString())
                    {
                        dr["GroupStateName"] = drState["FullName"].ToString();
                        dr["GroupStateAbbreviation"] = drState["Abbreviation"].ToString();

                        break;
                    }
                }
            }

            return ds;
        }





    }
}