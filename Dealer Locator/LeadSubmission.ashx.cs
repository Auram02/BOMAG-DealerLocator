using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Dealer_Locator
{
    /// <summary>
    /// Summary description for LeadSubmission
    /// </summary>
    public class LeadSubmission : IHttpHandler, IRequiresSessionState
    {

        private struct VerificationResult
        {
            public bool IsValid;
            public string NewVerificationCode;
            public bool HasModelsSelected;
        }


        public void ProcessRequest(HttpContext context)
        {
            string returnValue = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();


            string action = context.Request["action"].ToString();

            switch (action)
            {
                case "Verification":

                    string code = context.Request["code"].ToString();
                    string kioskMode = context.Request["kioskMode"].ToString();
                    string isDoNotPlanToBuy = context.Request["isDoNotPlanToBuy"].ToString();

                    VerificationResult vr = new VerificationResult();

                    if (kioskMode == "false")
                    {
                        vr = CheckVerificationCode(code);

                    }
                    else
                    {
                        vr.IsValid = true;
                        vr.NewVerificationCode = string.Empty;
                    }

                    // If it is 'do not plan to buy', then do not run the selected machines check
                    // If it is, then act as if they have selected machines since having 1+ machines selected is no longer required
                    if (isDoNotPlanToBuy == "false")
                        vr.HasModelsSelected = HasModelsSelected();
                    else
                        vr.HasModelsSelected = true;

                    returnValue = js.Serialize(vr);

                    break;


                default:

                    break;


            }

            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Write(returnValue);
        }

        private VerificationResult CheckVerificationCode(string verificationCode)
        {
            VerificationResult vr = new VerificationResult();

            if (verificationCode == HttpContext.Current.Session["CaptchaImageText"].ToString())
            {
                vr.IsValid = true;
                vr.NewVerificationCode = string.Empty;
            }
            else
            {
                vr.IsValid = false;
                vr.NewVerificationCode = BR.Utility.GenerateRandomCode();
                HttpContext.Current.Session["CaptchaImageText"] = vr.NewVerificationCode;
                
            }

            return vr;
        }

        private bool HasModelsSelected()
        {
            bool result = false;

            List<ProductSearch.Product> pageProducts = ProductSearch.GetList();

            if (pageProducts != null && pageProducts.Count > 0)
            {
                foreach (ProductSearch.Product product in pageProducts)
                {
                    if (product.selected == true)
                        result = true;
                }
            }

            return result;
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