using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class DeleteLead : System.Web.UI.UserControl
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            //bool LeadRemovalResult = BR.Lead.RemoveLead(leadID);

            if (!Page.IsPostBack)
            {
                LeadsTable.Visible = false;
                tblConfirm.Visible = false;
                lblResult.Visible = false;
            }
        }

        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {
            lblResult.Visible = false;

            BindGrid();

        }

        private void BindGrid()
        {
            LeadsTable.Visible = true;

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();

            ldt = BR.Lead.GetLeadsByLastName(txtLastName.Text);

            DataColumn dc0 = new DataColumn();
            dc0.ColumnName = "pk_leadID";

            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "First Name";

            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "Last Name";

            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "City";

            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "State";

            DataColumn dc5 = new DataColumn();
            dc5.ColumnName = "Zip Code";

            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);


            foreach (DA.LeadsTDS.DL_LeadRow dr in ldt.Rows)
            {

                BR.Lead mylead = new Dealer_Locator.BR.Lead(dr.pk_leadID);

                string firstName = "", lastName = "", city = "", state = "", zip = "";

                foreach (BR.Lead.LeadValue item in mylead._leadValues)
                {
                    if (item.elementName.ToLower().Contains("firstname"))
                        firstName = item.elementValue;

                    if (item.elementName.ToLower().Contains("lastname"))
                        lastName = item.elementValue;

                    if (item.elementName.ToLower().Contains("city"))
                        city = item.elementValue;

                    if (item.elementName.ToLower().Contains("state"))
                        state = item.elementValue;

                    if (item.elementName.ToLower().Contains("zip"))
                        zip = item.elementValue;

                }

                DataRow drNew = dt.NewRow();
                drNew["pk_leadID"] = dr.pk_leadID;
                drNew["First Name"] = firstName;
                drNew["Last Name"] = lastName;
                drNew["City"] = city;
                drNew["State"] = state;
                drNew["Zip Code"] = zip;

                dt.Rows.Add(drNew);
            }

            // gvLeads = new GridView();
            GridView1.Visible = true;
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {

                //e.Row.Cells[1].Visible = false;
                // updated the indexes
                e.Row.Cells[1].Visible = false;


            }
            catch
            {

            }
        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("RemoveLead"))
            {

                tblConfirm.Visible = true;

                int leadID = 0;

                leadID = Convert.ToInt32(GridView1.Rows[rowIndex].Cells[1].Text);

                lblConfirm.Text = "Are you sure you wish to remove the lead submitted by " + GridView1.Rows[rowIndex].Cells[2].Text + " " + GridView1.Rows[rowIndex].Cells[3].Text + "?";

                lblLeadID.Text = leadID.ToString();

                lblResult.Visible = false;

            }

        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lblLeadID.Text != "")
            {

                bool result = BR.Lead.RemoveLead(Convert.ToInt32(lblLeadID.Text));

                BindGrid();

                tblConfirm.Visible = false;


                lblResult.Visible = true;

                if (result == true)
                {
                    lblResult.Text = "Lead Removed Successfully!";
                }
                else
                {
                    lblResult.Text = "There was an error removing the lead with ID " + lblLeadID.Text + ".  Please contact the developer with this Lead ID.";
                }

                lblLeadID.Text = "";


            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            lblLeadID.Text = "";

            lblResult.Text = "Lead Removal Cancelled";
            lblResult.Visible = true;

            tblConfirm.Visible = false;
        }

    }
}