using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

using System.IO;
using System.Threading;

using System.Xml;

namespace Dealer_Locator.admin.WebServices
{
    /// <summary>
    /// Summary description for LeadSender
    /// </summary>
    [WebService(Namespace = "http://www.findbomag.com/", Description = "Runs all jobs that have not completed yet")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class LeadSender : System.Web.Services.WebService
    {

        protected static Mutex mut = new Mutex();


        [WebMethod]
        public void SendLeads()
        {

            try
            {
                mut.WaitOne();
                Dealer_Locator.BR.Lead.SendPendingLeads();


            }
            finally
            {
                mut.ReleaseMutex();
            }
        }

        [WebMethod]
        public string ProcessUploadedLeads(string filePath)
        {
            string returnMessage = string.Empty;

            try
            {

                filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/admin/DataImport/Data/") + filePath;

                // load the xml document into memory
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(filePath);

                
                Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync();

                returnMessage = dbSync.ProcessXMLLeads(xdoc);

            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
            }

            return returnMessage;
        }

        [WebMethod]
        public string UploadLeads(byte[] f, string fileName)
        {
            string returnMessage = "";

            // the byte array argument contains the content of the file
            // the string argument contains the name and extension
            // of the file passed in the byte array
            try
            {
                mut.WaitOne();

                // instance a memory stream and pass the
                // byte array to its constructor
                MemoryStream ms = new MemoryStream(f);



                // instance a filestream pointing to the
                // storage folder, use the original file name
                // to name the resulting file
                FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath
                                                        ("~/admin/DataImport/Data/") + fileName, FileMode.Create);

                string filePath = System.Web.Hosting.HostingEnvironment.MapPath
                                                        ("~/admin/DataImport/Data/") + fileName;

                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
                
                if (returnMessage == "")
                {

                    // return OK if we made it this far
                    returnMessage = "OK";
                }


            }

            catch (Exception ex)
            {

                // return the error message if the operation fails
                return ex.Message.ToString();

            }
            finally
            {
                mut.ReleaseMutex();
            }


            return returnMessage;

        }

    }
}
