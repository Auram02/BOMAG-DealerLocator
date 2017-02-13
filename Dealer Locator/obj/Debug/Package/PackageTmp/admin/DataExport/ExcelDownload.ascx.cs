using System;
using System.Data;
using System.Configuration;
using System.Collections;

using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Dealer_Locator.admin.DataExport
{
    public partial class ExcelDownload : System.Web.UI.UserControl
    {
        private string _excelDownloadType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {

                ListItem calItem = new ListItem();
                calItem.Text = "Any";
                calItem.Value = "Any";

                cboUseDateRange.Items.Add(calItem);

                calItem = new ListItem();
                calItem.Text = "Specify";
                calItem.Value = "Specify";

                cboUseDateRange.Items.Add(calItem);

                CalendarEntryStart.Disabled = true;
                CalendarEntryEnd.Disabled = true;

                CalendarStartButton.Visible = false;
                CalendarEndButton.Visible = false;

                DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();


                DA.SubCategoryTDS2.DL_SubCategoryDataTable scdt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();
                DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter scta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();

                mcdt = mcta.GetData_nonDisabled();

                foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_mainCatID.ToString();

                    cboMainCategory.Items.Add(tempItem);
                }

                scdt = scta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue));

                ListItem tempItem2 = new ListItem();
                tempItem2.Text = "-None-";
                tempItem2.Value = "-1";

                cboSubCategory.Items.Add(tempItem2);
                cboModel.Items.Add(tempItem2);


                foreach (DA.SubCategoryTDS2.DL_SubCategoryRow tempRow in scdt.Rows)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_subCatID.ToString();

                    cboSubCategory.Items.Add(tempItem);
                }

            }

        }

        public string ExcelDownloadType
        {
            get { return _excelDownloadType; }
            set { _excelDownloadType = value; }
        }

        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";

            string dateRange = "";

            string mainSubCategoryID = "";

            string leadIDs = "";
            Hashtable idHash = new Hashtable();

            Hashtable idHash2 = new Hashtable();

            Hashtable leadValueHash = new Hashtable();

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            if (CalendarEntryStart.Disabled == false)
            {
                DateTime startDate = new DateTime();
                startDate = Convert.ToDateTime(StartDate.Text);

                DateTime endDate = new DateTime();
                endDate = Convert.ToDateTime(EndDate.Text);

                ldt = lta.GetDataBySubmitDate(startDate, endDate);

            }
            else
            {
                ldt = lta.GetData();

            }


            foreach (DA.LeadsTDS.DL_LeadRow dr in ldt.Rows)
            {
                if (idHash.ContainsKey(dr.pk_leadID) == false)
                {
                    idHash.Add(dr.pk_leadID, dr.pk_leadID);

                }
            }


            DA.LeadsTDS.DL_LeadProductDataTable lpdt = new Dealer_Locator.DA.LeadsTDS.DL_LeadProductDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter lpta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter();

            int modelID = Convert.ToInt32(cboModel.SelectedItem.Value.ToString());
            int mainCatID = Convert.ToInt32(cboMainCategory.SelectedItem.Value);
            int subCatID = Convert.ToInt32(cboSubCategory.SelectedItem.Value);
            string mainCatName, subCatName, modelName;

            modelName = cboModel.SelectedItem.Text;
            subCatName = cboSubCategory.SelectedItem.Text;
            mainCatName = cboMainCategory.SelectedItem.Text;
            bool IsModelReport = false;


            if (cboModel.SelectedValue != "-1")
            {
                // Model report
                lpdt = lpta.GetDataByModelID(modelID);
                IsModelReport = true;
            }
            else
            {
                // main category/subcategory report
                lpdt = lpta.GetDataByMainSub(mainCatID, subCatID);
            }

            foreach (DA.LeadsTDS.DL_LeadProductRow dr in lpdt.Rows)
            {
                if (idHash.ContainsKey(dr.fk_leadID) == true && idHash2.ContainsKey(dr.fk_leadID) == false)
                    idHash2.Add(dr.fk_leadID, dr.fk_leadID);

            }

            // should have all lead id's collected now
            // get the leadvalues submitted for each of these leads.
            List<BR.Lead> myArrList = new List<Dealer_Locator.BR.Lead>();



            int leadValueCount = 0;
            ArrayList headerArray = new ArrayList();

            foreach (int key in idHash2.Keys)
            {
                BR.Lead tempLead = new Dealer_Locator.BR.Lead(key);
                myArrList.Add(tempLead);

                foreach (BR.Lead.LeadValue leadVal in tempLead._leadValues)
                {
                    if (leadValueHash.ContainsKey(leadVal.elementName.ToUpper()) == false)
                    {
                        // "index", columnName;
                        leadValueHash.Add(leadVal.elementName.ToUpper(), leadValueCount);

                        headerArray.Add(leadVal.elementName.ToUpper());
                        leadValueCount += 1;
                    }
                }
            }

            // add extra columns
            if (IsModelReport)
            {
                //valueCollection.Add("Model");
                leadValueHash.Add("MODEL", leadValueCount);

                headerArray.Add("MODEL");


            }
            else
            {
                //   valueCollection.Add("MainCategory");
                leadValueHash.Add("MAINCATEGORY", leadValueCount);
                headerArray.Add("MAINCATEGORY");

                leadValueCount += 1;


                leadValueHash.Add("SUBCATEGORY", leadValueCount);
                headerArray.Add("SUBCATEGORY");

                // valueCollection.Add("SubCategory");
            }

            int counter = 0;

            ArrayList arrayCollection = new ArrayList();


            foreach (BR.Lead tempLead in myArrList)
            {
                ArrayList valueCollection = new ArrayList();

                if (counter == 0)
                {
                    // add headers
                    foreach (string str in headerArray)
                    {
                        valueCollection.Add(str.ToUpper());
                    }

                    arrayCollection.Add(valueCollection);
                }

                // clear it incase it was used
                valueCollection = new ArrayList();

                int colCounter = 0;
                int getIndex = 1;

                foreach (string str in headerArray)
                {
                    // blank values
                    valueCollection.Add("");
                }

                foreach (BR.Lead.LeadValue leadVal in tempLead._leadValues)
                {

                    getIndex = Convert.ToInt32(leadValueHash[leadVal.elementName.ToUpper()].ToString());

                    valueCollection[getIndex] = leadVal.elementValue.ToString();

                    //valueCollection.Add(leadVal.elementValue);

                    //colCounter += 1;
                }

                // add extra columns
                if (IsModelReport)
                {
                    //valueCollection.Add(modelName);

                    getIndex = Convert.ToInt32(leadValueHash["MODEL"].ToString());

                    valueCollection[getIndex] = modelName;
                }
                else
                {
                    //valueCollection.Add(mainCatName);
                    getIndex = Convert.ToInt32(leadValueHash["MAINCATEGORY"].ToString());

                    valueCollection[getIndex] = mainCatName;

                    //valueCollection.Add(subCatName);
                    getIndex = Convert.ToInt32(leadValueHash["SUBCATEGORY"].ToString());

                    valueCollection[getIndex] = subCatName;

                }

                arrayCollection.Add(valueCollection);

                counter += 1;
            }

            string pathName = Server.MapPath("~/admin/DataExport/Data/");

            string fileName = "DownloadsExcel_" + BR.Utility.GenerateRandomCode() + ".xls";
            pathName = pathName + fileName;

            BR.ExcelReport xlsReport = new Dealer_Locator.BR.ExcelReport(pathName, arrayCollection);

            string errorMessage = "";

            errorMessage = xlsReport.GenerateSelf();

            if (errorMessage != "")
                lblResult.Text = errorMessage;
            else
                lblResult.Text = "Download: <a href=\"Data/" + fileName + "\">" + fileName + "</a>";

            lblResult.Visible = true;



        }

        protected void cboMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DA.SubCategoryTDS2.DL_SubCategoryDataTable scdt = new Dealer_Locator.DA.SubCategoryTDS2.DL_SubCategoryDataTable();
            DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter scta = new Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter();
            scdt = scta.GetDataByMainCatID(Convert.ToInt32(cboMainCategory.SelectedValue));

            cboSubCategory.Items.Clear();

            ListItem tempItem2 = new ListItem();
            tempItem2.Text = "-None-";
            tempItem2.Value = "-1";
            cboSubCategory.Items.Add(tempItem2);


            foreach (DA.SubCategoryTDS2.DL_SubCategoryRow tempRow in scdt.Rows)
            {
                ListItem tempItem = new ListItem();
                tempItem.Text = tempRow.categoryName;
                tempItem.Value = tempRow.pk_subCatID.ToString();

                cboSubCategory.Items.Add(tempItem);
            }

        }

        protected void cboUseDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUseDateRange.SelectedItem.Value == "Specify")
            {
                CalendarEntryStart.Disabled = false;
                CalendarEntryEnd.Disabled = false;

                CalendarStartButton.Visible = true;
                CalendarEndButton.Visible = true;
            }
            else
            {
                CalendarEntryStart.Disabled = true;
                CalendarEntryEnd.Disabled = true;

                CalendarStartButton.Visible = false;
                CalendarEndButton.Visible = false;
            }

        }

        protected void cboSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DA.ModelTDS.DL_ModelDataTable mdt = new Dealer_Locator.DA.ModelTDS.DL_ModelDataTable();
            DA.ModelTDSTableAdapters.DL_ModelTableAdapter mta = new Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter();

            mdt = mta.GetDataByMainCatID_SubCatID(Convert.ToInt32(cboSubCategory.SelectedItem.Value), Convert.ToInt32(cboMainCategory.SelectedItem.Value));

            ListItem li = new ListItem();
            li.Value = "-1";
            li.Text = "-None-";
            cboModel.Items.Clear();
            cboModel.Items.Add(li);

            foreach (DA.ModelTDS.DL_ModelRow dr in mdt.Rows)
            {
                ListItem li2 = new ListItem();
                li2.Text = dr.modelName;
                li2.Value = dr.pk_modelID.ToString();

                cboModel.Items.Add(li2);
            }

        }


    }
}