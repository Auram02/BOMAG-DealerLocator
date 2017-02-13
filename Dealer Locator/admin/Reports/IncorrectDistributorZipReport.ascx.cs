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
    public partial class IncorrectDistributorZipReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoErrors.Visible = false;

            try
            {
                DataTable dtList;
                dtList = DA.Reports.CreateDistributorZipReport();

                if (dtList.Rows.Count == 0)
                {
                    lblNoErrors.Visible = true;
                }
                else
                {
                    gvDistributorZipReport.DataSource = dtList;
                    gvDistributorZipReport.DataBind();
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot find "))
                {
                    lblNoErrors.Visible = true;
                }
            }

        }

    }
}