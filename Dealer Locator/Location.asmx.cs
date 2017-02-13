

using System;
using System.Web.Services;
using System.Web.Script.Services;

namespace LocationExtender
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>

    [ScriptService]
    public class Location : WebService
    {

        [WebMethod]
        public string GetMessage(string zipCode, string CityName)
        {

            Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();
            Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();

            zldt = zlta.GetDataByZipCode(Convert.ToInt32(zipCode));

            string returnval;
            returnval = "<SELECT NAME=\"cboCityName\" onChange=\"javascript:updateHiddenCity(this);\" class=\"InputControl\">";
            string selectedCity = "";


            foreach (Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupRow tempRow in zldt.Rows)
            {
                returnval += "<OPTION VALUE=\"" + tempRow.CITY_ALIAS_NAME.ToString() + "\"";

                if (CityName.ToUpper() == tempRow.CITY_ALIAS_NAME.ToString().ToUpper())
                {
                    returnval += " SELECTED";
                    selectedCity = CityName.ToUpper();
                }

                returnval += ">" + tempRow.CITY_ALIAS_NAME.ToString();
            }

            if (zldt.Rows.Count > 0 && selectedCity == "")
                selectedCity = zldt[0].CITY_ALIAS_NAME;

            returnval += "</SELECT>";

            if (zldt.Rows.Count < 0)
                returnval = "None";

            //returnval = "YOOOOOOOOOOOOO";

            return returnval;

        }

        [WebMethod]
        public string GetMessage_SelectedCity(string zipCode, string CityName)
        {

            Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();
            Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();

            zldt = zlta.GetDataByZipCode(Convert.ToInt32(zipCode));

            string selectedCity = "";


            foreach (Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupRow tempRow in zldt.Rows)
            {

                if (CityName.ToUpper() == tempRow.CITY_ALIAS_NAME.ToString().ToUpper())
                {
                    selectedCity = CityName.ToUpper();
                }

            }

            if (zldt.Rows.Count > 0 && selectedCity == "")
                selectedCity = zldt[0].CITY_ALIAS_NAME;

            //returnval = "YOOOOOOOOOOOOO";

            return selectedCity;

        }

        [WebMethod]
        public string GetState(string zipCode, string CityName)
        {

            Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable zldt = new Dealer_Locator.DA.ZipLookupTDS.DL_ZipLookupDataTable();
            Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();

            string returnval = "";

            try
            {
                zldt = zlta.GetDataByZipCode_CityName(Convert.ToInt32(zipCode), CityName);


                if (zldt.Rows.Count < 0)
                    returnval = "";
                else
                    returnval = zldt[0].STATE;
            }
            catch
            {
                returnval = "";
            }
                //returnval = "<asp:TextBox ID=\"txtState\" Width=\"25px\">" + zldt[0].STATE + "</asp:TextBox>";
                
                //returnval = "<input type=\"text\" id=\"txtState\" runat=\"server\" value=\"" + zldt[0].STATE + "\" />";

                //returnval = "<input type=\"text\" id=\"txtState\" readonly value=\"" +  zldt[0].STATE + "\" style=\"width: 25px;\">" ;

            //returnval = "YOOOOOOOOOOOOO";

            return returnval;

        }
    }
}
