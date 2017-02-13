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

namespace Dealer_Locator
{
    public partial class DevSendFax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendFax_Click(object sender, EventArgs e)
        {
            BR.Lead myLead = new Dealer_Locator.BR.Lead(false);

            myLead.SendFax("fax@findbomag.com", txtFaxNumber.Text, "", "smtp.findbomag.com", "This is a test fax my friends", "TEST FAX", "findbomagco", "Territory Manager");
        }
    }
}
