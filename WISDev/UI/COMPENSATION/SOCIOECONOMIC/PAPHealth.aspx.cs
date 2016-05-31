using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class PAPHealth : System.Web.UI.Page  
    {
        /// <summary>
        /// to Set Page header,Call BindDiseases() to get the all diease data from the database
        /// call BindDisabilities() to bind the disability data from the database to the dropdown
        /// call BindDisabilityGrid to bind the Disability data from the database by HHId
        /// call GetHealthCenter() to get the HealthCenter Data from the database 
        /// call BindHIVContracted() to get the HIV Contracted Data from the database
        /// call GetHealthInfo() to get the health info records from the database
        /// set the status of the Link Button lnkPAPHealthDisability,lnkPAPHealthInfo
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.Health;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.HealthCareDetails;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.DisabilityDetails;

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
                chkUsedByFamily.Attributes.Add("onclick", string.Format("EnableBuried(this,'{0}');", txtNonUseReason.ClientID));
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Health Information";

                ViewState["HEALTH_ID"] = 0;

                BindDiseases();
                BindDisabilities();
                BindDisabilityGrid();                
                GetHealthCenter();
                BindHIVContracted();
                GetHealthInfo();
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkPAPHealthDisability.Visible = false;
                    lnkPAPHealthInfo.Visible = false;
                    btnSaveDisability.Visible = false;
                    btnClearDisability.Visible = false;
                    btnSaveHealthInfo.Visible = false;
                    btnClearHealthInfo.Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 1].Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 2].Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 3].Visible = false;
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
                lnkPAPHealthDisability.Visible = false;
                lnkPAPHealthInfo.Visible = false;
                btnSaveDisability.Visible = false;
                btnClearDisability.Visible = false;
                btnSaveHealthInfo.Visible = false;
                btnClearHealthInfo.Visible = false;
                grdDisabilities.Columns[3].Visible = false;
                grdDisabilities.Columns[4].Visible = false;
                grdDisabilities.Columns[5].Visible = false;
            }
        }
        /// <summary>
        /// to bind the HIV Contracted Data
        /// </summary>
        private void BindHIVContracted()
        {
            HIVContractedBLL HIVContractedBLLobj = new HIVContractedBLL();
            chklsthivcontracted.DataSource = HIVContractedBLLobj.GetALLHIVContracted();
            chklsthivcontracted.DataTextField = "ContractedThrough";
            chklsthivcontracted.DataValueField = "ContractedID";
            chklsthivcontracted.DataBind();
        }
        /// <summary>
        /// to get the HealthCenter Data
        /// </summary>
        private void GetHealthCenter()
        {
            HealthCenterBLL BLLobj = new HealthCenterBLL();

            ddlNearestHealthCentre.DataSource = BLLobj.GetHealthCenter();
            ddlNearestHealthCentre.DataTextField = "healthcentername";
            ddlNearestHealthCentre.DataValueField = "healthcenterid";
            ddlNearestHealthCentre.DataBind();
        }

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSaveDisability.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region froze / Approval / pending / Decline
        /// <summary>
        /// to check the Status of the froze / Approval / pending / Decline
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSaveDisability.Visible = false;
                    btnClearDisability.Visible = false;
                    btnSaveHealthInfo.Visible = false;
                    btnClearHealthInfo.Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 1].Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 2].Visible = false;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 3].Visible = false;

                    getApprrequtStatusHealthDisability();
                    getApprrequtStatusHealthInfo();
                    checkApprovalExitOrNot();
                }
            }
        }
        /// <summary>
        ///  to check the status of the Approvar that Exist or Not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusPAPHealthInfo.Text = "";
            StatusPAPHealthInfo.Visible = false;
            StatusPAPHealthDisability.Text = "";
            StatusPAPHealthDisability.Visible = false; // used to display the Status if you send Request for change data
            // getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {

                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-HD");
                lnkPAPHealthDisability.Attributes.Add("onclick", paramChangeRequest);
                string paramChangeRequest1 = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHHIF");
                lnkPAPHealthInfo.Attributes.Add("onclick", paramChangeRequest1);
                lnkPAPHealthDisability.Visible = true;
                lnkPAPHealthInfo.Visible = true;
            }
            else
            {
                lnkPAPHealthInfo.Visible = false;
                lnkPAPHealthDisability.Visible = false;
            }
            #endregion
            getApprrequtStatusHealthDisability();
            getApprrequtStatusHealthInfo();
        }
        /// <summary>
        ///  to get the status of the ChangeRequest  Health Disability
        /// </summary>

        public void ChangeRequestStatusHealthDisability()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-HD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the status of the Approvar Health Disability
        /// </summary>
        public void getApprrequtStatusHealthDisability()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-HD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPAPHealthDisability.Visible = false;
                    btnSaveDisability.Visible = false;
                    btnClearDisability.Visible = false;
                    StatusPAPHealthDisability.Visible = true;
                    StatusPAPHealthDisability.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPAPHealthDisability.Visible = true;
                    btnSaveDisability.Visible = false;
                    btnClearDisability.Visible = false;
                    StatusPAPHealthDisability.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPAPHealthDisability.Visible = false;
                    btnSaveDisability.Visible = true;
                    btnClearDisability.Visible = true;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 1].Visible = true;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 2].Visible = true;
                    grdDisabilities.Columns[grdDisabilities.Columns.Count - 3].Visible = true;
                    StatusPAPHealthDisability.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        /// <summary>
        /// to get status of the Change Request Health Info 
        /// </summary>

        public void ChangeRequestStatusHealthInfo()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHHIF";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the Status of the Approvar Health Info
        /// </summary>

        public void getApprrequtStatusHealthInfo()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHHIF";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPAPHealthInfo.Visible = false;
                    btnSaveHealthInfo.Visible = false;
                    btnClearHealthInfo.Visible = false;
                    StatusPAPHealthInfo.Visible = true;
                    StatusPAPHealthInfo.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPAPHealthInfo.Visible = true;
                    btnSaveHealthInfo.Visible = false;
                    btnClearHealthInfo.Visible = false;
                    StatusPAPHealthInfo.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPAPHealthInfo.Visible = false;
                    btnSaveHealthInfo.Visible = true;
                    btnClearHealthInfo.Visible = true;
                    StatusPAPHealthInfo.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
