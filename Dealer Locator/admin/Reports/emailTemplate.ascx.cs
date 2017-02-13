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
    public partial class emailTemplate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetText(string to, string from, string bcc, string cc, string subject, string body, string type)
        {
            // replace newlines with br
            body = body.Replace(Environment.NewLine, "<BR>");
            body = body.Replace("\n", "<BR>");
            //body = body.Replace(" ", "&nbsp;");
            //body = body.Replace("\t", "<img src=\"../Images/spacer.gif\" width=\"5px\" height=\"5px\">");
            body = body.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            litTo.Text = to;
            litFrom.Text = from;
            litBCC.Text = bcc;
            litCC.Text = cc;
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

    }
}