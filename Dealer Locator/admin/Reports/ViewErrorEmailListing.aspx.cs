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
    public partial class ViewErrorEmailListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int leadID = Convert.ToInt32(Request.QueryString["leadID"]);

                DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();
                DA.LeadsTDS.DL_LeadEmailDataTable ledt = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();

                ledt = leta.GetDataByLeadID(leadID);

                tblErrorEmailListing.Rows.Clear();

                CreateHeader();

                int counter = 0;
                foreach (DA.LeadsTDS.DL_LeadEmailRow dr in ledt.Rows)
                {
                    AddLeadEmailRow(dr, counter);

                    if (counter == 0)
                        counter = 1;
                    else
                        counter = 0;

                }

            }

        }

        private void CreateHeader()
        {


            TableRow tr = new TableRow();


            tr.BackColor = System.Drawing.Color.LightGray;



            TableCell tc = new TableCell();
            tc.Text = "Email Type";
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "View Email";
            tr.Cells.Add(tc);

            tblErrorEmailListing.Rows.Add(tr);
            // .Rows.Add(tr);

        }


        private void AddLeadEmailRow(DA.LeadsTDS.DL_LeadEmailRow dr, int counter)
        {
            System.Drawing.Color rowColor = GetRowColor(counter);

            DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter letta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();

            string status = letta.GetEmailTypeByID(dr.fk_emailType).ToString();

            TableRow tr = new TableRow();
            tr.BackColor = rowColor;

            TableCell tc = new TableCell();
            tc.Text = status;

            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = "<a href=\"ViewErrorEmail.aspx?emailID=" + dr.pk_leadEmailID.ToString() + "\">View Email</a>";

            tr.Cells.Add(tc);


            tblErrorEmailListing.Rows.Add(tr);

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
