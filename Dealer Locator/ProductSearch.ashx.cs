using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

using Dealer_Locator.BR;

namespace Dealer_Locator
{
    /// <summary>
    /// Summary description for ProductSearch
    /// </summary>
    public class ProductSearch : IHttpHandler, IRequiresSessionState
    {
        private const string _productList = "PRODUCT_LIST";

        public struct Item
        {
            public List<Subcategory> categories;
        }

        public struct Subcategory
        {
            public string name;
            public int subCatId;
            public List<Product> products;
        }

        public struct Product
        {
            public string name;
            public int modelID;
            public bool selected;
        }

        public void ProcessRequest(HttpContext context)
        {
            string returnValue = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

            Item returnItem = new Item();

            string action = context.Request["action"].ToString();

            switch (action)
            {
                case "toggle":

                    int productId = Convert.ToInt32(context.Request.QueryString["productId"].ToString());
                    bool isSelected = Convert.ToBoolean(context.Request.QueryString["selected"].ToString());

                    Product returnProduct = ToggleItemStatus(productId, isSelected);
                    returnValue = js.Serialize(returnProduct);

                    break;

                case "search":

                    string searchQuery = context.Request.QueryString["q"].ToString();

                    ArrayList results = SearchForModels(searchQuery);

                    returnValue = js.Serialize(results);

                    break;

                case "GetList":

                    List<Product> productList = GetList();
                    returnValue = js.Serialize(productList);
                    break;

                case "list":
                    returnItem = ListProducts(context);
                    returnValue = js.Serialize(returnItem);

                    break;
                case "fake":
                    returnValue =js.Serialize("dock");
                    break;

                default:
                    break;
            }



            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Write(returnValue);
        }

        public static List<Product> GetList()
        {
            return ((List<Product>)HttpContext.Current.Session[_productList]);
        }

        private Product ToggleItemStatus(int productId, bool isSelected)
        {
            List<Product> productList = new List<Product>();

            if (HttpContext.Current.Session != null && HttpContext.Current.Session[_productList] != null)
            {
                productList = ((List<Product>)HttpContext.Current.Session[_productList]);
            }

            int i = 0;
            bool productFound = false;
            foreach (Product tempProduct in productList)
            {
                if (tempProduct.modelID == productId)
                {
                    productFound = true;
                    break;
                }

                i++;
            }

            Product tempProd;

            if (productFound)
            {
                tempProd = productList[i];

                tempProd.selected = isSelected;
                productList[i] = tempProd;
            }
            else
            {
                tempProd = GetProduct(productId);

                tempProd.selected = isSelected;
                tempProd.modelID = productId;
                productList.Add(tempProd);
            }

            HttpContext.Current.Session[_productList] = productList;

            return tempProd;
        }


        private Product GetProduct(int modelId)
        {
            Product returnProduct = new Product();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT model.pk_modelId, model.modelName ");
            sb.AppendLine(" FROM [DL.Model] model");
            sb.AppendLine(" WHERE pk_modelId = " + modelId.ToString());

            DataSet ds = new DataSet();

            ds = DA.DataAccess.Read(sb.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                returnProduct.name = ds.Tables[0].Rows[0]["modelName"].ToString();
            }

            returnProduct.modelID = modelId;

            return returnProduct;
        }

        private static Item ListProducts(HttpContext context)
        {
            string category = context.Request.QueryString["category"];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" SELECT subCat.pk_subCatId, subCat.categoryName, model.pk_modelId, model.modelName, 0 as rank, model.position ");
            sb.AppendLine(" FROM [DL.SubCategory] subCat ");
            sb.AppendLine(" INNER JOIN [DL.Model] model  ");
            sb.AppendLine(" 	ON model.fk_subCatId = subCat.pk_subCatId ");
            sb.AppendLine(" INNER JOIN [DL.MainCategory] mainCat  ");
            sb.AppendLine(" 	ON mainCat.pk_mainCatId = subCat.fk_mainCatId ");
            sb.AppendLine(" WHERE mainCat.categoryName = '" + category + "'");
            sb.AppendLine(" AND model.disable = 0 ");
            sb.AppendLine(" UNION ");
            sb.AppendLine(" SELECT '','',model.pk_modelId, model.modelName, 1 as rank, model.position ");
            sb.AppendLine(" FROM [dl.model] model ");
            sb.AppendLine(" INNER JOIN [DL.MainCategory] mainCat  ");
            sb.AppendLine(" 	ON mainCat.pk_mainCatId = model.fk_mainCatId ");
            sb.AppendLine(" WHERE mainCat.categoryName = '" + category + "'");
            sb.AppendLine(" AND model.fk_subCatId = -1 ");
            sb.AppendLine(" AND model.disable = 0 ");
            sb.AppendLine(" ORDER BY rank, model.position ");

