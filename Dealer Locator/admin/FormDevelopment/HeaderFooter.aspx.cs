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
using ParameterPasser;

namespace Dealer_Locator.admin.FormDevelopment
{
    public partial class HeaderFooter : System.Web.UI.Page
    {
        private int _footerID, _headerID;

        protected void Page_Load(object sender, EventArgs e)
        {

            //SessionParameterPasser sessionWrapper = new SessionParameterPasser();
            //Literal headerFormName = (Literal)Master.FindControl("litFormName");
            //headerFormName.Text = "Form: " + sessionWrapper["formName"];


            //string navLinks = BR.FormTemplate.WriteNavigationLinks("Dashboard.aspx", (bool)Session["IsZipLocator"]);
            //Literal header = (Literal)Master.FindControl("litHeaderContent");
            //header.Text = navLinks;

            //if (Request.QueryString["result"] == "True")
            //    lblResult.Text = "Update Successful";
            //else
            //    lblResult.Text = "";


            //ParameterPasser.SessionParameterPasser myParam = new ParameterPasser.SessionParameterPasser();
            //int formID = Convert.ToInt32(myParam["formID"]);
            //string formName = Convert.ToString(myParam["formName"]);
            //int groupID = Convert.ToInt32(Request.Cookies["Authenticated"]["groupID"]);



            //DA.FooterTDS.DL_FooterDataTable fdt;
            //DA.HeaderTDS.DL_HeaderDataTable hdt;
            //fdt = BR.FormTemplate.GetFooter(formID);
            //_footerID = (Int32)(fdt.Rows[0]["pk_footerID"]);
            //hdt = BR.FormTemplate.GetHeader(formID);
            //_headerID = (Int32)(hdt.Rows[0]["pk_headerID"]);


            if (cboHeaderFooter.SelectedValue.ToString() != "")
            {
                string script = "";

                script = "window.open" +
                  "('" + "/locate.aspx?hfID=" + Convert.ToInt32(cboHeaderFooter.SelectedValue.ToString()) + "','_new', 'width=800,height=600');";
                lnkUrl.Attributes.Add("Onclick", script);
            }
            if (Page.IsPostBack == false)
            {
                DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter ta = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                DA.HeaderFooter.DL_HeaderFooterDataTable dt = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

                dt = ta.GetData();

                ListItem temp2 = new ListItem();
                temp2.Value = "-1";
                temp2.Text = "New...";

                cboHeaderFooter.Items.Add(temp2);

                foreach (DataRow dr in dt.Rows)
                {
                    ListItem temp = new ListItem();
                    temp.Text = dr["headerFooterName"].ToString();
                    temp.Value = dr["pk_headerFooterID"].ToString();

                    cboHeaderFooter.Items.Add(temp);

                }

                //txtFooter.Text = (string)(fdt.Rows[0]["Content"]);
                //txtHeader.Text = (string)(fdt.Rows[0]["Content"]); 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string header, footer, name, customCode, thanksUrl;

            header = txtHeader.Text;
            footer = txtFooter.Text;
            customCode = txtCustomCode.Text;
            name = txtName.Text;
            thanksUrl = txtThanksUrl.Text;

            DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter ta = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();

            // get the group id
            int groupID = Convert.ToInt32(Request.Cookies["Authenticated"]["groupID"]);


            if (cboHeaderFooter.SelectedItem.Text != "New...")
            {
                bool defaultHeaderFooter = chkDefault.Checked;

                if (defaultHeaderFooter)
                {
                    // clear out all other default headers
                    //ta.ClearHeaderFooters(0);
                    ta.ClearHeaderFooters();
                }

                ta.UpdateQuery(Convert.ToInt32(cboHeaderFooter.SelectedValue.ToString()), groupID, name, header, footer, customCode, defaultHeaderFooter, thanksUrl);
            }
            else
            {
                // insert
                int nextID = DA.DataAccess.GetNextID("[DL.HeaderFooter]", "pk_headerFooterID");


                ta.InsertQuery(nextID, groupID, name, header, footer, customCode, thanksUrl);    
            }

            //BR.FormTemplate.UpdateFooter(footer, _footerID);
            //BR.FormTemplate.UpdateHeader(header, _headerID);

            // redirect
            Response.Redirect("~/admin/FormDevelopment/HeaderFooter.aspx");
                        
        }



        protected void cboHeaderFooter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHeaderFooter.SelectedItem.Text != "New...")
            {
                //load the record data from the database
                DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter ta = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
                DA.HeaderFooter.DL_HeaderFooterDataTable dt = new Dealer_Locator.DA.HeaderFooter.DL_HeaderFooterDataTable();

                dt = ta.GetDataByHeaderFooterID(Convert.ToInt32(cboHeaderFooter.SelectedValue.ToString()));

                txtFooter.Text = dt[0]["footerText"].ToString();
                txtHeader.Text = dt[0]["headerText"].ToString();
                txtCustomCode.Text = dt[0]["customCodeText"].ToString();
                txtName.Text = dt[0]["headerFooterName"].ToString();
                chkDefault.Checked = Convert.ToBoolean(dt[0]["defaultHeader"].ToString());
                txtThanksUrl.Text = dt[0]["thanksUrl"].ToString();
                
            } else {
                txtFooter.Text = "";
                txtHeader.Text = "";
                txtCustomCode.Text = "";
                txtName.Text = "New Header Footer";
                chkDefault.Checked = false;
                txtThanksUrl.Text = "";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter ta = new Dealer_Locator.DA.HeaderFooterTableAdapters.DL_HeaderFooterTableAdapter();
            ta.DeleteQuery(Convert.ToInt32(cboHeaderFooter.SelectedValue));

            Response.Redirect("~/admin/FormDevelopment/HeaderFooter.aspx");
        }

        protected void lnkUrl_Click(object sender, EventArgs e)
        {

        }

 



    }
}
