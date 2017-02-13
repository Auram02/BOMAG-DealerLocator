using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.usercontrols
{
    public partial class WebUserControl1 : System.Web.UI.UserControl
    {
        string mainCatSelect = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            mcdt = mcta.GetData_nonDisabled();

            mainCatSelect += "<ul id=\"MainCategoryCarousel\" class=\"MainCategoryCarousel\">";
            

            foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
            {
                if (!(tempRow.pk_mainCatID == 5 || tempRow.pk_mainCatID == 1))
                    AddMachineToDock(tempRow);
            }

            mainCatSelect += "</ul>";

            Random randNum = new Random();
            int nextNumber = randNum.Next();

            string jQueryScript = string.Empty;

            Literal newItem = new Literal();
            newItem.Text = mainCatSelect;

            MainCategoryItems.Controls.Add(newItem);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "carouselScript" + nextNumber, jQueryScript, false);

        }


        private void AddMachineToDock(DA.MainCategoryTDS.DL_MainCategoryRow machineRow)
        {
            if (machineRow.dockMenuImageUrlLarge.Length > 0 && machineRow.disable == false)
            {
                mainCatSelect += "<li class='carousel-item-wrapper'><a class=\"dock-item2\" href=\"javascript: SelectProductCategory('" + machineRow.categoryName + "'," + machineRow.pk_mainCatID + ",'" + machineRow.dockMenuImageUrlLarge + "');\"><img src='" + machineRow.dockMenuImageUrlLarge + "' width='140' height='140' /><div class='carousel-item-title'>" + machineRow.dockMenuTitle + "</div></a></li>";
        
                //MainCategoryItems.Controls.Add(newItem);
            }

        }
    }
}