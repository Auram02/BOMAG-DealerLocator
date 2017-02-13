using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Dealer_Locator.admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Abandon session and log the user out
            Request.Cookies["Authenticated"].Values["IsLoggedIn"] = "false";
            HttpCookie adminCookie = new HttpCookie("Authenticated", Request.Cookies["Authenticated"].Values["admin"].ToString());
            adminCookie.Values["IsLoggedIn"] = "false";
            Session.Abandon();

            Response.Cookies["Authenticated"].Expires = DateTime.Now.AddYears(-30);
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}