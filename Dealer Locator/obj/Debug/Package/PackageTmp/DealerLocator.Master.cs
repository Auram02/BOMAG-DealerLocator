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
using System.Diagnostics;


namespace Dealer_Locator
{
    public partial class DealerLocator : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

        }

        public static string GetHeader(HttpContext context)
        {
            string returnText = "";
            string hfID = context.Request.QueryString["hfID"];

            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            // check if the header footer id is good.  If so, continue to get data from tht b
            if (hfID == "" || hfID == null)
            {
                 hfDT = GetDefaultHeaderFooter();
            } else {
                try
                {
                    DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                    hfDT = hfTA.GetDataByHeaderFooterID(Convert.ToInt32(hfID));
                }
                catch
                {
                    hfDT = GetDefaultHeaderFooter();
                }
            }
            
            Debug.Assert(null != hfDT);

            // if an invalid header/footer id is specified, get the default
            if (null == hfDT || hfDT.Count == 0)
                hfDT = GetDefaultHeaderFooter();

                //throw new ArgumentNullException("Header/Footer ID is invalid", "hfDT -->hfID");


            returnText = hfDT[0]["headerText"].ToString();

            return "";
            return returnText;
        }

        public static string GetFooter(HttpContext context)
        {
            string returnText = "";
            string hfID = context.Request.QueryString["hfID"];

            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            // check if the header footer id is good.  If so, continue to get data from tht b
            if (hfID == "" || hfID == null)
            {
                hfDT = GetDefaultHeaderFooter();
            }
            else
            {
                try
                {
                    DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                    hfDT = hfTA.GetDataByHeaderFooterID(Convert.ToInt32(hfID));
                }
                catch
                {
                    hfDT = GetDefaultHeaderFooter();
                }
            }

            Debug.Assert(null != hfDT);

            // if an invalid header/footer id is specified, get the default
            if (null == hfDT || hfDT.Count == 0)
                hfDT = GetDefaultHeaderFooter();


            //throw new ArgumentNullException("Header/Footer ID is invalid", "hfDT -->hfID");



            returnText = hfDT[0]["footerText"].ToString();

            return "";
            return returnText;
        }

        public static string GetCustomCodeText(HttpContext context)
        {
            string returnText = "";
            string hfID = context.Request.QueryString["hfID"];

            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            // check if the header footer id is good.  If so, continue to get data from tht b
            if (hfID == "" || hfID == null)
            {
                hfDT = GetDefaultHeaderFooter();
            }
            else
            {
                try
                {
                    DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                    hfDT = hfTA.GetDataByHeaderFooterID(Convert.ToInt32(hfID));
                }
                catch
                {
                    hfDT = GetDefaultHeaderFooter();
                }
            }

            Debug.Assert(null != hfDT);

            // if an invalid header/footer id is specified, get the default
            if (null == hfDT || hfDT.Count == 0)
                hfDT = GetDefaultHeaderFooter();

            //throw new ArgumentNullException("Header/Footer ID is invalid", "hfDT -->hfID");



            returnText = hfDT[0]["customCodeText"].ToString();

            return returnText;
        }

        private static DA.HeaderFooter.DL_HeaderFooterDataTable GetDefaultHeaderFooter()
        {
            DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            hfDT = hfTA.GetDefaultHeaderFooter();

            return hfDT;
        }
    }
}
