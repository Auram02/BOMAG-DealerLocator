using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace Dealer_Locator.DA
{
    public class LeadBlackList
    {

        public DataTable GetBlackList()
        {
            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            SqlDataAdapter da = new SqlDataAdapter("", sqlConn);

            SqlCommand comm = new SqlCommand("EXECUTE LeadBlackListSelect", sqlConn);

            da.SelectCommand = comm;

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds.Tables[0];



        }

        public string AddItem(string lastName, string phoneNumber, string city, string state, string zip)
        {
            string result = string.Empty;

            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            SqlCommand cmd = new SqlCommand("EXECUTE LeadBlackListInsert @lastName, @phone, @city, @state, @zip", sqlConn);
            cmd.Parameters.Add(new SqlParameter("lastName", lastName));
            cmd.Parameters.Add(new SqlParameter("phone", phoneNumber));
            cmd.Parameters.Add(new SqlParameter("city", city));
            cmd.Parameters.Add(new SqlParameter("state", state));
            cmd.Parameters.Add(new SqlParameter("zip", zip));


            SqlTransaction trans;


            try
            {
                sqlConn.Open();
                trans = sqlConn.BeginTransaction();

                cmd.Transaction = trans;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return result;
        }

        public string DeleteItem(int leadBlackListId)
        {

            string result = string.Empty;

            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            SqlCommand cmd = new SqlCommand("EXECUTE LeadBlackListDelete @leadBlackListId", sqlConn);
            cmd.Parameters.Add(new SqlParameter("leadBlackListId", leadBlackListId));

            SqlTransaction trans;

            try
            {
                sqlConn.Open();
                trans = sqlConn.BeginTransaction();

                cmd.Transaction = trans;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                trans.Commit();


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return result;

        }

        public string UpdateItem(int leadBlackListId, string emailAddress, int numberOfTriesToAdd)
        {

            string result = string.Empty;

            SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            SqlCommand cmd = new SqlCommand("EXECUTE LeadBlackListUpdate @leadBlackListId, @emailAddress, @numberOfTriesToAdd", sqlConn);
            cmd.Parameters.Add(new SqlParameter("leadBlackListId", leadBlackListId));
            cmd.Parameters.Add(new SqlParameter("emailAddress", emailAddress));
            cmd.Parameters.Add(new SqlParameter("numberOfTriesToAdd", numberOfTriesToAdd));

            SqlTransaction trans;

            try
            {
                sqlConn.Open();
                trans = sqlConn.BeginTransaction();

                cmd.Transaction = trans;
                cmd.CommandTimeout = 90;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                trans.Commit();


            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return result;

        }

    }

}