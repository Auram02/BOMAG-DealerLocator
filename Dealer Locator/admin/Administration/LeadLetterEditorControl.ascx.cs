using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Admin
{
    public partial class LeadLetterEditorControl : System.Web.UI.UserControl
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
                etta.UpdateQuery(0, txtVendorEmail.Text, "vendorEmail", "");
                lblResult.Text = "Update Successful";
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error Updating: " + ex.Message + Environment.NewLine + ex.StackTrace;
            }

        }
    }
}