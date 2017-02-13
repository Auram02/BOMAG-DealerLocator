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

using System.IO;


namespace Dealer_Locator.BR
{
    public class SalesLeadImport
    {
        private int _formID;
        private string _formName;
        public ArrayList elementNames;
        private string _url;
        private string _outputURL;
        public DataTable dt = new DataTable();
        public DataTable errorRows = new DataTable();

        public string outputURL
        {
            set { _outputURL = value; }
            get { return _outputURL; }
        }

        public SalesLeadImport(int formID, string formName, string url)
        {
            this._formID = formID;
            this._formName = formName;
            this._url = url;
            elementNames = new ArrayList();
        }

        #region CreateImportTemplate()

        public string CreateImportTemplate()
        {
            DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
            DA.FormElementTDS.DL_FormElementDataTable dt = new Dealer_Locator.DA.FormElementTDS.DL_FormElementDataTable();

            dt = ta.GetAllFormElementsBySLFormID(this._formID);

            AddStaticFields();

            foreach (DA.FormElementTDS.DL_FormElementRow dr in dt.Rows)
            {
                try
                {
                    string labelName = dr.label;

                    labelName = labelName.Replace(" ", "");

                    if (labelName != "")
                        elementNames.Add(labelName);
                    else
                        elementNames.Add(dr.ID);

                }
                catch
                {

                    elementNames.Add(dr.ID);
                }

            }


            AddStaticFields_after();

            elementNames.Add("MODELS");

            //BR.ExcelReport xlsReport = new ExcelReport(this._url, elementNames);
            //xlsReport.GenerateSelf();
            FileInfo fi = new FileInfo(this._url);

            StreamWriter s = fi.CreateText();

            string crlf = "\r\n";


            string outputString = "";

            foreach (string elementName in elementNames)
            {
                if (outputString != "")
                    outputString += ",";

                outputString = outputString + elementName;
            }
            s.Write(outputString);
            s.Close();

            return "";

        }
        
#endregion

        #region AddStaticFields() and AddStaticFields_after()

        private void AddStaticFields()
        {

            elementNames.Add("FIRSTNAME");
            elementNames.Add("LASTNAME");
            elementNames.Add("COMPANY_AGENCY");
            elementNames.Add("COUNTRYREGION");
            elementNames.Add("ADDRESS1");
            elementNames.Add("ADDRESS2");
            elementNames.Add("ZIP");
            elementNames.Add("CITY");
            elementNames.Add("STATE");
            elementNames.Add("PHONE");
            elementNames.Add("EMAIL");
            elementNames.Add("FAX");
            elementNames.Add("PLANTOBUY");
            
        }

        private void AddStaticFields_after()
        {

            elementNames.Add("COMMENTS");

        }
        #endregion


        struct ErrorRows
        {
            public int index;
            public bool ModelError;
            public bool LocationError;
        }

        public void Read()
        {
            DA.CSV csvClass = new Dealer_Locator.DA.CSV(outputURL);
            dt = csvClass.PopulateDataTableFromUploadedFile();


            DA.ModelTDSTableAdapters.DL_ModelTableAdapter mta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();


            dt.Rows.RemoveAt(0);

            errorRows = new DataTable();  // clear error row table

            errorRows = dt.Clone();

            ArrayList errorRowIndexes = new ArrayList();

            int counter = 0;
            foreach (DataRow dr in dt.Rows)
            {
                bool ModelErrorOccurred = false;
                bool LocationErrorOccurred = false;

                string[] models = dr[dt.Columns.Count - 1].ToString().Split(';');

                for (int i = 0; i < models.Length; i++)
                {

                    string currentModelName = models[i];
                    if (currentModelName.Length > 0)
                    {
                        // strip leading and trailing spaces
                        if (currentModelName.Substring(0, 1) == " ")
                            currentModelName = currentModelName.Remove(0, 1);

                        if (currentModelName.Substring(currentModelName.Length - 1, 1) == " ")
                            currentModelName = currentModelName.Remove(currentModelName.Length - 1, 1);

                        models[i] = currentModelName;

                        int modelExists = Convert.ToInt32(mta.DoesModelExist(currentModelName));

                        //if invalid
                        if (modelExists == 0)
                        {
                            ModelErrorOccurred = true;

                            i = models.Length + 1;  // break out of the loop.  that row has an error
                        }
                    }
                }

                // Check for location error
                string cityName = dr[7].ToString();
                string stateName = dr[8].ToString();
                int zipCode = Convert.ToInt32(dr[6].ToString());

                DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter zlta = new Dealer_Locator.DA.ZipLookupTDSTableAdapters.DL_ZipLookupTableAdapter();
                int cityCount = (int)zlta.CheckIfCityExistsInZipCode(cityName, zipCode);
                int stateCount = (int)zlta.CheckIfStateExists(stateName);

                if (cityCount < 1 || stateCount < 1)
                    LocationErrorOccurred = true;

                if (ModelErrorOccurred || LocationErrorOccurred)
                {
                    ErrorRows tempRow = new ErrorRows();
                    tempRow.index = counter + 2;
                    tempRow.LocationError = LocationErrorOccurred;
                    tempRow.ModelError = ModelErrorOccurred;

                    errorRowIndexes.Add(tempRow);


                    errorRows.ImportRow(dt.Rows[counter]);
                }

                ModelErrorOccurred = false;
                LocationErrorOccurred = false;

                counter += 1;
            }

            // add error indexes to error rows

            errorRows.Columns.Add("Row Number");
            errorRows.Columns.Add("Model Error Occurred");
            errorRows.Columns.Add("City/State/Zip Error Occurred");

            int counter2 = 0;
            foreach (DataRow dr in errorRows.Rows)
            {
                ErrorRows temperror = ((ErrorRows)(errorRowIndexes[counter2]));
                dr[errorRows.Columns.Count - 3] = temperror.index.ToString();
                dr[errorRows.Columns.Count - 2] = temperror.ModelError.ToString();
                dr[errorRows.Columns.Count - 1] = temperror.LocationError.ToString();
                
                counter2 += 1;
            }

            
        }


