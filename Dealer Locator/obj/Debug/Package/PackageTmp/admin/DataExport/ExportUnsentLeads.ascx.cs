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
using System.Xml;

using System.Collections.Generic;
namespace Dealer_Locator.admin.DataExport
{
    public partial class ExportUnsentLeads1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                lblResult.Visible = false;
            }

        }

        protected void btnExportUnsentLeads_Click(object sender, EventArgs e)
        {
            Dealer_Locator.Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync();

            string SourceDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorConnectionString");

            List<int> newLeadIDs = dbSync.GetNewSalesLeadForms(SourceDatabaseConnection);

            XmlDocument xDoc =  BR.Lead.CreateNewLeadXmlDataSet(newLeadIDs);

            string pathName = Server.MapPath("~/admin/DataExport/Data/");

            string fileName = "UnsentLeadsXML_" + BR.Utility.GenerateRandomCode() + ".xml";
            pathName = pathName + fileName;

            xDoc.Save(pathName);

            xmlDocUrl.Text = "<a href='" + pathName + "'>" + pathName + "</a>";

        }
    }
}