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

using Dealer_Locator.BR;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class ModifyLeadData : System.Web.UI.Page
    {
        int _leadId = -1;
        BR.Lead _lead;

        protected void Page_Load( object sender, EventArgs e )
        {

            if ( Request[ "leadID" ] != null )
            {
                _leadId = Convert.ToInt32(Request[ "leadID" ].ToString());
                _lead = new Dealer_Locator.BR.Lead( _leadId );

                PopulateScreen();
                lblResult.Text = "";
            }
            
            
        }


        private void PopulateScreen()
        {
            foreach ( Lead.LeadValue leadValue in _lead._leadValues )
            {
                if ( leadValue.elementName.ToUpper().Contains( "FIRSTNAME" ) )
                {
                    txtFirstName.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "LASTNAME" ) )
                {
                    txtLastName.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "COMPANY_AGENCY" ) )
                {
                    txtCompany.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "COUNTRYREGION" ) )
                {
                    txtCountry.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "ADDRESS1" ) )
                {
                    txtAddressLine1.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "ADDRESS2" ) )
                {
                    txtAddressLine2.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "ZIP CODE" ) )
                {
                    txtZipCode.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "CITY" ) )
                {
                    txtCity.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "STATE" ) )
                {
                    txtState.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "PHONE" ) )
                {
                    txtPhone.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "FAX" ) )
                {
                    txtFax.Text = leadValue.elementValue;
                }

                if ( leadValue.elementName.ToUpper().Contains( "EMAIL" ) )
                {
                    txtEmail.Text = leadValue.elementValue;
                }

            }
        }

        protected void btnSave_Click( object sender, EventArgs e )
        {
         //   _lead

            for ( int i = 0; i < _lead._leadValues.Count; i++ )
            {
                Lead.LeadValue leadValue = _lead._leadValues[ i ];

                if ( leadValue.elementName.ToUpper().Contains( "FIRSTNAME" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtFirstName"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "LASTNAME" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtLastName"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "COMPANY_AGENCY" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtCompany"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "COUNTRYREGION" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtCountry"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "ADDRESS1" ) )
                {
                    leadValue.elementValue =Request["ctl00$MainContent$txtAddressLine1"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "ADDRESS2" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtAddressLine2"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "ZIP CODE" ) )
                {
                    leadValue.elementValue =Request["ctl00$MainContent$txtZipCode"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "CITY" ) )
                {
                    leadValue.elementValue =Request["ctl00$MainContent$txtCity"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "STATE" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtState"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "PHONE" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtPhone"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "FAX" ) )
                {
                    leadValue.elementValue = Request["ctl00$MainContent$txtFax"].ToString();
                }

                if ( leadValue.elementName.ToUpper().Contains( "EMAIL" ) )
                {
                    leadValue.elementValue = Request[ "ctl00$MainContent$txtEmail" ].ToString();
                }

                _lead._leadValues[ i ] = leadValue;

            }

            bool isError = _lead.UpdateLeadValues();


            if ( isError == false )
            {
                // success!
                PopulateScreen();
                lblResult.Text = "The lead values were updated successfully";
            }
            else
            {
                // Failure
                lblResult.Text = "There was an error updating the lead values.  Please forward this error on to the developer with the following lead id: " + _lead.LeadID.ToString();
            }

        }

    }
}
