using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Reports
{
    public partial class PendingLeadsListReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            LeadList1.SetDateRange(DateTime.Now.AddMonths(0), DateTime.Now.AddMonths(6));
            LeadList2.SetDateRange(DateTime.Now, DateTime.Now.AddMonths(12));

        }
    }
}