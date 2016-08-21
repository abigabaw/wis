using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using System.Text;

namespace WIS
{
    public partial class Grievances1 : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,call  Getcategory() to get category
        /// call  Getresolby() to get result by id
        /// call Getactionby() to get action 
        /// call   BindGrid() to bind the data to the grid
        /// Set attributes to link buttons lnkGrievances,lnkUploadDoc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            calactionDatePicker.Format = UtilBO.DateFormat;
            calresolutionDatePicker.Format = UtilBO.DateFormat;
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
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS", CreateStartupScript());
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["PROJECT_ID"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }

            if (!IsPostBack)
            {
                descriptionTextBox.Attributes.Add("maxlength", descriptionTextBox.MaxLength.ToString());
                actionTextBox.Attributes.Add("maxlength", actionTextBox.MaxLength.ToString());
                ViewState["GRIEVANCEID"] = 0;
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Grievances";
                int hhid = Convert.ToInt32(Session["HH_ID"]);
                int userID = Convert.ToInt32(Session["USER_ID"]);

                ResolutionStatusChange.Visible = false;
                screenIntialization(hhid);

                Getcategory();
                Getresolby();
                Getactionby();
                BindGrid();
                lnkGrievances.Visible = false;
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_GRIEVANCES) == false)
                {
                    lnkGrievances.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    lnkUploadDoc.Visible = false;
                    grdgrvlist.Columns[grdgrvlist.Columns.Count - 1].Visible = false;
                    grdgrvlist.Columns[grdgrvlist.Columns.Count - 2].Visible = false;
                    grdgrvlist.Columns[grdgrvlist.Columns.Count - 3].Visible = false;
                }
            }

          
          
           // checkGrievanceApprovalExitOrNot();
            //projectFrozen();
            if (Mode == "Readonly")
            {
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkGrievances.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                lnkUploadDoc.Visible = false;
                grdgrvlist.Columns[4].Visible = false;
                grdgrvlist.Columns[5].Visible = false;
            }
        }
        
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region Frozen / Approval / Decline / Pending
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string Frozen = Session["FROZEN"].ToString();
                if (Frozen == "Y")
                {
                    EnableChangeRequest(true);
                    //checkApprovalExitOrNot();
                }
                else
                {
                    EnableChangeRequest(false);
                }
            }
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusGrievances.Text = "";
            StatusGrievances.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "Griev");
                lnkGrievances.Attributes.Add("onclick", paramChangeRequest);
                lnkGrievances.Visible = true;
            }
            else
            {
                lnkGrievances.Visible = false;
            }
            #endregion
            getApprrequtStatusGrievances();

        }

        private void EnableChangeRequest(bool status)
        {
            if (status)
            {
                lnkGrievances.Visible = false;
                btnSave.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                lnkGrievances.Visible = false;
                btnSave.Visible = true;
                btnClear.Visible = true;
            }
        }

        public void ChangeRequestStatusGrievances()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "Griev";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusGrievances()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "Griev";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkGrievances.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusGrievances.Visible = true;
                    StatusGrievances.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkGrievances.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusGrievances.Visible = false;
                    StatusGrievances.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkGrievances.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusGrievances.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
       /// <summary>
        /// get category data from database
       /// </summary>
 
        private void Getcategory()
        {
            GrievancesBLL GrievancesBLL = new GrievancesBLL();
            categoryDropDownList.DataSource = GrievancesBLL.Getcategory();
            categoryDropDownList.DataTextField = "GrievCategory";
            categoryDropDownList.DataValueField = "GrievCategoryID";
            categoryDropDownList.DataBind();
        }
        /// <summary>
        /// get results data from database by id
        /// </summary>
        private void Getresolby()
        {
            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();
            ProjectPersonalList ProjectPersonnel = objProjPersonalLogic.GetProjectPersonnel(Convert.ToInt32(Session["PROJECT_ID"]));

            resolDropDownList.DataSource = ProjectPersonnel;
            resolDropDownList.DataTextField = "UserName";
            resolDropDownList.DataValueField = "UserID";
            resolDropDownList.DataBind();
        }
        /// <summary>
        /// get actionby data from database
        /// </summary>
        private void Getactionby()
        {
            //UserBLL objUserBLL = new UserBLL();
            //UserList objUserList = new UserList();
            //UserBO oBOUser = null;
            //oBOUser = new UserBO();
            //oBOUser.UserName = string.Empty;
            //oBOUser.UserID = 0;
            //oBOUser.RoleID = 0;
            //objUserList = objUserBLL.GetUsers(oBOUser);

            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();
            ProjectPersonalList ProjectPersonnel = objProjPersonalLogic.GetProjectPersonnel(Convert.ToInt32(Session["PROJECT_ID"]));

            actionDropDownList.DataSource = ProjectPersonnel;
            actionDropDownList.DataTextField = "UserName";
            actionDropDownList.DataValueField = "UserID";
            actionDropDownList.DataBind();
            actionDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
            actionDropDownList.SelectedIndex = 0;
        }
        /// <summary>
        /// get the data from database and bind it to grid
        /// </summary>
        private void BindGrid()
        {
            GrievancesBLL objGrievancesBLL = new GrievancesBLL();
            grdgrvlist.DataSource = objGrievancesBLL.Getgrievancedata(Convert.ToInt32(Session["HH_ID"]));
            grdgrvlist.DataBind();
        }
        /// <summary>
        /// to assign the data from database to the textbox's districtTextBox, countyTextBox
        /// subcountyTextBox,parishTextBox,villageTextBox
        /// </summary>
        /// <param name="hhid"></param>
        private void screenIntialization(int hhid)
        {
            GrievancesBLL objGrievancesBLL = new GrievancesBLL();
            GrievancesBO GrievancesBOobj = new GrievancesBO();
            GrievancesBOobj = objGrievancesBLL.getscreenIntialization(hhid);

            districtTextBox.Text = GrievancesBOobj.District;
            countyTextBox.Text = GrievancesBOobj.County;
            subcountyTextBox.Text = GrievancesBOobj.SubCounty;
            parishTextBox.Text = GrievancesBOobj.Parish;
            villageTextBox.Text = GrievancesBOobj.Village;
        }

        /// <summary>
        /// to update and delete the grievances 
        /// to call SetUpdateMode() method,and 
        /// to clear the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grievances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["GRIEVANCEID"] = e.CommandArgument;
                GetGrievancedatarow();
                SetUpdateMode(true);
                ResolutionStatusChange.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                int Result = 0;

                int GrievanceID = Convert.ToInt32(e.CommandArgument);
                GrievancesBLL objGrievancesBLL = new GrievancesBLL();
                Result = objGrievancesBLL.Delete(GrievanceID);
                if (Result == -1)
                    BindGrid();
                ResolutionStatusChange.Visible = false;
                SetUpdateMode(false);
                ClearDetails();
            }
        }
