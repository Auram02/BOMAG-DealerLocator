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
    public partial class BySourceReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                int currentYear = DateTime.Now.AddYears(3).Year;

                cboYear.Items.Clear();

                ListItem liTemp = new ListItem();

                liTemp.Value = "-1";
                liTemp.Text = "-Select-";

                cboYear.Items.Add(liTemp);

                liTemp = new ListItem();
                liTemp.Value = "0";
                liTemp.Text = "-All-";

                cboYear.Items.Add(liTemp);


                for (int i = 2005; i < currentYear; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = i.ToString();
                    li.Value = i.ToString();
                    cboYear.Items.Add(li);
                }


                // breadcrumbs stuff
                ArrayList breadCrumbsArr = ((ArrayList)Session["breadCrumbs"]);
                if (breadCrumbsArr == null)
                    breadCrumbsArr = new ArrayList();

                Session.Remove("breadCrumbs");

                breadCrumbsArr.Clear();

                string breadCrumbs = "";


                breadCrumbs = breadCrumbs + "Leads By Source";

                breadCrumbsArr.Add("Leads By Source");

                Session.Add("breadCrumbs", breadCrumbsArr);




            }
        }

        protected void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            tblLeadsBySource.Rows.Clear();
            lblError.Text = "";


            if (cboYear.SelectedItem.Value != "-1")
            {

                TableRow trHeader = new TableRow();
                TableCell tcHeader = new TableCell();

                tcHeader.Text = "Source";
                trHeader.Cells.Add(tcHeader);

                tcHeader = new TableCell();
                tcHeader.Text = "Lead Count";
                trHeader.Cells.Add(tcHeader);

                trHeader.BackColor = System.Drawing.Color.LightGray;

                tblLeadsBySource.Rows.Add(trHeader);


                string dateString = "";

                if (cboYear.SelectedItem.Value == "0")
                {

                }
                else
                {
                    // year selected
                    int year = Convert.ToInt32(cboYear.SelectedItem.Value);

                    dateString = "AND (lead.submitDate >= '1/1/" + year + "' AND lead.submitDate < '1/1/" + (year + 1) + "')";
                }

                string sql = "";

                sql = "SELECT DISTINCT COUNT(pk_leadID) AS [Lead Count], elementValue AS [value] " +
                    " FROM [DL.Lead] lead " +
                    " INNER JOIN [DL.LeadValues] lv ON pk_leadid = fk_leadid " +
                    " WHERE (lv.elementName LIKE '%source%') " +
                    dateString +
                    " GROUP BY elementValue";


                DataSet ds2 = new DataSet();
                ds2 = DA.DataAccess.Read(sql);





                int counter = 0;

                foreach (DataRow dr in ds2.Tables[0].Rows)
                {

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

                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();

                    tc.Text = "<a href=\"ViewLeadList.aspx?MonthID=1&year=" + cboYear.SelectedItem.Value + "&source=" + dr["value"].ToString() + "\">" + dr["value"].ToString() + "</a>";
                    tr.Cells.Add(tc);

                    tc = new TableCell();

                    //tc.Text = "<a href=\"ViewLeadList.aspx?MonthID=" + MonthID.ToString() + "&year=" + year.ToString() + "\">View</a>";
                    int leadCount = 0;

                    leadCount = Convert.ToInt32(dr["Lead Count"].ToString());

                    tc.Text = leadCount.ToString();

                    tr.Cells.Add(tc);

                    tr.BackColor = rowColor;
                    tblLeadsBySource.Rows.Add(tr);

                }

            }
            else
            {
                lblError.Text = "Please Select a Year";
            }
        }
    }
}