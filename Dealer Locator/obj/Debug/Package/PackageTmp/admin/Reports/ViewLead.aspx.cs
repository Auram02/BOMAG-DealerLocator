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
    public partial class ViewLead : System.Web.UI.Page
    {

        int leadID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // expect LEADID
            string leadTemp = Request.QueryString["leadID"].ToString();

            if (leadTemp != "")
            {
                leadID = Convert.ToInt32(leadTemp);

                DA.LeadsTDS.DL_LeadEmailDataTable ledt = new Dealer_Locator.DA.LeadsTDS.DL_LeadEmailDataTable();
                DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter leta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTableAdapter();

                DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter etta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadEmailTypeTableAdapter();

                ledt = leta.GetDataByLeadID(leadID);

                ConstructBreadcrumbTrail();
        

                foreach (DA.LeadsTDS.DL_LeadEmailRow dr in ledt.Rows)
                {
                    emailTemplate myTemplate = ((emailTemplate)LoadControl("emailTemplate.ascx"));

                    string emailType = etta.GetEmailTypeByID(dr.fk_emailType).ToString();
                    
                    myTemplate.SetText(dr.emailTo, dr.emailFrom, dr.emailBCC, dr.emailCC, dr.emailSubject, dr.emailBody, emailType);
                    phEmailTemplates.Controls.Add(myTemplate);
                    
                }

            }
            else
            {
                // error.  alert them
            }
        }



        private void ConstructBreadcrumbTrail()
        {
            string breadCrumbs = "";

            ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
            if (breadCrumbsArr == null)
                breadCrumbsArr = new ArrayList();

            Session.Remove("breadCrumbs");


            foreach (string item in breadCrumbsArr)
            {
                if (breadCrumbs != "")
                    breadCrumbs += " : ";
                breadCrumbs += item;
            }

            if (breadCrumbs != "")
            {
                breadCrumbs = breadCrumbs + " : ";
            }

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            ldt = lta.GetDataByLeadID(leadID);

            string leadDate = ldt[0].submitDate.ToString();

            breadCrumbs += "Lead " + leadDate;

            Session.Add("breadCrumbs", breadCrumbsArr);

            lblBreadcrumb.Text = breadCrumbs;
        }


    }
}
