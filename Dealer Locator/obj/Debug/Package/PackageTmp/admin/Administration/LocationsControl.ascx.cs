using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dealer_Locator.admin.Admin
{
    public partial class LocationsControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {

            DA.DLTableAdapters.DL_RegionTableAdapter rta = new Dealer_Locator.DA.DLTableAdapters.DL_RegionTableAdapter();

            int nextRegionID = DA.DataAccess.GetNextID("[DL.Region]", "pk_regionID");

            rta.Insert(nextRegionID, "*New Region", "", "");

            Response.Redirect("/admin/Administration/RegionManagement.aspx");
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
        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

            //if (e.CommandName.Equals("deleteGroup"))
            //{

            //    string groupID = "";

            //    groupID = GridView1.Rows[rowIndex].Cells[0].Text;

            //    int itemToRemove = -1;
            //    int counter = 0;



            //    for (int i = 0; i < cboGroups.Items.Count; i++)
            //        if (cboGroups.Items[i].Value == groupID)
            //        {
            //            cboGroups.Items.RemoveAt(i);
            //            i = cboGroups.Items.Count + 2;
            //        }

            //    //foreach (ListItem item in cboGroups.Items)
            //    //{
            //    //    if (item.Value == groupID)
            //    //        itemToRemove = counter;

            //    //    counter += 1;
            //    //}

            //    //if (itemToRemove != -1)
            //    //{
            //    //    // remove the group we are "deleting"
            //    //    cboGroups.Items.RemoveAt(itemToRemove);
            //    //}


            //    lblGroupID.Text = groupID;
            //    DeleteGroupTable.Visible = true;

            //}

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}