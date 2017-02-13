using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace Dealer_Locator.DDA.DataAccess
{
    class Contract_da
    {
        public static DataSet GetContractList(int p_id)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractNumber FROM [Contract] WHERE MainDistributorID = '" + p_id + "'";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);
            
            return ds;
        }

        public static DataSet GetContractList(int categoryID, int countyID)
        {
            DataSet ds = new DataSet();
            string sql = "";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);
            return ds;
        }

        public static int GetContractCount()
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Count(ContractNumber) FROM [Contract]";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            int returnCount;

            returnCount = (Int32)ds.Tables[0].Rows[0][0];
            return returnCount;
        }


        public static void DeleteContract(int p_id)
        {
            // Contract, ContractDistributor, ContractCategory, ContractCounty
            string sql;

            sql = "DELETE FROM ContractDistributor WHERE fk_ContractID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM ContractCategory WHERE fk_ContractID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM ContractCounty WHERE fk_ContractID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM [Contract] WHERE ContractID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

        }


        public static void GetContract(string p_contractNumber)
        {

//            Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_contractNumber);

            DataSet ds = new DataSet();
            string sql;

            int contractID;

            contractID = GetContractID(p_contractNumber);
            
            sql = "SELECT contractID, contractNumber, contractDate, ModifyDate, MainDistributorID " + 
                    "FROM [Contract] " +
                    "WHERE contractID = " + contractID;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID = Convert.ToInt32(ds.Tables[0].Rows[0]["contractID"]);
            Dealer_Locator.DataObjects.AppData.CurrentContract.ContractNumber = Convert.ToString(ds.Tables[0].Rows[0]["contractNumber"]);
            Dealer_Locator.DataObjects.AppData.CurrentContract.ContractDate = Convert.ToString(ds.Tables[0].Rows[0]["contractDate"]);
            Dealer_Locator.DataObjects.AppData.CurrentContract.ModifyDate = Convert.ToString(ds.Tables[0].Rows[0]["modifyDate"]);
            Dealer_Locator.DataObjects.AppData.DistributorID = Convert.ToInt32(ds.Tables[0].Rows[0]["MainDistributorID"]);


            int i;


            sql = "SELECT * FROM [ContractCategory] WHERE fk_contractID = " + contractID;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearCounties();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteCounties();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearBranches();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteBranches();

            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearCategories();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Dealer_Locator.DataObjects.AppData.CurrentContract.Categories.Add(ds.Tables[0].Rows[i]["fk_categoryID"]);
            }


            sql = "SELECT * FROM [ContractCounty] WHERE fk_contractID = " + contractID;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Dealer_Locator.DataObjects.AppData.CurrentContract.Counties.Add(ds.Tables[0].Rows[i]["fk_countyID"]);
            }

            sql = "SELECT * FROM [ContractDistributor] WHERE fk_contractID = " + contractID;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Dealer_Locator.DataObjects.AppData.CurrentContract.Branches.Add(ds.Tables[0].Rows[i]["fk_distributorID"]);
            }


        }

        public static DataSet GetContractCategories(string p_contractNumber)
        {
            DataSet ds;
            string sql;

            int contractID;

            contractID = Dealer_Locator.DDA.DataAccess.Contract_da.GetContractID(p_contractNumber);

            sql = "SELECT DISTINCT CategoryName FROM [Category], [ContractCategory]" +
                    " WHERE [ContractCategory].fk_CategoryID = [Category].CategoryID" +
                    " AND [ContractCategory].fk_ContractID = " + contractID + " ORDER BY CategoryName";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static DataSet GetContractStates(string p_contractNumber)
        {
            DataSet ds;
            string sql;

            int contractID;

            contractID = Dealer_Locator.DDA.DataAccess.Contract_da.GetContractID(p_contractNumber);

            sql = "SELECT DISTINCT Abbreviation FROM [State], [ContractCounty], County" +
                    " WHERE [ContractCounty].fk_countyID = [County].CountyID" +
                    " AND [County].fk_StateID = [State].StateID" +
                    " AND [ContractCounty].fk_contractID = " + contractID;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static DataSet GetOccupiedCounties(int p_state, ArrayList p_categories, int contractID, ArrayList p_deleteCounties)
        {
            DataSet dsReturn;
            string sql;
            string cats;
            cats = "";

            int i;

            for (i = 0; i < p_categories.Count; i++)
            {
                if (cats != "")
                {
                    cats = cats + ",";
                }

                cats = cats + p_categories[i].ToString();
            }

            string delCounties;
            delCounties = "";
            
            for (i = 0; i < p_deleteCounties.Count; i++)
            {
                if (delCounties != "")
                    delCounties = delCounties + ",";

                delCounties = delCounties + p_deleteCounties[i].ToString();
            }

            sql = "SELECT fk_ContractID FROM [ContractCategory] WHERE fk_CategoryID IN (" + cats + ")";
            dsReturn = Dealer_Locator.DA.DataAccess.Read(sql);

            string contractIDs;
            contractIDs = "";

            for (i = 0; i < dsReturn.Tables[0].Rows.Count; i++)
            {
                if (contractIDs != "")
                    contractIDs = contractIDs + ",";

                contractIDs = contractIDs + dsReturn.Tables[0].Rows[i]["fk_ContractID"];
            }

//            int stateID;

  //          stateID = DDA.DataAccess.Location_da.GetStateID(p_state);

            //sql = "SELECT DISTINCT(CountyName) FROM County, ContractCategory, ContractCounty " +
                    //"WHERE ContractCounty.fk_CountyID = County.CountyID " +
                    //"AND ContractCounty.fk_CountyID IN (SELECT CountyID FROM County, ContractCounty " +
                    //    "WHERE County.CountyID = ContractCounty.fk_CountyID AND County.fk_StateID = " + p_state + ")" +
                    //"AND ContractCategory.fk_CategoryID IN (" + cats + ") ";

            sql = "SELECT DISTINCT(CountyName), [ContractCounty].fk_ContractID FROM [County], [ContractCounty], [State] " +
                    "WHERE [ContractCounty].fk_CountyID = [County].CountyID " +
                    "AND [State].StateID = [County].fk_StateID ";

            if (contractIDs != "")
            {
                sql = sql + "AND [ContractCounty].fk_ContractID IN (" + contractIDs + ") ";
            }
            else
            {
                sql = sql + "AND [ContractCounty].fk_ContractID IN (-1) ";  // Account for no categories having been saved previously
            }
                    
            if (delCounties != "")
                sql = sql + "AND [County].CountyID NOT IN (" + delCounties + ") ";

            if (Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID > -1)
            {
                //sql = sql + "AND ContractCategory.fk_ContractID NOT IN (" + contractID + ") " +
                //"AND ContractCounty.fk_ContractID NOT IN (" + contractID + ")";
                sql = sql + "AND [ContractCounty].fk_ContractID NOT IN (" + contractID + ")";
            }

            sql = sql + " AND [State].StateID = " + p_state.ToString();
            sql = sql + " GROUP BY CountyName, fk_ContractID";

            dsReturn = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsReturn;
        }

        public static DataSet GetDistributorContract(int p_branchID)
        {
            string sql;
            DataSet dsReturn;

            string cats;
            cats = "";
            int i;

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (cats != "")
                    cats = cats + ",";

                cats = cats + Dealer_Locator.DataObjects.AppData.CurrentContract.Categories[i].ToString();
            }

            sql = "SELECT * FROM ContractCategory WHERE fk_ContractID IN " +
                    "(SELECT fk_ContractID FROM [ContractDistributor] WHERE fk_DistributorID = " + p_branchID + ") ";


            if (cats != "")
                    sql = sql + "AND fk_CategoryID IN (" + cats + ")";


            dsReturn = Dealer_Locator.DA.DataAccess.Read(sql);
            return dsReturn;
        }

        public static int GetContractID(string p_number)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            int p_id;
            DataSet ds = new DataSet();

            sql = "SELECT contractID FROM [Contract] WHERE contractNumber = '" + p_number + "'";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            p_id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            return p_id;

        }

        public static string GetContractModifiedDate(string p_number)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            string p_id;
            DataSet ds = new DataSet();

            sql = "SELECT ModifyDate FROM [Contract] WHERE contractNumber = '" + p_number + "'";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            p_id = ds.Tables[0].Rows[0][0].ToString();

            return p_id;

        }

        public static string GetContractNumber(int p_id)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            string contractNumber;
            DataSet ds = new DataSet();

            sql = "SELECT contractNumber FROM [Contract] WHERE contractID = " + p_id;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            contractNumber = ds.Tables[0].Rows[0][0].ToString();

            return contractNumber;

        }


        public static int GetCountyContractCount(int p_id)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            int count;
            DataSet ds = new DataSet();

            sql = "SELECT COUNT(fk_contractID) FROM [ContractCounty] WHERE fk_countyID = " + p_id;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            return count;

        }

        public static void DeleteCountyFromContract(int p_id)
        {

            string sql;

            sql = "DELETE FROM [ContractCounty] WHERE fk_countyID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM [County] WHERE CountyID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

        }

        //SELECT DistName FROM Contract, Distributor WHERE Contract.MainDistributorID LIKE Distributor.pk_DistributorID and Contract.ContractID = 6



        public static string GetContractDistributor2(int p_id)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            string distName;
            DataSet ds = new DataSet();

            sql = "SELECT DistName FROM [Contract], [Distributor] WHERE [Contract].MainDistributorID LIKE [Distributor].pk_DistributorID and Contract.ContractID = " + p_id;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            distName = ds.Tables[0].Rows[0][0].ToString();

            return distName;

        }

        public static string GetContractDistributor(int p_id)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_number);

            string sql;
            string distName;
            DataSet ds = new DataSet();

            sql = "SELECT DistName FROM [ContractDistributor], [Distributor] WHERE [Distributor].pk_DistributorID = [ContractDistributor].fk_DistributorID AND fk_contractID = " + p_id;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            distName = ds.Tables[0].Rows[0][0].ToString();

            return distName;

        }

        public static void DeleteCounties_byState_ContractID(int p_state, int p_contractID)
        {
            string sql;

            sql = "SELECT CountyID FROM County WHERE fk_StateID = " + p_state;

            DataSet ds;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            int i;
            int j;
            ArrayList removeList = new ArrayList();


            for (j = 0; j < Dealer_Locator.DataObjects.AppData.CurrentContract.Counties.Count; j++)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // if they match, remove the county from the list
                    if (ds.Tables[0].Rows[i]["CountyID"].ToString() == Dealer_Locator.DataObjects.AppData.CurrentContract.Counties[j].ToString())
                    {
                        removeList.Add(Convert.ToInt32(Dealer_Locator.DataObjects.AppData.CurrentContract.Counties[j].ToString()));
                        
                    }
                }
            }

            for (i = 0; i < removeList.Count; i++)
            {
                Dealer_Locator.DataObjects.AppData.CurrentContract.RemoveCounty(Convert.ToInt32(removeList[i].ToString()));
            }

        }


        public static void UpdateContract()
        {
            string contractNumber;
            string contractDate;
            string modifyDate;
            string mainDistributorID;
            string contractID;

            // Don't need to do this as we should already have the id
//            DDA.DataObjects.AppData.CurrentContract.ContractID = Dealer_Locator.DA.DataAccess.GetNextID("Contract", "ContractID");

            contractID = Convert.ToString(Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID);
            //contractNumber = DDA.DataObjects.AppData.CurrentContract.ContractNumber;
            //contractDate = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            modifyDate = Dealer_Locator.DataObjects.AppData.CurrentContract.ModifyDate;
            //mainDistributorID = Convert.ToString(DDA.DataObjects.AppData.DistributorID);

            string countyList, catList, delCountyList, delBranchList;
            countyList = "";
            catList = "";
            delBranchList = "";
            delCountyList = "";

            string distID;
            string catID;
            string countyID;

            int i;
            string sql;

            
            // #######################
            DeleteOldData();
            

            // do a delete first, then an insert
            sql = "DELETE FROM [ContractCategory] WHERE fk_ContractID = " + contractID + "";
            Dealer_Locator.DA.DataAccess.Update(sql);

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {

                // Do the insert
                sql = "INSERT INTO [ContractCategory] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Categories[i].ToString() + "','" + contractID + "')";
                Dealer_Locator.DA.DataAccess.Update(sql);
            }


            // do a delete first, then an insert
            sql = "DELETE FROM [ContractCounty] WHERE fk_ContractID = " + contractID + "";
            Dealer_Locator.DA.DataAccess.Update(sql);

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {
                // Do the insert
                sql = "INSERT INTO [ContractCounty] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Counties[i].ToString() + "','" + contractID + "')";
                Dealer_Locator.DA.DataAccess.Update(sql);

            }

            // do a delete first, then an insert
            sql = "DELETE FROM [ContractDistributor] WHERE fk_ContractID = " + contractID + "";
            Dealer_Locator.DA.DataAccess.Update(sql);

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Branches.Count; i++)
            {
                // Insert new values
                sql = "INSERT INTO [ContractDistributor] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Branches[i].ToString() + "','" + contractID + "')";
                Dealer_Locator.DA.DataAccess.Update(sql);
            }

            
            contractDate = Dealer_Locator.DataObjects.AppData.CurrentContract.ContractDate;

            sql = "Update [Contract] SET ModifyDate = '" + modifyDate + "', ContractDate = '" + contractDate + "' " + 
                    "WHERE ContractID = " + contractID;

            Dealer_Locator.DA.DataAccess.Update(sql);

            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearBranches();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearCounties();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteBranches();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteCounties();

        }

        private static void DeleteOldData()
        {
            string contractNumber;
            string contractDate;
            string modifyDate;
            string mainDistributorID;
            string contractID;

            // Don't need to do this as we should already have the id
            //            DDA.DataObjects.AppData.CurrentContract.ContractID = Dealer_Locator.DA.DataAccess.GetNextID("Contract", "ContractID");

            contractID = Convert.ToString(Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID);
            //contractNumber = DDA.DataObjects.AppData.CurrentContract.ContractNumber;
            //contractDate = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            modifyDate = Dealer_Locator.DataObjects.AppData.CurrentContract.ModifyDate;
            //mainDistributorID = Convert.ToString(DDA.DataObjects.AppData.DistributorID);

            string countyList, catList, delCountyList, delBranchList;
            countyList = "";
            catList = "";
            delBranchList = "";
            delCountyList = "";

            string distID;
            string catID;
            string countyID;

            int i;
            string sql;

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {
                if (countyList != "")
                    countyList = countyList + ",";

                countyList = countyList + Dealer_Locator.DataObjects.AppData.CurrentContract.Counties[i].ToString();
            }

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (catList != "")
                    catList = catList + ",";

                catList = catList + Dealer_Locator.DataObjects.AppData.CurrentContract.Categories[i].ToString();
            }

            DataSet dsContract_County;

            sql = "SELECT fk_ContractID FROM [ContractCategory] WHERE fk_CategoryID IN (" + catList + ")";
            dsContract_County = Dealer_Locator.DA.DataAccess.Read(sql);
            string contractIDList;
            contractIDList = "";

            for (i = 0; i < dsContract_County.Tables[0].Rows.Count; i++)
            {
                if (contractIDList != "")
                    contractIDList = contractIDList + ",";

                contractIDList = contractIDList + dsContract_County.Tables[0].Rows[i]["fk_ContractID"];
            }


            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.DeleteCounties.Count; i++)
            {
                if (delCountyList != "")
                    delCountyList = delCountyList + ",";

                delCountyList = delCountyList + Dealer_Locator.DataObjects.AppData.CurrentContract.DeleteCounties[i].ToString();
            }

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.DeleteBranches.Count; i++)
            {
                if (delBranchList != "")
                    delBranchList = delBranchList + ",";

                delBranchList = delBranchList + Dealer_Locator.DataObjects.AppData.CurrentContract.DeleteBranches[i].ToString();
            }

            if (delBranchList != "")
            {
                sql = "DELETE FROM [ContractDistributor] WHERE fk_DistributorID IN (" + delBranchList + ") " +
                        "AND fk_ContractID IN (SELECT fk_ContractID FROM [ContractCategory] WHERE fk_CategoryID IN (" + catList + "))";
                Dealer_Locator.DA.DataAccess.Update(sql);
            }

            if (delCountyList != "")
            {
                sql = "DELETE FROM [ContractCounty] WHERE fk_CountyID IN (" + delCountyList + ") AND fk_ContractID IN (" + contractIDList + ")";
                //sql = "DELETE FROM ContractCounty WHERE fk_CountyID IN (" + delCountyList + ") " +
                //      "AND fk_ContractID IN (SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";
                Dealer_Locator.DA.DataAccess.Update(sql);
            }

            // #######################

        }

        public static void AddContract()
        {

            string contractNumber;
            string contractDate;
            string modifyDate;
            string mainDistributorID;
            string contractID;

            Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID = Dealer_Locator.DA.DataAccess.GetNextID("Contract", "ContractID");

            contractID = Convert.ToString(Dealer_Locator.DataObjects.AppData.CurrentContract.ContractID);
            contractNumber = Dealer_Locator.DataObjects.AppData.CurrentContract.ContractNumber;
            contractDate = Dealer_Locator.DataObjects.AppData.CurrentContract.ContractDate;
            modifyDate = Dealer_Locator.DataObjects.AppData.CurrentContract.ModifyDate;
            mainDistributorID = Convert.ToString(Dealer_Locator.DataObjects.AppData.DistributorID);


            string distID;
            string catID;
            string countyID;

            int i;
            string sql;

            DeleteOldData();

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {

                sql = "INSERT INTO [ContractCategory] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Categories[i].ToString() + "','" + contractID + "')";
                
                Dealer_Locator.DA.DataAccess.Update(sql);
            }


            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {

                sql = "INSERT INTO [ContractCounty] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Counties[i].ToString() + "','" + contractID + "')";
                
                Dealer_Locator.DA.DataAccess.Update(sql);
            
            }

            for (i = 0; i < Dealer_Locator.DataObjects.AppData.CurrentContract.Branches.Count; i++)
            {
                sql = "INSERT INTO [ContractDistributor] VALUES ('" + Dealer_Locator.DataObjects.AppData.CurrentContract.Branches[i].ToString() + "','" + contractID + "')";
                
                Dealer_Locator.DA.DataAccess.Update(sql);
            }
            
            


            //            string contractNumber;
            //string contractDate;
            //string modifyDate;
            //string mainDistributorID;
            //string contractID;

            sql = "INSERT INTO [Contract] VALUES('" + contractID + "','" + contractNumber + "','" + contractDate + "','" + modifyDate + "','" + mainDistributorID + "')";

            Dealer_Locator.DA.DataAccess.Update(sql);

            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearBranches();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearCounties();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteBranches();
            Dealer_Locator.DataObjects.AppData.CurrentContract.ClearDeleteCounties();

        }

    }
}
