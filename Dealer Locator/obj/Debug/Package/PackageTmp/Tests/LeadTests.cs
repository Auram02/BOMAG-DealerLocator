using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using NUnit.Framework;

using System.Xml;
using System.Collections.Generic;

using System.Net.Mail;

namespace Dealer_Locator.Tests
{   
    [TestFixture]
    public class LeadTests
    {

    

        [SetUp]
        public void Init()
        {

        }

        [Test, Category("Tests")]
        public void GetLeadsByLastNameTest()
        {
            string lastName = "Medina";

            DA.LeadsTDS.DL_LeadDataTable ldt = BR.Lead.GetLeadsByLastName(lastName);

            Assert.IsTrue(ldt.Rows.Count > 0, "No leads found for the last name '" + lastName + "'");

        }

        /// <summary>
        /// This test will create a lead that is pending, then send it to test the recreation of all emails for that lead.
        /// </summary>
        [Test, Category("Tests")]
        public void NewSendPendingLead()
        {
            // set 126 to not sent
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            lta.UpdateSentStatus(false, false, "", 126);


            BR.Lead.SendPendingLeads();
            
            //Assert.IsTrue(ldt.Rows.Count > 0, "No leads found for the last name '" + lastName + "'");

        }


        /// <summary>
        /// This test will create a lead that is pending, then send it to test the recreation of all emails for that lead.
        /// </summary>
        [Test, Category("Tests")]
        public void FaxTest()
        {
            string faxNumber = "8006086479@mhsfax.com";
            string fromAddress = "fax@findbomag.com";

            System.Web.UI.Page page = new Page();

            BR.Lead lead = new BR.Lead(false);
            string errorMessage = lead.SendEmail("fax@findbomag.com", faxNumber, "", "Test Fax FaxTest()", "Test Fax FaxTest() Body", "findbomagco1!", "Distributor Fax", ref page, null, false);

        }


        /// <summary>
        /// This test will create a lead that is pending, then send it to test the recreation of all emails for that lead.
        /// </summary>
        [Test, Category("Tests")]
        public void FaxTest2()
        {

            string faxNumber = "8006086479@mhsfax.com";
            string fromAddress = "fax@findbomag.com";

            //Assert.IsTrue(ldt.Rows.Count > 0, "No leads found for the last name '" + lastName + "'");
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("neil.wilson@gmail.com", "Strider02");

            try
            {

                using (var message = new MailMessage("neil.wilson@gmail.com", faxNumber))
                {
                    message.Subject = "Test Fax from .NET";
                    message.Body = "Test Fax Body from .NET";
                    message.IsBodyHtml = false;
                    smtp.Timeout = 60000;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
 

        [Test, Category("Tests")]
        public void LeadXmlTest()
        {

            BR.Lead testLead = new Dealer_Locator.BR.Lead();

            System.Xml.XmlNode xNode = testLead.LoadLeadToXML(170);



            Assert.IsNotNull(xNode);
            
            XmlDocument xDoc = new XmlDocument();
            XmlNode root = xDoc.FirstChild;

         

            xDoc.InsertAfter(xDoc.ImportNode((XmlNode)xNode,true),root);

            testLead.ReadLeadFromXML(xNode);


        }

        [Test, Category( "Tests" )]
        public void LeadXmlTest_IncompleteXml()
        {

            BR.Lead testLead = new Dealer_Locator.BR.Lead();

            XmlDocument xDoc = new XmlDocument();

            XmlNode xNode = xDoc.CreateNode( XmlNodeType.Element, "Lead", null );
            xNode.InnerXml = "<Data><errorOccurred>False</errorOccurred><errorValue></errorValue><pk_leadID>1</pk_leadID><sendDate>2/10/2008 12:00:00 AM</sendDate><submitDate>2/10/2008 12:00:00 AM</submitDate><submitted>False</submitted></Data>";


            testLead.ReadLeadFromXML( xNode );


        }

        

        [Test, Category("Tests")]
        public void LeadExportTest()
        {
            Dealer_Locator.Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync();


            string SourceDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorConnectionString");

            List<int> newLeadIDs = dbSync.GetNewSalesLeadForms(SourceDatabaseConnection);

            XmlDocument xDoc = BR.Lead.CreateNewLeadXmlDataSet(newLeadIDs);

            string output = xDoc.OuterXml;

            Assert.IsNotNull(xDoc);
            Assert.AreNotEqual(output, "");
            

        }

        [Test, Category("Tests")]
        public void DeleteLead()
        {

            int leadID = 115;

            bool LeadRemovalResult = BR.Lead.RemoveLead(leadID);

            Assert.IsTrue(LeadRemovalResult, "Lead Removal for LeadID " + leadID + " failed.");

        }

    }
}
