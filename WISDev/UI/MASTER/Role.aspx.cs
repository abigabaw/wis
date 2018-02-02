using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class Role : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Role";
                ViewState["ROLEID"] = 0;
                BindGrid(false, false);
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //txtRoleName.Attributes.Add("onclick", "SetVisible(1);");
                txtRoleName.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_USER) == false)
                {
                    btn_Save.Visible = false;
                    btnClear.Visible = false;
                    btnSearch.Visible = false;
                    btn_Clear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    grdRoles.Columns[3].Visible = false;
                    grdRoles.Columns[4].Visible = false;
                    grdRoles.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdRoles.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)grRow.FindControl("IsObsolete");
                            chk.Enabled = false;
                        }
                    }
                }
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnSearch.ClientID);
            else
                stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            RoleBLL objRoleBLL = new RoleBLL();
            grdRoles.DataSource = objRoleBLL.GetAllRoles("");
            grdRoles.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            RoleBO objRole = new RoleBO();
            RoleBLL objroleBLL = new RoleBLL();

            objRole.RoleID = Convert.ToInt32(ViewState["ROLEID"]);
            objRole.RoleName = txtRoleName.Text.Trim();
            objRole.RoleDescription = txtDescription.Text.Trim();

            if (objRole.RoleDescription.Length >= 200)
                objRole.RoleDescription = objRole.RoleDescription.Substring(0, 198);
            
            string AlertMessage = string.Empty;
            string message = string.Empty;
            AlertMessage = "alert('" + message + "');";

            if (objRole.RoleID == 0)
            {
                objRole.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objroleBLL.AddRole(objRole);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                }
            }
            else
            {
                objRole.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objroleBLL.UpdateRole(objRole);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    SetUpdateMode(false);
                }
            }

            BindGrid(true, false);

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtRoleName.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ShowHideSections(true, false);
                    ViewState["ROLEID"] = e.CommandArgument;
                    RoleBLL objroleBLL = new RoleBLL();
                    RoleBO objRole = objroleBLL.GetRoleByRoleID(Convert.ToInt32(ViewState["ROLEID"]));
                    txtRoleName.Text = objRole.RoleName;
                    txtDescription.Text = objRole.RoleDescription;
                    SetUpdateMode(true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
                  }

                else if (e.CommandName == "DeleteRow")
                {
                    RoleBLL objroleBLL = new RoleBLL();
                    message=objroleBLL.DeleteRole(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data Deleted successfully";
                    SetUpdateMode(false);
                    BindGrid(false, true);
                    ClearDetails();
                }

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
           
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

                string roleID = ((Literal)gr.FindControl("litRoleID")).Text;
                RoleBLL objroleBLL = new RoleBLL();
                message = objroleBLL.ObsoleteRole(Convert.ToInt32(roleID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid(false, true);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
             }
           catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Used to display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRoles.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlRoleDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlRoleDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }
        /// <summary>
        /// Show Add county Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindGrid(false, false);
            SetUpdateMode(false);
            ClearDetails();
        }
        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }
        /// <summary>
        /// To display details on click of button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string RoleName;
            RoleName = txtSearch.Text.Trim();
            RoleBLL objroleBLL = new RoleBLL();
            grdRoles.DataSource = objroleBLL.GetAllRoles(RoleName);
            grdRoles.DataBind();
            ClearDetails();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails(); 
            string RoleName;
            RoleName = txtSearch.Text.Trim();
            RoleBLL objroleBLL = new RoleBLL();
            grdRoles.DataSource = objroleBLL.GetRoles(RoleName);
            grdRoles.DataBind();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["ROLEID"] = "0";
            }
        }
    }
}

