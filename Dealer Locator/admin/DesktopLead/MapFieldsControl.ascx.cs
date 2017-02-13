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

using System.Collections.Generic;

namespace Dealer_Locator.admin.DesktopLead
{
    public partial class MapFieldsControl : System.Web.UI.UserControl
    {

        BR.Mapping FieldMapping;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblAddMapError.Text = "";
            lblSaveAndProcessError.Text = "";

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

                FieldMapping = new Dealer_Locator.BR.Mapping();


                DA.MapTDS.DL_MapReadMethodDataTable mrmdt = new Dealer_Locator.DA.MapTDS.DL_MapReadMethodDataTable();
                DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter mrmta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapReadMethodTableAdapter();

                mrmdt = mrmta.GetData();

                cboReadMethod.Items.Clear();

                foreach (DA.MapTDS.DL_MapReadMethodRow dr in mrmdt.Rows)
                {
                    ListItem temp = new ListItem();

                    temp.Text = dr.MethodName;
                    temp.Value = dr.pk_mapReadMethod.ToString();

                    cboReadMethod.Items.Add(temp);
                }

                tblMapFieldSource.Visible = false;
                tblMapFields.Visible = false;
                tblMappedFields.Visible = false;

            }

            try
            {
                if (FieldMapping == null)
                {
                    int WorkingMapID = Convert.ToInt32(Session["WorkingMapID"]);
                    FieldMapping = new Dealer_Locator.BR.Mapping(WorkingMapID);
                }
            }
            catch { }


        }


        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[3].Visible = false;
            }
            catch
            { }
        }

        protected void gvCurrentMappings_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
            }
            catch
            { }
        }


        protected void gvCurrentMappings_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            int fieldMapID = Convert.ToInt32(gvCurrentMappings.Rows[rowIndex].Cells[0].Text);

            if (e.CommandName.Equals("removeMapping"))
            {

                // remove the mapping
                FieldMapping.RemoveMapping(fieldMapID);

                gvCurrentMappings.DataBind();

            }


        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            int mapID = -1;

            mapID = Convert.ToInt32(GridView1.Rows[rowIndex].Cells[1].Text);


            if (e.CommandName.Equals("deleteMap"))
            {


                // Do the map delete here
                bool Result = BR.Mapping.DeleteMap(mapID);

                if (Result == false)
                {
                    lblSetActiveFailure.Text = "There was an error deleting the map.  Please contact the developer.";
                }

                GridView1.DataBind();

            }

            if (e.CommandName.Equals("selectFields"))
            {
                FieldMapping = new Dealer_Locator.BR.Mapping(mapID);


                int counter = 0;
                foreach (ListItem li in cboReadMethod.Items)
                {
                    if (li.Text == FieldMapping.ReadMethodName)
                    {
                        cboReadMethod.SelectedIndex = counter;
                        break;
                    }
                    counter += 1;
                }

                foreach (BR.Mapping.FieldMap fieldMap in FieldMapping.MapFields)
                {

                }

                try
                {
                    Session.Remove("WorkingMapID");
                    Session.Add("WorkingMapID", mapID);
                }
                catch
                {
                    Session.Add("WorkingMapID", mapID);
                }

                gvCurrentMappings.DataBind();

                tblMapFieldSource.Visible = true;

            }

            if (e.CommandName.Equals("setActive"))
            {
                bool IsSuccessful = false;


                IsSuccessful = BR.Mapping.SetActive(mapID);

                if (IsSuccessful == false)
                {
                    lblSetActiveFailure.Text = "There was an error setting this map active.  Please contact the developer.";
                }

                GridView1.DataBind();
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // hit
        }

        protected void txtScan_TextChanged(object sender, EventArgs e)
        {

        }

        protected void cboSalesLeadForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int formID = Convert.ToInt32(cboSalesLeadForm.SelectedItem.Value.ToString());

            DA.FormElementTDS.DL_FormElementDataTable fedt = new Dealer_Locator.DA.FormElementTDS.DL_FormElementDataTable();
            DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter feta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();

            fedt = feta.GetAllFormElementsBySLFormID(formID);

            cboSalesLeadFields.Items.Clear();

            // add static fields
            List<string> staticSaleLeadFields = new List<string>();
            staticSaleLeadFields.Add("txtFirstName");
            staticSaleLeadFields.Add("txtLastName");
            staticSaleLeadFields.Add("txtCompany_Agency");
            staticSaleLeadFields.Add("txtAddress1");
            staticSaleLeadFields.Add("txtAddress2");
            staticSaleLeadFields.Add("txtZip2");

            staticSaleLeadFields.Add("txtOtherCity");
            staticSaleLeadFields.Add("txtOtherStateProvince");
            staticSaleLeadFields.Add("txtOtherPostalCode");
            staticSaleLeadFields.Add("txtOtherCountry");

            //staticSaleLeadFields.Add("cboCityName");
            //staticSaleLeadFields.Add("txtState");

            staticSaleLeadFields.Add("txtPhone");
            staticSaleLeadFields.Add("txtFax");
            staticSaleLeadFields.Add("txtEmail");

            staticSaleLeadFields.Add("txtComments");





            foreach (string staticField in staticSaleLeadFields)
            {
                ListItem tempItem = new ListItem();

                tempItem.Text = staticField;
                tempItem.Value = staticField;

                cboSalesLeadFields.Items.Add(tempItem);

            }


            foreach (DA.FormElementTDS.DL_FormElementRow dr in fedt.Rows)
            {
                //dr.ID

                ListItem temp = new ListItem();
                temp.Text = dr.ID;
                temp.Value = dr.ID;

                cboSalesLeadFields.Items.Add(temp);
            }

            tblMapFields.Visible = true;
            tblMappedFields.Visible = true;

        }

        protected void btnSaveAndProcess_Click(object sender, EventArgs e)
        {

            try
            {
                DA.MapTDSTableAdapters.DL_MapTableAdapter mta = new Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter();

                mta.UpdateMapReadMethod(Convert.ToInt32(cboReadMethod.SelectedItem.Value), FieldMapping.MapID);

                string ReadMethod = cboReadMethod.SelectedItem.Text;
                string CardText = txtScan.Text;

                // send off to be processed
                List<string> cardItems = new List<string>();

                cardItems = FieldMapping.ProcessMap(ReadMethod, CardText);

                int counter = 0;

                foreach (string item in cardItems)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = "(" + counter + ") - " + item;
                    tempItem.Value = counter.ToString();

                    cboCardFields.Items.Add(tempItem);

                    counter += 1;
                }

                tblMapFields.Visible = true;
                tblMappedFields.Visible = true;

            }
            catch (Exception ex)
            {
                lblSaveAndProcessError.Text = "An error occurred while saving and processing the scanned card sample.  Please ensure you have selected a valid card Reading Method above." +
                    Environment.NewLine + Environment.NewLine + ex.Message;
            }

        }

        protected void btnAddMap_Click(object sender, EventArgs e)
        {
            try
            {
                string FieldName = cboSalesLeadFields.SelectedItem.Value;
                string CardPosition = cboCardFields.SelectedItem.Value;

                FieldMapping.AddMapping(FieldName, Convert.ToInt32(CardPosition));
                gvCurrentMappings.DataBind();

            }
            catch (Exception ex)
            {
                lblAddMapError.Text = "An error occurred while adding the mapping.  Please ensure that you have selected both a Form Field and a Card Field before adding a mapping." + Environment.NewLine +
                    Environment.NewLine + ex.Message;
            }

        }

        protected void btnAddMapping_Click(object sender, EventArgs e)
        {
            string NewMapName = txtAddMapping.Text;

            if (NewMapName != "")
            {
                try
                {
                    int NewMapID = BR.Mapping.AddNewMap(NewMapName);

                    if (NewMapID > -1)
                    {

                        GridView1.DataBind();
                    }
                    else
                    {
                        lblCreateNewMapError.Text = "There was an error while trying to create the map '" + NewMapName + "'.  Please contact the developer with this map name.";
                    }

                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                lblCreateNewMapError.Text = "Please specify a name for the new map before trying to add it.";
            }
        }

    
    }
}