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

namespace Dealer_Locator.admin
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblEmailUpdate.Text = "";
            lblPasswordUpdate.Text = "";

            txtPassword.Text = "";
            txtNewPassword1.Text = "";
            txtNewPassword2.Text = "";

            // fill this in
            txtEmailAddress.Text = "";

            PopulateEmailAddress();


        }

        private void PopulateEmailAddress()
        {
            try
            {
                int userID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["UserID"].ToString());
                DA.UserTDSTableAdapters.DL_UserTableAdapter uta = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
                DA.UserTDS.DL_UserDataTable udt = new Dealer_Locator.DA.UserTDS.DL_UserDataTable();
                udt = uta.GetDataByUserID(userID);

                txtEmailAddress.Text = udt[0].email;
            }
            catch (Exception ex)
            {
                
            }
        }

        private bool CheckIfPasswordValid(string password)
        {
            int userID = Convert.ToInt32(Request.Cookies["Authenticated"].Values["UserID"].ToString());
            DA.UserTDSTableAdapters.DL_UserTableAdapter uta = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
            DA.UserTDS.DL_UserDataTable udt = new Dealer_Locator.DA.UserTDS.DL_UserDataTable();
            udt = uta.GetDataByUserID(userID);

            if (password == udt[0].password)
                return true;
            else
                return false;

        }


        protected void btnUpdateEmail_Click(object sender, EventArgs e)
        {

            DA.UserTDSTableAdapters.DL_UserTableAdapter uta = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();

            string emailAddress = "";


            foreach (string name in Request.Form)
            {

                if (name.Contains("txtEmailAddress"))
                    emailAddress = Request.Form[name];

            }

            try
            {
                uta.UpdateEmail(emailAddress, Convert.ToInt32(Request.Cookies["Authenticated"].Values["UserID"].ToString()));
                lblEmailUpdate.Text = "Your email address was updated successfully!";
            }
            catch (Exception ex)
            {
                // do stuff
            }
            finally
            {
                PopulateEmailAddress();
            }

        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string curPassword = "";
            string newPassword1 = "";
            string newPassword2 = "";

            foreach (string name in Request.Form)
            {

                if (name.Contains("txtPassword"))
                    curPassword = Request.Form[name];

                if (name.Contains("txtNewPassword1"))
                    newPassword1 = Request.Form[name];

                if (name.Contains("txtNewPassword2"))
                    newPassword2 = Request.Form[name];

            }

            if (newPassword1 == newPassword2)
            {
                if (curPassword != "")
                {
                    // we have current password entered and new passwords entered that match


                    if (CheckIfPasswordValid(curPassword))
                    {
                        DA.UserTDSTableAdapters.DL_UserTableAdapter uta = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();

                        try
                        {
                            uta.UpdatePassword(newPassword2, Convert.ToInt32(Request.Cookies["Authenticated"].Values["UserID"].ToString()));

                            lblPasswordUpdate.Text = "Your password was updated successfully!";
                        }
                        catch (Exception ex)
                        {
                            // do stuff
                        }
                    }
                    else
                    {
                        lblPasswordUpdate.Text = "The current password field was entered incorrectly or left blank.  Please enter your current password and try again.";
                    }
                }
                else
                {
                    lblPasswordUpdate.Text = "The current password field was entered incorrectly or left blank.  Please enter your current password and try again.";
                }
            }
            else
            {
                lblPasswordUpdate.Text = "The new password fields do not match.  Please enter the same new password for both fields.";
            }
        }
    }
}
