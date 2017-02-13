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

namespace Dealer_Locator.Tests
{
    [TestFixture]
    public class DatabaseSyncTests
    {

        private Dealer_Locator.Utilities.DatabaseSync dbSync;
        private string productionConnectionString = @"data source=Arad\AradSQL;Initial Catalog=DEALERLOCATOR;Persist Security Info=True;User ID=dl;Password=dl";

        private string copyDatabaseString = @"data source=Arad\AradSQL;Initial Catalog=DEALERLOCATORCopyTest;Persist Security Info=True;User ID=dl;Password=dl";


        [SetUp]
        public void Init()
        {
            dbSync = new Dealer_Locator.Utilities.DatabaseSync(productionConnectionString, copyDatabaseString);
        }

        [Test, Category("Tests")]
        public void CheckConnectionTest()
        {
            Assert.IsTrue(dbSync.ConnectedToInternet(), "Not connected to internet");
        }


        [Test, Category("Tests")]
        public void GetTableNamesTest()
        {
            DataTable returnTable = dbSync.GetTableNames(productionConnectionString);
            Assert.Greater(returnTable.Rows.Count, 0, "No table names returned");

        }

        [Test, Category("Tests")]
        public void SyncTablesTest()
        {
            int tableCount = dbSync.SyncTables();

            System.Diagnostics.Debug.WriteLine("Tables Sync'd: " + tableCount.ToString());

            Assert.Greater(tableCount, 0, "No tables were sync'd");
             
        }

        [Test, Category("Tests")]
        public void SubmitNewLeadsTest()
        {

            int rowsProcessed = dbSync.UploadNewLeadsToWeb(copyDatabaseString, productionConnectionString,0);

            System.Diagnostics.Debug.WriteLine("New Leads Sync'd: " + rowsProcessed.ToString());
            
            Assert.Greater(rowsProcessed, 0, "No rows were uploaded to the web");


        }


    }
}
