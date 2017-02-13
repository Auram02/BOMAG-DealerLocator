using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Dealer_Locator.DA
{
    public class DataAccess
    {
        public static string DataAccessConnectionString;

        public static string GetConnectionString()
        {
            return GetConnectionString("DealerLocatorConnectionString");
        }

        public static string GetConnectionString(string ConnectionName)
        {
            string returnString;

            try
            {
                returnString = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();

            }
            catch
            {
                returnString = @"data source=Arad\AradSQL;Initial Catalog=DEALERLOCATORCopyTest;Persist Security Info=True;User ID=dl;Password=dl";
            }

            return returnString;
        }

        public static SqlConnection GetDatabaseConnection()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            return conn;
        }

        public static OleDbConnection GetDatabaseConnection(string filePath)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";");
            return conn;
        }


        public static string Update(string sql)
        {
            string error = string.Empty;

            using (SqlConnection conn = GetDatabaseConnection())
            {
                
                SqlTransaction trans;

                conn.Open();
                trans = conn.BeginTransaction();

                try
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    

                    cmd.Transaction = trans;
                    cmd.CommandTimeout = 90;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    trans.Commit();

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                    error = ex.Message;

                    trans.Rollback();
                }
                finally
                {
                    // close the connection
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }

                return error;
            }

        }

        public static DataSet Read(string sql)
        {
            DataSet _ds = new DataSet();

            using (SqlConnection conn = GetDatabaseConnection())
            {
                try
                {
                    conn.Open();

                    SqlDataAdapter _da = new SqlDataAdapter(sql, conn);
                    _da.Fill(_ds);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return _ds;
        }

        public static DataSet Read(string sql, SqlConnection conn)
        {
            DataSet _ds = new DataSet();

            using (conn)
            {
                try
                {
                    conn.Open();

                    SqlDataAdapter _da = new SqlDataAdapter(sql, conn);
                    _da.Fill(_ds);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return _ds;
        }

        public static DataSet Read(string sql, OleDbConnection conn)
        {
            DataSet _ds = new DataSet();

            using (conn)
            {
                try
                {
                    conn.Open();

                    OleDbDataAdapter _da = new OleDbDataAdapter(sql, conn);
                    _da.Fill(_ds);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return _ds;
        }

        //public static void PrepareSQL(out string item)
        //{
        //   // using an out will be faster than a return
        //   item = item.Replace("'","''");
        //}

        public static int GetNextID(string p_tableName, string p_columnName)
        {
            int returnVal;

            try
            {
                string sql = "SELECT MAX(" + p_columnName + ") FROM " + p_tableName;

                DataSet ds = Read(sql);
                returnVal = (int)ds.Tables[0].Rows[0][0];
            }
            catch (Exception ex)
            {
                returnVal = -1;
            }

            return (returnVal + 1);

        }

        public static int GetNextID(string p_tableName, string p_columnName, string DestinationConnectionString)
        {
            int returnVal;

            SqlConnection conn = new SqlConnection(DestinationConnectionString);

            try
            {
                string sql = "SELECT MAX(" + p_columnName + ") FROM " + p_tableName;

                DataSet ds = Read(sql,conn);
                returnVal = (int)ds.Tables[0].Rows[0][0];
            }
            catch (Exception ex)
            {
                returnVal = -1;
            }

            return (returnVal + 1);

        }

        public static DataSet GetAllData(string p_tableName)
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT * FROM " + p_tableName;

            ds = Read(sql);

            return ds;
        }

        public static DataSet GetAllData_AccessDatabase(string p_tableName, string filePath)
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT * FROM " + p_tableName;

            OleDbConnection conn = GetDatabaseConnection(filePath);

            ds = Read(sql, conn);


            int counter = ds.Tables[0].Rows.Count;

            return ds;

        }

    }
}
