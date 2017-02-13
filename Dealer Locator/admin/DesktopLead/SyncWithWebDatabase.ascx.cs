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

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class SyncWithWebDatabase : System.Web.UI.UserControl
    {
        string SourceDatabaseConnection = "";
        string DestinationDatabaseConnection = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DestinationDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorConnectionString");
            SourceDatabaseConnection = DA.DataAccess.GetConnectionString("DealerLocatorDatabaseCopy");

            string[] Source = SourceDatabaseConnection.Split(';');
            string[] Destination = DestinationDatabaseConnection.Split(';');

            Source[0] = Source[0].Replace("data source=", "<b>Server: </b>");
            Source[1] = Source[1].Replace("Initial Catalog=", "<b>Database:</b> ");

            Destination[0] = Destination[0].Replace("data source=", "<b>Server: </b>");
            Destination[1] = Destination[1].Replace("Initial Catalog=", "<b>Database:</b> ");

            litSourceDatabase.Text = Source[0] + Environment.NewLine + Source[1];
            litDestinationDatabase.Text = Destination[0] + Environment.NewLine + Environment.NewLine + Destination[1];
        }

        protected void btnSyncData_Click(object sender, EventArgs e)
        {
            Dealer_Locator.Utilities.DatabaseSync dbSync = new Dealer_Locator.Utilities.DatabaseSync(SourceDatabaseConnection, DestinationDatabaseConnection);

            int tableCount = dbSync.SyncTables();


            lblResult.Text = tableCount + " of " + dbSync.TableToSyncCount + " tables Sync'd Successfully";

        }
    }
}