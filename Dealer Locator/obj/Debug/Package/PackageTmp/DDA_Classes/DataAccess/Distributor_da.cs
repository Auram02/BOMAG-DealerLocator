using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dealer_Locator.DA;
using System.Collections;

namespace Dealer_Locator.DDA.DataAccess
{
    class Distributor_da
    {

        public static DataSet GetDistributorList()
        {
            return GetDistributorList("none", "");
        }

        public static DataSet GetDistributorListContacts()
        {
            String sql;


            sql = "SELECT Dist.pk_DistributorID, Contacts " +
                    "FROM Distributor Dist ";

            DataSet dsList;
            dsList = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsList;
        }


        public static DataSet GetDistributorList(int ContractCategoryID, ArrayList ZipCodes)
        {
            DataSet ds = new DataSet();
            String sql;

            string zipList = "";

            foreach (string item in ZipCodes)
            {
                if (zipList != "")
                    zipList = zipList + ",";

                zipList = zipList + "'" + item + "'";

            }

            sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName, fk_ZipID, pk_DistributorID " +
                    " FROM Distributor Dist " +
                    " WHERE pk_DistributorID IN (" +
                    " SELECT [ContractDistributor].fk_DistributorID FROM ContractDistributor, Contract " +
                    " ContractCategory, Category, " +
                    " )";


            


            return ds;
        }



        public static DataSet GetMainDistributorNames()
        {
            String sql;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";
            sql = "SELECT DistName as [DISTRIBUTOR NAME], pk_DistributorID " +
                    "FROM Distributor Dist " +
                    "WHERE MainDistributor = 1" +
                    " ORDER BY DistName";

            DataSet dsList;
            dsList = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsList;
        }


        public static DataSet GetDistributorList(string listMode, string searchKey)
        {
            String sql;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";
            sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Dist.pk_DistributorID " +
        "FROM Distributor Dist, State " +
        "WHERE Dist.fk_StateID = StateID ";

            if (listMode == "Main")
            {
                sql = sql + " AND MainDistributor = 1";
            }

            if (searchKey != "")
            {
                sql = sql + " AND DistName LIKE '%" + searchKey + "%'";
            }

            sql = sql + " ORDER BY DistName";

            DataSet dsList;
            dsList = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsList;
        }

        public static DataSet GetDistributorEmailList(int p_mainDist)
        {
            DataSet ds;
            string sql;

            sql = "SELECT DistName AS [DISTRIBUTOR], ShippingAddress AS [ADDRESS], CityName AS [CITY], Abbreviation AS [STATE], fk_ZipID AS [ZIP], PersonName AS [CONTACT NAME], Email AS [EMAIL ADDRESS] " + 
                    " FROM Distributor, DistributorEmail, State" +
                    " WHERE Distributor.pk_DistributorID = DistributorEmail.fk_DistributorID" +
                    " AND Distributor.fk_StateID = State.StateID " +
                    " AND MainDistributor = " + p_mainDist +
                    " ORDER BY DistName, Abbreviation, CityName";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static DataSet GetDistributorInformation(int p_distrituborID)
        {
            String sql;

            //sql = "SELECT * FROM Distributor WHERE pk_DistributorID = " + p_distrituborID;

            //sql = "SELECT SAP, Node, Distributor.Email, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
            //        "Distributor.Fax, Distributor.CityName, State.FullName, Country.CountryName, " +
            //        "Representative.RepName, Distributor.fk_ZipID " +
            //        "FROM Distributor, City, State, Country, Representative " +
            //        "WHERE State.StateID = Distributor.fk_StateID " +
            //        "AND Country.CountryID = Distributor.fk_CountryID " +
            //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
            //        "AND pk_DistributorID = " + p_distrituborID;
            sql = "SELECT SAP, Node, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
                    "Distributor.Fax, Distributor.CityName, State.FullName, Country.CountryName, " +
                    "Contacts, Distributor.fk_ZipID, Distributor.PartsOnly " +
                    "FROM Distributor, City, State, Country " +
                    "WHERE State.StateID = Distributor.fk_StateID " +
                    "AND Country.CountryID = Distributor.fk_CountryID " +
                    "AND pk_DistributorID = " + p_distrituborID;

            DataSet dsDI;
            dsDI = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsDI;
        }

        public static DataSet GetDistributorInformation_DL(int p_distrituborID)
        {
            String sql;

            //sql = "SELECT * FROM Distributor WHERE pk_DistributorID = " + p_distrituborID;

            //sql = "SELECT SAP, Node, Distributor.Email, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
            //        "Distributor.Fax, Distributor.CityName, State.FullName, Country.CountryName, " +
            //        "Representative.RepName, Distributor.fk_ZipID " +
            //        "FROM Distributor, City, State, Country, Representative " +
            //        "WHERE State.StateID = Distributor.fk_StateID " +
            //        "AND Country.CountryID = Distributor.fk_CountryID " +
            //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
            //        "AND pk_DistributorID = " + p_distrituborID;
            sql = "SELECT pk_DistributorID, SAP, Node, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
                    "Distributor.Fax, Distributor.CityName, State.FullName, fk_StateID, " +
                    "Contacts, Distributor.fk_ZipID, Distributor.PartsOnly " +
                    "FROM Distributor,  State " +
                    "WHERE State.StateID = Distributor.fk_StateID " +
                    "AND pk_DistributorID = " + p_distrituborID;

            DataSet dsDI;
            dsDI = Dealer_Locator.DA.DataAccess.Read(sql);

            return dsDI;
        }

        public static void DeleteDistributor(int p_id)
        {

            string sql;

            //sql = "DELETE FROM Distributor WHERE Distributor.pk_DistributorID = distributorbranch.fk_BranchDistID AND DistributorBranch.fk_MainDistID = " + p_id;
            //Dealer_Locator.DA.DataAccess.Update(sql);

            //sql = "DELETE FROM DistributorBranch WHERE fk_MainDistID = " + p_id;
            //Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "SELECT fk_ContractID FROM ContractDistributor WHERE fk_DistributorID = " + p_id;
            DataSet ds;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            int i;
            int contractID;

            //--for each contract id, delete all stuff associated.
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                contractID = Convert.ToInt32(ds.Tables[0].Rows[i]["fk_ContractID"].ToString());
                Dealer_Locator.DDA.DataAccess.Contract_da.DeleteContract(contractID);
            }

            sql = "DELETE FROM DistributorRepresentative WHERE fk_DistributorID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM DistributorEmail WHERE fk_DistributorID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);


            
            sql = "DELETE FROM Distributor WHERE Distributor.pk_DistributorID = 1 IN " + 
                    "(select DISTINCT(DistributorBranch.fk_BranchDistID) " + 
                    "FROM Distributor, DistributorBranch WHERE DistributorBranch.fk_MainDistID = " + p_id + ")";
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM DistributorBranch WHERE fk_MainDistID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "DELETE FROM Distributor WHERE pk_DistributorID = " + p_id;
            Dealer_Locator.DA.DataAccess.Update(sql);



        }

