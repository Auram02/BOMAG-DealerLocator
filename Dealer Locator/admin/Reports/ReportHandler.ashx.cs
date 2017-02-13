using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;

using System.Data;
using System.Data.SqlClient;


namespace Dealer_Locator.admin.Reports
{
    /// <summary>
    /// Summary description for ReportHandler
    /// </summary>
    public class ReportHandler : IHttpHandler
    {
        SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

        public void ProcessRequest(HttpContext context)
        {
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            string response = "";

            string action = context.Request["action"].ToString();
            //string stateIDs = context.Request["states"].ToString();
            //string mainCategories = context.Request["categories"].ToString();
            int repID;
            switch (action)
            {
                case "GetStates":
                    repID = Convert.ToInt32(context.Request["repID"].ToString());
                    List<ResponseObject> returnValues = GetStates(repID);

                    response = js.Serialize(returnValues);
                    break;

                case "GetProductLines":
                    int distID = Convert.ToInt32(context.Request["DistributorID"].ToString());
                    List<ResponseObject> returnValues3 = GetMainCategories(distID);
                    response = js.Serialize(returnValues3);
                    break;

                case "GetProductLinesByStateList":

                    repID = Convert.ToInt32(context.Request["repID"].ToString());
                    List<string> stateList2 = new List<string>();
                    string selectedStateList2 = context.Request["stateNameList[]"].ToString();

                    foreach (string tempStateName in selectedStateList2.Split(','))
                        stateList2.Add(tempStateName);

                    List<ResponseObject> returnValues4 = GetMainCategoriesByStateList(repID, stateList2);
                    response = js.Serialize(returnValues4);
                    break;

                case "GetDistributors":
                    repID = Convert.ToInt32(context.Request["repID"].ToString());

                    List<ResponseObject> returnValues2 = GetDistributors(repID);
                    response = js.Serialize(returnValues2);
                    break;

                case "TMMapReport1":

                    List<string> stateList = new List<string>();
                    string selectedStateList = context.Request["stateNameList[]"].ToString();

                    foreach (string tempStateName in selectedStateList.Split(','))
                        stateList.Add(tempStateName);
                    
                    int categoryID = Convert.ToInt32(context.Request["categoryID"].ToString());
                    repID = Convert.ToInt32(context.Request["repID"].ToString());
                    string categoryName = context.Request["categoryName"].ToString();

                    string reportID = GenerateMapReport1(repID, stateList, categoryID, categoryName);

                    response = js.Serialize(reportID);

                    break;


                case "TMMapReport2":


                    string cats = context.Request["categoryIDList[]"].ToString();
                    List<int> categoryIDList = new List<int>();

                    foreach (string tempCategoryID in cats.Split(','))
                        categoryIDList.Add(Convert.ToInt32(tempCategoryID));

                    int distributorID = Convert.ToInt32(context.Request["distributorID"].ToString());
                    repID = Convert.ToInt32(context.Request["repID"].ToString());

                    List<ResponseObject> result = GenerateMapReport2(categoryIDList, distributorID.ToString(), repID);
                    response = js.Serialize(result);

                    break;

                case "OverviewReport":


                    string catsOverview = context.Request["categoryIDList[]"].ToString();
                    List<int> categoryIDOverviewList = new List<int>();

                    foreach (string tempCategoryID in catsOverview.Split(','))
                        categoryIDOverviewList.Add(Convert.ToInt32(tempCategoryID));

                    List<ResponseObject> resultOverview = GenerateOverviewMapReport(categoryIDOverviewList);
                    response = js.Serialize(resultOverview);

                    break;

                default:
                    break;
            }

            context.Response.Write(response);

        }

        private string GenerateMapReport1(int repID, List<string> stateList, int categoryID, string categoryName)
        {
            DataSet ds = ReportGeneration.Reports.GenerateContractListByCategoryListStateListRepID(categoryID, stateList, repID);

            Dealer_Locator.admin.WebServices.MapReportService svc = new WebServices.MapReportService();

            string reportID = svc.UploadReportData(ds, categoryName, stateList);

            reportID = reportID.Substring(0, reportID.IndexOf(".json"));

            return reportID;
        }

        private List<ResponseObject> GenerateMapReport2(List<int> categoryIDList, string distributorID, int territoryRepID)
        {
            List<ResponseObject> result = new List<ResponseObject>();

            foreach (int categoryID in categoryIDList)
            {
                List<string> stateList = new List<string>();

                DataSet ds = ReportGeneration.Reports.GenerateContractListByCategoryIDListDistributorIDList(categoryID, distributorID, ref stateList, territoryRepID);
                Dealer_Locator.admin.WebServices.MapReportService svc = new WebServices.MapReportService();

                string categoryName = "";
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

                categoryName = mcta.GetMainCategoryNameByID(categoryID).ToString();

                string reportID = svc.UploadReportData(ds, categoryName, stateList);
                reportID = reportID.Substring(0, reportID.IndexOf(".json"));

                ResponseObject tempObj = new ResponseObject();
                tempObj.Key = reportID;
                tempObj.Value = reportID;
                result.Add(tempObj);
            }

            return result;
        }

