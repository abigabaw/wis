using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class Approvals : System.Web.UI.Page
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
                Master.PageHeader = " Approvals Report";

                BindProjects();
                BindUser();
            }
            caldpApprStartDate.Format = UtilBO.DateFormat;
            CaldpApprEndDate.Format = UtilBO.DateFormat;

        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindUser()
        {
            RolePrivilegesBO objRolePrivileges = new RolePrivilegesBO();
            RolePrivilegesBLL RolePrivilegesBLLobj = new RolePrivilegesBLL();

            UserBLL objUserBLL = new UserBLL();
            UserList objUserList = new UserList();
            UserBO oBOUser = null;
            oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;

            ddlAssignTo.DataSource = objUserBLL.GetUsers(oBOUser);
            ddlAssignTo.DataTextField = "UserName";
            ddlAssignTo.DataValueField = "UserID";
            ddlAssignTo.DataBind();
        }
        /// <summary>
        /// To bind values to  dropdownlist
        /// </summary>
        private void BindProjects()
        {
            drpProject.DataSource = (new ProjectBLL()).GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            drpProject.DataTextField = "ProjectName";
            drpProject.DataValueField = "ProjectID";
            drpProject.DataBind();
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlAssignTo.ClearSelection();
            ddlAssignTo.SelectedIndex = 0;
            drpProject.ClearSelection();
            drpProject.SelectedIndex = 0;
            dpApprStartDate.Text = "";
            dpApprEndDate.Text = "";

        }
    }
}