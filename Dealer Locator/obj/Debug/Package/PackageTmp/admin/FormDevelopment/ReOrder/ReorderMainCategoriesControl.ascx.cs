using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Admin
{
    public partial class ReorderMainCategoriesControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Page.IsPostBack == false)
            {

                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter ta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
                ta.Connection = DA.DataAccess.GetDatabaseConnection();

                DA.MainCategoryTDS.DL_MainCategoryDataTable dt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
                dt = ta.GetData_nonDisabled();

                //lstReorder.Items.Clear();

                //ReorderList1.DataSource = dt;
                //ReorderList1.DataBind();

                //foreach (DataRow dr in dt.Rows)
                //{
                //    //ListItem temp = new ListItem();

                //    AjaxControlToolkit.ReorderListItem temp = new AjaxControlToolkit.ReorderListItem(0);
                //    temp.

                //    temp.Value = dr["pk_mainCatID"].ToString();
                //    temp.Text = dr["categoryName"].ToString();

                //    //lstReorder.Items.Add(temp);
                //    roList.Items.Add(temp);
                //}

            }
        }

        //protected void btnSaveOrder_Click(object sender, EventArgs e)
        //{
        //    int count = 0;

        //    foreach (ListItem li in lstReorder.Items)
        //    {
        //        DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter ta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();
        //        ta.UpdatePosition_2(count, Convert.ToInt32(li.Value));

        //        count++;
        //    }
        //}

        //protected void btnUp_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (lstReorder.SelectedIndex > 0)
        //    {
        //        int curIndex = lstReorder.SelectedIndex;
        //        ListItem li = lstReorder.SelectedItem;

        //        lstReorder.Items.RemoveAt(curIndex);
        //        lstReorder.Items.Insert(curIndex - 1, li);

        //    }
        //}

        //protected void btnDown_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (lstReorder.SelectedIndex < (lstReorder.Items.Count - 1))
        //    {
        //        int curIndex = lstReorder.SelectedIndex;
        //        ListItem li = lstReorder.SelectedItem;

        //        lstReorder.Items.RemoveAt(curIndex);
        //        lstReorder.Items.Insert(curIndex + 1, li);

        //    }
        //}
    }
}