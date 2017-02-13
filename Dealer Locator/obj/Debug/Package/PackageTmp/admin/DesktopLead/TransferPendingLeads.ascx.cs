using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Threading;

using System.Xml;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class TransferPendingLeads : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }


        protected void btnSendPendingLeads_Click(object sender, EventArgs e)
        {
            BR.Lead.SendPendingLeads();
            litResult.Text = BR.Lead.PendingLeadErrorMessage;
        }

        protected void btnUploadXMLFile_Click(object sender, EventArgs e)
        {
            string uploadPath = "";

            if (FileUpload1.HasFile == true)
            {
                uploadPath = System.Web.Hosting.HostingEnvironment.MapPath
                                                        ("~/admin/DataImport/Data/") + FileUpload1.FileName.ToString();


                FileUpload1.SaveAs(uploadPath);


                string returnMessage = "";

                try
                {
                    // load the xml document into memory
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(uploadPath);

                    Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync();

                    returnMessage = dbSync.ProcessXMLLeads(xdoc);

                    if (returnMessage == "")
                    {

                        // return OK if we made it this far
                        returnMessage = "OK";
                    }

                    litResult.Text = "leads uploaded and processed successfully\n" + returnMessage;

                }
                catch (Exception ex)
                {
                    litResult.Text = ex.Message;
                }
            }
            else
            {

                litResult.Text = "Please upload a file first.";
            }
        }

    }
}