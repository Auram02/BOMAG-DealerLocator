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
    public partial class ViewErrorEmails : System.Web.UI.Page
    {
        DA.LeadsTDS.DL_LeadEmailRow ler;
        int emailID;

        protected void Page_Load(object sender, EventArgs e)
        {
            emailID = Convert.ToInt32(Request.QueryString["emailID"].ToString());

            DA.LeadsTDS.DL_LeadEmailDataTable ledt = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();

            ledt = leta.GetDataByEmailID(emailID);

            DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter letta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();

            

            ler = ((DA.LeadsTDS.DL_LeadEmailRow)ledt.Rows[0]);

            if (Page.IsPostBack == false)
            {
                SetText(ler.emailTo, ler.emailFrom, ler.emailBCC, ler.emailCC, ler.emailSubject, ler.emailBody, letta.GetEmailTypeByID(ler.fk_emailType).ToString());
            }

        }

        public void SetText(string to, string from, string bcc, string cc, string subject, string body, string type)
        {
            // replace newlines with br
            body = body.Replace(Environment.NewLine, "<BR>");
            body = body.Replace("\n", "<BR>");
            //body = body.Replace(" ", "&nbsp;");
            //body = body.Replace("\t", "<img src=\"../Images/spacer.gif\" width=\"5px\" height=\"5px\">");
            body = body.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");

            txtTo.Text = to;
            litFrom.Text = from;
            txtBCC.Text = bcc;
            txtCC.Text = cc;
            litSubject.Text = subject;
            litBody.Text = body;
            litType.Text = type;
            

            switch (type)
            {
                case "Distributor Fax":
                    tblEmail.BackColor = System.Drawing.Color.FromArgb(109, 185, 249);
                    break;

                case "Territory Manager":
                    tblEmail.BackColor = System.Drawing.Color.FromArgb(109, 249, 152);
                    break;

                case "Distribution Vendor":
                    tblEmail.BackColor = System.Drawing.Color.FromArgb(249, 208, 109);
                    break;

                case "User Email":
                    tblEmail.BackColor = System.Drawing.Color.FromArgb(254, 255, 139);
                    break;

                default:
                    break;
            }

        }

        protected void btnResendLead_Click(object sender, EventArgs e)
        {
            BR.Lead myLead = new Dealer_Locator.BR.Lead(false);
            BR.Lead.LeadEmail leadEmail = new Dealer_Locator.BR.Lead.LeadEmail();

            leadEmail.emailTo = txtTo.Text;

            Page myPage = new Page();

            DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter letta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();

            string error = "";

            if (litType.Text == "Distributor Fax")
            {
                //error = myLead.SendFax(ler.emailFrom, txtTo.Text, txtCC.Text, "", ler.emailBody, ler.emailSubject, "findbomagco", letta.GetEmailTypeByID(ler.fk_emailType));
                error = myLead.SendEmail(ler.emailFrom, txtTo.Text, "",  ler.emailBody, ler.emailSubject, "findbomagco", letta.GetEmailTypeByID(ler.fk_emailType).ToString(), ref myPage);
            }
            else
            {
                error = myLead.SendEmail(ler.emailFrom, txtTo.Text, txtCC.Text, ler.emailBody, ler.emailSubject, "findbomagco", letta.GetEmailTypeByID(ler.fk_emailType).ToString(), ref myPage);
            }

            if (error == "")
            {
                error = "Lead Email Sent Successfully!";
            }
            else
            {
                error = "An error occurred while sending the email." + Environment.NewLine + Environment.NewLine + "Error Message: " + error;

            }

            lblResult.Text = error;


            DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();

            leta.UpdateLeadEmail(txtTo.Text, txtCC.Text, txtBCC.Text, emailID);

        }

    }
}
