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

namespace Dealer_Locator
{
    public partial class DevDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string territoryEmail = "";
            string vendorEmail = "";
            string userEmail = "";
            string distributorFax = "";

            try
            {
                //territoryEmail = DA.Debug.territoryEmail;
                territoryEmail = Session["Territory Manager"].ToString();
                territoryEmail = territoryEmail.Replace(Environment.NewLine, "<BR>");
            }
            catch (Exception ex)
            {
                // do nothing...
            }

            try
            {
                //vendorEmail = DA.Debug.vendorEmail;

                vendorEmail = Session["Distribution Vendor"].ToString();

                vendorEmail= vendorEmail.Replace(Environment.NewLine, "<BR>");
            }
            catch
            {
                // do nothing.  none of the products were selected as "Mail"
            }

            try
            {
                //userEmail = DA.Debug.userEmail;

                userEmail = Session["User Email"].ToString();

                userEmail = userEmail.Replace(Environment.NewLine, "<BR>");
            }
            catch
            {
                // do nothing.  none of the products were selected as "Mail"
            }

            try
            {
                //distributorFax = DA.Debug.distributorFax;

                distributorFax = Session["Distributor Fax"].ToString();

                distributorFax = distributorFax.Replace(Environment.NewLine, "<BR>");
            }
            catch
            {
                // do nothing.  none of the products were selected as "Mail"
            }
            


            litTerritoryManagerEmails.Text = territoryEmail;
            litProductVendorEmails.Text = vendorEmail;
            litUserEmail.Text = userEmail;
            litDistributorFax.Text = distributorFax;
        }
    }
}
