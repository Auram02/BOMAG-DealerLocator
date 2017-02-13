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
    public partial class ViewLeadList : System.Web.UI.Page
    {
        int repID, year;
        string source;
        int monthID;

        int collectionNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                repID = Convert.ToInt32(Session["repID"].ToString());
            }
            catch
            {
                repID = 0;

            }

            try
            {
                source = Session["source"].ToString();
            }
            catch
            {
                source = "";
            }

            if (repID == 0)
            {
                try
                {

                    repID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["repID"].ToString());

                }
                catch { repID = 0; }
            }

            if (Request.QueryString["r"] == "f")
            {
                repID = 0;
            }

            int distID = -1;

            if (Request.QueryString["distID"] != null)
            {
                distID = Convert.ToInt32(Request.QueryString["distID"].ToString());
            }

            if (Request.QueryString["source"] != null)
            {
                source = Request.QueryString["source"].ToString();
            }

            if (Request.QueryString["colNum"] != null)
            {
                collectionNumber = Convert.ToInt32(Request.QueryString["colNum"].ToString());
            }


            if (!Page.IsPostBack)
            {

            }


            year = Convert.ToInt32(Request.QueryString["year"]);
            monthID = Convert.ToInt32(Request.QueryString["MonthID"]);
            if (source != "" && year > 0 && monthID > 0)
            {
                ConstructBreadcrumbTrailSource();
                CreateLeadList(0, year, monthID, -1, source);
            }
            else
            {
                if (repID > 0 && year > 0 && monthID > 0)
                {
                    if (!Page.IsPostBack)
                    {
                        ConstructBreadcrumbTrail();

                        CreateLeadList(repID, year, monthID, -1, "");

                    }
                }
                else
                {
                    if (year > 0 && monthID > 0)
                    {
                        if (distID == 0)
                        {
                            CreateLeadList(0, year, monthID, -1, "");
                        }
                        else
                        {
                            // distributor ID passed in...create list specific to them
                            CreateLeadList(0, year, monthID, distID, "");
                        }

                        ConstructBreadcrumbTrailYearMonth();

                    }
                    else
                    {

                        lblError.Text = "No Representative ID, Month ID, or Year has been specified.  This page cannot load correctly.  Aborting...";

                    }

                }
            }

        }

        private void ConstructBreadcrumbTrailYearMonth()
        {

            string month = GetMonth(monthID);

            string totalDateString = month + " " + year;

            string breadCrumbs = "";


            ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
            if (breadCrumbsArr == null)
                breadCrumbsArr = new ArrayList();





            Session.Remove("breadCrumbs");

            breadCrumbsArr.Clear();


            breadCrumbs += "Monthly Breakdown : ";
            breadCrumbsArr.Add("Monthly Breakdown");

            breadCrumbs += totalDateString;
            breadCrumbsArr.Add(totalDateString);

            Session.Add("breadCrumbs", breadCrumbsArr);


            lblBreadcrumb.Text = breadCrumbs;
        }

        private void ConstructBreadcrumbTrail()
        {
            string breadCrumbs = "";

            ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
            if (breadCrumbsArr == null)
                breadCrumbsArr = new ArrayList();

            Session.Remove("breadCrumbs");

            string repName = "";

            try
            {
                repName = breadCrumbsArr[0].ToString();
            }
            catch { }

            breadCrumbsArr.Clear();

            breadCrumbsArr.Add(repName);

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

            breadCrumbs += "Monthly Breakdown : ";
            breadCrumbsArr.Add("Monthly Breakdown");


            string month = GetMonth(monthID);



            string totalDateString = month + " " + year;
            breadCrumbs += totalDateString;


            breadCrumbsArr.Add(totalDateString);

            Session.Add("breadCrumbs", breadCrumbsArr);

            lblBreadcrumb.Text = breadCrumbs;
        }

        private void ConstructBreadcrumbTrailSource()
        {
            string breadCrumbs = "";

            ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
            if (breadCrumbsArr == null)
                breadCrumbsArr = new ArrayList();

            Session.Remove("breadCrumbs");

            string sourceName = "";

            try
            {
                sourceName = breadCrumbsArr[0].ToString();
            }
            catch { }

            sourceName = source;

            breadCrumbsArr.Clear();

            breadCrumbsArr.Add(sourceName);

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


            string month = GetMonth(monthID);



            string totalDateString = year.ToString();
            breadCrumbs += totalDateString;


            breadCrumbsArr.Add(totalDateString);

            Session.Add("breadCrumbs", breadCrumbsArr);

            lblBreadcrumb.Text = "Leads by Source : " + breadCrumbs;
        }

        private string GetMonth(int monthID)
        {
            string month = "";

            switch (monthID)
            {
                case 1:
                    month = "January";

                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;

            }

            return month;
        }

        private void CreateLeadList(int repID, int year, int monthID, int distID, string source)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();

            tc.Text = "Last Name";
            tr.Cells.Add(tc);
            tc = new TableCell();


            tc.Text = "Lead Date";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = "View Lead Emails";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tr.BackColor = System.Drawing.Color.LightGray;

            tblTM.Rows.Add(tr);

            tr = new TableRow();

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            DateTime beginningDate = new DateTime(year, monthID, 1);

            if (monthID == 12)
            {
                // wrap to next year
                monthID = 0;
                year = year + 1;
            }

            DateTime endDate = new DateTime(year, monthID + 1, 1);


            //ldt = lta.GetDataBysubmitDateRepID(beginningDate, endDate,repID);
            string sql = "";

            if (distID == -1)
            {
                if (source == "")
                {
                    if (repID > 0)
                    {
                        sql = "SELECT     DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].submitDate" +
                                " FROM         [DL.Lead] INNER JOIN" +
                                " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID" +
                                " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate <= '" + endDate.ToString() + "') AND" +
                                " ([DL.LeadProduct].territoryManagerID = " + repID + ")" +
                                " GROUP BY pk_leadID, submitDate";
                    }
                    else
                    {
                        // only month/year needed

                        sql = "SELECT     DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].submitDate" +
                                " FROM         [DL.Lead] INNER JOIN" +
                                " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID" +
                                " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate <= '" + endDate.ToString() + "') " +
                                " GROUP BY pk_leadID, submitDate";

                    }
                }
                else
                {

                    // A source was selected
                    endDate = beginningDate.AddYears(1);

                    sql = "SELECT     DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].submitDate" +
                            " FROM         [DL.Lead] " +
                            " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate <= '" + endDate.ToString() + "') " +
                            " AND [DL.Lead].pk_leadID IN (select fk_leadID from [dl.leadvalues]" +
                            " WHERE elementname = 'source_css'" +
                            " AND elementValue LIKE '" + source + "')" +
                            " GROUP BY pk_leadID, submitDate";
                }
            }
            else
            {
                // A distributor was selected

                    sql = "SELECT     DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].submitDate" +
                            " FROM         [DL.Lead] INNER JOIN" +
                            " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID" +
                            " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate <= '" + endDate.ToString() + "') " +
                            " AND ([DL.LeadProduct].distributorID = " + distID.ToString() + ")" +
                            " GROUP BY pk_leadID, submitDate";
            }


            string countSQL = "SELECT COUNT (tbl.pk_leadID) FROM (" + sql + ") as tbl";
            DataSet dsCount = new DataSet();
            dsCount = DA.DataAccess.Read(countSQL);

            int rowCount = Convert.ToInt32(dsCount.Tables[0].Rows[0][0].ToString());

            double doubleRowCount = rowCount;
            doubleRowCount = doubleRowCount / 200;
            rowCount = rowCount / 200;

            if (doubleRowCount > rowCount)
                rowCount++;

            string litPageCountOutput = string.Empty;
            for (int i = 0; i < rowCount; i++)
            {
                Uri MyUrl = Request.Url;

                string theUrl = MyUrl.AbsoluteUri;
                if (theUrl.Contains("?") == false)
                    theUrl += "?";

                if (theUrl.Contains("&colNum"))
                    theUrl = theUrl.Substring(0,theUrl.IndexOf("&colNum"));

                litPageCountOutput += "<a href=\"" + theUrl + "&colNum=" + i.ToString() + "\">Page " + (i + 1) + "</a>&nbsp;&nbsp;";
            }

            litReportPageListing.Text = litPageCountOutput;



            Session.Add("ExcelReportSQL", sql + " ORDER BY [DL.Lead].submitDate");

            int startRow = 200 * collectionNumber + 1;
            int endRow = 200 + (200 * collectionNumber);

            sql = sql.Replace("'", "''");
            sql = "exec sp_MidRows_Query '" + sql + "', " + startRow + ", " + endRow;

            DataSet ds = new DataSet();
            ds = DA.DataAccess.Read(sql);

            lblExcelUrl.Visible = true;
            lblExcelUrl.Text = "<a href=\"CreateExcelReport.aspx\">Generate Full Excel Report</a>";

            int counter = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sql = "SELECT [elementValue] FROM [DL.LeadValues] WHERE [elementName] = 'LastName' AND [fk_leadID] = " + dr["pk_leadID"].ToString();
                DataSet ds2 = new DataSet();
                ds2 = DA.DataAccess.Read(sql);

                string lastName = ds2.Tables[0].Rows[0]["elementValue"].ToString();

                tc.Text = lastName;
                tr.Cells.Add(tc);
                tc = new TableCell();


                tc.Text = dr["submitDate"].ToString();
                tr.Cells.Add(tc);
                tc = new TableCell();


                tc.Text = "<a href=\"ViewLead.aspx?leadID=" + dr["pk_leadID"] + "\">View</a>";
                tr.Cells.Add(tc);
                tc = new TableCell();

                tblTM.Rows.Add(tr);


                System.Drawing.Color rowColor = new System.Drawing.Color();

                if (counter == 0)
                {
                    rowColor = System.Drawing.Color.FromArgb(212, 215, 233);
                    counter = 1;
                }
                else
                {
                    rowColor = System.Drawing.Color.FromArgb(163, 175, 216);
                    counter = 0;
                }

                tr.BackColor = rowColor;

                tr = new TableRow();
            }


        }
    }
}
