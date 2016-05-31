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
    public partial class TemporaryAuthorization : System.Web.UI.Page
    {
        #region GlobalDeclaration
        TemporaryAuthorizationBLL objTemporaryAuthorizationBLL;
        TemporaryAuthorizationBO objTemporaryAuthorizationBO;
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
            CaldptxtFrom.Format = UtilBO.DateFormat;
            caldptxtTo.Format = UtilBO.DateFormat;
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Temporary Authorization";

                ViewState["TEMPAUTHID"] = 0;
                BindUser();
                Bindgrid();
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void Bindgrid()
        {
            objTemporaryAuthorizationBLL = new TemporaryAuthorizationBLL();
            grdTempAuth.DataSource = objTemporaryAuthorizationBLL.GetTempAuthorizationsByUser(Convert.ToInt32(Session["USER_ID"].ToString()), Convert.ToInt32(Session["PROJECT_ID"].ToString()));
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
                    ViewState["TEMPAUTHID"] = e.CommandArgument;
                    TemporaryAuthorizationBLL objtempBLL = new TemporaryAuthorizationBLL();
                    TemporaryAuthorizationBO objtemp = objtempBLL.GetTempAuthorizationsByID(Convert.ToInt32(ViewState["TEMPAUTHID"]));
                    ddlAssignTo.ClearSelection();
                    if (ddlAssignTo.Items.FindByValue(objtemp.AssignedTo) != null)
                        ddlAssignTo.Items.FindByValue(objtemp.AssignedTo).Selected = true;
                    dptxtFrom.Text = Convert.ToDateTime(objtemp.Fromdate).ToString(UtilBO.DateFormat);
                    dptxtTo.Text = Convert.ToDateTime(objtemp.Todate).ToString(UtilBO.DateFormat);                        
                    txtRemarks.Text = objtemp.Remarks;
                    btnSave.Text = "Update";
                    btnClear.Text = "Cancel";
                }
                else if (e.CommandName == "DeleteRow")
                {
                    TemporaryAuthorizationBLL objtempBLL = new TemporaryAuthorizationBLL();

                    objtempBLL.DeleteTempAuthorizationsByID(Convert.ToInt32(e.CommandArgument));
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
                TemporaryAuthorizationBLL objtempBLL = new TemporaryAuthorizationBLL();
                TemporaryAuthorizationBO objtemp = objtempBLL.GetTempAuthorizationsByID(Convert.ToInt32(APPROVALTEMPAUTHORISERID));

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
                DateTime Fromdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Fromdate"));
                DateTime Todate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Todate"));
                if (Fromdate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblFromDate")).Text = Fromdate.ToString(UtilBO.DateFormat);
                if (Todate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblToDate")).Text = Todate.ToString(UtilBO.DateFormat);

                CheckBox chk = ((CheckBox)e.Row.FindControl("IsObsolete"));
                chk.Enabled = !chk.Checked;
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
            objTemporaryAuthorizationBLL = new TemporaryAuthorizationBLL();
            objTemporaryAuthorizationBO = new TemporaryAuthorizationBO();
            try
            {

                
               objTemporaryAuthorizationBO.Assigntoid = Convert.ToInt32(ddlAssignTo.SelectedValue);
                objTemporaryAuthorizationBO.Authoriserid = Convert.ToInt32(Session["USER_ID"]);
                
                objTemporaryAuthorizationBO.Fromdate = Convert.ToDateTime(dptxtFrom.Text);
                objTemporaryAuthorizationBO.Todate = Convert.ToDateTime(dptxtTo.Text);
                objTemporaryAuthorizationBO.Remarks = txtRemarks.Text.Trim();
                if (objTemporaryAuthorizationBO.Remarks.Length > 500)
                    objTemporaryAuthorizationBO.Remarks = objTemporaryAuthorizationBO.Remarks.Substring(0, 499);
                objTemporaryAuthorizationBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                objTemporaryAuthorizationBO.Createdby = Convert.ToInt32(Session["USER_ID"]);
                objTemporaryAuthorizationBO.Approvaltempauthoriserid = Convert.ToInt32(ViewState["TEMPAUTHID"]);
                
                result = objTemporaryAuthorizationBLL.AddTemporaryAuthorization(objTemporaryAuthorizationBO);

                SendMailForUser("INS", Convert.ToInt32(ddlAssignTo.SelectedValue), objTemporaryAuthorizationBO);
                if (string.IsNullOrEmpty(result) || result == "" || result == "null")
                {
                    if (Convert.ToInt32(ViewState["TEMPAUTHID"]) == 0)
                        result = "Temporary Authorization added successfully";
                    else if (result == "Temp Authorization already exists in the system.")
                        result = "Temporary Authorization already exists";
                    else
                        result = "Temporary Authorization Updated successfully";
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
        /// <param name="objTemporaryAuthorizationBO"></param>
        private void SendMailForUser(string Status, int UID, TemporaryAuthorizationBO objTemporaryAuthorizationBO)
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
                mailMessage.Subject = Session["userName"].ToString() + " assign you as a Temporary authorized person for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project.";
                mailMessage.Body = "Dear Sir <br/><br/> " + Session["userName"].ToString() + " assign you as a Temporary authorized person for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project. <br/><br/> ProjectCode : " + Session["PROJECT_CODE"].ToString()
                                    + "<br/> ProjectName : " + objProject.ProjectName + "<br/>" +
                                    "<br/> From Date : " + objTemporaryAuthorizationBO.Fromdate.ToString(UtilBO.DateFormat) + "<br/>" +
                                    "<br/> To Date : " + objTemporaryAuthorizationBO.Todate.ToString(UtilBO.DateFormat) + "<br/><br/>" +
                                    "<br/> Remarks : " + objTemporaryAuthorizationBO.Remarks + "<br/><br/>" +
                                    "Thanks and Regards <br/> WIS - UETCL Team";
            }
            else
            {
                mailMessage.Subject = Session["userName"].ToString() + " is available now. Your Temporary authorization for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project was canceled.";
                mailMessage.Body = "Dear Sir <br/><br/> " + Session["userName"].ToString() + " is available now. Your Temporary authorization for all the approvals belogns to " + Session["PROJECT_CODE"].ToString() + " project was canceled. <br/><br/> ProjectCode : " + Session["PROJECT_CODE"].ToString()
                                    + "<br/> ProjectName : " + objProject.ProjectName + "<br/>" +
                                    "<br/> From Date : " + objTemporaryAuthorizationBO.Fromdate.ToString(UtilBO.DateFormat) + "<br/>" +
                                    "<br/> To Date : " + objTemporaryAuthorizationBO.Todate.ToString(UtilBO.DateFormat) + "<br/><br/>" +
                                    "<br/> Remarks : " + objTemporaryAuthorizationBO.Remarks + "<br/><br/>" +
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

            ProjectPersonalList ProjectPersonnels = objProjPersonalLogic.GetProjectPersonnel(Convert.ToInt32(Session["PROJECT_ID"]));

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
            dptxtFrom.Text = "";
            dptxtTo.Text = "";
            txtRemarks.Text = string.Empty;

            ViewState["TEMPAUTHID"] = 0;
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }

        #endregion
    }
}