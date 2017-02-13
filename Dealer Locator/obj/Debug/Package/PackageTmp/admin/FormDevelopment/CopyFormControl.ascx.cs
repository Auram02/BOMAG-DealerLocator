using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class CopyFormControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable sldt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

                sldt = slta.GetData();

                cboSalesLeadForm.Items.Clear();

                foreach (DA.SalesLeadFormTDS.DL_SalesLeadFormRow dr in sldt.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = dr.FormName;
                    li.Value = dr.pk_SLFormID.ToString();

                    cboSalesLeadForm.Items.Add(li);
                }



            }



        }

        protected void btnCopyForm_Click(object sender, EventArgs e)
        {
            if (cboSalesLeadForm.SelectedIndex > -1)
            {
                string NewFormName = txtNewFormName.Text;
                int oldFormID = Convert.ToInt32(cboSalesLeadForm.SelectedItem.Value);


                DoCopyForm(oldFormID, NewFormName);

            }
        }

        private void DoCopyForm(int formID, string NewFormName)
        {

            try
            {
                int nextFormID = DA.DataAccess.GetNextID("[DL.SalesLeadForm]", "pk_SLFormID");

                DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable sldt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

                sldt = slta.GetDataByFormID(formID);



                if (sldt[0].IsthanksUrlNull() == true)
                    sldt[0].thanksUrl = " ";

                if (sldt[0].IsCreatedByNull() == true)
                    sldt[0].CreatedBy = " ";


                slta.Insert(nextFormID, sldt[0].fk_groupID, sldt[0].fk_headerID, sldt[0].fk_footerID, sldt[0].thanksUrl, NewFormName, sldt[0].ZipLocator, sldt[0].CreatedBy);

                DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
                DA.FormElementTDS.DL_FormElementDataTable dt = new Dealer_Locator.DA.FormElementTDS.DL_FormElementDataTable();

                dt = ta.GetAllFormElementsBySLFormID(formID);

                //ArrayList fieldIDs = new ArrayList();

                Hashtable fieldIDHashTable = new Hashtable();

                foreach (DA.FormElementTDS.DL_FormElementRow dr in dt)
                {
                    int nextFormElementID = DA.DataAccess.GetNextID("[DL.FormElement]", "pk_formElementID");

                    //fieldIDs.Add(dr.pk_formElementID);

                    fieldIDHashTable.Add(dr.pk_formElementID, nextFormElementID);

                    if (dr.IsIDNull() == true)
                        dr.ID = " ";

                    if (dr.IslabelNull() == true)
                        dr.label = " ";

                    if (dr.IsTextNull() == true)
                        dr.Text = " ";

                    if (dr.IsCssClassNull() == true)
                        dr.CssClass = " ";

                    ta.Insert(nextFormElementID, nextFormID, dr.ID, dr.label, dr.Text, dr.required, dr.CssClass, dr.position, dr.fk_typeID);


                }

                DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter evTA = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();

                //foreach (int formElementID in fieldIDs)
                foreach (System.Collections.DictionaryEntry dicEnt in fieldIDHashTable)
                {
                    int oldID = Convert.ToInt32(dicEnt.Key.ToString());
                    int newID = Convert.ToInt32(dicEnt.Value.ToString());  // get the new form element's id

                    DA.ElementValueTDS.DL_ElementValueDataTable evDT = new Dealer_Locator.DA.ElementValueTDS.DL_ElementValueDataTable();

                    // get all element values for this form element
                    evDT = evTA.GetDataByFormElementID(oldID);

                    // for each value found, add it to the table again with a new id
                    foreach (DA.ElementValueTDS.DL_ElementValueRow dr in evDT.Rows)
                    {
                        int nextElementValueID = DA.DataAccess.GetNextID("[DL.ElementValue]", "pk_valueID");

                        if (dr.IsvalueNull() == true)
                            dr.value = " ";

                        evTA.Insert(nextElementValueID, dr.fk_typeID, dr.value.ToString(), newID, dr.position);
                    }

                }

                // success!
                litResult.Text = "Form Copied Successfully into new Form '" + NewFormName + "'";

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                litResult.Text = "An error occurred during the copy.  Please consult the developer with the following error message." + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            // import complete

        }
    }
}