using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Dealer_Locator.admin.FormDevelopment.ReOrder
{
    public partial class ReorderModelsControl : System.Web.UI.UserControl
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

                }

                GetSubCategories();
            }
        }

        private void GetSubCategories()
        {
            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter ta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            DA.SubCategoryTDS2.DL_SubCategoryDataTable dt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();

            dt = ta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue.ToString()));


            cboSubCategory.Items.Clear();

            ListItem temp2 = new ListItem();
            temp2.Value = "-1";
            temp2.Text = "-None-";

            cboSubCategory.Items.Add(temp2);


            foreach (DataRow dr in dt.Rows)
            {
                ListItem temp = new ListItem();
                temp.Value = dr["pk_subCatID"].ToString();
                temp.Text = dr["categoryName"].ToString();

                cboSubCategory.Items.Add(temp);
            }

        }

        protected void cboMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubCategories();
        }
    }
}