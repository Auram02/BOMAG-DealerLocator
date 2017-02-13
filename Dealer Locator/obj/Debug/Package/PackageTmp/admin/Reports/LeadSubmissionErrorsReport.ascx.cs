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

namespace Dealer_Locator.admin.Reports
{
    public partial class LeadSubmissionErrorsReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
                DisplayFakeLeads();
        }

        private void DisplayFakeLeads()
        {
            tblResendLeads.Rows.Clear();

            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();


            ldt = lta.GetFailedLeads();

            int counter = 0;


            CreateHeader();

            foreach (DA.LeadsTDS.DL_LeadRow dr in ldt.Rows)
            {
                AddLeadRow(dr, counter);

                if (counter == 0)
                    counter = 1;
                else
                    counter = 0;
            }

        }

        private void AddLeadRow(DA.LeadsTDS.DL_LeadRow dr, int counter)
        {

            System.Drawing.Color rowColor = GetRowColor(counter);

            TableRow tr = new TableRow();

            tr.BackColor = rowColor;



            TableCell tc = new TableCell();
            tc.Text = dr.sendDate.ToString();
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = dr.submitDate.ToString();
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = dr.errorValue;
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "<a href=\"ViewErrorEmailListing.aspx?leadID=" + dr.pk_leadID + "\">View Emails</a>";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "<a href=\"MarkAsFixed.aspx?leadID=" + dr.pk_leadID + "\">Mark as Fixed</a>";
            tr.Cells.Add(tc);


            tblResendLeads.Rows.Add(tr);

        }

        private void CreateHeader()
        {


            TableRow tr = new TableRow();


            tr.BackColor = System.Drawing.Color.LightGray;



            TableCell tc = new TableCell();
            tc.Text = "Send Attempt Date";
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "Submitted Date";
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "Error Message";
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "Emails List";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Update Status";
            tr.Cells.Add(tc);


            tblResendLeads.Rows.Add(tr);

        }


        /// <summary>
        /// Gets color
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        private System.Drawing.Color GetRowColor(int counter)
        {
            System.Drawing.Color rowColor = new System.Drawing.Color();

            if (counter == 0)
            {
                rowColor = System.Drawing.Color.FromArgb(212, 215, 233);
                counter = 1;
            }
            else
            {
                rowColor = System.Drawing.Color.FromArgb(163, 175, 216);
                counter = 0;
            }

            return rowColor;

        }
    }
}