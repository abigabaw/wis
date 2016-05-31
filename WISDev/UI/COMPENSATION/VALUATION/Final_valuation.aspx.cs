using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class Final_valuation : System.Web.UI.Page
    {
        string NegotiatedAmount = string.Empty;
        string pageCode = "HFVAL";
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.FinalValuation;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.FinalValuation;

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
                grandTextBox.Text = (cropTextBox.Text + landTextBox.Text + fixturesTextBox.Text + replacementTextBox.Text + damagedTextBox.Text + culturalTextBox.Text);

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Final Valuation";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }


                negotiatedTextBox.Enabled = true;
                negotiatedTextBox.BackColor = System.Drawing.Color.White;
                CropsrequestButton.Attributes.Add("onclick", "return CheckText(" + txtCropsNegAmount.ClientID + ");");
                LandrequestButton.Attributes.Add("onclick", "return CheckText(" + txtLandNegAmount.ClientID + ");");
                FixturesrequestButton.Attributes.Add("onclick", "return CheckText(" + txtFixturesNegAmount.ClientID + ");");
                ReplacementrequestButton.Attributes.Add("onclick", "return CheckText(" + txtReplacementNegAmount.ClientID + ");");
                DamagedrequestButton.Attributes.Add("onclick", "return CheckText(" + txtDamagedNegAmount.ClientID + ");");
                CulturalrequestButton.Attributes.Add("onclick", "return CheckText(" + txtCulturalNegAmount.ClientID + ");");

                cropTextBox.Attributes.Add("onchange", "CalculateAmount();");
                landTextBox.Attributes.Add("onchange", "CalculateAmount();");
                fixturesTextBox.Attributes.Add("onchange", "CalculateAmount();");
                //houseTextBox.Attributes.Add("onchange", "CalculateAmount();");
                replacementTextBox.Attributes.Add("onchange", "CalculateAmount();");
                damagedTextBox.Attributes.Add("onchange", "CalculateAmount();");
                culturalTextBox.Attributes.Add("onchange", "CalculateAmount();");
                grandTextBox.Attributes.Add("onKeyDown", "doCheck();");

                uploadPopWindow();
                LoadData();


                projectFrozen();
                getAppoverReqStatusPakClos();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    btnLoadNEGAmount.Visible = false;
                    requestButton.Visible = false;
                    lnkUPloadDoc.Visible = false;
                    lnkFinalValuation.Visible = false;
                    SetVisiBle(false);
                }
                //ChangeRequestStatusFinalValuation();
            }
            if (Mode == "Readonly")
            {
                ValuationMenu1.Visible = false;
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                btn_Save.Visible = false;
                clearButton.Visible = false;
                btnLoadNEGAmount.Visible = false;
                requestButton.Visible = false;
                CropsrequestButton.Visible = false;
                LandrequestButton.Visible = false;
                FixturesrequestButton.Visible = false;
                ReplacementrequestButton.Visible = false;
                DamagedrequestButton.Visible = false;
                CulturalrequestButton.Visible = false;
                txtCropsNegAmount.Enabled = false;
                txtLandNegAmount.Enabled = false;
                txtFixturesNegAmount.Enabled = false;
                txtReplacementNegAmount.Enabled = false;
                txtDamagedNegAmount.Enabled = false;
                txtCulturalNegAmount.Enabled = false;
                lnkUPloadDoc.Visible = false;
                lnkFinalValuation.Visible = false;
            }
        }
        /// <summary>
        /// SetVisiBle based on status
        /// </summary>
        /// <param name="status"></param>
        protected void SetVisiBle(Boolean status)
        {
            CropsrequestButton.Visible = status;
            LandrequestButton.Visible = status;
            FixturesrequestButton.Visible = status;
            ReplacementrequestButton.Visible = status;
            DamagedrequestButton.Visible = status;
            CulturalrequestButton.Visible = status;
            txtCropsNegAmount.Enabled = status;
            txtLandNegAmount.Enabled = status;
            txtFixturesNegAmount.Enabled = status;
            txtReplacementNegAmount.Enabled = status;
            txtDamagedNegAmount.Enabled = status;
            txtCulturalNegAmount.Enabled = status;
        }
        /// <summary>
        /// get sessionID
        /// </summary>
        private int SessionHHID
        {
            get
            {
                if (Session["HH_ID"] != null)
                    return Convert.ToInt32(Session["HH_ID"].ToString());
                else
                    return 0;
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region Frozen / Approval / Decline / Pending
        /// <summary>
        ///  checks whether project is Frozen
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    requestButton.Visible = false;
                    SetVisiBle(false);
                    checkApprovalExitOrNotforFrozen();
                    getApprrequtStatusFinalValuation();
                }
                else
                {
                    lnkFinalValuation.Visible = false;
                    //getApprrequtStatusFinalValuation();
                }
            }
        }
        /// <summary>
        /// checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNotforFrozen()
        {
            #region Enable ChangeRequest Button
            StatusFinalValuation.Text = "";
            StatusFinalValuation.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequestfroze('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HFVAL");
                lnkFinalValuation.Attributes.Add("onclick", paramChangeRequest);
                lnkFinalValuation.Visible = true;
            }
            else
            {
                lnkFinalValuation.Visible = false;
            }
            #endregion
            getApprrequtStatusFinalValuation();

        }
        /// <summary>
        /// ChangeRequestStatusFinalValuation
        /// </summary>
        public void ChangeRequestStatusFinalValuation()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = pageCode;
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
            getApprrequtStatusFinalValuation();
        }
        /// <summary>
        /// getApprrequtStatusFinalValuation
        /// </summary>
        public void getApprrequtStatusFinalValuation()
        {
            string value = string.Empty;

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = pageCode;
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            int chkApproverStatus = CheckAllApproverLevels();
            if (objHouseHold != null)
            {
                value = objHouseHold.ApproverStatus.ToString();

                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkFinalValuation.Visible = false;
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    requestButton.Visible = false;
                    SetVisiBle(false);
                    StatusFinalValuation.Visible = true;
                    StatusFinalValuation.Text = "Pending Approval";
                }
                else if (objHouseHold.ApproverStatus == 2)
                {
                    lnkFinalValuation.Visible = true;
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    requestButton.Visible = false;
                    StatusFinalValuation.Visible = false;
                    StatusFinalValuation.Text = string.Empty;
                }
                else if (objHouseHold.ApproverStatus == 1)
                {
                    lnkFinalValuation.Visible = false;
                    btn_Save.Visible = true;
                    clearButton.Visible = true;
                    requestButton.Visible = true;
                    SetVisiBle(true);
                    LoadNegIndData(Convert.ToInt32(Session["HH_ID"]));
                    getApprovalChangerequestStatus(value);
                    StatusFinalValuation.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }

            }
        }

        #endregion
        /// <summary>
        /// fetch details and assign to textbox
        /// </summary>
        private void LoadData()
        {
            FinalValuationBLL oFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO oFinalValuationBO = new FinalValuationBO();
            // FinalValuationList lstFinalValuation = new FinalValuationList();
            int HHID = 0;

            if (Session["HH_ID"] != null)
                HHID = Convert.ToInt32(Session["HH_ID"].ToString());

            if (SessionHHID > 0)
            {
                oFinalValuationBO = oFinalValuationBLL.getFinalValuatin(SessionHHID);

                if (oFinalValuationBO != null)
                {
                    cropTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CropValue);
                    //MaxCap TextBox & CheckBox
                    txtMaxcapCrops.Text = UtilBO.CurrencyFormat(oFinalValuationBO.Crop_Val_Aft_Max_Cap);
                    if (!string.IsNullOrEmpty(oFinalValuationBO.Crop_Max_Cap_Case) && oFinalValuationBO.Crop_Max_Cap_Case.ToLower() == "yes")
                        ChkMaxCapCase.Checked = true;
                    else
                        ChkMaxCapCase.Checked = false;

                    landTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.LandValue);
                    fixturesTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.FixtureValue);
                    // houseTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.HouseValue);
                    replacementTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.ReplacementValue);
                    damagedTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.DamagedcropValue);
                    culturalTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CulturalpropertyValue);
                    grandTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.GrandtotalValue);
                    negotiatedTextBox.Text = UtilBO.CurrencyFormat(oFinalValuationBO.NegotiatedAmount);
                    commentsTextBox.Text = oFinalValuationBO.ValsummaryComments;

                    SetVisiBle(true);
                    LoadNegIndData(HHID);
                    if ((oFinalValuationBO.NegotiatedAmount) == 0)
                    {
                        getApprovalChangerequestStatus("0");
                        //requestButton.Visible = false;
                    }
                    else
                    {
                        getApprovalChangerequestStatus(oFinalValuationBO.NegotiatedAmount.ToString());
                        //requestButton.Visible = false;
                    }
                }
                else
                {
                    SetVisiBle(false);
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    requestButton.Visible = false;
                }
            }

        }
        /// <summary>
        /// LoadNegIndData
        /// </summary>
        /// <param name="HHID"></param>
        private void LoadNegIndData(int HHID)
        {
            FinalValuationBLL oFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO oFinalValuationBO = new FinalValuationBO();
            oFinalValuationBO = oFinalValuationBLL.getNegIndValuation(HHID);

            if (oFinalValuationBO != null)
            {
                txtCropsNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CropNegValue);
                txtLandNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.LandNegValue);
                txtFixturesNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.FixtureNegValue);
                txtReplacementNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.ReplacementNegValue);
                txtDamagedNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.DamagedcropNegValue);
                txtCulturalNegAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CulturalpropertyNegValue);
                if (oFinalValuationBO.CropNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.CropNegValueAppStatus.ToLower() == "request declined")
                {
                    txtCropsNegAmount.Enabled = true;
                    CropsrequestButton.Visible = true;
                    if (oFinalValuationBO.CropNegValueAppStatus.ToUpper() == "N")
                        lblCropsStatus.Text = "";
                    else
                        lblCropsStatus.Text = oFinalValuationBO.CropNegValueAppStatus;
                }
                else
                {
                    txtCropsNegAmount.Enabled = false;
                    CropsrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.CropNegValueAppStatus)
                        lblCropsStatus.Text = "<font color='Green'>" + oFinalValuationBO.CropNegValueAppStatus + "</font>";
                    else
                        lblCropsStatus.Text = oFinalValuationBO.CropNegValueAppStatus;
                }
                if (oFinalValuationBO.LandNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.LandNegValueAppStatus.ToLower() == "request declined")
                {
                    txtLandNegAmount.Enabled = true;
                    LandrequestButton.Visible = true;
                    if (oFinalValuationBO.LandNegValueAppStatus.ToUpper() == "N")
                        lblLandStatus.Text = "";
                    else
                        lblLandStatus.Text = oFinalValuationBO.LandNegValueAppStatus;
                }
                else
                {
                    txtLandNegAmount.Enabled = false;
                    LandrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.LandNegValueAppStatus)
                        lblLandStatus.Text = "<font color='Green'>" + oFinalValuationBO.LandNegValueAppStatus + "</font>";
                    else
                        lblLandStatus.Text = oFinalValuationBO.LandNegValueAppStatus;
                }
                if (oFinalValuationBO.FixtureNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.FixtureNegValueAppStatus.ToLower() == "request declined")
                {
                    txtFixturesNegAmount.Enabled = true;
                    FixturesrequestButton.Visible = true;
                    if (oFinalValuationBO.FixtureNegValueAppStatus.ToUpper() == "N")
                        lblFixturesStatus.Text = "";
                    else
                        lblFixturesStatus.Text = oFinalValuationBO.FixtureNegValueAppStatus;
                }
                else
                {
                    txtFixturesNegAmount.Enabled = false;
                    FixturesrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.FixtureNegValueAppStatus)
                        lblFixturesStatus.Text = "<font color='Green'>" + oFinalValuationBO.FixtureNegValueAppStatus + "</font>";
                    else
                        lblFixturesStatus.Text = oFinalValuationBO.FixtureNegValueAppStatus;
                }
                if (oFinalValuationBO.ReplacementNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.ReplacementNegValueAppStatus.ToLower() == "request declined")
                {
                    txtReplacementNegAmount.Enabled = true;
                    ReplacementrequestButton.Visible = true;
                    if (oFinalValuationBO.ReplacementNegValueAppStatus.ToUpper() == "N")
                        lblReplacementStatus.Text = "";
                    else
                        lblReplacementStatus.Text = oFinalValuationBO.ReplacementNegValueAppStatus;
                }
                else
                {
                    txtReplacementNegAmount.Enabled = false;
                    ReplacementrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.ReplacementNegValueAppStatus)
                        lblReplacementStatus.Text = "<font color='Green'>" + oFinalValuationBO.ReplacementNegValueAppStatus + "</font>";
                    else
                        lblReplacementStatus.Text = oFinalValuationBO.ReplacementNegValueAppStatus;
                }
                if (oFinalValuationBO.CulturalpropertyNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.CulturalpropertyNegValueAppStatus.ToLower() == "request declined")
                {
                    txtCulturalNegAmount.Enabled = true;
                    CulturalrequestButton.Visible = true;
                    if (oFinalValuationBO.CulturalpropertyNegValueAppStatus.ToUpper() == "N")
                        lblCulturalStatus.Text = "";
                    else
                        lblCulturalStatus.Text = oFinalValuationBO.CulturalpropertyNegValueAppStatus;
                }
                else
                {
                    txtCulturalNegAmount.Enabled = false;
                    CulturalrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.CulturalpropertyNegValueAppStatus)
                        lblCulturalStatus.Text = "<font color='Green'>" + oFinalValuationBO.CulturalpropertyNegValueAppStatus + "</font>";
                    else
                        lblCulturalStatus.Text = oFinalValuationBO.CulturalpropertyNegValueAppStatus;
                }
                if (oFinalValuationBO.DamagedcropNegValueAppStatus.ToUpper() == "N" || oFinalValuationBO.DamagedcropNegValueAppStatus.ToLower() == "request declined")
                {
                    txtDamagedNegAmount.Enabled = true;
                    DamagedrequestButton.Visible = true;
                    if (oFinalValuationBO.DamagedcropNegValueAppStatus.ToUpper() == "N")
                        lblDamagedStatus.Text = "";
                    else
                        lblDamagedStatus.Text = oFinalValuationBO.DamagedcropNegValueAppStatus;
                }
                else
                {
                    txtDamagedNegAmount.Enabled = false;
                    DamagedrequestButton.Visible = false;
                    if ("Approved" == oFinalValuationBO.DamagedcropNegValueAppStatus)
                        lblDamagedStatus.Text = "<font color='Green'>" + oFinalValuationBO.DamagedcropNegValueAppStatus + "</font>";
                    else
                        lblDamagedStatus.Text = oFinalValuationBO.DamagedcropNegValueAppStatus;
                }
            }
        }
        /// <summary>
        /// uploadPopWindow
        /// </summary>
        public void uploadPopWindow()
        {
            //Add By Ramu For upload Document
            string userName = (Session["userName"].ToString());
            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = 0;
            string ProjectCode = string.Empty;
            //string perStu = string.Empty;

            if (Session["PROJECT_ID"] != null)
            {
                ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                ProjectCode = Session["PROJECT_CODE"].ToString();
            }
            else
            {
                ProjectID = 21; //hardcord value

            }
            int HHID = 0;
            if (Session["HH_ID"] != null)
            {
                HHID = Convert.ToInt32(Session["HH_ID"]);
            }
            else
            {
                HHID = 0;
            }
            if (Session["PROJECT_CODE"] != null)
            {
                ProjectCode = Session["PROJECT_CODE"].ToString();
            }
            else
            {
                ProjectCode = "";
            }
            string DocumentCode = "FF";

            string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

            string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

            lnkUPloadDoc.Attributes.Add("onclick", param);

            lnkUPloadDoclist.Attributes.Add("onclick", paramView);

        }
        /// <summary>
        /// saves details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {

            FinalValuationBLL objFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO objFinalValuationBO = new FinalValuationBO();
            objFinalValuationBO.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objFinalValuationBO.HhId = householdID;
            objFinalValuationBO.PageCode = "NEG";
            objFinalValuationBO.Workflowcode = UtilBO.WorkflowNegotatedCodeApproval;

            objFinalValuationBO = objFinalValuationBLL.ApprovalChangerequestStatus(objFinalValuationBO);
            if (objFinalValuationBO != null && objFinalValuationBO.IsFinal == "Y")
            {
                if (objFinalValuationBO.IsFinal == "Y")
                {
                    negotiatedTextBox.Enabled = false;
                    negotiatedTextBox.BackColor = System.Drawing.Color.Gray;

                    int count = 0;
                    FinalValuationBLL Final_ValuationBLLobj = null;
                    try
                    {
                        string hhid = Session["HH_ID"].ToString();
                        FinalValuationBO Finalvaluationobj = new FinalValuationBO();

                        Finalvaluationobj.CropValue = Convert.ToDecimal(cropTextBox.Text.ToString());
                        //MaxCap Cap Value For Saving
                        Finalvaluationobj.Crop_Max_Cap_Case = "No";

                        if (ChkMaxCapCase.Checked == true)
                            Finalvaluationobj.Crop_Max_Cap_Case = "Yes";
                           
                        Finalvaluationobj.Crop_Val_Aft_Max_Cap = Convert.ToDecimal(txtMaxcapCrops.Text.ToString());
                        //End Max Cap Value Addition
                        Finalvaluationobj.LandValue = Convert.ToDecimal(landTextBox.Text.ToString());
                        Finalvaluationobj.FixtureValue = Convert.ToDecimal(fixturesTextBox.Text.ToString());
                        Finalvaluationobj.HouseValue = 0;   // Is not considered for Final Valuation.
                        Finalvaluationobj.ReplacementValue = Convert.ToDecimal(replacementTextBox.Text.ToString());
                        Finalvaluationobj.DamagedcropValue = Convert.ToDecimal(damagedTextBox.Text.ToString());
                        Finalvaluationobj.CulturalpropertyValue = Convert.ToDecimal(culturalTextBox.Text.ToString());
                        Finalvaluationobj.GrandtotalValue = Convert.ToDecimal(grandTextBox.Text.ToString());
                        Finalvaluationobj.NegotiatedAmount = Convert.ToDecimal(negotiatedTextBox.Text.ToString());
                        if (commentsTextBox.Text.Length > 1000)
                            Finalvaluationobj.ValsummaryComments = commentsTextBox.Text.Substring(0, 1000);
                        else
                            Finalvaluationobj.ValsummaryComments = commentsTextBox.Text;
                        Finalvaluationobj.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                        Finalvaluationobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                        Finalvaluationobj.HouseholdID = Convert.ToInt32(hhid);

                        Final_ValuationBLLobj = new FinalValuationBLL();
                        count = Final_ValuationBLLobj.Insert(Finalvaluationobj);
                        projectFrozen();
                        checkApprovalExitOrNotforFrozen();
                        getApprrequtStatusFinalValuation();
                        if (count == -1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data updated successfully');", true);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Final_ValuationBLLobj = null;
                    }
                }
            }
            else
            {
                decimal NAmount = 0;
                if (string.IsNullOrEmpty(negotiatedTextBox.Text))
                {
                    NAmount = 0;
                }
                else
                {
                    NAmount = Convert.ToDecimal(negotiatedTextBox.Text);
                }
                if ((NAmount) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Negotiated Amount Not Approved');", true);
                }
                else
                {
                    negotiatedTextBox.Enabled = false;
                    negotiatedTextBox.BackColor = System.Drawing.Color.Gray;

                    int count = 0;
                    FinalValuationBLL Final_ValuationBLLobj = null;
                    try
                    {
                        string hhid = Session["HH_ID"].ToString();
                        FinalValuationBO Finalvaluationobj = new FinalValuationBO();

                        Finalvaluationobj.CropValue = Convert.ToDecimal(cropTextBox.Text.ToString());
                        //MaxCap Cap Value For Saving
                        Finalvaluationobj.Crop_Max_Cap_Case = "No";
                        
                        if (ChkMaxCapCase.Checked == true)
                            Finalvaluationobj.Crop_Max_Cap_Case = "Yes";
                           
                        Finalvaluationobj.Crop_Val_Aft_Max_Cap = Convert.ToDecimal(txtMaxcapCrops.Text.ToString());
                        //End Max Cap Value Addition
                        Finalvaluationobj.LandValue = Convert.ToDecimal(landTextBox.Text.ToString());
                        Finalvaluationobj.FixtureValue = Convert.ToDecimal(fixturesTextBox.Text.ToString());
                        Finalvaluationobj.HouseValue = 0;   // Is not considered for Final Valuation.
                        Finalvaluationobj.ReplacementValue = Convert.ToDecimal(replacementTextBox.Text.ToString());
                        Finalvaluationobj.DamagedcropValue = Convert.ToDecimal(damagedTextBox.Text.ToString());
                        Finalvaluationobj.CulturalpropertyValue = Convert.ToDecimal(culturalTextBox.Text.ToString());
                        Finalvaluationobj.GrandtotalValue = Convert.ToDecimal(grandTextBox.Text.ToString());
                        Finalvaluationobj.NegotiatedAmount = NAmount; // sending as Zero
                        // Finalvaluationobj.NegotiatedAmount = Convert.ToInt32(negotiatedTextBox.Text.ToString()); // change by ramu
                        if (commentsTextBox.Text.Length > 1000)
                            Finalvaluationobj.ValsummaryComments = commentsTextBox.Text.Substring(0, 1000);
                        else
                            Finalvaluationobj.ValsummaryComments = commentsTextBox.Text;
                        Finalvaluationobj.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                        Finalvaluationobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                        Finalvaluationobj.HouseholdID = Convert.ToInt32(hhid);

                        Final_ValuationBLLobj = new FinalValuationBLL();
                        count = Final_ValuationBLLobj.Insert(Finalvaluationobj);
                        if (count == -1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data updated successfully');", true);
                        }
                        negotiatedTextBox.Enabled = true;
                        negotiatedTextBox.BackColor = System.Drawing.Color.White;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Final_ValuationBLLobj = null;
                    }

                }
            }
            ChangeRequestStatusFinalValuation();
            projectFrozen();

        }

        #region for egotiated Amount

        /// <summary>
        ///Negotiated Amount Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void requestButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            requestButton.Visible = false;
            NegotiatedAmount = negotiatedTextBox.Text.ToString();
            int result = checkApprovalExitOrNot(NegotiatedAmount);
            if (result == 0)
            {
                count = NogotiatedAmountSave(NegotiatedAmount);
            }
            if (count == -1)
            {
                getApprovalChangerequestStatus(NegotiatedAmount);
                btn_Save.Visible = false;
                clearButton.Visible = false;
                requestButton.Visible = false;
            }
            LoadData();
        }
        /// <summary>
        /// To change buttonid based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void requestButtonForInd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int count = 0;
            int result = 0;
            btn.Visible = false;
            string NegInd;
            if (btn.ID.ToString() == "CropsrequestButton")
            {
                NegInd = txtCropsNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalCrops);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalCrops);
            }
            else if (btn.ID.ToString() == "LandrequestButton")
            {
                NegInd = txtLandNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalLand);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalLand);
            }
            else if (btn.ID.ToString() == "FixturesrequestButton")
            {
                NegInd = txtFixturesNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalFixtures);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalFixtures);
            }
            else if (btn.ID.ToString() == "ReplacementrequestButton")
            {
                NegInd = txtReplacementNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalRep);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalRep);
            }
            else if (btn.ID.ToString() == "DamagedrequestButton")
            {
                NegInd = txtDamagedNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalDamCrops);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalDamCrops);
            }
            else if (btn.ID.ToString() == "CulturalrequestButton")
            {
                NegInd = txtCulturalNegAmount.Text.ToString();
                result = checkApprovalExitOrNotIndividual(NegInd, UtilBO.WorkflowNegotatedCodeApprovalCulPro);
                if (result == 0)
                    count = IndividualNogotiatedAmountSave(NegInd, UtilBO.WorkflowNegotatedCodeApprovalCulPro);
            }


            //if (result == 0)
            //{
            //    count = NogotiatedAmountSave(NegotiatedAmount);
            //}
            //if (count == -1)
            //{
            //    getApprovalChangerequestStatus(NegotiatedAmount);
            //    btn_Save.Visible = false;
            //    clearButton.Visible = false;
            //    requestButton.Visible = false;
            //}

        }


        /// <summary>
        ///Save the Neg Amount with status N
        /// </summary>
        /// <param name="NegotiatedAmount"></param>
        /// <returns></returns>
        private int NogotiatedAmountSave(string NegotiatedAmount)
        {
            int count = 0;
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            FinalValuationBLL objFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO objFinalValuationBO = new FinalValuationBO();

            objFinalValuationBO.NegotiatedAmount = Convert.ToDecimal(NegotiatedAmount);
            objFinalValuationBO.HhId = householdID;
            count = objFinalValuationBLL.SaveNogotiatedAmount(objFinalValuationBO);
            return count;
        }


        /// <summary>
        /// Check for approval Exit
        /// </summary>
        /// <param name="NegotiatedAmount_"></param>
        /// <returns></returns>
        public int checkApprovalExitOrNot(string NegotiatedAmount_)
        {
            #region Enable ChangeRequest Button
            StatusLabel.Text = "";
            StatusLabel.Visible = false;
            int status = 0;
            // used to display the Status if you send Request for change data
            string NegotiatedAmount = NegotiatedAmount_.ToString();
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowNegotatedCodeApproval;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);
            decimal NAmount = 0;
            if (!string.IsNullOrEmpty(NegotiatedAmount))
            {
                NAmount = Convert.ToDecimal(negotiatedTextBox.Text.ToString());
            }
            else
            {
                NAmount = 0;
            }

            if (NAmount > 0)
            {
                if (objWorkFlowBO != null)
                {
                    int userID = Convert.ToInt32(Session["USER_ID"]);
                    int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    int HHID = Convert.ToInt32(Session["HH_ID"]);
                    NegotiatedAmount = negotiatedTextBox.Text.ToString();
                    string pageCode = "NEG";

                    string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}','{5}')", ChangeRequestCode, ProjectID, userID, HHID, NegotiatedAmount, pageCode);

                    ClientScript.RegisterStartupScript(this.GetType(), "UPLOADPHOTO", paramChangeRequest, true);
                    status = 0;

                }
                else
                {
                    requestButton.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approver Not Defined');", true);

                    negotiatedTextBox.Text = "0";
                    status = 1;

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Negotiated Amount must be greater than Zero(0)');", true);
            }
            #endregion
            negotiatedTextBox.Text = NegotiatedAmount.ToString();
            getApprovalChangerequestStatus(NegotiatedAmount);
            return status;
        }


        /// <summary>
        ///Save the Neg Amount with status N
        /// </summary>
        /// <param name="NegotiatedAmount"></param>
        /// <param name="WorkFlowCode"></param>
        /// <returns></returns>
        private int IndividualNogotiatedAmountSave(string NegotiatedAmount, string WorkFlowCode)
        {
            int count = 0;
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            FinalValuationBLL objFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO objFinalValuationBO = new FinalValuationBO();

            //objFinalValuationBO.NegotiatedAmount = Convert.ToDecimal(NegotiatedAmount);
            //objFinalValuationBO.HhId = householdID;
            count = objFinalValuationBLL.SaveNogotiatedAmountIndividual(householdID, Convert.ToDecimal(NegotiatedAmount), WorkFlowCode);
            return count;
        }


        /// <summary>
        /// Check for approval Exit or not Add By Ramu
        /// </summary>
        /// <param name="NegotiatedAmount_"></param>
        /// <param name="WorkFlowCode"></param>
        /// <returns></returns>
        public int checkApprovalExitOrNotIndividual(string NegotiatedAmount_, string WorkFlowCode)
        {
            #region Enable ChangeRequest Button
            StatusLabel.Text = "";
            StatusLabel.Visible = false;
            int status = 0;
            // used to display the Status if you send Request for change data
            string NegotiatedAmount = NegotiatedAmount_.ToString();
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = WorkFlowCode;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);
            decimal NAmount = 0;
            if (!string.IsNullOrEmpty(NegotiatedAmount))
            {
                NAmount = Convert.ToDecimal(NegotiatedAmount);
            }
            else
            {
                NAmount = 0;
            }

            if (NAmount > 0)
            {
                if (objWorkFlowBO != null)
                {
                    int userID = Convert.ToInt32(Session["USER_ID"]);
                    int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    int HHID = Convert.ToInt32(Session["HH_ID"]);
                    //NegotiatedAmount = NegotiatedAmount;
                    string pageCode = WorkFlowCode;

                    string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}','{5}')", ChangeRequestCode, ProjectID, userID, HHID, NegotiatedAmount, pageCode);

                    ClientScript.RegisterStartupScript(this.GetType(), "SendRequest", paramChangeRequest, true);
                    status = 0;

                }
                else
                {
                    //requestButton.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approver Not Defined');", true);

                    negotiatedTextBox.Text = "0";
                    status = 1;

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Negotiated Amount must be greater than Zero(0)');", true);
            }
            #endregion
            //negotiatedTextBox.Text = NegotiatedAmount.ToString();
            //getApprovalChangerequestStatus(NegotiatedAmount);
            return status;
        }

        #endregion
        /// <summary>
        /// LoadExpense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadExpense_Click(object sender, EventArgs e)
        {
            grandTextBox.Text = (cropTextBox.Text + landTextBox.Text + fixturesTextBox.Text + replacementTextBox.Text + damagedTextBox.Text + culturalTextBox.Text);

            if (Session["PROJECT_CODE"] != null)
            {
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Final Valuation";
            }
            else
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }


            negotiatedTextBox.Enabled = true;
            negotiatedTextBox.BackColor = System.Drawing.Color.White;

            cropTextBox.Attributes.Add("onchange", "CalculateAmount();");
            landTextBox.Attributes.Add("onchange", "CalculateAmount();");
            fixturesTextBox.Attributes.Add("onchange", "CalculateAmount();");
            // houseTextBox.Attributes.Add("onchange", "CalculateAmount();");
            replacementTextBox.Attributes.Add("onchange", "CalculateAmount();");
            damagedTextBox.Attributes.Add("onchange", "CalculateAmount();");
            culturalTextBox.Attributes.Add("onchange", "CalculateAmount();");
            grandTextBox.Attributes.Add("onKeyDown", "doCheck();");

            uploadPopWindow();
            LoadData();
        }
        /// <summary>
        /// getApprovalChangerequestStatus
        /// </summary>
        /// <param name="NegotiatedAmount"></param>
        public void getApprovalChangerequestStatus(string NegotiatedAmount)
        {
            FinalValuationBLL objFinalValuationBLL = new FinalValuationBLL();
            FinalValuationBO objFinalValuationBO = new FinalValuationBO();
            objFinalValuationBO.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objFinalValuationBO.HhId = householdID;
            objFinalValuationBO.PageCode = "NEG";
            objFinalValuationBO.Workflowcode = UtilBO.WorkflowNegotatedCodeApproval;

            objFinalValuationBO = objFinalValuationBLL.ApprovalChangerequestStatus(objFinalValuationBO);

            int chkApproverStatus = CheckAllApproverLevels();

            if ((objFinalValuationBO) != null)
            {
                if (NegotiatedAmount == "3")
                {
                    lnkFinalValuation.Visible = false;
                }
                else if (NegotiatedAmount == "2")
                {
                    lnkFinalValuation.Visible = true;
                }
                else if (NegotiatedAmount == "1")
                {
                    lnkFinalValuation.Visible = false;
                }
                // if(chkApproverStatus != 0 )
                //{
                int totalchkApproverStatus = totalcountapproval();
                if (totalchkApproverStatus == 3 && objFinalValuationBO.IsFinal == "N")//(objFinalValuationBO.ApproverStatus == 3 && objFinalValuationBO.IsFinal == "N")
                {  //PENDING
                    requestButton.Visible = false;
                    StatusLabel.Visible = true;
                    StatusLabel.Text = "Pending Approval";
                    //negotiatedTextBox.Text = "0";
                    negotiatedTextBox.Enabled = false;
                    VisibleSave(false);
                    SetVisiBle(false);
                }
                if (totalchkApproverStatus == 2 && objFinalValuationBO.IsFinal == "N")//(objFinalValuationBO.ApproverStatus == 2 && objFinalValuationBO.IsFinal == "N")
                {  //DECLINE
                    requestButton.Visible = true;
                    StatusLabel.Visible = true;
                    negotiatedTextBox.Text = "0";
                    negotiatedTextBox.Enabled = true;
                    StatusLabel.Text = "Negotiated Amount Is Declined";//objFinalValuationBO.Comments;
                    VisibleSave(true);
                    LoadNegIndData(householdID);
                }
                if (totalchkApproverStatus == 1 && objFinalValuationBO.IsFinal == "Y")//(objFinalValuationBO.ApproverStatus == 1 && objFinalValuationBO.IsFinal == "Y")
                {  //APPROVED
                    requestButton.Visible = false;
                    StatusLabel.Visible = true;
                    //negotiatedTextBox.Text = NegotiatedAmount.ToString();
                    negotiatedTextBox.Enabled = false;
                    StatusLabel.Text = "<font color='green'>Negotiated Amount Is Approved</font>";
                    //StatusLabel.Text = "Pending Approval";
                    VisibleSave(true);
                    SetVisiBle(false);
                }
            }
            else
            {
                #region for Else Conditin
                if (NegotiatedAmount == "1")
                {
                    WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                    WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                    string ChangeRequestCode = UtilBO.WorkflowNegotatedCodeApproval;

                    objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

                    if (objWorkFlowBO != null)
                    {
                        btn_Save.Visible = true;
                        clearButton.Visible = true;
                        requestButton.Visible = true;
                    }
                    else
                    {
                        requestButton.Visible = false;
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// totalcountapproval
        /// </summary>
        /// <returns></returns>
        public int totalcountapproval()
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;
            WorkFlowList objWorkFlowList_ = null;

            objProjectRoute.WorkFlowApprover = UtilBO.WorkflowNegotatedCodeApproval;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"]);

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    if (Session["HH_ID"] != null)
                    {
                        objProjectRoute.HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                    }
                    else
                    {
                        objProjectRoute.HHID = 0;
                    }
                    //objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = "NEG";  // objHouseHold.PageCode = "DATAV";
                    objProjectRoute.WorkflowCode = UtilBO.WorkflowNegotatedCodeApproval;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    //addtional list
                    objWorkFlowList_ = objProjectRouteBLL.ApprovalStatuschecklist(objProjectRoute);

                    if (objPrintApprovalWF != null)
                    {
                        if (objWorkFlowList[i].CountApproval == objPrintApprovalWF.LEVEL)
                        {
                            if (objPrintApprovalWF.ApprovalstatusID == 1)
                            {
                                finalcount = 1;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 2)
                            {
                                finalcount = 2;
                                approvalcount = 0;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 3)
                            {
                                finalcount = 3;
                                approvalcount = 0;
                                break;
                            }
                        }
                        else
                        {
                            //i + 1; addtionl Code
                            if (objWorkFlowList[i].CountApproval == objWorkFlowList_[i].LEVEL)
                            {
                                if (objWorkFlowList_[i].ApprovalstatusID == 3)
                                {
                                    finalcount = 3;
                                    break;
                                }
                                else
                                {
                                    approvalcount = 0;
                                }
                            }
                        }
                    }
                    if (objWorkFlowList.Count == approvalcount)
                    {
                        finalcount = 1;
                    }
                    else
                    {
                        finalcount = 0;
                    }
                }
            }

            return finalcount;
        }
        /// <summary>
        /// VisibleSave
        /// </summary>
        /// <param name="Status"></param>
        private void VisibleSave(bool Status)
        {
            btn_Save.Visible = Status;
            clearButton.Visible = Status;
        }
        /// <summary>
        /// calls Clearfields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clearButton_Click(object sender, EventArgs e)
        {
            Clearfields();
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clearfields()
        {
            //cropTextBox.Text = string.Empty;
            //landTextBox.Text = string.Empty;
            //fixturesTextBox.Text = string.Empty;
            //houseTextBox.Text = string.Empty;
            //replacementTextBox.Text = string.Empty;
            //damagedTextBox.Text = string.Empty;
            //culturalTextBox.Text = string.Empty;
            //grandTextBox.Text = string.Empty;
            //negotiatedTextBox.Text = string.Empty;
            commentsTextBox.Text = string.Empty;
        }

        #region For Check N- level Approval
        /// <summary>
        /// For Check N- level Approval
        /// </summary>
        /// <returns></returns>
        public int CheckAllApproverLevels()//TotalCountApproval()
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;
            WorkFlowList objWorkFlowList_ = null;

            string ChangeRequestCode = UtilBO.WorkflowNegotatedCodeApproval;

            objProjectRoute.WorkFlowApprover = ChangeRequestCode;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"].ToString());

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    int householdID = Convert.ToInt32(Session["HH_ID"].ToString());
                    objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = UtilBO.WorkflowNegotatedCodeApproval;//Alias DocumentCode;
                    objProjectRoute.WorkflowCode = UtilBO.WorkflowNegotatedCodeApproval;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    //addtional list
                    objWorkFlowList_ = objProjectRouteBLL.ApprovalStatuschecklist(objProjectRoute);

                    if (objPrintApprovalWF != null)
                    {
                        if (objWorkFlowList[i].CountApproval == objPrintApprovalWF.LEVEL)
                        {
                            if (objPrintApprovalWF.ApprovalstatusID == 1)
                            {
                                finalcount = 1;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 2)
                            {
                                finalcount = 2;
                                approvalcount = 0;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 3)
                            {
                                finalcount = 3;
                                approvalcount = 0;
                                break;
                            }
                        }
                        else
                        {
                            //i + 1; addtionl Code
                            if (objWorkFlowList_.Count > 0 && i < objWorkFlowList_.Count)
                            {
                                if (objWorkFlowList[i].CountApproval == objWorkFlowList_[i].LEVEL)
                                {
                                    if (objWorkFlowList_[i].ApprovalstatusID == 3)
                                    {
                                        finalcount = 3;
                                        break;
                                    }
                                    else
                                    {
                                        approvalcount = 0;
                                    }
                                }
                            }
                            else
                            {
                                approvalcount = 0;
                            }
                        }
                    }
                    if (objWorkFlowList.Count == approvalcount)
                    {
                        finalcount = 1;
                    }
                    else
                    {
                        finalcount = 0;
                    }
                }
            }

            return finalcount;

        }
        #endregion

        public void getAppoverReqStatusPakClos()
        {
            WorkFlowBO oWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL oWorkFlowBLL = new WorkFlowBLL();
            PAP_HouseholdBLL oHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO oHouseHold = new PAP_HouseholdBO();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalFL;

            oWorkFlowBO = oWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            if (oWorkFlowBO != null)
            {
                #region When Approver Defined
                oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                int HHID = Convert.ToInt32(Session["HH_ID"]);
                oHouseHold.HhId = HHID;
                oHouseHold.PageCode = "CREND";
                oHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalFL;

                oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);//get Status of Request

                int ApproverStatus = CheckAllApproverLevels("CREND", UtilBO.WorkflowChangeRequestApprovalFL);
                //                #region When Package is NOT Closed



                //                if (oHouseHold != null && ApproverStatus != 1)//oHouseHold.ApproverStatus != 1)
                //                {



                //                }
                //                #endregion When Package is NOT Closed
                //else
                #region When Package is Closed
                if (oHouseHold != null && oHouseHold.ApproverStatus == 1 && ApproverStatus == 1)
                {
                    //Sent for Approval & it is Approved
                    //AprovedStatus(true);
                    //lnkValuationPCI.Visible = false;
                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Green;
                    lblStatusValuationPCI.Text = "PAP file is closed.";
                    // chkOverrideGriv.Visible = false;

                    //Additional Hiding Button
                    btn_Save.Visible = false;
                    clearButton.Visible = false;
                    btnLoadNEGAmount.Visible = false;
                    requestButton.Visible = false;
                    lnkUPloadDoc.Visible = false;
                    lnkFinalValuation.Visible = false;
                    SetVisiBle(false);
                }


                #endregion When Package is Closed

                #endregion When Approver Defined
            }

        }

        public int CheckAllApproverLevels(string PageCode, string WorkFlowCode)//TotalCountApproval()
        {
            int finalcount = 0;
            int approvalcount = 0;

            WorkFlowBO objProjectRoute = new WorkFlowBO();
            WorkFlowBLL objProjectRouteBLL = new WorkFlowBLL();
            WorkFlowList objWorkFlowList = new WorkFlowList();
            WorkFlowBO objPrintApprovalWF = null;

            string ChangeRequestCode = WorkFlowCode;

            objProjectRoute.WorkFlowApprover = ChangeRequestCode;
            objProjectRoute.Project_Id = Convert.ToInt32(Session["PROJECT_ID"].ToString());

            objWorkFlowList = objProjectRouteBLL.getTotalcountapproval(objProjectRoute);

            if (objWorkFlowList.Count > 0)
            {
                //int totalapprovalCount = Convert.ToInt32(objProjectRoute.CountApproval);
                for (int i = 0; i < objWorkFlowList.Count; i++)
                {
                    int householdID = Convert.ToInt32(Session["HH_ID"].ToString());
                    objProjectRoute.HHID = householdID;
                    objProjectRoute.PageCode = PageCode;//Alias DocumentCode;//PageCode;
                    objProjectRoute.WorkflowCode = WorkFlowCode;
                    objProjectRoute.LEVEL = objWorkFlowList[i].CountApproval;

                    objPrintApprovalWF = objProjectRouteBLL.ApprovalStatuscheck(objProjectRoute);

                    if (objPrintApprovalWF != null)
                    {
                        if (objWorkFlowList[i].CountApproval == objPrintApprovalWF.LEVEL)
                        {
                            if (objPrintApprovalWF.ApprovalstatusID == 1)
                            {
                                finalcount = 1;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 2)
                            {
                                finalcount = 2;
                                approvalcount = 0;
                                break;
                            }
                            else if (objPrintApprovalWF.ApprovalstatusID == 3)
                            {
                                finalcount = 3;
                                approvalcount = 0;
                                break;
                            }
                        }
                        else
                        {
                            approvalcount = 0;
                        }
                    }

                    if (objWorkFlowList.Count == approvalcount)
                        finalcount = 1;
                    else
                        finalcount = 0;
                }
            }
            return finalcount;
        }
    }
}