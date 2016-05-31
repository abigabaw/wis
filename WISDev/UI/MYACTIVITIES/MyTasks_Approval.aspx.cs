using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using WIS_DataAccess;
using System.Text;

namespace WIS
{
    public partial class MyTasks_Approval : System.Web.UI.Page
    {
        string projectNameDisp = string.Empty;
        int decCount = 0, chkcount = 0;

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
                Master.PageHeader = "Approval";
                BindGrid(false, false);
                pnlAprovalFooter.Visible = false;
                trackHeaderIDLabel.Text = "0";
                trackHeaderIDLabel.Visible = false;
                PnlProjectDtl.Visible = false;

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_APPROVALS) == false)
                {
                    BtnApprove.Visible = false;
                    btnDecline.Visible = false;
                    foreach (GridViewRow grRow in GrdMyTaskApproval.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            LinkButton linkPendingCount = (LinkButton)grRow.FindControl("linkPendingCount");
                            linkPendingCount.Enabled = false;
                            LinkButton linkapprovedCount = (LinkButton)grRow.FindControl("linkapprovedCount");
                            linkapprovedCount.Enabled = false;
                            LinkButton linkDeclinedCount = (LinkButton)grRow.FindControl("linkDeclinedCount");
                            linkDeclinedCount.Enabled = false;
                        }
                    }
                }
            }
        }

        #region First Grid
        /// <summary>
        /// Set Data to Grid GrdMyTaskApproval
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            MyTasks_ApprovalBLL objMyTaskApproval = new MyTasks_ApprovalBLL();
            int USER_ID = 0;
            if (Session["USER_ID"] != null)
                USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());

            GrdMyTaskApproval.DataSource = objMyTaskApproval.GetMyTaskDetails(USER_ID);
            GrdMyTaskApproval.DataBind();

            if (GrdMyTaskApproval.Rows.Count > 0)
            {
                foreach (GridViewRow oRow in GrdMyTaskApproval.Rows)
                {
                    LinkButton linkPendingCount = oRow.FindControl("linkPendingCount") as LinkButton;
                    LinkButton linkapprovedCount = oRow.FindControl("linkapprovedCount") as LinkButton;
                    LinkButton linkDeclinedCount = oRow.FindControl("linkDeclinedCount") as LinkButton;

                    if (linkPendingCount.Text == "0")
                    {
                        linkPendingCount.Enabled = false;
                    }
                    if (linkapprovedCount.Text == "0")
                    {
                        linkapprovedCount.Enabled = false;
                    }
                    if (linkDeclinedCount.Text == "0")
                    {
                        linkDeclinedCount.Enabled = false;
                    }
                }
                pnlAprovalFooter.Visible = false;
                PnlProjectDtl.Visible = false;
            }
            else if (GrdMyTaskApproval.Rows.Count == 0)
            {
                pnlAprovalFooter.Visible = false;
                PnlProjectDtl.Visible = false;
            }

            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SHARED_APPROVALS) == false)
            {
                BtnApprove.Visible = false;
                btnDecline.Visible = false;
                foreach (GridViewRow grRow in GrdMyTaskApproval.Rows)
                {
                    if (grRow.RowType == DataControlRowType.DataRow)
                    {
                        LinkButton linkPendingCount = (LinkButton)grRow.FindControl("linkPendingCount");
                        linkPendingCount.Enabled = false;
                        LinkButton linkapprovedCount = (LinkButton)grRow.FindControl("linkapprovedCount");
                        linkapprovedCount.Enabled = false;
                        LinkButton linkDeclinedCount = (LinkButton)grRow.FindControl("linkDeclinedCount");
                        linkDeclinedCount.Enabled = false;
                    }
                }
            }

        }
        /// <summary>
        /// To Get Approved or Declined or Pending Request Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdMyTaskApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ProjectId; string ModuleId; string Status;

            //if (GrdMyTaskApproval.FindControl("lblProjectID") is Label)
            //{
            //    Label lblProjectId1 = (Label)GrdMyTaskApproval.FindControl("lblProjectID");
            //    ViewState["ProjectId"] = lblProjectId1.Text;//For Fetching the Root Appovals in Third Grid
            //}

            if (e.CommandName == "ClickApprovedCount")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectId = (Label)row.FindControl("lblProjectID");
                Label lblModuleId = (Label)row.FindControl("lblModuleId");
                Literal ltrProjectName = (Literal)row.FindControl("ltrProjectName");
                if (row != null)
                {
                    ProjectId = lblProjectId.Text;
                    ViewState["ProjectId"] = ProjectId;
                    ModuleId = lblModuleId.Text;
                    ViewState["MODULE_ID"] = ModuleId;
                    Status = "APPROVED";
                    BindDetailGrid(ProjectId, ModuleId, Status);
                    lblTrackerDetail.Text = ltrProjectName.Text + " - " + row.Cells[2].Text.ToString() + " - Approved";//row.Cells[1].Text.ToString()
                    ViewState["Status"] = "APPROVED";

                }
            }
            else if (e.CommandName == "ClickPendingCount")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectId = (Label)row.FindControl("lblProjectID");
                Label lblModuleId = (Label)row.FindControl("lblModuleId");
                Literal ltrProjectName = (Literal)row.FindControl("ltrProjectName");
                if (row != null)
                {
                    ProjectId = lblProjectId.Text;
                    ViewState["ProjectId"] = ProjectId;
                    ModuleId = lblModuleId.Text;
                    ViewState["MODULE_ID"] = ModuleId;
                    Status = "PENDING";
                    BindDetailGrid(ProjectId, ModuleId, Status);
                    lblTrackerDetail.Text = ltrProjectName.Text + " - " + row.Cells[2].Text.ToString() + " - Pending";
                    ViewState["Status"] = "PENDING";
                }
            }

            else if (e.CommandName == "ClickDeclinedCount")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblProjectId = (Label)row.FindControl("lblProjectID");
                Label lblModuleId = (Label)row.FindControl("lblModuleId");
                Literal ltrProjectName = (Literal)row.FindControl("ltrProjectName");
                if (row != null)
                {
                    ProjectId = lblProjectId.Text;
                    ViewState["ProjectId"] = ProjectId;
                    ModuleId = lblModuleId.Text;
                    ViewState["MODULE_ID"] = ModuleId;
                    Status = "DECLINED";
                    BindDetailGrid(ProjectId, ModuleId, Status);
                    lblTrackerDetail.Text = ltrProjectName.Text + " - " + row.Cells[2].Text.ToString() + " - Declined";
                    ViewState["Status"] = "DECLINED";

                }
            }
            ApprovalMultiView.ActiveViewIndex = 0;
            ApprovalMultiView.Visible = false;
            pnlAprovalFooter.Visible = false;
        }
        /// <summary>
        /// To Chenge Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdMyTaskApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdMyTaskApproval.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
        }
        /// <summary>
        /// To hide Repeted Values inside the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdMyTaskApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltrProjectName = (Literal)e.Row.FindControl("ltrProjectName");

                //  CheckBox chkSelectRoute = (CheckBox)e.Row.FindControl("chkSelectRoute");

                //if (chkSelectRoute is CheckBox)
                //    chkSelectRoute.Checked = false;
                //Text='<%#Eval("ProjectName") %>'
                if (ltrProjectName.Text.ToUpper() == projectNameDisp.ToUpper())
                {
                    //GrdMyTaskApproval.Columns[1].Visible = false;
                    ltrProjectName.Visible = false;
                }
                else
                {
                    projectNameDisp = ltrProjectName.Text;
                }
            }
        }
        #endregion

        #region Second grid
        /// <summary>
        /// To Bind data to GrdProjectDtl grid
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        private void BindDetailGrid(string ProjectId, string ModuleId, string Status)
        {
            MyTasks_ApprovalBLL objMyTaskApproval = new MyTasks_ApprovalBLL();
            int USERIDIN_ = Convert.ToInt32(Session["USER_ID"]);
            GrdProjectDtl.DataSource = objMyTaskApproval.GetMyTrackHdrDetails(ProjectId, ModuleId, Status, USERIDIN_);
            GrdProjectDtl.DataBind();
            if (GrdProjectDtl.Rows.Count > 0)
            {
                PnlProjectDtl.Visible = true;
            }
            pnlAprovalFooter.Visible = false;
            txtapprovercomments.Text = string.Empty;
        }
        /// <summary>
        /// To Check And show Related data for Selected work flow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdProjectDtl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LblHhidBatch.Text = "";
            spanPackage.Style.Add("display", "none");
            string WorkFlowCode;
            ApprovalMultiView.Visible = true;
            GetApproavlComments();
            if (e.CommandName == "ClickWorkflow")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                LinkButton lblWorkflowCode = (LinkButton)row.FindControl("linkWorkflowcode");
                Label lblWORKFLOWAPPROVERID = (Label)row.FindControl("lblWORKFLOWAPPROVERID");
                Label lbllblWORKFLOWDEFINITIONID = (Label)row.FindControl("lblWORKFLOWDEFINITIONID");
                Label lblWorkFlowId = (Label)row.FindControl("lblWorkFlowId");
                Label lblHHId = (Label)row.FindControl("lblHHId");
                Label lblApproverLevel = (Label)row.FindControl("lblApproverLevel");
                Label lblElementID = (Label)row.FindControl("lblElementID");
                Literal litTrackerDetailID = (Literal)row.FindControl("litTrackerDetailID");
                Literal litWORKFLOWCODE = (Literal)row.FindControl("litWORKFLOWCODE");

                Literal litPageCode = (Literal)row.FindControl("litPageCode");
                Literal litWorkflowDes = (Literal)row.FindControl("litWorkflowDes");
                Literal TrackHdrId = (Literal)row.FindControl("litTrackHdrId");

                string TrackerDetailID = litTrackerDetailID.Text.ToString();

                ViewState["ElementID"] = lblElementID.Text;
                ViewState["ApproverLevel"] = lblApproverLevel.Text;
                ViewState["HHID"] = lblHHId.Text;
                ViewState["PageCode"] = litPageCode.Text;
                ViewState["TrackHdrId"] = TrackHdrId.Text.ToString();

                int myActiveHHID = 0;
                if (row.FindControl("lblHHId") is Label)
                    myActiveHHID = Convert.ToInt32(lblHHId.Text);
                MyActiveStatusHHID = myActiveHHID;

                if (e.CommandArgument.ToString() == UtilBO.WorkflowCodeRouteApproval)//"RTA")
                {
                    if (row != null)
                    {
                        if (Convert.ToString(ViewState["Status"]) == "PENDING")
                        {
                            ViewState["WorkFlowApproverID"] = lblWORKFLOWAPPROVERID.Text;
                            ViewState["WorkflowdefinationID"] = lbllblWORKFLOWDEFINITIONID.Text;
                            ViewState["WorkFlowCode"] = litWORKFLOWCODE.Text;
                            WorkFlowCode = litWORKFLOWCODE.Text;
                            int WORKFLOWAPPID = Convert.ToInt32(lblWORKFLOWAPPROVERID.Text);
                            int workflowId = Convert.ToInt32(lblWorkFlowId.Text);

                            BindFinalProjectGrid(workflowId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, TrackerDetailID);
                            lblFinalProjectDetl.Text = WorkFlowCode + " - Final Route Approval";
                            if (litPageCode.Text == "DATAV")
                            {
                                lnkPageSource.Visible = false;
                            }
                            if (Convert.ToString(ViewState["Status"]) == "APPROVED" || Convert.ToString(ViewState["Status"]) == "DECLINED")
                            {
                                BtnApprove.Visible = false;
                                btnDecline.Visible = false;
                            }
                            else
                            {
                                BtnApprove.Visible = true;
                                btnDecline.Visible = true;
                            }

                        }
                        else
                        {
                            //after save approval Data
                            ViewState["WorkFlowApproverID"] = lblWORKFLOWAPPROVERID.Text;
                            ViewState["WorkflowdefinationID"] = lbllblWORKFLOWDEFINITIONID.Text;
                            ViewState["WorkFlowCode"] = litWORKFLOWCODE.Text;

                            WorkFlowCode = litWORKFLOWCODE.Text;
                            int WORKFLOWAPPID = Convert.ToInt32(lblWORKFLOWAPPROVERID.Text);
                            int workflowId = Convert.ToInt32(lblWorkFlowId.Text);
                            BindApprovalFinalProjectGrid(workflowId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, "");
                            lblFinalProjectDetl.Text = WorkFlowCode + " - Final Route Approval";
                            if (Convert.ToString(ViewState["Status"]) == "APPROVED" || Convert.ToString(ViewState["Status"]) == "DECLINED")
                            {
                                BtnApprove.Visible = false;
                                btnDecline.Visible = false;
                            }
                            else
                            {
                                BtnApprove.Visible = true;
                                btnDecline.Visible = true;
                            }
                        }
                    }

                    ApprovalMultiView.SetActiveView(ViewRTA);
                    pnlAprovalFooter.Visible = true;
                }
                else if (e.CommandArgument.ToString() == UtilBO.WorkflowChangeRequestApprovalHH || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApproval || e.CommandArgument.ToString() == UtilBO.PaymentRequestCode || e.CommandArgument.ToString() == UtilBO.CompensationPrintRequest || e.CommandArgument.ToString() == UtilBO.GrievancesCode || e.CommandArgument.ToString() == UtilBO.WorkflowChangeRequestApprovalFL || e.CommandArgument.ToString() == UtilBO.PackagePaymentRequestCode || e.CommandArgument.ToString() == UtilBO.CdapBudgetCode || e.CommandArgument.ToString() == UtilBO.PaymentVerificationCode || e.CommandArgument.ToString() == UtilBO.DataVerificationCode
                    || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalCrops || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalLand || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalFixtures || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalRep || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalDamCrops || e.CommandArgument.ToString() == UtilBO.WorkflowNegotatedCodeApprovalCulPro)
                {
                    if (row != null)
                    {
                        if (Convert.ToString(ViewState["Status"]) == "PENDING")
                        {
                            ViewState["WorkFlowApproverID"] = lblWORKFLOWAPPROVERID.Text;
                            ViewState["WorkflowdefinationID"] = lbllblWORKFLOWDEFINITIONID.Text;
                            ViewState["WorkFlowCode"] = litWORKFLOWCODE.Text;
                            WorkFlowCode = litWORKFLOWCODE.Text;
                            int WORKFLOWAPPID = Convert.ToInt32(lblWORKFLOWAPPROVERID.Text);
                            int workflowId = Convert.ToInt32(lblWorkFlowId.Text);
                            BindFinalProjectGrid(workflowId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, TrackerDetailID);

                            ViewCR.InnerText = litWorkflowDes.Text;
                            if (litPageCode.Text == "DATAV")
                            {
                                lnkPageSource.Visible = false;
                            }
                            if (Convert.ToString(ViewState["Status"]) == "APPROVED" || Convert.ToString(ViewState["Status"]) == "DECLINED")
                            {
                                BtnApprove.Visible = false;
                                btnDecline.Visible = false;
                            }
                            else
                            {
                                BtnApprove.Visible = true;
                                btnDecline.Visible = true;
                            }
                        }
                        else
                        {
                            //after save data
                            ViewState["WorkFlowApproverID"] = lblWORKFLOWAPPROVERID.Text;
                            ViewState["WorkflowdefinationID"] = lbllblWORKFLOWDEFINITIONID.Text;
                            ViewState["WorkFlowCode"] = litWORKFLOWCODE.Text;
                            WorkFlowCode = litWORKFLOWCODE.Text;
                            int WORKFLOWAPPID = Convert.ToInt32(lblWORKFLOWAPPROVERID.Text);
                            int workflowId = Convert.ToInt32(lbllblWORKFLOWDEFINITIONID.Text);
                            BindApprovalFinalProjectGrid(workflowId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, litPageCode.Text);
                            lblFinalProjectDetl.Text = WorkFlowCode + " - Final Route Approval";

                            ViewCR.InnerText = litWorkflowDes.Text;

                            if (Convert.ToString(ViewState["Status"]) == "APPROVED" || Convert.ToString(ViewState["Status"]) == "DECLINED")
                            {
                                BtnApprove.Visible = false;
                                btnDecline.Visible = false;
                            }
                            else
                            {
                                BtnApprove.Visible = true;
                                btnDecline.Visible = true;
                            }
                        }
                    }
                    if (ViewState["WorkFlowCode"] != null)
                    {
                        if (ViewState["WorkFlowCode"].ToString().ToUpper() == UtilBO.PaymentRequestCode.ToUpper())
                        {
                            grdPaymentRequestBatch.Visible = true;
                            BindPAP(myActiveHHID);
                        }
                        else
                        {
                            grdPaymentRequestBatch.Visible = false;
                        }
                        #region for CDAP-BUDGET Approved / Declined
                        //IF its CDAP-BUDGET Approved
                        if (ViewState["WorkFlowCode"].ToString().ToUpper() == UtilBO.CdapBudgetCode.ToUpper() && Convert.ToString(ViewState["Status"]) == "APPROVED")
                        {
                            grdCDAPBudget.Visible = true;
                            int ProjectID = Convert.ToInt32(ViewState["ProjectId"]);
                            string status = "A";
                            BindgrdCDAPBudget(ProjectID, status);
                        }
                        else
                        {
                            grdCDAPBudget.Visible = false;
                        }
                        //IF its CDAP-BUDGET Declined
                        if (ViewState["WorkFlowCode"].ToString().ToUpper() == UtilBO.CdapBudgetCode.ToUpper() && Convert.ToString(ViewState["Status"]) == "DECLINED")
                        {
                            grdCDAPBudget.Visible = true;
                            int ProjectID = Convert.ToInt32(ViewState["ProjectId"]);
                            string status = "D";
                            BindgrdCDAPBudget(ProjectID, status);
                        }
                        else
                        {
                            grdCDAPBudget.Visible = false;
                        }
                        #endregion
                        #region for CDAP PENDING
                        if (ViewState["WorkFlowCode"].ToString().ToUpper() == UtilBO.CdapBudgetCode.ToUpper() && Convert.ToString(ViewState["Status"]) == "PENDING")
                        {
                            grdCDAPBudget.Visible = true;
                            int ProjectID = Convert.ToInt32(ViewState["ProjectId"]);
                            string status = "S";
                            BindgrdCDAPBudget(ProjectID, status);
                        }
                        else
                        {
                            grdCDAPBudget.Visible = false;
                        }
                        #endregion
                    }
                    ApprovalMultiView.SetActiveView(ViewCON);
                    pnlAprovalFooter.Visible = true;
                }

                // Get Approver Comments
                TrackerDetailBO objTrackerDetailBO = (new TrackerDetailBLL()).GetApprovalTrackerDetailsByID(Convert.ToInt32(litTrackerDetailID.Text));
                if (objTrackerDetailBO != null)
                    txtapprovercomments.Text = objTrackerDetailBO.ApproverComments;
            }
        }
        /// <summary>
        /// To Change Grid Page number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdProjectDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdProjectDtl.PageIndex = e.NewPageIndex;
            BindDetailGrid(ViewState["ProjectId"].ToString(), ViewState["MODULE_ID"].ToString(), ViewState["Status"].ToString());
        }

        #endregion

        #region 3rd grid Data
        /// <summary>
        /// Bind data to Grid based on selection
        /// Set Attribute to Link buttons to open related pages like Upload document etc
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="WORKFLOWAPPID"></param>
        /// <param name="myActiveHHID"></param>
        /// <param name="TrackerDetailID"></param>
        private void BindFinalProjectGrid(int WorkFlowId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, string TrackerDetailID)//ProjectId)
        {
            GetApproavlComments();

            string DocumentCode = string.Empty;
            string ProjectCode = string.Empty;
            int userID = 0;
            int HHID = 0;

            MyTasks_ApprovalBLL objMyTaskApproval = new MyTasks_ApprovalBLL();
            int ProjectId = 0;
            if (ViewState["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(ViewState["ProjectId"]);
            }
            if (WorkFlowCode == "RTA")
            {
                grdFinalProjectDtl.DataSource = objMyTaskApproval.GetFinalProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, myActiveHHID);
                grdFinalProjectDtl.DataBind();
                if (grdFinalProjectDtl.Rows.Count > 0)
                {
                    pnlFinalPojectdEtail.Visible = true;
                }
            }
            else
            {
                string ChangeRequest = string.Empty;
                int ApprovalLevel = 0;
                int TrackerDetailID_ = Convert.ToInt32(TrackerDetailID.ToString());
                //ApprovalscoredtlList AppscoreList = new ApprovalscoredtlList();
                ApprovalscoredtlBO Approvalscor = new ApprovalscoredtlBO();
                Approvalscor = objMyTaskApproval.GetCRProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, TrackerDetailID_);
                if (Approvalscor != null)
                {
                    ProjectCode = Approvalscor.ProjectCode.ToString();
                    ViewState["ProjectCode"] = Approvalscor.ProjectCode.ToString();
                    GetProjCodeLabel.Text = Approvalscor.ProjectCode.ToString();
                    GetProjNameLabel.Text = Approvalscor.ProjectName.ToString();
                    getEmailSubLabel.Text = Approvalscor.EmailSubject.ToString();
                    trackHeaderIDLabel.Text = Approvalscor.TrackHeaderID.ToString();
                    trackHeaderIDLabel.Visible = false;
                    ViewState["TrackHeaderID"] = Approvalscor.TrackHeaderID.ToString();
                    if (Approvalscor.WorkFlowCode == "CR-HH")
                    {
                        ChangeRequest = "HouseHold";
                    }
                    string EmailBody = (Approvalscor.EmailBody);
                    EmailBodyLabel.Text = EmailBody.ToString();
                }
                string pagecode = Convert.ToString(ViewState["PageCode"]);
                if (pagecode == "HH")
                {
                    DocumentCode = "HH";
                }
                else if (pagecode == "NEG" || pagecode == "NEGC" || pagecode == "NEGL" || pagecode == "NEGF" || pagecode == "NEGR"
            || pagecode == "NEGD" || pagecode == "NEGCU")
                {
                    DocumentCode = "FF";
                }
                else if (pagecode == "CRGRA")
                {
                    DocumentCode = "GRV";
                }
                if (Session["USER_ID"] != null)
                    userID = Convert.ToInt32(Session["USER_ID"].ToString());
                if (ViewState["HHID"] != null)
                    HHID = Convert.ToInt32(ViewState["HHID"]);
                if (ViewState["ApproverLevel"] != null)
                {
                    ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"]);
                }
                string paramViewSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", ViewState["PageCode"].ToString(), ApprovalLevel);
                if (ViewState["PageCode"].ToString() == "PAYRQ")
                {
                    //lnkPageSource.InnerText = "View Payment Request Details";
                    lnkPageSource.Visible = false;
                }
                else
                {
                    lnkPageSource.Visible = true;
                    lnkPageSource.InnerText = "View Details";
                }
                lnkPageSource.Attributes.Add("onclick", paramViewSource);
                //if (ViewState["PageCode"].ToString() == "PAYRQ")
                //{
                //    spanPackage.Style.Add("display", "");
                //    string PhotoModule = "PAP";
                //    string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, PhotoModule);
                //    lnkPapPhoto.Attributes.Add("onclick", paramPhotoView);
                //    string paramSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}');", ProjectId, HHID, userID, ProjectCode, "Readonly", "CPREV");
                //    lnkPackageDocument.Attributes.Add("onclick", paramSource);
                //    string paramView = string.Format("OpenDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, "Readonly");
                //    lnkUPloadDoclistSup.Attributes.Add("onclick", paramView);
                //}
                //else
                //    spanPackage.Style.Add("display", "none");

                if (DocumentCode != string.Empty)
                {
                    if (ViewState["PageCode"].ToString() != "PAYRQ")
                    {
                        string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, DocumentCode);
                        lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                        lnkUPloadDoclist.Visible = false;
                    }
                }
                else
                {
                    lnkUPloadDoclist.Visible = false;
                }
                if (ViewState["PageCode"].ToString() != "PKREV")
                {
                    string paramView = string.Format("OpenDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, "Readonly");
                    lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                    lnkUPloadDoclist.Visible = true;
                }

                if (ViewState["PageCode"].ToString() == "CPREV")
                {
                    string paramReView = string.Format("OpenReviewCom({0},{1});", ProjectId, HHID);
                    lnkAppReviewCom.Attributes.Add("onclick", paramReView);
                    spanReviewCom.Style.Add("display", "");
                }
                else
                    spanReviewCom.Style.Add("display", "none");
                if (pagecode == "CRGRA" || pagecode == "CREND" || pagecode == "CRFND")
                {
                    spanPkgForGri.Style.Add("display", "");
                    string paramSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", "CPREV-PAYRQ", ApprovalLevel);
                    lnkPackageDocumentGri.Attributes.Add("onclick", paramSource);
                }
                else
                    spanPkgForGri.Style.Add("display", "none");
            }
        }

        //After Save Data
        /// <summary>
        /// After Save Data refresh the grid
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="WORKFLOWAPPID"></param>
        /// <param name="myActiveHHID"></param>
        /// <param name="pageCode"></param>
        private void BindApprovalFinalProjectGrid(int workflowId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, string pageCode)
        {
            int Status_Id = 0;
            if (Convert.ToString(ViewState["Status"]) == "APPROVED")
            {
                Status_Id = 1;
            }
            else if (Convert.ToString(ViewState["Status"]) == "DECLINED")
            {
                Status_Id = 2;
            }
            else if (Convert.ToString(ViewState["Status"]) == "PENDING")
            {
                Status_Id = 3;
            }

            GetApproavlComments();
            string DocumentCode = string.Empty;
            string ProjectCode = string.Empty;

            MyTasks_ApprovalBLL objMyTaskApproval = new MyTasks_ApprovalBLL();
            ApprovalscoredtlList ApprovalScoreList = new ApprovalscoredtlList();
            int ProjectId = 0;
            if (ViewState["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(ViewState["ProjectId"]);
            }
            if (WorkFlowCode == "RTA")
            {
                ApprovalScoreList = objMyTaskApproval.GetApprovedFinalProjectDetails(workflowId, ProjectId, WorkFlowCode, myActiveHHID);
                grdFinalProjectDtl.DataSource = ApprovalScoreList;
                grdFinalProjectDtl.DataBind();
                if (grdFinalProjectDtl.Rows.Count > 0)
                {
                    pnlFinalPojectdEtail.Visible = true;
                }
                for (int i = 0; i < (ApprovalScoreList.Count); i++)
                    if (ApprovalScoreList[i].ApproverComments != null)
                    {
                        txtapprovercomments.Text = ApprovalScoreList[i].ApproverComments;
                    }
            }
            else
            {
                string ChangeRequest = string.Empty;

                //ApprovalscoredtlList AppscoreList = new ApprovalscoredtlList();
                ApprovalscoredtlBO Approvalscor = new ApprovalscoredtlBO();
                Approvalscor = objMyTaskApproval.GetApprovalCRProjectDetails(workflowId, ProjectId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, pageCode, Status_Id);
                ProjectCode = Approvalscor.ProjectCode.ToString();
                GetProjCodeLabel.Text = Approvalscor.ProjectCode.ToString();
                GetProjNameLabel.Text = Approvalscor.ProjectName.ToString();
                getEmailSubLabel.Text = Approvalscor.EmailSubject.ToString();
                trackHeaderIDLabel.Text = Approvalscor.TrackHeaderID.ToString();
                trackHeaderIDLabel.Visible = false;
                if (Approvalscor.WorkFlowCode == "CR-HH")
                {
                    ChangeRequest = "HouseHold";
                }
                string EmailBody = (Approvalscor.EmailBody);
                //string EmailBody = (Approvalscor.EmailBody + " " + ChangeRequest + " " + Approvalscor.ProjectCode + " " + Approvalscor.ProjectName).ToString();
                EmailBodyLabel.Text = EmailBody.ToString();
                if (Convert.ToString(ViewState["Status"]) == "APPROVED")
                {
                    int StatusID = 1;
                    int TrackHeaderID = Convert.ToInt32(Approvalscor.TrackHeaderID);
                    Approvalscor = objMyTaskApproval.GetApprovalComments(TrackHeaderID, StatusID);
                }
                if (Convert.ToString(ViewState["Status"]) == "DECLINED")
                {
                    int StatusID = 2;
                    int TrackHeaderID = Convert.ToInt32(Approvalscor.TrackHeaderID);
                    Approvalscor = objMyTaskApproval.GetApprovalComments(TrackHeaderID, StatusID);
                }
                if (!string.IsNullOrEmpty(Approvalscor.ApproverComments))// != string.Empty)
                    txtapprovercomments.Text = Approvalscor.ApproverComments.ToString();
                else
                    txtapprovercomments.Text = "";

            }
            int userID = 0;
            int HHID = 0;
            int ApprovalLevel = 0;
            string pagecode = Convert.ToString(ViewState["PageCode"]);
            if (pagecode == "HH")
            {
                DocumentCode = "HH";
            }
            else if (pagecode == "NEG" || pagecode == "NEGC" || pagecode == "NEGL" || pagecode == "NEGF" || pagecode == "NEGR"
            || pagecode == "NEGD" || pagecode == "NEGCU")
            {
                DocumentCode = "FF";
            }
            else if (pagecode == "CRGRA")
            {
                DocumentCode = "GRV";
            }
            if (Session["USER_ID"] != null)
                userID = Convert.ToInt32(Session["USER_ID"].ToString());
            if (ViewState["HHID"] != null)
                HHID = Convert.ToInt32(ViewState["HHID"]);
            if (ViewState["ApproverLevel"] != null)
            {
                ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"]);
            }
            if (ViewState["PageCode"].ToString() != "PAYRQ")
            {
                string paramViewSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", ViewState["PageCode"].ToString(), ApprovalLevel);
                lnkPageSource.Attributes.Add("onclick", paramViewSource);
            }
            else
            {
                lnkPageSource.Visible = false;
            }

            if (DocumentCode != string.Empty)
            {
                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, DocumentCode);
                lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                lnkUPloadDoclist.Visible = true;
            }
            else
            {
                lnkUPloadDoclist.Visible = false;
            }
            if (ViewState["PageCode"].ToString() != "PKREV")
            {
                string paramView = string.Format("OpenDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, "Readonly");
                lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                lnkUPloadDoclist.Visible = true;
            }

            if (ViewState["PageCode"].ToString() == "CPREV")
            {
                string paramReView = string.Format("OpenReviewCom({0},{1});", ProjectId, HHID);
                lnkAppReviewCom.Attributes.Add("onclick", paramReView);
                spanReviewCom.Style.Add("display", "");
            }
            else
                spanReviewCom.Style.Add("display", "none");

            if (pagecode == "CRGRA" || pagecode == "CREND" || pagecode == "CRFND")
            {
                spanPkgForGri.Style.Add("display", "");
                string paramSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", "CPREV-PAYRQ", ApprovalLevel);
                lnkPackageDocumentGri.Attributes.Add("onclick", paramSource);
            }
            else
                spanPkgForGri.Style.Add("display", "none");
        }


        /// <summary>
        /// to set final route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFinalProjectDtl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltlIsFinal = (Literal)e.Row.FindControl("ltlIsFinal");
                CheckBox chkSelectRoute = (CheckBox)e.Row.FindControl("chkSelectRoute");

                if (chkSelectRoute is CheckBox)
                    chkSelectRoute.Checked = false;

                if (ltlIsFinal.Text.ToUpper() == "TRUE")
                {
                    chkSelectRoute.Checked = true;
                }
            }
        }
        //grdFinalProjectDtl_RowDataBound()
        /// <summary>
        /// To open RouteIdentification page to see the score of Route
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lblScore_Click1(object sender, EventArgs e)
        {
            int ProjectId = 0;
            if (ViewState["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(ViewState["ProjectId"]);
            }
            int RouteID = 0;
            {
                Literal ltlIsFinal = (Literal)grdFinalProjectDtl.FindControl("ltlIsFinal");
                if (ltlIsFinal != null)
                    RouteID = Convert.ToInt32(ltlIsFinal.Text);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openpopup", "window.open('" + "../PROJECT/RouteIdentification.aspx?Mode=Score&ProjectID=" + ProjectId + "','ChildWindow','height = 520, width = 730,status=no,location=no,directories=no, resizable = 1, scrollbars=yes, left=75, top=75')", true);
        }

        /// <summary>
        /// To View Routemap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lblViewMap_Click1(object sender, EventArgs e)
        {
            int ProjectId = 0;
            if (ViewState["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(ViewState["ProjectId"]);
            }
            int RouteID = 0;
            {
                try
                {
                    LinkButton chk = (LinkButton)sender;
                    GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                    Label lblRouteID = (Label)gr.FindControl("lblRouteID");
                    if (lblRouteID != null)
                        RouteID = Convert.ToInt32(lblRouteID.Text);
                }
                catch { }
            }
            if (RouteID > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "openpopup", "window.open('" + "../PROJECT/RouteMap.aspx?Mode=Score&routeID=" + RouteID + "','ChildWindow','height = 520, width = 730,status=no,location=no,directories=no, resizable = 1, scrollbars=yes, left=75, top=75')", true);
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "openpopup", "window.open('" + "../PROJECT/RouteMap.aspx?Mode=Score&projID=" + ProjectId + "','ChildWindow','height = 520, width = 730,status=no,location=no,directories=no, resizable = 1, scrollbars=yes, left=75, top=75')", true);
        }

        #region for approval comment
        //rightnow not in use
        public void GetApproavlComments()
        {
            //.ApprovalMessage.viewApproverLog(ProjectID, WorkFlowCode, pageCode);
            //ApprovalMessage1.Visible = true;
            lnkAppComments.Visible = true;
            int ProjectID = 0;
            string WorkFlowCode = string.Empty;
            string pageCode = string.Empty;
            int TrackHdrId = 0;
            int BatchNo = 0;
            if (ViewState["PageCode"] != null)
            {
                pageCode = Convert.ToString(ViewState["PageCode"]);
            }
            else
            {
                pageCode = "0";
            }
            if (ViewState["WorkFlowCode"] != null)
            {
                WorkFlowCode = ViewState["WorkFlowCode"].ToString();
            }
            else
            {
                WorkFlowCode = "0";
            }

            if (ViewState["ProjectId"] != null)
            {
                ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString());
            }
            else
            {
                ProjectID = 0;
            }
            if (ViewState["TrackHdrId"] != null)
            {
                TrackHdrId = Convert.ToInt32(ViewState["TrackHdrId"].ToString());
            }
            else
            {
                TrackHdrId = 0;
            }
            if (ViewState["ElementID"] != null)
            {
                BatchNo = Convert.ToInt32(ViewState["ElementID"].ToString());
            }
            else
                BatchNo = 0;

            string param = string.Format("OpenApproverDocumnet({0},'{1}','{2}',{3},{4} );", ProjectID, WorkFlowCode, pageCode, TrackHdrId, BatchNo);
            lnkAppComments.Attributes.Add("onclick", param);
            // ApprovalMessage1.ViewApproverLog(ProjectID, WorkFlowCode, pageCode);
        }
        #endregion

        #endregion 3rd grid


        /// <summary>
        /// TO approve a request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region Old Code

        
        /* protected void BtnApprove_Click(object sender, EventArgs e)
        {
            #region PreDefined Variable
            int countCheck;
            string ChangeRequest = string.Empty;
            string EmailSubject = string.Empty;
            string SmsText = string.Empty;
            int SaveData = 0;

            if (ViewState["WorkFlowCode"] != null)
            {
                ChangeRequest = ViewState["WorkFlowCode"].ToString();
            }
            string ResultValue = string.Empty;

            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();

            int WorkflowdefinationID = 0;
            Boolean chkStatusofBatch = true;
            if (ChangeRequest == "PAYRQ")
            {
                string approvalStatus = string.Empty;
                int ChkCount = 0;
                //int SelcectCount = 0;

                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                {
                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                    if (Selectstatus.Checked)
                    {
                        ChkCount = ChkCount + 1;
                    }
                }
                if (ChkCount == 0)
                {
                    chkStatusofBatch = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alertsa", "alert('Please select a batch to Appove.')", true);
                }                
            }
            ApprovalscoredtlBO objFinalRoute = new ApprovalscoredtlBO();

            if (ViewState["WorkFlowApproverID"] != null)
            {
                WorkflowdefinationID = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
            }
            objFinalRoute = objMytaskApprovalBLL.GetEmailID(WorkflowdefinationID);
            #endregion
            if (chkStatusofBatch)
            {
                #region objFinalRoute not null
                if ((objFinalRoute) != null)
                {
                    #region if RTA
                    if (ChangeRequest == "RTA")
                    {
                        foreach (GridViewRow oRow in grdFinalProjectDtl.Rows)
                        {
                            CheckBox chkSelectRoute = (CheckBox)oRow.FindControl("chkSelectRoute");
                            if (chkSelectRoute.Checked)
                            {
                                SaveData = 1;
                                break;
                            }
                            else
                            {
                                SaveData = 0;
                            }
                        }
                    }
                    else
                    {
                        SaveData = 1;
                    }
                    #endregion

                    #region for PAYRQ
                    if (ChangeRequest == "PAYRQ")
                    {
                        int totalCount = grdPaymentRequestBatch.Rows.Count;
                        // int TotalApprovalCount = 0;
                        string approvalStatus = string.Empty;
                        int Count = 0;
                        //int SelcectCount = 0;

                        foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                        {
                            CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                            if (Selectstatus.Checked)
                            {
                                // Count = Count + 1;
                            }

                            if (totalCount == 1)
                            {
                                Count = 1;
                            }
                            else
                            {
                                Label RequestStatus = (Label)gvw.FindControl("lblRequestStatus");
                                approvalStatus = (RequestStatus.Text.ToString());
                                //if (approvalStatus == "Approved")
                                //{
                                //    Count = Count + 1;
                                //}
                                if (approvalStatus == "Declined") //Declined
                                {
                                    Count = Count + 1;
                                }
                                else if (approvalStatus == "Approved" && !Selectstatus.Visible)
                                {
                                    Count = Count + 1;
                                }
                                //if (Selectstatus.Checked)
                                //{
                                //    Count = Count - 1;
                                //}
                            }
                        }
                        if (Count == (totalCount - 1))
                        {
                            SaveData = 1;

                        }
                        else if (Count == totalCount)
                        {
                            SaveData = 1;
                        }
                        else
                        {
                            SaveData = 0;
                        }
                    }
                    #endregion
                    #region for Save Approval Comments and Data to DB
                    if (SaveData == 1)
                    {
                        int count = 2;
                        #region if Approver Exists
                        WorkflowApprovalBO objWorkflowapproval = new WorkflowApprovalBO();
                        objWorkflowapproval.WorkflowapprovalId = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                        objWorkflowapproval.Status = "APPROVED";
                        objWorkflowapproval.AuthoriserID = 1; //Convert.ToInt32(Session["ROLE_ID"]);
                        objWorkflowapproval.WorkFlowDefinationId = Convert.ToInt32(ViewState["WorkflowdefinationID"]);
                        objWorkflowapproval.Auctiontakenby = Convert.ToInt32(Session["USER_ID"]);
                        string strMax = txtapprovercomments.Text.ToString().Trim();
                        if (strMax.Trim().Length >= 500)
                        {
                            strMax = txtapprovercomments.Text.ToString().Trim().Substring(0, 500);
                        }
                        objWorkflowapproval.Approvercomments = strMax;
                        int trackerheader = Convert.ToInt32(ViewState["TrackHdrId"]);

                        if (trackerheader != 0)
                        {
                            objWorkflowapproval.TrackerHdrID = trackerheader;
                        }
                        else
                        {
                            objWorkflowapproval.TrackerHdrID = 0;
                        }

                        #region for second to n - approval status checkin
                        // for Checking Second approval exit or not
                        if (ViewState["ApproverLevel"] != null)
                        {
                            objWorkflowapproval.ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                        }
                        if (ViewState["ProjectId"] != null)
                        {
                            objWorkflowapproval.ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString());
                        }
                        if (ViewState["HHID"] != null)
                        {
                            objWorkflowapproval.HHID = Convert.ToInt32(ViewState["HHID"].ToString());
                        }
                        if (ViewState["ElementID"] != null)
                        {
                            objWorkflowapproval.ElementID = Convert.ToInt32(ViewState["ElementID"].ToString());
                        }
                        objWorkflowapproval.PageCode = Convert.ToString(ViewState["PageCode"]);
                        objWorkflowapproval.WorkFlowCode = ChangeRequest;
                        #endregion

                        //int Check = 0;
                        if (ChangeRequest == "PAYRQ")
                        {
                            int act = 0;
                            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                            {
                                CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                if (Selectstatus.Checked)
                                {
                                    act = 1;
                                }
                                //else
                                //{
                                //    act = 0;
                                //}
                            }
                            if (act == 1)
                            {
                                count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, 1);
                            }

                        }
                        else
                        {
                            count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, 1);
                        }

                        if (count == 0)
                        {
                            #region for Update Root Data only if RTA
                            if (ChangeRequest == "RTA")
                            {
                                UPdateFinalRoute();
                            }
                            #endregion
                            #region for Update Root Data only if other than RTA
                            if (ChangeRequest == "NEG")
                            {
                                int trackerheader_ = Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
                                UPdateFinalValue(trackerheader_);
                            }
                            if (ChangeRequest == "NEGC" || ChangeRequest == "NEGL" || ChangeRequest == "NEGF" || ChangeRequest == "NEGR"
                || ChangeRequest == "NEGD" || ChangeRequest == "NEGCU")
                            {
                                int trackerheader_ = Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
                                UPdateFinalValueForIndNeg(trackerheader_, ChangeRequest);
                            }
                            if (ChangeRequest == "PAYRQ")
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                                int USER_ID = 0;
                                if (Session["USER_ID"] != null)
                                    USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                Close_Batch(HHID_, USER_ID);
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                                PnlProjectDtl.Visible = false;
                            }
                            if (ChangeRequest == "CRGRA")
                            {
                                string updateStatus = "A";
                                UpdateGRievancesStatus((Convert.ToInt32(ViewState["HHID"].ToString())), (Convert.ToInt32(ViewState["ElementID"].ToString())), updateStatus);
                            }
                            if (ChangeRequest == "PAYVR")
                            {
                                string updateStatus = "PP";
                                //Added By Anjan For Update the Fund Status
                                PaymentBLL objPBll = new PaymentBLL();
                                objPBll.UpdateStatus(Convert.ToInt32(ViewState["HHID"].ToString()), "A");
                                //End
                                UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                            }
                            if (ChangeRequest == "CR-FL")
                            {
                                string updateStatus = "CP";
                                UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                            }
                            if (ChangeRequest == "PKREV")
                            {
                                string updateStatus = "CP";
                                //UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                            }
                            if (ChangeRequest == "CDAPB")
                            {
                                string updateStatus = "A";
                                UpdateCDAPBUGStatus(updateStatus);
                            }
                            #endregion
                        }
                        #region For sending Email and SMS
                        if (count == 0 || count == 1)
                        {
                            #region for EMAIL / SMS Text
                            trackHeaderIDLabel.Text = "0";
                            int HHID_ = Convert.ToInt32(ViewState["HHID"].ToString());
                            NotificationBO NotificationObj = new NotificationBO();
                            string EmailBody = string.Empty;
                            if (ChangeRequest == "RTA")
                            {
                                EmailSubject = "Route Approval request has been Approved For Project :" + objFinalRoute.ProjectCode;
                                SmsText = "Route Approval request is Approved For Project " + objFinalRoute.ProjectCode;
                            }
                            else if (ChangeRequest == "PAYRQ")
                            {
                                EmailSubject = "Your Payment Request For Batch";
                                SmsText = "Your Payment Request For Batch";
                            }
                            else if (ChangeRequest == "CR-FL")
                            {
                                EmailSubject = "Package Closing Payment Request has been Approved";
                                SmsText = "Package Closing Payment Request Has been Approved";
                            }
                            else if (ChangeRequest == "NEG")
                            {
                                EmailSubject = "Negotiation Amount request has been Approved";
                                SmsText = "Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGC")
                            {
                                EmailSubject = "Crops Negotiation Amount request has been Approved";
                                SmsText = "Crops Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGL")
                            {
                                EmailSubject = "Land Negotiation Amount request has been Approved";
                                SmsText = "Land Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGF")
                            {
                                EmailSubject = "Fixtures Negotiation Amount request has been Approved";
                                SmsText = "Fixtures Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGR")
                            {
                                EmailSubject = "Replacement Negotiation Amount request has been Approved";
                                SmsText = "Replacement Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGD")
                            {
                                EmailSubject = "Damaged crop Negotiation Amount request has been Approved";
                                SmsText = "Damaged crop Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "NEGCU")
                            {
                                EmailSubject = "Cultural Property Negotiation Amount request has been Approved";
                                SmsText = "Cultural Property Negotiation Amount request has been Approved";
                            }
                            else if (ChangeRequest == "CRGRA")
                            {
                                EmailSubject = "Grievance Resolution request has been Approved";
                                SmsText = "Grievance Resolution request has been Approved";
                            }
                            else if (ChangeRequest == "CDAPB")
                            {
                                EmailSubject = "CDAP Budget request has been Approved for the project:" + objFinalRoute.ProjectCode;
                                SmsText = "CDAP Budget request has been Approved for the project:" + objFinalRoute.ProjectCode;
                            }
                            else
                            {
                                EmailSubject = "Change Request has been Approved for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_;
                                SmsText = "Change Request has been Approved for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_; ;
                            }
                            string ApproverText = txtapprovercomments.Text;

                            NotificationObj.WorkflowCode = ChangeRequest.ToString();
                            NotificationObj.EmailID = objFinalRoute.EmailID;
                            NotificationObj.EmailSubject = EmailSubject;
                            NotificationObj.EmailBody = ApproverText;
                            NotificationObj.ProjectCode = objFinalRoute.ProjectCode;
                            NotificationObj.ProjectName = objFinalRoute.ProjectName;
                            NotificationObj.RequesterName = objFinalRoute.RequesterName;

                            (new NotificationBLL()).SendEmail(NotificationObj, "A");
                            #endregion

                            #region for sending SMS
                            WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                            WIS_ConfigBLL WIS_ConfigBLL = new WIS_ConfigBLL();
                            WIS_ConfigBO = WIS_ConfigBLL.GetConfigSMSsending();
                            if (WIS_ConfigBO != null)
                            {
                                if (WIS_ConfigBO.MobileStatus == "Y")
                                {
                                    string Result = string.Empty;
                                    string SendsmsTest = SmsText + objFinalRoute.ProjectCode + objFinalRoute.ProjectName;
                                    NotificationBO SMSNotificationBO = new NotificationBO();
                                    NotificationBLL SMSNotificationBLL = new NotificationBLL();
                                    SMSNotificationBO.ProviderMobileNo = WIS_ConfigBO.MobileNumber;
                                    SMSNotificationBO.ProviderPasword = WIS_ConfigBO.MobilePassword;
                                    SMSNotificationBO.ProviderURL = WIS_ConfigBO.SiteUrl;

                                    SMSNotificationBO.CellNumber = objFinalRoute.CellNumber;
                                    SMSNotificationBO.SmsText = SendsmsTest;

                                    Result = SMSNotificationBLL.SENDSMS(SMSNotificationBO);
                                }
                            }
                            #endregion
                            //NotificationObj.SendEmail(objFinalRoute.EmailID, EmailSubject, EmailBody, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);
                            //ResultValue = NotificationObj.SendSMS(objFinalRoute.CellNumber, SmsText, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);

                            string message = "Request has been Approved.";
                            if (message != "")
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                            BindGrid(false, false);
                            pnlFinalPojectdEtail.Visible = false;
                            ApprovalMultiView.Visible = false;
                            pnlAprovalFooter.Visible = false;

                        }
                        if (ChangeRequest == "PAYRQ" && count == 1)
                        {
                            UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                        }
                        if (ChangeRequest == "PAYRQ" && count == 2)
                        {
                            UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                            pnlFinalPojectdEtail.Visible = false;
                            ApprovalMultiView.Visible = false;
                            pnlAprovalFooter.Visible = false;
                            PnlProjectDtl.Visible = false;
                        }
                        #endregion

                        #endregion
                    }
                    #endregion
                    else
                    {
                        if (ChangeRequest == "RTA")
                        {
                            string message = "Select Any one Route For Approved";
                            if (message != "")
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                        }
                        if (ChangeRequest == "PAYRQ")
                        {
                            int chec = 0;
                            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                            {
                                CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                if (Selectstatus.Checked)
                                {
                                    UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                                    pnlFinalPojectdEtail.Visible = false;
                                    ApprovalMultiView.Visible = false;
                                    pnlAprovalFooter.Visible = false;
                                    PnlProjectDtl.Visible = false;
                                    chec = 1;
                                }
                                //else
                                //{
                                //    chec = 0;
                                //}
                            }
                            if (chec == 0)
                            {
                                string message = "Select Any one Payment Request For Approved";
                                if (message != "")
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                            }
                        }
                    }
                }
                else
                {
                    string message = "Approver Not Exit";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                #endregion
            }
        }*/

        #endregion
                
        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            #region PreDefined Variables

            int countCheck;
            string ChangeRequest = string.Empty;
            string EmailSubject = string.Empty;
            string SmsText = string.Empty;
            int SaveData = 0;

            if (ViewState["WorkFlowCode"] != null)
            {
                ChangeRequest = ViewState["WorkFlowCode"].ToString();
            }
            string ResultValue = string.Empty;

            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();

            int WorkflowdefinationID = 0;
            Boolean chkStatusofBatch = true;
            if (ChangeRequest == "PAYRQ")
            {
                string approvalStatus = string.Empty;
                int ChkCount = 0;
                //int SelcectCount = 0;

                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                {
                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                    if (Selectstatus.Checked)
                    {
                        ChkCount = ChkCount + 1;
                    }
                }
                if (ChkCount == 0)
                {
                    chkStatusofBatch = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alertsa", "alert('Please select a batch to Appove.')", true);
                }
            }

            int AppDataCount = 0;
            int DecDataCount = 0;

            #region New Batch Procedure

            if (ChangeRequest == "PAYRQ")
            {


                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                {

                    Label RequestStatus = (Label)gvw.FindControl("lblRequestStatusShow");
                    string approvalStatus = (RequestStatus.Text.ToString());
                    if (approvalStatus == "Approved")
                    {
                        AppDataCount = AppDataCount + 1;
                    }
                    else if (approvalStatus == "Declined")
                    {
                        DecDataCount = DecDataCount + 1;
                    }
                }


                //Batch Count
                int totalBatchcount = grdPaymentRequestBatch.Rows.Count;
                int count = 0;
                // MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();

                //Edwin: 11/04/2016 Parameters for creating next level request
                WorkflowApprovalBO objWorkflowapproval = new WorkflowApprovalBO();
                objWorkflowapproval.WorkflowapprovalId = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                objWorkflowapproval.Status = "APPROVED";
                objWorkflowapproval.AuthoriserID = 1;//Convert.ToInt32(Session["ROLE_ID"]);
                objWorkflowapproval.WorkFlowDefinationId = Convert.ToInt32(ViewState["WorkflowdefinationID"]);
                objWorkflowapproval.Auctiontakenby = Convert.ToInt32(Session["USER_ID"]);
                objWorkflowapproval.Approvercomments = txtapprovercomments.Text;
                int trackerheader = Convert.ToInt32(ViewState["TrackHdrId"]);
                objWorkflowapproval.TrackerHdrID = trackerheader;
                objWorkflowapproval.PageCode = Convert.ToString(ViewState["PageCode"]);
                if (ViewState["ApproverLevel"] != null) { objWorkflowapproval.ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString()); }
                if (ViewState["ProjectId"] != null) { objWorkflowapproval.ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString()); }
                if (ViewState["HHID"] != null) { objWorkflowapproval.HHID = Convert.ToInt32(ViewState["HHID"].ToString()); }
                if (ViewState["ElementID"] != null) { objWorkflowapproval.ElementID = Convert.ToInt32(ViewState["ElementID"].ToString()); }
                objWorkflowapproval.WorkFlowCode = ChangeRequest;

                //Edwin: 11/04/2016 Batching Conditions and Logic

                if (AppDataCount > 0)
                {
                    if ((AppDataCount + DecDataCount) == (totalBatchcount - 1))
                    {
                        UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                        objWorkflowapproval.Status = "APPROVED";
                        MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
                        objMyTaskApprovalDAL.ApproveStatus(objWorkflowapproval);
                        int result = objMyTaskApprovalDAL.CreateNextRequestOrExit(objWorkflowapproval);

                        pnlFinalPojectdEtail.Visible = false;
                        ApprovalMultiView.Visible = false;
                        pnlAprovalFooter.Visible = false;
                        PnlProjectDtl.Visible = false;

                        GrdMyTaskApproval.DataSource = null;
                        BindGrid(false, false);
                    }
                    else
                    {

                        UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");

                        pnlFinalPojectdEtail.Visible = false;
                        ApprovalMultiView.Visible = false;
                        pnlAprovalFooter.Visible = false;
                        PnlProjectDtl.Visible = false;
                    }

                }

                else if (totalBatchcount == 1)
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                    objWorkflowapproval.Status = "APPROVED";
                    MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
                    objMyTaskApprovalDAL.ApproveStatus(objWorkflowapproval);
                    int result = objMyTaskApprovalDAL.CreateNextRequestOrExit(objWorkflowapproval);

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;

                    GrdMyTaskApproval.DataSource = null;
                    BindGrid(false, false);
                }

                else if (DecDataCount == (totalBatchcount - 1))
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                    objWorkflowapproval.Status = "APPROVED";
                    MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
                    objMyTaskApprovalDAL.ApproveStatus(objWorkflowapproval);
                    int result = objMyTaskApprovalDAL.CreateNextRequestOrExit(objWorkflowapproval);

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;

                    GrdMyTaskApproval.DataSource = null;
                    BindGrid(false, false);
                }
                else
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;
                }


            }
            #endregion

            #region Old Batching Procedure

            else
            {
                ApprovalscoredtlBO objFinalRoute = new ApprovalscoredtlBO();

                if (ViewState["WorkFlowApproverID"] != null)
                {
                    WorkflowdefinationID = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                }
                objFinalRoute = objMytaskApprovalBLL.GetEmailID(WorkflowdefinationID);

                if (chkStatusofBatch)
                {
                    #region objFinalRoute not null
                    if ((objFinalRoute) != null)
                    {
                        #region if RTA
                        if (ChangeRequest == "RTA")
                        {
                            foreach (GridViewRow oRow in grdFinalProjectDtl.Rows)
                            {
                                CheckBox chkSelectRoute = (CheckBox)oRow.FindControl("chkSelectRoute");
                                if (chkSelectRoute.Checked)
                                {
                                    SaveData = 1;
                                    break;
                                }
                                else
                                {
                                    SaveData = 0;
                                }
                            }
                        }
                        else
                        {
                            SaveData = 1;
                        }
                        #endregion

                        #region for PAYRQ

                        if (ChangeRequest == "PAYRQ")
                        {
                            int totalCount = grdPaymentRequestBatch.Rows.Count;
                            // int TotalApprovalCount = 0;
                            string approvalStatus = string.Empty;
                            int Count = 0;
                            //int SelcectCount = 0;

                            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                            {
                                CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                if (Selectstatus.Checked)
                                {
                                    // Count = Count + 1;
                                }

                                if (totalCount == 1)
                                {
                                    Count = 1;
                                }
                                else
                                {
                                    Label RequestStatus = (Label)gvw.FindControl("lblRequestStatus");
                                    approvalStatus = (RequestStatus.Text.ToString());
                                    //if (approvalStatus == "Approved")
                                    //{
                                    //    Count = Count + 1;
                                    //}
                                    if (approvalStatus == "Declined") //Declined
                                    {
                                        Count = Count + 1;
                                    }
                                    else if (approvalStatus == "Approved" && !Selectstatus.Visible)
                                    {
                                        Count = Count + 1;
                                    }
                                    //if (Selectstatus.Checked)
                                    //{
                                    //    Count = Count - 1;
                                    //}
                                }
                            }
                            if (Count == (totalCount - 1))
                            {
                                SaveData = 1;

                            }
                            else if (Count == totalCount)
                            {
                                SaveData = 1;
                            }
                            else
                            {
                                SaveData = 0;
                            }
                        }

                        #endregion

                        #region for Save Approval Comments and Data to DB

                        if (SaveData == 1)
                        {
                            int count = 2;
                            #region if Approver Exists
                            WorkflowApprovalBO objWorkflowapproval = new WorkflowApprovalBO();
                            objWorkflowapproval.WorkflowapprovalId = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                            objWorkflowapproval.Status = "APPROVED";
                            objWorkflowapproval.AuthoriserID = 1; //Convert.ToInt32(Session["ROLE_ID"]);
                            objWorkflowapproval.WorkFlowDefinationId = Convert.ToInt32(ViewState["WorkflowdefinationID"]);
                            objWorkflowapproval.Auctiontakenby = Convert.ToInt32(Session["USER_ID"]);
                            string strMax = txtapprovercomments.Text.ToString().Trim();
                            if (strMax.Trim().Length >= 500)
                            {
                                strMax = txtapprovercomments.Text.ToString().Trim().Substring(0, 500);
                            }
                            objWorkflowapproval.Approvercomments = strMax;
                            int trackerheader = Convert.ToInt32(ViewState["TrackHdrId"]);

                            if (trackerheader != 0)
                            {
                                objWorkflowapproval.TrackerHdrID = trackerheader;
                            }
                            else
                            {
                                objWorkflowapproval.TrackerHdrID = 0;
                            }

                            #region for second to n - approval status checkin
                            // for Checking Second approval exit or not
                            if (ViewState["ApproverLevel"] != null)
                            {
                                objWorkflowapproval.ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                            }
                            if (ViewState["ProjectId"] != null)
                            {
                                objWorkflowapproval.ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString());
                            }
                            if (ViewState["HHID"] != null)
                            {
                                objWorkflowapproval.HHID = Convert.ToInt32(ViewState["HHID"].ToString());
                            }
                            if (ViewState["ElementID"] != null)
                            {
                                objWorkflowapproval.ElementID = Convert.ToInt32(ViewState["ElementID"].ToString());
                            }
                            objWorkflowapproval.PageCode = Convert.ToString(ViewState["PageCode"]);
                            objWorkflowapproval.WorkFlowCode = ChangeRequest;
                            #endregion

                            //int Check = 0;
                            if (ChangeRequest == "PAYRQ")
                            {
                                int act = 0;
                                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                                {
                                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                    if (Selectstatus.Checked)
                                    {
                                        act = 1;
                                    }
                                    //else
                                    //{
                                    //    act = 0;
                                    //}
                                }
                                if (act == 1)
                                {
                                    count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, 1);
                                }

                            }
                            else
                            {
                                count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, 1);
                            }

                            if (count == 0)
                            {
                                #region for Update Root Data only if RTA
                                if (ChangeRequest == "RTA")
                                {
                                    UPdateFinalRoute();
                                }
                                #endregion
                                
                                #region for Update Root Data only if other than RTA
                                if (ChangeRequest == "NEG")
                                {
                                    int trackerheader_ = Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
                                    UPdateFinalValue(trackerheader_);
                                }
                                if (ChangeRequest == "NEGC" || ChangeRequest == "NEGL" || ChangeRequest == "NEGF" || ChangeRequest == "NEGR" || ChangeRequest == "NEGD" || ChangeRequest == "NEGCU")
                                {
                                    int trackerheader_ = Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
                                    UPdateFinalValueForIndNeg(trackerheader_, ChangeRequest);
                                }
                                if (ChangeRequest == "PAYRQ")
                                {
                                    UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                                    int USER_ID = 0;
                                    if (Session["USER_ID"] != null)
                                        USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                    int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                    Close_Batch(HHID_, USER_ID);
                                    pnlFinalPojectdEtail.Visible = false;
                                    ApprovalMultiView.Visible = false;
                                    pnlAprovalFooter.Visible = false;
                                    PnlProjectDtl.Visible = false;
                                }
                                if (ChangeRequest == "CRGRA")
                                {
                                    string updateStatus = "A";
                                    UpdateGRievancesStatus((Convert.ToInt32(ViewState["HHID"].ToString())), (Convert.ToInt32(ViewState["ElementID"].ToString())), updateStatus);
                                }
                                if (ChangeRequest == "PAYVR")
                                {
                                    string updateStatus = "PP";
                                    //Added By Anjan For Update the Fund Status
                                    PaymentBLL objPBll = new PaymentBLL();
                                    objPBll.UpdateStatus(Convert.ToInt32(ViewState["HHID"].ToString()), "A");
                                    //End
                                    UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                                }
                                if (ChangeRequest == "CR-FL")
                                {
                                    string updateStatus = "CP";
                                    UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                                }
                                if (ChangeRequest == "PKREV")
                                {
                                    string updateStatus = "CP";
                                    //UpdatePackageClosingInfo((Convert.ToInt32(ViewState["HHID"].ToString())), updateStatus);
                                }
                                if (ChangeRequest == "CDAPB")
                                {
                                    string updateStatus = "A";
                                    UpdateCDAPBUGStatus(updateStatus);
                                }
                                #endregion
                            }
                            #region For sending Email and SMS
                            if (count == 0 || count == 1)
                            {
                                #region for EMAIL / SMS Text
                                trackHeaderIDLabel.Text = "0";
                                int HHID_ = Convert.ToInt32(ViewState["HHID"].ToString());
                                NotificationBO NotificationObj = new NotificationBO();
                                string EmailBody = string.Empty;
                                if (ChangeRequest == "RTA")
                                {
                                    EmailSubject = "Route Approval request has been Approved For Project :" + objFinalRoute.ProjectCode;
                                    SmsText = "Route Approval request is Approved For Project " + objFinalRoute.ProjectCode;
                                }
                                else if (ChangeRequest == "PAYRQ")
                                {
                                    EmailSubject = "Your Payment Request For Batch";
                                    SmsText = "Your Payment Request For Batch";
                                }
                                else if (ChangeRequest == "CR-FL")
                                {
                                    EmailSubject = "Package Closing Payment Request has been Approved";
                                    SmsText = "Package Closing Payment Request Has been Approved";
                                }
                                else if (ChangeRequest == "NEG")
                                {
                                    EmailSubject = "Negotiation Amount request has been Approved";
                                    SmsText = "Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGC")
                                {
                                    EmailSubject = "Crops Negotiation Amount request has been Approved";
                                    SmsText = "Crops Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGL")
                                {
                                    EmailSubject = "Land Negotiation Amount request has been Approved";
                                    SmsText = "Land Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGF")
                                {
                                    EmailSubject = "Fixtures Negotiation Amount request has been Approved";
                                    SmsText = "Fixtures Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGR")
                                {
                                    EmailSubject = "Replacement Negotiation Amount request has been Approved";
                                    SmsText = "Replacement Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGD")
                                {
                                    EmailSubject = "Damaged crop Negotiation Amount request has been Approved";
                                    SmsText = "Damaged crop Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "NEGCU")
                                {
                                    EmailSubject = "Cultural Property Negotiation Amount request has been Approved";
                                    SmsText = "Cultural Property Negotiation Amount request has been Approved";
                                }
                                else if (ChangeRequest == "CRGRA")
                                {
                                    EmailSubject = "Grievance Resolution request has been Approved";
                                    SmsText = "Grievance Resolution request has been Approved";
                                }
                                else if (ChangeRequest == "CDAPB")
                                {
                                    EmailSubject = "CDAP Budget request has been Approved for the project:" + objFinalRoute.ProjectCode;
                                    SmsText = "CDAP Budget request has been Approved for the project:" + objFinalRoute.ProjectCode;
                                }
                                else
                                {
                                    EmailSubject = "Change Request has been Approved for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_;
                                    SmsText = "Change Request has been Approved for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_; ;
                                }
                                string ApproverText = txtapprovercomments.Text;

                                NotificationObj.WorkflowCode = ChangeRequest.ToString();
                                NotificationObj.EmailID = objFinalRoute.EmailID;
                                NotificationObj.EmailSubject = EmailSubject;
                                NotificationObj.EmailBody = ApproverText;
                                NotificationObj.ProjectCode = objFinalRoute.ProjectCode;
                                NotificationObj.ProjectName = objFinalRoute.ProjectName;
                                NotificationObj.RequesterName = objFinalRoute.RequesterName;

                                (new NotificationBLL()).SendEmail(NotificationObj, "A");
                                #endregion

                                #region for sending SMS
                                WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                                WIS_ConfigBLL WIS_ConfigBLL = new WIS_ConfigBLL();
                                WIS_ConfigBO = WIS_ConfigBLL.GetConfigSMSsending();
                                if (WIS_ConfigBO != null)
                                {
                                    if (WIS_ConfigBO.MobileStatus == "Y")
                                    {
                                        string Result = string.Empty;
                                        string SendsmsTest = SmsText + objFinalRoute.ProjectCode + objFinalRoute.ProjectName;
                                        NotificationBO SMSNotificationBO = new NotificationBO();
                                        NotificationBLL SMSNotificationBLL = new NotificationBLL();
                                        SMSNotificationBO.ProviderMobileNo = WIS_ConfigBO.MobileNumber;
                                        SMSNotificationBO.ProviderPasword = WIS_ConfigBO.MobilePassword;
                                        SMSNotificationBO.ProviderURL = WIS_ConfigBO.SiteUrl;

                                        SMSNotificationBO.CellNumber = objFinalRoute.CellNumber;
                                        SMSNotificationBO.SmsText = SendsmsTest;

                                        Result = SMSNotificationBLL.SENDSMS(SMSNotificationBO);
                                    }
                                }
                                #endregion
                                //NotificationObj.SendEmail(objFinalRoute.EmailID, EmailSubject, EmailBody, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);
                                //ResultValue = NotificationObj.SendSMS(objFinalRoute.CellNumber, SmsText, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);

                                string message = "Request has been Approved.";
                                if (message != "")
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                                BindGrid(false, false);
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;

                            }
                            if (ChangeRequest == "PAYRQ" && count == 1)
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                            }
                            if (ChangeRequest == "PAYRQ" && count == 2)
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                                PnlProjectDtl.Visible = false;
                            }
                            #endregion

                            #endregion

                            if (HfTHeaderID.Value.ToString() != "0")
                            {
                                // to Unlock Previos one
                                SharedAuthorizationBO objbo1 = new SharedAuthorizationBO();
                                objbo1.TRACKERHEADERID = Convert.ToInt32(HfTHeaderID.Value.ToString());
                                objbo1.LockStatus = "N";
                                objbo1.UpdateBy = Convert.ToInt32(Session["USER_ID"]);
                                (new SharedApprovalsBLL()).UPdateLockStatus(objbo1);
                            }
                        }
                        #endregion
                        else
                        {
                            if (ChangeRequest == "RTA")
                            {
                                string message = "Select Any one Route For Approved";
                                if (message != "")
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                            }
                            if (ChangeRequest == "PAYRQ")
                            {
                                int chec = 0;
                                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                                {
                                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                    if (Selectstatus.Checked)
                                    {
                                        UpdatePaymentStatus(BatchBLL.RequestStatus_Approved, "A");
                                        pnlFinalPojectdEtail.Visible = false;
                                        ApprovalMultiView.Visible = false;
                                        pnlAprovalFooter.Visible = false;
                                        PnlProjectDtl.Visible = false;
                                        chec = 1;
                                    }
                                    //else
                                    //{
                                    //    chec = 0;
                                    //}

                                    if (HfTHeaderID.Value.ToString() != "0")
                                    {
                                        // to Unlock Previos one
                                        SharedAuthorizationBO objbo1 = new SharedAuthorizationBO();
                                        objbo1.TRACKERHEADERID = Convert.ToInt32(HfTHeaderID.Value.ToString());
                                        objbo1.LockStatus = "N";
                                        objbo1.UpdateBy = Convert.ToInt32(Session["USER_ID"]);
                                        (new SharedApprovalsBLL()).UPdateLockStatus(objbo1);
                                    }
                                }
                                if (chec == 0)
                                {
                                    string message = "Select Any one Payment Request For Approved";
                                    if (message != "")
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        string message = "Approver Not Exit";
                        if (message != "")
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    }
                    #endregion
                }
            }

                #endregion

            #endregion
        }
        
        #region for Decline
        
        /// <summary>
        /// To decline a request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnDecline_Click(object sender, EventArgs e)
        {
            string ChangeRequest = string.Empty;
            string EmailSubject = string.Empty;
            string SmsText = string.Empty;
            string message = string.Empty;
            int SaveData = 0;

            if (ViewState["WorkFlowCode"] != null)
            {
                ChangeRequest = ViewState["WorkFlowCode"].ToString();
            }

            Boolean chkStatusofBatch = true;
            if (ChangeRequest == "PAYRQ")
            {
                string approvalStatus = string.Empty;
                int ChkCount = 0;
                //int SelcectCount = 0;

                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                {
                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                    if (Selectstatus.Checked)
                    {
                        ChkCount = ChkCount + 1;
                    }
                }
                if (ChkCount == 0)
                {
                    chkStatusofBatch = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alertsa", "alert('Please select a batch to Decline.')", true);
                }
            }

            int AppDataCount = 0;
            int DecDataCount = 0;

            #region New Batch Procedure

            if (ChangeRequest == "PAYRQ")
            {
                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                {

                    Label lblRequestStatus = (Label)gvw.FindControl("lblRequestStatus");
                    if (lblRequestStatus.Text.Trim().ToUpper() == "Approved".ToUpper())
                    {
                        AppDataCount++;
                    }
                    else if (lblRequestStatus.Text.Trim().ToUpper() == "Declined".ToUpper())
                    {
                        DecDataCount++;
                    }
                }

                //Batch Count
                int totalBatchcount = grdPaymentRequestBatch.Rows.Count;
                int count;
                MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();

                //Edwin: 11/04/2016 Parameters for creating next level request
                WorkflowApprovalBO objWorkflowapproval = new WorkflowApprovalBO();
                objWorkflowapproval.WorkflowapprovalId = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                objWorkflowapproval.Status = "DECLINED";
                objWorkflowapproval.AuthoriserID = 1;//Convert.ToInt32(Session["ROLE_ID"]);
                objWorkflowapproval.WorkFlowDefinationId = Convert.ToInt32(ViewState["WorkflowdefinationID"]);
                objWorkflowapproval.Auctiontakenby = Convert.ToInt32(Session["USER_ID"]);
                objWorkflowapproval.Approvercomments = txtapprovercomments.Text;
                int trackerheader = Convert.ToInt32(ViewState["TrackHdrId"]);
                objWorkflowapproval.TrackerHdrID = trackerheader;
                objWorkflowapproval.PageCode = Convert.ToString(ViewState["PageCode"]);
                if (ViewState["ApproverLevel"] != null) objWorkflowapproval.ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                if (ViewState["ProjectId"] != null) objWorkflowapproval.ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString());
                if (ViewState["HHID"] != null) objWorkflowapproval.HHID = Convert.ToInt32(ViewState["HHID"].ToString());
                if (ViewState["ElementID"] != null) objWorkflowapproval.ElementID = Convert.ToInt32(ViewState["ElementID"].ToString());
                objWorkflowapproval.WorkFlowCode = ChangeRequest;

                //Edwin: 11/04/2016 Batching Conditions and Logic

                if (AppDataCount > 0)
                {
                    if ((AppDataCount + DecDataCount) == (totalBatchcount - 1))
                    {
                        UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                        objWorkflowapproval.Status = "APPROVED";
                        count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, AppDataCount);

                        pnlFinalPojectdEtail.Visible = false;
                        ApprovalMultiView.Visible = false;
                        pnlAprovalFooter.Visible = false;
                        PnlProjectDtl.Visible = false;
                    }
                    else
                    {
                        UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");

                        pnlFinalPojectdEtail.Visible = false;
                        ApprovalMultiView.Visible = false;
                        pnlAprovalFooter.Visible = false;
                        PnlProjectDtl.Visible = false;
                    }

                }

                // tested and okay
                if (totalBatchcount == 1)
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                    MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
                    objMyTaskApprovalDAL.DeclineStatus(objWorkflowapproval);

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;
                }

                // tested and okay
                if (DecDataCount == (totalBatchcount - 2))
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;
                }

                // tested and okay
                if (DecDataCount == (totalBatchcount - 1))
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                    MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
                    objMyTaskApprovalDAL.DeclineStatus(objWorkflowapproval);

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;
                }

                // tested and okay
                else
                {
                    UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");

                    pnlFinalPojectdEtail.Visible = false;
                    ApprovalMultiView.Visible = false;
                    pnlAprovalFooter.Visible = false;
                    PnlProjectDtl.Visible = false;
                }

            }

            #endregion

            #region Old Batch Procedure

            else
            {

                if (chkStatusofBatch)
                {
                    string ResultValue = string.Empty;
                    MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();

                    int WorkflowdefinationID = 0;
                    ApprovalscoredtlBO objFinalRoute = new ApprovalscoredtlBO();

                    if (ViewState["WorkFlowApproverID"] != null)
                    {
                        WorkflowdefinationID = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                    }
                    objFinalRoute = objMytaskApprovalBLL.GetEmailID(WorkflowdefinationID);
                    #region objFinalRoute not null
                    if ((objFinalRoute) != null)
                    {
                        if (ChangeRequest == "RTA")
                        {
                            foreach (GridViewRow oRow in grdFinalProjectDtl.Rows)
                            {
                                CheckBox chkSelectRoute = (CheckBox)oRow.FindControl("chkSelectRoute");
                                if (chkSelectRoute.Checked)
                                {
                                    SaveData = 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            SaveData = 1;
                        }

                        #region for PAYRQ
                        int totalBatchcount = 0, tdec = 0, tapp = 0;

                        if (ChangeRequest == "PAYRQ")
                        {
                            int totalCount = grdPaymentRequestBatch.Rows.Count;
                            // int TotalApprovalCount = 0;
                            totalBatchcount = totalCount;
                            string approvalStatus = string.Empty;
                            int Count = 0;
                            //int SelcectCount = 0;

                            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                            {
                                CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                if (Selectstatus.Checked)
                                {
                                    tapp++;
                                    //Count = Count + 1;
                                }

                                if (totalCount == 1)
                                {
                                    Count = 1;
                                }
                                else
                                {
                                    Label RequestStatus = (Label)gvw.FindControl("lblRequestStatus");
                                    approvalStatus = (RequestStatus.Text.ToString());
                                    if (approvalStatus == "Approved")
                                    {
                                        Count = Count + 1;
                                        //tapp++;
                                    }
                                    if (approvalStatus == "Declined") //Declined
                                    {
                                        Count = Count + 1;
                                        tdec++;
                                    }
                                }
                            }
                            if (Count == (totalCount - 1))
                            {
                                SaveData = 1;

                            }
                            else if (Count == totalCount)
                            {
                                SaveData = 1;
                            }
                            else
                            {
                                SaveData = 0;
                            }
                        }
                        #endregion

                        // int AppDataCount = 0;
                        if (SaveData == 1)
                        {
                            int count = 2;
                            WorkflowApprovalBO objWorkflowapproval = new WorkflowApprovalBO();
                            objWorkflowapproval.WorkflowapprovalId = Convert.ToInt32(ViewState["WorkFlowApproverID"]);
                            objWorkflowapproval.Status = "DECLINED";
                            objWorkflowapproval.AuthoriserID = 1;//Convert.ToInt32(Session["ROLE_ID"]);
                            objWorkflowapproval.WorkFlowDefinationId = Convert.ToInt32(ViewState["WorkflowdefinationID"]);
                            objWorkflowapproval.Auctiontakenby = Convert.ToInt32(Session["USER_ID"]);
                            objWorkflowapproval.Approvercomments = txtapprovercomments.Text;
                            int trackerheader = Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
                            if (trackerheader != 0)
                            {
                                objWorkflowapproval.TrackerHdrID = trackerheader;
                            }
                            else
                            {
                                objWorkflowapproval.TrackerHdrID = 0;
                            }
                            objWorkflowapproval.PageCode = Convert.ToString(ViewState["PageCode"]);


                            #region for second to n - approval status checkin
                            // for Checking Second approval exit or not
                            if (ViewState["ApproverLevel"] != null)
                            {
                                objWorkflowapproval.ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                            }
                            if (ViewState["ProjectId"] != null)
                            {
                                objWorkflowapproval.ProjectID = Convert.ToInt32(ViewState["ProjectId"].ToString());
                            }
                            if (ViewState["HHID"] != null)
                            {
                                objWorkflowapproval.HHID = Convert.ToInt32(ViewState["HHID"].ToString());
                            }
                            if (ViewState["ElementID"] != null)
                            {
                                objWorkflowapproval.ElementID = Convert.ToInt32(ViewState["ElementID"].ToString());
                            }
                            objWorkflowapproval.WorkFlowCode = ChangeRequest;
                            #endregion

                            if (ChangeRequest == "PAYRQ")
                            {
                                int act = 0;
                                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                                {
                                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                    if (Selectstatus.Checked)
                                    {
                                        act = 1;
                                    }
                                    //else
                                    //{
                                    //    act = 0;
                                    //}
                                    Label lblRequestStatus = (Label)gvw.FindControl("lblRequestStatus");
                                    if (lblRequestStatus.Text.Trim().ToUpper() == "Approved".ToUpper())
                                    {
                                        AppDataCount++;
                                    }
                                }
                                //Edwin: 10/04/2016
                                // if (act > 1)
                                if (AppDataCount > 0)
                                {
                                    objWorkflowapproval.Status = "APPROVED";
                                    count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, AppDataCount);
                                }
                                else
                                {
                                    objWorkflowapproval.Status = "DECLINED";
                                    count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, AppDataCount);
                                }
                                //Edwin:
                            }
                            else
                            {
                                count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval, AppDataCount);
                            }

                            // count = objMytaskApprovalBLL.AddWorkflowApproval(objWorkflowapproval);

                            if (count == -1)
                            {

                                #region for SMS / Email
                                #region for EMAIL / SMS TEXT only
                                int HHID_ = Convert.ToInt32(ViewState["HHID"].ToString());
                                NotificationBO NotificationObj = new NotificationBO();
                                if (ChangeRequest == "RTA")
                                {
                                    EmailSubject = "Route Approval request is been DECLINED For Project :" + objFinalRoute.ProjectCode;
                                    SmsText = "Route Approval request is DECLINED For Project " + objFinalRoute.ProjectCode;
                                }
                                else if (ChangeRequest == "PAYRQ")
                                {
                                    EmailSubject = "Your Payment Request For Batch has been DECLINED";
                                    SmsText = "Your Payment Request For Batch has been DECLINED";
                                }
                                else if (ChangeRequest == "CR-FL")
                                {
                                    EmailSubject = "Package Closing Payment Request has been DECLINED";
                                    SmsText = "Package Closing Payment Request Has been DECLINED";
                                }
                                else if (ChangeRequest == "NEG")
                                {
                                    EmailSubject = "Your request for Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGC")
                                {
                                    EmailSubject = "Your request for Crops Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Crops is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGL")
                                {
                                    EmailSubject = "Your request for Land Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Land is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGF")
                                {
                                    EmailSubject = "Your request for Fixtures Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Fixtures is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGR")
                                {
                                    EmailSubject = "Your request for Replacement Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Replacement is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGD")
                                {
                                    EmailSubject = "Your request for Damaged crop Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Damaged crop is been DECLINED";
                                }
                                else if (ChangeRequest == "NEGCU")
                                {
                                    EmailSubject = "Your request for Cultural Property Negotiation Amount has been DECLINED";
                                    SmsText = "Your Negotiation Amount for Cultural Property is been DECLINED";
                                }
                                else if (ChangeRequest == "CRGRA")
                                {
                                    EmailSubject = "Your request for Grievances has been DECLINED";
                                    SmsText = "Your request Grievances has been DECLINED";
                                }
                                else if (ChangeRequest == "CDAPB")
                                {
                                    EmailSubject = "Your request for CDAPB has been DECLINED for the project:" + objFinalRoute.ProjectCode;
                                    SmsText = "Your request CDAPB has been DECLINED for the project:" + objFinalRoute.ProjectCode;
                                }
                                else
                                {
                                    EmailSubject = "your request for Change Request has been DECLINED for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_;
                                    SmsText = "your request for Change Request has been DECLINED for the project:" + objFinalRoute.ProjectCode + " HHID" + HHID_; ;
                                }

                                string ApproverText = txtapprovercomments.Text;

                                NotificationObj.WorkflowCode = ChangeRequest.ToString();
                                NotificationObj.EmailID = objFinalRoute.EmailID;
                                NotificationObj.EmailSubject = EmailSubject;
                                NotificationObj.EmailBody = ApproverText;
                                NotificationObj.ProjectCode = objFinalRoute.ProjectCode;
                                NotificationObj.ProjectName = objFinalRoute.ProjectName;
                                NotificationObj.RequesterName = objFinalRoute.RequesterName;

                                (new NotificationBLL()).SendEmail(NotificationObj, "D");
                                #endregion

                                #region for sending SMS
                                WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
                                WIS_ConfigBLL WIS_ConfigBLL = new WIS_ConfigBLL();
                                WIS_ConfigBO = WIS_ConfigBLL.GetConfigSMSsending();
                                if (WIS_ConfigBO != null)
                                {
                                    if (WIS_ConfigBO.MobileStatus == "Y")
                                    {
                                        string Result = string.Empty;
                                        string SendsmsTest = SmsText + objFinalRoute.ProjectCode + objFinalRoute.ProjectName;
                                        NotificationBO SMSNotificationBO = new NotificationBO();
                                        NotificationBLL SMSNotificationBLL = new NotificationBLL();
                                        SMSNotificationBO.ProviderMobileNo = WIS_ConfigBO.MobileNumber;
                                        SMSNotificationBO.ProviderPasword = WIS_ConfigBO.MobilePassword;
                                        SMSNotificationBO.ProviderURL = WIS_ConfigBO.SiteUrl;

                                        SMSNotificationBO.CellNumber = objFinalRoute.CellNumber;
                                        SMSNotificationBO.SmsText = SendsmsTest;

                                        Result = SMSNotificationBLL.SENDSMS(SMSNotificationBO);
                                    }
                                }
                                #endregion

                                #endregion

                                //NotificationObj.SendEmail(objFinalRoute.EmailID, EmailSubject, EmailBody, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);
                                //ResultValue = NotificationObj.SendSMS(objFinalRoute.CellNumber, SmsText, objFinalRoute.ProjectCode, objFinalRoute.ProjectName);

                                if (ChangeRequest == "RTA")
                                {
                                    message = "Request has been Declined.";
                                }
                                else
                                {
                                    message = "Request has been Declined.";
                                }
                                if (message != "")
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                                BindGrid(false, false);
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                            }
                            else if (count == 0 && ChangeRequest == "RTA")//final Approver 
                            {
                                UPdateFinalRoute();
                            }
                            if (count == 0 && ChangeRequest == UtilBO.PaymentRequestCode)
                            {
                                int act = 0;
                                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                                {
                                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                    if (Selectstatus.Checked)
                                    {
                                        act = 1;
                                    }
                                    //else
                                    //{
                                    //    act = 0;
                                    //}
                                }
                                if (act == 1)
                                {
                                    UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                                }
                                if (SaveData == 1)
                                {
                                    int USER_ID = 0;
                                    if (Session["USER_ID"] != null)
                                        USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                    int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                    Close_Batch(HHID_, USER_ID);
                                    pnlFinalPojectdEtail.Visible = false;
                                    ApprovalMultiView.Visible = false;
                                    pnlAprovalFooter.Visible = false;
                                    PnlProjectDtl.Visible = false;
                                }
                            }
                            if (count == -1 && ChangeRequest == "CRGRA")
                            {
                                string updateStatus = "D";
                                UpdateGRievancesStatus((Convert.ToInt32(ViewState["HHID"].ToString())), (Convert.ToInt32(ViewState["ElementID"].ToString())), updateStatus);
                            }
                            if (count == -1 && ChangeRequest == "CDAPB")
                            {
                                string updateStatus = "D";
                                UpdateCDAPBUGStatus(updateStatus);
                            }
                            if (count == -1 && ChangeRequest == UtilBO.PaymentRequestCode)
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                                PnlProjectDtl.Visible = false;
                                if (totalBatchcount == (tdec + tapp))
                                {
                                    int USER_ID = 0;
                                    if (Session["USER_ID"] != null)
                                        USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                    int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                    Close_Batch(HHID_, USER_ID);
                                }
                            }
                            if (count == 1 && ChangeRequest == UtilBO.PaymentRequestCode)
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                                PnlProjectDtl.Visible = false;
                                if (totalBatchcount == (tdec + tapp))
                                {
                                    int USER_ID = 0;
                                    if (Session["USER_ID"] != null)
                                        USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                    int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                    Close_Batch(HHID_, USER_ID);
                                }
                            }
                            if (count == 2 && ChangeRequest == UtilBO.PaymentRequestCode)
                            {
                                UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                                pnlFinalPojectdEtail.Visible = false;
                                ApprovalMultiView.Visible = false;
                                pnlAprovalFooter.Visible = false;
                                PnlProjectDtl.Visible = false;
                                if (totalBatchcount == (tdec + tapp))
                                {
                                    int USER_ID = 0;
                                    if (Session["USER_ID"] != null)
                                        USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());
                                    int HHID_ = Convert.ToInt32(ViewState["HHID"]);
                                    Close_Batch(HHID_, USER_ID);
                                }
                            }
                            if (ChangeRequest == "PAYVR")
                            {
                                //Added By Anjan For Update the Fund Status
                                PaymentBLL objPBll = new PaymentBLL();
                                objPBll.UpdateStatus(Convert.ToInt32(ViewState["HHID"].ToString()), "D");
                                //End
                            }
                        }
                        else
                        {
                            //message = "Select any one Route";
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                            if (ChangeRequest == "RTA")
                            {
                                message = "Select Any one Route For Decline";
                                if (message != "")
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                            }
                            if (ChangeRequest == "PAYRQ")
                            {
                                int CheckPayReqStatus = 0;
                                foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
                                {
                                    CheckBox Selectstatus = (CheckBox)gvw.FindControl("chkSelect");
                                    if (Selectstatus.Checked)
                                    {
                                        UpdatePaymentStatus(BatchBLL.RequestStatus_Declined, "D");
                                        pnlFinalPojectdEtail.Visible = false;
                                        ApprovalMultiView.Visible = false;
                                        pnlAprovalFooter.Visible = false;
                                        PnlProjectDtl.Visible = false;
                                        CheckPayReqStatus = 1;
                                    }
                                    //else
                                    //{
                                    //    CheckPayReqStatus = 0;
                                    //}
                                }
                                if (CheckPayReqStatus == 0)
                                {
                                    message = "Select Any one Payment Request For Approved";
                                    if (message != "")
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        message = "Approver is not defined.";

                        if (message != "")
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    }
                    #endregion
                }
            }
            #endregion
        }

        //update the final route
        /// <summary>
        /// to update the final route
        /// </summary>
        
        protected void UPdateFinalRoute()
        {
            ApprovalscoredtlBO objFinalRoute = new ApprovalscoredtlBO();
            int RouteID = 0;
            foreach (GridViewRow oRow in grdFinalProjectDtl.Rows)
            {
                CheckBox chkSelectRoute = (CheckBox)oRow.FindControl("chkSelectRoute");
                if (chkSelectRoute.Checked)
                {
                    Label lblRouteID = (Label)oRow.FindControl("lblRouteID");
                    RouteID = Convert.ToInt32(lblRouteID.Text);
                }
            }
            objFinalRoute.RouteID = RouteID;
            objFinalRoute.RouteDetails = txtapprovercomments.Text;
            objFinalRoute.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
            objMytaskApprovalBLL.UPdateFinalRoute(objFinalRoute);
        }
        
        /// <summary>
        /// update the final Value
        /// </summary>
        /// <param name="trackerheader_"></param>
        
        protected void UPdateFinalValue(int trackerheader_)
        {
            ApprovalscoredtlBO objFinalValue = new ApprovalscoredtlBO();

            int trackerheader = trackerheader_; //Convert.ToInt32(trackHeaderIDLabel.Text.ToString());
            if (trackerheader != 0)
            {
                objFinalValue.TrackerHdrID = trackerheader;
            }
            else
            {
                objFinalValue.TrackerHdrID = 0;
            }

            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
            objMytaskApprovalBLL.UPdateFinalValue(objFinalValue);
        }
        
        /// <summary>
        /// update the final Value For Individuval Neg amonts
        /// </summary>
        /// <param name="trackerheader_"></param>
        /// <param name="ChangeRequest"></param>
        
        private void UPdateFinalValueForIndNeg(int trackerheader_, string ChangeRequest)
        {
            ApprovalscoredtlBO objFinalValue = new ApprovalscoredtlBO();

            int trackerheader = trackerheader_; 

            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
            objMytaskApprovalBLL.UPdateFinalValueForIndNeg(trackerheader, ChangeRequest);
        }

        private int MyActiveStatusHHID { get; set; }
       
        /// <summary>
        /// To update Payment status
        /// </summary>
        /// <param name="RequestStatus"></param>
        /// <param name="StatusMsg"></param>
        
        private void UpdatePaymentStatus(string RequestStatus, string StatusMsg)
        {
            BatchBLL oBatchBLL = new BatchBLL();
            BatchBO oBatchBO;
            string message = string.Empty;
            int USER_ID = 0;
            int payemntRequestId = 0;
            int CMPBatchNo = 0;
            int PRHHID = 0;
            int ApprovalLevel = 0;
            int StausLevel_ = 0;

            if (Session["USER_ID"] != null)
                USER_ID = Convert.ToInt32(Session["USER_ID"].ToString());

            oBatchBO = new BatchBO();
            oBatchBO.RequestStatus = RequestStatus;
            oBatchBO.UpdatedBy = USER_ID;
            bool Flg = false;
            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
            {
                CheckBox chkSelectBatch = (CheckBox)gvw.FindControl("chkSelect");
                if (chkSelectBatch.Checked)
                {

                    if (ViewState["ApproverLevel"] != null)
                    {
                        ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                    }
                                        
                    payemntRequestId = Convert.ToInt32((gvw.FindControl("lblPaymentRequestId") as Label).Text);

                    Label lblCMPBatchNo = (Label)gvw.FindControl("lbl_CMP_BatchNo");
                    CMPBatchNo = Convert.ToInt32(lblCMPBatchNo.Text.ToString());

                    Label lblPRHHID = (Label)gvw.FindControl("lbl_PR_HHID");
                    PRHHID = Convert.ToInt32(lblPRHHID.Text.ToString());
                    //if (gvw.FindControl("lbl_PR_HHID") is Label)
                    //  MyActiveStatusHHID = Convert.ToInt32((gvw.FindControl("lbl_PR_HHID") as Label).Text);
                    if (StatusMsg == "A")
                    {
                         StausLevel_ = (ApprovalLevel + (Convert.ToInt32(UtilBO.BatchPaymentStatus)));
                    }
                    else if (StatusMsg == "D")
                    {
                        StausLevel_ = (ApprovalLevel);
                    }
                    oBatchBO.Payt_RequestID = payemntRequestId;
                    oBatchBO.CMP_BatchNo = CMPBatchNo.ToString();
                    oBatchBO.HHID = PRHHID;
                    oBatchBO.StausLevel = StausLevel_;
                    message = oBatchBLL.UpdatePaymentSubmit(oBatchBO);

                    oBatchBO.Comments = txtapprovercomments.Text.Trim();
                    if (oBatchBO.Comments.Length > 1000)
                        oBatchBO.Comments = oBatchBO.Comments.Substring(0, 999);
                    oBatchBLL.AddBatchComments(oBatchBO);
                    //Flg = true;
                }
            }
            //if (Flg)
               // Close_Batch(MyActiveStatusHHID, USER_ID);
        }

        #region code not used
        private void DeclinePaymetnStatus()
        {
            int payemntRequestId = 0;
            // BatchBO oBatchBO;
            BatchBLL oBatchBLL = new BatchBLL();


            foreach (GridViewRow gvw in grdPaymentRequestBatch.Rows)
            {
                payemntRequestId = Convert.ToInt32((gvw.FindControl("lblPaymentRequestId") as Label).Text);

                if (gvw.FindControl("lbl_PR_HHID") is Label)
                    MyActiveStatusHHID = Convert.ToInt32((gvw.FindControl("lbl_PR_HHID") as Label).Text);

                // oBatchBO.Payt_RequestID = payemntRequestId;

                oBatchBLL.DeclineBatchHHID(0, payemntRequestId, MyActiveStatusHHID);
                // Flg = true;
            }
        }
