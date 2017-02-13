using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

// for email
using System.Net.Mail;

using System.Text;

namespace Dealer_Locator.admin.Reports
{
    public partial class FaxBulletin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendFax_Click(object sender, EventArgs e)
        {

            StringBuilder stackTrace = new StringBuilder();


            // Clear the results
            litResults.Text = "";

            bool CategorySelected = false;

            List<int> CategoryList = new List<int>();

            Hashtable Distributors = new Hashtable();

            //for (int i = 0; i < cblDistributorTypes.Items.Count; i++)
            //{
            //    if (cblDistributorTypes.Items[i].Selected == true)
            //    {
            //        CategoryList.Add(Convert.ToInt32(cblDistributorTypes.Items[i].Value.ToString()));

            //        CategorySelected = true;
            //    }
            //}


            string categories = "";


            for (int i = 0; i < cblCategories.Items.Count; i++)
            {
                if (cblCategories.Items[i].Selected == true)
                {
                    CategorySelected = true;

                    if (categories != "")
                        categories += ",";

                    categories += cblCategories.Items[i].Value.ToString();
                }
            }

            // If we have a subject, at least one file, and some distributors selected, proceed
            if (txtSubject.Text != "" && (fu1.HasFile == true || fu2.HasFile == true || fu3.HasFile == true) && CategorySelected == true)
            {
                DA.ContractTDSTableAdapters.DistributorTableAdapter dta = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();


                //foreach (int CategoryID in CategoryList)
                //{

               
                    BR.Fax faxObj = new Dealer_Locator.BR.Fax();

                    DataSet ds = faxObj.GetContractsToFax(categories);

                    //stackTrace.AppendLine("Category: " + CategoryID.ToString());
                    //stackTrace.AppendLine("");

                    // Get the distributors to send to.  Adding to a hashtable should ensure that they are only sent to once.
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string faxNumber = "";
                        faxNumber = dr["Fax"].ToString();

                        faxNumber = faxNumber.Replace("(", "");
                        faxNumber = faxNumber.Replace(")", "");
                        faxNumber = faxNumber.Replace(" ", "");
                        faxNumber = faxNumber.Replace("-", "");

                        // remove leading 1
                        if (faxNumber.Length > 0)
                        {
                            if (faxNumber.Substring(0, 1) == "1")
                                faxNumber = faxNumber.Remove(0, 1);

                            faxNumber = faxNumber + "@metrofax.com";

                            if (Distributors.ContainsKey(dr["pk_DistributorID"]) == false)
                                Distributors.Add(dr["pk_DistributorID"], faxNumber);
                        }
                        //stackTrace.AppendLine("DistributorID: " + dr.pk_DistributorID + "      Fax: " + faxNumber + "<BR>");

                    //}


                }

            }

            //stackTrace.AppendLine("<BR>-----------------------------------------------<BR>");

            List<Attachment> fileAttachments = new List<Attachment>();


            // Check to see if a file was posted
            if (fu1.PostedFile != null &&
               fu1.PostedFile.ContentLength > 0)
            {
                // attachFile.PostedFile.FileName contains 
                // the full path of the file. We only want the file
                // so we delimit it by forward slashes into an array

                string[] tempFileName =
                    fu1.PostedFile.FileName.Split('\\');

                // attachFile.PostedFile exposes a System.IO.Stream
                // property named InputStream

                Attachment emailAttach = new Attachment(
                    fu1.PostedFile.InputStream,
                    tempFileName[tempFileName.Length - 1]);

                fileAttachments.Add(emailAttach);
            }

            // Check to see if a file was posted
            if (fu2.PostedFile != null &&
               fu2.PostedFile.ContentLength > 0)
            {
                // attachFile.PostedFile.FileName contains 
                // the full path of the file. We only want the file
                // so we delimit it by forward slashes into an array

                string[] tempFileName =
                    fu2.PostedFile.FileName.Split('\\');

                // attachFile.PostedFile exposes a System.IO.Stream
                // property named InputStream

                Attachment emailAttach = new Attachment(
                    fu2.PostedFile.InputStream,
                    tempFileName[tempFileName.Length - 1]);

                fileAttachments.Add(emailAttach);
            }

            // Check to see if a file was posted
            if (fu3.PostedFile != null &&
               fu3.PostedFile.ContentLength > 0)
            {
                // attachFile.PostedFile.FileName contains 
                // the full path of the file. We only want the file
                // so we delimit it by forward slashes into an array

                string[] tempFileName =
                    fu3.PostedFile.FileName.Split('\\');

                // attachFile.PostedFile exposes a System.IO.Stream
                // property named InputStream

                Attachment emailAttach = new Attachment(
                    fu3.PostedFile.InputStream,
                    tempFileName[tempFileName.Length - 1]);

                fileAttachments.Add(emailAttach);
            }




            BR.Lead myLead = new Dealer_Locator.BR.Lead(false);

            int FaxCount = 0;
            int ErrorCount = 0;

            bool ErrorOccurred = false;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

             DA.ContractTDSTableAdapters.DistributorTableAdapter dta2 = new Dealer_Locator.DA.ContractTDSTableAdapters.DistributorTableAdapter();



            foreach (DictionaryEntry DicEntry in Distributors)
            {

                string faxNumber = DicEntry.Value.ToString();


                string Distributor = DicEntry.Key.ToString();
                string DistributorName = DDA.DataAccess.Distributor_da.GetDistributorName(Convert.ToInt32(Distributor));

                // Debugging
                //faxNumber = "8006086479@mhsfax.com";  // Debugging only

                Page myPage = this.Page;

                string errorMessage = "";
                //errorMessage = myLead.SendEmail("fax@findbomag.com", faxNumber, "",  "", txtSubject.Text, "findbomagco", "Distributor Fax", ref myPage, fileAttachments);
                errorMessage = myLead.SendEmail("fax@findbomag.com", faxNumber, "", "", txtSubject.Text, "findbomagco1!", "Distributor Fax", ref myPage, fileAttachments);

                
                if (errorMessage != "")
                {
                    sb.AppendLine("Error Faxing To Distributor [" + Distributor + "] at [ " + faxNumber + "]: " + errorMessage);
                    stackTrace.AppendLine("<font color=\"red\">Fax to " + DistributorName + " (id: " + Distributor + ") at " + faxNumber + " <b>FAILED</b></font><BR>");

                    ErrorCount = ErrorCount + 1;
                }
                else
                {
                    FaxCount = FaxCount + 1;

                    stackTrace.AppendLine("<font color=\"green\">Fax to " + DistributorName + " (id: " + Distributor + ") at " + faxNumber + " <b>SUCCEEDED</b></font><BR>");

                }

            }

            if (sb.ToString() == "")
                sb.Append("NONE");

            litResults.Text = "Faxes Successful: " + FaxCount + "<BR>" + "Faxes Failed: " + ErrorCount + "<BR><BR>" + "[Errors]" + "<BR>" + sb.ToString();

            litStackTrace.Text = stackTrace.ToString();


            // send the fax
            //myLead.SendFax(


        }
    }
}
