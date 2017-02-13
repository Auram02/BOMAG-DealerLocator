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

namespace Dealer_Locator.admin.DataImport
{
    public partial class ImportDealerInformation1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";

            // Ensure a file has been uploaded
            if (FileUpload1.HasFile)
            {

                string filePath = Server.MapPath("~/admin/DataImport/Data/DDA.mdb");
                FileUpload1.SaveAs(filePath);
                // do more stuff like read the database

                DataSet ds;
                ArrayList tableNames = new ArrayList();

                // ###########################
                tableNames.Add("Category");  // This one will have to be processed seperately!
                // ###########################

                tableNames.Add("Contract");
                tableNames.Add("ContractCategory");
                tableNames.Add("ContractCounty");
                tableNames.Add("ContractDistributor");
                tableNames.Add("[County]");
                tableNames.Add("Distributor");
                tableNames.Add("DistributorBranch");
                tableNames.Add("DistributorEmail");
                tableNames.Add("DistributorRepresentative");
                tableNames.Add("Representative");
                tableNames.Add("RepresentativeType");
                tableNames.Add("SplitCounty");
                tableNames.Add("State");

                System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
                bool result = Dealer_Locator.DA.DataImport.ImportData(filePath, tableNames, "", out errors);

                if (result == true)
                    lblResult.Text = "Upload Complete";
                else
                {
                    lblResult.Text = "An Error Occurred.  Please contact the developer with a copy of the data you were trying to Import.<BR><BR>[Errors]<BR>";

                    foreach (string error in errors)
                    {
                        lblResult.Text += error + "<BR>";
                    }
                    
                }
                //GridView1.DataSource = ds.Tables[0];
                //GridView1.DataBind();
            }
        }
    }
}