        public int ProcessLeads()
        {
            ArrayList salesLeads = new ArrayList();
            BR.Lead tempLead = new Lead(false);

            int rowsProcessed = 0;

            try
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    tempLead = new Lead(false);

                    DataRow dr = dt.Rows[j];

                    string[] models = dr[dt.Columns.Count - 1].ToString().Split(';');

                    ArrayList fields = new ArrayList();                    

                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string fieldID, fieldValue;

                        fieldID = this.elementNames[k].ToString();
                        fieldValue = dr[k].ToString();

                        //tempLead.AddLeadValue(fieldID, fieldValue);
                        
                        // add to fields collection
                        BR.Lead.FormField tempField = new Lead.FormField();
                        tempField.ID = fieldID;
                        tempField.value = fieldValue;
                        tempField.IsValid = true;

                        fields.Add(tempField);

                    }

                    
                    // MODELS
                    ArrayList modelArrayList = new ArrayList();

                    for (int i = 0; i < models.Length; i++)
                    {
                        BR.Lead.ProductList tempProduct = new Lead.ProductList();
                        DA.ModelTDS.DL_ModelDataTable mdt = new Dealer_Locator.DA.ModelTDS.DL_ModelDataTable();
                        DA.ModelTDSTableAdapters.DL_ModelTableAdapter mta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();

                        string modelName = models[i].ToString();

                        // Get model ID
                        int modelID = 0;
                        int mainCatID = 0;
                        int subCatID = 0;

                        if (modelName.Length > 0)
                        {
                            // strip leading and trailing spaces
                            if (modelName.Substring(0, 1) == " ")
                                modelName = modelName.Remove(0, 1);

                            if (modelName.Substring(modelName.Length - 1, 1) == " ")
                                modelName = modelName.Remove(modelName.Length - 1, 1);


                            modelID = (int)mta.GetModelIDByName(modelName);

                            mdt = mta.GetDataByModelID(Convert.ToInt32(modelID));

                            mainCatID = Convert.ToInt32(mdt[0].fk_mainCatID.ToString());
                            subCatID = Convert.ToInt32(mdt[0].fk_subCatID.ToString());


                            string modelUrl = "";
                            try
                            {
                                modelUrl = mdt[0].modelUrl.ToString();
                            }
                            catch
                            {

                            }



                            tempProduct.model = modelName;
                            tempProduct.modelID = modelID.ToString();
                            tempProduct.modelURL = modelUrl;
                            tempProduct.productId = mainCatID.ToString();
                            tempProduct.subCatID = subCatID.ToString();
                            tempProduct.IsElectronic = false;
                            tempProduct.IsMail = true;
                            tempProduct.modelDisplayText = "";


                            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mainta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                            tempProduct.product = mainta.GetMainCategoryNameByID(mainCatID).ToString();
                            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter subta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();

                            try
                            {
                                tempProduct.subCat = subta.GetSubCatNameByID(subCatID).ToString();
                            }
                            catch
                            {
                                tempProduct.subCat = "";
                            }

                            modelArrayList.Add(tempProduct);
                        }
                    }

                    // Add lead info
                    //#region Lead Info

                    //string PlanToBuy = dr[12].ToString();
                    //int MonthsToAddToSendDate = tempLead.GetMonthsToAddToSubmit(PlanToBuy);

                    //bool ErrorOccurred = false;
                    //bool submitted = false;

                    //if (MonthsToAddToSendDate == 0)
                    //    submitted = true;

                    //DateTime sendDate = DateTime.Now.AddMonths(MonthsToAddToSendDate);

                    //tempLead.AddLead(DateTime.Now, submitted, ErrorOccurred, "", sendDate);


                    //#endregion

                    System.Text.StringBuilder errorBuilder = new System.Text.StringBuilder();

                    System.Web.UI.Page myPage = new Page();

                    tempLead.zipCode = dr[6].ToString();
                    tempLead.leadCity = dr[7].ToString();
                    tempLead.leadState = dr[8].ToString();



//                    tempLead.leadCity = "Dunlap";
  //                  tempLead.leadState = "IL";
    //                tempLead.zipCode = "61525";

                    tempLead.SalesLeadsMaster(ref errorBuilder, modelArrayList, ref fields, false, ref myPage);

                    rowsProcessed += 1;
                }
            }
            catch (Exception ex)
            {


            }

            return rowsProcessed;
        }

    }
}
