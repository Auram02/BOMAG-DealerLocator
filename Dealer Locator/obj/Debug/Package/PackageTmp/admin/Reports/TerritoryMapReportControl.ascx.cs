using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace Dealer_Locator.admin
{
    public partial class TerritoryMapReportControl : System.Web.UI.UserControl
    {

        public bool IsAdmin
        {
            get
            {
                return Convert.ToBoolean(Request.Cookies["Authenticated"].Values["admin"].ToString());
            }
        }
        public bool IsRep
        {
            get
            {
                return Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsRep"].ToString());
            }
        }

        public int TerritoryRepID
        {
            get
            {
                return Convert.ToInt32(Request.Cookies["Authenticated"].Values["repID"].ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            string script = "";

            DataSet ds = new DataSet();
            if (IsAdmin)
            {
                //
                SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
                SqlCommand comm = new SqlCommand("RepresentativeGetTerritoryRep", sqlConn);
                comm.CommandType = CommandType.StoredProcedure;

                da2.SelectCommand = comm;
                da2.Fill(ds);

                TMList.AppendDataBoundItems = true;
                TMList.DataTextField = "RepName";
                TMList.DataValueField = "RepID";
                TMList.DataSource = ds.Tables[0];
                TMList.DataBind();

                script = "ShowAdminInterface();";

            } else if (IsRep)
            {

                // StateListByTerritoryRep
               
                SqlDataAdapter da2 = new SqlDataAdapter("", sqlConn);
                SqlCommand comm = new SqlCommand("StateListByTerritoryRep", sqlConn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@TerritoryRepID", TerritoryRepID);

                da2.SelectCommand = comm;
                da2.Fill(ds);

                StatesList1.DataTextField = "FullName";
                StatesList1.DataValueField = "StateID";
                StatesList1.DataSource = ds.Tables[0];
                StatesList1.DataBind();


                script = "ShowTerritoryRepInterface(" + TerritoryRepID + ");";

            }

            DA.MainCategoryTDS.DL_MainCategoryDataTable dt = new DA.MainCategoryTDS.DL_MainCategoryDataTable();
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter da = new DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            dt = da.GetData_nonDisabled();

            ProductLinesList1.DataTextField = "categoryName";
            ProductLinesList1.DataValueField = "pk_mainCatID";
            ProductLinesList1.DataSource = dt;
            ProductLinesList1.DataBind();

            TMProductLinesOverviewList.DataTextField = "categoryName";
            TMProductLinesOverviewList.DataValueField = "pk_mainCatID";
            TMProductLinesOverviewList.DataSource = dt;
            TMProductLinesOverviewList.DataBind();
            

            //ProductLinesList2.DataTextField = "categoryName";
            //ProductLinesList2.DataValueField = "pk_mainCatID";
            //ProductLinesList2.DataSource = dt;
            //ProductLinesList2.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UIInitialization", script, true);
        }
    }
}