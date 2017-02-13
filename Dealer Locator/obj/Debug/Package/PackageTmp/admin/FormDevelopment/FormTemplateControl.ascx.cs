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
using ParameterPasser;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class FormTemplateControl : System.Web.UI.UserControl
    {
        bool isZipLocator;
        //12346



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                // If "edit" on the querystring has SL, that means they selected sales lead templates.  Otherwise, ZL = ZipLocator template
                // This will tell the listbox on this form which forms to display for add,edit, delete
                if (Request.QueryString["edit"] == "SL")
                {
                    Session.Add("IsZipLocator", false);

                }
                else if (Request.QueryString["edit"] == "ZL")
                {
                    Session.Add("IsZipLocator", true);
                }
            }

            isZipLocator = Convert.ToBoolean(Session["IsZipLocator"]);

        }


        //protected void btnEditFormName_Click(object sender, EventArgs e)
        //{
        //    if (lstFormNames.SelectedIndex > -1)
        //    {
        //        txtFormName.Text = lstFormNames.SelectedItem.Text;
        //        txtFormID.Text = lstFormNames.SelectedValue;
        //        btnModify.Text = "Update";
        //        lblFormStatus.Text = "Update Existing Form";
        //    }

        //}

        //protected void btnModify_Click(object sender, EventArgs e)
        //{

        //    //DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slfta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

        //    //string formName = txtFormName.Text;
        //    //int formID;

        //    //if (txtFormID.Text == "")
        //    //{


        //    //    int nextSalesLeadFormID = DA.DataAccess.GetNextID("[DL.SalesLeadForm]","pk_SLFormID");
        //    //    int groupID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["groupID"]);

        //    //    slfta.Insert(nextSalesLeadFormID, groupID, 0, 0, "", formName,isZipLocator);
        //    //}
        //    //else
        //    //{
        //    //    // update
        //    //     formID = Convert.ToInt32(txtFormID.Text);
        //    //    slfta.UpdateFormName(formName, formID);
        //    //}

        //    //Response.Redirect("~/admin/FormDevelopment/FormTemplate.aspx");
        //    //txtFormName.Text = "";
        //    //txtFormID.Text = "";
        //    //btnModify.Text = "Add";
        //    //lblFormStatus.Text = "Add New Form";

        //    //deleteForm.Visible = false;
        //    //newForm.Visible = true;
        //    //listForm.Visible = true;
        //}

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (lstFormNames.SelectedIndex > -1)
        //    {
        //        txtFormID_delete.Text = lstFormNames.SelectedValue;
        //        lblFormName_delete.Text = lstFormNames.SelectedItem.Text;

        //        deleteForm.Visible = true;
        //        newForm.Visible = false;
        //        listForm.Visible = false;
        //    }
        //}

        //protected void btnEditDetails_Click(object sender, EventArgs e)
        //{
        //    // edit
        //    if (lstFormNames.SelectedIndex > -1)
        //    {

        //        // Pass textbox values to ReceiveSession.aspx
        //        SessionParameterPasser sessionWrapper = new SessionParameterPasser("Dashboard.aspx");

        //        // Add some values
        //        sessionWrapper["formID"] = lstFormNames.SelectedValue.ToString();
        //        sessionWrapper["formName"] = lstFormNames.SelectedItem.Text;

        //        sessionWrapper.PassParameters();
        //    }
        //}

        //protected void btnConfirmDelete_Click(object sender, EventArgs e)
        //{
        //    int formID = Convert.ToInt32(txtFormID_delete.Text);
        //    DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slfta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();
        //    slfta.DeleteAllData(formID);

        //    txtFormID_delete.Text = "";
        //    lblFormName_delete.Text = "";

        //    deleteForm.Visible = false;
        //    newForm.Visible = true;
        //    listForm.Visible = true;

        //    Response.Redirect("~/admin/FormDevelopment/FormTemplate.aspx?edit=SL");
        //}

        //protected void btnCancelDelete_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/admin/FormDevelopment/FormTemplate.aspx?edit=SL");
        //}

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slfta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

            int formID;

            int nextSalesLeadFormID = DA.DataAccess.GetNextID("[DL.SalesLeadForm]", "pk_SLFormID");
            int groupID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["groupID"]);

            slfta.Insert(nextSalesLeadFormID, groupID, 0, 0, "", "*New Form Name*", isZipLocator, Request.Cookies["Authenticated"].Values["email"]);

            Response.Redirect("~/admin/FormDevelopment/FormTemplate.aspx?edit=SL");
        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("editData"))
            {

                // Pass textbox values to ReceiveSession.aspx
                SessionParameterPasser sessionWrapper = new SessionParameterPasser("FormElements.aspx");

                // Add some values
                sessionWrapper["formID"] = GridView1.Rows[rowIndex].Cells[4].Text;
                sessionWrapper["formName"] = GridView1.Rows[rowIndex].Cells[0].Text;

                sessionWrapper.PassParameters();
            }

            if (e.CommandName.Equals("deleteForm"))
            {

                string formID, formName;

                formID = GridView1.Rows[rowIndex].Cells[4].Text;
                formName = GridView1.Rows[rowIndex].Cells[0].Text;

                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slfta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

                slfta.DeleteQuery(Convert.ToInt32(formID));

                // refresh screen
                Response.Redirect("~/admin/FormDevelopment/FormTemplate.aspx?edit=SL");
            }

        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // updated the indexes
                e.Row.Cells[1].Visible = false;  // hide thanks url
                e.Row.Cells[2].Enabled = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;

            }
            catch
            {

            }
        }


    }
}