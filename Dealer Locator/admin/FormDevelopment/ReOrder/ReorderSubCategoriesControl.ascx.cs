using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Dealer_Locator.admin.FormDevelopment.ReOrder
{
    public partial class ReorderSubCategoriesControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter ta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
            DA.MainCategoryTDS.DL_MainCategoryDataTable dt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
            dt = ta.GetData();

            if (Page.IsPostBack == false)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ListItem temp = new ListItem();
                    temp.Text = dr["categoryName"].ToString();
                    temp.Value = dr["pk_mainCatID"].ToString();

                    cboMainCategory.Items.Add(temp);

                }
            }

        }
    }
}