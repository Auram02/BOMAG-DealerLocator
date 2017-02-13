using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class BlockUserCompany : DLAdminPage
    {

        private string SEARCH_RESULTS_DATASOURCE = "SEARCH_RESULTS_DATASOURCE";

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click +=new EventHandler(btnSearch_Click);
            gdvSearchResults.PageIndexChanging += new GridViewPageEventHandler(gdvSearchResults_PageIndexChanging);
            gdvSearchResults.RowCommand += new GridViewCommandEventHandler(gdvSearchResults_RowCommand);

            gvLeadBlackList.RowCommand+=new GridViewCommandEventHandler(gvLeadBlackList_RowCommand);

            if (!Page.IsPostBack)
            {
                BindBlackListDatasource();
            }

        }

        void gvLeadBlackList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblResult.Text = string.Empty;

            if (e.CommandName == "UnBlockUser")
            {
                GridViewRow dr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                HiddenField leadBlackListId = (HiddenField)dr.FindControl("hdnLeadBlackListId");

                Dealer_Locator.DA.LeadBlackList lbl = new DA.LeadBlackList();

                string deleteResult = lbl.DeleteItem(Convert.ToInt32(leadBlackListId.Value));


                if (deleteResult.Length == 0)
                {
                    lblResult.Text = "User Un-Blocked Successfully";
                }
                else
                {
                    lblResult.Text = deleteResult;
                }
                
                BindBlackListDatasource();

            }
        }
        


        void gdvSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblResult.Text = string.Empty;

            if (e.CommandName == "BlockUser")
            {
                GridViewRow dr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);


                Label lastName = (Label)dr.FindControl("lblLastName");
                Label phone = (Label)dr.FindControl("lblPhone");
                Label city = (Label)dr.FindControl("lblCity");
                Label state = (Label)dr.FindControl("lblState");
                Label zip = (Label)dr.FindControl("lblZip");

                string phoneNumber = Regex.Replace(phone.Text, @"[^\d]", "");

                

                Dealer_Locator.DA.LeadBlackList lbl = new DA.LeadBlackList();

                string addResult = lbl.AddItem(lastName.Text, phoneNumber, city.Text, state.Text, zip.Text);

                if (addResult.Length == 0)
                {
                    lblResult.Text = "User Blocked Successfully";
                }
                else
                {
                    lblResult.Text = addResult;
                }
                BindBlackListDatasource();

            }
        }

        void gdvSearchResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = new DataTable();

            if (Session[SEARCH_RESULTS_DATASOURCE] != null)
            {
                dt = (DataTable)Session[SEARCH_RESULTS_DATASOURCE];
            }
            else
            {
                dt = GetDataSource();
            }

            gdvSearchResults.PageIndex = e.NewPageIndex;
            gdvSearchResults.DataSource = dt;
            gdvSearchResults.DataBind();
        }

        private void BindBlackListDatasource()
        {

            Dealer_Locator.DA.LeadBlackList lbl = new DA.LeadBlackList();

            DataTable dt = lbl.GetBlackList();

            gvLeadBlackList.DataSource = dt;
            gvLeadBlackList.DataBind();

            
        }

        private DataTable GetDataSource()
        {

            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);

            SqlCommand comm = new SqlCommand("EXECUTE LeadValuesSelectByLastName @lastName", sqlConn);
            comm.Parameters.Add(new SqlParameter("lastName", txtLastName.Text));

            da.SelectCommand = comm;

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("Zip");
            dt.Columns.Add("Phone");

            DataRow newRow = dt.NewRow();

            int counter = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                counter += 1;
                newRow[dr["elementName"].ToString()] = dr["elementValue"];

                if (counter == 6)
                {
                    dt.Rows.Add(newRow);
                    newRow = dt.NewRow();
                    counter = 0;
                }

            }

            Session[SEARCH_RESULTS_DATASOURCE] = dt;

            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt =  GetDataSource();


            gdvSearchResults.DataSource = dt;
            gdvSearchResults.DataBind();

            
            
        }

    }
}