using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

using SagaraSoftware.ZipCodeUtil;


using System.Drawing;
using System.Drawing.Imaging;

namespace Dealer_Locator
{
    public partial class SalesLeadForm : System.Web.UI.Page
    {

        BR.Lead CurrentSalesLead = new Dealer_Locator.BR.Lead(false);
        bool InDevMode = true;

        string leadCity;
        string leadState;
        string zipCode;
        private string _countryRegionSelected;

        private ArrayList _requiredFieldArray = new ArrayList();


        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Buffer = true;

            lblProductListEmpty.Visible = false;

            txtZip2.Attributes.Add("onChange", "javascript:callServer();");
            if (Request.QueryString["debug"] == "true")
            {
                InDevMode = true;
            }
            else
            {

                InDevMode = false;
            }

            Trace.IsEnabled = false;


            zipCode = Request.QueryString["Zip"];
            leadCity = Request.QueryString["City"];
            leadState = Request.QueryString["State"];

            CurrentSalesLead.leadCity = leadCity;
            CurrentSalesLead.leadState = leadState;
            CurrentSalesLead.zipCode = zipCode;


            if (!Page.IsPostBack)
            {

                /* Image verification */
                #region Image Verification

                // Create a random code and store it in the Session object.
                this.Session["CaptchaImageText"] = BR.Utility.GenerateRandomCode();



                #endregion

                //txtOtherCity.Visible = false;
                //txtOtherCountry.Visible = false;
                //txtOtherPostalCode.Visible = false;
                //txtOtherStateProvince.Visible = false;

                if (zipCode != null && zipCode != "")
                {
                    txtZip2.Value = zipCode;
                }

                if (leadCity != null && leadCity != "")
                {
                    txtCity.Text = leadCity;
                }

                if (leadState != null && leadState != "")
                {
                    txtState.Value = leadState;
                }


                DA.DL.DL_RegionDataTable rdt = new Dealer_Locator.DA.DL.DL_RegionDataTable();
                DA.DLTableAdapters.DL_RegionTableAdapter rta = new Dealer_Locator.DA.DLTableAdapters.DL_RegionTableAdapter();

                rdt = rta.GetData();

                ListItem li2 = new ListItem();
                li2.Text = "United States";
                li2.Value = "United States";
                cboCountryRegion.Items.Add(li2);

                foreach (DA.DL.DL_RegionRow tempRow in rdt.Rows)
                {

                    ListItem li = new ListItem();
                    li.Value = tempRow.RegionName;
                    li.Text = tempRow.RegionName;
                    cboCountryRegion.Items.Add(li);

                }

            }
            else
            {

            }

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ZipCallServer", "callServer();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayModels", "DisplayModels();", true);            

            //cboProductType.Attributes.Add(
            System.Collections.Specialized.NameValueCollection coll = Request.Form;
            litSalesLeadForm.Text = CreateSalesLeadForm2(coll);



            ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language=\"javascript\">callServer();</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "CheckProductScript", "<script language=\"javascript\">CheckProductDisplayMode();</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "CheckProductDisplayMode1", "<script language=\"javascript\">CheckProductDisplayMode();</script>");



            PopulateFieldsFromSessionCardMapping();


            // If in KIOSK mode, do not show or require image verification check

