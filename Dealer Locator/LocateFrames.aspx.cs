using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SagaraSoftware.ZipCodeUtil;

namespace Dealer_Locator
{
    public partial class LocateFrames : System.Web.UI.Page
    {


        protected void Page_Load( object sender, EventArgs e )
        {
            Dealer_Locator.DA.DataAccess.DataAccessConnectionString = "";

            DisplayStep2( false );
            //DisplayStep3(false);

            if ( Page.IsPostBack == false )
            {

                DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

                mcdt = mcta.GetData_nonDisabled();

                foreach ( DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows )
                {
                    ListItem tempItem = new ListItem();

                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_mainCatID.ToString();

                    cboProductLine.Items.Add( tempItem );
                }



                string sql = "SELECT DISTINCT([STATE]) FROM [DL.ZipLookup] ORDER BY STATE ASC";

                DataSet ds = new DataSet();

                ds = DA.DataAccess.Read( sql );

                foreach ( DataRow dr in ds.Tables[ 0 ].Rows )
                {
                    if ( dr[ "STATE" ].ToString() != "PR"
                            && dr[ "STATE" ].ToString() != "AA"
                            && dr[ "STATE" ].ToString() != "AE"
                            && dr[ "STATE" ].ToString() != "GU"
                            && dr[ "STATE" ].ToString() != "PW"
                            && dr[ "STATE" ].ToString() != "AP"
                            && dr[ "STATE" ].ToString() != "AS"
                            && dr[ "STATE" ].ToString() != "FM"
                            && dr[ "STATE" ].ToString() != "MH"
                            && dr[ "STATE" ].ToString() != "MP"
                            && dr[ "STATE" ].ToString() != "VI" )
                    {
                        ListItem tempItem = new ListItem();

                        tempItem.Text = dr[ "STATE" ].ToString();
                        tempItem.Value = dr[ "STATE" ].ToString();

                        cboState.Items.Add( tempItem );
                    }
                }


            }

            string prefix = "";
            string headerFooterID = "";
            string redirectUrl = "";

            // build the redirect url
            redirectUrl = "locate.aspx";

            headerFooterID = Request.QueryString[ "hfID" ];

            if ( headerFooterID != "" && headerFooterID != null )
            {
                prefix = "hfID=" + headerFooterID;

            }

            string salesLeadFormID = Request.QueryString[ "slID" ];

            if ( salesLeadFormID != "" && salesLeadFormID != null )
            {
                salesLeadFormID = "slID=" + salesLeadFormID;

                if ( prefix == "" )
                {
                    prefix = salesLeadFormID;
                }
                else
                {
                    prefix = prefix + "&" + salesLeadFormID;
                }


            }

            if ( prefix != "" )
                prefix = "?" + prefix;

            redirectUrl = redirectUrl + prefix;

            litNewSearch.Text = "<a href=\"" + redirectUrl + "\">New Search</a>";


        }



        protected void btnSubmitZip_Click( object sender, EventArgs e )
        {

            cboCity.Items.Clear();
            lblZipValid.Visible = false;
            lblZipValid.Text = "";
            lblCityStateValid.Text = "";
            lblCityStateValid.Visible = false;


            if ( txtZip.Text != string.Empty )
            {

                int zip = Convert.ToInt32( txtZip.Text );

                string sql = "SELECT DISTINCT([CITY_ALIAS_NAME]), [STATE] FROM [DL.ZipLookup] WHERE ZIP_CODE = " + txtZip.Text + " GROUP BY [CITY_ALIAS_NAME], [STATE] ORDER BY [CITY_ALIAS_NAME]";
                DataSet ds = new DataSet();

                ds = DA.DataAccess.Read( sql );


                int counter = 0;

                foreach ( DataRow dr in ds.Tables[ 0 ].Rows )
                {
                    counter = counter + 1;
                    ListItem lstItemTemp = new ListItem( dr[ "CITY_ALIAS_NAME" ].ToString(), dr[ "STATE" ].ToString() + counter.ToString() );
                    cboCity.Items.Add( lstItemTemp );
                }

                if ( counter == 0 )
                {
                    lblZipValid.Text = "The Zip You Entered Seems To Be Invalid.  Please Try Again.";
                    lblZipValid.Visible = true;
                }

                DisplayStep2( false );
            }
            else if ( txtCityEntry.Text != string.Empty && cboState.SelectedItem.Value != string.Empty )
            {
                //                string sql = "SELECT DISTINCT([CITY_ALIAS_NAME]), [STATE] FROM [DL.ZipLookup] WHERE ZIP_CODE = " + txtZip.Text + " GROUP BY [CITY_ALIAS_NAME], [STATE] ORDER BY [CITY_ALIAS_NAME]";

                string sql = "SELECT DISTINCT([CITY_ALIAS_NAME]), [STATE] FROM [DL.ZipLookup] WHERE [CITY_ALIAS_NAME] LIKE '%" + txtCityEntry.Text + "%' AND [STATE] = '" + cboState.SelectedItem.Value + "' ORDER BY [CITY_ALIAS_NAME]";

                DataSet ds = new DataSet();

                ds = DA.DataAccess.Read( sql );

                int counter = 0;



                foreach ( DataRow dr in ds.Tables[ 0 ].Rows )
                {
                    counter = counter + 1;
                    ListItem lstItemTemp = new ListItem( dr[ "CITY_ALIAS_NAME" ].ToString(), dr[ "STATE" ].ToString() + counter.ToString() );
                    cboCity.Items.Add( lstItemTemp );
                }

                if ( counter == 0 )
                {
                    lblCityStateValid.Text = "The City / State Combination You Entered Seems To Be Invalid.  Please Try Again.";
                    lblCityStateValid.Visible = true;
                }

                DisplayStep2( false );

            }

        }

