using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class MaxCapDistrict : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - MAXCAP";
                else
                    Response.Redirect("ViewProjects.aspx");
                //Master.PageHeader = "MAXCAP";
                ViewState["MAXCAPID"] = 0;
                GetDistrictName();
                BindGrid();
                getFrozen();
                txtMaxCap.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 1].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 2].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 3].Visible = false;
                }
            }

        }

        /// <summary>
        /// Check Frozen
        /// </summary>
        private void getFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string SessionValue = Session["FROZEN"].ToString();
                if (SessionValue == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 1].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 2].Visible = false;
                    grdDistrict.Columns[grdDistrict.Columns.Count - 3].Visible = false;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            //StringBuilder stb = new StringBuilder();

            //stb.Append("\n<script language=\"javascript\">\n<!--\n");

            //stb.Append("var LOGIN_BUTTON_ID = \"");
            //if (hfVisible.Value.Trim() == "1")
            //    //stb.Append(btnDistrictSearch.ClientID);
            //else
            //    stb.Append(btnSave.ClientID);
            //stb.Append("\";\n");

            //stb.Append("-->\n</script>\n");

            return "";
        }
        /// <summary>
        /// to get district names
        /// </summary>
        /// <returns></returns>
        private void GetDistrictName()
        {
            DistrictBLL BLLobj = new DistrictBLL();

            ddlDistrictName.DataSource = BLLobj.GetDistrict();
            ddlDistrictName.DataTextField = "DISTRICTNAME";
            ddlDistrictName.DataValueField = "DISTRICTID";
            ddlDistrictName.DataBind();

            //DDLSearchDistrictName.DataSource = BLLobj.GetDistrict();
            //DDLSearchDistrictName.DataTextField = "DISTRICTNAME";
            //DDLSearchDistrictName.DataValueField = "DISTRICTID";
            //DDLSearchDistrictName.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            MaxCapBLL DistrictBLLobj = new MaxCapBLL();
            grdDistrict.DataSource = DistrictBLLobj.GetAllMaxCap(Convert.ToInt32(Session["PROJECT_ID"]));
            grdDistrict.DataBind();

        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <returns></returns>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdDistrict.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to get District names based on ID
        /// </summary>
        /// <returns></returns>
        private void GetDistrictById()
        {
            MaxCapBLL DistrictBLLobj = new MaxCapBLL();

            MaxCapBO DistrictBOobj = DistrictBLLobj.GetMaxCapById(Convert.ToInt32(ViewState["MAXCAPID"]));

            if (DistrictBOobj != null)
                txtMaxCap.Text =Convert.ToString(DistrictBOobj.MaxCapVal);
            ddlDistrictName.ClearSelection();
            if (ddlDistrictName.Items.FindByText(DistrictBOobj.DistrictName.ToString()) != null)
                ddlDistrictName.Items.FindByText(DistrictBOobj.DistrictName.ToString()).Selected = true;
            DistrictBOobj = null;
            DistrictBLLobj = null;
        }
        /// <summary>
        /// Update Database Make data as Obsoluted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string MAXCAPID = ((Literal)gr.FindControl("litDistrictID")).Text;

                MaxCapBLL DistrictBLLobj = new MaxCapBLL();
                message = DistrictBLLobj.ObsoleteMaxCap(Convert.ToInt32(MAXCAPID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// to save data to database
        /// </summary>
        /// <returns></returns>
        private void SaveDistrict()
        {
            MaxCapBLL DistrictBLLobj = new MaxCapBLL();
            MaxCapBO DistrictBOobj = new MaxCapBO();

            string message = "";


            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            DistrictBOobj.DistrictID = Convert.ToInt32(ddlDistrictName.SelectedItem.Value);
            //DistrictBOobj.MaxCapID = Convert.ToInt32(ViewState["MAXCAPID"]);
            DistrictBOobj.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            DistrictBOobj.MaxCapVal = Convert.ToDecimal(txtMaxCap.Text.Trim());
            DistrictBOobj.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = DistrictBLLobj.AddMaxCap(DistrictBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                BindGrid();
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                DistrictBLLobj = null;
            }
        }
        /// <summary>
        /// to update data to database
        /// </summary>
        /// <returns></returns>
        private void UpdateDistrict()
        {
            MaxCapBLL DistrictBLLobj = new MaxCapBLL();
            MaxCapBO DistrictBOobj = new MaxCapBO();
            string message = "";

            try
            {
                if (ViewState["MAXCAPID"] != null)
                    DistrictBOobj.MaxCapID = Convert.ToInt32(ViewState["MAXCAPID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                DistrictBOobj.DistrictID = Convert.ToInt32(ddlDistrictName.SelectedItem.Value);
                DistrictBOobj.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                DistrictBOobj.MaxCapVal =Convert.ToDecimal(txtMaxCap.Text.Trim());
                DistrictBOobj.UpdatedBy = Convert.ToInt32(uID);

                message = DistrictBLLobj.UpdateMaxCap(DistrictBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                ClearData();
                SetUpdateMode(false);
                BindGrid();

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DistrictBLLobj = null;
            }
        }
        /// <summary>
        /// calls save method
        /// </summary>
        /// <returns></returns>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveDistrict();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateDistrict();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// calls clear method
        /// </summary>
        /// <returns></returns>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearData();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
            BindGrid();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlDistrictName.ClearSelection();
            txtMaxCap.Text = string.Empty;

            SetUpdateMode(false);



        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["MAXCAPID"] = "0";
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["MAXCAPID"] = e.CommandArgument;
                GetDistrictById();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                MaxCapBLL DistrictBLLobj = new MaxCapBLL();
                message = DistrictBLLobj.DeleteMaxCap(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                ClearData();
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// to change values in dropdownlist based on index 
        /// </summary>
        protected void ddlDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        public void searchBind(string District)
        {
            MaxCapBLL DistrictBLLobj = new MaxCapBLL();
            grdDistrict.DataSource = DistrictBLLobj.GetMaxCap(District);
            grdDistrict.DataBind();
            // DDLSearchDistrictName.ClearSelection();
            //txtSearchDistrict.Text = string.Empty;
        }
        /// <summary>
        /// to change values in dropdownlist based on index 
        /// </summary>
        protected void DDLSearchDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }
    }

}