#endregion
        
        /// <summary>
        /// to Close the Batch
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="UserId"></param>
        
        private void Close_Batch(int HHID, int UserId)
        {
            BatchBLL oBatchBLL = new BatchBLL();
            int BatchNo = 0;
            if (ViewState["ElementID"] != null)
                BatchNo = Convert.ToInt32(ViewState["ElementID"]);
            string message = oBatchBLL.CloseBatch(HHID, UserId, BatchNo);
            //int FL = 0;
            if (message == "null")
            {
                string saveMessage = "Request has been Approved";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + saveMessage + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
        }
        
        #endregion

        /// <summary>
        /// Bind Papa Data releted to ths batch payment request
        /// </summary>
        /// <param name="myActiveHHID"></param>
        
        private void BindPAP(int myActiveHHID)
        {
            int ApprovalLevel = 0;

            BatchBLL oBatchBLL = new BatchBLL();
            int ElementaryId = 0; 
            string  status = string.Empty;
            if (ViewState["ElementID"] != null)
                ElementaryId = Convert.ToInt32(ViewState["ElementID"].ToString());

            if (ViewState["Status"] != null)
            {
                if (ViewState["Status"].ToString().ToUpper() == "PENDING")
                    status = "P";
                if (ViewState["Status"].ToString().ToUpper() == "DECLINED")
                    status = "D";
                if (ViewState["Status"].ToString().ToUpper() == "APPROVED")
                    status = "A";
            }
            if (ViewState["ApproverLevel"] != null)
            {
                ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
            }

            BatchList objBatchList = oBatchBLL.GetPaymentRequestByHHID(Convert.ToInt32(ViewState["ProjectId"]), myActiveHHID, ElementaryId, status, ApprovalLevel);//, Convert.ToInt32(ViewState["CMP_BATCHNO"]));
            decCount = 0;
            chkcount = 0;
            BtnApprove.Visible = true;
            btnDecline.Visible = true;
            grdPaymentRequestBatch.DataSource = objBatchList;
            grdPaymentRequestBatch.DataBind();
            if (grdPaymentRequestBatch.Rows.Count == decCount)
            {
                BtnApprove.Visible = false;
                btnDecline.Visible = false;
            }
            else if (chkcount > 0)
            {
                BtnApprove.Visible = true;
                btnDecline.Visible = true;
            }
            else
            {
                BtnApprove.Visible = false;
                btnDecline.Visible = false;
            }
            //new Code For Batch Comments
            lnkAppComments.Visible = true;

            string param = string.Format("OpenBatchComments({0},{1} );", ElementaryId, 0);
            lnkAppComments.Attributes.Add("onclick", param);
        }

        #region for payment Rquest Row Bind Data
        /// <summary>
        /// to Check How meny is pending in Bach payment request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentRequestBatch_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ApprovalLevel = 0;
                Label lblReqStatus = e.Row.FindControl("lblRequestStatus") as Label;
                CheckBox chkboxSelect = e.Row.FindControl("chkSelect") as CheckBox;
                Label lblStatusLevel = e.Row.FindControl("LblStausLevel") as Label;

                chkboxSelect.Attributes.Add("onclick", "valPayCheckBoxes(this);");
                string getCharacter = string.Empty;
                if (lblReqStatus.Text.Length > 0)
                {
                    getCharacter = lblReqStatus.Text.Substring(0, 1).ToLower();

                    if (ViewState["ApproverLevel"] != null)
                    {
                        ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                    }

                    if (ApprovalLevel == Convert.ToInt32(lblStatusLevel.Text))
                    {
                        if (lblReqStatus.Text.ToUpper() == BatchBLL.RequestStatus_Submitted.ToUpper())//(getCharacter == "p" || getCharacter == "r")
                        {
                            chkboxSelect.Visible = true;
                            chkcount++;
                        }
                        else
                        {
                            chkboxSelect.Visible = false;
                        }
                    }
                    if (ApprovalLevel == Convert.ToInt32(lblStatusLevel.Text))
                    {
                        if (lblReqStatus.Text.ToUpper() == BatchBLL.RequestStatus_Approved.ToUpper())//(getCharacter == "p" || getCharacter == "r")
                        {
                            chkboxSelect.Visible = true;
                            BtnApprove.Visible = true;
                            btnDecline.Visible = true;
                            chkcount++;
                        }
                    }
                    else
                    {
                        if (lblReqStatus.Text.ToUpper() == BatchBLL.RequestStatus_Declined.ToUpper())
                        {
                            BtnApprove.Visible = false;
                            btnDecline.Visible = false;
                            decCount++;
                        }
                            chkboxSelect.Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// to set Attributes to link buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentRequestBatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                int ApprovalLevel = 0;
                int ProjectId = 0;
                int userID = 0;
                if (ViewState["ProjectId"] != null)
                {
                    ProjectId = Convert.ToInt32(ViewState["ProjectId"]);
                }
                string pagecode = Convert.ToString(ViewState["PageCode"]);
                if (Session["USER_ID"] != null)
                    userID = Convert.ToInt32(Session["USER_ID"].ToString());
                string ProjectCode = Convert.ToString(ViewState["ProjectCode"]);

                string HHID = e.CommandArgument.ToString();

                if (ViewState["ApproverLevel"] != null)
                {
                    ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"].ToString());
                }
                LblHhidBatch.Text = "";
                if (ViewState["PageCode"].ToString() == "PAYRQ")
                {
                    spanPackage.Style.Add("display", "");
                    string PhotoModule = "PAP";
                    if (ViewState["ApproverLevel"] != null)
                    {
                        ApprovalLevel = Convert.ToInt32(ViewState["ApproverLevel"]);
                    }
                    LblHhidBatch.Text = "For HHID: " + HHID.ToString();
                    string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, PhotoModule);
                    lnkPapPhoto.Attributes.Add("onclick", paramPhotoView);
                    string paramSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", "CPREV-PAYRQ", ApprovalLevel);
                    lnkPackageDocument.Attributes.Add("onclick", paramSource);
                    string paramView = string.Format("OpenDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, "Readonly");
                    lnkUPloadDoclistSup.Attributes.Add("onclick", paramView);

                    string DocumentCode = "CMP_PKG";

                    string paramViewDocument = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectId, HHID, userID, ProjectCode, DocumentCode);
                    lnkUPloadDoclist.Attributes.Add("onclick", paramViewDocument);
                    lnkUPloadDoclist.Visible = false;

                    string paramViewSource = string.Format("OpenSourcePage({0},{1},{2},'{3}','{4}','{5}',{6});", ProjectId, HHID, userID, ProjectCode, "Readonly", ViewState["PageCode"].ToString(), ApprovalLevel);
                    if (ViewState["PageCode"].ToString() == "PAYRQ")
                        lnkPageSource.InnerText = "View Payment Request Details";

                    lnkPageSource.Attributes.Add("onclick", paramViewSource);
                    lnkPageSource.Visible = true;


                    string param = string.Format("OpenBatchComments({0},{1} );", Convert.ToInt32(ViewState["ElementID"].ToString()), HHID);
                    lnkAppComments.Attributes.Add("onclick", param);
                }
                else
                    spanPackage.Style.Add("display", "none");

            }
        }

        #endregion

        #region for Update Grievances
        /// <summary>
        /// To update Grievances
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="ElementID"></param>
        /// <param name="updateStatus"></param>
        public void UpdateGRievancesStatus(int HHID, int ElementID, string updateStatus)
        {
            GrievancesBO objGrievance = new GrievancesBO();
            objGrievance.Hhid = HHID;
            objGrievance.GrievanceID = ElementID;
            objGrievance.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
            objGrievance.ResolutionStatus = updateStatus;
            MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
            objMytaskApprovalBLL.UPdateGrievance(objGrievance);

        }
        #endregion

        #region for Update Pagackage Closing / CDAP
        /// <summary>
        /// To update Package Closing Info
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="updateStatus"></param>
        private void UpdatePackageClosingInfo(int HHID, string updateStatus)
        {
            string message = string.Empty, AlertMessage = string.Empty;
            PaymentBLL oPaymentBLL = new PaymentBLL();
            message = oPaymentBLL.UpdatePapValutaion(HHID, updateStatus);
        }
        /// <summary>
        /// To update CDAP Status
        /// </summary>
        /// <param name="updateStatus"></param>
        public void UpdateCDAPBUGStatus(string updateStatus)
        {
            ApprovalscoredtlBO objFinalRoute = new ApprovalscoredtlBO();

            foreach (GridViewRow oRow in grdCDAPBudget.Rows)
            {
                Literal litCDAPBUDID = (Literal)oRow.FindControl("litCDAPBUDID");
                if (litCDAPBUDID != null)
                {
                    ApprovalscoredtlBO objApprovalCDPABUG = new ApprovalscoredtlBO();
                    objApprovalCDPABUG.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                    objApprovalCDPABUG.CDAPBudgetID = Convert.ToInt32(litCDAPBUDID.Text);
                    objApprovalCDPABUG.Status = updateStatus;
                    MyTasks_ApprovalBLL objMytaskApprovalBLL = new MyTasks_ApprovalBLL();
                    objMytaskApprovalBLL.UPdateCDAPBUGStatus(objApprovalCDPABUG);
                }
            }
        }
        #endregion

        #region Grid CDAPBuget
        /// <summary>
        /// Set data to grdCDAPBudget grid
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="status"></param>
        public void BindgrdCDAPBudget(int ProjectID, string status)
        {
            CDAPBudgetBLL objCDAPBudgetBLL = new CDAPBudgetBLL();
            CDAPBudgetList objCDAPBudgetList = new CDAPBudgetList();
            string Status = status;
            objCDAPBudgetList = objCDAPBudgetBLL.GetCDAPBudget(ProjectID, Status);
            grdCDAPBudget.DataSource = objCDAPBudgetList;
            grdCDAPBudget.DataBind();
        }
        #endregion
    }
        // #endregion
}