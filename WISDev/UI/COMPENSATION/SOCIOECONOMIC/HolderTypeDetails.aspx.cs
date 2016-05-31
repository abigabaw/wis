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
    public partial class HolderTypeDetails : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call GetHolderType() to get the holder type from the database
        /// call FillCurrentSchoolStatus() to get the CurrentSchoolStatus from the database and fill data to the droupdown
        /// call FillSchoolDropReason() to get the SchoolDropReason from the database and fill data to the droupdown
        /// call   FillNeverAttendedSchool() to get the NeverAttendedSchool from the database and fill data to the dropdown
        /// call FillLiteracyLevel() to get the LiteracyLevel from the database
        /// call BindHolderTypes() to get the data from the database and bind it to the dropdown
        /// call GetHouseHoldDtlData() to get the HouseHoldData from the database
        /// set status of  the link button lnkHolderType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Request.QueryString["HHID"] != null)
                    Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                else
                    Session["HH_ID"] = null;
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.HouseholderDetails;
            if (!IsPostBack)
            {
                txtHolderName.Attributes.Add("onchange", "setDirtyText();");
                ViewState["RELATION_ID"] = "0";
                ViewState["HOLDERTYPE_ID"] = Request.QueryString["id"];
                GetHolderType();
                FillCurrentSchoolStatus();
                FillSchoolDropReason();
                FillNeverAttendedSchool();
                FillLiteracyLevel();
                BindHolderTypes();
                GetHouseHoldDtlData();
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkHolderType.Visible = false;
                    btnSaveHolder.Visible = false;
                    btnClearHolder.Visible = false;
                    grdHolders.Columns[grdHolders.Columns.Count - 1].Visible = false;
                    grdHolders.Columns[grdHolders.Columns.Count - 2].Visible = false;
                }
            }

            calDateOfBirth.Format = UtilBO.DateFormat;
            lnkHolderType.Visible = false;
            
          

            if (Mode == "Readonly")
            {
                lnkHolderType.Visible = false;
                btnSaveHolder.Visible = false;
                btnClearHolder.Visible = false;
                grdHolders.Columns[grdHolders.Columns.Count - 1].Visible = false;
                grdHolders.Columns[grdHolders.Columns.Count - 2].Visible = false;
            }
        }

        /// <summary>
        /// to get the HouseHoldData from the database
        /// </summary>
        private void GetHouseHoldDtlData()
        {            
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHousaeHoldData(Convert.ToInt32(Session["HH_ID"]));
            if (objHouseHold != null)
            {
                if (objHouseHold.DateofBirth.Trim() != "")
                    hfPapDOB.Value = Convert.ToDateTime(objHouseHold.DateofBirth).ToString(UtilBO.DateFormat);
                else
                    hfPapDOB.Value = "false";
            }
        }
        /// <summary>
        /// to get the CurrentSchoolStatus from the database and fill data to the droupdown
        /// </summary>

        private void FillCurrentSchoolStatus()
        {
            ddlCurrentSchoolStatus.DataSource = (new CurrentSchoolStatusBLL()).GetSchoolDetails();
            ddlCurrentSchoolStatus.DataTextField = "CurrentSchoolStatus";
            ddlCurrentSchoolStatus.DataValueField = "CurrentSchoolStatusID";
            ddlCurrentSchoolStatus.DataBind();
        }

        /// <summary>
        /// to get the SchoolDropReason from the database and fill data to the droupdown
        /// </summary>
        private void FillSchoolDropReason()
        {
            ddlSchoolDropReason.DataSource = (new SchoolDropBLL()).GetschoolDropReason();
            ddlSchoolDropReason.DataTextField = "Schooldropreason";
            ddlSchoolDropReason.DataValueField = "SchooldropreasonID";
            ddlSchoolDropReason.DataBind();
        }
        /// <summary>
        /// to get the NeverAttendedSchool from the database and fill data to the dropdown
        /// </summary>

        private void FillNeverAttendedSchool()
        {
            ddlNeverAttendedSchool.DataSource = (new NeverAttendedSchoolBLL()).FetchNeverAttendedSchool();
            ddlNeverAttendedSchool.DataTextField = "NVR_ATT_SCH_REASON";
            ddlNeverAttendedSchool.DataValueField = "NVR_ATT_SCH_REASONID";
            ddlNeverAttendedSchool.DataBind();
        }
        /// <summary>
        /// to update Frozen/Approval/Decline/Pending status
        /// </summary>

        #region for Frozen / Approval / Decline / Pedning status
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSaveHolder.Visible = false;
                    btnClearHolder.Visible = false;
                    grdHolders.Columns[grdHolders.Columns.Count - 1].Visible = false;
                    grdHolders.Columns[grdHolders.Columns.Count - 2].Visible = false;
                    lnkHolderType.Visible = true;
                    checkApprovalExitOrNot();
                    getApprrequtStatusHolderType();
                }
            }
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusHolderType.Text = "";
            StatusHolderType.Visible = false; // used to display the Status if you send Request for change data
            // getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHTRD");
                lnkHolderType.Attributes.Add("onclick", paramChangeRequest);

                lnkHolderType.Visible = true;
            }
            else
            {
                lnkHolderType.Visible = false;               
            }
            #endregion
            getApprrequtStatusHolderType();
        }

        public void ChangeRequestStatusHolderType()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHTRD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusHolderType()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHTRD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkHolderType.Visible = false;
                    btnSaveHolder.Visible = false;
                    btnClearHolder.Visible = false;
                    StatusHolderType.Visible = true;
                    StatusHolderType.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkHolderType.Visible = true;
                    btnSaveHolder.Visible = false;
                    btnClearHolder.Visible = false;
                    StatusHolderType.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkHolderType.Visible = false;
                    btnSaveHolder.Visible = true;
                    btnClearHolder.Visible = true;
                    grdHolders.Columns[grdHolders.Columns.Count - 1].Visible = true;
                    grdHolders.Columns[grdHolders.Columns.Count - 2].Visible = true;
                    StatusHolderType.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }

        #endregion
        /// <summary>
        /// to get the HolderType data from the database 
        /// </summary>
        private void GetHolderType()
        {
            HolderTypeBO objHolderType = (new PAP_RelationBLL()).GetHolderTypes(Convert.ToInt32(Session["HH_ID"]), Convert.ToInt32(ViewState["HOLDERTYPE_ID"]))[0];

            if (objHolderType != null)
            {
                Master.PageHeader = string.Format("Details of {0}", objHolderType.HolderTypeName);
                if (objHolderType.HolderTypeName.ToUpper() == "Children (U18)".ToUpper())
                {
                    hfDateCheck.Value = "true";
                    CustomValidator2.Enabled = true;
                    CustomValidator3.Enabled = true;
                    CustomValidator4.Enabled = false;
                }
                else if (objHolderType.HolderTypeName.ToUpper() == "Children (+18)".ToUpper())
                {
                    hfDateCheck.Value = "true";
                    CustomValidator3.Enabled = true;
                    CustomValidator2.Enabled = false;
                    CustomValidator4.Enabled = true;
                }
                else

                {
                    hfDateCheck.Value = "false";
                    CustomValidator3.Enabled = false;
                    CustomValidator2.Enabled = false;
                    CustomValidator4.Enabled = false;
                }
            }
            else
            {
                hfDateCheck.Value = "false";
                CustomValidator2.Enabled = false;
            }
        }
        /// <summary>
        /// to get the data from the database and bind it to the dropdown
        /// </summary>

        private void BindHolderTypes()
        {
            grdHolders.DataSource = (new PAP_RelationBLL()).GetRelations(Convert.ToInt32(Session["HH_ID"]), Convert.ToInt32(ViewState["HOLDERTYPE_ID"]));
            grdHolders.DataBind();
        }
        /// <summary>
        /// to get the LiteracyLevel from the database and fill it to the dropdown
        /// </summary>

        private void FillLiteracyLevel()
        {
            ddlLiteracyLevel.DataSource = (new LiteracyStatusBLL()).GetLiteracyStatus();
            ddlLiteracyLevel.DataValueField = "LTR_STATUSID";
            ddlLiteracyLevel.DataTextField = "LTR_STATUS";
            ddlLiteracyLevel.DataBind();
        }
        /// <summary>
        /// to save the data to the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSaveHolder_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            ChangeRequestStatusHolderType();
            PAP_RelationBO objRelation = new PAP_RelationBO();

            objRelation.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
            objRelation.HolderTypeID = Convert.ToInt32(ViewState["HOLDERTYPE_ID"]);
            objRelation.HolderName = txtHolderName.Text.Trim();
            if (dpDateOfBirth.Text.Trim().Length > 0 && Convert.ToDateTime(dpDateOfBirth.Text) != DateTime.MinValue)
                objRelation.DateOfBirth = Convert.ToDateTime(dpDateOfBirth.Text);
            objRelation.CurrentSchoolStatusID = Convert.ToInt32(ddlCurrentSchoolStatus.SelectedItem.Value);
            objRelation.NeverAttendedSchoolID = Convert.ToInt32(ddlNeverAttendedSchool.SelectedItem.Value);
            objRelation.SchoolDropReasonID = Convert.ToInt32(ddlSchoolDropReason.SelectedItem.Value);
            objRelation.LiteracyLevelID = Convert.ToInt32(ddlLiteracyLevel.SelectedItem.Value);
            objRelation.Sex = ddlGender.SelectedItem.Value;

            if (chkResideOnAffectedLand.Checked)
                objRelation.ResideOnAffectedLand = "YES";
            else
                objRelation.ResideOnAffectedLand = "NO";

            if (Convert.ToInt32(ViewState["RELATION_ID"]) > 0)
            {
                objRelation.RelationID = Convert.ToInt32(ViewState["RELATION_ID"]);
                objRelation.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                ClearDetails();
                message = (new PAP_RelationBLL()).UpdateRelation(objRelation);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            else
            {
                objRelation.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = (new PAP_RelationBLL()).AddRelation(objRelation);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            projectFrozen();
           
            ClearDetails();
            BindHolderTypes();
            SetUpdateMode(false);
        }
        /// <summary>
        /// to clear the data fields by calling ClearDetails() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClearHolder_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// to clear the data of the dropdown's and textbox's
        /// </summary>

        protected void ClearDetails()
        {
            txtHolderName.Text = "";
            dpDateOfBirth.Text = "";
            ddlCurrentSchoolStatus.ClearSelection();
            ddlNeverAttendedSchool.ClearSelection();
            ddlSchoolDropReason.ClearSelection();
            ddlLiteracyLevel.ClearSelection();
            ddlGender.ClearSelection();
            chkResideOnAffectedLand.Checked = false;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdHolders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["RELATION_ID"] = e.CommandArgument;

                PAP_RelationBO objRelation = (new PAP_RelationBLL()).GetRelationByID(Convert.ToInt32(ViewState["RELATION_ID"]));

                if (objRelation != null)
                {
                    txtHolderName.Text = objRelation.HolderName;
                    if (objRelation.DateOfBirth != DateTime.MinValue)
                        dpDateOfBirth.Text = objRelation.DateOfBirth.ToString(UtilBO.DateFormat);

                    ddlCurrentSchoolStatus.ClearSelection();
                    if (ddlCurrentSchoolStatus.Items.FindByValue(objRelation.CurrentSchoolStatusID.ToString()) != null)
                    {
                        ddlCurrentSchoolStatus.Items.FindByValue(objRelation.CurrentSchoolStatusID.ToString()).Selected = true;
                    }

                    ddlNeverAttendedSchool.ClearSelection();
                    if (ddlNeverAttendedSchool.Items.FindByValue(objRelation.NeverAttendedSchoolID.ToString()) != null)
                    {
                        ddlNeverAttendedSchool.Items.FindByValue(objRelation.NeverAttendedSchoolID.ToString()).Selected = true;
                    }

                    ddlSchoolDropReason.ClearSelection();
                    if (ddlSchoolDropReason.Items.FindByValue(objRelation.SchoolDropReasonID.ToString()) != null)
                    {
                        ddlSchoolDropReason.Items.FindByValue(objRelation.SchoolDropReasonID.ToString()).Selected = true;
                    }

                    ddlLiteracyLevel.ClearSelection();
                    if (ddlLiteracyLevel.Items.FindByValue(objRelation.LiteracyLevelID.ToString()) != null)
                    {
                        ddlLiteracyLevel.Items.FindByValue(objRelation.LiteracyLevelID.ToString()).Selected = true;
                    }

                    if (objRelation.ResideOnAffectedLand == "YES")
                        chkResideOnAffectedLand.Checked = true;
                    else
                        chkResideOnAffectedLand.Checked = false;

                    ddlGender.ClearSelection();
                    if (ddlGender.Items.FindByValue(objRelation.Sex) != null)
                        ddlGender.Items.FindByValue(objRelation.Sex).Selected = true;
                }

                SetUpdateMode(true);
                BindHolderTypes();
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                PAP_RelationBLL objPAPRelationBLL = new PAP_RelationBLL();

                message = objPAPRelationBLL.DeleteRelation(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
                ViewState["RELATION_ID"] = "0";
                ClearDetails();
                SetUpdateMode(false);
                BindHolderTypes();
            }
        }
        /// <summary>
        /// to set the status of the panel
        /// </summary>
        /// <param name="updateMode"></param>

        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSaveHolder.Text = "Update";
                btnClearHolder.Text = "Cancel";
            }
            else
            {
                btnSaveHolder.Text = "Save";
                btnClearHolder.Text = "Clear";
                ViewState["RELATION_ID"] = "0";
            }
        }
        /// <summary>
        /// Set link attributes to Holder link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdHolders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dateOfBirth = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DateOfBirth"));
                if (dateOfBirth != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litDateOfBirth")).Text = dateOfBirth.ToString(UtilBO.DateFormat);
            }
        }
        /// <summary>
        /// to set the page indexing to the grid grdHolders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdHolders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHolders.PageIndex = e.NewPageIndex;
            BindHolderTypes();
        }
        /// <summary>
        /// to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RefreshRelationsList", "RefreshRelationsList();", true);
        }
    }
}