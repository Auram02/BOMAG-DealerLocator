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
using System.Diagnostics;
using System.Text;


namespace Dealer_Locator.BR
{
    public class FormTemplate
    {



        public static string WriteNavigationLinks(string curPage, bool isZipLocator)
        {
            string navLinks = "";
            string urlStart = "";

            if (isZipLocator == false)
            {
                navLinks = "<div class=\"headerLink\"><table cellspacing=\"0px\" cellpadding=\"0\" class=\"headerTable\"><tr><td>Dash Board: </td>";
                navLinks = navLinks + "<td><a href=\"/admin/FormDevelopment/ThanksURL.aspx\">Thanks URL Forward</a></td>";
                navLinks = navLinks + "<td><a href=\"/admin/FormDevelopment/FormElements.aspx\">Form Elements</a></td>";
                navLinks = navLinks + "<td><a href=\"/admin/FormDevelopment/Reorder/ReorderFormElements.aspx\">Reorder Form Elements</a></td>";
                navLinks = navLinks + "<td><a href=\"/admin/FormDevelopment/Reorder/ReorderElementValues.aspx\">Reorder Form Element Values</a></td>";
                navLinks = navLinks + "</tr></table></div>";
            }

            switch (curPage)
            {
                case "Dashboard.aspx":

                    break;

                case "HeaderFooter.aspx":

                    break;

                case "FormElements.aspx":

                    break;

                case "SubCategories.aspx":

                    break;

                case "ProductModels.aspx":

                    break;
            }

            return navLinks;
        }

        public static string GetThanksURL(int formID)
        {
            DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter ta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();
            DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable dt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
            dt = ta.GetDataByFormID(formID);

            return (string)(dt.Rows[0]["thanksURL"]);
        }

        public static bool UpdateThanksURL(string URL, int formID)
        {
            bool returnVal = true;

            try
            {
                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter ta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();
                ta.UpdateThanksUrl(URL, formID);
            }
            catch
            {
                returnVal = false;
            }

            return returnVal;
        }