        /// <summary>
        /// Specifies whether to display the objects in step 2 of the the locator page
        /// </summary>
        /// <param name="SetRowsVisible">The value to set the object visibility to</param>
        private void DisplayStep2( bool SetRowsVisible )
        {

            int cityCount = cboCity.Items.Count;

            if ( cityCount == 1 || SetRowsVisible == true )
            {
                // we have the distributor, show the result table and hide everything else
                FindClosestDistributor( cboProductLine.SelectedItem.Text );

                LocateButtonRow.Visible = false;
                CityRow.Visible = false;
                CategoryRow.Visible = false;
                ZipRow.Visible = false;
                StateRow.Visible = false;
                OrRow.Visible = false;
                CityRowEntry.Visible = false;
                ResultTable.Visible = true;
            }
            else if ( cityCount == 0 )
            {
                LocateButtonRow.Visible = false;
                CityRow.Visible = false;
                CategoryRow.Visible = true;
                ZipRow.Visible = true;
                StateRow.Visible = true;
                OrRow.Visible = true;
                CityRowEntry.Visible = true;
                ResultTable.Visible = false;
            }
            else
            {
                // multiple
                LocateButtonRow.Visible = true;
                CityRow.Visible = true;
                CategoryRow.Visible = true;

                if ( txtZip.Text != string.Empty )
                {
                    ZipRow.Visible = true;
                    StateRow.Visible = false;
                    CityRowEntry.Visible = false;
                }
                else
                {
                    ZipRow.Visible = false;
                    StateRow.Visible = true;
                    CityRowEntry.Visible = true;
                }


                OrRow.Visible = false;

                ResultTable.Visible = false;
            }
        }

        private void DisplayStep3( bool SetRowVisible1 )
        {
            ResultTable.Visible = SetRowVisible1;
        }

        private void HideAllRows()
        {
            LocateButtonRow.Visible = false;
            CityRow.Visible = false;
            CategoryRow.Visible = false;
            ZipRow.Visible = false;
        }




        private DA.ContractTDS.DistributorRow GetClosestDistributor(out double shortestDistance, out Location shortestLocation, string category, int isManufacturerRep = -1)
        {
            string stateName = cboCity.SelectedItem.Value.Substring(0, 2);  // lame, strip off extra stuff
            string cityName = cboCity.SelectedItem.Text;

            Dealer_Locator.BR.DistributorLookup dl = new Dealer_Locator.BR.DistributorLookup(Convert.ToInt32(cboProductLine.SelectedValue.ToString()), cityName, stateName);
            DA.ContractTDS.DistributorRow distributorRow = dl.GetClosestDistributor(out shortestDistance, out shortestLocation, Convert.ToInt32(cboProductLine.SelectedValue.ToString()), isManufacturerRep);

            return distributorRow;
        }

        private static DA.ContractTDS.DistributorRow GetClosestDistributorStatic(out double shortestDistance, out Location shortestLocation, int categoryId, string category, string city, string state, int isManufacturerRep = -1)
        {

            Dealer_Locator.BR.DistributorLookup dl = new Dealer_Locator.BR.DistributorLookup(categoryId, city, state);
            DA.ContractTDS.DistributorRow distributorRow = dl.GetClosestDistributor(out shortestDistance, out shortestLocation, categoryId, isManufacturerRep);

            return distributorRow;
        }

        protected void btnLocate_Click( object sender, EventArgs e )
        {
            lblSelectedCity.Text = Request.QueryString[ "cboCity" ];

            //FindClosestDistributor();
            DisplayStep2( true );
        }

        [WebMethod]
        public static int FindDistributor(string category, string categoryId, string city, string state)
        {

            double shortestDistance;
            Location shortestLocation;
            int distributorId = -1;

            DA.ContractTDS.DistributorRow drShortestDistance = GetClosestDistributorStatic(out shortestDistance, out shortestLocation, Convert.ToInt32(categoryId), category, city, state);

            if (drShortestDistance != null)
                distributorId = drShortestDistance.pk_DistributorID;

            return distributorId;
        }

