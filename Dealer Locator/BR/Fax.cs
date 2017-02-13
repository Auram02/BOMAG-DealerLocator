using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.BR
{
    public class Fax
    {


        public DataSet GetContractsToFax(string Categories)
        {
            string sql = "";

            sql = "SELECT     DISTINCT(Distributor.pk_DistributorID), Distributor.DistName, Distributor.BillingAddress, Distributor.ShippingAddress, Distributor.CityName, Distributor.fk_StateID, " +
   "Distributor.fk_ZipID, Distributor.fk_CountryID, Distributor.Phone, Distributor.Fax,  Distributor.BillingCityName, " +
  "Distributor.fk_BillingStateID, Distributor.fk_BillingZipID, Distributor.fk_BillingCountryID, Distributor.SAP, Distributor.Node, Distributor.MainDistributor, " +
 "Distributor.PartsOnly, State.Abbreviation " +
"FROM         Contract INNER JOIN " +
     "ContractCategory ON Contract.ContractID = ContractCategory.fk_contractID INNER JOIN " +
    "ContractCounty ON Contract.ContractID = ContractCounty.fk_contractID INNER JOIN " +
   "ContractDistributor ON Contract.ContractID = ContractDistributor.fk_ContractID INNER JOIN " +
  "Distributor ON Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID INNER JOIN " +
 "State ON State.StateID = Distributor.fk_StateID " +
"WHERE     (ContractCategory.fk_categoryID IN (" + Categories + "))";

            DataSet ds = new DataSet();

            ds = Dealer_Locator.DA.DataAccess.Read(sql);

            return ds;

        }


    }
}