        public string BuildSalesLeadForm(int formID, System.Collections.Specialized.NameValueCollection formColl, bool IsPostBack)
        {

            string outputForm = "";

            DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
            DA.FormElementTDS.DL_FormElementDataTable dt = new Dealer_Locator.DA.FormElementTDS.DL_FormElementDataTable();

            dt = ta.GetAllFormElementsBySLFormID(formID);

            // for checking field type
            DA.FieldElementTypeTDSTableAdapters.DL_ElementTypeTableAdapter feTa = new Dealer_Locator.DA.FieldElementTypeTDSTableAdapters.DL_ElementTypeTableAdapter();
            DA.FieldElementTypeTDS.DL_ElementTypeDataTable feDt = new Dealer_Locator.DA.FieldElementTypeTDS.DL_ElementTypeDataTable();


            ArrayList requiredFields = new ArrayList();

            string className = "evenRowElement";

            // loop through each form element to display
            // build the form as needed
            foreach (DA.FormElementTDS.DL_FormElementRow dr in dt.Rows)
            {
                string typeName = "", valueList = "";
                int typeID = 0;



                typeID = Convert.ToInt32(dr.fk_typeID.ToString());

                // determine element type
                feDt = feTa.GetDataByTypeID(typeID);
                typeName = feDt[0]["name"].ToString();

                string fieldText = "";
                string fieldEndTag = "";
                string divText = "";
                string labelText = string.Empty;

                // Get details about controls
                string label = "", id = "", text = "", cssclass = "";
                bool isRequired = false;

                if (dr.IsCssClassNull() == true)
                    cssclass = "";
                else
                    cssclass = dr.CssClass.ToString();


                if (dr.IslabelNull() == true)
                    label = "";
                else
                    label = dr.label.ToString();

                if (dr.IsIDNull() == true)
                    id = "";
                else
                    id = dr.ID.ToString();

                if (dr.IsTextNull() == true)
                    text = "";
                else
                    text = dr.Text.ToString();

                if (dr.IsrequiredNull() == true)
                    isRequired = false;
                else
                    isRequired = Convert.ToBoolean(dr.required.ToString());

                // if item is required, add the current field to the required field list
                if (isRequired)
                {
                    requiredFields.Add(id);
                    cssclass += " RequiredField";
                }

                fieldText = "<div>";

                if (typeName == "RadioButtonList")
                {
                    fieldText = fieldText + CreateRadioButtonList(dr.pk_formElementID, id, label, formColl, IsPostBack, className, isRequired);

                    labelText = CreateLabelText(id);
                    fieldText = fieldText + labelText + label;  // opens the label

                    if (isRequired)
                        fieldText += "<span class='RequiredFieldIndicator'>*</span>";

                    fieldText += "</label></div>";
                }
                else if (typeName == "CheckboxList")
                {
                    fieldText = fieldText + CreateCheckboxList(dr.pk_formElementID, id, label, formColl, IsPostBack, isRequired, className);

                    labelText = CreateLabelText(id);
                    fieldText = fieldText + labelText + label;  // opens the label

                    if (isRequired)
                        fieldText += "<span class='RequiredFieldIndicator'>*</span>";

                    fieldText += "</label></div>";
                }
                else
                {

                    #region TypeName switch


                    switch (typeName)
                    {
                        case "TextBox (SingleLine)":
                            fieldText += "<input type=\"text\"  size=\"30\" runat=\"server\" ";
                            fieldEndTag = "";
                            break;

                        case "Textbox (MultiLine)":
                            fieldText += "<TEXTAREA rows=\"6\" cols=\"30\"  runat=\"server\" ";
                            fieldEndTag = "</TEXTAREA>";

                            break;


                        case "DropdownList":
                            fieldText += "<SELECT  STYLE=\"overflow: scroll\" runat=\"server\" ";
                            fieldEndTag = "</SELECT>";
                            break;


                        case "ListBox":
                            fieldText += "<SELECT SIZE=\"5\" MULTIPLE  STYLE=\"height: 10px; overflow: scroll\" runat=\"server\" ";
                            fieldEndTag = "</SELECT>";
                            break;


                        case "RadioButtonList":
                            // HANDLED SEPERATELY ABOVE

                            //returnText = "";
                            //fieldEndTag = "</SELECT>";

                            break;

                        case "CheckboxList":
                            //<asp:CheckBoxList id="MyCheckboxes" runat="server">

                            //fieldText += "<asp:CheckBoxList runat\"server\" ";
                            //fieldEndTag = "</asp:CheckBoxList>";

                            break;

                        case "Label":
                            // not sure yet
                            fieldText += "<label  runat=\"server\" ";
                            fieldEndTag = "</label>";
                            break;


                        case "Password":
                            fieldText += "<input type=\"password\" size=\"30\"  runat=\"server\" ";
                            fieldEndTag = "";
                            break;


                        case "Hidden":
                            fieldText += "<input type=\"hidden\"  runat=\"server\" ";
                            fieldEndTag = "";
                            break;

                        default:
                            break;
                    }

                    #endregion


                    if (typeName != "Label")
                    {


                        // Get ID
                        id = CreateElementID(id);

                        fieldText = fieldText + CreateIDForElement(id, false, 0);


                        fieldText = fieldText + CreateNameForElement(id);
                        fieldText = fieldText + CreateCssClassForElement(cssclass);

                        string postbackFormValue = "";

                        // set the values of the controls if we are in a postback
                        if (IsPostBack)
                        {
                            foreach (string name in formColl)
                            {
                                if (name == id)
                                {
                                    postbackFormValue = formColl[name];

                                    fieldText = fieldText + " value=\"" + postbackFormValue + "\" ";

                                }
                            }

                        }

                        // close the tag
                        fieldText = fieldText + ">";


                        // Add options if this is a multivalue type control
                        bool IsMultiValueType = Convert.ToBoolean(feDt.Rows[0]["isMultiValue"].ToString());

                        if (IsMultiValueType)
                            valueList = CreateMultiValueItemList(dr.pk_formElementID, id, formColl, IsPostBack);

                        if (valueList != "")
                            fieldText = fieldText + valueList;

                        // add the value from the postback to the textarea
                        if (typeName == "Textbox (MultiLine)")
                            fieldText = fieldText + postbackFormValue;

                        // close off the field and the div
                        fieldText = fieldText + fieldEndTag;

                        labelText = CreateLabelText(id);
                        fieldText = fieldText + labelText + label;  // opens the label

                        if (isRequired)
                            fieldText += "<span class='RequiredFieldIndicator'>*</span>";



                        //fieldText += CreateLabelForElement(label, divText, isRequired, className);
                        fieldText += "</label></div>";


                    }
                    else
                    {
                        fieldText = fieldText + ">" + text + fieldEndTag;
                    }
                }
                fieldText = fieldText + Environment.NewLine;

                outputForm = outputForm + fieldText + "" + Environment.NewLine;

                if (className == "evenRowElement")
                    className = "oddRowElement";
                else
                    className = "evenRowElement";
            }


            outputForm = outputForm + Environment.NewLine + Environment.NewLine;

            // Create the Required Fields Check JavaScript
            string verifyScript = CreateRequiredVerifyScript(requiredFields);


            string outputRequiredFields = "";
            foreach (string name in requiredFields)
            {
                if (outputRequiredFields != "")
                    outputRequiredFields = outputRequiredFields + ",";

                outputRequiredFields = outputRequiredFields + name;

            }

            //outputForm = verifyScript + outputForm;


            outputForm = outputForm + "<input type=\"text\" name=\"requiredFieldsList\" value=\"" + outputRequiredFields + "\" style=\"display: none;\">";

            return outputForm;
        }

