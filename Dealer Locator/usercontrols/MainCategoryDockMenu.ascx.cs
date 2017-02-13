using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.usercontrols
{
    public partial class MainCategoryDockMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            mcdt = mcta.GetData_nonDisabled();

            foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
            {
                if ( !( tempRow.pk_mainCatID == 5 || tempRow.pk_mainCatID == 1 ) )
                    AddMachineToDock(tempRow);
            }

            Random randNum = new Random();
            int nextNumber = randNum.Next();
            string newMenuId = "menu_" + nextNumber.ToString();

            MenuIdHiddenField.Value = newMenuId;
            
            //string jQueryScript = "        jQuery(document).ready(function ($) {" + Environment.NewLine +
            //"// set up the options to be used for jqDock..." + Environment.NewLine +
            //"var dockOptions =" + Environment.NewLine +
            //"{ align: 'bottom' // horizontal menu, with expansion DOWN from a fixed TOP edge" + Environment.NewLine +
            //", size: 60 //increase 'at rest' size to 60px" + Environment.NewLine +
            //", labels: 'bc'  // add labels (defaults to 'br')" + Environment.NewLine +
            //", setLabel: function (txt, i) { //set colours..." + Environment.NewLine +
            //"    return txt;" + Environment.NewLine +
            //"}" + Environment.NewLine +
            //"};" + Environment.NewLine +
            //"// ...and apply..." + Environment.NewLine +
            //"$('#" + menu.ClientID + "').jqDock(dockOptions);" + Environment.NewLine +
            //"});";


//            string jQueryScript = "<script type=\"text/javascript\">" + Environment.NewLine +
//    "$(document).ready(" + Environment.NewLine +
//    "function()" + Environment.NewLine +
//    "{" + Environment.NewLine +
//    "InitializedDockMenu_" + dock2.ClientID + "();" + Environment.NewLine + 
//"}" + Environment.NewLine +
//");" + Environment.NewLine +
//"function InitializedDockMenu_" + dock2.ClientID + "()" + Environment.NewLine +
//"{" + Environment.NewLine +
//   "$('#" + dock2.ClientID + "').Fisheye(" + Environment.NewLine +
//   "{" + Environment.NewLine +
//   "maxWidth: 60," + Environment.NewLine +
//   "items: 'a'," + Environment.NewLine +
//   "itemsText: 'span'," + Environment.NewLine +
//   "container: '.dock-container2'," + Environment.NewLine +
//   "itemWidth: 60," + Environment.NewLine +
//   "proximity: 40," + Environment.NewLine +
//   "alignment : 'left'," + Environment.NewLine +
//   "halign : 'center'," + Environment.NewLine +
//   "valign : 'bottom'" + Environment.NewLine +
//   "}" + Environment.NewLine +
//   ")" + Environment.NewLine +
//   "}" + Environment.NewLine +
//    "</script>";


            string jQueryScript = "<script type=\"text/javascript\">" + Environment.NewLine +
    "$(document).ready(" + Environment.NewLine +
    "function()" + Environment.NewLine +
    "{" + Environment.NewLine +
"}" + Environment.NewLine +
");" + Environment.NewLine +
"function InitializedDockMenu()" + Environment.NewLine +
"{" + Environment.NewLine +
   "$('.dock').Fisheye(" + Environment.NewLine +
   "{" + Environment.NewLine +
   "maxWidth: 60," + Environment.NewLine +
   "items: 'a'," + Environment.NewLine +
   "itemsText: 'span'," + Environment.NewLine +
   "container: '.dock-container2'," + Environment.NewLine +
   "itemWidth: 60," + Environment.NewLine +
   "proximity: 40," + Environment.NewLine +
   "alignment : 'left'," + Environment.NewLine +
   "halign : 'center'," + Environment.NewLine +
   "valign : 'bottom'" + Environment.NewLine +
   "}" + Environment.NewLine +
   ")" + Environment.NewLine +
   "}" + Environment.NewLine +
    "</script>";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "dockScript" + nextNumber, jQueryScript,false);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "dockScript" + nextNumber, jQueryScript, false);
        }

        private void AddMachineToDock(DA.MainCategoryTDS.DL_MainCategoryRow machineRow)
        {
            if ( machineRow.dockMenuImageUrlLarge.Length > 0 && machineRow.disable == false)
            {
                Literal newItem = new Literal();
                newItem.Text = "<a class=\"dock-item2\" href=\"javascript: SelectProductCategory('" + machineRow.categoryName + "'," + machineRow.pk_mainCatID + ");\"><span>" + machineRow.dockMenuTitle + "</span><img src=\"" + machineRow.dockMenuImageUrlLarge + "\" alt=\"" + machineRow.dockMenuTitle + "\" /></a>";
                
                Image dockItem = new Image();
                dockItem.ImageUrl = machineRow.dockMenuImageUrlSmall;
                dockItem.AlternateText = machineRow.dockMenuImageUrlLarge;
                dockItem.Attributes.Add("title", machineRow.dockMenuTitle);

                DockMenuItems.Controls.Add(newItem);
            }

        }

        public static string GetUniqueId()
        {
            return HttpContext.Current.Session["MenuId"].ToString();
        }

        public string DockMenuId
        {
            get
            {
                return dock2.ClientID;
            }
        }
    }
}