        private List<ResponseObject> GenerateOverviewMapReport(List<int> categoryIDList)
        {
            List<ResponseObject> result = new List<ResponseObject>();

            foreach (int categoryID in categoryIDList)
            {
                List<string> stateList = new List<string>();

                DataSet ds = ReportGeneration.Reports.GenerateOverview(categoryID, ref stateList);
                Dealer_Locator.admin.WebServices.MapReportService svc = new WebServices.MapReportService();

                string categoryName = "";
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

                categoryName = mcta.GetMainCategoryNameByID(categoryID).ToString();

                DA.State.StateDataTable dt = new DA.State.StateDataTable();
                DA.StateTableAdapters.StateTableAdapter ta = new DA.StateTableAdapters.StateTableAdapter();

                ta.Fill(dt);

                stateList = new List<string>();
                foreach ( DA.State.StateRow dr in dt.Rows)
                {
                    stateList.Add(dr["FullName"].ToString());
                }

                string reportID = svc.UploadReportData(ds, categoryName, stateList);
                reportID = reportID.Substring(0, reportID.IndexOf(".json"));

                ResponseObject tempObj = new ResponseObject();
                tempObj.Key = reportID;
                tempObj.Value = reportID;
                result.Add(tempObj);
            }

            return result;
        }

        private struct ResponseObject
        {
            public string Key;
            public string Value;
        }

        private List<ResponseObject> GetStates(int repID)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
            SqlCommand comm = new SqlCommand("StateListByTerritoryRep", sqlConn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@TerritoryRepID", repID);

            da2.SelectCommand = comm;
            da2.Fill(ds);

            List<ResponseObject> returnValues = new List<ResponseObject>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ResponseObject tempObject = new ResponseObject();

                tempObject.Key = dr["StateID"].ToString();
                tempObject.Value = dr["FullName"].ToString();

                returnValues.Add(tempObject);
            }

            return returnValues;
        }

        private List<ResponseObject> GetMainCategories(int distributorID)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
            SqlCommand comm = new SqlCommand("MainCategoryByDistributorID", sqlConn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@DistributorID", distributorID);

            da2.SelectCommand = comm;
            da2.Fill(ds);

            List<ResponseObject> returnValues = new List<ResponseObject>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ResponseObject tempObject = new ResponseObject();

                tempObject.Key = dr["pk_mainCatID"].ToString();
                tempObject.Value = dr["CategoryName"].ToString();

                returnValues.Add(tempObject);
            }

            return returnValues;
        }

        private List<ResponseObject> GetMainCategoriesByStateList(int territoryRepID, List<string> stateList)
        {
            string stateIDs = "";

            foreach (string state in stateList)
            {
                if (stateIDs.Length > 0)
                    stateIDs += ",";

                DA.StateTableAdapters.StateTableAdapter ta = new DA.StateTableAdapters.StateTableAdapter();
                stateIDs += ta.GetStateIDByFullName(state).ToString();
            }


            DataSet ds = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
            SqlCommand comm = new SqlCommand("MainCategoryByStateListTerritoryRepID", sqlConn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@StateIDList", stateIDs);
            comm.Parameters.AddWithValue("@TerritoryRepID", territoryRepID);
            
            da2.SelectCommand = comm;
            da2.Fill(ds);

            List<ResponseObject> returnValues = new List<ResponseObject>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ResponseObject tempObject = new ResponseObject();

                tempObject.Key = dr["pk_mainCatID"].ToString();
                tempObject.Value = dr["CategoryName"].ToString();

                returnValues.Add(tempObject);
            }

            return returnValues;
        }


        private string GetProductLines(int repID)
        {

            string result = "";
            return result;
        }

        private List<ResponseObject> GetDistributors(int repID)
        {
            
            DataSet ds = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
            SqlCommand comm = new SqlCommand("DistributorsSelectByTerritoryRep", sqlConn);
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.AddWithValue("@TerritoryRepID", repID);

            da2.SelectCommand = comm;
            da2.Fill(ds);

            List<ResponseObject> returnValues = new List<ResponseObject>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ResponseObject tempObj = new ResponseObject();

                tempObj.Key = dr["pk_DistributorID"].ToString();
                tempObj.Value = dr["DistName"].ToString();
                returnValues.Add(tempObj);
            }
            
            return returnValues;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}