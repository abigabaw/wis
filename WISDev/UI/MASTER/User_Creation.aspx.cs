using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class User_Creation : System.Web.UI.Page
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
                Master.PageHeader = "User Creation";
                ViewState["UserID"] = 0;
                BindGrid();
                ClearDetails();
                ClearSearchDetails();
                LoadRoles();

                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //txtUsername.Attributes.Add("onchange", "isDirty = 1;");
                //txtDisplayName.Attributes.Add("onchange", "isDirty = 1;");
                //txtEmailID.Attributes.Add("onchange", "isDirty = 1;");
                txtUsername.Attributes.Add("onchange", "setDirtyText();");
                txtDisplayName.Attributes.Add("onchange", "setDirtyText();");
                txtEmailID.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_USER) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnUserSearch.Visible = false;
                    btnClearSearch.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    grdUsers.Columns[7].Visible = false;
                    grdUsers.Columns[8].Visible = false;
                    grdUsers.Columns[9].Visible = false;
                    foreach (GridViewRow grRow in grdUsers.Rows)
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
        //protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
        //        imgEdit.Attributes.Add("onclick", "isDirty = 0;");
        //        CheckBox IsObsolete = (CheckBox)e.Row.FindControl("IsObsolete");
        //        IsObsolete.Attributes.Add("onclick", "isDirty = 0;");
        //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
        //        imgDelete.Attributes.Add("onclick", "isDirty = 0;");
        //    }
        //}

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnUserSearch.ClientID);
            else
                stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        private void LoadRoles()
        {        
            RoleBLL objRoleBLL = new RoleBLL();
            RoleList oRoleList = objRoleBLL.GetRoles("");
            ddlRole.Items.Clear();
            ddlRole.DataSource = oRoleList;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlRole.SelectedIndex = 0;

            ddlRoleSearch.Items.Clear();
            ddlRoleSearch.DataSource = oRoleList;
            ddlRoleSearch.DataTextField = "RoleName";
            ddlRoleSearch.DataValueField = "RoleID";
            ddlRoleSearch.DataBind();
            ddlRoleSearch.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlRoleSearch.SelectedIndex = 0;
             
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
            UserBLL objUserBLL = new UserBLL();
            UserBO oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;

            grdUsers.DataSource = objUserBLL.GetAllUsers(oBOUser);
            grdUsers.DataBind();
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(UserBO oUser)
        {
            //For Search Assignemnt
            UserBLL objUserBLL = new UserBLL();

            grdUsers.DataSource = objUserBLL.GetAllUsers(oUser);
            grdUsers.DataBind();
        }

        /// <summary>
        /// Set link attributes to User link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["UserID"] = e.CommandArgument;
                GetUserDetails();
                ShowHideSections(true, false);
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteUser(Convert.ToInt32(e.CommandArgument));
                ClearDetails();
                SetUpdateMode(false);
                BindGrid();
            }
        }

        /// <summary>
        /// To get the user details
        /// </summary>
        private void GetUserDetails()
        {
            UserBLL objUserBLL = new UserBLL();
            int UserID = 0;

            if (ViewState["UserID"] != null)
                UserID = Convert.ToInt32(ViewState["UserID"].ToString());

            UserBO objUser = new UserBO();
            objUser = objUserBLL.GetUserById(UserID);

            txtUsername.Text = objUser.UserName;
            txtDisplayName.Text = objUser.DisplayName;
            txtEmailID.Text = objUser.EmailID;
            txtCellNumber.Text = objUser.CellNumber;
            ddlRole.SelectedValue = objUser.RoleID.ToString();
           
        }

        /// <summary>
        /// To delete the user
        /// </summary>
        private void DeleteUser(int userID)
        {
            string message = string.Empty;
           // try
           // {
                UserBLL objUserBLL = new UserBLL();

                message = objUserBLL.DeleteUser(userID);
                
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data Deleted successfully";
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
           // }
           // catch (Exception ex)
          //  {                
          //      throw ex;
          //  }
        }

        /// <summary>
        /// To obsolete
        /// </summary>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            //try
           // {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string userID = ((Literal)gr.FindControl("litUserID")).Text;
                UserBLL objUserBLL = new UserBLL();

                message = objUserBLL.ObsoleteUser(Convert.ToInt32(userID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
                SetUpdateMode(false);
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
           /* }
            catch (Exception ex)
            {
                throw ex;
            }/8*/
        }

        #region Clear Button & Methods

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Call ClearDetails method to Clear search data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearchDetails();
            BindGrid();
        }
        
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtUsername.Text = "";
            txtEmailID.Text = "";
            txtCellNumber.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
            ViewState["UserID"] = 0;
        }
       
        /// <summary>
        /// Clear the search data.
        /// </summary>
        private void ClearSearchDetails()
        {
            txtUserNameSearch.Text = string.Empty;
            ddlRoleSearch.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserBO objUser = new UserBO();
            UserBLL objUserBLL = new UserBLL();

            objUser.CellNumber = txtCellNumber.Text.Trim();
            objUser.DisplayName = txtDisplayName.Text.Trim();
            objUser.Pwd = txtUsername.Text.Trim();
            objUser.UserName = txtUsername.Text.Trim();
            objUser.EmailID = txtEmailID.Text.Trim();

            if (ViewState["UserID"] != null)
                objUser.UserID = Convert.ToInt32(ViewState["UserID"].ToString());

            objUser.RoleID = Convert.ToInt32(ddlRole.SelectedValue.ToString());

            objUser.IsDeleted = "False";

            objUser.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            objUser.CreatedDate = DateTime.Now;
            string AlertMessage=string.Empty;
            string message=string.Empty;

            if (objUser.UserID < 1)
            {
                //If UserID does Not exists then SaveUser
                objUser.UserID = -1;//For New User
                message = objUserBLL.AddUser(objUser);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message=="" || message=="null")
                {
                    message = "Data saved successfully";
                        ClearDetails();
                }
            }
            else
            {
                //If UserID exists then UpdateUser
                message = objUserBLL.UpdateUser(objUser);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    SetUpdateMode(false);
                }
            }

            BindGrid();

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUserSearch_Click(object sender, EventArgs e)
        {
            UserBO oUser = new UserBO();

            oUser.UserName = txtUserNameSearch.Text.Trim();
            if (ddlRoleSearch.SelectedIndex >= 0)
                oUser.RoleID = Convert.ToInt32(ddlRoleSearch.SelectedValue);
            BindGrid(oUser);
        }

        /// <summary>
        /// Show or hide the selections.
        /// </summary>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlUserDetails.Visible = false;
            pnlSearch.Visible = false;

            if (showAdd) pnlUserDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ViewState["UserID"] = "0";
            ShowHideSections(true, false);
            SetUpdateMode(false);
            BindGrid();
            ClearSearchDetails();
            ClearDetails();
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
            ClearSearchDetails();
            ClearDetails();
        }

        /// <summary>
        /// to change the page index
        /// </summary>
        protected void grdUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid();
            grdUsers.PageIndex = e.NewPageIndex;
            grdUsers.DataBind();
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
                ViewState["UserID"] = "0";
            }
        }
    }
}