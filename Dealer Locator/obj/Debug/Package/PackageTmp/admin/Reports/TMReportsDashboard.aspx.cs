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
    public partial class TMReportsDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnLeadsByYear_Click(object sender, EventArgs e)
        {
            int selectedYear = 0;

            selectedYear = Convert.ToInt32(SelectYear1.Year);

            Response.Redirect("TerritoryManagerReport.aspx?year=" + selectedYear.ToString());
        }

        protected void btnLeadsByDealer_Click(object sender, EventArgs e)
        {
            int selectedYear = 0;

            selectedYear = Convert.ToInt32(SelectYear1.Year);

            Response.Redirect("TerritoryManagerLeadsByDealer.aspx?year=" + selectedYear.ToString());
        }
    }
}