            DataSet ds = new DataSet();

            ds = DA.DataAccess.Read(sb.ToString());

            ArrayList values = new ArrayList();

            Item returnItem = new Item();
            returnItem.categories = new List<Subcategory>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bool subCatFound = false;
                int subCatLocation = 0;
                Subcategory currentSubCat = new Subcategory();

                foreach (Subcategory subCat in returnItem.categories)
                {
                    if (subCat.subCatId == Convert.ToInt32(dr["pk_subCatId"].ToString()))
                    {
                        currentSubCat = subCat;
                        subCatFound = true;
                        break;
                    }

                    subCatLocation++;
                }

                if (!subCatFound)
                {
                    currentSubCat.subCatId = Convert.ToInt32(dr["pk_subCatId"].ToString());
                    currentSubCat.name = dr["categoryName"].ToString();
                    currentSubCat.products = new List<Product>();
                }

                Product newProduct = new Product();
                newProduct.name = dr["modelName"].ToString();

                newProduct.modelID = Convert.ToInt32(dr["pk_modelId"].ToString());


                List<Product> productList = new List<Product>();

                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_productList] != null)
                {
                    productList = ((List<Product>)HttpContext.Current.Session[_productList]);
                }

                foreach (Product tempProduct in productList)
                {
                    if (tempProduct.modelID == newProduct.modelID)
                    {
                        newProduct.selected = tempProduct.selected;
                        break;
                    }
                }

                currentSubCat.products.Add(newProduct);

                if (!subCatFound)
                    returnItem.categories.Add(currentSubCat);
                else
                    returnItem.categories[subCatLocation] = currentSubCat;

            }
            return returnItem;
        }

        private struct ProductSearchResults
        {
            public string value;
            public string MatchingText;
            public string MatchType;
            public string MainCategory;
            public string SubCategory;
            public string Model;
            public int ModelID;
            public bool Selected;
        }

        private ArrayList SearchForModels(string searchQuery)
        {
            string[] searchTerms = searchQuery.Split(' ');

            ArrayList searchItemList = new ArrayList();

            ModelList modelList = new ModelList();
            List<ModelList.Model> list = modelList.List;

            List<ProductSearchResults> modelResults = new List<ProductSearchResults>();
            List<ProductSearchResults> subCategoryResults = new List<ProductSearchResults>();
            List<ProductSearchResults> mainCategoryResults = new List<ProductSearchResults>();

            foreach (ModelList.Model model in list)
            {
                foreach (string term in searchTerms)
                {
                    string termUpper = term.ToUpper();

                    if (model.ModelName.ToUpper().Contains(termUpper) && model.Disabled == false)
                    {
                        ProductSearchResults psr = new ProductSearchResults();
                        psr.MatchingText = term;
                        psr.MatchType = "model";
                        psr.MainCategory = model.MainCategoryName;
                        psr.SubCategory = model.SubCategoryName;
                        psr.Model = model.ModelName;
                        psr.ModelID = model.ModelID;

                        psr.Selected = GetProduct(model.ModelID).selected;

                        psr.value = model.ModelName;

                        modelResults.Add(psr);
                    }
                }
            }

            foreach (ModelList.Model model in list)
            {
                foreach (string term in searchTerms)
                {
                    string termUpper = term.ToUpper();


                    if (model.SubCategoryName.ToUpper().Contains(termUpper) && model.Disabled == false)
                    {
                        ProductSearchResults psr = new ProductSearchResults();
                        psr.MatchingText = term;
                        psr.MatchType = "subcategory";
                        psr.MainCategory = model.MainCategoryName;
                        psr.SubCategory = model.SubCategoryName;
                        psr.Model = model.ModelName;
                        psr.ModelID = model.ModelID;

                        psr.Selected = GetProduct(model.ModelID).selected;

                        psr.value = model.ModelName;

                        subCategoryResults.Add(psr);
                    }
                }
            }

            foreach (ModelList.Model model in list)
            {
                foreach (string term in searchTerms)
                {
                    string termUpper = term.ToUpper();

                    if (model.MainCategoryName.ToUpper().Contains(termUpper) && model.Disabled == false)
                    {
                        ProductSearchResults psr = new ProductSearchResults();
                        psr.MatchingText = term;
                        psr.MatchType = "maincategory";
                        psr.MainCategory = model.MainCategoryName;
                        psr.SubCategory = model.SubCategoryName;
                        psr.Model = model.ModelName;
                        psr.ModelID = model.ModelID;

                        psr.Selected = GetProduct(model.ModelID).selected;

                        psr.value = model.ModelName;

                        mainCategoryResults.Add(psr);
                    }
                }
            }

            searchItemList.AddRange(modelResults);
            searchItemList.AddRange(subCategoryResults);
            searchItemList.AddRange(mainCategoryResults);

            return searchItemList;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}