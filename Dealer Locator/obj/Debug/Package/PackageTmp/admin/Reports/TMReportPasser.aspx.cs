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
    public partial class TMReportPasser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int repID = Convert.ToInt32(Request.QueryString["tmID"]);

            Session.Remove("repID");
            Session.Add("repID", repID.ToString());

            int year = Convert.ToInt32(Request.QueryString["year"]);


            Response.Redirect("TerritoryManagerReport.aspx?year=" + year.ToString());

        }
    }
}
