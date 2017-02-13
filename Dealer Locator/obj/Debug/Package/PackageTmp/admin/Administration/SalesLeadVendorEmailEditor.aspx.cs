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
    public partial class SalesLeadVendorEmailEditor : System.Web.UI.Page
    {
        DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();
        DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                etdt = etta.GetDataByEmailType("vendorEmail");

                try
                {
                    txtVendorEmail.Text = etdt[0].emailText;
                }
                catch
                {
                    txtVendorEmail.Text = "";
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                etta.UpdateQuery(0, txtVendorEmail.Text, "vendorEmail","");
                lblResult.Text = "Update Successful";
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error Updating: " + ex.Message + Environment.NewLine + ex.StackTrace;
            }

        }
    }
}
