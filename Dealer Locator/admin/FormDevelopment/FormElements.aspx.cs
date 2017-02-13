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
    public partial class FormElements : System.Web.UI.Page
    {
        private static int _formElementID;
        private static int _FormID;
        private static int _typeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            litFormName.Text = sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;

            SqlDataSource1.ConnectionString = DA.DataAccess.GetConnectionString();
            SqlDataSource2.ConnectionString = DA.DataAccess.GetConnectionString();
        }

        protected void lnkUrl_Click(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            int formID = Convert.ToInt32(sessionWrapper["formID"].ToString());

            Response.Redirect("/SalesLeadForm.aspx?slID=" + formID.ToString());

        }

        protected void gvElements_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool isAdmin = Convert.ToBoolean(Request.Cookies["Authenticated"]["Admin"].ToString());

            try
            {

                //e.Row.Cells[1].Visible = false;
                // updated the indexes
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;

                e.Row.Cells[8].Visible = isAdmin;  // show this if they are an admin.  otherwise, hide

                lnkAddNewMultiValue.Visible = false;  // hide multivalue add new link

                if (e.Row.RowIndex == gvElements.Rows.Count - 1)
                {
                    
                }
                
            }
            catch
            {

            }
        }

        protected void dvDetails_ItemUpdated(Object sender, DetailsViewUpdatedEventArgs e)
        {

            gvElements.DataBind(); 
            dvDetails.DataBind();
        }

        protected void dvDetails_ItemUpdating(Object sender, DetailsViewUpdateEventArgs e)
        {
            int newValue = Convert.ToInt32(((DropDownList)((DetailsView)sender).FindControl("dlTypeID")).SelectedValue);
            e.NewValues["fk_typeID"] = newValue;
        }

        protected void dvDetails_modechanged(object sender, EventArgs e)
        {
            if (dvDetails.CurrentMode == DetailsViewMode.Edit || dvDetails.CurrentMode == DetailsViewMode.ReadOnly)
            {
                GetDetailViewIDs(sender);  // get the ids if possible
            }


        }

        private void GetDetailViewIDs(object sender)
        {
            try
            {

                if (((DetailsView)sender).Rows.Count > 0)
                {
                    string rowVal = ((DetailsView)sender).Rows[0].Cells[1].Text;
                    string elementID = ((DetailsView)sender).Rows[1].Cells[1].Text;
                   
                    _formElementID = Convert.ToInt32(rowVal);
                    _FormID = Convert.ToInt32(elementID);

                    string sql;
                    sql = "SELECT pk_typeID FROM [DL.ElementType], [DL.FormElement] WHERE pk_typeID = fk_typeID AND [DL.FormElement].pk_formElementID = " + _formElementID.ToString();
                    DataSet ds;

                    ds = DA.DataAccess.Read(sql);
                    string typeID = ds.Tables[0].Rows[0][0].ToString();
                    _typeID = Convert.ToInt32(typeID);

                }

            }
            catch
            {

            }
        }

        protected void dvDetails_OnItemDatabound(object sender, EventArgs e)
        {
            try
            {
                // updated the indexes
                ((DetailsView)sender).Rows[0].Visible = false;
                ((DetailsView)sender).Rows[1].Visible = false;
                ((DetailsView)sender).Rows[5].Visible = false;
                ((DetailsView)sender).Rows[7].Visible = false;

                if (Convert.ToBoolean(Request.Cookies["Authenticated"]["Admin"]) == true)
                    ((DetailsView)sender).Rows[5].Visible = true;
            }
            catch
            {
            }

            GetDetailViewIDs(sender);

            lnkAddNewMultiValue.Visible = false;  // hide multivalue add new link

            try {

                if (((DetailsView)sender).Rows.Count > 0)
                {

                    ((DropDownList)((DetailsView)sender).FindControl("dlTypeID")).SelectedValue = _typeID.ToString();

                    // manage if the field type is multivalue
                    ManageEditValues(sender);


                }

            }
            catch
            {

            }
        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("deleteForm"))
            {

                string elementID = "";

                elementID = gvElements.Rows[rowIndex].Cells[6].Text;

                DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter feta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
                feta.DeleteQuery(Convert.ToInt32(elementID));

                Response.Redirect("~/admin/FormDevelopment/FormElements.aspx");


            }

        }


        private void ManageEditValues(object sender)
        {
            bool isMultiValueType;

            DA.FieldElementTypeTDSTableAdapters.DL_ElementTypeTableAdapter ta = new Dealer_Locator.DA.FieldElementTypeTDSTableAdapters.DL_ElementTypeTableAdapter();
            DA.FieldElementTypeTDS.DL_ElementTypeDataTable dt = new Dealer_Locator.DA.FieldElementTypeTDS.DL_ElementTypeDataTable();
            dt = ta.GetDataByTypeID(_typeID);

            

            if (Convert.ToBoolean(dt.Rows[0]["isMultiValue"]) == true)
            {
                isMultiValueType = true;
            }
            else
            {
                isMultiValueType = false;
            }


            ((LinkButton)((DetailsView)sender).FindControl("lblEditValues")).Visible = isMultiValueType;
            gvElementValues_tableCell.Visible = isMultiValueType;
            gvMultiValueList.Visible = false;
            lnkAddNewMultiValue.Visible = false;
        }

        protected void lblEditValues_onClick(object sender, EventArgs e)
        {
            // do stuff
            SqlDataSource sqds = new SqlDataSource();
            //sqds.ConnectionString = ConfigurationManager.ConnectionStrings["Dev"].ConnectionString;
            //sqds.SelectCommand = "SELECT * FROM [DL.ElementValue] WHERE fk_formElementID = " + _formElementID;
            //sqds.UpdateCommand = "UPDATE [DL.ElementValue] SET [value] = @Value WHERE pk_valueID = @pk_valueID";
            //sqds.UpdateParameters.Add("value", TypeCode.String, "-1");
            //sqds.UpdateParameters.Add("pk_valueID", TypeCode.String, "-1");


            //gvMultiValueList.AutoGenerateEditButton = true;
            //gvMultiValueList.DataSource = sqds;
            //gvMultiValueList.DataBind();
            gvMultiValueList.Visible = true;
            lnkAddNewMultiValue.Visible = true;


        }

        protected void gvMultiValueList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //updated the indexes
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;

            }
            catch { }

            // maybe add databoundcolumns to datagrid on page load.  that might alleviate it.
        }

        protected void gvMultiValueList_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            int pk_valueID = Convert.ToInt32(e.Values[0].ToString());
         
            DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter ta = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();
            ta.DeleteQuery(pk_valueID);

            gvMultiValueList.DataBind();
                
        }



        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            

            DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
            


            int nextElementID;
            int nextPositionID;

            nextElementID = DA.DataAccess.GetNextID("[DL.FormElement]", "pk_formElementID");
            nextPositionID = DA.DataAccess.GetNextID("[DL.FormElement]", "position");

            ta.Insert(nextElementID, Convert.ToInt32(Session["formID"].ToString()), "New Element Name", "", "None", false, "", nextPositionID, 1);

            gvElements.DataBind();
            dvDetails.DataBind();

        }

        protected void lnkAddNewMultiValue_Click(object sender, EventArgs e)
        {



            DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter ta = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();

            int nextElementID;
            int nextPositionID;

            nextElementID = DA.DataAccess.GetNextID("[DL.ElementValue]", "pk_valueID");
            nextPositionID = DA.DataAccess.GetNextID("[DL.ElementValue]", "[position]");

            ta.Insert(nextElementID, _typeID, "New Value", _formElementID, nextPositionID);

            gvMultiValueList.DataBind();

        }
    }
}
