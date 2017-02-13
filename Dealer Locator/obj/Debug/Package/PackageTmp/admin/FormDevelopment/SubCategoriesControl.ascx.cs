using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class SubCategoriesControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            //Literal headerFormName = (Literal)Master.FindControl("litFormName");
            //headerFormName.Text = "Form: " + sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;
        }


        protected void GridViewInsert(object sender, EventArgs e)
        {
            //   SqlDataSource1.Insert();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            // Get the country for the row being edited. For this example, the
            // country is contained in the seventh column (index 6). 


            string keyID = GridView1.Rows[e.NewEditIndex].Cells[0].Text;

            // For this example, cancel the edit operation if the user attempts
            // to edit the record of a customer from the Unites States. 


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

        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            // Return the Grid View to it's viewing mode

            // Iterate through the NewValues collection and HTML encode all 
            // user-provided values before updating the data source.

        }



        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter ta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            int nextID = DA.DataAccess.GetNextID("[DL.SubCategory]", "pk_subCatID");
            int mainID = Convert.ToInt32(cboMainCategory.SelectedValue);
            int nextPosition = DA.DataAccess.GetNextID("[DL.SubCategory]", "position");

            ta.Insert(nextID, mainID, "*New Sub-Category*", false, nextPosition);
            GridView1.DataBind();

        }
    }
}