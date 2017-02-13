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
using SagaraSoftware.ZipCodeUtil;

namespace Dealer_Locator.admin.Reports
{
    public partial class IncorrectDistributorCityReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoErrors.Visible = false;

            DataSet ds;
            ds = DA.Reports.CreateDistributorCityReport();



            try
            {
                ds.Tables[0].Columns.Add("View Cities in Zip");

                string Cities = "";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Cities = "";
                    DataSet tempCities = DA.Reports.GetCitiesInZipCode(Convert.ToInt32(dr["fk_ZipID"]));


                    foreach (DataRow dr2 in tempCities.Tables[0].Rows)
                    {
                        if (Cities != "")
                            Cities = Cities + "," + Environment.NewLine;

                        Cities = Cities + dr2["CITY_ALIAS_NAME"];
                    }

                    dr["View Cities in Zip"] = Cities;

                }

                gvDistributorCityReport.DataSource = ds.Tables[0];
                gvDistributorCityReport.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);

                if (ex.Message.Contains("Cannot find table 0"))
                {
                    lblNoErrors.Visible = true;
                }
            }


        }
    }
}