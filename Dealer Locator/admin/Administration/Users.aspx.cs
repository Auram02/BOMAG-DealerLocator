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

namespace Dealer_Locator.admin.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        public int groupID;
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);


            //groupID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["GroupID"]);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DA.UserTDS.DL_UserDataTable ds = new Dealer_Locator.DA.UserTDS.DL_UserDataTable();
            DA.UserTDS.DL_UserDataTable dsUser = new DA.UserTDS.DL_UserDataTable();

            DA.UserTDSTableAdapters.DL_UserTableAdapter UserTA = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
            UserTA.Connection = DA.DataAccess.GetDatabaseConnection();
            
            dsUser = UserTA.GetData();

            //GridView1.DataSource = dsUser;
            //GridView1.DataBind();

            //4, 5
            //GridView1.Columns[4].Visible = false;
            //GridView1.Columns[5].Visible = false;

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
            catch
            {

            }
        }


        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            // Add new user
            DA.UserTDSTableAdapters.DL_UserTableAdapter UserTA = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
            UserTA.Connection = DA.DataAccess.GetDatabaseConnection();

            Random randomGenerator = new Random();
            int ranNumber = randomGenerator.Next(1,20000);
            
            UserTA.Insert("*New User*", "NewPassword" + ranNumber.ToString(), "", false, DA.DataAccess.GetNextID("[DL.User]", "pk_userID"), Convert.ToInt32(cboGroupName.SelectedValue),false);
            Response.Redirect("~/admin/Administration/Users.aspx");

        }

        protected void cboGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
