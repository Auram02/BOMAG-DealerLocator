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

namespace Dealer_Locator.admin.Reports
{
    public partial class TerritoryManagerReport : System.Web.UI.Page
    {
        int repID, year;

        protected void Page_Load(object sender, EventArgs e)
        {
            string breadCrumbs = "";

            ;

            try
            {
                repID = Convert.ToInt32(Session["repID"].ToString());


            }
            catch
            {
                repID = 0;

            }

            if (repID == 0)
            {
                try
                {

                    repID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["repID"].ToString());

                }
                catch { repID = 0; }
            }


            year = Convert.ToInt32(Request.QueryString["year"]);

            if (repID > 0 && year > 0)
            {
                if (!Page.IsPostBack)
                {
                    string repName = "";

                    ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
                    if (breadCrumbsArr == null)
                        breadCrumbsArr = new ArrayList();

                    Session.Remove("breadCrumbs");

                    breadCrumbsArr.Clear();

                    repName = DDA.DataAccess.Representative_da.GetRepresentativeName(repID);

                    breadCrumbs = breadCrumbs + repName;
                    
                    breadCrumbsArr.Add(repName);
                    
                    if (breadCrumbs != "")
                    {
                        breadCrumbs = breadCrumbs + " : ";
                    }

                    breadCrumbs += year.ToString();

                    Session.Add("breadCrumbs", breadCrumbsArr);

                    lblBreadcrumb.Text = breadCrumbs;
                    

                    CreateMonthlyRepReport(repID, year);
                }
            }
            else
            {

                lblError.Text = "No Representative ID or Year has been specified.  This page cannot load correctly.  Aborting...";

            }


        }

        private void CreateMonthlyRepReport(int repID, int year)
        {
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();

            tc.Text = "Month";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = "Lead Count";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = "View Leads";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tr.BackColor = System.Drawing.Color.LightGray;

            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("January", 1, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("February", 2, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("March", 3, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("April", 4, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("May", 5, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("June", 6, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("July", 7, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("August", 8, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("September", 9, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("October", 10, year, repID);
            tblTM.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("November", 11, year, repID);
            tblTM.Rows.Add(tr);


            tr = new TableRow();
            tr = CreateMonthRow("December", 12, year, repID);
            tblTM.Rows.Add(tr);

        }

        private TableRow CreateMonthRow(string Month, int MonthID, int year, int repID)
        {
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();

            tc.Text = Month;
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = GetLeadCount(MonthID, year, repID).ToString();  // get lead count
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = "<a href=\"ViewLeadList.aspx?MonthID=" + MonthID.ToString() + "&year=" + year.ToString() + "\">View</a>";
            tr.Cells.Add(tc);
            tc = new TableCell();

            tr.BackColor = GetRowColor(MonthID % 2);

            return tr;
        }

        /// <summary>
        /// I REVERSED HOW THE COUNTER WORKS.  DO NOT USE THIS FOR OTHER AREAS
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        private System.Drawing.Color GetRowColor(int counter)
        {
            System.Drawing.Color rowColor = new System.Drawing.Color();

            if (counter == 1)
            {
                rowColor = System.Drawing.Color.FromArgb(212, 215, 233);
                counter = 0;
            }
            else
            {
                rowColor = System.Drawing.Color.FromArgb(163, 175, 216);
                counter = 1;
            }

            return rowColor;

        }



        private int GetLeadCount(int monthID, int year, int repID)
        {
            int returnCount = 0;

            try
            {

                DateTime beginningDate = new DateTime(year, monthID, 1);

                DateTime endDate = new DateTime(year, monthID + 1, 1);

                string sql = "SELECT DISTINCT [DL.LeadProduct].territoryManagerID, COUNT(DISTINCT [DL.LeadProduct].fk_LeadID) AS LeadCount " +
                " FROM         [DL.LeadProduct] INNER JOIN " +
                " [DL.Lead] ON [DL.LeadProduct].fk_leadID = [DL.Lead].pk_leadID " +
                " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate < '" + endDate.ToString() + "') AND ([DL.LeadProduct].territoryManagerID > - 1)" +
                " AND [DL.LeadProduct].territoryManagerID = " + repID.ToString() +
                " GROUP BY [DL.LeadProduct].territoryManagerID";

                returnCount = Convert.ToInt32(DA.DataAccess.Read(sql).Tables[0].Rows[0]["LeadCount"].ToString());
            }
            catch
            {
                returnCount = 0;
            }

            return returnCount;
        }

    }
}