/// <summary>
/// to get the grievance  data row by row
/// </summary>
        private void GetGrievancedatarow()
        {
            GrievancesBLL objGrievancesBLL = new GrievancesBLL();
            int GrievanceID = 0;

            if (ViewState["GRIEVANCEID"] != null)
                GrievanceID = Convert.ToInt32(ViewState["GRIEVANCEID"]);
            //For Upload Documents Added by Anjankumar
            lnkUploadDoc.Attributes.Clear();
            lnkViewUploadDoc.Attributes.Clear();
            int hhid = Convert.ToInt32(Session["HH_ID"]);
            int userID = Convert.ToInt32(Session["USER_ID"]);
            string paramUploadDoc = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}','{5}');", Convert.ToInt32(Session["PROJECT_ID"]),
                hhid, userID, Session["PROJECT_CODE"].ToString(), "GRV", GrievanceID);
            string paramViewDocs = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}','{5}');", Convert.ToInt32(Session["PROJECT_ID"]),
                    hhid, userID, Session["PROJECT_CODE"].ToString(), "GRV", GrievanceID);

            lnkUploadDoc.Attributes.Add("onclick", paramUploadDoc);
            lnkViewUploadDoc.Attributes.Add("onclick", paramViewDocs);

            // End Documnts
            GrievancesBO GrievancesBOobj = new GrievancesBO();
            GrievancesBOobj = objGrievancesBLL.GetGrievancedatarow(GrievanceID);

            categoryDropDownList.ClearSelection();
            if (categoryDropDownList.Items.FindByValue(GrievancesBOobj.GrievCategoryID.ToString()) != null)
                categoryDropDownList.Items.FindByValue(GrievancesBOobj.GrievCategoryID.ToString()).Selected = true;

            //Getactionby();
            actionDropDownList.ClearSelection();
            if (actionDropDownList.Items.FindByValue(GrievancesBOobj.ActionTakenBy.ToString()) != null)
                actionDropDownList.Items.FindByValue(GrievancesBOobj.ActionTakenBy.ToString()).Selected = true;

            //Getresolby();
            resolDropDownList.ClearSelection();
            if (resolDropDownList.Items.FindByValue(GrievancesBOobj.ResolvedBy.ToString()) != null)
                resolDropDownList.Items.FindByValue(GrievancesBOobj.ResolvedBy.ToString()).Selected = true;

            descriptionTextBox.Text = GrievancesBOobj.Description.ToString();
            actionTextBox.Text = GrievancesBOobj.ActionTaken.ToString();

            if (GrievancesBOobj.ActionTakenDate != DateTime.MinValue)
                actionDatePicker.Text = GrievancesBOobj.ActionTakenDate.ToString(UtilBO.DateFormat);

            basicTextBox.Text = GrievancesBOobj.BasicFacts.ToString();
            resolutionTextBox.Text = GrievancesBOobj.Resolution.ToString();

            if (GrievancesBOobj.ResolutionDate != DateTime.MinValue)
                resolutionDatePicker.Text = GrievancesBOobj.ResolutionDate.ToString(UtilBO.DateFormat);

        }
        /// <summary>
        /// to check the grievance data Exit Or Not
        /// to enable the change request button 
        /// </summary>
        public void checkGrievanceApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button

            string ResultValue = string.Empty;
            string message = string.Empty;

            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteList objProjectRouteList = new ProjectRouteList();

            objProjectRoute.WorkFlowApprover = UtilBO.WorkflowGrievances;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);

            objProjectRoute = objProjectRouteBLL.getWOrkFlowApprovalID(objProjectRoute);

            if ((objProjectRoute) != null)
            {

                btnSave.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
            #endregion
        }
        /// <summary>
        /// to Save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            string descriptionText = "";
            string message = "";
            GrievancesBLL objGrievancesBLL = new GrievancesBLL();
            GrievancesBO GrievancesBOobj = null;

            if (Convert.ToInt32(ViewState["GRIEVANCEID"]) > 0)
            {
                try
                {
                    string hhid = Session["HH_ID"].ToString();

                    GrievancesBOobj = new GrievancesBO();

                    GrievancesBOobj.GrievanceID = Convert.ToInt32(ViewState["GRIEVANCEID"]);
                    GrievancesBOobj.GrievCategoryID = Convert.ToInt32(categoryDropDownList.SelectedValue);

                    descriptionText = descriptionTextBox.Text.Trim();
                    if (descriptionText.Length >= 1000)
                    {
                        descriptionText = descriptionText.Substring(0, 1000);
                    }

                    GrievancesBOobj.Description = descriptionText;

                    descriptionText = actionTextBox.Text.Trim();
                    if (descriptionText.Length >= 500)
                    {
                        descriptionText = descriptionText.Substring(0, 500);
                    }

                    GrievancesBOobj.ActionTaken = descriptionText;

                    if (actionDatePicker.Text.Trim() != "")
                        GrievancesBOobj.ActionTakenDate = Convert.ToDateTime(actionDatePicker.Text.ToString());

                    GrievancesBOobj.ActionTakenBy = Convert.ToInt32(actionDropDownList.SelectedValue.ToString().Trim());

                    descriptionText = basicTextBox.Text.Trim();
                    if (descriptionText.Length >= 500)
                    {
                        descriptionText = descriptionText.Substring(0, 500);
                    }

                    GrievancesBOobj.BasicFacts = descriptionText;

                    descriptionText = resolutionTextBox.Text.Trim();
                    if (descriptionText.Length >= 500)
                    {
                        descriptionText = descriptionText.Substring(0, 500);
                    }

                    GrievancesBOobj.Resolution = descriptionText;

                    if (resolutionDatePicker.Text.Trim() != "")
                        GrievancesBOobj.ResolutionDate = Convert.ToDateTime(resolutionDatePicker.Text.ToString());
                    
                    GrievancesBOobj.ResolvedBy = Convert.ToInt32(resolDropDownList.SelectedValue.ToString().Trim());
                    GrievancesBOobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                    GrievancesBLL objGrievancBLL = new GrievancesBLL();

                    if ((resolDropDownList.SelectedItem.Value.ToString()) == "0")
                    {
                        GrievancesBOobj.ResolutionStatus = "P";
                        count = objGrievancBLL.EditGRIEVANCE(GrievancesBOobj);
                        message = "Data updated successfully";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('" + message + "');", true);
                    }
                    else
                    {
                        bool approverExists = CheckApprover(); //Send email for Approval if Approver Exists

                        if (approverExists)
                        {
                            GrievancesBOobj.ResolutionStatus = "S";
                            count = objGrievancBLL.EditGRIEVANCE(GrievancesBOobj);
                            message = "Data updated successfully";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('" + message + "');", true);

                            string scriptContent = string.Format("SendApprovalEmail('{0}',{1},{2},{3},'{4}',{5});",
                                UtilBO.WorkflowGrievances,
                                Convert.ToInt32(Session["PROJECT_ID"]),
                                Convert.ToInt32(Session["USER_ID"]),
                                Convert.ToInt32(Session["HH_ID"]),
                                UtilBO.WorkflowGrievances,
                                Convert.ToInt32(ViewState["GRIEVANCEID"])
                                );
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Updated2", scriptContent, true);
                            
                        }
                        else
                        {
                            message = "You are trying to resolve the status, but the Approver for Grievance Resolution is not defined.";
                        }
                    }

                    ClearDetails();
                    SetUpdateMode(false);
                    BindGrid();
                    ResolutionStatusChange.Visible = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objGrievancesBLL = null;

                }

            }
            else
            {
                // insert

                try
                {
                    string hhid = Session["HH_ID"].ToString();                    

                    GrievancesBOobj = new GrievancesBO();
                    GrievancesBOobj.GrievanceID = Convert.ToInt32(ViewState["GRIEVANCEID"]);

                    GrievancesBOobj.GrievCategoryID = Convert.ToInt32(categoryDropDownList.SelectedValue);

                    descriptionText = descriptionTextBox.Text.Trim();
                    if (descriptionText.Length >= 1000)
                    {
                        descriptionText = descriptionText.Substring(0, 1000);
                    }

                    GrievancesBOobj.Description = descriptionText;

                    GrievancesBOobj.ActionTaken = actionTextBox.Text.ToString().Trim();

                    if (actionDatePicker.Text.Trim() != "")
                    {
                        GrievancesBOobj.ActionTakenDate = Convert.ToDateTime(actionDatePicker.Text.ToString());
                    }

                    GrievancesBOobj.ActionTakenBy = Convert.ToInt32(actionDropDownList.SelectedValue.ToString().Trim());

                    descriptionText = basicTextBox.Text.Trim();
                    if (descriptionText.Length >= 500)
                    {
                        descriptionText = descriptionText.Substring(0, 500);
                    }

                    GrievancesBOobj.BasicFacts = descriptionText;

                    descriptionText = resolutionTextBox.Text.Trim();
                    if (descriptionText.Length >= 500)
                    {
                        descriptionText = descriptionText.Substring(0, 500);
                    }

                    GrievancesBOobj.Resolution = descriptionText;

                   
                    if (resolutionDatePicker.Text.Trim() != "")
                    {
                        GrievancesBOobj.ResolutionDate = Convert.ToDateTime(resolutionDatePicker.Text.ToString());
                    }                  
           
                    GrievancesBOobj.ResolvedBy = Convert.ToInt32(resolDropDownList.SelectedValue.ToString().Trim());
                    if ((resolDropDownList.SelectedItem.Value.ToString()) == "0")
                    {
                        GrievancesBOobj.ResolutionStatus = "P";
                    }
                    else
                    {
                        GrievancesBOobj.ResolutionStatus = "P";
                    }

                    GrievancesBOobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    GrievancesBOobj.Hhid = Convert.ToInt32(hhid);


                    GrievancesBLL GrievancesBLL = new GrievancesBLL();
                    count = GrievancesBLL.Insert(GrievancesBOobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);
                    ClearDetails();
                    SetUpdateMode(false);
                    BindGrid();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objGrievancesBLL = null;
                }
            }
            //projectFrozen();
            //ChangeRequestStatusGrievances();
        }
        /// <summary>
        /// to check the approver
        /// </summary>
        /// <returns></returns>
        public bool CheckApprover()
        {
            string ResultValue = string.Empty;
            string message = string.Empty;
            //bool mailSent = false;
            bool approverExists = false;

            ProjectRouteBO objProjectRoute = new ProjectRouteBO();
            ProjectRouteBLL objProjectRouteBLL = new ProjectRouteBLL();
            ProjectRouteList objProjectRouteList = new ProjectRouteList();

            objProjectRoute.WorkFlowApprover = UtilBO.WorkflowGrievances;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);

            objProjectRoute = objProjectRouteBLL.getWOrkFlowApprovalID(objProjectRoute);

            if (objProjectRoute != null)
            {
                //(new NotificationBLL()).SendEmail(Convert.ToInt32(Session["PROJECT_ID"]), UtilBO.WorkflowGrievances);

                ////NotificationObj.SendEmail(objProjectRoute.EmailID, objProjectRoute.EmailSubject, objProjectRoute.EmailBody, objProjectRoute.ProjectCode, objProjectRoute.ProjectName);
                ////ResultValue = NotificationObj.SendSMS(objProjectRoute.CellNumber, objProjectRoute.SmsText, objProjectRoute.ProjectCode, objProjectRoute.ProjectName);
                //string Description = descriptionTextBox.Text.ToString().Trim();
                //int GrievanceID = Convert.ToInt32(ViewState["GRIEVANCEID"]);

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("Dear Sir,");
                //sb.Append(System.Environment.NewLine);
                //sb.Append(objProjectRoute.EmailBody); sb.Append(GrievanceID);
                //sb.Append(System.Environment.NewLine);
                //sb.Append(Description);
                //sb.Append(System.Environment.NewLine);
                //sb.Append("Project Code : " + objProjectRoute.ProjectCode);
                //sb.Append(System.Environment.NewLine);
                //sb.Append("Project Name : " + objProjectRoute.ProjectName);
                //sb.Append(System.Environment.NewLine);
                //sb.Append(" Thanks and Regards");
                //sb.Append(System.Environment.NewLine);
                //sb.Append("WIS - UETCL Team");
                //string InputEmail = sb.ToString();

                //ProjectRouteBO objApprovalHeaderSave = new ProjectRouteBO();
                //objApprovalHeaderSave.WorkFlowApproverID = objProjectRoute.WorkFlowApproverID;
                //objApprovalHeaderSave.StatusID = objProjectRoute.StatusID;
                //objApprovalHeaderSave.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                //objApprovalHeaderSave.PageCode = UtilBO.WorkflowGrievances;
                //objApprovalHeaderSave.HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                //objApprovalHeaderSave.ElementID = Convert.ToInt32(ViewState["GRIEVANCEID"]); //sending Grivanceid
                //objApprovalHeaderSave.ApproverUserID = objProjectRoute.ApproverUserID;
                //objApprovalHeaderSave.WorkFlowDefinitionID = objProjectRoute.WorkFlowDefinitionID;
                //objApprovalHeaderSave.EmailSubject = objProjectRoute.EmailSubject;
                //objApprovalHeaderSave.EmailBody = InputEmail;

                //message = objProjectRouteBLL.AddApprovalTrackheader(objApprovalHeaderSave);
                approverExists = true;
            }

            return approverExists;
        }
        /// <summary>
        /// to clear the grievance details by calling  ClearDetails() method
        /// and to Set attributes to link buttons lnkUploadDoc,lnkViewUploadDoc
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
        /// to set the update mode
        /// </summary>
        /// <param name="updateMode"></param>
        private void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Get Approval";
                btnClear.Text = "Cancel";
                lnkUploadDoc.Visible = true;
                lnkViewUploadDoc.Visible = true;
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                ViewState["GRIEVANCEID"] = "0";
                lnkUploadDoc.Visible = false;
                lnkViewUploadDoc.Visible = false;
            }
        }
        /// <summary>
        /// to clear the all the textbox's and dropdownlist's
        /// </summary>
        private void ClearDetails()
        {
            categoryDropDownList.ClearSelection();
            actionDropDownList.ClearSelection();
            resolDropDownList.ClearSelection();
            descriptionTextBox.Text = string.Empty;
            actionTextBox.Text = string.Empty;
            resolutionTextBox.Text = string.Empty;
            basicTextBox.Text = string.Empty;
            actionDatePicker.Text = "";
            resolutionDatePicker.Text = "";
        }
        /// <summary>
        /// to set the page index's of the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdgrvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdgrvlist.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        ///  Set edit mode for edit comand Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdgrvlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime createdDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "CreatedDate"));
                System.Web.UI.HtmlControls.HtmlAnchor lnkGravience = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkGravience");
                lnkGravience.Visible = false;
                if (createdDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litCreatedDate")).Text = createdDate.ToString(UtilBO.DateFormat);
                Literal ResolutionStatus = (Literal)e.Row.FindControl("litResolutionStatus");
                System.Web.UI.HtmlControls.HtmlAnchor lnkGrievanceClosure = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkGrievanceClosure");
                int grievanceID = (int)DataBinder.Eval(e.Row.DataItem, "GrievanceID");

                if (ResolutionStatus.Text.ToLower() == "closed" || ResolutionStatus.Text.ToLower() == "sent for approval")
                {
                    ImageButton Edit = (ImageButton)e.Row.FindControl("imgEdit");
                    ImageButton Delete = (ImageButton)e.Row.FindControl("imgDelete");
                    Edit.Visible = false;
                    Delete.Visible = false;
                    lnkGravience.Visible = false;

                    if (ResolutionStatus.Text.ToLower() == "closed")
                    {
                        ResolutionStatus.Visible = false;
                        lnkGrievanceClosure.Visible = true;
                        lnkGrievanceClosure.Attributes.Add("onclick", "OpenGravienceClosure('readonly'," + grievanceID + ");");
                    }
                }
                 else if (ResolutionStatus.Text.ToLower() == "resolved")
                {
                    ImageButton Edit = (ImageButton)e.Row.FindControl("imgEdit");
                    ImageButton Delete = (ImageButton)e.Row.FindControl("imgDelete");
                    Edit.Visible = false;
                    Delete.Visible = false;
                    lnkGravience.Visible = true;                    
                    lnkGravience.Attributes.Add("onclick", "OpenGravienceClosure('edit'," + grievanceID + ");");
                }
                else
                {
                    lnkGravience.Visible = false;
                }               
            }            
        }
    }
}