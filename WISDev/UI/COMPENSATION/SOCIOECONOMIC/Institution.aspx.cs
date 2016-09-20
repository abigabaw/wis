using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using WIS_BusinessObjects.Collections;

namespace WIS
{
    public partial class Institution : System.Web.UI.Page
    {
        #region for page load
        /// <summary> 
        /// Set Page header,Call BindOptionGroups() to bind the Option Group from the database
        /// call BindGouAllowanc() to bind the GouAllowance from the database
        /// call BindDropDownDistrict() to bind the District names to the dropdown from the database
        /// call  BindPositions() to bind the Positions from the database
        /// call GetInstContactData() to get the Instruction Data from the database
        /// call   projectFrozen() to update the status of the project 
        /// to set the status of the Link Button lnkHHInstitution,lnkUPloadDoc,lnkUPloadPhoto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.Institution;
            caldpDateofBirth.Format = UtilBO.DateFormat;
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

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Institution Details";
                }
                
                #region UploadPhoto / Documents link
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

                string PhotoModule = "PAPINST";
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
                BindOptionGroups();
                BindGouAllowanc();
                BindDropDownDistrict();
                BindPositions();
                GetInstContactData();
                projectFrozen();
                txtPapUid.Attributes.Add("onchange", "setDirty();");
                txtName.Attributes.Add("onchange", "setDirty();");
                txtCapturedBy.Attributes.Add("onchange", "setDirty();");
                txtSurname.Attributes.Add("onchange", "setDirty();");
                txtPlots.Attributes.Add("onchange", "setDirty();");
                txtfirstname.Attributes.Add("onchange", "setDirty();");
                txtPlotReference.Attributes.Add("onchange", "setDirty();");
                txtTelephoneNo1.Attributes.Add("onchange", "setDirty();");


                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
               // lnkChangeRequest.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoc.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkViewPhoto.Attributes.Add("onclick", "isDirty = 0;");
                //lnkUPloadDoclist.Attributes.Add("onclick", "isDirty = 0;");
                ViewMasterCopy1.Attributes.Add("onclick", "isDirty = 0;");
                //getApprrequtStatusInstitution();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkHHInstitution.Visible = false;
                    ddlPaptype.Enabled = false;
                    lnkUPloadDoc.Visible = false;
                    lnkUPloadPhoto.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
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
                lnkHHInstitution.Visible = false;
                ddlPaptype.Enabled = false;
                lnkUPloadDoc.Visible = false;
                lnkUPloadPhoto.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
        }
        #endregion pageload

        #region Frozen
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string Frozen = Session["FROZEN"].ToString();
                if (Frozen == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    //lnkHHInstitution.Visible = true;
                    checkApprovalExitOrNot();
                }
                else
                {
                    lnkHHInstitution.Visible = false;
                }
            }
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusInstitution.Text = "";
            StatusInstitution.Visible = false; // used to display the Status if you send Request for change data
            // getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            //for checking Change Request Approver Exit or not
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
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHINS");
                lnkHHInstitution.Attributes.Add("onclick", paramChangeRequest);
                lnkHHInstitution.Visible = true;
            }
            else
            {
                lnkHHInstitution.Visible = false;
            }
            #endregion
            getApprrequtStatusInstitution();
           
        }

        public void ChangeRequestStatusInstitution()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHINS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusInstitution()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHINS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkHHInstitution.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusInstitution.Visible = true;
                    StatusInstitution.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkHHInstitution.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusInstitution.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkHHInstitution.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusInstitution.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
