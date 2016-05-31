using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Net.Mail;
using System.Net;
using System.Configuration;


namespace WIS.UI.MYACTIVITIES
{
    public partial class SharedAuthorization : System.Web.UI.Page
    {
        #region GlobalDeclaration
        SharedAuthorizationBLL oSharedAuthorizationBLL;
        SharedAuthorizationBO oSharedAuthorizationBO;
        #endregion

        #region PageEvent
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //CaldptxtFrom.Format = UtilBO.DateFormat;
            //caldptxtTo.Format = UtilBO.DateFormat;
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Shared Authorization";

                ViewState["SharedUser"] = 0;
                BindUser();
                ScreenIntiallization();
                getModule();
                Bindgrid();

                txtRemarks.Attributes.Add("maxlength",txtRemarks.MaxLength.ToString());
            }
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_TEMP_AUTHORIZATION) == false)
            {
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdTempAuth.Columns[grdTempAuth.Columns.Count - 1].Visible = false;
                grdTempAuth.Columns[grdTempAuth.Columns.Count - 2].Visible = false;
            }
        }
        /// <summary>
        /// Calls methods required for initialization of data on screen load
        /// </summary>
        public void ScreenIntiallization()
        {
            getModule(); //workflow Module
            getWorkfolwitemByModuleID(); //wor
        }

        /// <summary>
        /// method to bind data to ModuleDropDownList from database
        /// </summary>
        public void getModule()
        {
            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            objWorkFlowList = WorkFlowBLLobj.getAllModule();

            ddlModuleDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            ddlModuleDropDownList.DataTextField = "MODULENAME";
            ddlModuleDropDownList.DataValueField = "MODULEID";
            ddlModuleDropDownList.DataBind();
            ddlModuleDropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlModuleDropDownList.SelectedIndex = 0;
        }

        /// <summary>
        ///  method to bind data to WorkflowItemDropDownList from database
        /// </summary>
        public void getWorkfolwitemByModuleID()
        {
            int ModuleID = Convert.ToInt32(ddlModuleDropDownList.SelectedItem.Value.ToString());

            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            objWorkFlowList = WorkFlowBLLobj.GetWorkFlowByModuleId(ModuleID);

            ListItem lstItem = ddlWorkflowItemDropDownList.Items[0];
            ddlWorkflowItemDropDownList.Items.Clear();

            if (objWorkFlowList.Count > 0)
            {
                ddlWorkflowItemDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
                ddlWorkflowItemDropDownList.DataTextField = "WorkDesc";
                ddlWorkflowItemDropDownList.DataValueField = "WorkflowID";
                ddlWorkflowItemDropDownList.DataBind();
            }

            ddlWorkflowItemDropDownList.Items.Insert(0, lstItem);
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void Bindgrid()
        {
            oSharedAuthorizationBLL = new SharedAuthorizationBLL();
            int UserId = 0, ProjectId = 0;
            if (Session["USER_ID"] != null)
                UserId = Convert.ToInt32(Session["USER_ID"].ToString());
            if (Session["PROJECT_ID"] != null)
                ProjectId = Convert.ToInt32(Session["PROJECT_ID"].ToString());
            grdTempAuth.DataSource = oSharedAuthorizationBLL.GetSharedAuthorizationsByUser(UserId, ProjectId);
            grdTempAuth.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTempAuth_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ViewState["SharedUser"] = e.CommandArgument;
                    SharedAuthorizationBLL objtempBLL = new SharedAuthorizationBLL();
                    SharedAuthorizationBO objtemp = objtempBLL.GetSharedAuthorizationsByID(Convert.ToInt32(ViewState["SharedUser"].ToString()));

                    ddlAssignTo.ClearSelection();
                    ddlAssignTo.SelectedValue = objtemp.AssignedToId.ToString();
                    ddlModuleDropDownList.ClearSelection();
                    ddlModuleDropDownList.SelectedValue = objtemp.ModuleId.ToString();
                    getWorkfolwitemByModuleID();
                    ddlWorkflowItemDropDownList.ClearSelection();
                    ddlWorkflowItemDropDownList.SelectedValue = objtemp.WorkFlowId.ToString();
                   
                    txtRemarks.Text = objtemp.Remarks.ToString();
                    btnSave.Text = "Update";
                    btnClear.Text = "Cancel";
                }
                else if (e.CommandName == "DeleteRow")
                {
                    SharedAuthorizationBLL objtempBLL = new SharedAuthorizationBLL();

                    objtempBLL.DeleteSharedAuthorizationsByID(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";
                    ClearDetails();
                    Bindgrid();
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
                string APPROVALTEMPAUTHORISERID = ((Literal)gr.FindControl("litLineTypeID")).Text;
                SharedAuthorizationBLL objtempBLL = new SharedAuthorizationBLL();
                SharedAuthorizationBO objtemp = objtempBLL.GetSharedAuthorizationsByID(Convert.ToInt32(APPROVALTEMPAUTHORISERID));

                objtempBLL.ObsoleteTempAuthorizationsByID(Convert.ToInt32(APPROVALTEMPAUTHORISERID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Updated successfully";
                string status = "";
                if (!chk.Checked)
                    status = "INS";
                SendMailForUser(status, Convert.ToInt32(objtemp.AssignedTo), objtemp);
                ClearDetails();
                Bindgrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTempAuth_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdTempAuth.PageIndex = e.NewPageIndex;
            Bindgrid();
        }
        /// <summary>
        /// To select date and checkbox from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdTempAuth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DateTime Fromdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Fromdate"));
                //DateTime Todate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Todate"));
                //if (Fromdate != DateTime.MinValue)
                //    ((Label)e.Row.FindControl("lblFromDate")).Text = Fromdate.ToString(UtilBO.DateFormat);
                //if (Todate != DateTime.MinValue)
                //    ((Label)e.Row.FindControl("lblToDate")).Text = Todate.ToString(UtilBO.DateFormat);

                //CheckBox chk = ((CheckBox)e.Row.FindControl("IsObsolete"));
                //chk.Enabled = !chk.Checked;
            }
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            string AlertMessage = string.Empty;
            oSharedAuthorizationBLL = new SharedAuthorizationBLL();
            oSharedAuthorizationBO = new SharedAuthorizationBO();
            try
            {
                if (ViewState["SharedUser"] != null)
                    oSharedAuthorizationBO.WorkFlowSharedId = Convert.ToInt32(Convert.ToInt32(ViewState["SharedUser"].ToString()));
                oSharedAuthorizationBO.AssignedToId = Convert.ToInt32(ddlAssignTo.SelectedValue);
                oSharedAuthorizationBO.AuthoriserId = Convert.ToInt32(Session["USER_ID"]);

                oSharedAuthorizationBO.ModuleId = Convert.ToInt32(ddlModuleDropDownList.Text);
                oSharedAuthorizationBO.WorkFlowId = Convert.ToInt32(ddlWorkflowItemDropDownList.Text);
                oSharedAuthorizationBO.Remarks = txtRemarks.Text.Trim();
                if (oSharedAuthorizationBO.Remarks.Length > 500)
                    oSharedAuthorizationBO.Remarks = oSharedAuthorizationBO.Remarks.Substring(0, 499);
                oSharedAuthorizationBO.ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                oSharedAuthorizationBO.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                // oSharedAuthorizationBO.AuthoriserId = Convert.ToInt32(ViewState["SharedUser"]);

                //if (ViewState["SharedUser"] != null && Convert.ToInt32(ViewState["SharedUser"].ToString()) == 0)
                //{
                //    result = oSharedAuthorizationBLL.AddSharedAuthorization(oSharedAuthorizationBO);
                //}
                //else
                result = oSharedAuthorizationBLL.AddSharedAuthorization(oSharedAuthorizationBO);

                if (string.IsNullOrEmpty(result) || result == "" || result == "null")
                {
                    if (Convert.ToInt32(ViewState["SharedUser"]) == 0)
                    {
                        result = "Shared Authorization added successfully";
                        SendMailForUser("INS", Convert.ToInt32(ddlAssignTo.SelectedValue), oSharedAuthorizationBO);
                    }
                    else if (result == "Shared Authorization already exists in the system.")
                        result = "Shared Authorization already exists";
                    else
                    {
                        result = "Shared Authorization Updated successfully";
                        SendMailForUser("INS", Convert.ToInt32(ddlAssignTo.SelectedValue), oSharedAuthorizationBO);
                    }
                    AlertMessage = "alert('" + result + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                }
                else
                {
                    AlertMessage = "alert('" + result + "');";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

                ClearDetails();
            }
            catch (Exception ex)
            {
                AlertMessage = "alert('" + ex + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }

            Bindgrid();
        }
        /// <summary>
        /// To generate mail to the user
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="UID"></param>
        /// <param name="oSharedAuthorizationBO"></param>
        private void SendMailForUser(string Status, int UID, SharedAuthorizationBO oSharedAuthorizationBO)
        {
            UserBLL objUserBLL = new UserBLL();
            UserBO objUser = new UserBO();
            objUser = objUserBLL.GetUserById(UID);
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));
            string fileName = string.Empty;


            //NotificationObj.SendChangeRequestEmail(EmailToTextBox.Text, EmailSubjectTextBox.Text, EmailBodyTextBox.Text);
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.To.Add(objUser.EmailID.Trim());
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("FromMailAddress"));
            if (Status == "INS")
            {
                mailMessage.Subject = Session["userName"].ToString() + " assign you as a Shared authorized persion for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project.";
                mailMessage.Body = "Dear Sir <br/><br/> " + Session["userName"].ToString() + " assign you as a Shared authorized person for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project. <br/><br/> ProjectCode : " + Session["PROJECT_CODE"].ToString()
                                    + "<br/> ProjectName : " + objProject.ProjectName + "<br/>" +
                                    "<br/> Authoriser Name : " + oSharedAuthorizationBO.AuthoriserName + "<br/>" +
                                    "<br/> Assigned To: " + oSharedAuthorizationBO.AssignedTo + "<br/><br/>" +
                                    "<br/> Remarks : " + oSharedAuthorizationBO.Remarks + "<br/><br/>" +
                                    "Thanks and Regards <br/> WIS - UETCL Team";
            }
            else
            {
                mailMessage.Subject = Session["userName"].ToString() + " is available now. Your Shared authorization for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project was canceled.";
                mailMessage.Body = "Dear Sir <br/><br/> " + Session["userName"].ToString() + " is available now. Your Shared authorization for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project was canceled. <br/><br/> ProjectCode : " + Session["PROJECT_CODE"].ToString()
                                    + "<br/> ProjectName : " + objProject.ProjectName + "<br/>" +
                                    "<br/> Authoriser Name : " + oSharedAuthorizationBO.AuthoriserName + "<br/>" +
                                    "<br/> Assigned To: : " + oSharedAuthorizationBO.AssignedTo + "<br/><br/>" +
                                    "<br/> Remarks : " + oSharedAuthorizationBO.Remarks + "<br/><br/>" +
                                    "Thanks and Regards <br/> WIS - UETCL Team";
            }
            mailMessage.IsBodyHtml = true;

            smtp.Send(mailMessage);
        }
        /// <summary>
        ///calls clear method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();

        }

        #endregion

        #region Methods
        //private void BindRole()
        //{
        //    objRoleBLL = new RoleBLL();
        //    objRoleList = objRoleBLL.GetRoles("");
        //    if (objRoleList.Count > 0)
        //    {
        //        ddlAssignTo.DataSource = objRoleList;
        //        ddlAssignTo.DataTextField = "RoleName";
        //        ddlAssignTo.DataValueField = "RoleID";
        //        ddlAssignTo.DataBind();
        //    }
        //}
        /// <summary>
        /// To bind values from database to Assignto dropdownlist
        /// </summary>
        private void BindUser()
        {
            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();
            int UserId = 0, ProjectId = 0;
            if (Session["PROJECT_ID"] != null)
            {
                ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                //UserId = Convert.ToInt32(Session["USER_ID"]);
            }
            if (Session["USER_ID"] != null)
            {
                //ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                UserId = Convert.ToInt32(Session["USER_ID"]);
            }

            ProjectPersonalList ProjectPersonnels = objProjPersonalLogic.GetProjectOtherPersonnel(ProjectId, UserId);

            ddlAssignTo.DataSource = ProjectPersonnels;
            ddlAssignTo.DataTextField = "UserName";
            ddlAssignTo.DataValueField = "UserID";
            ddlAssignTo.DataBind();
        }
        /// <summary>
        /// To clear details and load default data
        /// </summary>
        private void ClearDetails()
        {
            ddlAssignTo.SelectedIndex = 0;
            //dptxtFrom.Text = "";
            //dptxtTo.Text = "";

            ddlModuleDropDownList.SelectedIndex = 0;
            ddlWorkflowItemDropDownList.SelectedIndex = 0;
            txtRemarks.Text = string.Empty;
            ScreenIntiallization();

            ViewState["SharedUser"] = 0;
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }

        #endregion

        protected void ddlModuleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getWorkfolwitemByModuleID(); //wor
        }
    }
}