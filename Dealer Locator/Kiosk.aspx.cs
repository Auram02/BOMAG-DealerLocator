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
    public partial class Kiosk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Remove("MappedData");
            }
        }

        
        protected void btnYesCard_Click(object sender, EventArgs e)
        {
        }

        protected void btnNoCard_Click(object sender, EventArgs e)
        {
            // redirect to sales lead form page

            Response.Redirect("SalesLeadForm.aspx?kioskMode=true");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetPage();
        }


        private void ResetPage()
        {
            Session.Remove("MappedData");

        }

        protected void btnProcessAndContinue_Click(object sender, EventArgs e)
        {
            
            // get active map + read method then process the scanned data

            if (txtFormInput.Text != "")
            {
                lblError.Text = "";

                // gets active map
                BR.Mapping tempMap = new Dealer_Locator.BR.Mapping();

                System.Collections.Specialized.NameValueCollection coll = tempMap.MapData(txtFormInput.Text);

                //for (int i = 0; i < coll.Count; i++)
                //{
                //    string FormField = coll.GetKey(i);
                //    string[] CardValue = coll.GetValues(i);

                //    Request.Form.Add(FormField, CardValue[0]);
                //}

                Session.Add("MappedData", coll);
                

               // Request.Form.Add(coll);
                Response.Redirect("SalesLeadForm.aspx?kioskMode=true");
                
                
            }
            else
            {
                lblError.Text = "There was no data in the TextBox.  Please place your cursor in the textbox above, scan a card, and then try to continue again.  " +
                                    "If you would like to start over, please press the \"Reset Page\" button.";

                txtFormInput.Focus();
            }

        }
    }
}