        private void FindClosestDistributor( string category )
        {
            string cityName2;

            cityName2 = Request.QueryString[ "cboCity" ];


            if ( cboCity.Text != "" )
            {

                double shortestDistance;
                Location shortestLocation;

                // Get closest distributor
                DA.ContractTDS.DistributorRow drShortestDistance = GetClosestDistributor( out shortestDistance, out shortestLocation, category );

                if ( drShortestDistance != null )
                {
                    ShowLocatorResults( drShortestDistance, shortestDistance, shortestLocation );
                }

            }
        }

        private void ShowLocatorResults( DA.ContractTDS.DistributorRow drShortestDistance, double shortestDistance, Location shortestLocation )
        {

            string result;

            string abbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation( Convert.ToInt32( drShortestDistance.fk_StateID ) );

            result = "<b>" + drShortestDistance.DistName + "</b>";
            result += "<BR>" + drShortestDistance.ShippingAddress;
            result += "<BR>" + drShortestDistance.CityName;
            result += ", " + abbreviation;
            result += " " + drShortestDistance.fk_ZipID;
            result += "<BR>" + drShortestDistance.Phone;
            result += "<BR>" + (int) shortestDistance + " miles";
            result += "<BR>";
            result += "<a href=\"http://maps.google.com/maps?f=q&hl=en&q=";
            result += drShortestDistance.ShippingAddress + ",+" + drShortestDistance.CityName + ",+" + abbreviation + "+" + drShortestDistance.fk_ZipID;
            //result += "12300+n+brentfield+drive+apt+304,+dunlap,+il";
            result += "\" target=\"_blank\">Map It!</a>";
            result += "<BR>";
            //result += "<div id=\"map\" style=\"width: 400px; height: 300px\"></div>";
            result += "<BR>";
            result += "<b>Distributor of BOMAG Americas Product(s):</b>";
            result += "<BR>";

            DA.ContractTDS.DistributorDataTable ddt = new Dealer_Locator.DA.ContractTDS.DistributorDataTable();
            DA.ContractTDSTableAdapters.DistributorTableAdapter dta = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();

            ddt = dta.GetDataMainCategoriesByDistributorID( Convert.ToInt32( drShortestDistance.pk_DistributorID.ToString() ) );

            string cats = "";
            foreach ( DA.ContractTDS.DistributorRow tempRow in ddt.Rows )
            {
                if ( cats != "" )
                    cats = cats + ", ";

                cats = cats + tempRow[ "CategoryName" ].ToString();
            }
            result += cats + "<BR>";

            litResults.Text = result;


            Session[ "drShortestDistance" ] = drShortestDistance;

        }

        private void BuildSalesLeadFormUrlAndRedirect()
        {
            string redirectUrl;
            string headerFooterID;
            string prefix = "";


            // build the redirect url
            redirectUrl = "SalesLeadForm.aspx";

            headerFooterID = Request.QueryString[ "hfID" ];

            if ( headerFooterID != "" && headerFooterID != null )
            {
                prefix = "hfID=" + headerFooterID;

            }

            string salesLeadFormID = Request.QueryString[ "slID" ];

            if ( salesLeadFormID != "" && salesLeadFormID != null )
            {
                salesLeadFormID = "slID=" + salesLeadFormID;

                if ( prefix == "" )
                {
                    prefix = salesLeadFormID;
                }
                else
                {
                    prefix = prefix + "&" + salesLeadFormID;
                }


            }

            string zipQueryString = "";

            if ( prefix != "" )
                zipQueryString = "&Zip=" + txtZip.Text;
            else
                zipQueryString = "Zip=" + txtZip.Text;

            string cityQueryString = "";

            if ( prefix != "" || zipQueryString != "" )
                cityQueryString = "&City=" + cboCity.SelectedItem.Text;
            else
                cityQueryString = "City=" + cboCity.SelectedItem.Text;


            string stateQueryString = "";

            if ( prefix != "" || zipQueryString != "" || cityQueryString != "" )
                stateQueryString = "&State=" + cboCity.SelectedItem.Value.Substring( 0, 2 );
            else
                stateQueryString = "State=" + cboCity.SelectedItem.Value.Substring( 0, 2 );

            redirectUrl = redirectUrl + "?" + prefix + zipQueryString + cityQueryString + stateQueryString;

            Response.Redirect( redirectUrl );
        }

        protected void btnSalesLeadForm_Click( object sender, EventArgs e )
        {
            BuildSalesLeadFormUrlAndRedirect();
        }

        protected void lstCity_SelectedIndexChanged( object sender, EventArgs e )
        {

        }

        protected void txtZip_TextChanged( object sender, EventArgs e )
        {

        }
        
        [WebMethod]
        public static void SetIndex(int index)
        {
            if (HttpContext.Current.Session["ReloadTab1"] != "false")
            {
                HttpContext.Current.Session["TabIndex1"] = index;
            }

            HttpContext.Current.Session["ReloadTab1"] = "true";
        }

    }
}
