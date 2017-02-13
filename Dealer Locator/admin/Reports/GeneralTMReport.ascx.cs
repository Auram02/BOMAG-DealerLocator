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
    public partial class GeneralTMReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Session.Remove("breadCrumbs");


            if (Page.IsPostBack == false)
            {
                int currentYear = DateTime.Now.AddYears(3).Year;

                cboYear.Items.Clear();

                ListItem liTemp = new ListItem();
                liTemp.Value = "-1";
                liTemp.Text = "-Select-";
                cboYear.Items.Add(liTemp);

                for (int i = 2005; i < currentYear; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = i.ToString();
                    li.Value = i.ToString();
                    cboYear.Items.Add(li);
                }
            }
        }


        private void CreateTMReport(int currentYear)
        {
            //gvGeneralTMReport.DataSource = null;  // Clear datasource
            tblTM.Rows.Clear();

            DateTime beginningDate = new DateTime(currentYear, 1, 1);
            DateTime endDate = new DateTime(currentYear + 1, 1, 1);

            // get each TM.
            // then, select count of all leads that occurred for them in the given time frame
            DA.LeadsTDS.DL_LeadProductDataTable lpdt = new Dealer_Locator.DA.LeadsTDS.DL_LeadProductDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter lpta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadProductTableAdapter();

            //lpdt = lpta.GetDataBySubmitDate(beginningDate, endDate);

            string sql = "SELECT DISTINCT [DL.LeadProduct].territoryManagerID, COUNT(DISTINCT [DL.LeadProduct].fk_LeadID) AS LeadCount " +
                            " FROM         [DL.LeadProduct] INNER JOIN " +
                            " [DL.Lead] ON [DL.LeadProduct].fk_leadID = [DL.Lead].pk_leadID " +
                            " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate < '" + endDate.ToString() + "') AND ([DL.LeadProduct].territoryManagerID > - 1)" +
            " GROUP BY [DL.LeadProduct].territoryManagerID";

            DataSet dsTM = new DataSet();
            dsTM = DA.DataAccess.Read(sql);

            //lpdt = lpta.GetTerritoryManagerLeadDatabySubmitDate(beginningDate, endDate);

            Hashtable tmLookup = new Hashtable();

            foreach (DataRow dr in dsTM.Tables[0].Rows)
            {
                // territoryManagerID, leadCount will be in the datarow
                int territoryManagerID = Convert.ToInt32(dr["territoryManagerID"].ToString());
                int leadCount = Convert.ToInt32(dr["LeadCount"].ToString());

                tmLookup.Add(territoryManagerID, leadCount);

            }

            DataSet ds = new DataSet();
            ds = DDA.DataAccess.Representative_da.GetRepresentativeNameList("Territory");
            ds.Tables[0].Columns.Add("Lead Count");
            ds.Tables[0].Columns.Add("Monthly Lead Report", typeof(string));

            TableRow tr2 = new TableRow();
            TableCell tc2 = new TableCell();
            tc2.Text = "Territory Manager";
            tr2.Cells.Add(tc2);
            tc2 = new TableCell();
            tc2.Text = "Lead Count";
            tr2.Cells.Add(tc2);
            tc2 = new TableCell();
            tc2.Text = "Monthly Lead Report";

            tr2.Cells.Add(tc2);

            tr2.BackColor = System.Drawing.Color.LightGray;
            tblTM.Rows.Add(tr2);

            int counter = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int leadCount = 0;
                int repID = Convert.ToInt32(ds.Tables[0].Rows[i]["RepID"].ToString());



                try
                {
                    leadCount = Convert.ToInt32(tmLookup[repID]);
                }
                catch
                {
                    leadCount = 0;
                }

                if (leadCount < 0)
                    leadCount = 0;

                ds.Tables[0].Rows[i]["Lead Count"] = leadCount.ToString();

                ds.Tables[0].Rows[i]["Monthly Lead Report"] = "<a href=\"TMReportPasser.aspx?tmID=" + repID.ToString() + "&year=" + beginningDate.Year + "\">Report</a>";

                TableRow tr = new TableRow();

                // skip the first column
                for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
                {
                    TableCell tc = new TableCell();
                    tc.Text = ds.Tables[0].Rows[i][j].ToString();

                    tr.Cells.Add(tc);
                }


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

                tblTM.Rows.Add(tr);

            }

            //gvGeneralTMReport.DataSource = ds.Tables[0];
            //gvGeneralTMReport.DataBind();




        }

        private void CreateMonthlyLeadBreakdown(int year)
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

            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("January", 1, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("February", 2, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("March", 3, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("April", 4, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("May", 5, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("June", 6, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("July", 7, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("August", 8, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("September", 9, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("October", 10, year);
            tblMonthlyBreakdown.Rows.Add(tr);

            tr = new TableRow();
            tr = CreateMonthRow("November", 11, year);
            tblMonthlyBreakdown.Rows.Add(tr);


            tr = new TableRow();
            tr = CreateMonthRow("December", 12, year);
            tblMonthlyBreakdown.Rows.Add(tr);


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

        private TableRow CreateMonthRow(string Month, int MonthID, int year)
        {
            TableCell tc = new TableCell();
            TableRow tr = new TableRow();

            tc.Text = Month;
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = GetLeadCount(MonthID, year).ToString();  // get lead count
            tr.Cells.Add(tc);
            tc = new TableCell();

            tc.Text = "<a href=\"ViewLeadList.aspx?year=" + year.ToString() + "&MonthID=" + MonthID.ToString() + "&r=f\">Report</a>";  // get lead count
            tr.Cells.Add(tc);
            tc = new TableCell();

            tr.BackColor = GetRowColor(MonthID % 2);

            return tr;
        }

        private int GetLeadCount(int monthID, int year)
        {
            int returnCount = 0;

            try
            {

                DateTime beginningDate = new DateTime(year, monthID, 1);

                if (monthID == 12)
                {
                    monthID = 1;
                    year = year + 1;
                }
                else
                {
                    monthID++;
                }

                DateTime endDate = new DateTime(year, monthID, 1);

                string sql = "SELECT COUNT(DISTINCT pk_LeadID) AS LeadCount FROM [DL.Lead] " +
                                " WHERE ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate < '" + endDate.ToString() + "')";


                //string sql = "SELECT COUNT([DL.LeadProduct].pk_leadProductID) AS LeadCount " +
                //" FROM         [DL.LeadProduct] INNER JOIN " +
                //" [DL.Lead] ON [DL.LeadProduct].fk_leadID = [DL.Lead].pk_leadID " +
                //" WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate < '" + endDate.ToString() + "') AND ([DL.LeadProduct].territoryManagerID > - 1)" +

                //" GROUP BY [DL.LeadProduct].territoryManagerID";

                returnCount = Convert.ToInt32(DA.DataAccess.Read(sql).Tables[0].Rows[0]["LeadCount"].ToString());
            }
            catch
            {
                returnCount = 0;
            }

            return returnCount;
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboYear.SelectedIndex > -1)
            {
                if (cboYear.SelectedItem.Value != "-1")
                {
                    int currentYear = Convert.ToInt32(cboYear.SelectedItem.Value);

                    CreateTMReport(currentYear);
                    CreateMonthlyLeadBreakdown(currentYear);
                }
            }
            else
            {

            }

        }

    }
}