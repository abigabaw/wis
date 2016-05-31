using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;

namespace WIS
{
    public partial class GroupOwnership : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,call BindDropDownDistrict() to bind the district names to the dropdwon
        /// call BindOptionGroups() to get the data of the optiongroup
        /// call BindPositions() to get the data of the positions
        /// call BindGouAllowanc() to get the data of the GouAllowance
        /// call  GetHouseHoldDtlData() to get the household data 
        /// call  BindGrid() to bind the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }

            mskPlotReference.Mask = UtilBO.PlotReferenceMask;
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.HouseholdDetails;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.GroupOwnerShip;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.GroupMembersDetails;
            caldpDateofBirth.Format = UtilBO.DateFormat;
            cpCapturedDate.Format = UtilBO.DateFormat;
            LinkButton lnk = (LinkButton)ViewMasterCopy1.FindControl("lnkMasterCopy");
            lnk.ToolTip = "Main";
            LinkButton lnk1 = (LinkButton)ViewMasterCopy2.FindControl("lnkMasterCopy");
            lnk1.ToolTip = "Child1";
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Group Ownership Details";
                }
                #region PhotoModule / Upload
                int userID = Convert.ToInt32(Session["USER_ID"]);
                int ProjectID = 0;
                string ProjectCode = string.Empty;

                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                int HHID = 0;
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }

                if (Session["PROJECT_CODE"] != null)
                {
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                string PhotoModule = "PAPGROUP";
                string DocumentCode = "HH";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, PhotoModule);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, PhotoModule);

                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
                //End of code
                #endregion
                BindDropDownDistrict();
                BindOptionGroups();
                BindPositions();
                BindGouAllowanc();
                GetHouseHoldDtlData();
                BindGrid();
                projectFrozen();
                txtPapUid.Attributes.Add("onchange", "setDirty();");
                txtSurname.Attributes.Add("onchange", "setDirty();");
                txtCapturedBy.Attributes.Add("onchange", "setDirty();");
                txtfirstname.Attributes.Add("onchange", "setDirty();");
                txtPlotReference.Attributes.Add("onchange", "setDirty();");
                txtTelephoneNo1.Attributes.Add("onchange", "setDirty();");
                txtMeberSurname.Attributes.Add("onchange", "setDirty();");
                txtMeberFirstname.Attributes.Add("onchange", "setDirty();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
                //lnkChangeRequest.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoc.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkViewPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoclist.Attributes.Add("onclick", "isDirty = 0;");
                ViewMasterCopy1.Attributes.Add("onclick", "isDirty = 0;");
                btnSavemember.Attributes.Add("onclick", "isDirty = 0;");
                btnClearMember.Attributes.Add("onclick", "isDirty = 0;");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkGroupMenber.Visible = false;
                    lnkGroupOwner.Visible = false;
                    ddlPaptype.Enabled = false;
                    lnkUPloadDoc.Visible = false;
                    lnkUPloadPhoto.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSavemember.Visible = false;
                    btnClearMember.Visible = false;
                    grdGroupMembers.Columns[grdGroupMembers.Columns.Count - 1].Visible = false;
                    grdGroupMembers.Columns[grdGroupMembers.Columns.Count - 2].Visible = false;
                }
            }
            if (Request.QueryString["Mode"] != null)
            {
                Mode = Request.QueryString["Mode"].ToString();
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
                imgSearch.Visible = false;
                lnkGroupMenber.Visible = false;
                lnkGroupOwner.Visible = false;
                ddlPaptype.Enabled = false;
                lnkUPloadDoc.Visible = false;
                lnkUPloadPhoto.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                btnSavemember.Visible = false;
                btnClearMember.Visible = false;
                grdGroupMembers.Columns[grdGroupMembers.Columns.Count - 1].Visible = false;
                grdGroupMembers.Columns[grdGroupMembers.Columns.Count - 2].Visible = false;
            }
        }
        #endregion
        /// <summary>
        /// to call Frozen/approval/decline/pending status methods
        /// and to upload PAP photo
        /// </summary>
        #region for Frozen / Approval / Decline / Pedning status
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string Frozen = Session["FROZEN"].ToString();
                if (Frozen == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSavemember.Visible = false;
                    btnClearMember.Visible = false;
                    //lnkGroupOwner.Visible = true;
                    //lnkGroupMenber.Visible = true;

                    checkApprovalExitOrNot();
                    //getApprrequtStatusGroupOwnerShip();
                    //getApprrequtStatusGroupMemberShip();
                }
                else
                {
                    lnkGroupOwner.Visible = false;
                    lnkGroupMenber.Visible = false;
                }
            }
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusGroupOwner.Text = "";
            StatusGroupOwner.Visible = false; // used to display the Status if you send Request for change data
            StatusGroupMenber.Text = "";
            StatusGroupMenber.Visible = false;
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
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHGOS");
                lnkGroupOwner.Attributes.Add("onclick", paramChangeRequest);
                string paramChangeRequest1 = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHGMS");
                lnkGroupMenber.Attributes.Add("onclick", paramChangeRequest1);
                lnkGroupOwner.Visible = true;
                lnkGroupMenber.Visible = true;
            }
            else
            {
                lnkGroupOwner.Visible = false;
                lnkGroupMenber.Visible = false;
            }
            #endregion
            getApprrequtStatusGroupOwnerShip();
            getApprrequtStatusGroupMemberShip();
        }

        public void ChangeRequestStatusGroupOwnerShip()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHGOS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusGroupOwnerShip()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHGOS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkGroupOwner.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusGroupOwner.Visible = true;
                    StatusGroupOwner.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkGroupOwner.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusGroupOwner.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkGroupOwner.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusGroupOwner.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }

        public void getApprrequtStatusGroupMemberShip()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHGMS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkGroupMenber.Visible = false;
                    btnSavemember.Visible = false;
                    btnClearMember.Visible = false;
                    StatusGroupMenber.Visible = true;
                    StatusGroupMenber.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkGroupMenber.Visible = true;
                    btnSavemember.Visible = false;
                    btnClearMember.Visible = false;
                    StatusGroupMenber.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkGroupMenber.Visible = false;
                    btnSavemember.Visible = true;
                    btnClearMember.Visible = true;
                    StatusGroupMenber.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        
       
        #region for PAP Photo in Update panal
        //writen By Ramu.S
        public void btnimgPAPPhoto_Click(object sender, EventArgs e)
        {
            imgPAPPhoto.ImageUrl = null;
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            getImage(householdID);
            //imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString();
            //updimgPAPPhoto.Update();

        }

        public void getImage(int householdID)
        {
            imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString() + "&dt=" + DateTime.Now.ToString();
            //"~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString();
            updimgPAPPhoto.Update();
        }
        #endregion
        /// <summary>
        /// to bind the option groups
        /// </summary>


        private void BindOptionGroups()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddloptionGroup.DataTextField = "OptionGroupName";
            ddloptionGroup.DataValueField = "OptionGroupID";
            ddloptionGroup.DataSource = objMasterBLL.LoadOptionGroupData();
            ddloptionGroup.DataBind();
            ListItem firstListItem = new ListItem("--Select--", "0");
            ddloptionGroup.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// to bind the data  of GouAllowances
        /// </summary>
        private void BindGouAllowanc()
        {
            GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();

            ddlGouAllowance.DataSource = GOUAllowanceBLLObj.GetGouAllowance();
            ddlGouAllowance.DataTextField = "GOUAllowanceCategory";
            ddlGouAllowance.DataValueField = "GOUALLOWANCECATEGORYID";
            ddlGouAllowance.DataBind();

        }
        /// <summary>
        /// to bind the data of positions
        /// </summary>

        private void BindPositions()
        {
            PositionBLL PositionBLLobj = new PositionBLL();
            ddlPosition.DataTextField = "PositionName";
            ddlPosition.DataValueField = "POSITIONID";
            ddlPosition.DataSource = PositionBLLobj.GetPosition();
            ddlPosition.DataBind();

            ListItem firstListItem = new ListItem("--Select--", "0");
            ddlPosition.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// to get the houseHold details of the data
        /// </summary>
        private void GetHouseHoldDtlData()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHousaeHoldData(householdID);
            if (objHouseHold != null)
            {
                txtHouseHoldID.Text = Convert.ToString(objHouseHold.HhId);
                txtPlotReference.Text = Convert.ToString(objHouseHold.PlotReference);
                txtTelephoneNo1.Text = Convert.ToString(objHouseHold.Contactphone1);
                txtTelephoneNo2.Text = Convert.ToString(objHouseHold.Contactphone2);
                txtSurname.Text = Convert.ToString(objHouseHold.Surname);
                txtfirstname.Text = Convert.ToString(objHouseHold.Firstname);
                txtOthername.Text = Convert.ToString(objHouseHold.Othername);
                txtFullname.Text = txtSurname.Text + " " + txtfirstname.Text ;
                txtPapUid.Text = Convert.ToString(objHouseHold.Papuid);
                rdlResident.ClearSelection();
                if (objHouseHold.Isresident == "No")
                {
                    rdlResident.Items[1].Selected = true;
                }
                else
                {
                    rdlResident.Items[0].Selected = true;
                }
                ddloptionGroup.ClearSelection();
                ddloptionGroup.SelectedValue = Convert.ToString(objHouseHold.OptiongroupId);

                ddlGender.ClearSelection();
                if (ddlGender.Items.FindByValue(objHouseHold.Sex) != null)
                    ddlGender.Items.FindByValue(objHouseHold.Sex).Selected = true;

                ddlPosition.ClearSelection();
                if (ddlPosition.Items.FindByValue(Convert.ToString(objHouseHold.PositionId)) != null)
                    ddlPosition.Items.FindByValue(Convert.ToString(objHouseHold.PositionId)).Selected = true;

                if (objHouseHold.DateofBirth.Trim() != "")
                    dpDateofBirth.Text = Convert.ToDateTime(objHouseHold.DateofBirth).ToString(UtilBO.DateFormat);

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(Convert.ToString(objHouseHold.District).ToUpper()) != null)
                    ddlDistrict.Items.FindByText(Convert.ToString(objHouseHold.District).ToUpper()).Selected = true;

                if (ddlDistrict.SelectedIndex > 0)
                {
                    BindCounties(ddlDistrict.SelectedItem.Value);

                    if (Convert.ToString(objHouseHold.County) != "")
                    {
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(Convert.ToString(objHouseHold.County).ToUpper()) != null)
                            ddlCounty.Items.FindByText(Convert.ToString(objHouseHold.County).ToUpper()).Selected = true;
                    }
                }

                if (ddlCounty.SelectedIndex > 0)
                {
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    if (Convert.ToString(objHouseHold.SubCounty) != "")
                    {
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(Convert.ToString(objHouseHold.SubCounty).ToUpper()) != null)
                            ddlSubCounty.Items.FindByText(Convert.ToString(objHouseHold.SubCounty).ToUpper()).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    if (Convert.ToString(objHouseHold.Village) != "")
                    {
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(Convert.ToString(objHouseHold.Village).ToUpper()) != null)
                            ddlVillage.Items.FindByText(Convert.ToString(objHouseHold.Village).ToUpper()).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindParish(ddlSubCounty.SelectedItem.Value);
                    if (Convert.ToString(objHouseHold.Parish) != "")
                    {
                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()) != null)
                            ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()).Selected = true;
                    }
                }

                if (objHouseHold.CapturedDate.Trim() != "")
                    dpCapturedDate.Text = Convert.ToDateTime(objHouseHold.CapturedDate).ToString(UtilBO.DateFormat);
                txtCapturedBy.Text = Convert.ToString(objHouseHold.CapturedBy);

                ddlGouAllowance.ClearSelection();
                if (ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()) != null)
                    ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()).Selected = true;

                ddlUnderTakingPeriod.ClearSelection();
                if (ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()) != null)
                    ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()).Selected = true;

                getImage(householdID);
                //imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString();
            }
        }
        /// <summary>
        /// to bind the data from the database to the district dropdownlist
        /// </summary>

        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// to  redirect the page on selected PAP type from the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlPaptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaptype.SelectedValue == "IND")
            {
                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Household.aspx?PagePaptype=1"));
            }
            else if (ddlPaptype.SelectedValue == "GRP")
            {
                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/GroupOwnership.aspx?PagePaptype=1"));
            }
            else { Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Institution.aspx?PagePaptype=1")); }
        }
        /// <summary>
        /// to fill the data of county from the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            BindVillages(ddlSubCounty.SelectedItem.Value);
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// to fill the data of subcounty from the database wrt selected county
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// to fill the data of District  from the database wrt selected subcounty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// bind the counties from the database to the dropdown ddlCounty
        /// </summary>
        /// <param name="districtID"></param>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);
            ddlCounty.Items.Clear();
            if (districtID != "0")
            {
                ddlCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();
            }

            ddlCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// bind the subcounties from the database to the dropdown ddlSubCounty
        /// </summary>
        /// <param name="countyID"></param>

        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);
            ddlSubCounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataBind();
            }
            ddlSubCounty.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// bind the Villages from the database to the dropdown ddlVillage
        /// </summary>
        /// <param name="subCountyID"></param>

        private void BindVillages(string subCountyID)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);
            ddlVillage.Items.Clear();

            if (subCountyID != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCountyID);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }
            ddlVillage.Items.Insert(0, firstListItem);
        }

        /// <summary>
        /// bind the parish from the database to the dropdown ddlParish
        /// </summary>
        /// <param name="subCountyID"></param>
        private void BindParish(string subCountyID)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);
            ddlParish.Items.Clear();

            if (subCountyID != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCountyID);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishId";
                ddlParish.DataBind();
            }
            ddlParish.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// to save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            getApprrequtStatusGroupOwnerShip();//add by ramu.s @ 11 /07/2013
           
            PAP_GroupOwnershipBO objGroupOwnership = new PAP_GroupOwnershipBO();
            objGroupOwnership.HHID = Convert.ToInt32(Session["HH_ID"]);
            objGroupOwnership.Paptype = ddlPaptype.SelectedValue;
            objGroupOwnership.Papuid = txtPapUid.Text.Trim();
            objGroupOwnership.DistrictIN = ddlDistrict.SelectedItem.Text;
            objGroupOwnership.CountyIN = ddlCounty.SelectedItem.Text;
            objGroupOwnership.SubCountyIN = ddlSubCounty.SelectedItem.Text;
            objGroupOwnership.ParishIN = ddlParish.SelectedItem.Text;
            objGroupOwnership.VillageIN = ddlVillage.SelectedItem.Text;
            objGroupOwnership.OptionGroupIdIN = Convert.ToInt32(ddloptionGroup.SelectedItem.Value);
            objGroupOwnership.PlotReferenceIN = txtPlotReference.Text;
            objGroupOwnership.DateofBirthIN =Convert.ToDateTime(dpDateofBirth.Text.ToString());
            objGroupOwnership.IsResidentIN = rdlResident.SelectedItem.Text;
            objGroupOwnership.SexIN = ddlGender.SelectedItem.Text;
            objGroupOwnership.SurnameIN = txtSurname.Text;
            objGroupOwnership.FirstnameIN = txtfirstname.Text;
            objGroupOwnership.OthernameIN = txtOthername.Text;
            objGroupOwnership.PositionidIN = Convert.ToInt32(ddlPosition.SelectedValue);
            objGroupOwnership.Contactphone1IN = txtTelephoneNo1.Text;
            objGroupOwnership.Contactphone2IN = txtTelephoneNo2.Text;
            objGroupOwnership.Gouallowance = ddlGouAllowance.SelectedValue;
            objGroupOwnership.Undertakingperiod = ddlUnderTakingPeriod.SelectedValue;
            objGroupOwnership.Createdby = Convert.ToInt32(Session["USER_ID"]);
            if (dpCapturedDate.Text.ToString().Trim() != "" && dpCapturedDate.Text.ToString().Trim() != "1/1/0001")
                objGroupOwnership.CapturedDate = dpCapturedDate.Text.ToString();
            objGroupOwnership.CapturedBy = txtCapturedBy.Text;

            PAP_GroupOwnershipBLL objGroupOwnershipBll = new PAP_GroupOwnershipBLL();
            string message = objGroupOwnershipBll.UpdateGroupOwnershipDetails(objGroupOwnership);
            txtFullname.Text = txtSurname.Text + " " + txtfirstname.Text ;
            projectFrozen();//add by ramu.s @ 11 /07/2013
            ChangeRequestStatusGroupOwnerShip();
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }

        /// <summary>
        /// to clear the all the fields by calling ClearDetails() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        /// <summary>
        /// to clear all the fields by calling ClearDetails() method
        /// </summary>
        protected void ClearDetails()
        {
            txtCapturedBy.Text = "";
            dpCapturedDate.Text = "";
            dpDateofBirth.Text = "";
            ddlDistrict.ClearSelection();
            ddlDistrict.Items[0].Selected = true;
            ddlCounty.ClearSelection();
            ddlCounty.Items[0].Selected = true;
            ddlSubCounty.ClearSelection();
            ddlSubCounty.Items[0].Selected = true;
            ddlParish.ClearSelection();
            ddlParish.Items[0].Selected = true;
            ddlVillage.ClearSelection();
            ddlVillage.Items[0].Selected = true;
            ddlGender.ClearSelection();
            ddlGender.Items[0].Selected = true;
            ddloptionGroup.ClearSelection();
            ddloptionGroup.Items[0].Selected = true;
            rdlResident.ClearSelection();
            txtfirstname.Text = "";
            txtSurname.Text = "";
            txtOthername.Text = "";
            txtFullname.Text = "";
            txtPapUid.Text = "";
            ddlPosition.ClearSelection();
            ddlPosition.Items[0].Selected = true;
            txtTelephoneNo1.Text = "";
            txtTelephoneNo2.Text = "";
            txtPlotReference.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + dpDateofBirth.ClientID + "');", true);
        }

        /// <summary>
        /// to save the member data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSavemember_Click(object sender, EventArgs e)
        {
            PAP_GroupOwnershipBO objGroupOwnership = new PAP_GroupOwnershipBO();
            if (ViewState["Groupmemberid"] != null)
                objGroupOwnership.Groupmemberid = Convert.ToInt32(ViewState["Groupmemberid"]);
            else
                objGroupOwnership.Groupmemberid = 0;
            objGroupOwnership.HHID = Convert.ToInt32(Session["HH_ID"]);
            objGroupOwnership.SurnameIN = txtMeberSurname.Text;
            objGroupOwnership.FirstnameIN = txtMeberFirstname.Text;
            objGroupOwnership.OthernameIN = txtMeberOthername.Text;
            objGroupOwnership.Createdby = Convert.ToInt32(Session["USER_ID"]);

            PAP_GroupOwnershipBLL objGroupOwnershipBll = new PAP_GroupOwnershipBLL();
            objGroupOwnershipBll.InsertandUpdateGroupOwnership(objGroupOwnership);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
            txtMeberSurname.Text = "";
            txtMeberFirstname.Text = "";
            txtMeberOthername.Text = "";
            txtMeberFullname.Text = "";
            ViewState["Groupmemberid"] = 0;
            btnSavemember.Text = "Save";
            btnClearMember.Text = "Clear";
            projectFrozen();  //add by ramu.s @ 11 /07/2013
            getApprrequtStatusGroupMemberShip();//add by ramu.s @ 11 /07/2013
            BindGrid();
        }
        /// <summary>
        /// to clear the member data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearMember_Click(object sender, EventArgs e)
        {
            txtMeberSurname.Text = "";
            txtMeberFirstname.Text = "";
            txtMeberOthername.Text = "";
            txtMeberFullname.Text = "";
            ViewState["Groupmemberid"] = 0;
            btnSavemember.Text = "Save";
            btnClearMember.Text = "Clear";
        }
        /// <summary>
        /// to set the page index's of the gridview grdGroupMembers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdGroupMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGroupMembers.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// to get the data from the database and bind it to the gridview grdGroupMembers
        /// </summary>
        private void BindGrid()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            PAP_GroupOwnershipBLL objGroupOwnershipBLL = new PAP_GroupOwnershipBLL();
            grdGroupMembers.DataSource = objGroupOwnershipBLL.GetGroupOwnershipByHHID(householdID);
            grdGroupMembers.DataBind();
        }
        /// <summary>
        /// to open the new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

        }
        /// <summary>
        /// Set edit mode for edit comand Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdGroupMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["Groupmemberid"] = e.CommandArgument;
                PAP_GroupOwnershipBLL objGroupOwnershipBLL = new PAP_GroupOwnershipBLL();
                PAP_GroupOwnershipList objGroupOwnershipList = objGroupOwnershipBLL.GetGroupOwnershipByHHID(Convert.ToInt32(Session["HH_ID"]));
                if (objGroupOwnershipList.Count > 0)
                {
                    for (int iset = 0; iset < objGroupOwnershipList.Count; iset++)
                    {
                        if (Convert.ToString(ViewState["Groupmemberid"]) == Convert.ToString(objGroupOwnershipList[iset].Groupmemberid))
                        {
                            txtMeberSurname.Text = objGroupOwnershipList[iset].SurnameIN;
                            txtMeberFirstname.Text = objGroupOwnershipList[iset].FirstnameIN;
                            txtMeberOthername.Text = objGroupOwnershipList[iset].OthernameIN;
                            txtMeberFullname.Text = txtMeberSurname.Text + " " + txtMeberFirstname.Text;
                            btnSavemember.Text = "Update";
                            btnClearMember.Text = "Cancel";
                        }
                    }
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                PAP_GroupOwnershipBLL objGroupOwnershipBLL = new PAP_GroupOwnershipBLL();
                objGroupOwnershipBLL.DeleteGroupOwnershipByGMID(Convert.ToInt32(e.CommandArgument));
                ViewState["RELATION_ID"] = "0";
                txtMeberSurname.Text = "";
                txtMeberFirstname.Text = "";
                txtMeberOthername.Text = "";
                txtMeberFullname.Text = "";
                ViewState["Groupmemberid"] = 0;
                btnSavemember.Text = "Save";
                btnClearMember.Text = "Clear";
                BindGrid();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Selected Group Member Deleted successfully');", true);
            }
        }
    }
}