        #region Page Elements


        private string CreateRequiredVerifyScript(ArrayList requiredFields)
        {
            string verifyScript = "<SCRIPT LANGUAGE=\"JavaScript\">" + Environment.NewLine +
                " function __doPostBack(eventTarget,eventArgument) {" + Environment.NewLine +
                " var themessage = \"\";" + Environment.NewLine;

            foreach (string field in requiredFields)
            {
                verifyScript = verifyScript + " if (document.getElementById('" + field + "').value==\"\") " + Environment.NewLine + "{" +
                    "themessage = themessage + \" -  " + field + "<BR>\";" +
                          "document.getElementById('div" + field + "_verify').style.display = \"block\";}" + Environment.NewLine;


                //                                   "themessage = themessage + \" -  " + field + "<BR>\";}";

            }



            verifyScript = verifyScript + " if (themessage == \"\") {" +
                           " document.aspnetForm.submit();" +
                           " } else { alert(\"Please enter data into the Required fields\");" +
                           " return false; } }" + Environment.NewLine +
                           " //  End --> " + Environment.NewLine +
                           " </script>";

            return verifyScript;
        }

        private string CreateCssClassForElement(string cssclass)
        {
            string returnText = "";

            if (cssclass != "")
                returnText = " class=\"" + cssclass + "\"";
            else
                returnText = "";

            return returnText;

        }

        private string CreateIDForElement(string id, bool AddExtraDigit, int ExtraDigit)
        {
            string returnVal = "";

            if (AddExtraDigit)
            {
                id = id + ExtraDigit.ToString();
            }

            returnVal = (" id=\"" + id + "\"");

            return returnVal;
        }

        private string CreateNameForElement(string id)
        {
            return (" name=\"" + id + "\"");
        }

        //private string CreateLabelForElement(string label, string divText, bool isRequired, string className)
        //{
        //    string returnText = "";

        //    string requiredPlaceholder = "";

        //    if (isRequired)
        //        requiredPlaceholder = "<span class='RequiredFieldIndicator'>*</span>";


        //    // add label
        //    if (label == "")
        //    {


        //        //returnText = "<tr valign=\"top\" class=\"" + className + "\"><td>" + divText + requiredPlaceholder + "</td><td>";
        //        returnText = "<div class=\"" + className + "\">" + divText + requiredPlaceholder + "</div>";
        //    }
        //    else
        //    {
        //        returnText = "<div class=\"" + className + "\">" + divText + label + requiredPlaceholder + "</div>";
        //    }

        //    return returnText;
        //}

        //private string CreateLabelForRadioList(string label, string divText, bool isRequired, string className)
        //{
        //    string returnText = "";

        //    string requiredPlaceHolder = string.Empty;

        //    if (isRequired)
        //        requiredPlaceHolder = "<span class='RequiredFieldIndicator'>*</span>";

        //    // add label
        //    if (label == "")
        //    {


        //        returnText = "<div class=\"" + className + "\">" + divText + requiredPlaceHolder + "</div>";
        //    }
        //    else
        //    {
        //        returnText = "<div valign=\"top\" class=\"" + className + "\">" + divText + label + requiredPlaceHolder + "</div>";
        //    }

        //    return returnText;
        //}

        // Deprecated.  No Longer Needed
        //private string UpdateLabelText(string label)
        //{
        //    System.Random ranNumber = new Random(DateTime.Now.Millisecond);
        //    int nextRandom = ranNumber.Next(100, 10000);

        //    label = "formElement" + nextRandom.ToString();

        //    return label;
        //}

        private string CreateElementID(string id)
        {
            // Add an ID
            // we need to figure out the id before going forward
            if (id == "")
            {
                System.Random ranNumber = new Random(DateTime.Now.Millisecond);
                int nextRandom = ranNumber.Next(100, 10000);

                id = "id" + nextRandom.ToString();
            }

            return id;
        }

        private string CreateDivText(string id)
        {
            return ("<div runat=\"server\" id=\"div" + id + "\">");
        }

        private string CreateLabelText(string id)
        {
            return ("<label for=\"" + id + "\" class=\"FormFieldFullWidthLabel\">");
        }

        #endregion


