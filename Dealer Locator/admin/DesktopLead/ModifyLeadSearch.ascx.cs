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
    public partial class ModifyLeadSearch1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                editStep2.Visible = false;

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {

            // get lead list...
            LeadList1.SetLastName(txtLastName.Text);

            editStep1.Visible = false;
            editStep2.Visible = true;

        }
    }
}