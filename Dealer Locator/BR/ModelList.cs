using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;

namespace Dealer_Locator.BR
{

    public class ModelList
    {

        private List<Model> _modelList;

        public struct Model
        {
            public string ModelName;
            public int ModelID;

            public string MainCategoryName;
            public int MainCategoryID;

            public string SubCategoryName;
            public int SubCategoryID;

            public bool Disabled;
            public string ModelURL;

            public int Postion;
        }

        #region Member Variables

        public List<Model> List
        {
            get
            {
                return _modelList;
            }
        }

        #endregion

        public ModelList()
        {
            _modelList = new List<Model>();
            GetModelList();
        }

        private void GetModelList()
        {
            DA.ModelTDS.DL_ModelDataTable mdt = new Dealer_Locator.DA.ModelTDS.DL_ModelDataTable();

            DA.ModelTDSTableAdapters.DL_ModelTableAdapter mta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();

            mdt = mta.GetData();

            foreach (DA.ModelTDS.DL_ModelRow mr in mdt.Rows)
            {
                Model tempModel = new Model();

                tempModel.MainCategoryName = mr.mainCatName;
                tempModel.SubCategoryName = mr.subCatName;

                try
                {
                    tempModel.MainCategoryID = Convert.ToInt32(mr.fk_mainCatID.ToString());
                }
                catch
                {
                }

                try
                {
                    tempModel.SubCategoryID = mr.fk_subCatID;
                }
                catch
                {

                }
                try
                {
                    tempModel.ModelID = mr.pk_modelID;
                }
                catch
                {

                }

                try
                {
                    tempModel.ModelURL = mr.modelUrl;
                }
                catch
                {

                }

                try
                {
                    tempModel.ModelName = mr.modelName;
                }
                catch
                {

                }
                try
                {
                    tempModel.Postion = mr.position;
                }
                catch
                {

                }
                try
                {
                    tempModel.Disabled = mr.disable;
                }
                catch
                {

                }

                _modelList.Add(tempModel);
            }
        }

        public Model GetModel(int modelID)
        {

            foreach (Model tempModel in _modelList)
                if (tempModel.ModelID == modelID)
                    return tempModel;

            return new Model();
        }

        public bool ReAssignModel(List<int> modelIDs, int newMainID, int newSubID)
        {
            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            ldt = lta.GetData_NotSubmitted();

            string leadIDs = "";

            foreach (DA.LeadsTDS.DL_LeadRow lr in ldt.Rows)
            {

                if (leadIDs != "")
                    leadIDs += ",";

                leadIDs += "'" + lr.pk_leadID + "'";
            }


            bool IsSuccessful = true;

            string sql;

            foreach (int modelID in modelIDs)
            {
                try
                {
                    sql = "UPDATE [DL.LeadProduct] " +
                        " SET fk_MainCatID = " + newMainID.ToString() + ", " +
                        " fk_SubCatID = " + newSubID.ToString() +
                        " WHERE fk_ModelID = " + modelID +
                        " AND fk_LeadID IN (" + leadIDs + ")";

                    DA.DataAccess.Update(sql);

                    sql = "UPDATE [DL.Model] " +
                        " SET fk_subCatID = " + newSubID.ToString() + ", " +
                        " fk_mainCatID = " + newMainID.ToString() +
                        " WHERE pk_ModelID = " + modelID.ToString();

                    DA.DataAccess.Update(sql);
                }
                catch (Exception ex)
                {

                    IsSuccessful = false;
                }

            }

            return IsSuccessful;

        }

        public string GetModelName(int ModelID)
        {
            foreach (Model tempModel in _modelList)
                if (tempModel.ModelID == ModelID)
                    return tempModel.ModelName;

            return "";

        }

    }
}
