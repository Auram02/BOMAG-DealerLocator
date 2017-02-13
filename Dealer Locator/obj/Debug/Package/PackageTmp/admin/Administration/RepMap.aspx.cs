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
    public partial class RepMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                ListItem li = new ListItem();
                li.Text = "-None-";
                li.Value = "-1";

                cboRep.Items.Add(li);

                li = new ListItem();
                li.Text = "";
                li.Value = "-1";
                cboUser.Items.Add(li);


                DA.UserTDSTableAdapters.DL_UserTableAdapter uta = new Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter();
                DA.UserTDS.DL_UserDataTable udt = new Dealer_Locator.DA.UserTDS.DL_UserDataTable();

                udt = uta.GetTerritoryManagers();

                foreach (DA.UserTDS.DL_UserRow dr in udt.Rows)
                {
                    li = new ListItem();
                    li.Text = dr.username;
                    li.Value = dr.pk_userID.ToString();

                    cboUser.Items.Add(li);
                }


                DataSet ds = new DataSet();

                ds = DDA.DataAccess.Representative_da.GetRepresentativeNameList("Territory");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    li = new ListItem();

                    li.Value = dr["RepID"].ToString();
                    li.Text = dr["Rep Name"].ToString();
                    cboRep.Items.Add(li);
                }

            }
        }

        protected void btnUpdateMapping_Click(object sender, EventArgs e)
        {
            int userID;

            if (cboUser.SelectedItem.Value != "-1")
            {
                // remove mapping
                userID = Convert.ToInt32(cboUser.SelectedItem.Value.ToString());

                DA.RepMappingTableAdapters.DL_RepMappingTableAdapter rmta = new Dealer_Locator.DA.RepMappingTableAdapters.DL_RepMappingTableAdapter();
                rmta.DeleteByUserID(userID);

                if (cboRep.SelectedItem.Value != "-1")
                {

                    // add mapping if necessary

                    int nextID = DA.DataAccess.GetNextID("[DL.RepMapping]", "pk_repMappingID");

                    int repID = Convert.ToInt32(cboRep.SelectedItem.Value);

                    rmta.InsertQuery(nextID, repID, userID);

                }

                lblResult.Text = "Mapping Updated Successfully";
            }
            else
            {

                lblResult.Text = "Please Select a Valid User Before Mapping";
            }

        }

        protected void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID;

            if (cboUser.SelectedItem.Value != "-1")
            {
                userID = Convert.ToInt32(cboUser.SelectedItem.Value);


                DA.RepMappingTableAdapters.DL_RepMappingTableAdapter rmta = new Dealer_Locator.DA.RepMappingTableAdapters.DL_RepMappingTableAdapter();
                int repID = Convert.ToInt32(rmta.GetRepIDByUserID(userID));

                try
                {
                    cboRep.SelectedValue = repID.ToString();
                }
                catch (Exception ex)
                {
                    cboRep.SelectedValue = "-1";
                }

            }
            else
            {

                cboRep.SelectedValue = "-1";
            }
        }

        

    }
}
