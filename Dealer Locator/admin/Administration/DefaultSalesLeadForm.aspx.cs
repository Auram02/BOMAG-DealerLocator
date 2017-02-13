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
    public partial class DefaultSalesLeadForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

            try
            {
                lblCurrentDefaultSalesLeadForm.Text = slta.GetDefaultFormName();

            }
            catch
            {
                lblCurrentDefaultSalesLeadForm.Text = "None Specified";
            }


            // load the combobox
            if (!Page.IsPostBack)
            {
                DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable sldt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();

                sldt = slta.GetData();

                foreach (DA.SalesLeadFormTDS.DL_SalesLeadFormRow tempRow in sldt.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = tempRow.FormName;
                    li.Value = tempRow.pk_SLFormID.ToString();
                    cboNewDefaultSLF.Items.Add(li);
                }

            }
        }

        protected void btnReAssign_Click(object sender, EventArgs e)
        {
            DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();
            slta.ClearDefaultForm();

            slta.UpdateDefaultSLF(Convert.ToInt32(cboNewDefaultSLF.SelectedValue));

            // redirect
            Response.Redirect("/admin/Administration/DefaultSalesLeadForm.aspx");
        }
    }
}
