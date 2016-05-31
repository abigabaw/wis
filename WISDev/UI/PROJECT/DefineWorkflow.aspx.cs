using System;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using System.Web.UI;

namespace WIS
{
    public partial class DefineWorkflow : System.Web.UI.Page
    {
        #region PageLoding  
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.Workflow;

            if (Session["userName"] != null)
            {
                //Retrieving UserName from Session
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
                string projectID = Session["PROJECT_ID"].ToString();
            }
            if (!IsPostBack)
            {

                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Workflow Definition";

                ViewState["WorkApprovalID"] = 0;  // ViewState of TRN_WORKFLOW_APPROVAl
                ViewState["WorkFlowDefID"] = 0; //Viewstate of TRN_WorkFlow_Definiation
                BindGrid(false, false); //Calling the Grid Data
                BindGridWorkFlowDefinition();
                screenintiallization();
                ApprovalTable.Visible = false;
                CancelButton.Visible = false;
                UpDateButton.Visible = false;
                ViewState["StatusMode"] = "None";
                WORKFLOWDEFINITIONIDTextBox.Text = "0";
                WorkApproverIDTextBox.Text = "0";
                WorkDefinationTextBox.Text = "0";
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    ViewState["StatusMode"] = "ReadOnly";
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    ADDButton.Visible = false;
                    grdWorkflowItem.Columns[grdWorkflowItem.Columns.Count - 1].Visible = false;
                    grdWorkflowItem.Columns[grdWorkflowItem.Columns.Count - 2].Visible = false;
                    grdApprover.Columns[grdApprover.Columns.Count - 1].Visible = false;
                    grdApprover.Columns[grdApprover.Columns.Count - 2].Visible = false;
                }
            }
        }
        #endregion

        #region ScreenIntialization Data
        /// <summary>
        /// Calls methods required for initialization of data on screen load
        /// </summary>
        public void screenintiallization()
        {
            getModule(); //workflow Module
            getWorkfolwitemByModuleID(); //workflow Definition Item
            getAllRole(); //Approval Role by personal project
            getLevel();    //Approval Level
            getAllProjectPersonUser(); //HightAuthDropDownList
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


            ModuleDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            ModuleDropDownList.DataTextField = "MODULENAME";
            ModuleDropDownList.DataValueField = "MODULEID";
            ModuleDropDownList.DataBind();
            ModuleDropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            ModuleDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        ///  method to bind data to WorkflowItemDropDownList from database
        /// </summary>
        public void getWorkfolwitemByModuleID()
        {
            int ModuleID = Convert.ToInt32(ModuleDropDownList.SelectedItem.Value.ToString());

            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            objWorkFlowList = WorkFlowBLLobj.GetWorkFlowByModuleId(ModuleID);

            ListItem lstItem = WorkflowItemDropDownList.Items[0];
            WorkflowItemDropDownList.Items.Clear();

            WorkflowItemDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            WorkflowItemDropDownList.DataTextField = "WorkDesc";
            WorkflowItemDropDownList.DataValueField = "WorkflowID";
            WorkflowItemDropDownList.DataBind();
            WorkflowItemDropDownList.Items.Insert(0, lstItem);

        }
        /// <summary>
        ///  method to bind data to HightAuthDropDownList from database
        /// </summary>
        public void getAllProjectPersonUser()
        {
            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            string projectID = Session["PROJECT_ID"].ToString();
            objWorkFlowList = WorkFlowBLLobj.getAllProjectPersonUser(projectID);

            ListItem lstItem1 = HightAuthDropDownList.Items[0];
            HightAuthDropDownList.Items.Clear();

            HightAuthDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            HightAuthDropDownList.DataTextField = "UserName";
            HightAuthDropDownList.DataValueField = "UserID";
            HightAuthDropDownList.DataBind();
            HightAuthDropDownList.Items.Insert(0, lstItem1);
        }
        /// <summary>
        ///  method to bind data to ApproverRoleNameDropDownList from database
        /// </summary>
        public void getAllRole()
        {
            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            string projectID = Session["PROJECT_ID"].ToString();
            objWorkFlowList = WorkFlowBLLobj.getAllEmpName(projectID);

            ListItem lstItem = ApproverRoleNameDropDownList.Items[0];
            ApproverRoleNameDropDownList.Items.Clear();

            ApproverRoleNameDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            ApproverRoleNameDropDownList.DataTextField = "RoleName";
            ApproverRoleNameDropDownList.DataValueField = "RoleID";
            ApproverRoleNameDropDownList.DataBind();
            ApproverRoleNameDropDownList.Items.Insert(0, lstItem);
            ApproverRoleNameDropDownList.SelectedIndex = 0;
          //Comment add by Ramu under gudines of amalesh
            //HightAuthDropDownList
           
        }
        /// <summary>
        /// method to bind data to AfterDays DropDown List from database and add values to trigger dropdownlist
        /// </summary>
        public void getLevel()
        {
            ListItem lstItem = ApproverLevelDropDownList.Items[0];
            ApproverLevelDropDownList.Items.Clear();
            ApproverLevelDropDownList.Items.Insert(0, lstItem);
            for (int i = 1; i <= 10; i++)
            {
                ApproverLevelDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            //AfterDays DropDown List 
            AfterDropDownList.Items.Clear();
            AfterDropDownList.Items.Add(new ListItem("-- Select --", "0"));
            AfterDropDownList.SelectedIndex = 0;

            for (int i = 1; i <= 15; i++)
            {
                AfterDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            TriggerDropDownList.Items.Clear();
            TriggerDropDownList.Items.Add(new ListItem("-- Select --", "0"));
            TriggerDropDownList.Items.Add(new ListItem("SMS", "SMS"));
            TriggerDropDownList.Items.Add(new ListItem("EMail", "EMail"));
            TriggerDropDownList.Items.Add(new ListItem("Both", "Both"));
            TriggerDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        /// method to fetch userdetails based on roleid
        /// </summary>
        /// <param name="roleID"></param>
        public void getUserByRoleID(int roleID)
        {
           
            WorkFlowBO objWorkFlow = new WorkFlowBO();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            int projectID = Convert.ToInt32(Session["PROJECT_ID"].ToString());
            //int roleID_ = roleID;

            objWorkFlowList = WorkFlowBLLobj.getApprovalRoleUserID(projectID, roleID);

            ListItem lstItem = ApproverNameDropDownList.Items[0];
            ApproverNameDropDownList.Items.Clear();

            ApproverNameDropDownList.DataSource = objWorkFlowList;//dt.Tables[0];
            ApproverNameDropDownList.DataTextField = "ApproverUserName";
            ApproverNameDropDownList.DataValueField = "ApproverUserID";
            ApproverNameDropDownList.DataBind();
            ApproverNameDropDownList.Items.Insert(0, lstItem);

        }
        #endregion 

        #region for workflow Defination 

        #region WorkflowItem Save / EDIT / Update
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            int count;
            WorkFlowBLL WorkFlowBLLOBJ = new WorkFlowBLL();
            if (WORKFLOWDEFINITIONIDTextBox.Text != "0")
            {
                string message = string.Empty;
                string AlertMessage = string.Empty;

                string uID = Session["USER_ID"].ToString();
                string pID = Session["PROJECT_ID"].ToString();

                WorkFlowBO objWorkFlow = new WorkFlowBO();
                objWorkFlow.WorkFlowDefID = Convert.ToInt32(WORKFLOWDEFINITIONIDTextBox.Text.ToString());
                objWorkFlow.ModuleID = Convert.ToInt32(ModuleDropDownList.SelectedItem.Value.ToString().Trim());
                objWorkFlow.WorkflowID = Convert.ToInt32(WorkflowItemDropDownList.SelectedItem.Value.ToString());
                objWorkFlow.HighaultorityID = Convert.ToInt32(HightAuthDropDownList.SelectedItem.Value.ToString());
                objWorkFlow.Trigger = TriggerDropDownList.SelectedItem.Value.ToString();
                objWorkFlow.AfterDays = Convert.ToInt32(AfterDropDownList.SelectedItem.Value.ToString());
                objWorkFlow.UserID = Convert.ToInt32(uID);
                objWorkFlow.ProjectID = Convert.ToInt32(pID);

                WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
                count = WorkFlowBLLobj.EditWorkFlowDef(objWorkFlow);
                if (count == -1)
                {
                    BindGridWorkFlowDefinition();
                    WORKFLOWDEFINITIONIDTextBox.Text = "0";
                    message = "Data updated successfully.";
                    AlertMessage = "alert('" + message + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    clear();
                    SetUpdateMode(false);
                }
                if (count == 1)
                {
                    BindGridWorkFlowDefinition();
                    WORKFLOWDEFINITIONIDTextBox.Text = "0";
                    message = "Workflow Item Already Exist";
                    AlertMessage = "alert('" + message + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    clear();
                    SetUpdateMode(false);
                }

            }
            else
            {
                try
                {
                    string message = string.Empty;
                    string AlertMessage = string.Empty;

                    string uID = Session["USER_ID"].ToString();
                    string pID = Session["PROJECT_ID"].ToString();

                    WorkFlowBO objWorkFlow = new WorkFlowBO();
                    objWorkFlow.ModuleID = Convert.ToInt32(ModuleDropDownList.SelectedItem.Value.ToString().Trim());
                    objWorkFlow.WorkflowID = Convert.ToInt32(WorkflowItemDropDownList.SelectedItem.Value.ToString());
                    objWorkFlow.HighaultorityID = Convert.ToInt32(HightAuthDropDownList.SelectedItem.Value.ToString());
                    objWorkFlow.Trigger = TriggerDropDownList.SelectedItem.Value.ToString();
                    objWorkFlow.AfterDays = Convert.ToInt32(AfterDropDownList.SelectedItem.Value.ToString());
                    objWorkFlow.UserID = Convert.ToInt32(uID);
                    objWorkFlow.ProjectID = Convert.ToInt32(pID);

                    WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
                    count = WorkFlowBLLobj.InsertWorkFlow(objWorkFlow);

                    if (count == -1)
                    {
                        BindGridWorkFlowDefinition();
                        message = "Data saved successfully.";
                        AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                        clear();
                    }
                    if (count == 1)
                    {
                        BindGridWorkFlowDefinition();
                        message = "Workflow Item Already Exist";
                        AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                        clear();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    WorkFlowBLLOBJ = null;
                }
            }
                      
        }
        #endregion

        #region Workflow BindGrid Data / Edit / Delete/ view Approval Table
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        public void BindGridWorkFlowDefinition()
        {
            //TRN_WORKFLOW_DEFINITION
            string projectID = Session["PROJECT_ID"].ToString();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            grdWorkflowItem.DataSource = WorkFlowBLLobj.GetWorkFlowDefinition(projectID);
            grdWorkflowItem.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdWorkflowItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["WorkFlowDefID"] = e.CommandArgument;
                WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();
                objWorkFlowBO = objWorkFlowBLL.GetWorkFlowDefByID(Convert.ToInt32(ViewState["WorkFlowDefID"]));
                //Editing set the value to the respective dropDown box and textbox
                if (objWorkFlowBO != null)
                {
                    ModuleDropDownList.ClearSelection();
                    if (ModuleDropDownList.Items.FindByValue(objWorkFlowBO.ModuleID.ToString()) != null)
                        ModuleDropDownList.Items.FindByValue(objWorkFlowBO.ModuleID.ToString()).Selected = true;

                    getWorkfolwitemByModuleID();

                    WorkflowItemDropDownList.ClearSelection();
                    if (WorkflowItemDropDownList.Items.FindByValue(objWorkFlowBO.WorkflowID.ToString()) != null)
                        WorkflowItemDropDownList.Items.FindByValue(objWorkFlowBO.WorkflowID.ToString()).Selected = true;

                    getAllRole();

                    HightAuthDropDownList.ClearSelection();
                    if (HightAuthDropDownList.Items.FindByValue(objWorkFlowBO.HighaultorityID.ToString()) != null)
                        HightAuthDropDownList.Items.FindByValue(objWorkFlowBO.HighaultorityID.ToString()).Selected = true;

                    getLevel();

                    ApproverLevelDropDownList.ClearSelection();
                    if (ApproverLevelDropDownList.Items.FindByValue(objWorkFlowBO.ApprovalID.ToString()) != null)
                        ApproverLevelDropDownList.Items.FindByValue(objWorkFlowBO.ApprovalID.ToString()).Selected = true;
                    getLevel();
                    TriggerDropDownList.ClearSelection();
                    if (TriggerDropDownList.Items.FindByValue(objWorkFlowBO.Trigger.ToString()) != null)
                        TriggerDropDownList.Items.FindByValue(objWorkFlowBO.Trigger.ToString()).Selected = true;

                    AfterDropDownList.ClearSelection();
                    if(AfterDropDownList.Items.FindByValue(objWorkFlowBO.AfterDays.ToString()) != null)
                        AfterDropDownList.Items.FindByValue(objWorkFlowBO.AfterDays.ToString()).Selected = true;
                    if (HightAuthDropDownList.SelectedIndex > 0)
                    {
                        TriggerDropDownList.Enabled = true;
                        AfterDropDownList.Enabled = true;
                    }
                    else
                    {
                        TriggerDropDownList.Enabled = false;
                        AfterDropDownList.Enabled = false;
                    }
                        WORKFLOWDEFINITIONIDTextBox.Text = objWorkFlowBO.WorkFlowDefID.ToString();
                        SaveButton.Text = "Update";
                        ClearButton.Text = "Cancel";

                }
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                //Delete the record using WorkflowDefID
                string message = string.Empty;

                WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();
                message = objWorkFlowBLL.DeleteWorkFlowDef(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGridWorkFlowDefinition();
                clear();
                SetUpdateMode(false);
            }
            else if (e.CommandName == "ClickAddApproved")
            {
               // ViewState["WorkFlowDefID"] = e.CommandArgument;
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Literal WorkflowApproverID = (Literal)row.FindControl("litWorkFlowDefID"); //Literal value avaiable in the grid
                Literal LitModuleName = (Literal)row.FindControl("LitModuleName");
                Literal LitWorkflowName = (Literal)row.FindControl("LitWorkflowName");
               // WorkFlowDefIDTextBox.Text = WorkflowApproverID.Text;
                ViewState["WorkFlowDefID"] = WorkflowApproverID.Text; //view state value assigned by the help of Literal
                WorkFlowDefID(WorkflowApproverID.Text);
                ApprovalTable.Visible = true; //Approver Screen Visable
                ApproverTab.InnerText = "Approvers For " + LitModuleName.Text + "-" + LitWorkflowName.Text;
                #region For load data to Dropen down approval
                BindGrid(true, true);
                if (ViewState["StatusMode"].ToString() != "ReadOnly")
                {
                    ADDButton.Visible = true;
                    UpDateButton.Visible = false;
                    CancelButton.Visible = false;
                }
                WorkApproverIDTextBox.Text = "0";
                WorkDefinationTextBox.Text = "0"; 
                getAllRole();
                getLevel();
                ListItem lstItem = ApproverNameDropDownList.Items[0];
                ApproverNameDropDownList.Items.Clear();
                ApproverNameDropDownList.Items.Insert(0, lstItem);
                ApproverLevelDropDownList.Enabled = true;
                #endregion

                BindGrid(true, true); // Calling Approval Bind Grid Data
               
            }
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                SaveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                SaveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["BudgetEstimationID"] = "0";
            }
        }
        /// <summary>
        /// calls getWorkfolwitemByModuleID method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModuleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            getWorkfolwitemByModuleID();
        }
        protected void HightAuthDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HightAuthDropDownList.SelectedIndex > 0)
            {
                TriggerDropDownList.Enabled = true;
                AfterDropDownList.Enabled = true;
            }
            else
            {
                TriggerDropDownList.SelectedIndex = 0;
                AfterDropDownList.SelectedIndex = 0;
                TriggerDropDownList.Enabled = false;
                AfterDropDownList.Enabled = false;
            }
        }
     
        /// <summary>
        /// if AfterDays is Zero(0) grid colum AfterDays is made clear 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdWorkflowItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litAfterDays = (Literal)e.Row.FindControl("litAfterDays");
                litAfterDays.Text = DataBinder.Eval(e.Row.DataItem, "AfterDays").ToString();
                if (litAfterDays.Text == "0") litAfterDays.Text = "";
            }
        }
        /// <summary>
        /// to chanage page in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdWorkflowChangePage(object sender, GridViewPageEventArgs e)
        {
            grdWorkflowItem.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGridWorkFlowDefinition();
        }

        #endregion

        #endregion

        #region Approval Table

        #region Bind grid data with the help of WorkflowDefID
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            string projectID = Session["PROJECT_ID"].ToString();
            string WorkflowDefID = ViewState["WorkFlowDefID"].ToString();
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            grdApprover.DataSource = WorkFlowBLLobj.GetApprover(projectID, WorkflowDefID);
            grdApprover.DataBind();
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdApprover.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
        #endregion
        /// <summary>
        /// to fetch WorkfolowDEFID
        /// </summary>
        /// <param name="WorkfolowDEFID"></param>
        public void WorkFlowDefID(string WorkfolowDEFID)
        {
            int WorkFlowDefID_ = Convert.ToInt32(WorkfolowDEFID);
        }

        #region Approver Save/ Edit / Update
        /// <summary>
        /// To save or edit details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ADDButton_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

           
            WorkFlowBLL WorkFlowBLLOBJ = new WorkFlowBLL();
            WorkFlowList WorkFlowList = new WorkFlowList();
            int level = 0;
            int count;
            int sameLavel = 0;
            string projectID = Session["PROJECT_ID"].ToString();
            string testWorkflowDefID = ViewState["WorkFlowDefID"].ToString();
            string WorkflowDefID = ViewState["WorkFlowDefID"].ToString();

            WorkFlowList = WorkFlowBLLOBJ.GetApprover(projectID, WorkflowDefID);
            if (WorkFlowList.Count == 0)
            {
                level = level + 1;
                if (level == (Convert.ToInt32(ApproverLevelDropDownList.SelectedItem.Value.ToString())))
                {
                    sameLavel = 0;
                }
                else
                {
                    sameLavel = 1;
                }
            }
            else
            {
                for (int i = 0; i < WorkFlowList.Count; i++)
                {
                    if (WorkApproverIDTextBox.Text != "0")
                    {
                        level = (WorkFlowList[i].LEVEL) ;
                        if (level == (Convert.ToInt32(ApproverLevelDropDownList.SelectedItem.Value.ToString())))
                        {
                            sameLavel = 0;
                        }
                        else
                        {
                            sameLavel = 1;
                            break;
                        }
                    }
                    else
                    {
                        level = (WorkFlowList[i].LEVEL) + 1;
                        if (level == (Convert.ToInt32(ApproverLevelDropDownList.SelectedItem.Value.ToString())))
                        {
                            sameLavel = 0;
                        }
                        else
                        {
                            sameLavel = 1;
                        }
                    }
                }
            }

                if (WorkApproverIDTextBox.Text != "0")
                {
                    #region for update
                    if (sameLavel == 0)
                    {
                        string uID = Session["USER_ID"].ToString();
                        string pID = Session["PROJECT_ID"].ToString();
                        WorkFlowBO objWorkFlow = new WorkFlowBO();

                        objWorkFlow.ApprovalID = Convert.ToInt32(ApproverRoleNameDropDownList.SelectedItem.Value.ToString());
                        objWorkFlow.ApproverUserID = Convert.ToInt32(ApproverNameDropDownList.SelectedItem.Value.ToString());
                        objWorkFlow.LEVEL = Convert.ToInt32(ApproverLevelDropDownList.SelectedItem.Value.ToString());
                        objWorkFlow.UserID = Convert.ToInt32(uID);
                        objWorkFlow.ProjectID = Convert.ToInt32(pID);
                        objWorkFlow.WorkFlowDefID = Convert.ToInt32(WorkDefinationTextBox.Text.ToString());
                        objWorkFlow.WorkApprovalID = Convert.ToInt32(ViewState["WorkApprovalID"]);
                        
                        count = WorkFlowBLLOBJ.EditAPPROVALADD(objWorkFlow);
                        if (count == 1)
                        {
                            message = "Approver or Level Already Exists";
                            AlertMessage = "alert('" + message + "');";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                            BindGrid(true, true);
                            ADDButton.Visible = true;
                            UpDateButton.Visible = false;
                            CancelButton.Visible = false;
                            WorkApproverIDTextBox.Text = "0";
                            WorkDefinationTextBox.Text = "0";
                            ListItem lstItem = ApproverNameDropDownList.Items[0];
                            ApproverNameDropDownList.Items.Clear();
                            ApproverNameDropDownList.Items.Insert(0, lstItem);
                            ApproverLevelDropDownList.Enabled = true;
                        }
                        else if (count == -1)
                        {
                            message = "Approver updated successfully";
                            AlertMessage = "alert('" + message + "');";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                            BindGrid(true, true);
                            ADDButton.Visible = true;
                            UpDateButton.Visible = false;
                            CancelButton.Visible = false;
                            WorkApproverIDTextBox.Text = "0";
                            WorkDefinationTextBox.Text = "0";
                            ListItem lstItem = ApproverNameDropDownList.Items[0];
                            ApproverNameDropDownList.Items.Clear();
                            ApproverNameDropDownList.Items.Insert(0, lstItem);
                            ApproverLevelDropDownList.Enabled = true;
                        }
                    }
                    else
                    {
                        message = "Approver or Level Already Exists";
                        AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                        BindGrid(true, true);
                        ADDButton.Visible = true;
                        UpDateButton.Visible = false;
                        CancelButton.Visible = false;
                        WorkApproverIDTextBox.Text = "0";
                        WorkDefinationTextBox.Text = "0";
                        ListItem lstItem = ApproverNameDropDownList.Items[0];
                        ApproverNameDropDownList.Items.Clear();
                        ApproverNameDropDownList.Items.Insert(0, lstItem);
                        ApproverLevelDropDownList.Enabled = true;
                        //getAllRole();
                    }
                    getAllRole();
                    getLevel();
                    #endregion
                }
                else
                {
                    #region For Save
                    try
                    {
                        if (sameLavel == 0)
                        {
                            

                            string uID = Session["USER_ID"].ToString();
                            string pID = Session["PROJECT_ID"].ToString();
                            WorkFlowBO objWorkFlow = new WorkFlowBO();

                            objWorkFlow.ApprovalID = Convert.ToInt32(ApproverRoleNameDropDownList.SelectedItem.Value.ToString());
                            objWorkFlow.ApproverUserID = Convert.ToInt32(ApproverNameDropDownList.SelectedItem.Value.ToString());
                            objWorkFlow.LEVEL = Convert.ToInt32(ApproverLevelDropDownList.SelectedItem.Value.ToString());
                            objWorkFlow.UserID = Convert.ToInt32(uID);
                            objWorkFlow.ProjectID = Convert.ToInt32(pID);
                            objWorkFlow.WorkFlowDefID = Convert.ToInt32(ViewState["WorkFlowDefID"]);

                            count = WorkFlowBLLOBJ.InsertAPPROVALADD(objWorkFlow);
                            if (count == 1)
                            {
                                message = "Approver or Level Already Exists";
                                AlertMessage = "alert('" + message + "');";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                                BindGrid(true, true);
                                getAllRole();
                                getLevel();
                                ListItem lstItem = ApproverNameDropDownList.Items[0];
                                ApproverNameDropDownList.Items.Clear();
                                ApproverNameDropDownList.Items.Insert(0, lstItem);
                            }
                            else if (count == -1)
                            {
                                message = "Approver Added successfully";
                                AlertMessage = "alert('" + message + "');";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                                BindGrid(true, true);
                                getAllRole();
                                getLevel();
                                ListItem lstItem = ApproverNameDropDownList.Items[0];
                                ApproverNameDropDownList.Items.Clear();
                                ApproverNameDropDownList.Items.Insert(0, lstItem);
                            }
                        }
                        else
                        {
                            message = " Approvers are Not Defined for Previous Level(s) ";
                            AlertMessage = "alert('" + message + "');";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                            BindGrid(true, true);
                            getAllRole();
                            getLevel();
                            ListItem lstItem = ApproverNameDropDownList.Items[0];
                            ApproverNameDropDownList.Items.Clear();
                            ApproverNameDropDownList.Items.Insert(0, lstItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    finally
                    {
                        WorkFlowBLLOBJ = null;
                    }
                    #endregion
                }
        }
        #endregion

        #region grid Edit / Delete the record in Approval table
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdApprover_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["WorkApprovalID"] = e.CommandArgument;
                ADDButton.Visible = false;
                UpDateButton.Visible = true;
                CancelButton.Visible = true;           
                                
                GetApprover();
             ;
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["WorkApprovalID"] = e.CommandArgument;

                int level = 0;
                int sameLavel = 0;

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal litWorkApprovallevel = (Literal)row.FindControl("litWorkApprovallevel");

                WorkFlowBLL WorkFlowBLLOBJ = new WorkFlowBLL();
                WorkFlowList WorkFlowList = new WorkFlowList();
                
                string projectID = Session["PROJECT_ID"].ToString();
                string WorkflowDefID = ViewState["WorkFlowDefID"].ToString();

                WorkFlowList = WorkFlowBLLOBJ.GetApprover(projectID, WorkflowDefID);

                for (int i = 0; i < WorkFlowList.Count; i++)
                {
                    level = (WorkFlowList[i].LEVEL);
                    if (level == (Convert.ToInt32(litWorkApprovallevel.Text.ToString())))
                    {
                        sameLavel = 0;
                    }
                    else
                    {
                        sameLavel = 1;
                    }
                }

                WorkFlowList = WorkFlowBLLOBJ.GetApprover(projectID, WorkflowDefID);

                if(sameLavel == 0)
                {
                if (Convert.ToInt32(ViewState["WorkApprovalID"]) != 0)
                {
                    int result = 0;
                    
                    
                    result = DeleteApprover();
                    if (result == 0)
                    {
                        string message = "Approver Already in use";
                        string AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    }
                    else if (result == -1)
                    {
                        
                        string message = "Data Deleted successfully";
                        string AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    }

                    BindGrid(false, true);
                    ADDButton.Visible = true;
                    UpDateButton.Visible = false;
                    CancelButton.Visible = false;
                    getAllRole();
                    getLevel();
                }
                }
                else
                {
                     string message = "Delete Higher Level Approver First";
                     string AlertMessage = "alert('" + message + "');";
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                }
               
            }
        }
        /// <summary>
        /// method to softdelete approver
        /// </summary>
        /// <returns></returns>
        private int DeleteApprover()
        {
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            int WORKFLOWDEFID = 0;
            int Result = 0;
            if (ViewState["WorkApprovalID"] != null)
                WORKFLOWDEFID = Convert.ToInt32(ViewState["WorkApprovalID"].ToString());
            if (WORKFLOWDEFID != 0)
            {               
                Result = WorkFlowBLLobj.DeleteApprover(WORKFLOWDEFID);
                ViewState["WorkApprovalID"] = null;
            }
            return Result;
        }
      
        /// <summary>
        /// method to fetch values from grid and assign to textbox 
        /// </summary>
        private void GetApprover()
        {
            WorkFlowBLL WorkFlowBLLobj = new WorkFlowBLL();
            int WORKAPPROVALID = 0;

            if (ViewState["WorkApprovalID"] != null)
                WORKAPPROVALID = Convert.ToInt32(ViewState["WorkApprovalID"]);

            if (WORKAPPROVALID != 0)
            {
                WorkFlowBO WorkFlowObj = new WorkFlowBO();
                WorkFlowObj = WorkFlowBLLobj.GetWorkFlowById(WORKAPPROVALID);

                ApproverRoleNameDropDownList.ClearSelection();
                if (ApproverRoleNameDropDownList.Items.FindByValue(WorkFlowObj.RoleID.ToString()) != null)
                    ApproverRoleNameDropDownList.Items.FindByValue(WorkFlowObj.RoleID.ToString()).Selected = true;

                int RoleID = WorkFlowObj.RoleID;
                getUserByRoleID(RoleID);

                ApproverNameDropDownList.ClearSelection();
                if (ApproverNameDropDownList.Items.FindByValue(WorkFlowObj.UserID.ToString()) != null)
                    ApproverNameDropDownList.Items.FindByValue(WorkFlowObj.UserID.ToString()).Selected = true;
                
                ApproverLevelDropDownList.ClearSelection();
                if (ApproverLevelDropDownList.Items.FindByValue(WorkFlowObj.LEVEL.ToString()) != null)
                    ApproverLevelDropDownList.Items.FindByValue(WorkFlowObj.LEVEL.ToString()).Selected = true;
                ApproverLevelDropDownList.Enabled = false;

                WorkApproverIDTextBox.Text = WorkFlowObj.WorkApprovalID.ToString();
                WorkDefinationTextBox.Text = WorkFlowObj.WorkFlowDefID.ToString();
            }
        }
        /// <summary>
        /// to get values in dropdownlist based on index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApproverRoleNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roleID = Convert.ToInt32(ApproverRoleNameDropDownList.SelectedItem.Value.ToString());
            getUserByRoleID(roleID);
        }

        #endregion


        #region Cancel / clear / update Button
        /// <summary>
        /// used to clear data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ADDButton.Visible = true;
            UpDateButton.Visible = false;
            CancelButton.Visible = false;
            getAllRole();
            getLevel();
            getUserByRoleID(0);
        }
        /// <summary>
        /// calls clear method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// used to clear details entered and set values to default
        /// </summary>
        public void clear()
        {
            ModuleDropDownList.Items.Clear();
            WORKFLOWDEFINITIONIDTextBox.Text = "0";
            screenintiallization();
            SaveButton.Text = "Save";
            ClearButton.Text = "Clear";
            TriggerDropDownList.Enabled = false;
            AfterDropDownList.Enabled = false;
          
        }
        #endregion

       
        #endregion
    }
}