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
    public partial class DoNotPlanToBuySetup : System.Web.UI.Page
    {
        int emailTypeID = -1;
        string emailType = "DoNotPlanToBuy";

        protected void Page_Load(object sender, EventArgs e)
        {
            DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();
            DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();

            etdt = etta.GetDataByEmailType(emailType);

            try
            {
                emailTypeID = etdt[0].pk_emailID;

                if (!Page.IsPostBack)
                {

                    if (emailTypeID != -1)
                        txtPlanToBuyEmail.Text = etdt[0].emailAddress;

                }

            }
            catch
            {
                emailTypeID = -1;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DA.EmailTemplate.DL_EmailTemplateDataTable etdt = new Dealer_Locator.DA.EmailTemplate.DL_EmailTemplateDataTable();
            DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter etta = new Dealer_Locator.DA.EmailTemplateTableAdapters.DL_EmailTemplateTableAdapter();

            etdt = etta.GetDataByEmailType(emailType);

            try
            {
                if (etdt != null && etdt.Rows.Count > 0 && emailTypeID != -1)
                {
                    etta.UpdateQuery(emailTypeID, "", emailType, txtPlanToBuyEmail.Text);

                }
                else
                {

                    int nextId = Convert.ToInt32(DA.DataAccess.GetNextID("[DL.EmailTemplate]", "pk_emailID"));

                    etta.InsertQuery(nextId, "", emailType, txtPlanToBuyEmail.Text);
                }

            }
            catch
            {
                int nextId = Convert.ToInt32(DA.DataAccess.GetNextID("[DL.EmailTemplate]", "pk_emailID"));

                etta.InsertQuery(nextId, "", emailType, txtPlanToBuyEmail.Text);
            }

        }
    }
}
