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

namespace Dealer_Locator.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int errId = Convert.ToInt32(Request.QueryString["errID"]);

            if (errId == 1)
            {
                lblError.Text = "You do not have access to that page.  Please select a page from the menu.";
            }
        }
    }
}
