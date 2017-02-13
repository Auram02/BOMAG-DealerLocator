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
    public partial class SelectYear : System.Web.UI.UserControl
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

                for (int i = 2005; i < currentYear; i++)
                {
                    ListItem li = new ListItem();
                    li.Text = i.ToString();
                    li.Value = i.ToString();
                    cboYear.Items.Add(li);
                }
            }
        }

        public string Year
        {
            get { return cboYear.SelectedItem.Value; }
        }
    }
}