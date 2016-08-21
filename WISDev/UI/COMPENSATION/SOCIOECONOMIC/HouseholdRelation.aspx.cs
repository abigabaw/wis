using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class HouseholdRelation : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,call  GetHouseHoldDtlData() to get the household details from the database
        /// call   BindRelations() to bind the Relations from the database
        /// call  GetPAPStatus() to get the PAPStatus data from the dtaabase
        /// call  BindServiceMaster() to bind the ServiceMaster from the database
        /// call GetPAPServices() to get the PAPService data from the database
        /// call BindAffectedLandUsers() to bind the Affected Land User data from the database
        /// set the status of the link lnkChangeRequest,lnkLogout
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.HouseholdRelations;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.HouseholderDetails;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.ServicesonAffectedPlot;
            ViewMasterCopy3.HighlightMenu = ViewMasterCopy.MenuValue.AffectedLandusersontheAffectedPlotofLand;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
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

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Household Relations";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                
                ViewState["AFFECLANDUSER_ID"] = 0;
                ViewState["ChkMaritalStatus"] = "None";
                GetHouseHoldDtlData();
                BindRelations();
                GetPAPStatus();
                BindServiceMaster();
                GetPAPServices();
                BindAffectedLandUsers();
                txtAffecLandUserName.Attributes.Add("Onchange", "setDirtyText();");

                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    btnSaveLandUser.Visible = false;
                    btnClearLandUser.Visible = false;
                    btnSaveService.Visible = false;
                    btnClearService.Visible = false;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 1].Visible = false;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 2].Visible = false;
                }
            }
            if (Mode == "Readonly")
            {
                CompSocioEconomyMenu1.Visible = false;
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkChangeRequest.Visible = false;
                btnSaveLandUser.Visible = false;
                btnClearLandUser.Visible = false;
                btnSaveService.Visible = false;
                btnClearService.Visible = false;
                grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 1].Visible = false;
                grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 2].Visible = false;
            }
        }
        #region Frozen / Approval / Decline / Pending status
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSaveLandUser.Visible = false;
                    btnSaveService.Visible = false;
                    btnClearLandUser.Visible = false;
                    btnClearService.Visible = false;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 1].Visible = false;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 2].Visible = false;
                    checkApprovalExitOrNot();
                }
            }
        }
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusLandUser.Text = "";
            StatusLandUser.Visible = false; // used to display the Status if you send Request for change data
            StatusService.Text = "";
            StatusService.Visible = false;
           // getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            //for checking Change Request Approver Exists or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-LU");
                lnkChangeRequest.Attributes.Add("onclick", paramChangeRequest);
                string paramChangeRequest1 = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-SA");
                lnkServiceAffected.Attributes.Add("onclick", paramChangeRequest1);
                lnkChangeRequest.Visible = true;
                lnkServiceAffected.Visible = true;
            }
            else
            {
                lnkChangeRequest.Visible = false;
                lnkServiceAffected.Visible = false;
            }
            #endregion
            getApprrequtStatusServes();
            getApprrequtStatusLNDUsers();
        }
        public void ChangeRequestStatusServes()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SA";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusServes()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SA";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkServiceAffected.Visible = false;
                    btnClearService.Visible = false;
                    btnSaveService.Visible = false;
                    StatusService.Visible = true;
                    StatusService.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkServiceAffected.Visible = true;
                    btnClearService.Visible = false;
                    btnSaveService.Visible = false;
                    StatusService.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkServiceAffected.Visible = false;
                    btnClearService.Visible = true;
                    btnSaveService.Visible = true;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 1].Visible = true;
                    grdAffectedLandUsers.Columns[grdAffectedLandUsers.Columns.Count - 2].Visible = true;
                    StatusService.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        public void ChangeRequestStatusLNDUsers()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-LU";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        public void getApprrequtStatusLNDUsers()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-LU";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkChangeRequest.Visible = false;
                    btnClearLandUser.Visible = false;
                    btnSaveLandUser.Visible = false;
                    StatusLandUser.Visible = true;
                    StatusLandUser.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkChangeRequest.Visible = true;
                    btnClearLandUser.Visible = false;
                    btnSaveLandUser.Visible = false;
                    StatusLandUser.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkChangeRequest.Visible = false;
                    btnClearLandUser.Visible = true;
                    btnSaveLandUser.Visible = true;
                    StatusLandUser.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        /// <summary>
        /// to get the HouseHold Data from the database 
        /// </summary>

        private void GetHouseHoldDtlData()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHouseHoldData(householdID);
            if (objHouseHold != null)
            {
                ViewState["ChkMaritalStatus"] = objHouseHold.MaritalStatus;
            }
        }

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSaveService.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// to bind the data of the Relations to gridview from the database
        /// </summary>
        protected void BindRelations()
        {
            grdRelations.DataSource = (new PAP_RelationBLL()).GetHolderTypes(Convert.ToInt32(Session["HH_ID"]), 0);
            grdRelations.DataBind();
        }
        /// <summary>
        /// to get the data of the PAPServices  from the database
        /// </summary>

        protected void GetPAPServices()
        {
            PAPServicesList PAPServices = (new PAP_RelationBLL()).GetPAPServices(Convert.ToInt32(Session["HH_ID"]));

            CheckBox chkServiceType = null;
            TextBox txtServiceType = null;
            int serviceMasterID = 0;
            foreach (DataListItem lstItem in lstServices.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    serviceMasterID = Convert.ToInt32(((Literal)lstItem.FindControl("litServiceID")).Text);
                    chkServiceType = (CheckBox)lstItem.FindControl("chkServiceType");
                    txtServiceType = (TextBox)lstItem.FindControl("txtServiceType");

                    foreach (PAPServiceBO objService in PAPServices)
                    {
                        if (objService.ServiceID == serviceMasterID)
                        {
                            if (txtServiceType.Visible)
                            {
                                txtServiceType.Text = objService.FieldValue;
                            }
                            else if (chkServiceType.Visible && objService.FieldValue == "TRUE")
                            {
                                chkServiceType.Checked = true;
                            }

                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// to bind the data of the ServiceMaster  from the database
        /// </summary>

        private void BindServiceMaster()
        {
            lstServices.DataSource = (new PAP_RelationBLL()).GetPAPServiceMasters();
            lstServices.DataBind();
        }
        /// <summary>
        ///  to get the data of the PAPStatus  from the database
        /// </summary>

        private void GetPAPStatus()
        {
            PstatusBLL objPStatusBLL = new PstatusBLL();
            ddlStatus.DataSource = objPStatusBLL.GetPstatus();
            ddlStatus.DataTextField = "PAPDESIGNATION1";
            ddlStatus.DataValueField = "PAPDESIGNATIONID1";
            ddlStatus.DataBind();
        }
        /// <summary>
        /// to bind the data of the Affected Land Users  from the database
        /// </summary>

        protected void BindAffectedLandUsers()
        {
            grdAffectedLandUsers.DataSource = (new PAP_RelationBLL()).GetAffectedLandUsers(
                Convert.ToInt32(ViewState["AFFECLANDUSER_ID"]),
                Convert.ToInt32(Session["HH_ID"])
            );
            grdAffectedLandUsers.DataBind();
        }
        #region for Save LandUser
        /// <summary>
        /// to save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveLandUser_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            AffectedLandUserBO objAffLandUser = new AffectedLandUserBO();

            objAffLandUser.LandUserName = txtAffecLandUserName.Text.Trim();
            objAffLandUser.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
            objAffLandUser.StatusID = Convert.ToInt32(ddlStatus.SelectedItem.Value);
            objAffLandUser.RelatedTo = txtRelatedTo.Text.Trim();
            objAffLandUser.TimeOnLand = txtTimeOnLand.Text.Trim();

            if (Convert.ToInt32(ViewState["LANDUSER_ID"]) > 0)
            {
                objAffLandUser.LandUserID = Convert.ToInt32(ViewState["LANDUSER_ID"]);
                objAffLandUser.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = (new PAP_RelationBLL()).UpdateAffectedLandUser(objAffLandUser);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }
                SetLandUserUPDMode(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LandUserUpdated", "alert('" + message + "');", true);
            }
            else
            {
                objAffLandUser.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = (new PAP_RelationBLL()).AddAffectedLandUser(objAffLandUser);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LandUserAdded", "alert('" + message + "');", true);
            }
            ClearLandUserDetails();
            BindAffectedLandUsers();
            ChangeRequestStatusLNDUsers();
            projectFrozen();
        }
        /// <summary>
        /// to clear the data fields by calling ClearLandUserDetails() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClearLandUser_Click(object sender, EventArgs e)
        {
            ClearLandUserDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetLandUserUPDMode(false);
            }
        }
        #endregion
        /// <summary>
        /// to set the status of the LandUserUPDMode
        /// </summary>
        /// <param name="updateMode"></param>
        protected void SetLandUserUPDMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSaveLandUser.Text = "Update";
                btnClearLandUser.Text = "Cancel";
            }
            else
            {
                btnSaveLandUser.Text = "Save";
                btnClearLandUser.Text = "Clear";
                ViewState["LANDUSER_ID"] = "0";
            }
        }
        /// <summary>
        /// to clear the Land User Details
        /// </summary>
        protected void ClearLandUserDetails()
        {
            txtAffecLandUserName.Text = "";
            ddlStatus.ClearSelection();
            txtRelatedTo.Text = "";
            txtTimeOnLand.Text = "";
        }
        /// <summary>
        /// to change the index of the page in the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdAffectedLandUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAffectedLandUsers.PageIndex = e.NewPageIndex;
            BindAffectedLandUsers();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdAffectedLandUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["LANDUSER_ID"] = e.CommandArgument;

                PAP_RelationBLL objAffLandUserBLL = new PAP_RelationBLL();
                PAP_AffectedLandUserList AffecLandUserList = objAffLandUserBLL.GetAffectedLandUsers(Convert.ToInt32(ViewState["LANDUSER_ID"]), Convert.ToInt32(Session["HH_ID"]));

                AffectedLandUserBO objAffLandUser = AffecLandUserList[0];

                txtAffecLandUserName.Text = objAffLandUser.LandUserName;
                ddlStatus.ClearSelection();
                ddlStatus.Items.FindByValue(objAffLandUser.StatusID.ToString()).Selected = true;
                txtRelatedTo.Text = objAffLandUser.RelatedTo;
                txtTimeOnLand.Text = objAffLandUser.TimeOnLand;
                SetLandUserUPDMode(true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                ViewState["LANDUSER_ID"] = e.CommandArgument;
                PAP_RelationBLL objAffLandUserBLL = new PAP_RelationBLL();
                objAffLandUserBLL.DeleteAffectedLandUser(Convert.ToInt32(ViewState["LANDUSER_ID"]), Convert.ToInt32(Session["USER_ID"]));

                ClearLandUserDetails();
                SetLandUserUPDMode(false);
                BindAffectedLandUsers();
            }
        }

        protected void lstServices_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string fieldType = DataBinder.Eval(e.Item.DataItem, "FIELDTYPE").ToString();

                if (fieldType.ToUpper() == "TEXTBOX")
                {
                    ((CheckBox)e.Item.FindControl("chkServiceType")).Visible = false;
                    ((TextBox)e.Item.FindControl("txtServiceType")).Visible = true;
                    ((Label)e.Item.FindControl("lblServiceName")).Width = Unit.Pixel(100);
                }
            }
        }
        #region for Save Service 
        /// <summary>
        /// To Save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveService_Click(object sender, EventArgs e)
        {
            PAPServicesList PAPServices = new PAPServicesList();
            PAPServiceBO objPAPService = null;
            CheckBox chkServiceType = null;
            TextBox txtServiceType = null;

            foreach (DataListItem lstItem in lstServices.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    chkServiceType = (CheckBox)lstItem.FindControl("chkServiceType");
                    txtServiceType = (TextBox)lstItem.FindControl("txtServiceType");

                    objPAPService = new PAPServiceBO();
                    objPAPService.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                    objPAPService.ServiceID = Convert.ToInt32(((Literal)lstItem.FindControl("litServiceID")).Text);

                    if (txtServiceType.Visible)
                    {
                        objPAPService.FieldValue = txtServiceType.Text;
                    }
                    else if (chkServiceType.Visible)
                    {
                        if (chkServiceType.Checked)
                            objPAPService.FieldValue = "TRUE";
                        else
                            objPAPService.FieldValue = "FALSE";
                    }
                    else
                    {
                        objPAPService.FieldValue = "";
                    }

                    objPAPService.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                    PAPServices.Add(objPAPService);
                }
            }

            (new PAP_RelationBLL()).AddPAPService(PAPServices);
            ChangeRequestStatusServes();
            projectFrozen();//if project Frozen
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ServiceAdded", "alert('Data saved successfully');", true);
        }
        /// <summary>
        /// to clear the service fields by 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClearService_Click(object sender, EventArgs e)
        {
            foreach (DataListItem lstItem in lstServices.Items)
            {
                if (lstItem.ItemType == ListItemType.Item || lstItem.ItemType == ListItemType.AlternatingItem)
                {
                    ((CheckBox)lstItem.FindControl("chkServiceType")).Checked = false;
                    ((TextBox)lstItem.FindControl("txtServiceType")).Text = "";
                }
            }
        }
        #endregion

        protected void grdRelations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int holderTypeID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "HOLDERTYPEID"));
                System.Web.UI.HtmlControls.HtmlAnchor lnkHolderTypeDetails = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkHolderTypeDetails");
                //lnkHolderTypeDetails.Attributes.Add("onclick", "OpenHolderTypeDetails(" + holderTypeID + ")");
                if (lnkHolderTypeDetails.InnerText.Trim().ToLower() == "spouse" && ViewState["ChkMaritalStatus"].ToString().ToLower() == "single")
                {
                    lnkHolderTypeDetails.Attributes.Add("onclick", "alert('Cannot add Spouse. Selected Pap Marital Status is Single.'); return false;");
                }
                else
                    lnkHolderTypeDetails.Attributes.Add("onclick", "OpenHolderTypeDetails(" + holderTypeID + ")");

            }
        }
        /// <summary>
        /// to bind the Relations from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadRelations_Click(object sender, EventArgs e)
        {
            BindRelations();
        }

        /*
          protected void lstBillingAddress_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "GetAddr")
            {
                Literal litAddressID = (Literal)e.Item.FindControl("litAddressID");
                LinkButton lnkBillTo = (LinkButton)e.CommandSource;
                Literal litAddress = (Literal)e.Item.FindControl("litAddress");

                txtBuyer.Text = lnkBillTo.Text + "\r" + litAddress.Text.Replace("<br/>", "\r");
                ViewState["AddressID"] = litAddressID.Text;
            }
        }
        protected void lstBillingAddress_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litAddress = (Literal)e.Item.FindControl("litAddress");
                litAddress.Text = litAddress.Text.Replace("\r", "<br/>");
            }
        }
         */
    }
}