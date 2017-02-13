using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class MainCategoryDockImageManagerControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            //Literal headerFormName = (Literal)Master.FindControl("litFormName");
            //headerFormName.Text = "Form: " + sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;

            if (Page.IsPostBack == false)
            {
                DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
                DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

                mcdt = mcta.GetData_nonDisabled();

                foreach (DA.MainCategoryTDS.DL_MainCategoryRow tempRow in mcdt.Rows)
                {
                    ListItem tempItem = new ListItem();
                    tempItem.Text = tempRow.categoryName;
                    tempItem.Value = tempRow.pk_mainCatID.ToString();

                    cboMainCategory.Items.Add(tempItem);
                }

                LoadDockMenuImageUrls();
            }

        }

        private void LoadDockMenuImageUrls()
        {
            DA.MainCategoryTDS.DL_MainCategoryDataTable mcdt = new Dealer_Locator.DA.MainCategoryTDS.DL_MainCategoryDataTable();
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            mcdt = mcta.GetDataByMainCategoryID(Convert.ToInt32(cboMainCategory.SelectedValue));
            
            if (mcdt.Rows.Count > 0)
            {

                DA.MainCategoryTDS.DL_MainCategoryRow mcr = (DA.MainCategoryTDS.DL_MainCategoryRow)mcdt.Rows[0];
                CurrentImagePathLarge.Text = mcr.dockMenuImageUrlLarge;
                CurrentImagePathSmall.Text = mcr.dockMenuImageUrlSmall;
                DockMenuTitle.Text = mcr.dockMenuTitle;


                if (CurrentImagePathLarge.Text.Length == 0)
                {
                    CurrentImagePathLarge.Text = "None";
                    CurrentImagePreviewLarge.Visible = false;
                }
                else
                {
                    CurrentImagePreviewLarge.ImageUrl = "~" + CurrentImagePathLarge.Text;
                    CurrentImagePreviewLarge.Visible = true;
                }

                if (CurrentImagePathSmall.Text.Length == 0)
                {
                    CurrentImagePathSmall.Text = "None";
                    CurrentImagePreviewSmall.Visible = false;
                }
                else
                {
                    CurrentImagePreviewSmall.ImageUrl = "~" + CurrentImagePathSmall.Text;
                    CurrentImagePreviewSmall.Visible = true;
                }

                

            }
            //DockMenuImageUrl.Text = dockImageUrl;

        }

        protected void UpdateDockMenuImageUrl_Click(object sender, EventArgs e)
        {
            DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter mcta = new Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter();

            int mainCategoryId = Convert.ToInt32(cboMainCategory.SelectedValue);
            //string dockImageUrl = DockMenuImageUrl.Text;

            //mta.UpdateDockMenuImageUrl(dockImageUrl, modelId);

            //lblResult.Text = "";

            // Ensure a file has been uploaded
            lblResult.Text = string.Empty;

            if (DockMenuImageUrlLargeFileUpload.HasFile && DockMenuImageUrlSmallFileUpload.HasFile)
            {

                string filePath = Server.MapPath("~/images/dock/" + DockMenuImageUrlLargeFileUpload.FileName);

                try
                {
                    DockMenuImageUrlLargeFileUpload.SaveAs(filePath);

                    lblResult.Text = "Dock Image (large) Uploaded Successfully";
                }
                catch (Exception ex)
                {
                    lblResult.Text = "An Error Occurred Uploading the Dock Image (small).  Please contact the developer with a copy of the image you were trying to upload.  Error: " + ex.Message;
                }


                filePath = Server.MapPath("~/images/dock/" + DockMenuImageUrlSmallFileUpload.FileName);

                try
                {
                    DockMenuImageUrlSmallFileUpload.SaveAs(filePath);

                    lblResult.Text += "<BR>Dock Image (small) Uploaded Successfully";
                }
                catch (Exception ex)
                {
                    lblResult.Text += "<BR>An Error Occurred Uploading the Dock Image (small).  Please contact the developer with a copy of the image you were trying to upload.  Error: " + ex.Message;
                }

                try
                {

                    mcta.UpdateDockMenuImageUrls("/images/dock/" + DockMenuImageUrlLargeFileUpload.FileName, "/images/dock/" + DockMenuImageUrlSmallFileUpload.FileName, mainCategoryId);
                    lblResult.Text += "<BR>Dock Menu Images Associated Successfully<BR>Update Successful";
                }
                catch (Exception ex)
                {
                    lblResult.Text += "<BR>An Error Occurred When Associating the Dock Images to the Model.  Please contact the developer with the images you were trying to upload and the Main Category, Sub Category, and Model names.  Error: " + ex.Message;
                }

                LoadDockMenuImageUrls();
                
                // do more stuff like read the database

                //if (result == true)
                //    lblResult.Text = "Upload Complete";
                //else
                //    lblResult.Text = "An Error Occurred.  Please contact the developer with a copy of the data you were trying to Import";
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
            }
            else
            {
                lblResult.Text = "Both Image Upload Controls must have images selected before proceeding";
            }

            try
            {
                mcta.UpdateDockMenuTitle(DockMenuTitle.Text, mainCategoryId);
                lblResult.Text += "<BR><BR>Dock Menu Item Title Updated Successfully";
            }
            catch (Exception ex)
            {
                lblResult.Text += "There was an error trying to update the dock menu item's title.  Error: " + ex.Message;
            }
        }

       

        protected void cboMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDockMenuImageUrls();
        }

    }
}