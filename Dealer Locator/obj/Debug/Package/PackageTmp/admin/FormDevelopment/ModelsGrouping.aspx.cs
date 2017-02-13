using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Dealer_Locator.admin
{
    public partial class ModelsGrouping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void SetIndex(int index)
        {
            if (HttpContext.Current.Session["ReloadTab1"] != null && HttpContext.Current.Session["ReloadTab1"].ToString() != "false")
            {
                HttpContext.Current.Session["TabIndex1"] = index;
            }

            HttpContext.Current.Session["ReloadTab1"] = "true";
        }

        [WebMethod]
        public static void SetIndex2(int index)
        {
            HttpContext.Current.Session["TabIndex2"] = index;
            HttpContext.Current.Session["ReloadTab1"] = "false";

        }

        [WebMethod]
        public static void SetIndex3(int index)
        {
            HttpContext.Current.Session["TabIndex3"] = index;
            HttpContext.Current.Session["ReloadTab1"] = "false";
        }
    }
}