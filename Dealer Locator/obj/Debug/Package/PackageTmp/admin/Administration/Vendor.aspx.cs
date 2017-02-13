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

namespace Dealer_Locator.admin.Admin
{
    public partial class Vendor : System.Web.UI.Page
    {

        private int _vendorID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {
                    DA.VendorTDSTableAdapters.DL_VendorTableAdapter ta = new Dealer_Locator.DA.VendorTDSTableAdapters.DL_VendorTableAdapter();
                    DA.VendorTDS.DL_VendorDataTable dt = new Dealer_Locator.DA.VendorTDS.DL_VendorDataTable();
                    dt = ta.GetData();

                    txtAddress.Text = dt.Rows[0]["address"].ToString();
                    txtCity.Text = dt.Rows[0]["city"].ToString();
                    txtCompanyName.Text = dt.Rows[0]["companyName"].ToString();
                    txtState.Text = dt.Rows[0]["state"].ToString();
                    txtZip.Text = dt.Rows[0]["zip"].ToString();
                    txtWebsite.Text = dt.Rows[0]["vendorUrl"].ToString();
                    txtEmail.Text = dt.Rows[0]["email"].ToString();

                    _vendorID = Convert.ToInt32(dt.Rows[0]["pk_vendorID"].ToString());
                }
                catch (Exception ex)
                {
                    lblError.Text = "There was an error loading the vendor information: " + ex.Message;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DA.VendorTDSTableAdapters.DL_VendorTableAdapter ta = new Dealer_Locator.DA.VendorTDSTableAdapters.DL_VendorTableAdapter();

            ta.UpdateQuery(_vendorID, txtWebsite.Text, txtCompanyName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtZip.Text, txtEmail.Text);

            System.Threading.Thread.Sleep(2000);  // fake a sleep
        }




    }
}
