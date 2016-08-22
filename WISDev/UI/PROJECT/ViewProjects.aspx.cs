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
    public partial class ViewProjects : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            caldpProjStartDate.Format = UtilBO.DateFormat;
            caldpProjEndDate.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                Master.PageHeader = "View Projects";
                BindProjects(true);
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == true)
                {
                    grdProjects.Columns[grdProjects.Columns.Count - 2].Visible = false;
                    grdProjects.Columns[grdProjects.Columns.Count - 3].Visible = false;
                    //for (int i = grdProjects.Columns.Count - 1; i >= 0; i--)
                    //{
                    //    if (i > grdProjects.Columns.Count - 4)
                    //        grdProjects.Columns[i].Visible = false;
                    //}
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.VIEW_PROJECT) == false)
                {
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }
            }
        }
        /// <summary>
        /// freezes the project
        /// </summary>
        public void getFrozen()
        {
            ProjectBLL ObjProjectBLL = new ProjectBLL();
            ProjectBO ObjProjectBO = new ProjectBO();

            ObjProjectBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);


            ObjProjectBO = ObjProjectBLL.getFrozen(ObjProjectBO);

            if ((ObjProjectBO) != null)
            {
                Session["FROZEN"] = ObjProjectBO.Frozen;
            }

        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindProjects(bool searchMode)
        {
            string projectName = String.Empty;
            string startDate = String.Empty;
            string endDate = String.Empty;
            string projectStatus = String.Empty;

            if (txtProjectName.Text.Trim() != "") projectName = txtProjectName.Text.Trim();
            if (searchMode && dpProjStartDate.Text != DateTime.MinValue.ToString()) startDate = Convert.ToString(dpProjStartDate.Text.ToString());
            if (searchMode && dpProjEndDate.Text != DateTime.MinValue.ToString()) endDate = Convert.ToString(dpProjEndDate.Text.ToString());
            if (ddlProjectStatus.SelectedIndex > 0) projectStatus = ddlProjectStatus.SelectedValue;

            grdProjects.DataSource = (new ProjectBLL()).GetProjects(projectName, startDate, endDate, projectStatus, Convert.ToInt32(Session["USER_ID"]));
            grdProjects.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditProject")
            {
                Session["PROJECT_ID"] = e.CommandArgument;
                Session["FROZEN"] = null;
                getFrozen();
                Session["PROJECT_CODE"] = ((LinkButton)e.CommandSource).Text;
                Session["HH_ID"] = null;

                if (Request.QueryString["mode"] == "redir")
                    Response.Redirect(ResolveUrl("~/UI/Compensation/PAPList.aspx"));
                else if (Request.QueryString["mode"] == "redirCDAP")
                    Response.Redirect(ResolveUrl("~/UI/Compensation/CDAPImplementation.aspx"));
                else
                {
                    if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                    {
                        Response.Redirect(ResolveUrl("~/UI/Compensation/PAPList.aspx"));
                    }
                    else
                        Response.Redirect("ProjectDetails.aspx");
                }
            }
            else if (e.CommandName == "FreezeProject")
            {
                ProjectBLL ObjProjectBLL = new ProjectBLL();

                ObjProjectBLL.FreezeProject(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USER_ID"]));
                Session["FROZEN"] = null;
                getFrozen();
                BindProjects(false);
            }
            else if (e.CommandName == "DataVerificationProject")
            {
                string message = string.Empty;

                // Session["PROJECT_ID"] = e.CommandArgument;

                WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                string ChangeRequestCode = UtilBO.WorkflowDataVerification;

                objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(e.CommandArgument), ChangeRequestCode);

                string pageCode = "DATAV";

                if (objWorkFlowBO != null)
                {
                    int userID = Convert.ToInt32(Session["USER_ID"]);
                    int ProjectID = Convert.ToInt32(e.CommandArgument);
                    int HHID = Convert.ToInt32(Session["HH_ID"]);

                    string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                    ClientScript.RegisterStartupScript(this.GetType(), "DATAVERIFICATIONPROJECT", paramChangeRequest, true);
                }
                else
                {
                    message = "Data Verification Approval is not defined";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
            }
            else if (e.CommandName == "Unfreeze")
            {
                //Call the Popwindow here
                int ProjectId = 0;
                string ProjectCode = string.Empty;

                string CommandArguments = e.CommandArgument.ToString();
                string[] strArguments = e.CommandArgument.ToString().Split(new char[] { ',' });

                if (strArguments.Length > 1)
                {
                    ProjectId = Convert.ToInt32(strArguments[0]);
                    ProjectCode = strArguments[1];
                }
                string callUnfreezeWindow = string.Format("UnfreezeWindow({0},'{1}',{2})", ProjectId, ProjectCode, Convert.ToInt32(Session["USER_ID"]));
                ClientScript.RegisterStartupScript(this.GetType(), "callUnfreezeWindow", callUnfreezeWindow, true);

                BindProjects(false);
            }
        }
        /// <summary>
        /// used to search details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindProjects(true);
        }
        /// <summary>
        /// clears search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtProjectName.Text = "";
            dpProjStartDate.Text = "";
            dpProjEndDate.Text = "";
            ddlProjectStatus.ClearSelection();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + dpProjStartDate.ClientID + "');ClearDateField('" + dpProjEndDate.ClientID + "');", true);
            BindProjects(false);
        }
        /// <summary>
        /// To load controls in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ApprovalStatus = 0;
                DateTime projStartDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ProjectStartDate"));
                if (projStartDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litProjectStartDate")).Text = projStartDate.ToString(UtilBO.DateFormat);

                DateTime projEndDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ProjectEndDate"));
                if (projEndDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litProjectEndDate")).Text = projEndDate.ToString(UtilBO.DateFormat);

                LinkButton lnkDataVerification = (LinkButton)e.Row.FindControl("lnkDataVerification");
                LinkButton lnkFreeze = (LinkButton)e.Row.FindControl("lnkFreeze");
                lnkFreeze.Visible = false;

                Literal litProjectID = ((Literal)e.Row.FindControl("litProjectID"));

                Literal litDataVerification = ((Literal)e.Row.FindControl("litDataVerification"));

                if (litProjectID != null)
                {
                    WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                    WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                    string ChangeRequestCode = UtilBO.WorkflowDataVerification;

                    objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(litProjectID.Text), ChangeRequestCode);

                    if (objWorkFlowBO == null)
                    {
                        lnkFreeze.Visible = false;
                        lnkDataVerification.Visible = false;
                        litDataVerification.Visible = true;
                        litDataVerification.Text = "Define Approver";
                    }
                    else
                    {
                        //ApprovalStatus = getApprrequtStatusDataVerification(Convert.ToInt32(litProjectID.Text));
                        ApprovalStatus = totalcountapproval(Convert.ToInt32(litProjectID.Text));
                        if (ApprovalStatus == 1)
                        {
                            lnkFreeze.Visible = true;
                            lnkDataVerification.Visible = false;
                            litDataVerification.Visible = false;
                            litDataVerification.Text = string.Empty;

                            //lnkFreeze.Visible = true;
                            //lnkDataVerification.Visible = false;
                            //litDataVerification.Visible = false;
                        }
                        else if (ApprovalStatus == 2)
                        {
                            lnkFreeze.Visible = false;
                            lnkDataVerification.Visible = true;
                            litDataVerification.Visible = false;
                            //litDataVerification.Text = "Approval Pending";
                        }
                        else if (ApprovalStatus == 3)
                        {
                            lnkFreeze.Visible = false;
                            lnkDataVerification.Visible = false;
                            litDataVerification.Visible = false;
                        }
                    }
                }
                //lnkFreeze.Visible = false;

                string frozen = DataBinder.Eval(e.Row.DataItem, "Frozen").ToString();

                if (frozen == "Y")
                {
                    ((LinkButton)e.Row.FindControl("lnkFreeze")).Visible = false;
                    //((Literal)e.Row.FindControl("litFrozen")).Visible = true;//Under Testing
                    lnkDataVerification.Visible = false;
                    litDataVerification.Text = string.Empty;
                    litDataVerification.Visible = false;

                    ((LinkButton)e.Row.FindControl("lnkFrozen")).Visible = true; //Under Testing
                    ((Panel)e.Row.FindControl("pnlUnfreeze")).Visible = true; //Under Testing
                }
                else
                {
                    ((Panel)e.Row.FindControl("pnlUnfreeze")).Visible = false; //Under Testing
                }

                int routeCount = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RouteCount"));

                if (routeCount == 0)
                {
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkRouteMap")).Visible = false;
                }
                else
                {
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkRouteMap")).Attributes.Add("onclick", string.Format("OpenRouteMap({0});", DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString()));
                }
            }
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdProjects.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindProjects(true);
        }
        /// <summary>
        /// get ApprrequtStatusDataVerification
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public int getApprrequtStatusDataVerification(int ProjectId)
        {
            int ApprovalStatus = 0;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = ProjectId;
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "DATAV";
            objHouseHold.Workflowcode = UtilBO.WorkflowDataVerification;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    ApprovalStatus = 3;
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    ApprovalStatus = 2;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    ApprovalStatus = 1;
                }
            }
            else
            {
                ApprovalStatus = 0;
            }
            return ApprovalStatus;
        }
        /// <summary>
        /// method for total count of approval
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public int totalcountapproval(int ProjectId)
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;
            WorkFlowList objWorkFlowList_ = null;

            string ChangeRequestCode = UtilBO.WorkflowDataVerification;

            objProjectRoute.WorkFlowApprover = ChangeRequestCode;
            objProjectRoute.Project_Id = ProjectId;

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    //if (Session["HH_ID"] != null)
                    //{
                    //    objProjectRoute.HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                    //}
                    //else
                    //{
                    //    objProjectRoute.HHID = 0;
                    //}
                    objProjectRoute.HHID = 0;
                    //objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = UtilBO.WorkflowDataVerification; // objHouseHold.PageCode = "DATAV";
                    objProjectRoute.WorkflowCode = UtilBO.WorkflowDataVerification;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    //addtional list
                    objWorkFlowList_ = objProjectRouteBLL.ApprovalStatuschecklist(objProjectRoute);

                    if (objPrintApprovalWF != null)
                    {
                        if (objWorkFlowList[i].CountApproval == objPrintApprovalWF.LEVEL)
                        {
                            if (objPrintApprovalWF.ApprovalstatusID == 1)
                            {
                                finalcount = 1;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 2)
                            {
                                finalcount = 2;
                                approvalcount = 0;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 3)
                            {
                                finalcount = 3;
                                approvalcount = 0;
                                break;
                            }
                        }
                        else
                        {
                            //i + 1; addtionl Code
                            if (objWorkFlowList[i].CountApproval == objWorkFlowList_[i].LEVEL)
                            {
                                if (objWorkFlowList_[i].ApprovalstatusID == 3)
                                {
                                    finalcount = 3;
                                    break;
                                }
                                else
                                {
                                    approvalcount = 0;
                                }
                            }
                        }
                    }
                    if (objWorkFlowList.Count == approvalcount)
                    {
                        finalcount = 1;
                    }
                    else
                    {
                        finalcount = 0;
                    }
                }
            }

            return finalcount;
        }
    }
}