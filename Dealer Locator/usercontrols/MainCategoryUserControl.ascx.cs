using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.usercontrols
{
    public partial class MainCategoryUserControl : System.Web.UI.UserControl
    {
        private string mainCatSelect = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            mcdt = mcta.GetData_nonDisabled();

            mainCatSelect += "<select id=\"MainCategory_htmlselect\" class=\"MainCategory-htmlselect\">";
            mainCatSelect += "<option value='-1' selected='selected' class='default-category'>Product Category</option>";

            foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
            {
                if (!(tempRow.pk_mainCatID == 5 || tempRow.pk_mainCatID == 1))
                    AddMachineToDock(tempRow);
            }

            mainCatSelect += "</select>";

            Random randNum = new Random();
            int nextNumber = randNum.Next();
            string newMenuId = "menu_" + nextNumber.ToString();

            string jQueryScript = string.Empty;

            Literal newItem = new Literal();
            newItem.Text = mainCatSelect;
            
            MainCategoryItems.Controls.Add(newItem);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "dockScript" + nextNumber, jQueryScript, false);

        }


        private void AddMachineToDock(DA.MainCategoryTDS.DL_MainCategoryRow machineRow)
        {
            if (machineRow.dockMenuImageUrlLarge.Length > 0 && machineRow.disable == false)
            {
                mainCatSelect += "<option value='" + machineRow.pk_mainCatID + "' data-imagesrc='" + machineRow.dockMenuImageUrlSmall + "'>" + machineRow.dockMenuTitle + "</option>";

                
                //MainCategoryItems.Controls.Add(newItem);
            }

        }
    }
}