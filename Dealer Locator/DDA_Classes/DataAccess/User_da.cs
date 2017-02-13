using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Dealer_Locator.DDA.DataAccess
{
    class User_da
    {
        public static bool DoLogin(string login, string password, ref bool isAdmin)
        {
            bool result;
            result = false;

            DataSet dsUser;
            string sql;

            sql = "SELECT COUNT(userID) FROM [Users] WHERE [Login] = '" + login + "' AND [Password] = '" + password + "'";
            dsUser = Dealer_Locator.DA.DataAccess.Read(sql);

            
            try
            {
                if (Convert.ToInt32(dsUser.Tables[0].Rows[0][0]) > 0)
                {
                    // read our user
                    sql = "SELECT * FROM Users WHERE [Login] = '" + login + "' AND [Password] = '" + password + "'";
                    dsUser = Dealer_Locator.DA.DataAccess.Read(sql);

                    isAdmin = Convert.ToBoolean(dsUser.Tables[0].Rows[0]["Administrator"]);
                    Dealer_Locator.DataObjects.AppData.UserIsAdmin = isAdmin;
                    result = true;
                }
            } catch {
                result = false;
            }


            return result;
        }

        public static void AddUser(string p_name, string p_password, int p_Admin)
        {
            string sql;

            int nextID = Dealer_Locator.DA.DataAccess.GetNextID("Users", "userID");

            sql = "INSERT INTO Users VALUES (" + nextID + ",'" + p_name + "','" + p_password + "'," + p_Admin + ")";

            Dealer_Locator.DA.DataAccess.Update(sql);

        }

        public static void UpdateUser(int p_id, string p_name, string p_password, int p_Admin)
        {
            string sql;

            int nextID = Dealer_Locator.DA.DataAccess.GetNextID("Users", "userID");

            sql = "UPDATE Users SET [Login] = '" + p_name + "', [Password] = '" + p_password + "', Administrator = " + p_Admin + " WHERE userID = " + p_id;

            Dealer_Locator.DA.DataAccess.Update(sql);

        }
        
        public static void RemoveUser(int p_id)
        {
            string sql;

            sql = "DELETE FROM Users WHERE userID = " + p_id;

            Dealer_Locator.DA.DataAccess.Update(sql);

        }


        public static DataSet GetUsers()
        {
            DataSet ds;
            string sql;


            sql = "SELECT * FROM Users";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }
    }
}
