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
using Dealer_Locator.DA;

namespace Dealer_Locator.admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName.ToString();
            string password = Login1.Password.ToString();


            UserTDS.DL_UserDataTable dsUser = new UserTDS.DL_UserDataTable();
            DA.UserTDSTableAdapters.DL_UserTableAdapter UserTA = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
            UserTA.Connection = DA.DataAccess.GetDatabaseConnection();

            dsUser = UserTA.GetUserByUsernamePassword(username, password);



            if (dsUser.Rows.Count > 0)
            {
                int _userID = (Int32)(dsUser[0].pk_userID);
                bool _isAdmin = (bool)(dsUser[0].admin);
                string _email = (string)(dsUser[0].email);
                int _groupID = (Int32)(dsUser[0].fk_groupID);
                bool _isRep = (bool)(dsUser[0].territoryManager);

                GroupdTDS._DL_GroupDataTable groupDT = new GroupdTDS._DL_GroupDataTable();
                DA.GroupdTDSTableAdapters.DL_GroupTableAdapter groupTA = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();
                groupDT = groupTA.GetDataByGroupID(_groupID);

                if (groupDT[0]["disable"].ToString() == "False")
                {

                    int repID = 0;

                    if (_isRep == true)
                    {
                        DA.RepMappingTableAdapters.DL_RepMappingTableAdapter rmta = new Dealer_Locator.DA.RepMappingTableAdapters.DL_RepMappingTableAdapter();
                        repID = Convert.ToInt32(rmta.GetRepIDByUserID(_userID));
                    }

                    DA.GroupdTDSTableAdapters.DL_GroupTableAdapter gta = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();
                    DA.GroupdTDS._DL_GroupDataTable gdt = gta.GetDataByGroupID(_groupID);
                    gta.Connection = DA.DataAccess.GetDatabaseConnection();

                    string _groupName = gdt[0].GroupName;

                    HttpCookie adminCookie = new HttpCookie("Authenticated", _isAdmin.ToString());

                    adminCookie.Values.Add("admin", _isAdmin.ToString());
                    DateTime dtExpiry = DateTime.Now.AddDays(1);
                    adminCookie.Expires = dtExpiry;
                    adminCookie.Values.Add("username", username);
                    adminCookie.Values.Add("email", _email);
                    adminCookie.Values.Add("groupID", _groupID.ToString());
                    adminCookie.Values.Add("IsLoggedIn", "true");
                    adminCookie.Values.Add("groupName", _groupName);
                    adminCookie.Values.Add("UserID", _userID.ToString());
                    adminCookie.Values.Add("IsRep", _isRep.ToString());
                    adminCookie.Values.Add("repID", repID.ToString());

                    Response.Cookies.Add(adminCookie);

                    // clear out session.  we have a new user logged in
                    Session.Abandon();

                    // add in another session for the distributor/dealer they are connected to

                    e.Authenticated = true;




                    try
                    {
                        string test = Request.QueryString["ReturnUrl"].ToString();
                        FormsAuthentication.RedirectFromLoginPage(username, true);
                    }
                    catch (Exception ex)
                    {

                        Response.Redirect("~/admin/Default.aspx");
                    }

                }
                else
                {
                    // group is disabled
                }
            }

        }
    }
}