        public static string AddBranch(string p_ParentBranch, string p_name, string p_BillingAddress, string p_ShippingAddress,
                                                    string p_city, string p_country, string p_fax,
                                                    string p_phone, string p_state, string p_zip, string p_repName, string p_BillingCity, string p_BStateID,
                                                    string p_BZipID, string p_BCountryID, string p_SAP, string p_Node, int IsMainDist, int IsPartsOnly)
        {
            string returnVal = "";
            string sql;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_name);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BillingAddress);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_ShippingAddress);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_city);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_country);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_fax);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_phone);
            ////Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_state);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_zip);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_repName);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_ParentBranch);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BillingCity);
            ////Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BStateID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BZipID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BCountryID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_Node);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_SAP);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_repName);

            int cityID, stateID, countryID, billingStateID, billingCountryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);
            billingCountryID = Location_da.GetCountryID(p_BCountryID);
            billingStateID = Location_da.GetStateID(p_BStateID);

            int nextBranchID;
            nextBranchID = Dealer_Locator.DA.DataAccess.GetNextID("Distributor", "pk_DistributorID");

            //int repID;
            //repID = Representative_da.GetRepID(p_repName);


            int p_ParentBranchID;

            string p_Replace;
            p_Replace = p_ParentBranch;

            p_Replace = p_Replace.Replace("'", "");

            if (p_Replace == "")
            {
                p_ParentBranchID = nextBranchID;
            }
            else
            {
                p_ParentBranchID = GetDistributorID(p_ParentBranch);
            }

            sql = "INSERT INTO Distributor(pk_DistributorID, DistName, BillingAddress, ShippingAddress, CityName, fk_StateID, fk_ZipID, " +
                                    "fk_CountryID, Phone, Fax, Contacts, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, " +
                                    "SAP, Node, MainDistributor, PartsOnly) " +
                                    "VALUES (" + nextBranchID + ",'" + p_name + "','" + p_BillingAddress + "','" + p_ShippingAddress +
                                    "','" + p_city + "','" + stateID +
                                    "','" + p_zip + "','" + countryID + "','" + p_phone + "','" + p_fax +
                                    "','" + p_repName + "','" + p_BillingCity + "','" + billingStateID + "','" + p_BZipID + "','" + billingCountryID + "','" + p_SAP + "','" + p_Node + "','" + IsMainDist + "','" + IsPartsOnly + "')";

            Dealer_Locator.DA.DataAccess.Update(sql);

            returnVal = Convert.ToString(nextBranchID);
            return returnVal;
        }


        public static string UpdateBranch(int p_DistID, string p_ParentBranch, string p_name, string p_BillingAddress, string p_ShippingAddress,
                                                    string p_city, string p_country, string p_fax,
                                                    string p_phone, string p_state, string p_zip, string p_repName, string p_BillingCity, string p_BStateID,
                                                    string p_BZipID, string p_BCountryID, string p_SAP, string p_Node, int IsPartsOnly)
        {
            string returnVal = "";
            string sql;

            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_name);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BillingAddress);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_ShippingAddress);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_city);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_country);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_fax);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_phone);
            ////Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_state);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_zip);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_repName);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_ParentBranch);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BillingCity);
            ////Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BStateID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BZipID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_BCountryID);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_Node);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_SAP);
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_repName);

            int cityID, stateID, countryID, billingStateID, billingCountryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);
            billingCountryID = Location_da.GetCountryID(p_BCountryID);
            billingStateID = Location_da.GetStateID(p_BStateID);

            int repID;
            //repID = Representative_da.GetRepID(p_repName);

            int p_ParentBranchID;

            string parentTemp;
            parentTemp = p_ParentBranch;
            parentTemp = parentTemp.Replace("'", "");

            if (parentTemp == "")
            {
                // do nothing, no changes
            }
            else
            {
                p_ParentBranchID = GetDistributorID(p_ParentBranch);
                // update the parent branch listing here
            }

            sql = "UPDATE Distributor SET " +
                    " DistName = '" + p_name + "', BillingAddress = '" + p_BillingAddress + "', ShippingAddress = '" + p_ShippingAddress  + "', CityName = '" + p_city +
                    "' , fk_StateID = '" + stateID + "', fk_ZipID = '" + p_zip + "', " +
                    " fk_CountryID = '" + countryID + "', Phone = '" + p_phone + "', Fax = '" + p_fax +
                    "' , Contacts = '" + p_repName + "', BillingCityName = '" + p_BillingCity + "', fk_BillingStateID = '" + billingStateID + "', fk_BillingZipID = '" + p_BZipID + "', fk_BillingCountryID = '" + billingCountryID +
                    "' , SAP = '" + p_SAP + "', Node = '" + p_Node + "', PartsOnly = " + IsPartsOnly + " " +
                    " WHERE pk_DistributorID = " + p_DistID;

            Dealer_Locator.DA.DataAccess.Update(sql);

            sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID LIKE '" + p_DistID + "'";
            DataSet ds = Dealer_Locator.DA.DataAccess.Read(sql);

            string idlist = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (idlist != "")
                    idlist = idlist + ",";

                idlist = idlist + dr[0].ToString();

            }

            
            sql = "UPDATE DISTRIBUTOR SET DistName = '" + p_name + "' WHERE pk_DistributorID IN " +
                "(" + idlist + ")";
            Dealer_Locator.DA.DataAccess.Update(sql);


            return returnVal;
        }



        public static void AddBranch(int p_MainID, int p_BranchID)
        {
            string returnVal = "";
            string sql;

            int nextID;

            nextID = Dealer_Locator.DA.DataAccess.GetNextID("DistributorBranch", "DistributorBranchID");

            sql = "INSERT INTO DistributorBranch VALUES(" + nextID + "," + p_MainID + "," + p_BranchID + ")";


            Dealer_Locator.DA.DataAccess.Update(sql);
            
        }

        public static void ClearEmailAddresses(int p_BranchID)
        {
            string sql;
            
            sql = "DELETE FROM DistributorEmail WHERE fk_DistributorID = " + p_BranchID;
            Dealer_Locator.DA.DataAccess.Update(sql);

        }



        public static void AddEmailAddress(int p_BranchID, string p_Email, string p_PersonName)
        {
            string sql;

            sql = "INSERT INTO DistributorEmail VALUES(" + p_BranchID + ",'" + p_Email + "','" + p_PersonName + "')";

            Dealer_Locator.DA.DataAccess.Update(sql);

        }

        public static DataSet GetEmailAddresses(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT Email, PersonName FROM DistributorEmail WHERE fk_DistributorID = " + p_BranchID;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static int ContactPrimary(int p_distID, string p_ContactName)
        {
            string sql;
            DataSet ds;

            sql = "SELECT Primary FROM Contacts WHERE fk_DistributorID = " + p_distID + " AND ContactName = '" + p_ContactName + "'";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            int result;

            try
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                result = -1;
            }
            
            return result;
        }

        public static void ClearContacts(int p_distID)
        {
            string sql;
            sql = "DELETE FROM CONTACTS WHERE fk_DistributorID = " + p_distID;

            Dealer_Locator.DA.DataAccess.Update(sql);
        }

        public static void AddContact(int p_distID, string p_name, string p_title, int p_primary)
        {

            int nextContactID;
            nextContactID = Dealer_Locator.DA.DataAccess.GetNextID("Contacts", "pk_ContactID");

            string sql;
            sql = "INSERT INTO CONTACTS VALUES (" + nextContactID + "," + p_distID + ", '" + p_name + "', '" + p_title + "'," + p_primary + ")";

            Dealer_Locator.DA.DataAccess.Update(sql);
        }

        public static DataSet GetContacts(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT ContactTitle, ContactName, Primary FROM Contacts WHERE fk_DistributorID = " + p_BranchID;
            sql = sql + " ORDER BY Primary DESC, ContactName";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }

        public static DataSet GetPrimaryContact(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT ContactTitle, ContactName FROM Contacts WHERE fk_DistributorID = " + p_BranchID;
            sql = sql + " AND Primary = 1";

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }


        public static int GetDistributorID(string p_DistName)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_DistName);

            string sql;
            sql = "SELECT pk_DistributorID FROM Distributor WHERE DistName = '" + p_DistName + "'";

            int id;
            id = Convert.ToInt32(Dealer_Locator.DA.DataAccess.Read(sql).Tables[0].Rows[0]["pk_DistributorID"]);

            return id;
        }

        public static int GetMainDistributorID(string p_DistName)
        {
            //Dealer_Locator.DA.DataAccess.PrepareSQL(ref p_DistName);

            string sql;
            sql = "SELECT pk_DistributorID FROM Distributor WHERE DistName = '" + p_DistName + "' AND MainDistributor = 1";

            int id;
            id = Convert.ToInt32(Dealer_Locator.DA.DataAccess.Read(sql).Tables[0].Rows[0]["pk_DistributorID"]);

            return id;
        }


        public static string GetDistributorName(int p_id)
        {

            string sql;
            sql = "SELECT DistName FROM Distributor WHERE pk_DistributorID = " + p_id;

            string distName;
            distName = Convert.ToString(Dealer_Locator.DA.DataAccess.Read(sql).Tables[0].Rows[0]["DistName"]);

            return distName;
        }

        public static DataSet GetDistributorBranchList(int p_MainDistID, string searchKey)
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;

                sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID = " +
                        Convert.ToString(p_MainDistID);
                                
                ds = Dealer_Locator.DA.DataAccess.Read(sql);

                int i;

                string distList = "";

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (distList != "")
                    {
                        distList = distList + ",";  // add a new item
                    }

                    distList = distList + ds.Tables[0].Rows[i][0];

                }

                if (distList != "")
                {
                    //sql = "SELECT DistName as [BRANCH NAME], City.CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID, SPACE(30) AS srvcrep, SPACE(30) AS terrrep " +
                    //        "FROM Distributor Dist, City, State, Representative Rep " +
                    //        "WHERE City.CityID = Dist.fk_CityID " +
                    //        "AND State.StateID = Dist.fk_StateID " +
                    //        "AND Rep.RepID = Dist.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";

                    //sql = "SELECT DistName, City.CityName, Distributor.Phone, " +
                    //        "Distributor.Fax, State.FullName, Country.CountryName, " +
                    //        "Representative.RepName, Distributor.fk_ZipID " +
                    //        "FROM Distributor, City, State, Country, Representative " +
                    //        "WHERE City.CityID = Distributor.fk_CityID " +
                    //        "AND State.StateID = Distributor.fk_StateID " +
                    //        "AND Country.CountryID = Distributor.fk_CountryID " +
                    //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";

                    sql = "SELECT DistName as [BRANCH NAME], BillingAddress AS [ADDRESS], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Dist.pk_DistributorID, SPACE(30) AS srvcrep, SPACE(30) AS terrrep " +
                            "FROM Distributor Dist, State " +
                            "WHERE State.StateID = Dist.fk_StateID " +
                            "AND pk_DistributorID IN ( " + distList + ") " + 
                            " ORDER BY Abbreviation, CityName";

                    if (searchKey != "")
                        sql = sql + " AND DistName LIKE '%" + searchKey + "%'";

                    ds = Dealer_Locator.DA.DataAccess.Read(sql);
                }
            }
            catch (Exception ex)
            {
                
            }

            return ds;
        }

        public static DataSet GetContractCategoryList(int p_DistID)
        {
            string sql;
            sql = "SELECT DISTINCT Category.CategoryName FROM Contract, ContractDistributor, Category, ContractCategory" +
                    " WHERE ContractCategory.fk_CategoryID = Category.CategoryID " +
                    " AND ContractCategory.fk_contractID = Contract.ContractID " + 
                    " AND ContractDistributor.fk_contractID = Contract.ContractID " + 
                    " AND fk_DistributorID = " + p_DistID;

            DataSet ds;
            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;
        }


    }
}
