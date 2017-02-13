using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Dealer_Locator.DataObjects
{
    class Reports
    {

        ////public static DataTable DistributorsByState(int p_MainDistributor, int p_catID)
        ////{
        ////    string sql;

        ////    //sql = "Select State.Abbreviation as [STATE], Distributor.DistName as [DISTRIBUTOR], Distributor.ShippingAddress as [ADDRESS], Distributor.fk_ZipID as [ZIP], County.CountyName as [COUNTY], Category.CategoryName as [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax as [FAX], Rep.RepName as [TERRITORY MANAGER], ContractCounty.fk_ContractID" + 
        ////    //        " FROM ContractCounty, ContractCategory, Category,  Distributor, ContractDistributor, County, State, DistributorRepresentative DR, Representative Rep" + 
        ////    //        " WHERE ContractCounty.fk_contractID = ContractCategory.fk_contractID" + 
        ////    //        " AND County.CountyID = ContractCounty.fk_countyID" + 
        ////    //        " AND ContractCategory.fk_CategoryID = Category.CategoryID" + 
        ////    //        " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" + 
        ////    //        " AND ContractDistributor.fk_ContractID = ContractCounty.fk_contractID" + 
        ////    //        " AND State.StateID = County.fk_StateID" + 
        ////    //        " AND Distributor.pk_DistributorID = DR.fk_DistributorID" + 
        ////    //        " AND DR.fk_TerritoryRepID = Rep.RepID" +
        ////    //        " AND Distributor.MainDistributor = " + p_MainDistributor + 
        ////    //        " ORDER BY Abbreviation, CategoryName, DistName, CountyName";

        ////    string selectClause, whereClause, fromClause, orderbyClause;

        ////    selectClause = "Select State.Abbreviation as [STATE], Distributor.DistName as [DISTRIBUTOR], Distributor.CityName AS [CITY],Distributor.BillingAddress as [ADDRESS], Distributor.fk_ZipID as [ZIP],Category.CategoryName as [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax as [FAX] ";

        ////    if (p_MainDistributor == 0)
        ////        selectClause = selectClause + ", Rep.RepName as [TERRITORY MANAGER], DR.fk_ServiceRepID as [PRODUCT SUPPORT]";

        ////    fromClause = " FROM  ContractCategory, Category,  Distributor, ContractDistributor, State, DistributorRepresentative DR,  Representative Rep ";

        ////    whereClause = " WHERE ContractCategory.fk_CategoryID = Category.CategoryID" +
        ////            " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" +
        ////            " AND ContractDistributor.fk_ContractID = ContractCategory.fk_contractID" +
        ////            " AND Distributor.pk_DistributorID = DR.fk_DistributorID" +
        ////            " AND DR.fk_TerritoryRepID = Rep.RepID" +
        ////            " AND Category.CategoryID = " + p_catID;

        ////    if (p_MainDistributor == 1)
        ////    {
        ////        whereClause = whereClause + " AND Distributor.MainDistributor = " + p_MainDistributor;
        ////    }
        ////    else
        ////    {
        ////        fromClause = fromClause + ", DistributorBranch";
        ////        whereClause = whereClause + " AND Distributor.pk_DistributorID LIKE DistributorBranch.fk_BranchDistId";
        ////    }

        ////    orderbyClause = " ORDER BY Abbreviation, DistName, CategoryName, CityName";


        ////    DataSet ds;

        ////    sql = selectClause + fromClause + whereClause + orderbyClause;

        ////    ds = Dealer_Locator.DA.DataAccess.Read(sql);

        ////    return ds.Tables[0];
        ////}




        public static DataTable DistributorsByState(int p_MainDistributor, int p_catID, int p_distID)
        {
            string sql;

            string selectClause, whereClause, fromClause, orderbyClause;

            selectClause = "SELECT DISTINCT (fk_BranchDistID), Abbreviation AS [STATE], CityName AS [CITY], ShippingAddress AS [ADDRESS], Distributor.fk_ZipID AS [ZIP], Category.CategoryName AS [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax AS [FAX] ";

            if (p_MainDistributor == 0)
                selectClause = selectClause + ", Rep.RepName as [TERRITORY MANAGER], DR.fk_ServiceRepID";

            fromClause = " FROM Distributor, DistributorBranch, Contract, ContractCategory, Category, State, ContractDistributor, DistributorRepresentative DR, Representative Rep ";


            whereClause = " WHERE Distributor.pk_DistributorID LIKE DistributorBranch.fk_BranchDistId " +
                            " AND Contract.ContractID = ContractCategory.fk_ContractID" +
                            " AND ContractCategory.fk_CategoryID = Category.CategoryID" + 
                            " AND State.StateID = Distributor.fk_StateID" + 
                            " AND ContractDistributor.fk_ContractID = ContractCategory.fk_ContractID" + 
                            " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" + 
                            " AND Distributor.pk_DistributorID = DR.fk_DistributorID" + 
                            " AND DR.fk_TerritoryRepID = Rep.RepID" +
                            " AND Category.CategoryID = " + p_catID +
                            " AND Contract.MainDistributorID LIKE " + p_distID;


            if (p_MainDistributor == 1)
            {
                whereClause = whereClause + " AND Distributor.MainDistributor = " + p_MainDistributor;
            }

            orderbyClause = " ORDER BY Abbreviation, CategoryName, CityName";


            DataSet ds;

            sql = selectClause + fromClause + whereClause + orderbyClause;

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                ds.Tables[0].Rows[i][0] = "";

            //ds.Tables[0].Columns.RemoveAt(0);

            return ds.Tables[0];
        }

    }
}
