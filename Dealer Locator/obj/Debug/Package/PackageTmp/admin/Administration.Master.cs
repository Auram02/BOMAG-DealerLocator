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

using System.Web.Services;

using AjaxControlToolkit;

namespace Dealer_Locator.admin
{
    public partial class Administration : System.Web.UI.MasterPage
    {
        private const string _ACCORDION_INDEX = "ACCORDION_INDEX";

        protected virtual void OnPreRenderComplete(EventArgs e)
        {
        }

        public static string GetHiddenTabIndex()
        {
            return "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.PreRender += new EventHandler(Page_PreRender);

            bool IsValid = true;


            if (!Page.IsPostBack)
            {
                theHiddenTabIndex1.Value = "0";
                theHiddenTabIndex2.Value = "0";
                theHiddenTabIndex3.Value = "0";
            }
            else
            {
                if (HttpContext.Current.Session["TabIndex1"] != null && HttpContext.Current.Session["TabIndex1"].ToString() != null)
                    theHiddenTabIndex1.Value = HttpContext.Current.Session["TabIndex1"].ToString();

                if (HttpContext.Current.Session["TabIndex2"] != null && HttpContext.Current.Session["TabIndex2"].ToString() != null)
                    theHiddenTabIndex2.Value = HttpContext.Current.Session["TabIndex2"].ToString();

                if (HttpContext.Current.Session["TabIndex3"] != null && HttpContext.Current.Session["TabIndex3"].ToString() != null)
                    theHiddenTabIndex3.Value = HttpContext.Current.Session["TabIndex3"].ToString();
            }

            string activate = "activate: function() { var selectedTab = $('#{0}').tabs('option', 'selected'); $(\"#{1}\").val(selectedTab);}";
            string baseScript = "$('#tabs').tabs({ selected: {0},{1}});";


            string activate1 = "selected: $(#" + theHiddenTabIndex1.ClientID + ").val(), activate: function() { var selectedTab = $('#tabs').tabs('option', 'selected'); $(\"#" + theHiddenTabIndex1.ClientID + "\").val(selectedTab);}";
            string activate2 = "selected: $(#" + theHiddenTabIndex2.ClientID + ").val(), activate: function() { var selectedTab = $('#tabs2').tabs('option', 'selected'); $(\"#" + theHiddenTabIndex2.ClientID + "\").val(selectedTab);}";
            string activate3 = "selected: $(#" + theHiddenTabIndex3.ClientID + ").val(), activate: function() { var selectedTab = $('#tabs3').tabs('option', 'selected'); $(\"#" + theHiddenTabIndex3.ClientID + "\").val(selectedTab);}";
            //string activate1 = String.Format(activate, "tabs", theHiddenTabIndex1.ClientID);
            //string activate2 = String.Format(activate, "tabs2", theHiddenTabIndex2.ClientID);
            //string activate3 = String.Format(activate, "tabs3", theHiddenTabIndex3.ClientID);

            string tab1 = "$('#tabs').tabs({ " + activate1 + " });";
            string tab2 = "$('#tabs2').tabs({" + activate2 + " });";
            string tab3 = "$('#tabs3').tabs({ " + activate3 + "});";


            tab1 = "ActivateTabs('tabs','" + theHiddenTabIndex1.ClientID + "');";
            tab2 = "ActivateTabs('tabs2','" + theHiddenTabIndex2.ClientID + "');";
            tab3 = "ActivateTabs('tabs3','" + theHiddenTabIndex3.ClientID + "');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "selecttab", tab1, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "selecttab2", tab2, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "selecttab3", tab3, true);

            //for (int i = 0; i < AdminAccordianMenu.Panes.Count; i++)
            //{

                

            //    //foreach (Control item in AdminAccordianMenu.Panes[i].Controls)
            //    //{
            //    //    if (item is AccordionContentPanel)
            //    //    {
                        
            //    //    }
            //    //}
                
            //}

            if (Session[_ACCORDION_INDEX] == null)
            {

                //AdminAccordianMenu.SelectedIndex = 0;

            }
            else
            {

                //AdminAccordianMenu.SelectedIndex = Convert.ToInt32(Session[_ACCORDION_INDEX]);

            }


            try
            {

                if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsLoggedIn"].ToString()) == true)
                {

                    BuildLinks();

                    //if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["admin"].ToString()) == true)
                    //{

                    //    if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsRep"].ToString()) == false)
                    //    {
                    //        //this.td_TMReports.Visible = false;
                    //        this.td_TMReportsLink.Visible = false;
                    //    }

                    //}
                    //else
                    //{
                    //    RemoveStandardLinks();

                    //    if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsRep"].ToString()) == true)
                    //    {

                    //        // enable territory rep links
                    //        //this.td_TMReports.Visible = true;
                    //        this.td_TMReportsLink.Visible = true;
                    //    }

                    //    // Check if the user has security rights to be on this page.  If not, redirect
                    //    IsValid = ValidatePermissions();

                    //    if (IsValid == false)
                    //        Response.Redirect("/admin/Default.aspx");

                    //}


                    if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["admin"].ToString()) == false)
                    {
                        IsValid = ValidatePermissions();

                        accordion_admin.Visible = false;
                        accordion_TM.Visible = true;

                        if (IsValid == false)
                            Response.Redirect("/admin/Default.aspx");
                    }
                    else
                    {
                        accordion_admin.Visible = true;
                        accordion_TM.Visible = false;

//                        AdministrationAccordionHeading.Visible = false;
//ManageDataAccordionHeading.Visible = false;
//ManageLeadsAccordionHeading.Visible = false;
//ToolsAccordionHeading.Visible = false;
//ManageDesktopAccordionHeading.Visible = false;

                    }



                }
                else
                {
                    Response.Redirect("/admin/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                if (IsValid == false)
                    Response.Redirect("/admin/Default.aspx?errID=1");
                else
                    Response.Redirect("/admin/Login.aspx");
            }


            try
            {

                // if the groupID is null, but we are accessing a protected page, assume cookies have been set
                if (Session["groupID"] == null)
                    Session.Add("groupID", Request.Cookies["Authenticated"].Values["groupID"].ToString());

            }
            catch (Exception ex)
            {

            }

        }

