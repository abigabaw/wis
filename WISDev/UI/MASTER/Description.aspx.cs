using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS.UI.MASTER
{
    public partial class Description : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userName"] != null)
            {
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                ddlparam.Enabled = false;
                bindParam();
                bindoption();
                bindGrid();
            }
        }
        protected void ddloptionAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlparam.Items.Clear();
            ddlparam.Enabled = true;
            ddlparam.DataSource = (new DescriptionBLL()).GetParameters(Convert.ToInt32(ddloptionAvailable.SelectedValue));
            ddlparam.DataTextField = "ParameterName";
            ddlparam.DataValueField = "ParameterID";
            ddlparam.DataBind();
            ddlparam.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        private void bindGrid()
        {
            DescriptionList objDescriptionList = new DescriptionList();
            DescriptionBLL objdescriptionBLL = new DescriptionBLL();
            objDescriptionList = objdescriptionBLL.GetAllDescriptionDetails();
            grdDescription.DataSource = objDescriptionList;
            grdDescription.DataBind();
        }

        private void bindoption()
        {
            ddloptionAvailable.DataSource = (new DescriptionBLL()).GetOptionAvail();
            ddloptionAvailable.DataTextField = "OptionAvailablename";
            ddloptionAvailable.DataValueField = "OptionID";
            ddloptionAvailable.DataBind();
        }

        private void bindParam()
        {
            ddlparam.DataSource = (new DescriptionBLL()).GetParameters(0);
            ddlparam.DataTextField = "ParameterName";
            ddlparam.DataValueField = "ParameterID";
            ddlparam.DataBind();
        }
        private void SaveDescription()
        {
            DescriptionBLL descriptionBLL = new DescriptionBLL();
            DescriptionBO DescriptionBO = new DescriptionBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            // objSubCountyBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            DescriptionBO.ParameterID = Convert.ToInt32(ddlparam.SelectedItem.Value);
            DescriptionBO.OptionAvailID = Convert.ToInt32(ddloptionAvailable.SelectedItem.Value);
            if (txtDescription.Text != "")
            {
                DescriptionBO.Description = txtDescription.Text.Trim();
            }
            DescriptionBO.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = descriptionBLL.AddDescription(DescriptionBO);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                bindGrid();

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                descriptionBLL = null;
            }

        }
        private void ClearData()
        {
            ddlparam.ClearSelection();
            ddloptionAvailable.ClearSelection();
            txtDescription.Text = string.Empty;
            ddlparam.Enabled = false;
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
            //SetUpdateMode(false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                SaveDescription();
            }
            else if (btnSave.Text == "Update")
            {
                updateDescription();
            }

        }

        private void updateDescription()
        {
            DescriptionBLL descriptionBLL = new DescriptionBLL();
            DescriptionBO DescriptionBO = new DescriptionBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            DescriptionBO.DescriptionID = Convert.ToInt32(ViewState["DescriptionID"]);
            DescriptionBO.ParameterID = Convert.ToInt32(ddlparam.SelectedItem.Value);
            DescriptionBO.OptionAvailID = Convert.ToInt32(ddloptionAvailable.SelectedItem.Value);
            if (txtDescription.Text != "")
            {
                DescriptionBO.Description = txtDescription.Text.Trim();
            }
            DescriptionBO.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = descriptionBLL.UpdateDesription(DescriptionBO);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Updated successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                bindGrid();
                btnSave.Text = "Save";
                btnClear.Text = "Clear";

            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                descriptionBLL = null;
            }

        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();

        }

        protected void grdDescription_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["DescriptionID"] = Convert.ToInt32(e.CommandArgument);
                int desID = Convert.ToInt32(ViewState["DescriptionID"]);

                DescriptionBLL objDescriptionBLL = new DescriptionBLL();
                DescriptionBO objdesc = new DescriptionBO();
                objdesc = objDescriptionBLL.GetAllDescriptionDetailsByID(desID);
                ddlparam.SelectedValue = Convert.ToString(objdesc.ParameterID);
                ddloptionAvailable.SelectedValue = Convert.ToString(objdesc.OptionAvailID);
                txtDescription.Text = objdesc.Description;
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
                ddlparam.Enabled = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                string result = "";
                ViewState["DescriptionDelID"] = Convert.ToInt32(e.CommandArgument);
                int desID = Convert.ToInt32(ViewState["DescriptionDelID"]);
                DescriptionBLL objDescriptionBLL = new DescriptionBLL();
                DescriptionBO objdesc = new DescriptionBO();
                result = objDescriptionBLL.DeleteDescription(desID);
                if (string.IsNullOrEmpty(result) || result == "" || result == "null")
                {
                    result = "Data deleted successfully";
               
                }
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + result + "');", true);
                    bindGrid();

 
            }
           
        }
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string DescID = ((Literal)gr.FindControl("litSUBCOUNTYID")).Text;

                DescriptionBLL objDescriptionBLL = new DescriptionBLL();
                message = objDescriptionBLL.ObsoleteDescription(Convert.ToInt32(DescID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                bindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdDescription_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grdDescription.PageIndex = e.NewPageIndex;
            bindGrid();

        }
    }
}
