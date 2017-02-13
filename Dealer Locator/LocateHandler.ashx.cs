using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using SagaraSoftware.ZipCodeUtil;

namespace Dealer_Locator
{
    /// <summary>
    /// Summary description for LocateHandler
    /// </summary>
    public class LocateHandler : IHttpHandler, IRequiresSessionState
    {
        private string _city = string.Empty;
        private string _state = string.Empty;
        private string _zip = string.Empty;
        private int _productCategoryId = -1;
        private double _shortestDistance = 0;
        private Location _shortestLocation;
        private int _isManufacturerRep = 0;

        public void ProcessRequest(HttpContext context)
        {
            string returnValue = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();


            string action = context.Request["action"].ToString();

            switch (action)
            {
                case "LocateResults":
                    _productCategoryId = Convert.ToInt32(context.Request.QueryString["pcID"].ToString());
                    _city = context.Request.QueryString["city"].ToString();
                    _state = context.Request.QueryString["state"].ToString();
                    _zip = context.Request.QueryString["zip"].ToString();
                    _isManufacturerRep = 0;

                    _city = _city.Replace("-", " ");
                    DistributorOutput heavyDistributorOutput = BuildDistributorOutput();

                    DistributorOutput manuRepDistOutput = new DistributorOutput();
                    bool manuRepFound = false;

                    //returnValue = js.Serialize(output);
                    //break;

                //case "ManufacturerRepLocateResults":
                    // Check if the _productCategoryId allows overlap

                    //_productCategoryId = Convert.ToInt32(context.Request.QueryString["pcID"].ToString());

                    DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                    DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new DA.MainCategoryTDS.DL_MainCategoryDataTable();

                    mcdt = mcta.GetDataByMainCategoryID(_productCategoryId);

                    if (mcdt.Rows.Count > 0)
                    {
                        // if they allow territory overlap, retrieve additional info
                        if (((DA.MainCategoryTDS.DL_MainCategoryRow)mcdt.Rows[0]).AllowTerritoryOverlap == true)
                        {
                            _city = context.Request.QueryString["city"].ToString();
                            _state = context.Request.QueryString["state"].ToString();
                            _zip = context.Request.QueryString["zip"].ToString();

                            _city = _city.Replace("-", " ");

                            _isManufacturerRep = 1;
                            
                             manuRepDistOutput = BuildDistributorOutput();

                             if (manuRepDistOutput.DistributorName != null)
                             {
                                 manuRepFound = true;
                             }
                             else
                             {
                                 manuRepFound = false;
                             }

                            
                        }
                        else
                        {
                            returnValue = String.Empty;
                        }
                    }

                    DistributorOutput[] distOutputArray = new DistributorOutput[1];
                    
                    if (manuRepFound)
                    {
                        distOutputArray = new DistributorOutput[2];
                        distOutputArray[0] = heavyDistributorOutput;
                        distOutputArray[1] = manuRepDistOutput;
                    }
                    else
                    {
                        distOutputArray = new DistributorOutput[1];
                        distOutputArray[0] = heavyDistributorOutput;
                    }

                    returnValue = js.Serialize(distOutputArray);

                    break;

                default:

                    break;


            }

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Write(returnValue);
        }

        private struct DistributorOutput
        {

            public string DistributorName;
            public string ShippingAddress;
            public string City;
            public string StateAbbreviation;
            public string ZipCode;
            public string Phone;
            public string Categories;
            public string DistanceToDistributor;
            public string SalesLeadFormUrl;

        }

        private DistributorOutput BuildDistributorOutput()
        {
            DistributorOutput distributorOutput = new DistributorOutput();

            int distributorId = -1;

            Dealer_Locator.BR.DistributorLookup dl = new Dealer_Locator.BR.DistributorLookup(_productCategoryId, _city, _state);
            DA.ContractTDS.DistributorRow drShortestDistance = dl.GetClosestDistributor(out _shortestDistance, out _shortestLocation, _productCategoryId, _isManufacturerRep);

            if (drShortestDistance != null)
            {
                distributorId = drShortestDistance.pk_DistributorID;

                // **************************** Build Result Output ***********************************

                string abbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(drShortestDistance.fk_StateID));

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

                HttpContext.Current.Session["drShortestDistance"] = drShortestDistance;

                distributorOutput.Categories = cats;
                distributorOutput.City = drShortestDistance.CityName;
                distributorOutput.DistanceToDistributor = ((int)_shortestDistance).ToString();
                distributorOutput.DistributorName = drShortestDistance.DistName;
                distributorOutput.Phone = drShortestDistance.Phone;
                distributorOutput.SalesLeadFormUrl = BuildSalesLeadFormUrl();
                distributorOutput.ShippingAddress = drShortestDistance.ShippingAddress;
                distributorOutput.StateAbbreviation = abbreviation;
                distributorOutput.ZipCode = drShortestDistance.fk_ZipID;
            }
            return distributorOutput;
        }

        private string BuildSalesLeadFormUrl()
        {
            string redirectUrl;
            string headerFooterID;
            string prefix = "";

            // build the redirect url
            redirectUrl = "SalesLeadForm.aspx";

            headerFooterID = HttpContext.Current.Request.QueryString["hfID"];

            if (headerFooterID != "" && headerFooterID != null)
            {
                prefix = "hfID=" + headerFooterID;

            }

            string salesLeadFormID = HttpContext.Current.Request.QueryString["slID"];

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

            return redirectUrl;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}