        void Page_PreRender(object sender, EventArgs e)
        {
        //    string fileName = System.IO.Path.GetFileName(Request.PhysicalPath);
        //    fileName = fileName.Substring(0, fileName.IndexOf("."));

        //    int paneIndex = 0;
        //    AccordionPaneCollection panes = AdminAccordianMenu.Panes;
        //    foreach (AccordionPane accordionPane in panes)
        //    {
        //        foreach (Control paneControl in accordionPane.Controls)
        //        {
        //            if (paneControl is AccordionContentPanel)
        //            {
        //                foreach (Control link in (paneControl as AccordionContentPanel).Controls)
        //                {
        //                    if (link is System.Web.UI.HtmlControls.HtmlGenericControl)
        //                    {
        //                        if ((link as System.Web.UI.HtmlControls.HtmlContainerControl).InnerText.Contains(fileName))
        //                        {
        //                            (link as System.Web.UI.HtmlControls.HtmlContainerControl).Attributes.Remove("class");
        //                            (link as System.Web.UI.HtmlControls.HtmlContainerControl).Attributes.Add("class", "SelectedLink");
        //                            AdminAccordianMenu.SelectedIndex = paneIndex;
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //        paneIndex += 1;
        //    }
        }

        [WebMethod]
        public static void StoreAccordionIndex(int index)
        {
            HttpContext.Current.Session[_ACCORDION_INDEX] = index;
        }

        public bool IsAdmin
        {
            get
            {
                return Convert.ToBoolean(Request.Cookies["Authenticated"].Values["admin"].ToString());
            }
        }
        public bool IsRep
        {
            get
            {
                return Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsRep"].ToString());
            }
        }

        private void BuildLinks()
        {

            if (IsAdmin)
            {
                // Removed for new menu TMReportsDiv.Visible = false;
                TMReportsDiv.Visible = false;
            }
            else
            {

                AccountSetupDiv.Visible = false;
                DistributionSetupDiv.Visible = false;

                //ImportExportPane.Visible = false;
                //DevelopmentPane.Visible = false;
                //DesktopLeadPane.Visible = false;


                // Removed for new menu  GeneralReportsDiv.Visible = false;
                // Removed for new menu  PendingLeadsReportDiv.Visible = false;
                // Removed for new menu   ErrorReportsDiv.Visible = false;
                FaxBulletinsDiv.Visible = false;

                // Removed for new menu  if (!IsRep)
                // Removed for new menu     TMReportsDiv.Visible = false;
                if (!IsRep)
                    TMReportsDiv.Visible = false;

            }

        }

