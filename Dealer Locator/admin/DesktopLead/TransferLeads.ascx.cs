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

using System.IO;
using System.Xml;

using System.Threading;

using System.Collections.Generic;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class TransferLeads1 : System.Web.UI.UserControl
    {
        string SourceDatabaseConnection = "";
        string DestinationDatabaseConnection = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblRecordsTransfered.Visible = false;
            lblRecordsTransferedLabel.Visible = false;
            litResult.Visible = false;
            litResult.Text = "";
            litResultProcessing.Text = "";
            litResultProcessing.Visible = false;
            litReloadLink.Visible = false;
            litReloadLink.Text = "";

            //DealerLocatorDatabaseCopy

            SourceDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorConnectionString");
            DestinationDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorDatabaseCopy");

            string[] Source = SourceDatabaseConnection.Split(';');
            string[] Destination = DestinationDatabaseConnection.Split(';');

            Source[0] = Source[0].Replace("data source=", "<b>Server: </b>");
            Source[1] = Source[1].Replace("Initial Catalog=", "<b>Database:</b> ");

            Destination[0] = Destination[0].Replace("data source=", "<b>Server: </b>");
            Destination[1] = Destination[1].Replace("Initial Catalog=", "<b>Database:</b> ");

            litSourceDatabase.Text = Source[0] + Environment.NewLine + Source[1];
            litDestinationDatabase.Text = Destination[0] + Environment.NewLine + Environment.NewLine + Destination[1];

            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            //int numberOfLeads =((int) lta.GetLeadCount());
            int numberOfLeads = ((int)lta.GetLeadCount_NonSubmitted());

            lblRecordsToTransfer.Text = numberOfLeads.ToString();

            string str = string.Empty;

            if (Session["XMLLEADS_FILENAME"] != null && Session["XMLLEADS_FILENAME"].ToString().Length > 0)
            {
                str = "document.getElementById('" + tblProcessLeads.ClientID + "').style.display='block';";
                
            }else{
                str = "document.getElementById('" + tblProcessLeads.ClientID + "').style.display='none';";

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "hideShowtblProcessLeads", str, true);

        }

        protected void btnTransferLeads_Click(object sender, EventArgs e)
        {

            string returnMessage = "";



            // get leads...

            Dealer_Locator.Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync();


            string SourceDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorConnectionString");

            List<int> newLeadIDs = dbSync.GetNewSalesLeadForms(SourceDatabaseConnection);

            XmlDocument xDoc = BR.Lead.CreateNewLeadXmlDataSet(newLeadIDs);


            // now process



            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);

            // Save Xml Document to Text Writter.
            xDoc.WriteTo(xw);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            // Convert Xml Document To Byte Array.
            byte[] docAsBytes = encoding.GetBytes(sw.ToString());

            admin.WebServices.LeadSender ls = new Dealer_Locator.admin.WebServices.LeadSender();
            Random randomGenerator = new Random();
            int ranNumber = randomGenerator.Next(1, 20000);

            string FileName = "XmlLeads_" + ranNumber.ToString() + ".xml";

            Session["XMLLEADS_FILENAME"] = FileName;

            com.findbomag.www.LeadSender leadSender = new Dealer_Locator.com.findbomag.www.LeadSender();
            string result = leadSender.UploadLeads(docAsBytes, FileName);

            if (result == "OK")
            {
                litResult.Text = "<b>The Leads have finished transferring.<BR>No errors were detected.</b><BR><BR>";
                lblRecordsTransfered.Text = lblRecordsToTransfer.Text;
                lblRecordsTransfered.Visible = true;

                lblRecordsTransferedLabel.Visible = true;

            }
            else
            {
                litResult.Text = "<BR><BR><b>The Leads have finished transferring however an error occurred during the process.  No lead data was lost.<BR><BR>Please save the following error message and contact the developer with it.<BR><BR>FileName: " + FileName + "<BR><BR>Error: " + result + "</b>";

            }

            litResult.Visible = true;

            lblFileNameToProcess.Text = FileName;
            lblRecordsToTransfer.Text = "0";

            string str = "document.getElementById('" + tblProcessLeads.ClientID + "').style.display='block';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "hideShowtblProcessLeads2", str, true);

        }


        protected void btnProcessLeads_Click(object sender, EventArgs e)
        {
            string xmlFileName = Session["XMLLEADS_FILENAME"].ToString();

            com.findbomag.www.LeadSender leadSender = new Dealer_Locator.com.findbomag.www.LeadSender();
            leadSender.Timeout = Timeout.Infinite;
            string result = leadSender.ProcessUploadedLeads(xmlFileName);

            if (result == "")
            {
                litResult.Text = "<b>The Leads have finished processing.<BR>No errors were detected.</b><BR><BR>";
                lblRecordsTransfered.Text = lblRecordsToTransfer.Text;
                lblRecordsTransfered.Visible = true;

                lblRecordsTransferedLabel.Visible = true;

            }
            else
            {
                litResultProcessing.Text = "<BR><BR><b>The Leads have finished processing however an error occurred during the process.  No lead data was lost.<BR><BR>Please save the following filename and error message and contact the developer with it.<BR><BR>FileName: " + xmlFileName + "<BR><BR>Error: " + result + "</b>";

            }

            litResultProcessing.Visible = true;
            Session["XMLLEADS_FILENAME"] = string.Empty;
        }

    }
}