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

namespace Dealer_Locator.admin.FormDevelopment.ReOrder
{
    public partial class ReorderFormElements : System.Web.UI.Page
    {
        int _formID;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            litFormName.Text = sessionWrapper["formName"];



            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;

            //DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
            //ta.Connection = DA.DataAccess.GetDatabaseConnection();
            //DA.FormElementTDS.DL_FormElementDataTable dt = new Dealer_Locator.DA.FormElementTDS.DL_FormElementDataTable();

            //SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            //_formID = Convert.ToInt32(sessionWrapper["formID"]);

            //dt = ta.GetMultiValueDataByFormID(_formID);

            //if (Page.IsPostBack == false)
            //{
            //    lstReorder.Items.Clear();

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        ListItem temp = new ListItem();
            //        temp.Value = dr["pk_formElementID"].ToString();
            //        temp.Text = dr["ID"].ToString();

            //        lstReorder.Items.Add(temp);

            //    }
            //}
        }

        //protected void btnSaveOrder_Click(object sender, EventArgs e)
        //{
        //    int count = 0;

        //    foreach (ListItem li in lstReorder.Items)
        //    {


        //        //DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter ta = new Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter();
        //        //ta.UpdatePosition(count, Convert.ToInt32(li.Value));
        //        DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter ta = new Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter();
        //        ta.Connection = DA.DataAccess.GetDatabaseConnection();
        //        ta.UpdatePosition(count, Convert.ToInt32(li.Value));

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
        protected void lnkUrl_Click(object sender, EventArgs e)
        {
            SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            int formID = Convert.ToInt32(sessionWrapper["formID"].ToString());

            Response.Redirect("/SalesLeadForm.aspx?slID=" + formID.ToString());

        }

    }
}
