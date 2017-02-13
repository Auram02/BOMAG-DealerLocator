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
    public partial class ProductModels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            //Literal headerFormName = (Literal)Master.FindControl("litFormName");
            //headerFormName.Text = "Form: " + sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;

            if (Page.IsPostBack == false)
            {
                DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();


                DA.SubCategoryTDS2.DL_SubCategoryDataTable scdt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();
                DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter scta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();

                mcdt = mcta.GetData_nonDisabled();

                foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_mainCatID.ToString();

                    cboMainCategory.Items.Add(tempItem);
                }

                scdt = scta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue));

                ListItem tempItem2 = new ListItem();
                tempItem2.Text = "-None-";
                tempItem2.Value = "-1";
                cboSubCategory.Items.Add(tempItem2);

                foreach (DA.SubCategoryTDS2.DL_SubCategoryRow tempRow in scdt.Rows)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_subCatID.ToString();

                    cboSubCategory.Items.Add(tempItem);
                }

            }

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // updated the indexes
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[4].Visible = false;

            }
            catch
            {

            }
        }



        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            //DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter ta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            //int nextID = DA.DataAccess.GetNextID("[DL.SubCategory]", "pk_subCatID");
            //int mainID = Convert.ToInt32(cboMainCategory.SelectedValue);
            //int nextPosition = DA.DataAccess.GetNextID("[DL.SubCategory]", "position");

            //ta.Insert(nextID, mainID, "*NewSubCategory*", false, nextPosition);
            //GridView1.DataBind();


            DA.ModelTDSTableAdapters.DL_ModelTableAdapter ta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();

            if (cboMainCategory.SelectedIndex > -1)
            {

                int subCatID;

                if (cboSubCategory.SelectedIndex == -1)
                {
                    subCatID = -1;
                }
                else
                {
                    subCatID = Convert.ToInt32(cboSubCategory.SelectedValue);
                }

                    int nextModelID = DA.DataAccess.GetNextID("[DL.Model]", "pk_modelID");
                int nextPositionID = DA.DataAccess.GetNextID("[DL.Model]", "position");
                int mainCatID = Convert.ToInt32(cboMainCategory.SelectedValue);

                ta.InsertQuery(nextModelID, subCatID, "*New Model*", false, nextPositionID, "", mainCatID);
                GridView1.DataBind();
            }
        }

        void Selection_Change(Object sender, EventArgs e)
        {

            // Set the background color for days in the Calendar control
            // based on the value selected by the user from the 
            // DropDownList control.

            //FakeSubCategory_SelectedIndexChanged(sender, e);


            GridView1.DataBind();
        }

        protected void cboMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DA.SubCategoryTDS2.DL_SubCategoryDataTable scdt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();
            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter scta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            scdt = scta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue));

            cboSubCategory.Items.Clear();

            ListItem tempItem2 = new ListItem();
            tempItem2.Text = "-None-";
            tempItem2.Value = "-1";
            cboSubCategory.Items.Add(tempItem2);


            foreach (DA.SubCategoryTDS2.DL_SubCategoryRow tempRow in scdt.Rows)
            {
                ListItem tempItem = new ListItem();
                tempItem.Text = tempRow.categoryName;
                tempItem.Value = tempRow.pk_subCatID.ToString();

                cboSubCategory.Items.Add(tempItem);
            }


            GridView1.DataBind();
        }


    }
}
