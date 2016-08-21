using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.WebControls;
using System.Text;

namespace WIS
{
    public partial class LandInfoRespondentsOn : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindDropDownDistrict() to bind the district names to the dropdownlist
        /// call GetLandInfo() to bind the Land Info to the dropDown
        /// Call DisplayPanel() to set the status of the panel
        /// to set the status of the link button lnkLandInfoResOn
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.OnAffectedLand;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.LivingonAffectedLand;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
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
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Land Information - Respondents living on Affected Plot";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                txtYesLandHold.Enabled = false;
                BindDropDownDistrict();
                GetLandInfo();
                DisplayPanel();
                projectFrozen();
                chkFamilyMember.Attributes.Add("onclick", string.Format("EnableBuried(this,'{0}');", txtBuried.ClientID));

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkLandInfoResOn.Visible = false;
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
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkLandInfoResOn.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
        }
        /// <summary>
        /// to check the status like Frozen/Approval/Decline/Pending 
        /// </summary>
        #region Frozen / Approval / Decline / Pending
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    checkApprovalExitOrNot();
                }
            }
        }
        /// <summary>
        /// to get the status of the change request for land info ResOn
        /// </summary>
        public void ChangeRequestStatusLandInfoResOn()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HLION";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusLandInfoResOn.Text = "";
            StatusLandInfoResOn.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HLION");
                lnkLandInfoResOn.Attributes.Add("onclick", paramChangeRequest);
                lnkLandInfoResOn.Visible = true;
            }
            else
            {
                lnkLandInfoResOn.Visible = false;
            }
            #endregion
            getApprrequtStatusLandInfoResOn();

        }
        /// <summary>
        /// to get the status of the Approvar  request for land info ResOn
        /// </summary>

        public void getApprrequtStatusLandInfoResOn()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HLION";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkLandInfoResOn.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusLandInfoResOn.Visible = true;
                    StatusLandInfoResOn.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkLandInfoResOn.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusLandInfoResOn.Visible = false;
                    StatusLandInfoResOn.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkLandInfoResOn.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusLandInfoResOn.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

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


        #region Dropdowns & Checkbox Events
        /// <summary>
        /// to get the subcounty data from the database on selection of county 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            BindVillages(ddlSubCounty.SelectedItem.Value);
        }
        /// <summary>
        /// to get the Villages data from the database on selection of Subcounty 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
        }
        /// <summary>
        /// to get the District  data from the database on selection of counties
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
        }
        /// <summary>
        /// to check the status of the checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkLandHold_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLandHold.Checked == true)
            {
                txtYesLandHold.Enabled = true;
            }
            else if (chkLandHold.Checked == false)
            {
                txtYesLandHold.Text = "";
                txtYesLandHold.Enabled = false;
            }
        }
        #endregion Dropdowns & Checkbox Events

        #region Bind Methods
        /// <summary>
        /// to bind the data from the database to fill the district dropdown ddlDistrict
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
        /// to bind the data from the database to fill the county dropdown ddlCounty
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
        /// to bind the data from the database to fill the subcounty dropdown ddlSubCounty
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
        /// to bind the data from the database to fill the Villages dropdown ddlVillage
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
        #endregion Bind Methods

        #region Button Events
        /// <summary>
        /// to save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangeRequestStatusLandInfoResOn();

            LandLivingOnBO objLand = new LandLivingOnBO();
            try
            {
                string message = string.Empty;
                objLand.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                objLand.WhereLivedBefore = txtLivingbefore.Text.Trim();
                //objLand.PreferredVillege = txtVillage.Text.Trim();

                objLand.District = ddlDistrict.SelectedItem.Text;
                objLand.County = ddlCounty.SelectedItem.Text;
                objLand.Subcounty = ddlSubCounty.SelectedItem.Text;
                objLand.Village = ddlVillage.SelectedItem.Text;
                if (chkLandHold.Checked == true)
                {
                    objLand.IsOtherLandHold = "Yes";
                    objLand.WhichLandHold = txtYesLandHold.Text.Trim();
                    txtYesLandHold.Enabled = true;
                }
                else if (chkLandHold.Checked == false)
                {
                    objLand.IsOtherLandHold = "No";
                    objLand.WhichLandHold = "";
                    txtYesLandHold.Enabled = false;
                }
                if (chkTransport.Checked == true)
                {
                    objLand.RequireTransport = "Yes";
                }
                else if (chkTransport.Checked == false)
                {
                    objLand.RequireTransport = "No";
                }
                if (chkRelative.Checked == true)
                {
                    objLand.MovenearRelatives = "Yes";
                }
                else if (chkRelative.Checked == false)
                {
                    objLand.MovenearRelatives = "No";
                }

                if (chkFamilyMember.Checked == true)
                {
                    objLand.BuriedFamilyMemonLand = "Yes";
                    objLand.HowmanyBuried = txtBuried.Text.Trim();
                    txtBuried.Enabled = true;
                }
                else if (chkFamilyMember.Checked == false)
                {
                    objLand.BuriedFamilyMemonLand = "No";
                    objLand.HowmanyBuried = "";
                    txtBuried.Enabled = false;
                }

                if (chkAncestors.Checked == true)
                {
                    objLand.RelocateAncestors = "Yes";
                }
                else if (chkAncestors.Checked == false)
                {
                    objLand.RelocateAncestors = "No";
                }
                objLand.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                SurveyBLL objSurveyBLL = new SurveyBLL();

                message = objSurveyBLL.AddLandLivingOn(objLand);

                if (message == "I")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data saved successfully');", true);
                }
                else if (message == "U")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data updated successfully');", true);
                }
                projectFrozen();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// to clear the data fields by calling  Clear() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion Button Events

        #region Method
        /// <summary>
        /// to get the Land info fro the database
        /// </summary>
        private void GetLandInfo()
        {
            SurveyBLL objSurveyBLL = new SurveyBLL();
            LandLivingOnBO objSurvey = objSurveyBLL.GetLandLivingOnByHHID(Convert.ToInt32(Session["HH_ID"]));

            if (objSurvey != null)
            {
                txtLivingbefore.Text = objSurvey.WhereLivedBefore;
                //txtVillage.Text = objSurvey.PreferredVillege;
                if (objSurvey.IsOtherLandHold == "Yes")
                {
                    chkLandHold.Checked = true;
                    txtYesLandHold.Text = objSurvey.WhichLandHold;
                    txtYesLandHold.Enabled = true;
                }
                else if (objSurvey.IsOtherLandHold == "No")
                {
                    chkLandHold.Checked = false;
                    txtYesLandHold.Text = "";
                    txtYesLandHold.Enabled = false;
                }

                if (objSurvey.RequireTransport == "Yes")
                {
                    chkTransport.Checked = true;
                }
                else if (objSurvey.RequireTransport == "No")
                {
                    chkTransport.Checked = false;
                }

                if (objSurvey.MovenearRelatives == "Yes")
                {
                    chkRelative.Checked = true;
                }
                else if (objSurvey.MovenearRelatives == "No")
                {
                    chkRelative.Checked = false;
                }

                if (objSurvey.BuriedFamilyMemonLand == "Yes")
                {
                    chkFamilyMember.Checked = true;
                    txtBuried.Enabled = true;
                }
                else if (objSurvey.BuriedFamilyMemonLand == "No")
                {
                    chkFamilyMember.Checked = false;
                    txtBuried.Enabled = false;
                }

                txtBuried.Text = objSurvey.HowmanyBuried;

                if (objSurvey.RelocateAncestors == "Yes")
                {
                    chkAncestors.Checked = true;
                }
                else if (objSurvey.RelocateAncestors == "No")
                {
                    chkAncestors.Checked = false;
                }

                ddlDistrict.ClearSelection();
                if (ddlDistrict.Items.FindByText(objSurvey.District) != null)
                    ddlDistrict.Items.FindByText(objSurvey.District).Selected = true;

                if (ddlDistrict.SelectedIndex > 0)
                {
                    BindCounties(ddlDistrict.SelectedItem.Value);

                    if (Convert.ToString(objSurvey.County) != "")
                    {
                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(objSurvey.County) != null)
                            ddlCounty.Items.FindByText(objSurvey.County).Selected = true;
                    }
                }

                if (ddlCounty.SelectedIndex > 0)
                {
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    if (Convert.ToString(objSurvey.Subcounty) != "")
                    {
                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(objSurvey.Subcounty) != null)
                            ddlSubCounty.Items.FindByText(objSurvey.Subcounty).Selected = true;
                    }
                }

                if (ddlSubCounty.SelectedIndex > 0)
                {
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    if (Convert.ToString(objSurvey.Village) != "")
                    {
                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(objSurvey.Village) != null)
                            ddlVillage.Items.FindByText(objSurvey.Village).Selected = true;
                    }
                }

            }
        }
        /// <summary>
        /// to clear the data fields 
        /// </summary>
        private void Clear()
        {
            txtLivingbefore.Text = "";
            chkLandHold.Checked = false;
            txtYesLandHold.Enabled = false;
            txtYesLandHold.Text = "";
            chkTransport.Checked = false;
            chkRelative.Checked = false;
            chkFamilyMember.Checked = false;
            txtBuried.Text = "";
            chkAncestors.Checked = false;

            ListItem lstItem = null;
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
        }
        /// <summary>
        /// to set the status of the panel
        /// </summary>
        private void DisplayPanel()
        {
            if (Session["HH_ID"] != null)
            {
                int householdID = Convert.ToInt32(Session["HH_ID"]);
                PAP_HouseholdBLL PAP_HouseholdBLLobj = new PAP_HouseholdBLL();
                string PDP_Present = PAP_HouseholdBLLobj.IsPDP(householdID);

                if (PDP_Present.ToUpper() == "Y")
                {
                    pnlButtons.Visible = true;
                    lblMessage.Text = string.Empty;
                    pnlLivingOnEffectedLand.Enabled = true;
                }
                else
                {
                    pnlButtons.Visible = false;
                    lblMessage.Text = "These details are enabled only for PDP's";
                    pnlLivingOnEffectedLand.Enabled = false;
                }
            }
        }
        #endregion
    }
}