#endregion
        /// <summary>
        /// to bind the Diseases
        /// </summary>
        private void BindDiseases()
        {
            DiseaseBLL objDiseaseBLL = new DiseaseBLL();
            chklstCommonDiseases.DataSource = objDiseaseBLL.GetALLDiseases("");
            chklstCommonDiseases.DataTextField = "DiseaseName";
            chklstCommonDiseases.DataValueField = "DiseaseID";
            chklstCommonDiseases.DataBind();
        }


        /// <summary>
        /// to bind the DisabilityGrid
        /// </summary>
        private void BindDisabilityGrid()
        {
            grdDisabilities.DataSource = (new PAP_HealthBLL()).GetDisabilities(Convert.ToInt32(Session["HH_ID"]));
            grdDisabilities.DataBind();
        }
        /// <summary>
        /// to bind the Disabilities
        /// </summary>
        private void BindDisabilities()
        {
            DisabilityBLL objDisabilityBLL = new DisabilityBLL();
            ddlDisability.DataSource = objDisabilityBLL.GetDisabilities();
            ddlDisability.DataTextField = "DisabilityName";
            ddlDisability.DataValueField = "DisabilityID";
            ddlDisability.DataBind();
        }
        /// <summary>
        /// To get the Health Info data from the database
        /// </summary>
        private void GetHealthInfo()
        {
            PAP_HealthBO objHealth = (new PAP_HealthBLL()).GetHealthInfoByID(Convert.ToInt32(Session["HH_ID"]));

            if (objHealth != null)
            {
                ddlNearestHealthCentre.ClearSelection();
                if (ddlNearestHealthCentre.Items.FindByValue(objHealth.NearestHealthCentre) != null)
                    ddlNearestHealthCentre.Items.FindByValue(objHealth.NearestHealthCentre).Selected = true;

                txtDistanceToHealthCentre.Text = objHealth.DistanceToHealthCentre;

                if (objHealth.UsedByFamily.ToUpper() == "YES")
                    chkUsedByFamily.Checked = true;

                txtNonUseReason.Text = objHealth.NonUseReason;
                txtNoOfBirth.Text = objHealth.NoOfBirth.ToString();
                txtNoOfDeath.Text = objHealth.NoOfDeath.ToString();
                txtReasonForDeath.Text = objHealth.ReasonForDeath;
                string[] sCommonDiseases = objHealth.CommonDiseases.ToString().Split(',');
                for (int i = 0; i < sCommonDiseases.Length; i++)
                {
                    if (chklstCommonDiseases.Items.FindByValue(sCommonDiseases[i]) != null)
                    {
                        chklstCommonDiseases.Items.FindByValue(sCommonDiseases[i]).Selected = true;
                    }
                }


                string[] sContracted = objHealth.HowContracted.ToString().Split(',');
                for (int i = 0; i < sContracted.Length; i++)
                {
                    if (chklsthivcontracted.Items.FindByValue(sContracted[i]) != null)
                    {
                        chklsthivcontracted.Items.FindByValue(sContracted[i]).Selected = true;
                    }
                }


                if (objHealth.PracticeFamilyPlanning == "YES")
                    chkFamilyPlanning.Checked = true;

                if (objHealth.HeardOfAIDS == "YES")
                    chkHeardOfAIDS.Checked = true;

               // txtHowContracted.Text = objHealth.HowContracted;
                txtHowAvoided.Text = objHealth.HowAvoided;
            }
        }

        /// <summary>
        /// to save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveHealthInfo_Click(object sender, EventArgs e)
        {
            PAP_HealthBO objHealth = new PAP_HealthBO();

            objHealth.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
            objHealth.NearestHealthCentre = ddlNearestHealthCentre.SelectedValue;
            objHealth.DistanceToHealthCentre = txtDistanceToHealthCentre.Text.Trim();

            if (chkUsedByFamily.Checked)
                objHealth.UsedByFamily = "YES";
            else
                objHealth.UsedByFamily = "NO";

             string strMaxNonUseReason = txtNonUseReason.Text.Trim();
            if (strMaxNonUseReason.Trim().Length >= 500)
            {
                strMaxNonUseReason = txtNonUseReason.Text.ToString().Trim().Substring(0, 500);
            }
            objHealth.NonUseReason = strMaxNonUseReason;

            if (txtNoOfBirth.Text.Trim() != "")
                objHealth.NoOfBirth = Convert.ToInt32(txtNoOfBirth.Text.Trim());

            if (txtNoOfDeath.Text.Trim() != "")
                objHealth.NoOfDeath = Convert.ToInt32(txtNoOfDeath.Text.Trim());
            string strMax = txtReasonForDeath.Text.Trim();
            if (strMax.Trim().Length >= 999)
            {
                strMax = txtReasonForDeath.Text.ToString().Trim().Substring(0, 900);
            }
            objHealth.ReasonForDeath = strMax;

            string sCommonDiseases = "";
            for (int iitem = 0; iitem < chklstCommonDiseases.Items.Count; iitem++)
            {
                if (chklstCommonDiseases.Items[iitem].Selected)
                {
                    sCommonDiseases += "," + chklstCommonDiseases.Items[iitem].Value;
                }
            }

            if (sCommonDiseases.Length > 0)
                sCommonDiseases = sCommonDiseases.Remove(0, 1);

            objHealth.CommonDiseases = sCommonDiseases;


            string sContracted = "";
            for (int iitem = 0; iitem < chklsthivcontracted.Items.Count; iitem++)
            {
                if (chklsthivcontracted.Items[iitem].Selected)
                {
                    sContracted += "," + chklsthivcontracted.Items[iitem].Value;
                }
            }

            if (sContracted.Length > 0)
                sContracted = sContracted.Remove(0, 1);

            objHealth.HowContracted = sContracted;





            if (chkFamilyPlanning.Checked)
                objHealth.PracticeFamilyPlanning = "YES";
            else
                objHealth.PracticeFamilyPlanning = "NO";

            if (chkHeardOfAIDS.Checked)
                objHealth.HeardOfAIDS = "YES";
            else
                objHealth.HeardOfAIDS = "NO";



            //objHealth.HowContracted = txtHowContracted.Text.Trim();
            string HIVAVOIDED = txtHowAvoided.Text.Trim();
            if (HIVAVOIDED.Trim().Length >= 500)
            {
                HIVAVOIDED = txtHowAvoided.Text.ToString().Trim().Substring(0, 900);
            }
            objHealth.HowAvoided = HIVAVOIDED;

            objHealth.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

            (new PAP_HealthBLL()).UpdateHealthInfo(objHealth);
            ChangeRequestStatusHealthInfo();
            projectFrozen();
          
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saved", "alert('Health details saved successfully');", true);
        }
        /// <summary>
        /// to clear the Health Info records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearHealthInfo_Click(object sender, EventArgs e)
        {
            ddlNearestHealthCentre.ClearSelection();
            txtDistanceToHealthCentre.Text = "";
            chkUsedByFamily.Checked = false;
            txtNonUseReason.Text = "";
            txtNoOfBirth.Text = "";
            txtNoOfDeath.Text = "";
            txtReasonForDeath.Text = "";
            for (int item = 0; item < chklstCommonDiseases.Items.Count; item++)
            {
                chklstCommonDiseases.Items[item].Selected = false;
            }

            for (int item = 0; item < chklsthivcontracted.Items.Count; item++)
            {
                chklsthivcontracted.Items[item].Selected = false;
            }

            chkFamilyPlanning.Checked = false;
            txtNonUseReason.Enabled = true;
            chkHeardOfAIDS.Checked = false;
          //  txtHowContracted.Text = "";
            txtHowAvoided.Text = "";
        }
        /// <summary>
        /// to save the Disability data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSaveDisability_Click(object sender, EventArgs e)
        {
            string message = "";
            PAP_HealthBLL objHealthBLL = new PAP_HealthBLL();
            PAP_DisabilityBO objPAPDisability = new PAP_DisabilityBO();

            objPAPDisability.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
            objPAPDisability.DisabilityID = Convert.ToInt32(ddlDisability.SelectedItem.Value);
            objPAPDisability.HealthCareNeeded = txtHealthCareNeeded.Text.Trim();

            if (Convert.ToInt32(ViewState["DISABILITY_ID"]) > 0)
            {
                objPAPDisability.PAPDisabilityID = Convert.ToInt32(ViewState["DISABILITY_ID"]);
                objPAPDisability.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = objHealthBLL.UpdateDisability(objPAPDisability);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }
                SetUpdateMode(false);
            }
            else
            {
                objPAPDisability.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objHealthBLL.AddDisability(objPAPDisability);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
            }
            ChangeRequestStatusHealthDisability();
            projectFrozen();
          
            ClearDisabilityDetails();
            SetUpdateMode(false);
            BindDisabilityGrid();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }
        /// <summary>
        /// to clear the Disability Details
        /// </summary>
        private void ClearDisabilityDetails()
        {
            ddlDisability.ClearSelection();
            txtHealthCareNeeded.Text = "";
        }
        /// <summary>
        /// to check the status of the checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                ViewState["DISABILITY_ID"] = ((Literal)gr.FindControl("litDisabilityID")).Text;

                PAP_HealthBLL objPAPHealthBLL = new PAP_HealthBLL();

                message = objPAPHealthBLL.ObsoleteDisability(Convert.ToInt32(ViewState["DISABILITY_ID"]), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                BindDisabilityGrid();

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdDisabilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        /// <summary>
        /// for Edit and Delete Commnad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDisabilities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["DISABILITY_ID"] = e.CommandArgument;
                PAP_HealthBLL objHealthBLL = new PAP_HealthBLL();
                PAP_DisabilityBO objDisability = objHealthBLL.GetDisabilityByID(Convert.ToInt32(ViewState["DISABILITY_ID"]));

                if (objDisability != null)
                {
                    ddlDisability.ClearSelection();
                    if (ddlDisability.Items.FindByValue(objDisability.DisabilityID.ToString()) != null)
                        ddlDisability.Items.FindByValue(objDisability.DisabilityID.ToString()).Selected = true;

                    txtHealthCareNeeded.Text = objDisability.HealthCareNeeded;
                }
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                PAP_HealthBLL objHealthBLL = new PAP_HealthBLL();
                objHealthBLL.DeleteDisability(Convert.ToInt32(e.CommandArgument.ToString()));

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);

                ClearDisabilityDetails();
                SetUpdateMode(false);
                BindDisabilityGrid();
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
                btnSaveDisability.Text = "Update";
                btnClearDisability.Text = "Cancel";
            }
            else
            {
                btnSaveDisability.Text = "Save";
                btnClearDisability.Text = "Clear";
                ViewState["DISABILITY_ID"] = "0";
            }
        }
        /// <summary>
        /// to clear fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearDisability_Click(object sender, EventArgs e)
        {
            ClearDisabilityDetails();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
    }
}