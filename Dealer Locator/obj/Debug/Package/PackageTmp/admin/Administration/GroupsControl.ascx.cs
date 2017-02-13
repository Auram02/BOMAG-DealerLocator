using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Admin
{
    public partial class GroupsControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                PopulateGroupsDropdown();
            }

            if (lblGroupID.Text == "")
            {

                DeleteGroupTable.Visible = false;
            }

        }

        private void PopulateGroupsDropdown()
        {
            DA.GroupdTDSTableAdapters.DL_GroupTableAdapter gta = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();
            DA.GroupdTDS._DL_GroupDataTable gdt = new Dealer_Locator.DA.GroupdTDS._DL_GroupDataTable();

            gdt = gta.GetData();

            cboGroups.Items.Clear();

            foreach (DA.GroupdTDS._DL_GroupRow tempRow in gdt.Rows)
            {
                ListItem li = new ListItem();
                li.Text = tempRow.GroupName;
                li.Value = tempRow.pk_groupID.ToString();

                cboGroups.Items.Add(li);

            }
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            int nextGroupID;
            nextGroupID = DA.DataAccess.GetNextID("[DL.Group]", "pk_groupID");


            DA.GroupdTDSTableAdapters.DL_GroupTableAdapter ta = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();
            ta.InsertQuery(nextGroupID, "New Group Name", false);

            Response.Redirect("/admin/Administration/Groups.aspx");
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {

                //e.Row.Cells[1].Visible = false;
                // updated the indexes
                e.Row.Cells[0].Visible = false;

            }
            catch
            {

            }
        }

        protected void btnReAssign_Click(object sender, EventArgs e)
        {
            DA.GroupdTDSTableAdapters.DL_GroupTableAdapter gta = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();

            int newGroupID = Convert.ToInt32(cboGroups.SelectedValue.ToString());
            int groupID = Convert.ToInt32(lblGroupID.Text);

            try
            {
                gta.ReAssignUser(newGroupID, groupID);
            }
            catch
            {

            }


            gta.DeleteGroup(groupID);

            lblGroupID.Text = "";


            Response.Redirect("/admin/Administration/Groups.aspx");


        }

        protected void btnDeleteAllUsers_Click(object sender, EventArgs e)
        {
            DA.GroupdTDSTableAdapters.DL_GroupTableAdapter gta = new Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter();
            int groupID = Convert.ToInt32(lblGroupID.Text);

            gta.DeleteAllUsersFromGroup(groupID);
            gta.DeleteGroup(groupID);

            lblGroupID.Text = "";

            Response.Redirect("/admin/Administration/Groups.aspx");

        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.Equals("deleteGroup"))
            {

                string groupID = "";

                groupID = GridView1.Rows[rowIndex].Cells[0].Text;

                int itemToRemove = -1;
                int counter = 0;



                for (int i = 0; i < cboGroups.Items.Count; i++)
                    if (cboGroups.Items[i].Value == groupID)
                    {
                        cboGroups.Items.RemoveAt(i);
                        i = cboGroups.Items.Count + 2;
                    }

                //foreach (ListItem item in cboGroups.Items)
                //{
                //    if (item.Value == groupID)
                //        itemToRemove = counter;

                //    counter += 1;
                //}

                //if (itemToRemove != -1)
                //{
                //    // remove the group we are "deleting"
                //    cboGroups.Items.RemoveAt(itemToRemove);
                //}


                lblGroupID.Text = groupID;
                DeleteGroupTable.Visible = true;

            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}