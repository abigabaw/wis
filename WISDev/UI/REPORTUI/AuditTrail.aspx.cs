using System;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class AuditTrail : System.Web.UI.Page
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
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Audit Trail";
                else
                    Master.PageHeader = "Audit Trail";

            
                BindProject();
                BindUser();
            }

            calDPFromDate.Format = UtilBO.DateFormat;
            CalDPToDate.Format = UtilBO.DateFormat;
        }
        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindProject()
        {
            ProjectBLL BLLobj = new ProjectBLL();
            ddlProject.DataSource = BLLobj.GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            ddlProject.DataTextField = "projectName";
            ddlProject.DataValueField = "projectID";
            ddlProject.DataBind();

            if (Session["PROJECT_ID"] != null)
            {
                if (ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                {
                    ddlProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;
                }
            }
        }

        /// <summary>
        /// To bind values to dropdownlist
        /// </summary>
        private void BindUser()
        {
            ListItem firstListItem = new ListItem(ddlActionBy.Items[0].Text, ddlActionBy.Items[0].Value);
            ddlActionBy.Items.Clear();

            UserBLL objUserBLL = new UserBLL();
            UserBO oBOUser = new UserBO();
            oBOUser.UserName = string.Empty;
            oBOUser.UserID = 0;
            oBOUser.RoleID = 0;

            ddlActionBy.DataSource = objUserBLL.GetUsers(oBOUser);
            ddlActionBy.DataTextField = "UserName";
            ddlActionBy.DataValueField = "UserID";
            ddlActionBy.DataBind();

            ddlActionBy.Items.Insert(0, firstListItem);         
        }
        /// <summary>
        /// To clear input fields and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlActionBy.ClearSelection();
            DPFromDate.Text = "";
            DPToDate.Text = "";
            ddlProject.ClearSelection();
        }
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }     
    }
}