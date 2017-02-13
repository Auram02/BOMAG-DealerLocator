using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Collections.Generic;

namespace Dealer_Locator.BR
{
    public class LeadExcelReport
    {

        public string CreateLeadExcelReport(List<int> leadIDs, string pathName)
        {
            string dateRange = "";

            string mainSubCategoryID = "";

            ModelList modelList = new ModelList();
            Hashtable idHash = new Hashtable();


            Hashtable leadValueHash = new Hashtable();


            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();


            foreach (int pk_leadID in leadIDs)
            {
                if (idHash.ContainsKey(pk_leadID) == false)
                {
                    idHash.Add(pk_leadID, pk_leadID);

                }
            }


            DA.LeadsTDS.DL_LeadProductDataTable lpdt = new Dealer_Locator.DA.LeadsTDS.DL_LeadProductDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter lpta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter();



            // should have all lead id's collected now
            // get the leadvalues submitted for each of these leads.
            List<BR.Lead> myArrList = new List<Dealer_Locator.BR.Lead>();



            int leadValueCount = 0;
            ArrayList headerArray = new ArrayList();

            foreach (int key in idHash.Keys)
            {
                BR.Lead tempLead = new Dealer_Locator.BR.Lead(key);
                myArrList.Add(tempLead);

                foreach (BR.Lead.LeadValue leadVal in tempLead._leadValues)
                {
                    if (leadValueHash.ContainsKey(leadVal.elementName.ToUpper()) == false)
                    {
                        // "index", columnName;
                        leadValueHash.Add(leadVal.elementName.ToUpper(), leadValueCount);

                        headerArray.Add(leadVal.elementName.ToUpper());
                        leadValueCount += 1;
                    }
                }
            }


            int counter = 0;

            ArrayList arrayCollection = new ArrayList();

            DA.ModelTDSTableAdapters.DL_ModelTableAdapter mta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();

            foreach (BR.Lead tempLead in myArrList)
            {
                ArrayList valueCollection = new ArrayList();

                if (counter == 0)
                {
                    // add headers
                    foreach (string str in headerArray)
                    {
                        valueCollection.Add(str.ToUpper());
                    }

                    // For products
                    valueCollection.Add("PRODUCTS SELECTED");

                    arrayCollection.Add(valueCollection);
                }

                // clear it incase it was used
                valueCollection = new ArrayList();

                int colCounter = 0;
                int getIndex = 1;

                foreach (string str in headerArray)
                {
                    // blank values
                    valueCollection.Add("");
                }

                foreach (BR.Lead.LeadValue leadVal in tempLead._leadValues)
                {

                    getIndex = Convert.ToInt32(leadValueHash[leadVal.elementName.ToUpper()].ToString());

                    valueCollection[getIndex] = leadVal.elementValue.ToString();

                    //valueCollection.Add(leadVal.elementValue);

                    //colCounter += 1;
                }


                string productList = "";
                foreach (BR.Lead.LeadProduct _prod in tempLead._leadProducts)
                {

                    if (productList != "")
                        productList += ", ";

                    //productList += mta.GetModelNameByID(_prod.fk_ModelID);
                    productList += modelList.GetModelName(_prod.fk_ModelID);


                }

                // Add our products
                valueCollection.Add(productList);

                arrayCollection.Add(valueCollection);

                counter += 1;
            }

            BR.ExcelReport xlsReport = new Dealer_Locator.BR.ExcelReport(pathName, arrayCollection);

            string errorMessage = "";

            errorMessage = xlsReport.GenerateSelf();

            return errorMessage;
        }

    }
}
