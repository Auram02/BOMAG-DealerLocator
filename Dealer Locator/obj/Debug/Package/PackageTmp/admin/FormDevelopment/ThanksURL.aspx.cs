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
    public partial class ThanksURL : System.Web.UI.Page
    {
        int _formID;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            litFormName.Text = sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;


            ParameterPasser.SessionParameterPasser myParam = new ParameterPasser.SessionParameterPasser();
            _formID = Convert.ToInt32(myParam["formID"]);


            string qsresult;

            if (Request.QueryString["result"] == "False")
            {
                
                lblResult.Text = "Update Failed";
            }
            else if (Request.QueryString["result"] == "True")
            {
                
                lblResult.Text = "Update Successful";
            }

            

            if (Page.IsPostBack == false)
                txtURL.Text = BR.FormTemplate.GetThanksURL(_formID);
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            bool result = BR.FormTemplate.UpdateThanksURL(txtURL.Text, _formID);

            Response.Redirect("~/admin/FormDevelopment/ThanksURL.aspx?result=" + result.ToString());
        }

        protected void lnkUrl_Click(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            int formID = Convert.ToInt32(sessionWrapper["formID"].ToString());

            Response.Redirect("/SalesLeadForm.aspx?slID=" + formID.ToString());

        }

    }
}
