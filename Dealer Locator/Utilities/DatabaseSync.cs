using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;

using System.Data.SqlClient;

using System.Collections.Generic;

using System.Xml;

namespace Dealer_Locator.Utilities
{
    public class DatabaseSync
    {


        string _sourceConnectionString;
        string _destinationConnectionString;

        public int TableToSyncCount;

        public DatabaseSync()
        {
            TableToSyncCount = 0;
        }

        public DatabaseSync(string sourceConnectionString,
                    string destinationConnectionString)
        {
            TableToSyncCount = 0;

            _sourceConnectionString =
                        sourceConnectionString;
            _destinationConnectionString =
                        destinationConnectionString;
        }

        public void DeleteTableData(string table)
        {
            using (SqlConnection source =
        new SqlConnection(_destinationConnectionString))
            {
                string sql = string.Format("DELETE FROM {0}",
                                                          table);

                SqlCommand command = new SqlCommand(sql, source);

                source.Open();
                command.ExecuteNonQuery();
                source.Close();
            }
        }


        public int SyncTables()
        {

            DataTable returnTable = GetTableNames(this._sourceConnectionString);

            int ProcessedTableCount = 0;
            TableToSyncCount = 0;


            foreach (DataRow dr in returnTable.Rows)
            {


                string tableName = dr[0].ToString();


                tableName = "[" + tableName + "]";

                if (tableName.Contains("ZipLookup") == false && tableName.Contains("[DL.Lead]") == false &&
                    tableName.Contains("[DL.LeadEmail]") == false && tableName.Contains("DL.LeadValues") == false &&
                    tableName.Contains("DL.LeadProduct") == false && tableName.Contains("sysdiagrams") == false)
                {
                    TableToSyncCount += 1;
                }

            }

            foreach (DataRow dr in returnTable.Rows)
            {


                string tableName = dr[0].ToString();

                tableName = "[" + tableName + "]";

                if (tableName.Contains("ZipLookup") == false && tableName.Contains("[DL.Lead]") == false &&
                    tableName.Contains("[DL.LeadEmail]") == false && tableName.Contains("DL.LeadValues") == false &&
                    tableName.Contains("DL.LeadProduct") == false && tableName.Contains("sysdiagrams") == false)
                {

                    //tableName = tableName.Replace(".", "].[");

                    DeleteTableData(tableName);
                    CopyTable(tableName);

                    ProcessedTableCount += 1;
                }
            }

            return ProcessedTableCount;

        }

        private void CopyTable(string DestinationTableName)
        {
            if (DestinationTableName.Contains(".") == true)
            {
                CopyTableManual(DestinationTableName);
            }
            else
            {
                CopyTableBulkCopy(DestinationTableName);
            }
        }


        /// <summary>
        /// This method will copy the data in a table 
        /// from one database to another. The
        /// source and destination can be from any type of 
        /// .NET database provider.
        /// </summary>
        /// <param name="source">Source database connection</param>
        /// <param name="destination">Destination database connection</param>
        /// <param name="sourceSQL">Source SQL statement</param>
        /// <param name="destinationTableName">Destination table name</param>
        private void CopyTableManual(
            String destinationTableName)
        {
            SqlConnection source = new SqlConnection(_sourceConnectionString);
            SqlConnection destination = new SqlConnection(_destinationConnectionString);

            string sourceSQL = string.Format("SELECT * FROM {0}", destinationTableName);


            //System.Diagnostics.Debug.WriteLine(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") +
            //   " " + destinationTableName + " load started");
            IDbCommand cmd = source.CreateCommand();
            cmd.CommandText = sourceSQL;
            //            System.Diagnostics.Debug.WriteLine("\tSource SQL: " + sourceSQL);
            try
            {
                source.Open();
                destination.Open();
                IDataReader rdr = cmd.ExecuteReader();
                DataTable schemaTable = rdr.GetSchemaTable();

                IDbCommand insertCmd = destination.CreateCommand();
                string paramsSQL = String.Empty;

                //build the insert statement
                foreach (DataRow row in schemaTable.Rows)
                {
                    if (paramsSQL.Length > 0)
                        paramsSQL += ", ";
                    paramsSQL += "@" + row["ColumnName"].ToString();

                    IDbDataParameter param = insertCmd.CreateParameter();
                    param.ParameterName = "@" + row["ColumnName"].ToString();
                    param.SourceColumn = row["ColumnName"].ToString();

                    if (row["DataType"] == typeof(System.DateTime))
                    {
                        param.DbType = DbType.DateTime;
                    }

                    //Console.WriteLine(param.SourceColumn);
                    insertCmd.Parameters.Add(param);
                }
                insertCmd.CommandText =
                    String.Format("insert into {0} ( {1} ) values ( {2} )",
                    destinationTableName, paramsSQL.Replace("@", String.Empty),
                    paramsSQL);
                int counter = 0;
                int errors = 0;
                while (rdr.Read())
                {
                    try
                    {
                        foreach (IDbDataParameter param in insertCmd.Parameters)
                        {
                            object col = rdr[param.SourceColumn];

                            //special check for SQL Server and 
                            //datetimes less than 1753
                            if (param.DbType == DbType.DateTime)
                            {
                                if (col != DBNull.Value)
                                {
                                    //sql server can not have dates less than 1753
                                    if (((DateTime)col).Year < 1753)
                                    {
                                        param.Value = DBNull.Value;
                                        continue;
                                    }
                                }
                            }

                            param.Value = col;

                            //uncomment this line to see the 
                            //values being used for the insert
                            //System.Diagnostics.Debug.WriteLine( param.SourceColumn + " --> " + 
                            //param.ParameterName + " = " + col.ToString() );
                        }
                        insertCmd.ExecuteNonQuery();
                        //un-comment this line to get a record count. You may only want to show status for every 1000 lines
                        //this can be done by using the modulus operator against the counter variable
                        //System.Diagnostics.Debug.WriteLine(++counter);
                    }
                    catch (Exception ex)
                    {
                        if (errors == 0)
                            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                        errors++;
                    }
                }
                //System.Diagnostics.Debug.WriteLine(errors + " errors");
                //System.Diagnostics.Debug.WriteLine(counter + " records copied");
                //System.Diagnostics.Debug.WriteLine(System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss") +
                //" " + destinationTableName + " load completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                destination.Close();
                source.Close();
            }
        }