        private AccordionPane BuildAccordionPane(string url)
        {
            AccordionPane pane = new AccordionPane();


            return pane;
        }



        private bool ValidatePermissions()
        {
            HttpContext context = HttpContext.Current;
            string currentUrl = context.Request.RawUrl;

            ArrayList adminOnlyPages = new ArrayList();

            adminOnlyPages.Add("Users.aspx");
            adminOnlyPages.Add("Groups.aspx");
            adminOnlyPages.Add("Vendor.aspx");
            adminOnlyPages.Add("ImportDealerInformation.aspx");
            adminOnlyPages.Add("ZipCodeImport.aspx");
            adminOnlyPages.Add("DataExport");
            adminOnlyPages.Add("ReorderElementValues");
            adminOnlyPages.Add("ReorderFormElements");
            adminOnlyPages.Add("ReorderMainCategories");
            adminOnlyPages.Add("ReorderProductModels");
            adminOnlyPages.Add("ReorderSubCategories");
            adminOnlyPages.Add("Dashboard.aspx");
            adminOnlyPages.Add("DashboardCategories.aspx");
            adminOnlyPages.Add("FormElements.aspx");
            adminOnlyPages.Add("FormTemplate.aspx");
            adminOnlyPages.Add("ProductModels.aspx");
            adminOnlyPages.Add("SubCategories.aspx");
            adminOnlyPages.Add("ThanksURL.aspx");
            adminOnlyPages.Add("DistributorCityReport.aspx");
            adminOnlyPages.Add("DistributorZipReport.aspx");

            // Reassign
            adminOnlyPages.Add("ReassignModels.aspx");

            adminOnlyPages.Add("DefaultSalesLeadForm.aspx");
            adminOnlyPages.Add("DoNotPlanToBuySetup.aspx");
            adminOnlyPages.Add("RegionManagement.aspx");
            adminOnlyPages.Add("RepMap.aspx");
            adminOnlyPages.Add("SalesLeadVendorEmailEditor.aspx");
            adminOnlyPages.Add("ExcelDownloads.aspx");
            adminOnlyPages.Add("ImportSalesLeadInformation.aspx");
            adminOnlyPages.Add("CopyForm.aspx");
            adminOnlyPages.Add("GenerateFormUrls.aspx");
            adminOnlyPages.Add("HeaderFooter.aspx");
            adminOnlyPages.Add("GeneralTMReports.aspx");
            adminOnlyPages.Add("LatLongReport.aspx");
            adminOnlyPages.Add("TerritoryManagerLeadsByDealer.aspx");
            adminOnlyPages.Add("TerritoryManagerReport.aspx");


            adminOnlyPages.Add("TMReportPasser.aspx");
            if (Convert.ToBoolean(Request.Cookies["Authenticated"].Values["IsRep"].ToString()) == false)
            {
                adminOnlyPages.Add("TMReportsDashboard.aspx");

            }

            adminOnlyPages.Add("ViewLead.aspx");
            adminOnlyPages.Add("ViewLeadList.aspx");
            adminOnlyPages.Add("LeadsBySource.aspx");
            adminOnlyPages.Add("FaxBulletin.aspx");

            adminOnlyPages.Add("RemoveLead.aspx");

            // Desktop Leads
            adminOnlyPages.Add("TransferLeads.aspx");
            adminOnlyPages.Add("MapFields.aspx");
            adminOnlyPages.Add("SyncDatabases.aspx");
            adminOnlyPages.Add("ModifyLeadSearch.aspx");
            adminOnlyPages.Add("ModifyLeadData.aspx");


            adminOnlyPages.Add("ViewPendingLeadList.aspx");  // pending lead list report


            // adminOnlyPages.Add("User.aspx");  // allow since they are not admins but can still modify their own account



            bool IsValid = true;

            foreach (string pageName in adminOnlyPages)
            {
                if (pageName.Contains(currentUrl))
                    IsValid = false;
            }

            return IsValid;

        }

        protected void Page_Unload(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Abandon session and log the user out
            Request.Cookies["Authenticated"].Values["IsLoggedIn"] = "false";
            HttpCookie adminCookie = new HttpCookie("Authenticated", Request.Cookies["Authenticated"].Values["admin"].ToString());
            adminCookie.Values["IsLoggedIn"] = "false";
            Session.Abandon();

            Response.Cookies["Authenticated"].Expires = DateTime.Now.AddYears(-30);
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();

        }
    }
}
