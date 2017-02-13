using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.BR
{
    public static class Utility
    {
        //
        // Returns a string of six random digits.
        //
        public static string GenerateRandomCode()
        {
            // For generating random numbers.
            Random random = new Random();

            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}
