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
    public partial class TerritoryManagerLeadsByDealer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // year
            int year = Convert.ToInt32(Request.QueryString["year"]);
            int repID = 0;

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


            lblError.Text = "";

            if (year > 0 && repID > 0)
            {
                CreateDealerMonthlyReports(year,repID);
            }
            else
            {
                lblError.Text = "No Year or RepresentativeID Specified, Report Generation Aborted";
            }

        }

        private TableRow CreateMonthHeader(string month)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();


            tc.ColumnSpan = 4;
            tc.Text = month;
            tr.Cells.Add(tc);

            tr.BackColor = System.Drawing.Color.LightGray;


            return tr;
        }

        private TableRow CreateHeaderRow()
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();

            tc = new TableCell();
            tc.Text = "Dealer";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Location";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "Lead Count";
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "View Leads";
            tr.Cells.Add(tc);

            tr.BackColor = System.Drawing.Color.LightGray;

            return tr;
        }



        private void CreateDealerMonthlyReports(int year, int repID)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();


            tr = CreateMonthHeader("January");
            tblJanuary.Rows.Add(tr);

            tr = CreateMonthHeader("February");
            tblFebruary.Rows.Add(tr);

            tr = CreateMonthHeader("March");
            tblMarch.Rows.Add(tr);

            tr = CreateMonthHeader("April");
            tblApril.Rows.Add(tr);

            tr = CreateMonthHeader("May");
            tblMay.Rows.Add(tr);

            tr = CreateMonthHeader("June");
            tblJune.Rows.Add(tr);

            tr = CreateMonthHeader("July");
            tblJuly.Rows.Add(tr);

            tr = CreateMonthHeader("August");
            tblAugust.Rows.Add(tr);

            tr = CreateMonthHeader("September");
            tblSeptember.Rows.Add(tr);

            tr = CreateMonthHeader("October");
            tblOctober.Rows.Add(tr);

            tr = CreateMonthHeader("November");
            tblNovember.Rows.Add(tr);

            tr = CreateMonthHeader("December");
            tblDecember.Rows.Add(tr);
            

            // Add headers
            tr = CreateHeaderRow();

            tblJanuary.Rows.Add(tr);


            tr = CreateHeaderRow();
            tblFebruary.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblMarch.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblApril.Rows.Add(tr);


            tr = CreateHeaderRow();
            tblMay.Rows.Add(tr);


            tr = CreateHeaderRow();
            tblJune.Rows.Add(tr);


            tr = CreateHeaderRow();
            tblJuly.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblAugust.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblSeptember.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblOctober.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblNovember.Rows.Add(tr);

            tr = CreateHeaderRow();
            tblDecember.Rows.Add(tr);
            
            // ***********************************************

            // populate data
            DataSet ds = DDA.DataAccess.Representative_da.GetRepresentativeDistributorIDs(repID);

            int counter = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                int tempDistID = Convert.ToInt32(dr["fk_DistributorID"].ToString());
                string tempDistName = dr["DistName"].ToString();

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

                // loop for the months
                for (int i = 1; i <= 12; i++)
                {

                    tr = new TableRow();
                    tr = CreateMonthData(tempDistID, year, i);
                    tr.BackColor = rowColor;

                    switch (i)
                    {

                        case 1:
                            tblJanuary.Rows.Add(tr);
                            break;

                        case 2:
                            tblFebruary.Rows.Add(tr);
                            break;

                        case 3:
                            tblMarch.Rows.Add(tr);
                            break;

                        case 4:
                            tblApril.Rows.Add(tr);
                            break;

                        case 5:
                            tblMay.Rows.Add(tr);
                            break;

                        case 6:
                            tblJune.Rows.Add(tr);

                            break;

                        case 7:
                            tblJuly.Rows.Add(tr);
                            break;

                        case 8:
                            tblAugust.Rows.Add(tr);
                            break;

                        case 9:
                            tblSeptember.Rows.Add(tr);
                            break;

                        case 10:
                            tblOctober.Rows.Add(tr);
                            break;

                        case 11:
                            tblNovember.Rows.Add(tr);
                            break;

                        case 12:
                            tblDecember.Rows.Add(tr);
                            break;

                        default:

                            break;
                    }
                }

            }


        }

        private TableRow CreateMonthData(int distributorID, int year, int month)
        {
            TableRow tr = new TableRow();


            DateTime beginningDate = new DateTime(year, month, 1);

            if (month == 12)
            {
                year = year + 1;
                month = 0;
            }
            
            DateTime endingDate = new DateTime(year, month + 1, 1);

            
            //string sql = "SELECT     DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].submitDate, [DL.Lead].submitted, [DL.Lead].errorOccurred, [DL.Lead].errorValue, [DL.Lead].submitDate " + 
            //                " FROM [DL.Lead] INNER JOIN " +
            //                " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID " +
            //                " WHERE     ([DL.Lead].submitDate >= '" + beginningDate.ToString() + "') AND ([DL.Lead].submitDate <= '" + endingDate.ToString() + "') " + 
            //                " AND ([DL.LeadProduct].distributorID = " + distributorID.ToString() + ") " +
            //                " GROUP BY pk_leadID, submitDate, submitted, errorOccurred, errorValue, submitDate " +
            //                " ORDER BY [DL.Lead].submitDate";

            string sql = "SELECT COUNT (DISTINCT fk_LeadID) AS [Lead Count] FROM [DL.LeadProduct] INNER JOIN [DL.Lead] ON fk_leadID = pk_LeadID " +
                            " WHERE submitDate >= '" + beginningDate.ToString() + "' AND submitDate < '" +  endingDate.ToString() + "'" + 
                            " AND DistributorID = " + distributorID;

            DataSet ds = DA.DataAccess.Read(sql);

            int leadCount = Convert.ToInt32(ds.Tables[0].Rows[0]["Lead Count"].ToString());


            DataSet ds2 = DDA.DataAccess.Distributor_da.GetDistributorInformation_DL(distributorID);
            string location = ds2.Tables[0].Rows[0]["CityName"] + ", " + DDA.DataAccess.Location_da.GetStateAbbreviation(Convert.ToInt32(ds2.Tables[0].Rows[0]["fk_StateID"]));

            TableCell tc = new TableCell();
            tc.Text = DDA.DataAccess.Distributor_da.GetDistributorName(distributorID);
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = location;
            tr.Cells.Add(tc);


            tc = new TableCell();
            tc.Text = leadCount.ToString();
            tr.Cells.Add(tc);

            tc = new TableCell();
            tc.Text = "<a href=\"ViewLeadList.aspx?year=" + year + "&MonthID=" + month + "&r=f&distID=" + distributorID + "\">Report</a>";
            tr.Cells.Add(tc);
            

            return tr;
        }
    }
}
