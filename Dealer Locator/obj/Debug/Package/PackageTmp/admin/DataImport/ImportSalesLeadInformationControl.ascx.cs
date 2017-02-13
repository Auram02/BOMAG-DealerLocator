using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Dealer_Locator.admin.DataImport.Data
{
    public partial class ImportSalesLeadInformationControl : System.Web.UI.UserControl
    {
        private static string excelFilePath;
        private static BR.SalesLeadImport slImport;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                tblDownloadExcel.Visible = false;
                tblImportErrors.Visible = false;
                tblUploadContainer.Visible = false;
                lblResult.Visible = false;
                gvImportErrors.Visible = false;
                trImporting.Visible = false;


                DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable sldt = new Dealer_Locator.DA.SalesLeadFormTDS.DL_SalesLeadFormDataTable();
                DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter slta = new Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter();

                try
                {
                    sldt = slta.GetDataByGroupID(Convert.ToInt32(Request.Cookies["Authenticated"].Values["groupID"].ToString()), false);

                }
                catch
                {
                    Response.Redirect("~/admin/login.aspx");

                }


                ListItem liTemp2 = new ListItem();
                liTemp2.Text = "--Select Form--";
                liTemp2.Value = "-1";
                cboSalesLeadForm.Items.Add(liTemp2);

                foreach (DA.SalesLeadFormTDS.DL_SalesLeadFormRow slrow in sldt.Rows)
                {
                    ListItem liTemp = new ListItem();
                    liTemp.Text = slrow.FormName;
                    liTemp.Value = slrow.pk_SLFormID.ToString();
                    cboSalesLeadForm.Items.Add(liTemp);
                }

            }
        }

        protected void cboSalesLeadForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblUploadContainer.Visible = false;
            litDownloadURL.Text = "";
            tblImportErrors.Visible = false;
            lblResult.Text = "";
            gvImportErrors.DataSource = null;

            if (Convert.ToInt32(cboSalesLeadForm.SelectedValue) > -1)
            {
                tblDownloadExcel.Visible = true;



            }
            else
            {
                tblDownloadExcel.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile == true)
            {
                tblImportErrors.Visible = true;
                lblResult.Visible = true;
                lblResult.Text = "";
                gvImportErrors.DataSource = null;

                gvImportErrors.Visible = true;

                trImporting.Visible = true;
                string outputURL = Server.MapPath("~/admin/DataImport/Data/" + "upload_" + FileUpload1.FileName);
                FileUpload1.SaveAs(outputURL);

                slImport.outputURL = outputURL;

                bool hasErrors = false;

                slImport.Read();

                DataTable dt = slImport.errorRows;

                // more than 0 rows in error table
                if (dt.Rows.Count > 0)
                {

                    // gvImportErrors.ShowHeader = false;

                    DataRow headerRow = dt.NewRow();


                    gvImportErrors.DataSource = dt;
                    gvImportErrors.DataBind();

                    int counter = 0;
                    foreach (string name in slImport.elementNames)
                    {
                        gvImportErrors.HeaderRow.Cells[counter].Text = name;
                        counter += 1;
                    }

                    lblResult.Text = "The following rows in the excel file had incorrect model names or location information.  Please correct these data and re-upload the excel file.";

                }
                else
                {
                    // process the import.
                    int rowsProcessed = slImport.ProcessLeads();

                    if (rowsProcessed != slImport.dt.Rows.Count)
                    {
                        // error occurred
                        lblResult.Text = "An error occurred during the upload.  The first " + rowsProcessed + " rows were imported successfully." + Environment.NewLine + "--Please record this number and send it plus the excel file you were importing to the developer.";
                    }
                    else
                    {
                        // ok!
                        lblResult.Text = "All leads imported successfully";
                    }
                }


            }


            trImporting.Visible = false;

        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            int formID = Convert.ToInt32(cboSalesLeadForm.SelectedItem.Value);
            string formName = cboSalesLeadForm.SelectedItem.Text;

            System.Random ranNumber = new Random();
            int nextRandom = ranNumber.Next(100, 10000);

            formName = formName.Replace("*", "");
            formName = formName.Replace(" ", "_");

            //string url = @"admin/DataImport/Data/" + this._formName + nextRandom.ToString() + ".xls";
            excelFilePath = Server.MapPath("~/admin/DesktopLead/Data/") + formName + "_" + nextRandom.ToString() + ".csv";


            slImport = new Dealer_Locator.BR.SalesLeadImport(formID, formName, excelFilePath);


            string returnURL = slImport.CreateImportTemplate();

            lblResult.Visible = true;
            lblResult.Text = returnURL;

            string curRequest = Page.Request.Url.ToString();
            curRequest = curRequest.Substring(0, curRequest.LastIndexOf("/")) + "/";

            curRequest = curRequest + "Data/" + formName + "_" + nextRandom.ToString() + ".csv";

            litDownloadURL.Text = "Download: <a href=\"" + curRequest + "\">" + curRequest + "</a>";

            tblUploadContainer.Visible = true;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();


            System.Data.OleDb.OleDbConnection msConnectString = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + excelFilePath + "; Extended Properties=Excel 8.0;");

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", msConnectString);

            da.Fill(ds);

            //ds.ReadXml(excelFilePath);

            gvImportErrors.DataSource = ds;
            gvImportErrors.DataBind();


            //foreach (DataRow dr in ds.Tables["Data"].Rows)
            //    lblResult.Text = lblResult.Text + dr[0] + Environment.NewLine;

            lblResult.Visible = true;


            tblImportErrors.Visible = true;
            gvImportErrors.Visible = true;
        }
    }
}