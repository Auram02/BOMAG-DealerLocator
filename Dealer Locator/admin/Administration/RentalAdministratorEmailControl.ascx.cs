using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Admin
{
    public partial class RentalAdministratorEmailControl : System.Web.UI.UserControl
    {
        int emailTypeID = -1;
        string emailType = "RentalAdministrator";

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
                        txtHeavyDistributorEmail.Text = etdt[0].emailAddress;

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
                    etta.UpdateQuery(emailTypeID, "", emailType, txtHeavyDistributorEmail.Text);

                }
                else
                {

                    int nextId = Convert.ToInt32(DA.DataAccess.GetNextID("[DL.EmailTemplate]", "pk_emailID"));

                    etta.InsertQuery(nextId, "", emailType, txtHeavyDistributorEmail.Text);
                }

            }
            catch
            {
                int nextId = Convert.ToInt32(DA.DataAccess.GetNextID("[DL.EmailTemplate]", "pk_emailID"));

                etta.InsertQuery(nextId, "", emailType, txtHeavyDistributorEmail.Text);
            }

        }
    }
}