            if (Request.QueryString["kioskMode"] == "true")
            {
                // in kiosk mode

                CurrentSalesLead.InKioskMode = true;
                KioskModeHiddenField.Value = "true";
                
                if (txtOtherCountry.Text != "")
                {
                    lblKioskCountry.Text = "<BR>COUNTRY (card): " + txtOtherCountry.Text;
                    lblKioskCountry.Visible = true;
                }

                if (txtOtherCity.Text != "")
                {
                    lblOtherCityCard.Text = "<BR>CITY (card): " + txtOtherCity.Text;
                    lblOtherCityCard.Visible = true;
                }

                if (txtOtherCity.Text != "")
                {
                    lblCityCard.Text = "<BR>CITY (card): " + txtOtherCity.Text;
                    lblCityCard.Visible = true;
                    lblCityCardWrapper.Visible = true;
                }

                KioskControls.Visible = true;
            }
            else
            {
                KioskModeHiddenField.Value = "false";
                KioskControls.Visible = false;
                lblKioskCountry.Visible = false;
                lblCityCard.Visible = false;
                lblCityCardWrapper.Visible = false;
                lblOtherCityCard.Visible = false;

            }


        }

        void PopulateFieldsFromSessionCardMapping()
        {
            try
            {
                System.Collections.Specialized.NameValueCollection coll2 = new System.Collections.Specialized.NameValueCollection();

                coll2 = ((System.Collections.Specialized.NameValueCollection)Session["MappedData"]);
                Session.Remove("MappedData");

                if (coll2.Keys.Count > 0)


                    // add keys from session to this page
                    for (int i = 0; i < coll2.Keys.Count; i++)
                    {
                        string Key = coll2.GetKey(i);
                        string Value = coll2.GetValues(i)[0];

                        ProcessCardData(Key, Value);

                    }


            }
            catch (Exception exception) { }
        }

        private void ProcessCardData(string FieldName, string CardData)
        {

            switch (FieldName)
            {

                case "txtFirstName":
                    txtFirstName.Text = CardData;
                    break;

                case "txtLastName":
                    txtLastName.Text = CardData;
                    break;

                case "txtCompany_Agency":
                    txtCompany_Agency.Text = CardData;
                    break;

                case "txtAddress1":
                    txtAddress1.Text = CardData;
                    break;

                case "txtAddress2":
                    txtAddress2.Text = CardData;
                    break;

                case "txtZip2":
                    txtZip2.Value = CardData;
                    break;


                case "txtOtherCity":

                    txtOtherCity.Text = CardData;

                    break;


                case "txtOtherStateProvince":
                    txtOtherStateProvince.Text = CardData;
                    break;


                case "txtOtherPostalCode":
                    txtOtherPostalCode.Text = CardData;
                    break;


                case "txtOtherCountry":
                    txtOtherCountry.Text = CardData;
                    break;

                case "cboCityName":

                    break;

                case "txtState":
                    txtState.Value = CardData;
                    break;


                case "txtPhone":
                    txtPhone.Text = CardData;
                    break;

                case "txtFax":
                    txtFax.Text = CardData;
                    break;

                case "txtEmail":
                    txtEmail.Text = CardData;
                    break;


                case "txtComments":
                    txtComments.Value = CardData;
                    break;


            }
        }

        private int GetSalesLeadFormID()
        {

            string slQS = Request.QueryString["slID"];

            int slFormID = -1;

            if (null != slQS)
            {
                slFormID = Convert.ToInt32(slQS);
            }
            else
            {

                try
                {
                    DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();
                    slFormID = Convert.ToInt32(slta.GetDefaultFormID());
                }
                catch
                {
                    slFormID = -1;
                }
            }

            return slFormID;

            //GetDefaultFormID();
        }

        private string CreateSalesLeadForm2(System.Collections.Specialized.NameValueCollection coll)
        {
            string error = string.Empty;
            bool SetFieldValues = Page.IsPostBack;

            BR.FormTemplate ft = new Dealer_Locator.BR.FormTemplate();

            System.Collections.Specialized.NameValueCollection coll2 = new System.Collections.Specialized.NameValueCollection();
            System.Collections.Specialized.NameValueCollection NewCollectionOfRequestVariables = new System.Collections.Specialized.NameValueCollection();

            try
            {
                // add keys from Request.Form to this page
                for (int i = 0; i < coll.Keys.Count; i++)
                {

                    string ValuesToAdd = "";
                    string KeyToAdd = coll.GetKey(i);

                    for (int j = 0; j < coll.GetValues(i).Length; j++)
                    {
                        if (ValuesToAdd != "")
                            ValuesToAdd += ",";

                        ValuesToAdd += coll.GetValues(i)[j];
                    }


                    NewCollectionOfRequestVariables.Add(KeyToAdd, ValuesToAdd);


                }

            }
            catch (Exception exception) { error += exception.Message; }

            try
            {
                coll2 = ((System.Collections.Specialized.NameValueCollection)Session["MappedData"]);


                if (coll2.Keys.Count > 0)
                    SetFieldValues = true;

                // add keys from session to this page
                for (int i = 0; i < coll2.Keys.Count; i++)
                {
                    string Key = coll2.GetKey(i);
                    string Value = coll2.GetValues(i)[0];

                    NewCollectionOfRequestVariables.Add(Key, Value);
                }


            }
            catch (Exception exception) { error += exception.Message; }

            int slFormID = GetSalesLeadFormID();


            if (slFormID != -1)
            {

                return ft.BuildSalesLeadForm(slFormID, NewCollectionOfRequestVariables, SetFieldValues);
            }
            else
            {
                return "";
            }
        }



        private bool StaticRequiredFieldsFilled(BR.Lead.FormField tempField)
        {
            bool IsRequiredFilled = true;
            string tempFieldUpper = tempField.ID.ToUpper();
            string fieldValue = tempField.value;
            string fieldID = tempField.ID;


            foreach (string arrayItem in _requiredFieldArray)
            {


                if (tempFieldUpper.Contains(arrayItem))
                {
                    if (fieldValue == "")
                    {
                        if (tempFieldUpper.Contains("OTHER") && _countryRegionSelected.ToUpper() != "OTHER")
                        {

                        }
                        else
                        {
                            IsRequiredFilled = false;
                            tempField.IsValid = false;
                        }

                    }
                }

            }

            return IsRequiredFilled;

        }

        /// <summary>
        /// Sends the sales lead form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSendSalesLeadForm_Click(object sender, EventArgs e)
        {

            bool InKioskMode = false;
            KioskModeHiddenField.Value = "false";
            bool DoNotPlanToBuySelected = false;

            if (Request.QueryString["kioskMode"] == "true")
            {
                InKioskMode = true;
                CurrentSalesLead.InKioskMode = true;
                KioskModeHiddenField.Value = "true";
            }


                
                litError.Text = "";

                lblProductListEmpty.Visible = false;

                try
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();

                    string requiredFields = "";
                    ArrayList fields = new ArrayList();

                    DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable dt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
                    DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter ta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();


                    foreach (string item in Request.Form)
                    {

                        string ItemValue = Request.Form[item];

                        if (item.Contains("requiredFieldsList") == true)
                        {
                            requiredFields = ItemValue;
                        }
                        else
                        {
                            BR.Lead.FormField tempField = new BR.Lead.FormField();
                            tempField.ID = item;
                            tempField.value = ItemValue;

                            fields.Add(tempField);
                        }

                        if (item.Contains("PlanToBuy") == true)
                        {
                            if (ItemValue.ToUpper() == "DO NOT PLAN TO BUY")
                                DoNotPlanToBuySelected = true;
                            else
                                DoNotPlanToBuySelected = false;
                        }

                    }

                    string[] requiredFieldsArray = requiredFields.Split(',');

                    int fieldCounter = 0;

                    bool AreRequiredFieldsFilled = true;

                    if (requiredFieldsArray.Length == 0)
                        requiredFieldsArray = new String[1];

                    requiredFieldsArray[0] = "!!!***JustAPlaceHolder***!!!";

                    System.Collections.Generic.List<string> reqFieldsNotFilled = new System.Collections.Generic.List<string>();

                    _requiredFieldArray.Clear();

                    _requiredFieldArray.Add("FIRSTNAME");
                    _requiredFieldArray.Add("LASTNAME");
                    _requiredFieldArray.Add("COMPANY_AGENCY");
                    _requiredFieldArray.Add("COUNTRYREGION");
                    _requiredFieldArray.Add("ADDRESS1");
                    _requiredFieldArray.Add("CITY");
                    _requiredFieldArray.Add("PHONE");
                    _requiredFieldArray.Add("PLANTOBUY");

                    if (cboCountryRegion.SelectedItem.Text == "United States")
                    {
                        _requiredFieldArray.Add("ZIP2");
                        _requiredFieldArray.Add("STATE");
                    }

                    _countryRegionSelected = cboCountryRegion.SelectedItem.Text;


                    if (DoNotPlanToBuySelected)
                        _requiredFieldArray.Add("TXTCOMMENTS");

                    // validate our fields
                    foreach (BR.Lead.FormField tempField in fields)
                    {

                        if (tempField.ID.ToUpper().Contains("CHKRENTAL"))
                        {
                            if (tempField.value == "on")
                            {
                                CurrentSalesLead.isRentalCompany = true;
                            } else {
                                CurrentSalesLead.isRentalCompany = false;
                            }
                        }

                        if (StaticRequiredFieldsFilled(tempField) == false)
                        {
                            AreRequiredFieldsFilled = false;

                            reqFieldsNotFilled.Add(tempField.ID);

                            string fieldID = tempField.ID;

                            foreach (Control c in this.Page.Controls)
                            {
                                if (c.ID == fieldID)
                                    if (c is TextBox)
                                        ((TextBox)c).Text = "NOT SET";
                            }

                        }

                        foreach (string tempReqField in requiredFieldsArray)
                        {
                            if (tempReqField != "")
                            {
                                BR.Lead.FormField superTempField;
                                superTempField = (BR.Lead.FormField)(fields[fieldCounter]);

                                if (tempField.ID.Contains(tempReqField))
                                {

                                    if (tempField.value != "")
                                    {
                                        superTempField.IsValid = true;
                                    }
                                    else
                                    {
                                        reqFieldsNotFilled.Add(superTempField.ID);

                                        superTempField.IsValid = false;
                                        AreRequiredFieldsFilled = false;
                                    }
                                }
                            }
                        }


                        fieldCounter = fieldCounter + 1;
                    }


                    if (AreRequiredFieldsFilled == true)
                    {

                        if (this.CodeNumberTextBox.Text != this.Session["CaptchaImageText"].ToString() && InKioskMode == false)
                        {
                            string script = "ActivateTab(2); ShowToolTip('" + this.CodeNumberTextBox.ClientID + "','Incorrect Verification Code.  Please try again');";

                            ClientScript.RegisterStartupScript(this.Page.GetType(), "IncorrectVerificationCode", script, true);

                            // Clear the input and create a new random code.
                            this.CodeNumberTextBox.Text = "";
                            this.Session["CaptchaImageText"] = BR.Utility.GenerateRandomCode();
                        }
                        else
                        {


                            #region Required Fields Filled = true

                            string errorMessage;
                            System.Text.StringBuilder errorBuilder = new System.Text.StringBuilder();
                            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();


                            ArrayList productArray = new ArrayList();

                            List<ProductSearch.Product> pageProducts = ProductSearch.GetList();
                            Dealer_Locator.BR.ModelList modelList = new BR.ModelList();

                            if (pageProducts != null)
                            {
                                foreach (ProductSearch.Product tempPageProduct in pageProducts)
                                {

                                    if (tempPageProduct.selected)
                                    {
                                        string mainCatId = string.Empty;
                                        string mainCat = string.Empty;
                                        string subCatID = string.Empty;
                                        string subCat = string.Empty;
                                        string model = string.Empty;
                                        string modelID = string.Empty;
                                        string modelUrl = string.Empty;

                                        // Get the model from the model list and assign it
                                        modelID = tempPageProduct.modelID.ToString();

                                        BR.ModelList.Model tempProductModelList = modelList.GetModel(Convert.ToInt32(modelID));

                                        mainCatId = tempProductModelList.MainCategoryID.ToString();
                                        mainCat = tempProductModelList.MainCategoryName;

                                        subCatID = tempProductModelList.SubCategoryID.ToString();
                                        subCat = tempProductModelList.SubCategoryName;

                                        model = tempProductModelList.ModelName;
                                        modelUrl = tempProductModelList.ModelURL;


                                        BR.Lead.ProductList tempProduct = new BR.Lead.ProductList();

                                        tempProduct.model = model;
                                        tempProduct.product = mainCat;
                                        tempProduct.subCat = subCat;
                                        tempProduct.productId = mainCatId;
                                        tempProduct.modelID = modelID;
                                        tempProduct.subCatID = subCatID;
                                        tempProduct.modelURL = modelUrl;

                                        DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                                        DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new DA.MainCategoryTDS.DL_MainCategoryDataTable();
                                        
                                        mcdt = mcta.GetDataByMainCategoryID(Convert.ToInt32(mainCatId));

                                        if (mcdt.Rows.Count > 0) {

                                            DA.MainCategoryTDS.DL_MainCategoryRow mcdr = ((DA.MainCategoryTDS.DL_MainCategoryRow)mcdt.Rows[0]);

                                            if (mcdr.AllowTerritoryOverlap == true){
                                                tempProduct.AllowTerritoryOverlap = true;
                                            } else {
                                                tempProduct.AllowTerritoryOverlap = false;
                                            }

                                        }

                                        if (PhysicalDelivery.Checked)
                                            tempProduct.IsMail = true;

                                        if (EmailDelivery.Checked)
                                            tempProduct.IsElectronic = true;


                                        string tempModelDisplayText = "";
                                        tempModelDisplayText = model;

                                        if (tempProduct.IsMail && tempProduct.IsElectronic)
                                        {
                                            tempModelDisplayText = tempModelDisplayText + " - Electronic and Shipped";
                                        }
                                        else if (tempProduct.IsElectronic)
                                        {
                                            tempModelDisplayText = tempModelDisplayText + " - Electronic";
                                        }
                                        else if (tempProduct.IsMail)
                                        {
                                            tempModelDisplayText = tempModelDisplayText + " - Shipped";
                                        }
                                        else
                                        {
                                            tempModelDisplayText = tempModelDisplayText + " - None Specified";
                                        }

                                        tempProduct.modelDisplayText = tempModelDisplayText;


                                        productArray.Add(tempProduct);

                                    }
                                }
                            }

                            // Should there be a check for "Do Not Plan To Buy?"
                            if ((productArray != null && productArray.Count != 0) || DoNotPlanToBuySelected == true)
                            {

                                System.Web.UI.Page myPage = this.Page;
                                CurrentSalesLead.SalesLeadsMaster(ref errorBuilder, productArray, ref fields, InDevMode, ref myPage);

                                this.Page = myPage;

                                GotoThanksUrl();
                            }
                            else
                            {
                                lblProductListEmpty.Visible = true;
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        string requiredFieldsTooltipScript = string.Empty;
                        
                        foreach (string fieldID in reqFieldsNotFilled)
                        {

                            requiredFieldsTooltipScript += "ShowToolTip('" + fieldID.Replace("$", "_") + "');" + Environment.NewLine;

                        }

                        ClientScript.RegisterStartupScript(this.Page.GetType(), "RequiredFieldsTooltipScript", requiredFieldsTooltipScript, true);


                        //lblRequiredFieldsNotFilled.Visible = true;


                        //string FieldRequiredCheck = "<script language=\"JavaScript\">";


                        //foreach (string fieldID in reqFieldsNotFilled)
                        //{

                        //    FieldRequiredCheck += "document.getElementById('" + fieldID.Replace("$", "_") + "').style.backgroundColor = \"#FFA800\";" + Environment.NewLine;

                        //}

                        //FieldRequiredCheck += "</script>";
                        //RegisterStartupScript("FieldsAreRequiredCheck", FieldRequiredCheck);

                        //ClientScript.RegisterStartupScript(this.Page.GetType(), "RequiredFieldsNotFilledDisplay", "ShowRequiredFieldsNotFilled()", true);

                    }
                    litError.Visible = false;
                }
                catch (Exception ex)
                {
                    litError.Text = litError.Text + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Data;
                    litError.Visible = true;
                }
            

            CurrentSalesLead = new Dealer_Locator.BR.Lead(InKioskMode);

        }


        private void GotoThanksUrl()
        {
            string hfID = Request.QueryString["hfID"];

            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            // check if the header footer id is good.  If so, continue to get data from tht b
            if (hfID == "" || hfID == null)
            {
                hfDT = GetDefaultHeaderFooter();
            }
            else
            {
                try
                {
                    DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                    hfDT = hfTA.GetDataByHeaderFooterID(Convert.ToInt32(hfID));
                }
                catch
                {
                    hfDT = GetDefaultHeaderFooter();
                }
            }

            string ThanksUrl = "";
            try
            {
                ThanksUrl = hfDT[0].thanksUrl;


                // Redirect to the thanks url
                if (ThanksUrl == "")
                {
                    ThanksUrl = "http://www.findbomag.com/Thanks.aspx";

                    if (hfID != "")
                        ThanksUrl = ThanksUrl + "?hfID=" + hfID.ToString();

                }
            }
            catch
            {

            }

            string locationToGoto = "";

            if (InDevMode == false)
            {
                locationToGoto = ThanksUrl;
            }
            else
            {
                locationToGoto = "DevDisplay.aspx";
            }


            if (Request.QueryString["kioskMode"] == "true")
            {
                // in kiosk mode

                locationToGoto = "KioskThankYou.aspx";
            }

            Session.Remove("PRODUCT_LIST");  // remove the BR.Lead.ProductList

            Response.Buffer = true;
            Response.Clear();
            Response.Status = "301 Moved";
            Response.AddHeader("Location", locationToGoto);

            Response.End();
        }

        private static DA.HeaderFooter.DL_HeaderFooterDataTable GetDefaultHeaderFooter()
        {
            DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter hfTA = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
            DA.HeaderFooter.DL_HeaderFooterDataTable hfDT = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

            hfDT = hfTA.GetDefaultHeaderFooter();

            return hfDT;
        }

        protected void btnBackToKioskScreen_Click(object sender, EventArgs e)
        {
            string locationToGoto = "Kiosk.aspx";

            Session.Remove("ProductList");  // remove the BR.Lead.ProductList

            Response.Buffer = true;
            Response.Clear();
            Response.Status = "301 Moved";
            Response.AddHeader("Location", locationToGoto);

            Response.End();
        }

        [WebMethod]
        public static void SetIndex(int index)
        {
            if (HttpContext.Current.Session["ReloadTab1"].ToString() != "false")
            {
                HttpContext.Current.Session["TabIndex1"] = index;
            }

            HttpContext.Current.Session["ReloadTab1"] = "true";
        }

    }
}
