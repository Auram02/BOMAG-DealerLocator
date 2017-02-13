using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SagaraSoftware.ZipCodeUtil;

namespace Dealer_Locator
{
    public partial class LocateResults : System.Web.UI.Page
    {
        private string _city = string.Empty;
        private string _state = string.Empty;
        private string _zip = string.Empty;
        private int _productCategoryId = -1;
        private double _shortestDistance = 0;
        private Location _shortestLocation;

        protected void Page_Load(object sender, EventArgs e)
        {
            _productCategoryId = Convert.ToInt32(Request.QueryString["pcID"].ToString());
            _city = Request.QueryString["city"].ToString();
            _state = Request.QueryString["state"].ToString();   
            _zip = Request.QueryString["zip"].ToString();

            _city = _city.Replace("-", " ");

            BuildDistributorOutput();
        }

        private void BuildDistributorOutput()
        {

            int distributorId = -1;

            Dealer_Locator.BR.DistributorLookup dl = new Dealer_Locator.BR.DistributorLookup(_productCategoryId, _city, _state);
            DA.ContractTDS.DistributorRow drShortestDistance = dl.GetClosestDistributor(out _shortestDistance, out _shortestLocation, _productCategoryId);

            if (drShortestDistance != null)
                distributorId = drShortestDistance.pk_DistributorID;

            // **************************** Build Result Output ***********************************

            string result;

            string abbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(drShortestDistance.fk_StateID));

            string address = drShortestDistance.ShippingAddress + ", " + drShortestDistance.CityName + ", " + abbreviation + " " + drShortestDistance.fk_ZipID;
            string addressDisplay = drShortestDistance.DistName + "<BR>" + drShortestDistance.ShippingAddress + "<BR>" + drShortestDistance.CityName + ", " + abbreviation + " " + drShortestDistance.fk_ZipID;

            result = "<div style='display:inline; float: left; width: 50%;'><b>" + drShortestDistance.DistName + "</b>";
            result += "<BR>" + drShortestDistance.ShippingAddress;
            result += "<BR>" + drShortestDistance.CityName;
            result += ", " + abbreviation;
            result += " " + drShortestDistance.fk_ZipID;
            result += "<BR>" + drShortestDistance.Phone;

            result += "<BR>";
            //result += "<a href=\"http://maps.google.com/maps?f=q&hl=en&q=";
            //result += drShortestDistance.ShippingAddress + ",+" + drShortestDistance.CityName + ",+" + abbreviation + "+" + drShortestDistance.fk_ZipID;
            ////result += "12300+n+brentfield+drive+apt+304,+dunlap,+il";
            //result += "\" target=\"_blank\">Map It!</a>";
            result += "<BR>";
            result += "<BR>";
            result += "<b>Distributor of BOMAG Americas Product(s):</b>";
            result += "<BR>";

            DA.ContractTDS.DistributorDataTable ddt = new Dealer_Locator.DA.ContractTDS.DistributorDataTable();
            DA.ContractTDSTableAdapters.DistributorTableAdapter dta = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();

            ddt = dta.GetDataMainCategoriesByDistributorID(Convert.ToInt32(drShortestDistance.pk_DistributorID.ToString()));

            string cats = "";
            foreach (DA.ContractTDS.DistributorRow tempRow in ddt.Rows)
            {
                if (cats != "")
                    cats = cats + ", ";

                cats = cats + tempRow["CategoryName"].ToString();
            }
            result += cats + "<BR>";

            result += "</div><div style='float:right;'>";
            result += "<div id=\"map_canvas\" />";
            result += "<div><span id='DistanceLabel'>DISTANCE: </span><span>" + (int)_shortestDistance + " miles</span></div>";
            result += "</div>";
            
            litResults.Text = result;


            Session["drShortestDistance"] = drShortestDistance;

            BuildSalesLeadFormUrl();

            string javascript = "ShowMap('" + address + "','" + addressDisplay + "');";

            ClientScript.RegisterStartupScript(this.Page.GetType(), "MapScript", javascript, true);

        }

        private void BuildSalesLeadFormUrl()
        {
            string redirectUrl;
            string headerFooterID;
            string prefix = "";


            // build the redirect url
            redirectUrl = "SalesLeadForm.aspx";

            headerFooterID = Request.QueryString["hfID"];

            if (headerFooterID != "" && headerFooterID != null)
            {
                prefix = "hfID=" + headerFooterID;

            }

            string salesLeadFormID = Request.QueryString["slID"];

            if (salesLeadFormID != "" && salesLeadFormID != null)
            {
                salesLeadFormID = "slID=" + salesLeadFormID;

                if (prefix == "")
                {
                    prefix = salesLeadFormID;
                }
                else
                {
                    prefix = prefix + "&" + salesLeadFormID;
                }


            }

            string zipQueryString = "";

            if (prefix != "")
                zipQueryString = "&Zip=" + _zip;
            else
                zipQueryString = "Zip=" + _zip;

            string cityQueryString = "";

            if (prefix != "" || zipQueryString != "")
                cityQueryString = "&City=" + _city;
            else
                cityQueryString = "City=" + _city;


            string stateQueryString = "";

            if (prefix != "" || zipQueryString != "" || cityQueryString != "")
                stateQueryString = "&State=" + _state;
            else
                stateQueryString = "State=" + _state;

            redirectUrl = redirectUrl + "?" + prefix + zipQueryString + cityQueryString + stateQueryString;

            btnSalesLeadFormRedirect.Attributes.Add("onclick", "javascript: location.href='" + redirectUrl + "'");

        }
    }
}