using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using Dealer_Locator.DA;

namespace Dealer_Locator.DDA.DataAccess
{
    class Category_da
    {

        public static int GetCategoryID(string p_Name)
        {
            string sql;
            int catID;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_Name);

            sql = "SELECT CategoryID FROM Category WHERE CategoryName = '" + p_Name + "'";

            DataSet ds = new DataSet();
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            catID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            return catID;
        }

        public static bool DoLogin(string p_Name, string p_Password, ref bool isAdmin)
        {
            string sql;
            bool catID;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_Name);

            sql = "SELECT Count(userID) FROM [Users1] WHERE [Login1] = '" + p_Name + "' AND [Password1] = '" + p_Password + "'";

            DataSet ds = new DataSet();
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            catID = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

            return catID;
        }


        public static string GetCategoryName(int p_id)
        {
            string sql;
            string catName;

            sql = "SELECT CategoryName FROM Category WHERE CategoryID = " + p_id;

            DataSet ds = new DataSet();
             ds = Dealer_Locator.DA.DataAccess.Read(sql);

            catName = Convert.ToString(ds.Tables[0].Rows[0][0]);

            return catName;
        }


        public static DataSet GetCategoryList()
        {
            DataSet ds;
            string sql;

            sql = "SELECT CategoryName, CategoryID FROM Category";
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static void RemoveCategory(int p_id)
        {
            string sql;

            sql = "DELETE FROM Category WHERE CategoryID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);
            
            sql = "DELETE FROM ContractCategory WHERE fk_categoryID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

        }

        public static void AddCategory(int p_id, string p_name)
        {
            string sql;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_name);

            System.Data.OleDb.OleDbCommand cmd1 = new System.Data.OleDb.OleDbCommand();

            //cmd1.CommandText = "INSERT INTO Category VALUES(@P1, @P2)";

            sql = "INSERT INTO Category VALUES(" + p_id + ",'" + p_name + "')";
            Dealer_Locator.DA.DataAccess.Update(sql);

        }

        public static void UpdateCategory(int p_id, string p_name)
        {
            string sql;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_name);

            sql = "UPDATE Category SET CategoryName = '" + p_name + "' WHERE CategoryID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

        }


    }
}
