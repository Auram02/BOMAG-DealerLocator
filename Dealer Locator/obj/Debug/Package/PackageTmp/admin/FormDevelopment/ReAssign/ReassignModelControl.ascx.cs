using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Dealer_Locator.admin.FormDevelopment.ReAssign
{
    public partial class ReassignModelControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter ta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                DA.MainCategoryTDS.DL_MainCategoryDataTable dt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();

                dt = ta.GetData();

                foreach (DataRow dr in dt.Rows)
                {
                    ListItem temp = new ListItem();
                    temp.Text = dr["categoryName"].ToString();
                    temp.Value = dr["pk_mainCatID"].ToString();

                    cboMainCategory.Items.Add(temp);
                    cboNewMainCategory.Items.Add(temp);
                }

                GetSubCategories();
            }
        }

        private void GetSubCategories()
        {
            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter ta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            DA.SubCategoryTDS2.DL_SubCategoryDataTable dt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();
            DA.SubCategoryTDS2.DL_SubCategoryDataTable dt2 = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();

            dt = ta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue.ToString()));
            dt2 = ta.GetDataByMainCatID(Convert.ToInt32(cboNewMainCategory.SelectedValue.ToString()));


            cboSubCategory.Items.Clear();
            cboNewSubCategory.Items.Clear();

            ListItem temp2 = new ListItem();
            temp2.Value = "-1";
            temp2.Text = "-None-";

            cboSubCategory.Items.Add(temp2);
            cboNewSubCategory.Items.Add(temp2);

            foreach (DataRow dr in dt.Rows)
            {
                ListItem temp = new ListItem();
                temp.Value = dr["pk_subCatID"].ToString();
                temp.Text = dr["categoryName"].ToString();

                cboSubCategory.Items.Add(temp);
            }

            foreach (DataRow dr in dt2.Rows)
            {
                ListItem temp = new ListItem();
                temp.Value = dr["pk_subCatID"].ToString();
                temp.Text = dr["categoryName"].ToString();

                cboNewSubCategory.Items.Add(temp);
            }

        }

        protected void cboMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubCategories();
        }

        protected void btnReassign_Click(object sender, EventArgs e)
        {

            int oldMainID = -1;
            int newMainID = -1;
            int oldSubID = -1;
            int newSubID = -1;

            List<int> modelIDs = new List<int>();

            oldMainID = Convert.ToInt32(cboMainCategory.SelectedItem.Value.ToString());
            newMainID = Convert.ToInt32(cboNewMainCategory.SelectedItem.Value.ToString());

            oldSubID = Convert.ToInt32(cboSubCategory.SelectedItem.Value.ToString());
            newSubID = Convert.ToInt32(cboNewSubCategory.SelectedItem.Value.ToString());

            for (int i = 0; i < lstModels.Items.Count; i++)
            {
                if (lstModels.Items[i].Selected == true)
                {
                    // add to list
                    modelIDs.Add(Convert.ToInt32(lstModels.Items[i].Value.ToString()));
                }
            }

            // process models
            BR.ModelList ml = new Dealer_Locator.BR.ModelList();

            bool result = ml.ReAssignModel(modelIDs, newMainID, newSubID);

            if (result == true)
            {
                lblResult.Text = "The models have been ReAssigned successfully!  They will now appear in the new sub category if it is selected on the left.  If you made a mistake with the ReAssignment you may still select the correct new Main and SubCategories on the right while the models are still checked and hit the \"ReAssign Model\" button again.";
            }
            else
            {

                lblResult.Text = "An error occurred while ReAssigning the models.  Please contact the developer with the details of the ReAssignment you were attempting to make.";
            }

            // refresh!

        }

    }
}