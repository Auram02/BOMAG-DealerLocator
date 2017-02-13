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
    public partial class ZipCodeImport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName;

                fileName = FileUpload1.PostedFile.FileName;
            }
            catch
            {
            }
        }

        protected void upload_load(object sender, EventArgs e)
        {
            try
            {
                string fileName;

                fileName = FileUpload1.PostedFile.FileName;
            }
            catch
            {
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";

            try
            {
                string origFileName = FileUpload1.PostedFile.FileName;

                string filePath = Server.MapPath("~/admin/DataImport/Data/" + origFileName);

                FileUpload1.PostedFile.SaveAs(filePath);
                //FileUpload1.SaveAs(filePath);
                // do more stuff like read the database

                DataSet ds;
                ArrayList tableNames = new ArrayList();

                tableNames.Add("[DL.ZipLookup]");

                System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();

                bool result = Dealer_Locator.DA.DataImport.ImportData(filePath, tableNames, "ZipCodeDatabase_STANDARD", out errors);

                if (result == true)
                    lblResult.Text = "Upload Complete";
                else
                {
                    lblResult.Text = "An Error Occurred.  Please contact the developer with a copy of the data you were trying to Import and the following error: <BR>";

                    foreach (string error in errors)
                    {
                        lblResult.Text = lblResult.Text + "<BR>" + error;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}