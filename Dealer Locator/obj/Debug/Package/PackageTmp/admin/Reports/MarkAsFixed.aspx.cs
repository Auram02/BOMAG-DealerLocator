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
    public partial class MarkAsFixed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int leadID = Convert.ToInt32(Request.QueryString["leadID"].ToString());

            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            lta.SetLeadFixed(leadID);

            Response.Redirect("ReportsList.aspx");

        }
    }
}
