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
    public partial class LatLongReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();

                DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();

                //zldt = zlta.GetDistinctState();


                DataSet ds = new DataSet();
                string sql = "SELECT DISTINCT STATE  FROM dbo.[DL.ZipLookup] ORDER BY STATE ";

                ds = DA.DataAccess.Read(sql);


                ListItem li2 = new ListItem();
                li2.Value = "";
                li2.Text = "";

                cboState.Items.Add(li2);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem li = new ListItem();

                    li.Text = dr["STATE"].ToString();
                    li.Value = dr["STATE"].ToString();
                    
                    cboState.Items.Add(li);
                }

            }
        }

        protected void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {

            cboCity.Items.Clear();
            lblLatitude.Text = "";
            lblLongitude.Text = "";

            if (cboState.SelectedItem.Text != "")
            {
                
                string stateName = cboState.SelectedItem.Text;

                DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();

                DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();


                DataSet ds = new DataSet();
                string sql = "SELECT DISTINCT CITY FROM dbo.[DL.ZipLookup] WHERE STATE = '" + stateName + "'";

                ds = DA.DataAccess.Read(sql);

                ListItem li2 = new ListItem();
                li2.Text = "";
                li2.Value = "";
                cboCity.Items.Add(li2);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr["City"].ToString();
                    li.Value = dr["City"].ToString();

                    cboCity.Items.Add(li);
                }

            }

        }

        protected void cboCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cityName, stateName;

            if (cboCity.SelectedIndex > -1 && cboCity.SelectedItem.Text != "")
            {
                cityName = cboCity.SelectedItem.Text;
                stateName = cboState.SelectedItem.Text;


                //DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();

                //DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();

                //zldt = zlta.GetDataByCityState(cityName, stateName);

                DataSet ds = new DataSet();

                string sql = "SELECT LATITUDE, LONGITUDE FROM [DL.ZipLookup] WHERE City = '" + cityName + "' AND STATE = '" + stateName + "'";

                ds = DA.DataAccess.Read(sql);


                lblLatitude.Text = ds.Tables[0].Rows[0]["LATITUDE"].ToString();
                lblLongitude.Text = ds.Tables[0].Rows[0]["LONGITUDE"].ToString();


            }
            else
            {
                lblLatitude.Text = "";
                lblLongitude.Text = "";
            }
        }
    }
}
