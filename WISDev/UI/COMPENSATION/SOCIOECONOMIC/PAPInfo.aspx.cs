using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class PAPInfo : System.Web.UI.Page
    {
        /// <summary>
        ///  Set Page header,Call BindDistricts() to get the data from the database and bind it to the DropDownList
        ///  call BindSegments() to get the Segment data from the database and bind it to the dropDownList
        ///  call  BindRepresentation() to get the Representation data from the database and bind it to the dropDownList
        ///  to set the status of the Link Button lnkLogout
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.PAPInfo;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.StakeholderDetails;
            calDateOfBirth.Format = UtilBO.DateFormat;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Stakeholder Information";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                BindDistricts();
                BindSegments();
                BindRepresentation();
                GetStakeholderDetails(Convert.ToInt32(Session["HH_ID"]));

                ddlSegment.Attributes.Add("onchange", "isDirty = 0;");
                txtStakeholder.Attributes.Add("onchange", "setDirtyText();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
                GetProjectDetails();
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }

               //Add by Ramu
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
                btnSave.Visible = false;
                btnClear.Visible = false;
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
        /// <summary>
        /// to check the status of the Frozen / Approval / Decline / Pending
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    checkApprovalExitOrNot(); //Add by Ramu
                    getApprrequtStatusSocioEconomy();//Add by Ramu
                }
            }
        }
        /// <summary>
        /// to get the satus of the Change request for SocioEconomy
        /// </summary>
        public void ChangeRequestStatusSocioEconomy()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to check approvar Exist or not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusPAPINFO.Text = "";
            StatusPAPINFO.Visible = false; // used to display the Status if you send Request for change data
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

            if (objWorkFlowBO!= null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-SE");
                lnkPAPINFO.Attributes.Add("onclick", paramChangeRequest);
                lnkPAPINFO.Visible = true;
            }
            else
            {
                lnkPAPINFO.Visible = false;
            }
            #endregion
            getApprrequtStatusSocioEconomy();

        }
        /// <summary>
        /// to get the status of the approvar for SocioEconomy
        /// </summary>
        public void getApprrequtStatusSocioEconomy()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPAPINFO.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusPAPINFO.Visible = true;
                    StatusPAPINFO.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPAPINFO.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusPAPINFO.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPAPINFO.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusPAPINFO.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// to bind the segments
        /// </summary>
        private void BindSegments()
        {
            ddlSegment.DataSource = (new ProjectBLL()).GetProjectSegment(Convert.ToInt32(Session["PROJECT_ID"]));
            ddlSegment.DataTextField = "SEGMENTNAME";
            ddlSegment.DataValueField = "PROJECTSEGMENTID";
            ddlSegment.DataBind();
        }
        /// <summary>
        /// to bind the Representation
        /// </summary>
        private void BindRepresentation()
        {
            ddlRepresentation.DataSource = (new MasterBLL()).LoadRepresentationData();
            ddlRepresentation.DataTextField = "RepresentationName";
            ddlRepresentation.DataValueField = "RepresentationID";
            ddlRepresentation.DataBind();
        }
        /// <summary>
        /// To Get Segment Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypeOfLineDetails(int.Parse(ddlSegment.SelectedValue));
        }

        /// <summary>
        /// to get the StakeHolder Name
        /// </summary>
        /// <param name="householdID"></param>
        private void GetStakeholderDetails(int householdID)
        {
            PAP_StakeholderBO objStakeHolder = (new PAP_StakeholderBLL()).GetStakeholderByID(householdID);

            if (objStakeHolder != null)
            {
                objStakeHolder.HouseHoldID = householdID;
                txtStakeholder.Text = objStakeHolder.StakeholderName;
                ddlRepresentation.ClearSelection();
                if (ddlRepresentation.Items.FindByValue(objStakeHolder.Representation) != null)
                    ddlRepresentation.Items.FindByValue(objStakeHolder.Representation).Selected = true;
                txtResidentialAddress.Text = objStakeHolder.ResidentialAddress;
                txtPostalAddress.Text = objStakeHolder.PostalAddress;
                txtTelephoneNo.Text = objStakeHolder.TelephoneNo;

                if (objStakeHolder.DateOfSurvey != DateTime.MinValue)
                    dpSurveyDate.Text = objStakeHolder.DateOfSurvey.ToString(UtilBO.DateFormat);

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(objStakeHolder.District.ToUpper()) != null)
                {
                    ddlDistrict.Items.FindByText(objStakeHolder.District.ToUpper()).Selected = true;
                }

                BindCounties(ddlDistrict.SelectedItem.Value);
                if (ddlCounty.Items.FindByText(objStakeHolder.County.ToUpper()) != null)
                    ddlCounty.Items.FindByText(objStakeHolder.County.ToUpper()).Selected = true;

                BindSubCounties(ddlCounty.SelectedItem.Value);
                if (ddlSubCounty.Items.FindByText(objStakeHolder.Subcounty.ToUpper()) != null)
                    ddlSubCounty.Items.FindByText(objStakeHolder.Subcounty.ToUpper()).Selected = true;

                BindParishes(ddlSubCounty.SelectedItem.Value);
                if (ddlParish.Items.FindByValue(objStakeHolder.Parish.ToUpper()) != null)
                    ddlParish.Items.FindByValue(objStakeHolder.Parish.ToUpper()).Selected = true;

                BindVillages(ddlSubCounty.SelectedItem.Value);
                if (ddlVillage.Items.FindByText(objStakeHolder.Village.ToUpper()) != null)
                    ddlVillage.Items.FindByText(objStakeHolder.Village.ToUpper()).Selected = true;

                BindParishes(ddlSubCounty.SelectedItem.Value);
                if (ddlParish.Items.FindByText(objStakeHolder.parish.ToUpper()) != null)
                    ddlParish.Items.FindByText(objStakeHolder.parish.ToUpper()).Selected = true;

                ddlSegment.ClearSelection();
                if (ddlSegment.Items.FindByValue(objStakeHolder.SegmentID.ToString()) != null)
                {
                    ddlSegment.Items.FindByValue(objStakeHolder.SegmentID.ToString()).Selected = true;
                    LoadTypeOfLineDetails(int.Parse(ddlSegment.SelectedValue));
                }
            }

            objStakeHolder = null;
        }
        /// <summary>
        /// to get the Type of Line in Deatails
        /// </summary>
        /// <param name="segmentID"></param>
        private void LoadTypeOfLineDetails(int segmentID)
        {
            TypeOfLineBO objTypeOfLineBO = GetTypeOfLineDetails(segmentID);

            if (objTypeOfLineBO != null)
            {
                lblTypeOfLine.Text = objTypeOfLineBO.TypeOfLine + " kV";
                lblRightOfWay.Text = objTypeOfLineBO.Rightofwaymeasurement + " Metres";
                lblWayleave.Text = objTypeOfLineBO.Wayleavemeasurement + " Metres";
            }
            else
            {
                lblTypeOfLine.Text = "";
                lblRightOfWay.Text = "";
                lblWayleave.Text = "";
            }
        }
        /// <summary>
        /// To get the Project Deatails
        /// </summary>
        private void GetProjectDetails()
        {
            ProjectBO oProjectBO = new ProjectBO();
            ProjectBLL oProjectBLL = new ProjectBLL();
            int ProjectID = 0;
            if (Session["PROJECT_ID"] != null)
            {
                ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                oProjectBO = oProjectBLL.GetProjectByProjectID(ProjectID);
                hdnProjectStartDate.Value = oProjectBO.projectStartDate.ToString("dd/MM/yyyy");
                hdnProjectEndDate.Value = oProjectBO.ProjectEndDate.ToString("dd/MM/yyyy");
            }

        }
        #endregion Load Methods

        #region Save & Clear Buttons
        /// <summary>
        /// to save the data to the database and 
        /// clear the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangeRequestStatusSocioEconomy();
            PAP_StakeholderBO objStakeHolder = new PAP_StakeholderBO();

            objStakeHolder.HouseHoldID = Convert.ToInt32(Session["HH_ID"]);
            objStakeHolder.StakeholderName = txtStakeholder.Text.Trim();
            objStakeHolder.Representation = ddlRepresentation.SelectedItem.Value;
            objStakeHolder.ResidentialAddress = txtResidentialAddress.Text.Trim();
            objStakeHolder.PostalAddress = txtPostalAddress.Text.Trim();
            objStakeHolder.TelephoneNo = txtTelephoneNo.Text.Trim();

            if (dpSurveyDate.Text.Trim().Length > 0 && Convert.ToDateTime(dpSurveyDate.Text) != DateTime.MinValue)
                objStakeHolder.DateOfSurvey = Convert.ToDateTime(dpSurveyDate.Text);

            objStakeHolder.District = ddlDistrict.SelectedItem.Text;
            objStakeHolder.County = ddlCounty.SelectedItem.Text;
            objStakeHolder.Subcounty = ddlSubCounty.SelectedItem.Text;
            objStakeHolder.Parish = ddlParish.SelectedItem.Text;
            objStakeHolder.Village = ddlVillage.SelectedItem.Text;
            objStakeHolder.SegmentID = Convert.ToInt32(ddlSegment.SelectedItem.Value);
            objStakeHolder.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
            objStakeHolder.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

            (new PAP_StakeholderBLL()).UpdateStakeholder(objStakeHolder);
            projectFrozen();
            

            LoadTypeOfLineDetails(Convert.ToInt32(ddlSegment.SelectedItem.Value));

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Saved", "ShowSaveMessage('');", true);
            //ddlSegment.Attributes.Add("onchange", "Segment_IndexChanged(this);");
        }
        /// <summary>
        /// to clear the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            dpSurveyDate.Text = ""; //new DateTime(0).ToString(UtilBO.DateFormat);
            txtStakeholder.Text = "";
            ddlRepresentation.ClearSelection();
            txtResidentialAddress.Text = "";
            txtPostalAddress.Text = "";
            txtTelephoneNo.Text = "";
            lblTypeOfLine.Text = "";
            lblRightOfWay.Text = "";
            lblWayleave.Text = "";
            //dpSurveyDate.Text = "";
            ddlDistrict.ClearSelection();
            ddlCounty.ClearSelection();
            ddlSubCounty.ClearSelection();
            ddlParish.ClearSelection();
            ddlVillage.ClearSelection();
            ddlSegment.ClearSelection();
           
        }
        #endregion Save & Clear Buttons

        #region Location Bind
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            BindParishes(ddlSubCounty.SelectedItem.Value);
        }

        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
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

        private void BindParishes(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);

            ddlParish.Items.Clear();

            if (subCounty != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCounty);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishID";
                ddlParish.DataBind();
            }

            ddlParish.Items.Insert(0, firstListItem);
        }

        private void BindVillages(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);

            ddlVillage.Items.Clear();

            if (subCounty != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }

            ddlVillage.Items.Insert(0, firstListItem);
        }
        #endregion Location Bind

        #region WebService
        [System.Web.Services.WebMethod]
        public static TypeOfLineBO GetTypeOfLineDetails(int segmentID)
        {
            ProjectBLL objProjectBLL = new ProjectBLL();

            SegmentBO objSegment = objProjectBLL.GetProjectSegmentByID(segmentID);
            TypeOfLineBO objTypeOfLine = null;

            if (objSegment != null)
            {
                TypeOfLineBLL objTypeOfLineBLL = new TypeOfLineBLL();
                objTypeOfLine = objTypeOfLineBLL.GetLineTypebyID(objSegment.LineTypeID);
            }

            return objTypeOfLine;
        }
        #endregion WebService
    }
}