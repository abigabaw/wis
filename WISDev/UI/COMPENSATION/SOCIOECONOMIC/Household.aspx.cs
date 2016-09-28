using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class Household : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,call BindOptionGroups() bind the option group data from the database
        /// call BindGouAllowanc() to bind the GouAllowance data from the database
        /// call BindDropDownDistrict() to bind the District name to the dropdown from the database
        /// call BindDropDownTribes()  to bind the Tribes name to the dropdown from the database
        /// call BindReligions() to bind the Religion data to the dropdown from the database
        /// call BindOccupations() to bind the Occupations to the dropdown from the database
        /// call   BindStatuses() to set the occupation status data 
        /// to set the status of the link button lnkChangeRequest,lnkUPloadDoc,lnkUPloadPhoto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        
        #region Declaration & PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            //ddloptionGroup.ClearSelection();
            ddloptionGroup.Enabled = true;
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.HouseholdDetails;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.HouseholdDetails;
            mskPlotReference.Mask = UtilBO.PlotReferenceMask;

            calDateOfBirth.Format = UtilBO.DateFormat;
            cpCapturedDate.Format = UtilBO.DateFormat;

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
            #region Postback

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Household Details";
                }

                #region Upload  PHOTO / Image
                //Add By Ramu For upload Document
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

                string PhotoModule = "PAP";
                string DocumentCode = "HH";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, PhotoModule);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, PhotoModule);

                string paramViewComments = string.Format("OpenViewComments();");

                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);

                lnkViewComments.Attributes.Add("onclick", paramViewComments);

                //End of code
                #endregion
                BindOptionGroups();
                BindGouAllowanc();
                BindDropDownDistrict();
                BindDropDownTribes();
                BindReligions();
                BindOccupations();
                BindStatuses();
                BindCardtype();
                BindLiteracyStatuses();
                GetHouseHoldDtlData();
                CardTypeEnabled(false);
                Religion();
                chkOverRide.Attributes.Add("onclick", "ShowHideBoundaryDisputes();");
                // chkOverRide.Attributes.Add("onclick", "ShowHideBoundaryDisputes();");
                dpDateOfBirth.Attributes.Add("onblur", "CheckYearForMove();");
                txtwhendiducomehere.Attributes.Add("onblur", "CheckYear();");
                rdoParentAliveYes.Attributes.Add("onclick", "ShowHideWhichParents(1);");
                rdoParentAliveNo.Attributes.Add("onclick", "ShowHideWhichParents(0);");
                rdoIdentificationCardYes.Attributes.Add("onclick", "EnableDisableIDFields(1);");
                rdoIdentificationCardNo.Attributes.Add("onclick", "EnableDisableIDFields(0);");
                ddlcardType.Attributes.Add("onchange", "CheckCardType();");

                txtPapUid.Attributes.Add("onchange", "setDirty();");
                txtName.Attributes.Add("onchange", "setDirty();");
                txtCapturedBy.Attributes.Add("onchange", "setDirty();");
                txtDesignation.Attributes.Add("onchange", "setDirty();");
                txtPlotReference.Attributes.Add("onchange", "setDirty();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
                //lnkChangeRequest.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoc.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkViewPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoclist.Attributes.Add("onclick", "isDirty = 0;");
                ViewMasterCopy1.Attributes.Add("onclick", "isDirty = 0;");
                //lnkChangeRequest.Attributes.Add("onclick", "isDirty = 0;");
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkChangeRequest.Visible = false;
                    ddlPaptype.Enabled = false;
                    lnkUPloadDoc.Visible = false;
                    lnkUPloadPhoto.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
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
                imgSearch.Visible = false;
                lnkChangeRequest.Visible = false;
                ddlPaptype.Enabled = false;
                lnkUPloadDoc.Visible = false;
                lnkUPloadPhoto.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
            #endregion postback
        }

        #endregion

        #region checkChangeRequestApproverExit and getApprovalChangerequestStatus
        //Added By Ramu
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;

                    lnkUPloadDoc.Visible = false;
                    lnkUPloadPhoto.Visible = false;

                    checkApprovalExitOrNot();
                }
            }
        }
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusLabel.Text = "";

            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "HH";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                lnkChangeRequest.Attributes.Add("onclick", paramChangeRequest);
                lnkChangeRequest.Visible = true;
            }
            else
            {
                lnkChangeRequest.Visible = false;
            }

            getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            #endregion
        }
        /// <summary>
        /// to get the Approval Changerequest Status from the database
        /// </summary>

        public void getApprovalChangerequestStatus()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkChangeRequest.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusLabel.Visible = true;
                    StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkChangeRequest.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusLabel.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkChangeRequest.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusLabel.Visible = false;
                    lnkUPloadDoc.Visible = true;
                    lnkUPloadPhoto.Visible = true;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }


        #endregion

        #region Load Data
        /// <summary>
        /// bind the House Hold data from the database
        /// </summary>
        private void GetHouseHoldDtlData()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHouseHoldData(householdID);
            PAP_HouseholdBO objhhfrComnts = objHouseHoldBLL.getCommentsData(householdID);
            if (objhhfrComnts != null)
            {
                txtBoundaryDisputes.Text = objhhfrComnts.Comments;
            }
            if (objHouseHold != null)
            {
                txtHouseHoldID.Text = Convert.ToString(objHouseHold.HhId);
                txtName.Text = Convert.ToString(objHouseHold.PapName);
                txtPapUid.Text = Convert.ToString(objHouseHold.Papuid);
                ddloptionGroup.SelectedValue = Convert.ToString(objHouseHold.OptiongroupId);

                if (!IsPostBack)
                {
                    string prevPage = string.Empty;
                    if (Request.UrlReferrer != null)
                        prevPage = Request.UrlReferrer.ToString();
                    string PagePaptype = string.Empty;
                    if (Request.QueryString["PagePaptype"] != null)
                    {
                        PagePaptype = Request.QueryString["PagePaptype"].ToString();
                    }
                    if ((!prevPage.ToLower().Contains("institution.aspx") && !prevPage.ToLower().Contains("groupownership.aspx"))
                        || PagePaptype != "1")
                    {
                        string Paptype = objHouseHold.Paptype;
                        string Mode = string.Empty;
                        if (Request.QueryString["Mode"] != null)
                        {
                            Mode = Request.QueryString["Mode"].ToString();
                        }
                        if (Paptype.ToUpper() == "INS")
                        {
                            if (Mode == "Readonly")
                                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Institution.aspx?Mode=" + Mode));
                            else
                                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Institution.aspx"));
                        }
                        else if (Paptype.ToUpper() == "GRP")
                        {
                            if (Mode == "Readonly")
                                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Groupownership.aspx?Mode=" + Mode));
                            else
                                Response.Redirect(ResolveUrl("~/UI/Compensation/SocioEconomic/Groupownership.aspx"));
                        }
                    }
                }
                if (objHouseHold.LandCompensation == "Cash")
                {
                    RdbtnCash.Checked = true;

                }
                else if (objHouseHold.LandCompensation == "In-Kind")
                {
                    Rdbtninkind.Checked = true;

                }
                else if (objHouseHold.LandCompensation == "Both")
                {
                    RdbtnBoth.Checked = true;
                }
                else if (objHouseHold.LandCompensation == "Not Applicable")
                {
                    RdbtnNotApplic.Checked = true;
                }
                if (objHouseHold.HouseCompensation == "Cash")
                {
                    RdbtnHcash.Checked = true;
                }
                else if (objHouseHold.HouseCompensation == "In-Kind")
                {
                    RdbtnHbtninkind.Checked = true;

                }
                else if (objHouseHold.HouseCompensation == "Both")
                {
                    RdbtnHBoth.Checked = true;

                }
                else if (objHouseHold.HouseCompensation == "Not Applicable")
                {
                    RdbtnHNotApplic.Checked = true;

                }
                if (objHouseHold.Overrideopt == "Y")
                {
                    chkOverRide.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "ShowHideBoundaryDisputes();", true);
                    ddloptionGroup.SelectedValue = Convert.ToString(objHouseHold.OptiongroupId);
                    ddloptionGroup.Enabled = true;
                    Overide_Checkbox();
                }
                else
                {
                    ddloptionGroup.Enabled = false;
                    Overide_Checkbox();
                }
                txtPlotReference.Text = Convert.ToString(objHouseHold.PlotReference);


                ddlGender.ClearSelection();
                if (ddlGender.Items.FindByValue(objHouseHold.Sex) != null)
                    ddlGender.Items.FindByValue(objHouseHold.Sex).Selected = true;

                txtPlaceofBirth.Text = Convert.ToString(objHouseHold.PlaceofBirth);

                if (objHouseHold.DateofBirth.Trim() != "")
                    dpDateOfBirth.Text = Convert.ToDateTime(objHouseHold.DateofBirth).ToString(UtilBO.DateFormat);


                if (objHouseHold.CapturedDate.Trim() != "")
                    dpCapturedDate.Text = Convert.ToDateTime(objHouseHold.CapturedDate).ToString(UtilBO.DateFormat);
                txtCapturedBy.Text = Convert.ToString(objHouseHold.CapturedBy);

                txtPlaceofBirth.Text = Convert.ToString(objHouseHold.PlaceofBirth);
                txtwhendiducomehere.Text = Convert.ToString(objHouseHold.Yearmoveon);

                txtwhereparentslive.Text = Convert.ToString(objHouseHold.Whereparentslive);
                ddlcardType.ClearSelection();
                if (ddlcardType.Items.FindByValue(objHouseHold.Cardtype) != null)
                    ddlcardType.Items.FindByValue(objHouseHold.Cardtype).Selected = true;

                txtNameofCard.Text = Convert.ToString(objHouseHold.NameonCard);
                txtaddressoncard.Text = Convert.ToString(objHouseHold.AddressonCard);
                txtpercentage.Text = Convert.ToString(objHouseHold.PercentageOccupied);

                ddlMaritalStatus.ClearSelection();
                if (ddlMaritalStatus.Items.FindByValue(objHouseHold.MaritalStatus) != null)
                {
                    ddlMaritalStatus.Items.FindByValue(objHouseHold.MaritalStatus).Selected = true;

                    if (ddlMaritalStatus.SelectedItem.Text.ToUpper() == "MARRIED")
                    {
                        txtMaritalStatus.Text = objHouseHold.NoofSpouse.ToString();
                        txtMaritalStatus.Enabled = true;
                    }
                }

                ddlTribe.ClearSelection();
                if (ddlTribe.Items.FindByText(objHouseHold.Tribe) != null)
                    ddlTribe.Items.FindByText(objHouseHold.Tribe).Selected = true;

                BindClans(ddlTribe.SelectedItem.Value);

                if (ddlClan.Items.FindByText(objHouseHold.Clan) != null)
                    ddlClan.Items.FindByText(objHouseHold.Clan).Selected = true;

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(objHouseHold.District) != null)
                    ddlDistrict.Items.FindByText(objHouseHold.District).Selected = true;

                if (ddlDistrict.SelectedIndex > 0)
                {
                    BindCounties(ddlDistrict.SelectedItem.Value);

                    if (Convert.ToString(objHouseHold.County) != "")
                    {
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(objHouseHold.County) != null)
                            ddlCounty.Items.FindByText(objHouseHold.County).Selected = true;
                    }
                }

                if (ddlCounty.SelectedIndex > 0)
                {
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    if (Convert.ToString(objHouseHold.SubCounty) != "")
                    {
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(objHouseHold.SubCounty) != null)
                            ddlSubCounty.Items.FindByText(objHouseHold.SubCounty).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    if (Convert.ToString(objHouseHold.Village) != "")
                    {
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(objHouseHold.Village) != null)
                            ddlVillage.Items.FindByText(objHouseHold.Village).Selected = true;
                    }

                    BindParish(ddlSubCounty.SelectedItem.Value);
                    if (objHouseHold.Parish != null || Convert.ToString(objHouseHold.Parish) != "")
                    {
                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()) != null)
                            ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()).Selected = true;
                    }
                }

                ddlReligion.ClearSelection();
                if (ddlReligion.Items.FindByValue(objHouseHold.ReligionId.ToString()) != null)
                    ddlReligion.Items.FindByValue(objHouseHold.ReligionId.ToString()).Selected = true;

                ddlOccupation.ClearSelection();
                if (ddlOccupation.Items.FindByValue(objHouseHold.OccupationId.ToString()) != null)
                    ddlOccupation.Items.FindByValue(objHouseHold.OccupationId.ToString()).Selected = true;

                OccupationStatus.ClearSelection();
                if (OccupationStatus.Items.FindByValue(objHouseHold.PapstatusId.ToString()) != null)
                    OccupationStatus.Items.FindByValue(objHouseHold.PapstatusId.ToString()).Selected = true;

                ddlLiteracyStatus.ClearSelection();
                if (ddlLiteracyStatus.Items.FindByValue(objHouseHold.LiteracyCycleId.ToString()) != null)
                    ddlLiteracyStatus.Items.FindByValue(objHouseHold.LiteracyCycleId.ToString()).Selected = true;

                ddlGouAllowance.ClearSelection();
                if (ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()) != null)
                    ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()).Selected = true;

                ddlUnderTakingPeriod.ClearSelection();
                if (ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()) != null)
                    ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()).Selected = true;


                if (objHouseHold.Parentslive.ToUpper() == "YES")
                {
                    rdoParentAliveYes.Checked = true;
                    rdoParentAliveNo.Checked = false;
                    if (objHouseHold.Whichparentalive.ToString() == "Father")
                        rdoParentAliveFather.Checked = true;
                    else if (objHouseHold.Whichparentalive.ToString() == "Mother")
                        rdoParentAliveMother.Checked = true;
                    else
                        rdoParentAliveBoth.Checked = true;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideWhichParents", "ShowHideWhichParents(1);", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideWhichParents", "ShowHideWhichParents(0);", true);

                if (objHouseHold.Isidentificationcard.ToUpper() == "No".ToUpper())
                {
                    rdoIdentificationCardNo.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick123", "EnableDisableIDFields(0);", true);
                }
                else
                {
                    rdoIdentificationCardYes.Checked = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick12", "EnableDisableIDFields(1);", true);
                }

                if (objHouseHold.Isresident == "No")
                {
                    rdnResident.Items[1].Selected = true;
                }
                else
                {
                    rdnResident.Items[0].Selected = true;
                }

                txtDesignation.Text = objHouseHold.Designation;

                getImage(householdID);

                // imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString();
            }
        }
        #endregion Load Data

        #region for PAP Photo in Update panal
        //writen By Ramu.S
        /// <summary>
        /// to retrieve the PAP photo from the database on HHID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnimgPAPPhoto_Click(object sender, EventArgs e)
        {
            imgPAPPhoto.ImageUrl = null;
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            getImage(householdID);
            //imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString();
            //updimgPAPPhoto.Update();

        }
        /// <summary>
        /// to get the Image Path
        /// </summary>
        /// <param name="householdID"></param>
        public void getImage(int householdID)
        {
            imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + householdID.ToString() + "&dt=" + DateTime.Now.ToString();
            updimgPAPPhoto.Update();
        }
        #endregion

        /// <summary>
        /// to save the data to the database  
        /// clear the all the fields by calling  ClearCache() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void ReCache(int HHID)
        {
            PapDataCache PapCache = new PapDataCache();
            string householdID = Cache[PapCache.BuildCacheKey("HOUSEHOLD_ID")].ToString();
            PapCache.ClearCache();
            PapCache.CachePAPData(householdID);
        }

        #region Save & Clear Buttons
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.HhId = Convert.ToInt32(txtHouseHoldID.Text);
            objHouseHold.PapstatusId = Convert.ToInt32(OccupationStatus.SelectedValue);
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            objHouseHold.PapName = txtName.Text;
            objHouseHold.Papuid = txtPapUid.Text.Trim();
            objHouseHold.PlotReference = txtPlotReference.Text;
            
            objHouseHold.PercentageOccupied = Convert.ToDecimal(txtpercentage.Text);
            if (chkOverRide.Checked == true)
            {
                objHouseHold.Overrideopt = "Y";
                objHouseHold.Comments = txtBoundaryDisputes.Text;
            }
            else
            {
                objHouseHold.Overrideopt = "N";
                objHouseHold.Comments = String.Empty;
            }
            if (RdbtnCash.Checked == true)
            {
                objHouseHold.LandCompensation = "Cash";

            }
            else if (Rdbtninkind.Checked == true)
            {
                objHouseHold.LandCompensation = "In-Kind";

            }
            else if (RdbtnBoth.Checked == true)
            {
                objHouseHold.LandCompensation = "Both";

            }
            else if (RdbtnNotApplic.Checked == true)
            {
                objHouseHold.LandCompensation = "Not Applicable";

            }
            if (RdbtnHcash.Checked == true)
            {
                objHouseHold.HouseCompensation = "Cash";

            }
            else if (RdbtnHbtninkind.Checked == true)
            {
                objHouseHold.HouseCompensation = "In-Kind";

            }
            else if (RdbtnHBoth.Checked == true)
            {
                objHouseHold.HouseCompensation = "Both";

            }
            else if (RdbtnHNotApplic.Checked == true)
            {
                objHouseHold.HouseCompensation = "Not Applicable";

            }

            if (ddlDistrict.SelectedIndex > 0)
                objHouseHold.District = ddlDistrict.SelectedItem.Text;
            else
                objHouseHold.District = "";
            if (ddlCounty.SelectedIndex > 0)
                objHouseHold.County = ddlCounty.SelectedItem.Text;
            else
                objHouseHold.County = "";
            if (ddlSubCounty.SelectedIndex > 0)
                objHouseHold.SubCounty = ddlSubCounty.SelectedItem.Text;
            else
                objHouseHold.SubCounty = "";
            if (ddlParish.SelectedIndex == 0)
                objHouseHold.Parish = "";
            else
                objHouseHold.Parish = ddlParish.SelectedItem.Text;
            if (ddlVillage.SelectedIndex > 0)
                objHouseHold.Village = ddlVillage.SelectedItem.Text;
            else
                objHouseHold.Village = "";

            objHouseHold.Rightofway = "";
            objHouseHold.Wayleaves = "";
            if (chkOverRide.Checked == true)
            {
                objHouseHold.OptiongroupId = Convert.ToInt32(ddloptionGroup.SelectedValue);
            }
            else
            {
                string landStatus = OccupationStatus.SelectedItem.Text;
                PAP_HouseholdBLL objPAPHH = new PAP_HouseholdBLL();
                if (rdnResident.SelectedItem.Text == "Yes")
                {
                    objHouseHold.Isresident = "Resident";

                }
                else if (rdnResident.SelectedItem.Text == "No")
                {
                    objHouseHold.Isresident = "Non-Resident";

                }
                int OptId = objPAPHH.GetHousaeHold(objHouseHold, landStatus);
                objHouseHold.OptiongroupId = OptId;//Convert.ToInt32(ddloptionGroup.SelectedValue);
            }


            objHouseHold.Isresident = rdnResident.SelectedItem.Text;
            objHouseHold.Sex = ddlGender.SelectedItem.Value;
            objHouseHold.PlaceofBirth = txtPlaceofBirth.Text;
            if (dpDateOfBirth.Text.ToString().Trim() != "" && dpDateOfBirth.Text.ToString().Trim() != "1/1/0001")
                objHouseHold.DateofBirth = dpDateOfBirth.Text.ToString();
            objHouseHold.Yearmoveon = txtwhendiducomehere.Text;

            if (rdoParentAliveYes.Checked)
                objHouseHold.Parentslive = "YES";
            else
                objHouseHold.Parentslive = "NO";

            if (rdoParentAliveFather.Checked)
                objHouseHold.Whichparentalive = rdoParentAliveFather.Text;
            else if (rdoParentAliveMother.Checked)
                objHouseHold.Whichparentalive = rdoParentAliveMother.Text;
            else
                objHouseHold.Whichparentalive = rdoParentAliveBoth.Text;

            objHouseHold.Whereparentslive = txtwhereparentslive.Text.Trim();

            if (rdoIdentificationCardYes.Checked)
            {
                objHouseHold.Isidentificationcard = "YES";
                if (ddlcardType.SelectedIndex > 0)
                    objHouseHold.Cardtype = ddlcardType.SelectedItem.Value;
                objHouseHold.NameonCard = txtNameofCard.Text;
                objHouseHold.AddressonCard = txtaddressoncard.Text;
            }
            else
            {
                objHouseHold.Isidentificationcard = "NO";
                objHouseHold.Cardtype = string.Empty;
                objHouseHold.NameonCard = string.Empty;
                objHouseHold.AddressonCard = string.Empty;
            }

            objHouseHold.MaritalStatus = ddlMaritalStatus.SelectedItem.Text;

            if (ddlMaritalStatus.SelectedItem.Text.ToUpper() == "MARRIED")
            {
                if (txtMaritalStatus.Text.Trim() == "")
                    objHouseHold.NoofSpouse = 0;
                else
                {
                    objHouseHold.NoofSpouse = Convert.ToInt32(txtMaritalStatus.Text);
                }
            }
            else
            {
                objHouseHold.NoofSpouse = 0;
            }

            if (ddlTribe.SelectedIndex > 0)
                objHouseHold.Tribe = ddlTribe.SelectedItem.Text;
            else
                objHouseHold.Tribe = "";

            if (ddlClan.SelectedIndex > 0)
                objHouseHold.Clan = ddlClan.SelectedItem.Text;
            else
                objHouseHold.Clan = "";

            if (ddlReligion.SelectedValue == "-1" || ddlReligion.SelectedItem.Text.ToUpper() == "Other".ToUpper())
            {
                objHouseHold.ReligionId = -1;
                objHouseHold.Otherreligion = txtotherReligion.Text;
            }
            else
            {
                objHouseHold.ReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
                objHouseHold.Otherreligion = string.Empty;
            }
            objHouseHold.OccupationId = Convert.ToInt32(ddlOccupation.SelectedValue);
            objHouseHold.PapstatusId = Convert.ToInt32(OccupationStatus.SelectedValue);
            objHouseHold.LiteracyCycleId = Convert.ToInt32(ddlLiteracyStatus.SelectedValue);
            objHouseHold.Designation = txtDesignation.Text.Trim();
            objHouseHold.Isdeleted = "False";
            objHouseHold.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
            if (dpCapturedDate.Text.ToString().Trim() != "" && dpCapturedDate.Text.ToString().Trim() != "1/1/0001")
                objHouseHold.CapturedDate = dpCapturedDate.Text.ToString();
            objHouseHold.CapturedBy = txtCapturedBy.Text;

            objHouseHold.GouStatus = ddlGouAllowance.SelectedValue;
            objHouseHold.UnderTakingPeriod = ddlUnderTakingPeriod.SelectedValue;
            objHouseHold.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            string message = objHouseHoldBLL.UpdateHouseHoldDetails(objHouseHold);

            //Edwin: 19SEP2016 Reload Pap Details
            ReCache(objHouseHold.HhId);

            ChangeRequestStatus();
            projectFrozen();
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
            }

            string showHideParentsAlive = "";

            if (rdoParentAliveYes.Checked)
                showHideParentsAlive = "ShowHideWhichParents(true);";
            else
                showHideParentsAlive = "ShowHideWhichParents(false);";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ParetAliveDisp", showHideParentsAlive, true);

            if (rdoIdentificationCardYes.Checked)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick12", "EnableDisableIDFields(1);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick12", "EnableDisableIDFields(0);", true);
            }

            txtotherReligion.Text = "";

            GetHouseHoldDtlData();//Loading the Page Again for Page Refresh

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "ShowHideBoundaryDisputes();", true);
            ClearCache();
            //checkApprovalExitOrNot();
            //lnkChangeRequest.Visible = true;

        }

        public void ChangeRequestStatus()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        private string BuildCacheKey(string keyName)
        {
            return keyName + "_" + Session["USER_ID"].ToString();
        }

        private void ClearCache()
        {
            Cache.Remove(BuildCacheKey("HOUSEHOLD_ID"));
            Cache.Remove(BuildCacheKey("PAPNAME"));
            Cache.Remove(BuildCacheKey("PLOTREFERENCE"));
            Cache.Remove(BuildCacheKey("PAPDESIGNATION"));
            Cache.Remove(BuildCacheKey("Plotlatitude"));
            Cache.Remove(BuildCacheKey("Plotlongitude"));
        }
        /// <summary>
        /// to clear the data fields 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ListItem lstItem = null;

            OccupationStatus.ClearSelection();
            txtName.Text = "";
            txtPapUid.Text = "";
            txtPlotReference.Text = "";
            txtPlaceofBirth.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            dpDateOfBirth.Text = "";
            dpCapturedDate.Text = "";
            txtotherReligion.Text = "";
            ddlParish.ClearSelection();
            lstItem = ddlParish.Items[0];
            ddlParish.Items.Clear();
            ddlParish.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlVillage.Items[0];
            ddlVillage.Items.Clear();
            ddlVillage.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlSubCounty.Items[0];
            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlCounty.Items[0];
            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlDistrict.ClearSelection();

            ddloptionGroup.ClearSelection();
            rdnResident.ClearSelection();
            ddlGender.ClearSelection();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateFld", "ClearDateField('" + dpDateOfBirth.ClientID + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateFld1", "ClearDateField('" + dpCapturedDate.ClientID + "');", true);
            txtCapturedBy.Text = "";
            txtwhendiducomehere.Text = "";
            rdoParentAliveNo.Checked = false;
            rdoParentAliveYes.Checked = false;
            txtwhereparentslive.Text = "";

            rdoIdentificationCardYes.Checked = false;
            rdoIdentificationCardNo.Checked = false;
            ddlcardType.ClearSelection();
            txtNameofCard.Text = "";
            txtaddressoncard.Text = "";
            ddlMaritalStatus.ClearSelection();
            txtMaritalStatus.Text = "";

            lstItem = ddlClan.Items[0];
            ddlClan.Items.Clear();
            ddlClan.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlTribe.ClearSelection();

            ddlReligion.ClearSelection();
            ddlOccupation.ClearSelection();
            OccupationStatus.ClearSelection();
            ddlLiteracyStatus.ClearSelection();
            CardTypeEnabled(false);
            Religion();
            ddlGouAllowance.ClearSelection();
            ddlGouAllowance.SelectedIndex = 0;
            ddlUnderTakingPeriod.ClearSelection();
            ddlUnderTakingPeriod.SelectedIndex = 0;
        }
        #endregion Save & Clear Buttons

        /// <summary>
        /// to bind the data to the database
        /// </summary>

        #region Bind Data
        private void BindOptionGroups()
        {
            ListItem firstListItem = new ListItem(ddloptionGroup.Items[0].Text, ddloptionGroup.Items[0].Value);
            ddloptionGroup.Items.Clear();
            MasterBLL objMasterBLL = new MasterBLL();
            ddloptionGroup.DataTextField = "OptionGroupName";
            ddloptionGroup.DataValueField = "OptionGroupID";
            ddloptionGroup.DataSource = objMasterBLL.LoadOptionGroupData();
            ddloptionGroup.DataBind();

            ddloptionGroup.Items.Insert(0, firstListItem);
        }

        private void BindLiteracyStatuses()
        {
            LiteracyStatusBLL objMasterBLL = new LiteracyStatusBLL();
            ddlLiteracyStatus.DataTextField = "LTR_STATUS";
            ddlLiteracyStatus.DataValueField = "LTR_STATUSID";
            ddlLiteracyStatus.DataSource = objMasterBLL.GetLiteracyStatus();
            ddlLiteracyStatus.DataBind();
        }

        private void BindCardtype()
        {
            CardTypeBLL objMasterBLL = new CardTypeBLL();
            ddlcardType.DataTextField = "CardTypeName";
            ddlcardType.DataValueField = "CardTypeID";
            ddlcardType.DataSource = objMasterBLL.GetCardType();
            ddlcardType.DataBind();

        }

        private void BindStatuses()
        {
            PstatusBLL objMasterBLL = new PstatusBLL();
            OccupationStatus.DataTextField = "PAPDESIGNATION1";
            OccupationStatus.DataValueField = "PAPDESIGNATIONID1";
            OccupationStatus.DataSource = objMasterBLL.GetPstatus();
            OccupationStatus.DataBind();
        }

        private void BindOccupations()
        {
            OccupationBLL objMasterBLL = new OccupationBLL();
            ddlOccupation.DataTextField = "OCCUPATIONNAME";
            ddlOccupation.DataValueField = "OCCUPATIONID";
            ddlOccupation.DataSource = objMasterBLL.GetOccupation();
            ddlOccupation.DataBind();
        }

        private void BindReligions()
        {
            ListItem lstListItem = new ListItem("Other", "-1");
            MasterBLL objMasterBLL = new MasterBLL();
            ddlReligion.DataTextField = "ReligionName";
            ddlReligion.DataValueField = "ReligionID";
            ddlReligion.DataSource = objMasterBLL.LoadReligionData();
            ddlReligion.DataBind();
            ddlReligion.Items.Insert(ddlReligion.Items.Count, lstListItem);
        }

        private void BindDropDownTribes()
        {
            TribeBLL objMasterBLL = new TribeBLL();
            ddlTribe.DataTextField = "TribeName";
            ddlTribe.DataValueField = "TribeID";
            ddlTribe.DataSource = objMasterBLL.FetchTribeList();
            ddlTribe.DataBind();
        }

        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
        }

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

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

        }
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParish(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();

            //BindCounties(ddlDistrict.SelectedItem.Value);
            //BindSubCounties(ddlCounty.SelectedItem.Value);
            //BindVillages(ddlSubCounty.SelectedItem.Value);
            ////BindParishes(ddlSubCounty.SelectedItem.Value);
        }

        protected void ddlTribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClans(ddlTribe.SelectedItem.Value);
            updPnlClan.Update();
        }

        protected void BindClans(string tribeID)
        {
            ListItem lstItem = new ListItem(ddlClan.Items[0].Text, ddlClan.Items[0].Value);

            ddlClan.Items.Clear();
            if (ddlTribe.SelectedIndex > 0)
            {
                ClansBLL objMasterBLL = new ClansBLL();
                ClansList oClansList = new ClansList();
                ddlClan.DataTextField = "CLANNAME";
                ddlClan.DataValueField = "CLANID";
                oClansList = objMasterBLL.FetchClansList(Convert.ToInt32(tribeID));
                ddlClan.DataSource = oClansList;//objMasterBLL.FetchClansList(Convert.ToInt32(tribeID));
                ddlClan.DataBind();
            }
            ddlClan.Items.Insert(0, lstItem);
        }
        #endregion Bind Data

        /// <summary>
        /// to bind the GOUAllowance data from the database
        /// </summary>
        private void BindGouAllowanc()
        {
            GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();

            ddlGouAllowance.DataSource = GOUAllowanceBLLObj.GetGouAllowance();
            ddlGouAllowance.DataTextField = "GOUAllowanceCategory";
            ddlGouAllowance.DataValueField = "GOUALLOWANCECATEGORYID";
            ddlGouAllowance.DataBind();

        }

        #region Functional region
        protected void rdoIdentificationCardNo_CheckedChanged(object sender, EventArgs e)
        {
            CardTypeEnabled(false);
        }

        protected void rdoIdentificationCardYes_CheckedChanged(object sender, EventArgs e)
        {
            CardTypeEnabled(true);
        }

        private void CardTypeEnabled(bool blStatus)
        {
            if (rdoIdentificationCardYes.Checked == blStatus)
                ddlcardType.Enabled = blStatus;
            else
                ddlcardType.Enabled = !blStatus;
        }

        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Religion();
        }
        private void Religion()
        {
            if (ddlReligion.SelectedItem.Text.ToUpper() == "Other".ToUpper())// && ddlReligion.SelectedValue == "100" 
            {
                txtotherReligion.Enabled = true;
            }
            else
                txtotherReligion.Enabled = false;
        }
        #endregion

        protected void chkOverRide_CheckedChanged(object sender, EventArgs e)
        {
            Overide_Checkbox();
        }

        private void Overide_Checkbox()
        {
            if (chkOverRide.Checked)
            {
                tdOverideComments.Visible = true;
                 rfvOverideComments.Enabled = true;
                ImgRefresh.Enabled = false;
                ManSymbol.Visible = true;
            }
            else
            {
                rfvOverideComments.Enabled = false;
                 tdOverideComments.Visible = false;
                 ManSymbol.Visible = false;
                ImgRefresh.Enabled = true;
            }
        }

        protected void ImgRefresh_Click(object sender, ImageClickEventArgs e)
        {

            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.HhId = Convert.ToInt32(txtHouseHoldID.Text);
            objHouseHold.PercentageOccupied = Convert.ToDecimal(txtpercentage.Text);
            if (RdbtnCash.Checked == true)
            {
                objHouseHold.LandCompensation = "Cash";

            }
            else if (Rdbtninkind.Checked == true)
            {
                objHouseHold.LandCompensation = "In-Kind";

            }
            else if (RdbtnBoth.Checked == true)
            {
                objHouseHold.LandCompensation = "Both";

            }
            else if (RdbtnNotApplic.Checked == true)
            {
                objHouseHold.LandCompensation = "Not Applicable";

            }
            if (RdbtnHcash.Checked == true)
            {
                objHouseHold.HouseCompensation = "Cash";

            }
            else if (RdbtnHbtninkind.Checked == true)
            {
                objHouseHold.HouseCompensation = "In-Kind";

            }
            else if (RdbtnHBoth.Checked == true)
            {
                objHouseHold.HouseCompensation = "Both";

            }
            else if (RdbtnHNotApplic.Checked == true)
            {
                objHouseHold.HouseCompensation = "Not Applicable";

            }

            if (chkOverRide.Checked == true)
            {
                objHouseHold.OptiongroupId = Convert.ToInt32(ddloptionGroup.SelectedValue);
            }
            else
            {
                string landStatus = OccupationStatus.SelectedItem.Text;
                PAP_HouseholdBLL objPAPHH = new PAP_HouseholdBLL();
                if (rdnResident.SelectedItem.Text == "Yes")
                {
                    objHouseHold.Isresident = "Resident";

                }
                else if (rdnResident.SelectedItem.Text == "No")
                {
                    objHouseHold.Isresident = "Non-Resident";

                }
                int OptId = objPAPHH.GetHousaeHold(objHouseHold, landStatus);
                ddloptionGroup.ClearSelection();
                ddloptionGroup.SelectedValue = OptId.ToString();
                ddloptionGroup.Enabled = false;
                //objHouseHold.OptiongroupId = OptId;//Convert.ToInt32(ddloptionGroup.SelectedValue);
            }
        }


    }
}