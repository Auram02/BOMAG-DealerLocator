using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.IO;
using System.Threading;

using System.Xml;

using System.Data;

namespace Dealer_Locator.admin.WebServices
{
    /// <summary>
    /// Summary description for MapReportService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MapReportService : System.Web.Services.WebService
    {

        protected static Mutex mut = new Mutex();

        [WebMethod]
        public string UploadReportData(DataSet ds, string legendTitle, List<string> stateList)
        {
            string returnMessage = "";

            DataTable dtContractInfo = ds.Tables[0];
            DataTable dtSubGroups = ds.Tables[1];

            try
            {
                mut.WaitOne();

                BR.MapReport mapReport = new BR.MapReport();
                string jsonData = mapReport.BuildReportData(dtContractInfo, dtSubGroups, legendTitle, stateList);

                Random rnd = new Random(DateTime.Now.Millisecond);

                string fileName = "report_" + rnd.Next(100000,9999999) + ".json";

                string mapPath = Server.MapPath("~/admin/Reports/Data/");

                if (!Directory.Exists(mapPath))
                    Directory.CreateDirectory(mapPath);

                string filePath = mapPath + fileName;

                File.WriteAllText(filePath, jsonData);

                if (returnMessage == "")
                {
                    // return OK if we made it this far
                    returnMessage = fileName;
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