        private void CopyTableBulkCopy(string table)
        {
            using (SqlConnection source =
                    new SqlConnection(_sourceConnectionString))
            {
                string sql = string.Format("SELECT * FROM {0}",
                                                          table);

                SqlCommand command = new SqlCommand(sql, source);

                source.Open();
                IDataReader dr = command.ExecuteReader();

                using (SqlBulkCopy copy =
                        new SqlBulkCopy(_destinationConnectionString))
                {
                    copy.DestinationTableName = table;
                    copy.WriteToServer(dr);
                }
            }


        }

        public bool ConnectedToInternet()
        {
            System.Net.NetworkInformation.PingReply pingReply;
            System.Net.NetworkInformation.Ping pinger = new System.Net.NetworkInformation.Ping();

            pingReply = pinger.Send("findbomag.com");

            if (pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;

        }

        public List<int> GetNewSalesLeadForms(string SourceConnectionString)
        {
            List<int> returnList = new List<int>();

            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();
            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();

            lta.Connection.ConnectionString = SourceConnectionString;

            //ldt = lta.GetData();
            ldt = lta.GetData_NotSubmitted();

            foreach (DA.LeadsTDS.DL_LeadRow dr in ldt.Rows)
                returnList.Add(dr.pk_leadID);

            return returnList;
        }

        public string ProcessXMLLeads(XmlDocument xDoc)
        {
            StringBuilder returnString = new StringBuilder();

            BR.Lead lead = new Dealer_Locator.BR.Lead();

            XmlNodeList LeadNodeList = xDoc.SelectNodes("//Lead");

            int counter = 0;

            // loop through each lead xml node
            // as each is hit, read it and then process it.
            foreach (XmlNode LeadNode in LeadNodeList)
            {
                try
                {

                    BR.Lead leadObject = new Dealer_Locator.BR.Lead();
                    leadObject.ReadLeadFromXML(LeadNode);

                    leadObject.CommitLeadToDatabase();

                    counter += 1;


                }
                catch (Exception ex)
                {
                    returnString.AppendLine(ex.Message);
                }

            }

            try
            {
                Dealer_Locator.BR.Lead.SendPendingLeads();
            }
            catch (Exception ex)
            {
                returnString.AppendLine(ex.Message);
            }

            return returnString.ToString();

        }

        public int UploadNewLeadsToWeb(string SourceConnectionString, string DestinationDatabaseConnectionString, int tryNumber)
        {
            int rowsProcessed = 0;

            List<int> newLeadIDs = GetNewSalesLeadForms(SourceConnectionString);

            BR.Lead standardLead = new Dealer_Locator.BR.Lead(true);


            List<int> nonSumbittedLeads = new List<int>();

            // only try to send the leads 3 times
            if (tryNumber < 1)
            {

                foreach (int leadID in newLeadIDs)
                {
                    try
                    {
                        SqlConnection conn = new SqlConnection(SourceConnectionString);


                        BR.Lead tempLead = new Dealer_Locator.BR.Lead(conn, leadID);

                        // set in kiosk mode so that the lead is saved as "not sent yet"
                        tempLead.InKioskMode = true;

                        tempLead.CommitLeadToDatabase(DestinationDatabaseConnectionString);

                        // switch the tempLead to "submitted"
                        bool HasBeenUpdated = standardLead.UpdateLeadStatus(true, leadID);

                        if (HasBeenUpdated == true)
                            rowsProcessed += 1;


                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (newLeadIDs.Count != rowsProcessed)
                    rowsProcessed += UploadNewLeadsToWeb(SourceConnectionString, DestinationDatabaseConnectionString, tryNumber + 1);

            }





            return rowsProcessed;
        }

        public DataTable GetTableNames(string connectionString)
        {

            DataTable tables = new DataTable("Tables");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "select table_name as Name from INFORMATION_SCHEMA.Tables where TABLE_TYPE = 'BASE TABLE'";
                connection.Open();
                tables.Load(command.ExecuteReader(
                                CommandBehavior.CloseConnection));
            }

            return tables;

        }



    }
}
