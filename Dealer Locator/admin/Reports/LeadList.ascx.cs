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
    public partial class LeadList : System.Web.UI.UserControl
    {

        private const string VIEW_LEAD_LIST_URL = "ViewLeadList.aspx";
        private const string VIEW_LEAD_URL = "ViewLead.aspx";
        private const string EDIT_LEAD_URL = "ModifyLeadData.aspx";
        private string _linkUrl = string.Empty;

        protected void Page_Load( object sender, EventArgs e )
        {

        }

        public void SetDateRange( DateTime beginDate, DateTime endDate )
        {

            string sql = string.Empty;

            sql = "SELECT DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].sendDate" +
                    " FROM         [DL.Lead] INNER JOIN" +
                    " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID" +
                    " WHERE     ([DL.Lead].sendDate >= '" + beginDate.ToString() + "') AND ([DL.Lead].sendDate <= '" + endDate.ToString() + "') " +
                    " GROUP BY pk_leadID, sendDate" +
                    " ORDER BY sendDate";

            DataSet ds = DA.DataAccess.Read( sql );

            _linkUrl = VIEW_LEAD_URL;

            GenerateDatatable( ref ds );
        }

        public void SetLastName( string lastName )
        {
            string sql = string.Empty;

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();

            ldt = lta.GetDataByLastName( lastName );

            string leadIds = string.Empty;

            foreach ( DA.LeadsTDS.DL_LeadRow dr in ldt.Rows )
            {
                if ( leadIds != string.Empty )
                    leadIds += ",";

                leadIds += "'" + dr.pk_leadID.ToString() + "'";
            }


            sql = "SELECT DISTINCT ([DL.Lead].pk_leadID), [DL.Lead].sendDate" +
                    " FROM         [DL.Lead] INNER JOIN" +
                    " [DL.LeadProduct] ON [DL.Lead].pk_leadID = [DL.LeadProduct].fk_leadID" +
                    " WHERE  pk_leadID IN (" + leadIds + ")" +
                    " GROUP BY pk_leadID, sendDate" +
                    " ORDER BY sendDate";

            DataSet ds = DA.DataAccess.Read( sql );

            _linkUrl = EDIT_LEAD_URL;

            GenerateDatatable( ref ds );
        }

        private void GenerateDatatable( ref DataSet ds )
        {

            string sql = string.Empty;

            if ( tblLeadList == null )
                tblLeadList = new Table();

            TableRow tr = new TableRow();
            TableCell tc = new TableCell();

            tc.Text = "First Name";
            tr.Cells.Add( tc );
            tc = new TableCell();

            tc.Text = "Last Name";
            tr.Cells.Add( tc );
            tc = new TableCell();


            tc.Text = "Send Date";
            tr.Cells.Add( tc );
            tc = new TableCell();

            tc.Text = "View Lead Emails";
            tr.Cells.Add( tc );
            tc = new TableCell();

            tr.BackColor = System.Drawing.Color.LightGray;

            tblLeadList.Rows.Add( tr );

            tr = new TableRow();

            DA.LeadsTDS.DL_LeadDataTable ldt = new Dealer_Locator.DA.LeadsTDS.DL_LeadDataTable();
            DA.LeadsTDSTableAdapters.DL_LeadTableAdapter lta = new Dealer_Locator.DA.LeadsTDSTableAdapters.DL_LeadTableAdapter();


            int counter = 0;

            if ( ds.Tables.Count > 0 )
            {
                foreach ( DataRow dr in ds.Tables[ 0 ].Rows )
                {

                    sql = "SELECT [elementValue] FROM [DL.LeadValues] WHERE ([elementName] = 'FirstName' OR  [elementName] = 'req_FirstName') AND [fk_leadID] = " + dr[ "pk_leadID" ].ToString();
                    DataSet ds2 = new DataSet();
                    ds2 = DA.DataAccess.Read( sql );

                    string firstName = ds2.Tables[ 0 ].Rows[ 0 ][ "elementValue" ].ToString();

                    tc.Text = firstName;
                    tr.Cells.Add( tc );
                    tc = new TableCell();

                    sql = "SELECT [elementValue] FROM [DL.LeadValues] WHERE [elementName] = 'LastName' AND [fk_leadID] = " + dr[ "pk_leadID" ].ToString();
                    ds2 = new DataSet();
                    ds2 = DA.DataAccess.Read( sql );

                    string lastName = ds2.Tables[ 0 ].Rows[ 0 ][ "elementValue" ].ToString();

                    tc.Text = lastName;
                    tr.Cells.Add( tc );
                    tc = new TableCell();


                    tc.Text = dr[ "sendDate" ].ToString();
                    tr.Cells.Add( tc );
                    tc = new TableCell();


                    tc.Text = "<a href=\"" + _linkUrl + "?leadID=" + dr[ "pk_leadID" ] + "\">View</a>";
                    tr.Cells.Add( tc );
                    tc = new TableCell();

                    tblLeadList.Rows.Add( tr );


                    System.Drawing.Color rowColor = new System.Drawing.Color();

                    if ( counter == 0 )
                    {
                        rowColor = System.Drawing.Color.FromArgb( 212, 215, 233 );
                        counter = 1;
                    }
                    else
                    {
                        rowColor = System.Drawing.Color.FromArgb( 163, 175, 216 );
                        counter = 0;
                    }

                    tr.BackColor = rowColor;

                    tr = new TableRow();
                }

            }
        }
    }
}