        private string CreateMultiValueItemList(int elementID, string formElementID, System.Collections.Specialized.NameValueCollection formColl, bool isPostBack)
        {
            StringBuilder itemList = new StringBuilder();



            DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter evTA = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();
            DA.ElementValueTDS.DL_ElementValueDataTable evDT = new Dealer_Locator.DA.ElementValueTDS.DL_ElementValueDataTable();

            evDT = evTA.GetDataByFormElementID(elementID);

            foreach (DA.ElementValueTDS.DL_ElementValueRow dr in evDT.Rows)
            {
                itemList.Append("<OPTION VALUE=\"" + dr.value + "\"");

                if (isPostBack)
                {
                    foreach (string name in formColl)
                    {
                        if (name == formElementID)
                        {
                            if (formColl[name] == dr.value)
                            {
                                itemList.Append(" SELECTED ");
                            }
                        }
                    }
                }

                itemList.Append(">" + dr.value + "</OPTION>" + Environment.NewLine);
            }

            return itemList.ToString();
        }


        private string CreateRadioButtonList(int elementID, string id, string label, System.Collections.Specialized.NameValueCollection formColl, bool isPostBack, string className, bool isRequired)
        {


            string fieldText = "";
            string divText = "", cssclass = "";

            string labelText = string.Empty;

            DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter evTA = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();
            DA.ElementValueTDS.DL_ElementValueDataTable evDT = new Dealer_Locator.DA.ElementValueTDS.DL_ElementValueDataTable();

            evDT = evTA.GetDataByFormElementID(elementID);

            fieldText = Environment.NewLine + fieldText;

            // Get ID
            id = CreateElementID(id);

            //labelText = CreateLabelText(id);
            //fieldText = labelText + fieldText;


            int counter = 0;
            foreach (DA.ElementValueTDS.DL_ElementValueRow dr in evDT.Rows)
            {
                counter = counter + 1;
                fieldText = fieldText + "<input runat=\"server\" type=\"radio\" ";

                if (isPostBack == false)
                {
                    if (counter == 1)
                        fieldText = fieldText + "CHECKED ";
                }
                else
                {
                    foreach (string name in formColl)
                    {
                        if (name == id)
                        {
                            if (formColl[name] == dr.value)
                            {
                                fieldText = fieldText + "CHECKED ";
                            }
                        }
                    }

                }
                //if (label == "")
                //    label = UpdateLabelText(label);


                fieldText = fieldText + CreateIDForElement(id, true, counter);

                // Create a label
                fieldText = fieldText + CreateNameForElement(id);
                fieldText = fieldText + CreateCssClassForElement(cssclass);

                fieldText = fieldText + " value=\"" + dr.value + "\">" + dr.value + Environment.NewLine;


            }

            //fieldText += CreateLabelForRadioList(label, divText, isRequired, className);

            //fieldText = fieldText + "</label>";

            return fieldText;
        }


        private string CreateCheckboxList(int elementID, string id, string label, System.Collections.Specialized.NameValueCollection formColl, bool IsPostBack, bool isRequired, string className)
        {


            string fieldText = "";
            string divText = "", cssclass = "";
            string labelText;

            DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter evTA = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();
            DA.ElementValueTDS.DL_ElementValueDataTable evDT = new Dealer_Locator.DA.ElementValueTDS.DL_ElementValueDataTable();

            evDT = evTA.GetDataByFormElementID(elementID);


            //labelText = CreateLabelText(id);
            //fieldText = Environment.NewLine + labelText + fieldText;

            // Get ID
            id = CreateElementID(id);


            //  fieldText = CreateLabelForRadioList(label, divText) + fieldText;

            int counter = 0;
            foreach (DA.ElementValueTDS.DL_ElementValueRow dr in evDT.Rows)
            {
                counter = counter + 1;
                fieldText = fieldText + "<input type=\"checkbox\" name=\"" + id + "\" value=\"" + dr.value + "\" ";

                if (IsPostBack == false)
                {
                }
                else
                {
                    foreach (string name in formColl)
                    {
                        if (name == id)
                        {
                            if (formColl[name].Contains(dr.value) == true)
                            {
                                fieldText = fieldText + "CHECKED=\"yes\" ";
                            }
                        }
                    }

                }
                //if (label == "")
                //    label = UpdateLabelText(label);


                fieldText = fieldText + CreateIDForElement(id, true, counter);

                // Create a label
                fieldText = fieldText + CreateNameForElement(id);
                fieldText = fieldText + CreateCssClassForElement(cssclass);

                fieldText = fieldText + " />" + dr.value + "<BR>";

            }
            divText = CreateDivText(id + "div");
            // fieldText = divText + label + "</div>" + fieldText;

            //fieldText += CreateLabelForElement(label, divText, isRequired, className);

            //fieldText = fieldText + "</label>";

            return fieldText;
        }

    }
}
