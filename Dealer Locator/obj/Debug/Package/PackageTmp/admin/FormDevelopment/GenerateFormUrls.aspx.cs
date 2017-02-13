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

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class GenerateSalesLeadForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                DA.HeaderFooter.DL_HeaderFooterDataTable hfdt = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();
                DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfta = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();

                hfdt = hfta.GetData();
                
                //cboHeaderFooter.DataSource = hfdt;
                //cboHeaderFooter.DataTextField = "headerFooterName";
                //cboHeaderFooter.DataValueField = "pk_headerFooterID";
                //cboHeaderFooter.DataBind();

                ListItem liTemp2 = new ListItem();
                liTemp2.Text = "-none-";
                liTemp2.Value = "-1";
                cboHeaderFooter.Items.Add(liTemp2);
                cboSalesLeadForm.Items.Add(liTemp2);


                foreach (DA.HeaderFooter.DL_HeaderFooterRow hfrow in hfdt.Rows)
                {
                    ListItem liTemp = new ListItem();
                    liTemp.Text = hfrow.headerFooterName.ToString();
                    liTemp.Value = hfrow.pk_headerFooterID.ToString();

                    cboHeaderFooter.Items.Add(liTemp);
                }

                

                DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable sldt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

                sldt = slta.GetDataByGroupID(Convert.ToInt32(Request.Cookies["Authenticated"].Values["groupID"].ToString()), false);

                foreach (DA.SalesLeadFormTDS.DL_SalesLeadFormRow slrow in sldt.Rows)
                {
                    ListItem liTemp = new ListItem();
                    liTemp.Text = slrow.FormName;
                    liTemp.Value = slrow.pk_SLFormID.ToString();
                    cboSalesLeadForm.Items.Add(liTemp);
                }

                //cboSalesLeadForm.DataSource = sldt;
                //cboSalesLeadForm.DataTextField = "FormName";
                //cboSalesLeadForm.DataValueField = "pk_slFormID";
                //cboSalesLeadForm.DataBind();

            }


            CreateUrls();

        }

        protected void cboHeaderFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateUrls();
        }

        protected void cboSalesLeadForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateUrls();
        }

        private void CreateUrls()
        {
            string baseUrl = "http://www.findbomag.com/";
            string baseLocator = "locate.aspx";
            string baseSalesLeadForm = "SalesLeadForm.aspx";

            string hfID = "";
            string slID = "";
            string starterMarker = "?";

            if (cboHeaderFooter.SelectedIndex > -1 && cboHeaderFooter.SelectedValue != "-1")
                hfID = "hfID=" + cboHeaderFooter.SelectedValue;

            if (cboSalesLeadForm.SelectedIndex > -1 && cboSalesLeadForm.SelectedValue != "-1")
                slID = "slID=" + cboSalesLeadForm.SelectedValue;



            string locatorUrl = "", salesLeadUrl = "";
            locatorUrl = baseUrl + baseLocator;
            salesLeadUrl = baseUrl + baseSalesLeadForm;

            if (hfID != "")
            {
                
                locatorUrl = locatorUrl + starterMarker + hfID;
                salesLeadUrl = salesLeadUrl + starterMarker + hfID;

                starterMarker = "&";
            }


            if (slID != "")
            {

                locatorUrl = locatorUrl + starterMarker + slID;
                salesLeadUrl = salesLeadUrl + starterMarker + slID;
            }

            txtLocatorFormUrl.Text = locatorUrl;
            txtSalesLeadFormUrl.Text = salesLeadUrl;

        }
    }
}
