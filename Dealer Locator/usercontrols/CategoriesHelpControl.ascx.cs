using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace Dealer_Locator.usercontrols
{
    public partial class CategoriesHelpControl : System.Web.UI.UserControl
    {
        StringBuilder sb;
        StringBuilder css;
        StringBuilder cssLinks;
        StringBuilder cssHeadersActive;
        StringBuilder cssHeadersInActive;

        protected void Page_Load(object sender, EventArgs e)
        {
            sb = new StringBuilder();
            css = new StringBuilder();
            cssLinks = new StringBuilder();
            cssHeadersActive = new StringBuilder();
            cssHeadersInActive = new StringBuilder();

            StringBuilder sql = new StringBuilder();
            //sql.AppendLine(" select mc.categoryName as [MainCategoryName], sc.categoryName AS [SubCategoryName], model.modelName, modelUrl from [dl.maincategory] mc");
            //sql.AppendLine(" INNER JOIN [dl.subcategory] sc ON mc.pk_mainCatID = sc.fk_mainCatID");
            //sql.AppendLine(" INNER JOIN [dl.Model] model on model.fk_subCatID = sc.pk_subCatID");
            //sql.AppendLine(" order by mc.position, sc.position, model.position");

            sql.AppendLine(" SELECT mc.categoryName as [MainCategoryName],");
            sql.AppendLine(" sc.categoryName AS [SubCategoryName], model.modelName, modelUrl");
            sql.AppendLine(" FROM [DL.Model] model");
            sql.AppendLine(" LEFT OUTER JOIN [DL.SubCategory] sc ON model.fk_subCatID = sc.pk_subCatID");
            sql.AppendLine(" INNER JOIN [DL.MainCategory] mc ON mc.pk_mainCatID = model.fk_mainCatID ");
            sql.AppendLine(" WHERE mc.disable = 0");
            sql.AppendLine(" AND mc.pk_mainCatID NOT IN (5, 1) ");
            sql.AppendLine(" AND (sc.disable is null or sc.disable = 0)");
            sql.AppendLine(" AND (model.disable = 0)");
            sql.AppendLine(" ORDER BY mc.position, sc.position, model.position");

            DataSet ds = DA.DataAccess.Read(sql.ToString());

            string mainCatName = string.Empty;
            string subCatName = string.Empty;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                if (mainCatName != dr["MainCategoryName"].ToString())
                {
                    if (mainCatName.Length > 0)
                    {
                        CloseSubCategory();  // close previous subcategory before the next main opens
                        CloseMenu();  // close previous menu item before the next one opens
                    }


                    AddMainCategory(dr["MainCategoryName"].ToString());

                    AddSubCategory(dr["SubCategoryName"].ToString(), dr["MainCategoryName"].ToString());
                }
                else if (subCatName != dr["SubCategoryName"].ToString())
                {
                    
                        CloseSubCategory();
                    

                    AddSubCategory(dr["SubCategoryName"].ToString(), dr["MainCategoryName"].ToString());
                }

                AddModel(dr["modelName"].ToString(), dr["modelUrl"].ToString());

                mainCatName = dr["MainCategoryName"].ToString();
                subCatName = dr["SubCategoryName"].ToString();

            }

            CloseMenu();

            Literal menu = new Literal();
            menu.Text = sb.ToString();

            ProductCategoryHelp.Controls.Add(menu);

            Literal cssLit = new Literal();
            cssLit.Text = "<style type=\"text/css\">";
            cssLit.Text += css.ToString();
            cssLit.Text += "{     border: 0px; background:url('/images/producthelp/help-bar.png') no-repeat scroll 0px 3px transparent; text-indent: 18px; }";
            cssLit.Text += cssLinks.ToString();
            cssLit.Text += "{ color: #477ec1; }";
            cssLit.Text += cssHeadersActive.ToString();
            cssLit.Text += "{     background-image: url('/images/producthelp/down-arrow-help.png'); top: 11px; left: 2px; }";
            cssLit.Text += cssHeadersInActive.ToString();
            cssLit.Text += "{     background-image: url('/images/producthelp/up-arrow-help.png'); top: 11px; left: 2px; }";
            cssLit.Text += "</style>";

            ProductCategoryCSS.Controls.Add(cssLit);
        }

        private void AddMainCategory(string headingText)
        {
            headingText = headingText.Replace("/", "_");
            string divId = "ProductHeading_" + headingText.Replace(" ", "");

            sb.AppendLine("<div id=\"" + divId + "\" class=\"ProductTypesAccordionHeading\">");
            sb.AppendLine("<a href=\"#" + headingText + "\">" + headingText + "</a>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div>");

            if (css.ToString().Length > 0)
            {
                css.Append(", ");
                cssLinks.Append(", ");
            }

            if (cssHeadersActive.ToString().Length > 0)
            {
                cssHeadersActive.Append(", ");
                cssHeadersInActive.Append(", ");
            }

            css.Append("#" + divId);
            cssLinks.Append("#" + divId + " A:link");
            cssHeadersActive.Append("#" + divId + " .accordion-arrow-active");
            cssHeadersInActive.Append("#" + divId + " .accordion-arrow-inactive");

        }

        private void AddSubCategory(string subCategory, string mainCategory)
        {
            if (subCategory.Length == 0)
                subCategory = mainCategory;

            sb.AppendLine("<ul><li class=\"SubCategoryListItem\">" + subCategory + "</li><ul>");
        }

        private void CloseSubCategory()
        {
            sb.AppendLine("</ul></ul>");
        }

        private void AddModel(string modelName, string modelUrl)
        {
            //sb.AppendLine("<div class=\"AdminLink\">");
            sb.AppendLine("<li class=\"ModelListItem\">");
            sb.AppendLine("<a href=\"" + modelUrl + "\" target=\"_blank\">" + modelName + "</a>");
            sb.AppendLine("</li>");
            //sb.AppendLine("</div>");
        }

        private void CloseMenu()
        {
            sb.AppendLine("</div>");
        }
    }
}