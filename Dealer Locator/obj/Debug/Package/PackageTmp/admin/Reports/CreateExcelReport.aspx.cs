using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;

namespace Dealer_Locator.admin.Reports
{
    public partial class CreateExcelReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);

            string breadCrumbs = "";

            foreach (string item in breadCrumbsArr)
            {
                if (breadCrumbs != "")
                    breadCrumbs += " : ";
                breadCrumbs += item;
            }

            if (breadCrumbs != "")
            {
                breadCrumbs = breadCrumbs + " : ";
            }

            lblBreadcrumb.Text = breadCrumbs + "Generate Excel Report";

            string sql = Session["ExcelReportSQL"].ToString();

            DataSet ds = new DataSet();
            ds = DA.DataAccess.Read(sql);

            List<int> LeadIDs = new List<int>();

            // get all the lead ids
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                LeadIDs.Add(Convert.ToInt32(ds.Tables[0].Rows[i]["pk_leadID"].ToString()));

            BR.LeadExcelReport leadExcelReport = new Dealer_Locator.BR.LeadExcelReport();

            string pathName = Server.MapPath("~/admin/Reports/Data/");

            string fileName = "DownloadsExcel_" + BR.Utility.GenerateRandomCode() + ".xls";
            pathName = pathName + fileName;


            string errorMessage = leadExcelReport.CreateLeadExcelReport(LeadIDs, pathName);
                        
            if (errorMessage != "")
                litExcelReportUrl.Text = errorMessage;
            else
                litExcelReportUrl.Text = "Download: <a href=\"Data/" + fileName + "\">" + fileName + "</a>";


        }
    }
}
