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
using ParameterPasser;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            litFormName.Text = sessionWrapper["formName"];

            
            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;
        }

        protected void lnkUrl_Click(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            int formID = Convert.ToInt32(sessionWrapper["formID"].ToString());

            Response.Redirect("/SalesLeadForm.aspx?slID=" + formID.ToString());

        }
    }
}