#endregion  

        #region for PAP Photo in Update panal
        //writen By Ramu.S
        /// <summary>
        /// to get the PAP image on HHID 
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
            //"~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + HHID.ToString();
            updimgPAPPhoto.Update();
        }
        #endregion
        /// <summary>
        ///  to get the Instruction Data from the database
        /// </summary>

        private void GetInstContactData()
        {
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHouseHoldData(HHID);
            if (objHouseHold != null)
            {
                txtHouseHoldID.Text = Convert.ToString(objHouseHold.HhId);
                txtName.Text = Convert.ToString(objHouseHold.InstitutionName);
                txtPlots.Text = Convert.ToString(objHouseHold.Noofplots);
                txtPlotReference.Text = Convert.ToString(objHouseHold.PlotReference);
                txtSurname.Text = Convert.ToString(objHouseHold.Surname);
                txtfirstname.Text = Convert.ToString(objHouseHold.Firstname);
                txtOthername.Text = Convert.ToString(objHouseHold.Othername);
                txtFullname.Text = txtSurname.Text + " " + txtfirstname.Text;
                txtPapUid.Text = Convert.ToString(objHouseHold.Papuid);

                if (objHouseHold.CapturedDate.Trim() != "")
                    dpCapturedDate.Text = Convert.ToDateTime(objHouseHold.CapturedDate).ToString(UtilBO.DateFormat);
                txtCapturedBy.Text = Convert.ToString(objHouseHold.CapturedBy);

                ddlGouAllowance.ClearSelection();
                if (ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()) != null)
                    ddlGouAllowance.Items.FindByValue(objHouseHold.GouStatus.ToString()).Selected = true;

                ddlUnderTakingPeriod.ClearSelection();
                if (ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()) != null)
                    ddlUnderTakingPeriod.Items.FindByValue(objHouseHold.UnderTakingPeriod.ToString()).Selected = true;

                rdlResident.ClearSelection();
                if (objHouseHold.Isresident == "No")
                {
                    rdlResident.Items[1].Selected = true;
                }
                else
                {
                    rdlResident.Items[0].Selected = true;
                }
                ddloptionGroup.SelectedValue = Convert.ToString(objHouseHold.OptiongroupId);

                ddlGender.ClearSelection();
                if (ddlGender.Items.FindByValue(objHouseHold.Sex) != null)
                    ddlGender.Items.FindByValue(objHouseHold.Sex).Selected = true;

                if (objHouseHold.DateofBirth.Trim() != "")
                    dpDateofBirth.Text = Convert.ToDateTime(objHouseHold.DateofBirth).ToString(UtilBO.DateFormat);

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(Convert.ToString(objHouseHold.District).ToUpper()) != null)
                    ddlDistrict.Items.FindByText(Convert.ToString(objHouseHold.District).ToUpper()).Selected = true;

                if (ddlDistrict.SelectedIndex > 0)
                {
                    BindCounties(ddlDistrict.SelectedItem.Value, ddlCounty);

                    if (Convert.ToString(objHouseHold.County) != "")
                    {
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(Convert.ToString(objHouseHold.County).ToUpper()) != null)
                            ddlCounty.Items.FindByText(Convert.ToString(objHouseHold.County).ToUpper()).Selected = true;
                    }
                }

                if (ddlCounty.SelectedIndex > 0)
                {
                    BindSubCounties(ddlCounty.SelectedItem.Value, ddlSubCounty);
                    if (Convert.ToString(objHouseHold.SubCounty) != "")
                    {
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(Convert.ToString(objHouseHold.SubCounty).ToUpper()) != null)
                            ddlSubCounty.Items.FindByText(Convert.ToString(objHouseHold.SubCounty).ToUpper()).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindVillages(ddlSubCounty.SelectedItem.Value, ddlVillage);
                    if (Convert.ToString(objHouseHold.Village) != "")
                    {
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(Convert.ToString(objHouseHold.Village).ToUpper()) != null)
                            ddlVillage.Items.FindByText(Convert.ToString(objHouseHold.Village).ToUpper()).Selected = true;
                    }
                    BindParish(ddlSubCounty.SelectedItem.Value, ddlParish);
                    if (Convert.ToString(objHouseHold.Village) != "")
                    {
                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()) != null)
                            ddlParish.Items.FindByText(Convert.ToString(objHouseHold.Parish).ToUpper()).Selected = true;
                    }
                }
                PAP_InstitutionBLL objInstitutionBLL = new PAP_InstitutionBLL();
                PAP_InstitutionList objPAP_InstitutionList = objInstitutionBLL.GetInstContactByHHID(HHID);
                if (objPAP_InstitutionList.Count > 0)
                {
                    ddlPosition.ClearSelection();
                    ddlPosition.SelectedValue = (Convert.ToString(objPAP_InstitutionList[0].POSITIONID));

                    ddlCPDistrict.ClearSelection();
                    if (ddlCPDistrict.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_DISTRICT).ToUpper()) != null)
                        ddlCPDistrict.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_DISTRICT).ToUpper()).Selected = true;

                    if (ddlCPDistrict.SelectedIndex > 0)
                    {
                        BindCounties(ddlCPDistrict.SelectedItem.Value, ddlCPCounty);

                        if (Convert.ToString(objHouseHold.County) != "")
                        {
                            ddlCPCounty.ClearSelection();
                            if (ddlCPCounty.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_COUNTY).ToUpper()) != null)
                                ddlCPCounty.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_COUNTY).ToUpper()).Selected = true;
                        }
                    }

                    if (ddlCPCounty.SelectedIndex > 0)
                    {
                        BindSubCounties(ddlCPCounty.SelectedItem.Value, ddlCPSubCounty);
                        uplCPSubCounty.Update();
                        if (Convert.ToString(objHouseHold.SubCounty) != "")
                        {
                            ddlCPSubCounty.ClearSelection();
                            if (ddlCPSubCounty.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_SUBCOUNTY).ToUpper()) != null)
                                ddlCPSubCounty.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_SUBCOUNTY).ToUpper()).Selected = true;
                        }
                    }


                    if (ddlCPSubCounty.SelectedIndex > 0)
                    {
                        BindVillages(ddlCPSubCounty.SelectedItem.Value, ddlCPVillage);
                        uplCPVillage.Update();
                        if (Convert.ToString(objHouseHold.Village) != "")
                        {
                            ddlCPVillage.ClearSelection();
                            if (ddlCPVillage.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_VILLAGE).ToUpper()) != null)
                                ddlCPVillage.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_VILLAGE).ToUpper()).Selected = true;
                        }
                        BindParish(ddlCPSubCounty.SelectedItem.Value, ddlCPParish);
                        uplCPParish.Update();
                        if (Convert.ToString(objHouseHold.Village) != "")
                        {
                            ddlCPParish.ClearSelection();
                            if (ddlCPParish.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_PARISH).ToUpper()) != null)
                                ddlCPParish.Items.FindByText(Convert.ToString(objPAP_InstitutionList[0].CONT_PARISH).ToUpper()).Selected = true;
                        }
                    }
                    txtTelephoneNo1.Text = Convert.ToString(objPAP_InstitutionList[0].CONTACTPHONE1);
                    txtTelephoneNo2.Text = Convert.ToString(objPAP_InstitutionList[0].CONTACTPHONE2);
                }

                getImage(HHID);
                //imgPAPPhoto.ImageUrl = "~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=" + HHID.ToString();
            }
        
        }
        /// <summary>
        /// to get the  data of the PAP type from the database on change in the index of the dropdown
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
        /// to get the  data of the County from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            if (ddl1.ID == "ddlCounty")
            {
                BindSubCounties(ddlCounty.SelectedItem.Value, ddlSubCounty);
                BindVillages(ddlSubCounty.SelectedItem.Value, ddlVillage);
                BindParish(ddlSubCounty.SelectedItem.Value, ddlParish);                
                uplParish.Update();
                uplVillage.Update();
            }
            else
            {
                BindSubCounties(ddlCPCounty.SelectedItem.Value, ddlCPSubCounty);
                BindVillages(ddlCPSubCounty.SelectedItem.Value, ddlCPVillage);
                BindParish(ddlCPSubCounty.SelectedItem.Value, ddlCPParish);
                uplCPParish.Update();
                uplCPVillage.Update();
            }
        }
        /// <summary>
        /// to get the  data of the SubCounty from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            if (ddl1.ID == "ddlSubCounty")
            {
                BindVillages(ddlSubCounty.SelectedItem.Value, ddlVillage);
                BindParish(ddlSubCounty.SelectedItem.Value, ddlParish);
                uplParish.Update();
                uplVillage.Update();
            }
            else
            {
                BindVillages(ddlCPSubCounty.SelectedItem.Value, ddlCPVillage);
                BindParish(ddlCPSubCounty.SelectedItem.Value, ddlCPParish);
                uplCPParish.Update();
                uplCPVillage.Update();
            }
        }

        #region Save / clear fileds
        /// <summary>
        /// To save the data to the database 
        /// and Clear the datafields by calling  ClearDetails() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        [WebMethod]
        public void ReCache()
        {
            HouseholdSummaryCache.CachePAPData(Session["HH_ID"].ToString());
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangeRequestStatusInstitution();

            PAP_InstitutionBO objInstitution = new PAP_InstitutionBO();
            objInstitution.HHID = Convert.ToInt32(Session["HH_ID"]);
            objInstitution.Paptype = ddlPaptype.SelectedValue;
            objInstitution.InstitutionNameIN = txtName.Text;
            objInstitution.DistrictIN = ddlDistrict.SelectedItem.Text;
            objInstitution.CountyIN = ddlCounty.SelectedItem.Text;
            objInstitution.SubCountyIN = ddlSubCounty.SelectedItem.Text;
            objInstitution.ParishIN = ddlParish.SelectedItem.Text;
            objInstitution.VillageIN = ddlVillage.SelectedItem.Text;
            objInstitution.OptionGroupIdIN = Convert.ToInt32(ddloptionGroup.SelectedItem.Value);
            objInstitution.NoofplotsIN = Convert.ToInt32(txtPlots.Text);
            objInstitution.PlotReferenceIN = txtPlotReference.Text;
            objInstitution.DateofBirthIN =Convert.ToDateTime(dpDateofBirth.Text.ToString());
            objInstitution.IsResidentIN = rdlResident.SelectedItem.Text;
            objInstitution.SexIN = ddlGender.SelectedItem.Text;
            objInstitution.SurnameIN = txtSurname.Text;
            objInstitution.FirstnameIN = txtfirstname.Text;
            objInstitution.OthernameIN = txtOthername.Text;
            objInstitution.UpdatedbyIN = Convert.ToInt32(Session["USER_ID"]);
            objInstitution.POSITIONID = Convert.ToInt32(ddlPosition.SelectedValue);
            objInstitution.CONT_DISTRICT = ddlCPDistrict.SelectedItem.Text;
            objInstitution.CONT_COUNTY = ddlCPCounty.SelectedItem.Text;
            objInstitution.CONT_SUBCOUNTY = ddlCPSubCounty.SelectedItem.Text;
            objInstitution.CONT_PARISH = ddlCPParish.SelectedItem.Text;
            objInstitution.CONT_VILLAGE = ddlCPVillage.SelectedItem.Text;
            objInstitution.Gouallowance = ddlGouAllowance.SelectedValue;
            objInstitution.Undertakingperiod = ddlUnderTakingPeriod.SelectedValue;
            objInstitution.CONTACTPHONE1 = txtTelephoneNo1.Text;
            objInstitution.CONTACTPHONE2 = txtTelephoneNo2.Text;
            objInstitution.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
            objInstitution.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
            objInstitution.Papuid = txtPapUid.Text.Trim();
            if (dpCapturedDate.Text.ToString().Trim() != "" && dpCapturedDate.Text.ToString().Trim() != "1/1/0001")
                objInstitution.CapturedDate = dpCapturedDate.Text.ToString();
            objInstitution.CapturedBy = txtCapturedBy.Text;

            PAP_InstitutionBLL objInstBll = new PAP_InstitutionBLL();
            string message = objInstBll.UpdateInstitutionDetails(objInstitution);

            //Edwin: 19SEP2016 Reload Pap Details
            ReCache();

            txtFullname.Text = txtSurname.Text + " " + txtfirstname.Text;
            projectFrozen();
            checkApprovalExitOrNot();
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        protected void ClearDetails()
        {
            txtCapturedBy.Text = "";
            dpCapturedDate.Text = "";
            dpDateofBirth.Text = "";
            txtName.Text = "";
            txtPlots.Text = "";
            txtPapUid.Text = "";
            ddloptionGroup.ClearSelection();
            ddloptionGroup.Items[0].Selected = true;
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
            rdlResident.ClearSelection();
            txtfirstname.Text = "";
            txtSurname.Text = "";
            txtOthername.Text = "";
            txtFullname.Text = "";
            ddlPosition.ClearSelection();
            ddlPosition.Items[0].Selected = true;
            ddlCPDistrict.ClearSelection();
            ddlCPDistrict.Items[0].Selected = true;
            ddlCPCounty.ClearSelection();
            ddlCPCounty.Items[0].Selected = true;
            ddlCPSubCounty.ClearSelection();
            ddlCPSubCounty.Items[0].Selected = true;
            ddlCPParish.ClearSelection();
            ddlCPParish.Items[0].Selected = true;
            ddlCPVillage.ClearSelection();
            ddlCPVillage.Items[0].Selected = true;
            txtTelephoneNo1.Text = "";
            txtTelephoneNo2.Text = "";
            txtPlotReference.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + dpDateofBirth.ClientID + "');", true);
        }
        #endregion
        /// <summary>
        /// to get the  data of the District from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (DropDownList)sender;
            if (ddl1.ID == "ddlDistrict")
            {
                BindCounties(ddlDistrict.SelectedItem.Value, ddlCounty);
                BindSubCounties(ddlCounty.SelectedItem.Value, ddlSubCounty);
                uplSubCounty.Update();
                BindVillages(ddlSubCounty.SelectedItem.Value, ddlVillage);
                BindParish(ddlSubCounty.SelectedItem.Value, ddlParish);
                uplVillage.Update();
                uplParish.Update();
            }
            else
            {
                BindCounties(ddlCPDistrict.SelectedItem.Value, ddlCPCounty);
                BindSubCounties(ddlCPCounty.SelectedItem.Value, ddlCPSubCounty);
                uplCPSubCounty.Update();
                BindVillages(ddlCPSubCounty.SelectedItem.Value, ddlCPVillage);
                BindParish(ddlCPSubCounty.SelectedItem.Value, ddlCPParish);
                uplCPVillage.Update();
                uplCPParish.Update();
            }
        }
        /// <summary>
        ///  to get the  data of the Parish from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="subCountyID"></param>
        /// <param name="dd1"></param>

        private void BindParish(string subCountyID, DropDownList dd1)
        {
            ListItem firstListItem = new ListItem(dd1.Items[0].Text, dd1.Items[0].Value);
            dd1.Items.Clear();

            if (subCountyID != "0")
            {
                dd1.DataSource = (new MasterBLL()).LoadParishData(subCountyID);
                dd1.DataTextField = "ParishName";
                dd1.DataValueField = "ParishId";
                dd1.DataBind();
            }
            dd1.Items.Insert(0, firstListItem);
        }
        /// <summary>
        ///  to get the  data of the Villages from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="subCountyID"></param>
        /// <param name="dd1"></param>

        private void BindVillages(string subCountyID, DropDownList dd1)
        {
            ListItem firstListItem = new ListItem(dd1.Items[0].Text, dd1.Items[0].Value);
            dd1.Items.Clear();

            if (subCountyID != "0")
            {
                dd1.DataSource = (new MasterBLL()).LoadVillageData(subCountyID);
                dd1.DataTextField = "VillageName";
                dd1.DataValueField = "VillageID";
                dd1.DataBind();
            }
            dd1.Items.Insert(0, firstListItem);
        }
        /// <summary>
        ///  to get the  data of the SubCounty from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="countyID"></param>
        /// <param name="dd1"></param>

        private void BindSubCounties(string countyID, DropDownList dd1)
        {
            ListItem firstListItem = new ListItem(dd1.Items[0].Text, dd1.Items[0].Value);
            dd1.Items.Clear();

            if (countyID != "0")
            {
                dd1.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                dd1.DataTextField = "SubCountyName";
                dd1.DataValueField = "SubCountyID";
                dd1.DataBind();
            }
            dd1.Items.Insert(0, firstListItem);
        }
        /// <summary>
        ///  to get the  data of the Counties from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="districtID"></param>
        /// <param name="dd1"></param>

        private void BindCounties(string districtID, DropDownList dd1)
        {
            ListItem firstListItem = new ListItem(dd1.Items[0].Text, dd1.Items[0].Value);
            dd1.Items.Clear();
            if (districtID != "0")
            {
                dd1.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                dd1.DataTextField = "CountyName";
                dd1.DataValueField = "CountyID";
                dd1.DataBind();
            }

            dd1.Items.Insert(0, firstListItem);
        }
        /// <summary>
        /// to get the District Name from the database and bind it to the DropDownDistrict
        /// </summary>

        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();

            // for contact person
            ddlCPDistrict.DataTextField = "DistrictName";
            ddlCPDistrict.DataValueField = "DistrictID";
            ddlCPDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlCPDistrict.DataBind();
        }
        /// <summary>
        /// to get the GouAllowance from the database and bind it to the DropDown
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
        /// to open New Window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

        }
        /// <summary>
        /// To get the data from the database and bind it to the dropdown ddloptionGroup
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
        /// To get the data from the database and bind it to the dropdown ddlPosition
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
    }
}