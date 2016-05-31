using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class PaymentProcessing : System.Web.UI.Page
    {
        decimal Total = 0;
        int CountCheckBoxes = 0;
        int ipending = 0, isent = 0, iapp = 0;

        /// <summary>
        /// Check User permitions
        /// Set Page Header,set attributes to link buttons. 
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

            Page.Response.Cache.SetNoStore();

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }

            if (Session["PROJECT_CODE"] != null)
            {
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Fund Processing Info ";
            }
            else
            {
                Response.Redirect(ResolveUrl("~/UI/Project/ViewProjects.aspx"));
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect(ResolveUrl("~/UI/Compensation/PAPList.aspx"));
            }

            caldpcDeliveredDate.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                Clear();
                BindFixedCostCentre();
                BindBanks();
                LoadSummery();
                ModeOfPayment("0");
                txtFacilitation.Attributes.Add("onchange", "HideCheckBox();");
                txtLandPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtCashLand.ClientID + "', '" + lblLandPaid.ClientID + "');");
                txtResidentialPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtResStructure.ClientID + "', '" + lblResidentialPaid.ClientID + "');");
                txtFixturesPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtFixture.ClientID + "', '" + lblFixturesPaid.ClientID + "');");
                txtCropsPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtCrops.ClientID + "', '" + lblCropsPaid.ClientID + "');");
                txtCulturePending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtCulutralProperty.ClientID + "', '" + lblCulturePaid.ClientID + "');");
                txtDamagedPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtDamaged.ClientID + "', '" + lblDamagedPaid.ClientID + "');");
                txtFacilitationPending.Attributes.Add("onblur", "return CheckAmount(this, '" + txtFacilitation.ClientID + "', '" + lblFacilitationPaid.ClientID + "');");
                txtNegotiatedPending.Attributes.Add("onblur", "return CheckAmount(this, '" + lblNegotiatedAmount.ClientID + "', '" + lblNegotiatedPaid.ClientID + "');");
                DisableAllTextBox(false);
                dependencyStatusCheck();
                BindGrid(true, true);
                getAppoverReqStatusPakClos();
                txtPaymentAmount.Attributes.Add("Readonly", "Readonly");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PAYMENT_PROCESSING_INFO) == false)
                {
                    lnkPaymentVerification.Visible = false;
                    btnPaymentClear.Visible = false;
                    btnPaymentSave.Visible = false;
                    btnSummeryClear.Visible = false;
                    btnSummerySave.Visible = false;
                    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 1].Visible = false;
                    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 2].Visible = false;
                }
            }

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
                lnkPaymentVerification.Visible = false;
                btnPaymentClear.Visible = false;
                btnPaymentSave.Visible = false;
                btnSummeryClear.Visible = false;
                btnSummerySave.Visible = false;
                grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 1].Visible = false;
                grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 2].Visible = false;
            }
        }

        /// <summary>
        /// getAppoverReqStatusPakClos 
        /// </summary>
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
                oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                int HHID = Convert.ToInt32(Session["HH_ID"]);
                oHouseHold.HhId = HHID;
                oHouseHold.PageCode = "CREND";
                oHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalFL;

                oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);//get Status of Request

                int ApproverStatus = CheckAllApproverLevels("CREND", UtilBO.WorkflowChangeRequestApprovalFL);

                if (oHouseHold != null && ApproverStatus != 1)//oHouseHold.ApproverStatus != 1)
                {
                    if (checkGrievanceRequet())
                    {
                        if ((oHouseHold) != null)
                        {
                            //When Request is sent for Approval then check the Status of Responce from the Approver
                            if (ApproverStatus == 3)//if (oHouseHold.ApproverStatus == 3)
                            {
                                pnlPaymentDetail.Visible = false;
                                btnSummerySave.Visible = false;
                                btnSummeryClear.Visible = false;
                                DisableAllCheckBox();
                            }
                            else if (ApproverStatus == 2) // if (oHouseHold.ApproverStatus == 2)
                            {
                                //DECLINED=2
                            }
                            else if (ApproverStatus == 1) //if (oHouseHold.ApproverStatus == 1)
                            {
                                pnlPaymentDetail.Visible = false;
                                btnSummerySave.Visible = false;
                                btnSummeryClear.Visible = false;
                                DisableAllCheckBox();
                            }
                        }
                    }
                }
                else if (oHouseHold != null && oHouseHold.ApproverStatus == 1)
                {
                    pnlPaymentDetail.Visible = false;
                    btnSummerySave.Visible = false;
                    btnSummeryClear.Visible = false;
                    DisableAllCheckBox();
                }
            }
        }

        /// <summary>
        /// to Disable All TextBox
        /// </summary>
        /// <param name="Status"></param>
        private void DisableAllTextBox(bool Status)
        {
            txtLandPending.Enabled = Status;
            txtResidentialPending.Enabled = Status;
            txtFixturesPending.Enabled = Status;
            txtCropsPending.Enabled = Status;
            txtCulturePending.Enabled = Status;
            txtDamagedPending.Enabled = Status;
            txtFacilitationPending.Enabled = Status;
        }

        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSummerySave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region ViewStates/Sessions/Properties
        private int SessionHHID
        {
            get
            {
                if (Session["HH_ID"] != null)
                    return Convert.ToInt32(Session["HH_ID"]);
                else
                    return 0;
            }
            set
            {
                Session["HH_ID"] = value;
            }
        }
        private int SessionUserId
        {
            get
            {
                if (Session["USER_ID"] != null)
                    return Convert.ToInt32(Session["USER_ID"]);
                else
                    return 0;
            }
            //set
            //{
            //    Session["USER_ID"] = value;
            //}
        }
        private string ViewStateDeliveredDate
        {
            get
            {
                if (dpcDeliveredDate.Text == null)
                    return "";
                else
                    return dpcDeliveredDate.Text.ToString();
            }
        }

        private int ViewStateCompPaymentID
        {
            get
            {
                if (ViewState["CompPaymentID"] != null)
                    return Convert.ToInt32(ViewState["CompPaymentID"].ToString());
                else
                    return 0;
            }
            set { ViewState["CompPaymentID"] = value; }
        }

        private string PaymentDeliveredDate
        {
            get
            {
                if (dpcDeliveredDate.Text != null)
                {
                    return dpcDeliveredDate.Text.ToString();
                }
                else
                    return string.Empty;
            }
        }
        #endregion

        #region DropDrown Selected index Changed
        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCompensationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModeOfPayment(ddlCompensationType.SelectedValue);
            //if (ddlCompensationType.SelectedIndex == 1 && ddlCompensationType.SelectedValue.ToLower() == "Cash".ToLower())
            //{
            //    rfvPaymentAmount.Enabled = true;
            //    txtPaymentAmount.Enabled = true;
            //}
            //else
            //{
            //    rfvPaymentAmount.Enabled = false;
            //    txtPaymentAmount.Enabled = false;
            //}
            CompensationType();
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CompensationType();//If Compensation Mode Changes
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblDeliveredToStakeholder_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmountDelivered();
        }

        /// <summary>
        /// get compensation type
        /// </summary>
        private void CompensationType()
        {
            //if (ddlCompensationType.SelectedValue != "0" && ddlPaymentMode.SelectedValue != "0")
            //{
            if (ddlCompensationType.SelectedValue.ToLower() == "Cash".ToLower())
            {
                txtPaymentAmount.Enabled = true;
            }
            else
            {
                txtPaymentAmount.Enabled = false;
                txtPaymentAmount.Text = string.Empty;
            }

            //if (ddlPaymentMode.SelectedItem.Text.ToUpper() == "CHEQUE" || ddlPaymentMode.SelectedItem.Text.ToUpper() == "EFT")
            //{
            //    //ChequePaymentRow.Style["display"] = "block";

            //    if (ddlPaymentMode.SelectedItem.Text.ToUpper() == "CHEQUE")
            //        lblBankReference.Text = "Cheque No.";
            //    else if (ddlPaymentMode.SelectedItem.Text.ToUpper() == "EFT")
            //        lblBankReference.Text = "Reference No.";
            //}
            //else
            //{
            //    //ChequePaymentRow.Style["display"] = "none";
            //}
        }

        /// <summary>
        /// Check amount deliverd
        /// </summary>
        private void AmountDelivered()
        {
            if (rblDeliveredToStakeholder.SelectedValue == "0")
            {
                dpcDeliveredDate.Enabled = false;
                dpcDeliveredDate.Text = "";
            }
            else
                dpcDeliveredDate.Enabled = true;
        }

        #endregion

        #region Clear Buttons
        /// <summary>
        /// to clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPaymentClear_Click(object sender, EventArgs e)
        {
            ClearPaymentMode();
            LoadSummery();
            ClearSummery();
        }


        /// <summary>
        /// to clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSummeryClear_Click(object sender, EventArgs e)
        {
            //ClearSummery();
            txtInKindLand.Text = string.Empty;
        }

        /// <summary>
        /// to clear data
        /// </summary>
        private void ClearSummery()
        {
            chkLandValuation.Checked = false;
            chkReplacementHouseValue.Checked = false;
            chkFixtureValuation.Checked = false;
            chkCropsValuation.Checked = false;
            chkDamagedCropValue.Checked = false;
            chkCulturePropertyValue.Checked = false;
            chkFacilitation.Checked = false;
            chkFinalCompensation.Checked = false;
            chkNegotiatedAmount.Checked = false;
            txtLandPending.Text = "";
            txtResidentialPending.Text = "";
            txtFixturesPending.Text = "";
            txtCropsPending.Text = "";
            txtCulturePending.Text = "";
            txtDamagedPending.Text = "";
            txtFacilitationPending.Text = "";
            txtNegotiatedPending.Text = "";
            txtLandPending.Enabled = false;
            txtResidentialPending.Enabled = false;
            txtFixturesPending.Enabled = false;
            txtCropsPending.Enabled = false;
            txtCulturePending.Enabled = false;
            txtDamagedPending.Enabled = false;
            txtFacilitationPending.Enabled = false;
            txtNegotiatedPending.Enabled = false;
        }

        /// <summary>
        /// to clear data
        /// </summary>
        private void Clear()
        {
            ClearSummery();
            ClearPaymentMode();
        }

        /// <summary>
        /// to clear data
        /// </summary>
        private void ClearPaymentMode()
        {
            dpcDeliveredDate.Text = "";
            ddlCompensationType.SelectedIndex = 0;
            ddlPaymentMode.SelectedIndex = 0;

            CompensationType();
            rblDeliveredToStakeholder.SelectedValue = "No";
            txtPaymentAmount.Text = string.Empty;
            dpcDeliveredDate.Enabled = false;

            dpcDeliveredDate.Text = "";

            ddlBank.ClearSelection();
            BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));
            txtBankReference.Text = "";
            ddlFixedCostCentre.ClearSelection();

            AmountDelivered();
            btnPaymentSave.Text = "Save";
            btnPaymentClear.Text = "Clear";
            ViewStateCompPaymentID = 0;
            // CompensationType();
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// Load Summery Data
        /// </summary>
        private void LoadSummery()
        {
            //CompensationFinancialBO oCompensationFinancial = new CompensationFinancialBO();

            //oCompensationFinancial.HHID = Convert.ToInt32(SessionHHID);
            CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();

            CompensationFinancialBO oCompensationFinancial = oCompensationFinancialBLL.GetCompensationFinancial(SessionHHID);
            if (oCompensationFinancial != null)
            {
                txtCashLand.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation);
                txtFixture.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixtureTotalValuation);
                txtCrops.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropTotalValuation);
                txtResStructure.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResPayment);
                txtCulutralProperty.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropValuation);
                txtDamaged.Text = UtilBO.CurrencyFormat(oCompensationFinancial.DamagedCropValuation);
                txtInKindLand.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandInKindCompensation);
                //lblTotalAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.TotalValuation);
                lblTotalAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation + oCompensationFinancial.FixtureTotalValuation +
                    oCompensationFinancial.CropTotalValuation + oCompensationFinancial.ResPayment +
                    oCompensationFinancial.DamagedCropValuation + oCompensationFinancial.CulturePropValuation + oCompensationFinancial.FacilitationAllowance); //oCompensationFinancial.TotalValuation.ToString();
                lblNegotiatedAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.NegotiatedAmount);

                //new
                lblLandPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandPaidValuation);
                lblFixturesPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixturePaidValuation);
                lblCropsPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropPaidValuation);
                lblResidentialPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResPaidValuation);
                lblCulturePaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropPaidValuation);
                lblDamagedPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.DamagedCropPaidValuation);
                lblFacilitationPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FacilitationAllowancePaid);
                lblNegotiatedPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancial.NegotiatedAmountPaid);
                //end
                lblTestToalAmount.Text = lblTotalAmount.Text;


                ddlResidentialStructure.ClearSelection();
                if (!string.IsNullOrEmpty(oCompensationFinancial.ResInKindCompensation)
                    && ddlResidentialStructure.Items.FindByValue(oCompensationFinancial.ResInKindCompensation) != null)
                {
                    ddlResidentialStructure.SelectedValue = oCompensationFinancial.ResInKindCompensation;
                }
                else
                    ddlResidentialStructure.SelectedValue = "0";

                //if (string.IsNullOrEmpty(oCompensationFinancial.ResInKindCompensation))
                //    ddlResidentialStructure.SelectedValue = "0";
                //else
                //    ddlResidentialStructure.SelectedValue = oCompensationFinancial.ResInKindCompensation.ToString();

                SessionHHID = oCompensationFinancial.HHID;
                //ResidentialStructure();
            }
            LoadCompensationFinancial();
            GetPAPValuationSummery_NegotiatedAmount();
            //DISABLE ALL TEXTBOXES IN SUMMERY FOR EDITING 
            txtCashLand.Attributes.Add("readonly", "readonly");
            txtFixture.Attributes.Add("readonly", "readonly");
            txtCrops.Attributes.Add("readonly", "readonly");
            txtResStructure.Attributes.Add("readonly", "readonly");
            txtCulutralProperty.Attributes.Add("readonly", "readonly");
            txtDamaged.Attributes.Add("readonly", "readonly");
            txtInKindLand.Attributes.Add("readonly", "readonly");
            txtFacilitation.Attributes.Add("readonly", "readonly");
            ddlResidentialStructure.Enabled = false;
            //txtCashLand.Enabled = false;
            //txtFixture.Enabled = false;
            //txtCrops.Enabled = false;
            //txtResStructure.Enabled = false;
            //txtCulutralProperty.Enabled = false;
            //txtDamaged.Enabled = false;
            //txtInKindLand.Enabled = true;

            string LAS = string.Empty;
            string FAS = string.Empty;
            string CAL = string.Empty;
            string RAL = string.Empty;
            string DAL = string.Empty;
            string CPAL = string.Empty;
            string OPAL = string.Empty;
            string FCAL = string.Empty;
            string NAAS = string.Empty;

            if (oCompensationFinancial != null)
            {
                LAS = oCompensationFinancial.Land_Approval_Status;
                FAS = oCompensationFinancial.Fixture_Approval_Status;
                CAL = oCompensationFinancial.Crop_Approval_Status;
                RAL = oCompensationFinancial.Replacment_Approval_Status;
                DAL = oCompensationFinancial.Damaged_Approval_Status;
                CPAL = oCompensationFinancial.Culture_Approval_Status;
                OPAL = oCompensationFinancial.Facilitation_Approval_Status;
                FCAL = oCompensationFinancial.Final_Approval_Status;
                NAAS = oCompensationFinancial.Nego_Amount_Approval_Status;
            }

            CountCheckBoxes = 0;

            if (LAS == null || LAS.Trim() == string.Empty || (LAS.Length > 3 && LAS.Substring(0, 3).ToUpper() != "APP"))
                chkLandValuation.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (FAS == null || FAS.Trim() == string.Empty || (FAS.Length > 3 && FAS.Substring(0, 3).ToUpper() != "APP"))
                chkFixtureValuation.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (CAL == null || CAL.Trim() == string.Empty || (CAL.Length > 3 && CAL.Substring(0, 3).ToUpper() != "APP"))
                chkCropsValuation.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (RAL == null || RAL.Trim() == string.Empty || (RAL.Length > 3 && RAL.Substring(0, 3).ToUpper() != "APP"))
                chkReplacementHouseValue.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (DAL == null || DAL.Trim() == string.Empty || (DAL.Length > 3 && DAL.Substring(0, 3).ToUpper() != "APP"))
                chkDamagedCropValue.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (CPAL == null || CPAL.Trim() == string.Empty || (CPAL.Length > 3 && CPAL.Substring(0, 3).ToUpper() != "APP"))
                chkCulturePropertyValue.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            if (OPAL == null || OPAL.Trim() == string.Empty || (OPAL.Length > 3 && OPAL.Substring(0, 3).ToUpper() != "APP"))
                chkFacilitation.Style.Add("display", "none");
            else
                CountCheckBoxes++;

            //if (CountCheckBoxes == 0)
            //    chkFacilitation.Style.Add("display", "none");
            //else
            //    chkFacilitation.Style.Add("display", "");


            if (NAAS == null || NAAS.Trim() == string.Empty || (NAAS.Length > 3 && NAAS.Substring(0, 3).ToUpper() != "APP"))
            {
                chkNegotiatedAmount.Style.Add("display", "none");
                chkFinalCompensation.Style.Add("display", "none"); //"visibility" "hidden"
            }
            else
            {
                CountCheckBoxes = 1;
                chkFinalCompensation.Style.Add("display", "none"); //All time Disable Final Compensation
            }

            //if (FCAL == null || FCAL.Trim() == string.Empty || (FCAL.Length > 3 && FCAL.Substring(0, 3).ToUpper() != "APP"))
            //{
            //    // DisableAllCheckBox();
            //    chkNegotiatedAmount.Style.Add("display", "none"); //"visibility" "hidden"
            //}
            //else
            //{
            //    CountCheckBoxes = 1;
            //}
            if (oCompensationFinancial != null)
            {
                if (oCompensationFinancial.LandTotalValuation == oCompensationFinancial.LandPaidValuation)
                {
                    chkLandValuation.Style.Add("display", "none");
                }
                if (oCompensationFinancial.FixtureTotalValuation == oCompensationFinancial.FixturePaidValuation)
                {
                    chkFixtureValuation.Style.Add("display", "none");
                }

                if (oCompensationFinancial.CropTotalValuation == oCompensationFinancial.CropPaidValuation)
                {
                    chkCropsValuation.Style.Add("display", "none");
                }
                if (oCompensationFinancial.ResPayment == oCompensationFinancial.ResPaidValuation)
                {
                    chkReplacementHouseValue.Style.Add("display", "none");
                }

                if (oCompensationFinancial.CulturePropValuation == oCompensationFinancial.CulturePropPaidValuation)
                {
                    chkCulturePropertyValue.Style.Add("display", "none");
                }
                if (oCompensationFinancial.DamagedCropValuation == oCompensationFinancial.DamagedCropPaidValuation)
                {
                    chkDamagedCropValue.Style.Add("display", "none");
                }

                if (txtFacilitation.Text.Trim().Length == 0)
                {
                    chkFacilitation.Style.Add("display", "none");
                }
                else if (Convert.ToDecimal(txtFacilitation.Text.Trim()) == oCompensationFinancial.FacilitationAllowancePaid)
                {
                    chkFacilitation.Style.Add("display", "none");
                }
                if (oCompensationFinancial.NegotiatedAmount == oCompensationFinancial.NegotiatedAmountPaid)
                {
                    chkNegotiatedAmount.Style.Add("display", "none");
                }
            }
        }

        protected decimal ViewStateTotalPayAmount
        {
            get
            {
                if (ViewState["TotalPayAmount"] != null)
                    return Convert.ToDecimal(ViewState["TotalPayAmount"]);
                else return 0;
            }
            set
            {
                ViewState["TotalPayAmount"] = value;
            }
        }

        /// <summary>
        /// to Disable All CheckBox
        /// </summary>
        private void DisableAllCheckBox()
        {
            chkLandValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkFixtureValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkCropsValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkReplacementHouseValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkDamagedCropValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkCulturePropertyValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkFacilitation.Style.Add("display", "none");
            //chkFinalCompensation.Style.Add("display", "none"); //"visibility" "hidden"
            //chkNegotiatedAmount.Style.Add("display", "none"); //"visibility" "hidden"
        }

        /// <summary>
        /// get Negotiated Amount
        /// </summary>
        private void GetPAPValuationSummery_NegotiatedAmount()
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO oPaymentBO = new PaymentBO();
            oPaymentBO = oPaymentBLL.getPapValuation(SessionHHID);

            if (oPaymentBO != null && oPaymentBO.NegotiatedAmountApproved.ToUpper() == "Y")
            {
                trNegotiatedAmount.Visible = true;
                //  trNegotiatedAmount.BgColor = "Silver";
            }
            else
            {
                trNegotiatedAmount.Visible = false;
                // trNegotiatedAmount.Visible = true;
                // trNegotiatedAmount.BgColor = "#F0F8FF";
            }
        }

        /// <summary>
        /// Load Compensation Financial details
        /// </summary>
        private void LoadCompensationFinancial()
        {
            //CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();
            PaymentBLL oPaymentBLL = new PaymentBLL();
            CompensationFinancialBO oCompensationFinancialBO = new CompensationFinancialBO();
            oCompensationFinancialBO = oPaymentBLL.getCompnesationFinancial(SessionHHID);
            if (oCompensationFinancialBO != null)
            {
                if (oCompensationFinancialBO.LandInKindCompensation >= 0)
                {
                    txtInKindLand.Text = oCompensationFinancialBO.LandInKindCompensation.ToString();
                }
                else
                    txtInKindLand.Text = string.Empty;
                ddlResidentialStructure.ClearSelection();
                if (!string.IsNullOrEmpty(oCompensationFinancialBO.ResInKindCompensation)
                    && ddlResidentialStructure.Items.FindByValue(oCompensationFinancialBO.ResInKindCompensation) != null)
                {
                    ddlResidentialStructure.SelectedValue = oCompensationFinancialBO.ResInKindCompensation;
                }
                else
                    ddlResidentialStructure.SelectedValue = "0";
                
                //if (oCompensationFinancialBO.TotalValuation >= 0)
                //{
                //    //lblTotalAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancialBO.TotalValuation);
                //    lblTotalAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancialBO.LandTotalValuation + oCompensationFinancialBO.FixtureTotalValuation +
                //   oCompensationFinancialBO.CropTotalValuation + oCompensationFinancialBO.ResPayment +
                //   oCompensationFinancialBO.DamagedCropValuation + oCompensationFinancialBO.CulturePropValuation + oCompensationFinancialBO.FacilitationAllowance); //oCompensationFinancial.TotalValuation.ToString();
                //}
                //else
                //    lblTotalAmount.Text = "0";

                if (oCompensationFinancialBO.FacilitationAllowance >= 0)
                    txtFacilitation.Text = UtilBO.CurrencyFormat(oCompensationFinancialBO.FacilitationAllowance);
                else
                    txtFacilitation.Text = string.Empty;

                if (oCompensationFinancialBO.FacilitationAllowancePaid >= 0)
                    lblFacilitationPaid.Text = UtilBO.CurrencyFormat(oCompensationFinancialBO.FacilitationAllowancePaid);
                else
                    lblFacilitationPaid.Text = "0";
            }
        }

        /// <summary>
        /// Bind Data to Drodown
        /// </summary>
        /// <param name="PaymentType"></param>
        private void ModeOfPayment(string PaymentType)
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentList lstPaymentList = new PaymentList();

            ddlPaymentMode.Items.Clear();

            lstPaymentList = oPaymentBLL.GetModeOfPayment(PaymentType);

            if (lstPaymentList.Count > 0)
            {
                ddlPaymentMode.DataSource = lstPaymentList;
                ddlPaymentMode.DataTextField = "ModeOfPayment";
                ddlPaymentMode.DataValueField = "ModeOfPaymentId";
                ddlPaymentMode.DataBind();
            }
            else
            {
                ddlPaymentMode.DataSource = null;
                ddlPaymentMode.DataBind();
            }
            ddlPaymentMode.Items.Insert(0, (new ListItem("--Select--", "0")));
        }
        #endregion

        #region GridView


        /// <summary>
        /// Bind Data to Grid
        /// </summary>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            isent = 0;
            iapp = 0; ipending = 0;
            PaymentBLL oPaymentBLL = new PaymentBLL();
            CompensationPayementList lstCompensationPayement = new CompensationPayementList();

            lstCompensationPayement = oPaymentBLL.getCompnesationPayment(SessionHHID);
            grdPaymentDetails.DataSource = lstCompensationPayement;
            grdPaymentDetails.DataBind();
            lnkPaymentVerification.Visible = true;
            if (grdPaymentDetails.Rows.Count > 0)
                EnablePayVerificationReqButton(true);
            else
                EnablePayVerificationReqButton(false);
            if (isent > 0)
                lnkPaymentVerification.Visible = false;
            if (lstCompensationPayement.Count == iapp)
                lnkPaymentVerification.Visible = false;
            //Added Total Amount at the end of the Payment Paid column
            lblTotalAmountPaid.Text = UtilBO.CurrencyFormat(ViewStateGridViewTotalAmount);
            if (lblTotalAmountPaid.Text.Trim() == lblTotalAmount.Text.Trim())
            {
                btnPaymentSave.Visible = false;
                btnPaymentClear.Visible = false;
            }
        }

        /// <summary>
        /// Edit And Delete data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewStateCompPaymentID = Convert.ToInt32(e.CommandArgument);
                getPaymentById();
                btnPaymentSave.Text = "Update";
                btnPaymentClear.Text = "Cancel";
                btnPaymentSave.Visible = true;
                btnPaymentClear.Visible = true;
                //SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewStateCompPaymentID = Convert.ToInt32(e.CommandArgument);
                //ViewState["PaymentRequestId"] = e.CommandArgument;
                DeleteCompositionPayment();//Deleting Composition Payment
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Deleted", "alert('Data deleted successfully');", true);
                LoadSummery();
                ClearSummery();
                BindGrid(true, true);
            }
        }

        /// <summary>
        /// change page index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdPaymentDetails.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }

        private decimal ViewStateGridViewTotalAmount
        {
            get
            {
                if (ViewState["TotalAmount"] != null)
                    return Convert.ToDecimal(ViewState["TotalAmount"].ToString());
                return 0;
            }
            set
            {
                ViewState["TotalAmount"] = value;
            }
        }

        /// <summary>
        /// Format data inside grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentDetails_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblCompType = e.Row.FindControl("lblCompensationType") as Label;
                Label lbModeOfPayment = e.Row.FindControl("lblModeOfPayment") as Label;

                Literal ltModeOfPayment = e.Row.FindControl("ltrlModeOfPayment") as Literal;
                Literal ltInKindType = e.Row.FindControl("ltrlInKindType") as Literal;

                if (!string.IsNullOrEmpty(lblCompType.Text))
                {
                    if (lblCompType.Text.ToLower() == "Cash".ToLower())
                    {
                        ltModeOfPayment.Text = lbModeOfPayment.Text;
                    }
                    else
                    {
                        ltInKindType.Text = lbModeOfPayment.Text;
                    }
                }

                Label lbCompensationAmount = e.Row.FindControl("lblCompensationAmount") as Label;
                HiddenField hdnTotalAmount = e.Row.FindControl("hdnTotalAmount") as HiddenField;
                string strPaymentAmt = "";
                decimal paymentAmount;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    strPaymentAmt = DataBinder.Eval(e.Row.DataItem, "CompensationAmount").ToString();

                    if (decimal.TryParse(strPaymentAmt, out paymentAmount))
                    {
                        Total += paymentAmount;
                        lbCompensationAmount.Text = UtilBO.CurrencyFormat(paymentAmount);
                    }

                    //Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CompensationAmount"));

                    if (e.Row.FindControl("hdnTotalAmount") is HiddenField && hdnTotalAmount != null)
                    {
                        hdnTotalAmount.Value = Total.ToString();
                        ViewStateGridViewTotalAmount = Convert.ToDecimal(hdnTotalAmount.Value);
                    }
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblamount = (Label)e.Row.FindControl("lblTotal");
                    lblamount.Text = Total.ToString();//BO.PaymentBO.CompensationPayementBO.TotalCompensationAmount.ToString();               
                }

                //if (checkPaymentRequestStatus() == 1 || checkPaymentRequestStatus() == 3)
                //{
                //    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 1].Visible = false;
                //    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 2].Visible = false;
                //    pnlPaymentDetail.Visible = false;
                //}
                //else
                //{
                //    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 1].Visible = true;
                //    grdPaymentDetails.Columns[grdPaymentDetails.Columns.Count - 2].Visible = true;
                //    pnlPaymentDetail.Visible = true;
                //}
                Literal FundReqStatus = (Literal)e.Row.FindControl("litFundReqStatus");
                if (FundReqStatus.Text.ToLower() == "Approved".ToLower() || FundReqStatus.Text.ToLower() == "Sent for Approval".ToLower())
                {
                    ImageButton Edit = (ImageButton)e.Row.FindControl("imgEdit");
                    ImageButton Delete = (ImageButton)e.Row.FindControl("imgDelete");
                    Edit.Visible = false;
                    Delete.Visible = false;
                }
                if (FundReqStatus.Text.ToLower() == "Approved".ToLower())
                {
                    iapp++;
                }
                if (FundReqStatus.Text.ToLower() == "Sent for Approval".ToLower())
                {
                    isent++;
                }
                if (FundReqStatus.Text.ToLower() == "Pending Approval".ToLower())
                {
                    ipending++;
                    //checkApprovalExitOrNot();
                }
                else
                {
                    //lnkPaymentVerification.Visible = false;
                }
            }
        }

        /// <summary>
        /// send request to approve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            int getResult;
            //openEmailPopupWindow();
            getResult = sentforapproval();
            if (getResult == -1)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approval Notification has been sent');", true);
            }
            BindGrid(true, false);
        }
        /// <summary>
        /// Check sent for approval
        /// </summary>
        /// <returns></returns>
        public int sentforapproval()
        {
            int Result = 0;

            PaymentBLL oPaymentBLL = new PaymentBLL();
            Result = oPaymentBLL.SendforApproval(SessionHHID);
            return Result;
        }

        /// <summary>
        /// format data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPaymentDetails_RowCreated(Object sender, GridViewRowEventArgs e)
        {
            // Retrieve the current row. 
            GridViewRow row = e.Row;
            HiddenField hdnTotalAmount = (HiddenField)e.Row.FindControl("hdnTotalAmount");
            //string TotalAmount = string.Empty;

            //if (e.Row.FindControl("hdnTotalAmount") is HiddenField && hdnTotalAmount != null)
            //    TotalAmount = hdnTotalAmount.Value; //PaymentBO.CompensationPayementBO.TotalCompensationAmount.ToString();

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("lblTotal");

                lblAmount.Text = UtilBO.CurrencyFormat(ViewStateGridViewTotalAmount);

                //Compensation Status Calculation
                //decimal PaidAmount = Total;
                //decimal AcutalAmountToBePaid = 0;

                //if (!string.IsNullOrEmpty(lblTotalAmount.Text))
                //    AcutalAmountToBePaid = Convert.ToDecimal(lblTotalAmount.Text);

                //if (AcutalAmountToBePaid == PaidAmount)
                //    ddlCompensationStatus.SelectedValue = "CP";
                //else if (AcutalAmountToBePaid > PaidAmount)
                //    ddlCompensationStatus.SelectedValue = "PP";
                //else //(PaidAmount == 0)
                //    ddlCompensationStatus.SelectedValue = "NP";

                Total = 0;
            }
        }
        #endregion

        #region Save/Update/Delete/Retrieve Section
        /// <summary>
        /// Save data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSummerySave_Click(object sender, EventArgs e)
        {
            CompensationFinancialBO oCompensationFinancial = new CompensationFinancialBO();
            CompensationFinancialBLL oCompensationFinancialBLL;

            oCompensationFinancialBLL = new CompensationFinancialBLL();
            oCompensationFinancial.HHID = SessionHHID;
            if (ddlResidentialStructure.SelectedItem.ToString() == "0")
                oCompensationFinancial.ResInKindCompensation = "-1";
            else
                oCompensationFinancial.ResInKindCompensation = ddlResidentialStructure.SelectedItem.ToString();
            oCompensationFinancial.UpdatedBy = SessionUserId;

            string AlertMessage = string.Empty;
            string ResultStatus = oCompensationFinancialBLL.UpdateCompFinancial_ClosingInfo(oCompensationFinancial);

            //UPDATING FINANCIAL COMPENSATION
            PaymentBLL oPaymentBLL = new PaymentBLL();
            CompensationFinancialBO oCompensationFinancialBO = new CompensationFinancialBO();
            oCompensationFinancialBO.HHID = SessionHHID;
            oCompensationFinancialBO.ResInKindCompensation = ddlResidentialStructure.SelectedValue;

            if (!string.IsNullOrEmpty(txtInKindLand.Text))
            { //LAND In-Kind Compensation saved 
                oCompensationFinancialBO.LandInKindCompensation = Convert.ToDecimal(txtInKindLand.Text);
            }

            if (!string.IsNullOrEmpty(txtFacilitation.Text))
                oCompensationFinancialBO.FacilitationAllowance = Convert.ToDecimal(txtFacilitation.Text);
            oCompensationFinancialBO.UpdatedBy = SessionUserId;
            string message2 = oPaymentBLL.UpdateCompensationFinancial(oCompensationFinancialBO);

            AlertMessage = "alert('" + ResultStatus + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            //LoadSummery();
            LoadCompensationFinancial();
            BindGrid(true, true);
            ClearSummery();
            ClearPaymentMode();
            LoadSummery();
        }

        /// <summary>
        /// Save data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPaymentSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            string[] PaymentId;
            bool paymentSuccess = false;

            PaymentBO.CompensationPayementBO oCompensationPayementBO = new PaymentBO.CompensationPayementBO();

            oCompensationPayementBO.CompensationType = ddlCompensationType.SelectedItem.Text;
            oCompensationPayementBO.HHID = SessionHHID;
            oCompensationPayementBO.CompPaymentId = Convert.ToInt32(ddlPaymentMode.SelectedValue.ToString());

            oCompensationPayementBO.ModeOfPaymentId = Convert.ToInt32(ddlPaymentMode.SelectedValue.ToString());

            if (!string.IsNullOrEmpty(txtPaymentAmount.Text))
                oCompensationPayementBO.CompensationAmount = Convert.ToDecimal(txtPaymentAmount.Text);
            else
                oCompensationPayementBO.CompensationAmount = 0;

            oCompensationPayementBO.DeliveredToStakeHolder = rblDeliveredToStakeholder.SelectedItem.Text;

            if (string.IsNullOrEmpty(dpcDeliveredDate.Text))
                oCompensationPayementBO.DeliveredDate = "";
            else
                oCompensationPayementBO.DeliveredDate = ViewStateDeliveredDate;

            oCompensationPayementBO.BankID = Convert.ToInt32(ddlBank.SelectedItem.Value);
            oCompensationPayementBO.BranchID = Convert.ToInt32(ddlBranch.SelectedItem.Value);
            oCompensationPayementBO.BankReference = txtBankReference.Text.Trim();
            oCompensationPayementBO.FixedCostCentreID = Convert.ToInt32(ddlFixedCostCentre.SelectedItem.Value);

            oCompensationPayementBO.IsDeleted = "False";

            decimal TotalAmount = 0, PayingAmount = 0;
            if (!string.IsNullOrEmpty(lblTestToalAmount.Text))
                TotalAmount = Convert.ToDecimal(lblTestToalAmount.Text);

            if (!string.IsNullOrEmpty(txtPaymentAmount.Text))
                PayingAmount = Convert.ToDecimal(txtPaymentAmount.Text);

            PaymentBLL oPaymentBLL = new PaymentBLL();
            if (PayingAmount <= TotalAmount)// (TotalAmount > 0)
            {
                if (ViewStateCompPaymentID <= 0)
                {
                    oCompensationPayementBO.CreatedBy = SessionUserId;

                    PaymentId = oPaymentBLL.AddCompnesationPayment(oCompensationPayementBO);
                    message = PaymentId[0];
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearPaymentMode();
                        paymentSuccess = true;
                    }
                }
                else
                {
                    PaymentId = new string[2];
                    PaymentId[0] = message;
                    PaymentId[1] = ViewStateCompPaymentID.ToString();
                    oCompensationPayementBO.UpdatedBy = SessionUserId;
                    oCompensationPayementBO.CompPaymentId = ViewStateCompPaymentID;
                    message = oPaymentBLL.UpdateCompositionPayment(oCompensationPayementBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearPaymentMode();
                        paymentSuccess = true;
                    }
                }

                // For Update In Cmp Fina Table.
                if (paymentSuccess && (chkLandValuation.Checked || chkReplacementHouseValue.Checked || chkCropsValuation.Checked ||
                    chkFixtureValuation.Checked || chkDamagedCropValue.Checked || chkCulturePropertyValue.Checked ||
                    chkFacilitation.Checked || chkNegotiatedAmount.Checked))
                {
                    CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();
                    CompensationFinancialBO oCompensationFinancial = new CompensationFinancialBO();
                    oCompensationFinancial.HHID = SessionHHID;

                    int compenPaymentID = 0;

                    if (Int32.TryParse(PaymentId[1], out compenPaymentID))
                        oCompensationFinancial.CompPaymentId = compenPaymentID;
                    else
                        oCompensationFinancial.CompPaymentId = 0;

                    if (chkLandValuation.Checked)
                    {
                        oCompensationFinancial.LandPaidValuation = Convert.ToDecimal(txtLandPending.Text);
                    }
                    else
                        oCompensationFinancial.LandPaidValuation = 0;
                    if (chkReplacementHouseValue.Checked)
                    {
                        oCompensationFinancial.ResPaidValuation = Convert.ToDecimal(txtResidentialPending.Text);
                    }
                    else
                        oCompensationFinancial.ResPaidValuation = 0;
                    if (chkFixtureValuation.Checked)
                        oCompensationFinancial.FixturePaidValuation = Convert.ToDecimal(txtFixturesPending.Text);
                    else
                        oCompensationFinancial.FixturePaidValuation = 0;
                    if (chkCropsValuation.Checked)
                        oCompensationFinancial.CropPaidValuation = Convert.ToDecimal(txtCropsPending.Text);
                    else
                        oCompensationFinancial.CropPaidValuation = 0;
                    if (chkCulturePropertyValue.Checked)
                        oCompensationFinancial.CulturePropPaidValuation = Convert.ToDecimal(txtCulturePending.Text);
                    else
                        oCompensationFinancial.CulturePropPaidValuation = 0;
                    if (chkDamagedCropValue.Checked)
                        oCompensationFinancial.DamagedCropPaidValuation = Convert.ToDecimal(txtDamagedPending.Text);
                    else
                        oCompensationFinancial.DamagedCropPaidValuation = 0;
                    if (chkFacilitation.Checked)
                        oCompensationFinancial.FacilitationAllowancePaid = Convert.ToDecimal(txtFacilitationPending.Text);
                    else
                        oCompensationFinancial.FacilitationAllowancePaid = 0;
                    if (chkNegotiatedAmount.Checked)
                        oCompensationFinancial.NegotiatedAmountPaid = Convert.ToDecimal(txtNegotiatedPending.Text);
                    else
                        oCompensationFinancial.NegotiatedAmountPaid = 0;
                    oCompensationFinancialBLL.UpdateCompensationFinancialPayment(oCompensationFinancial);
                }
                // End 
            }
            else
                message = "Amount exceeds Total Amount payable.";

            LoadSummery();
            ClearSummery();
            BindGrid(true, true);
            //checkApprovalExistsForPaymentVerification();
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        /// <summary>
        /// To delete data
        /// </summary>
        private void DeleteCompositionPayment()
        {
            PaymentBLL oBatchBLL = new PaymentBLL();
            int CompPaymentId = 0;
            int Result = 0;

            CompPaymentId = ViewStateCompPaymentID;

            Result = oBatchBLL.DeleteCompositionPayment(CompPaymentId);
            BindGrid(false, true);
            ClearPaymentMode();
            ClearSummery();
            LoadSummery();
        }

        /// <summary>
        /// get Payment details By Id
        /// </summary>
        private void getPaymentById()
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO.CompensationPayementBO oComPayementBO = new PaymentBO.CompensationPayementBO();

            oComPayementBO = oPaymentBLL.getCompensationPaymentById(ViewStateCompPaymentID);//get By CompensationStateId

            ddlCompensationType.SelectedValue = oComPayementBO.CompensationType.ToString();
            //Call Method for Loading the Mode of Payment
            ModeOfPayment(ddlCompensationType.SelectedValue);

            if (ddlCompensationType.Items.Count > 0)
            {
                ddlPaymentMode.SelectedValue = oComPayementBO.ModeOfPaymentId.ToString();
            }

            if (!string.IsNullOrEmpty(oComPayementBO.DeliveredDate))
                dpcDeliveredDate.Text = Convert.ToString(oComPayementBO.DeliveredDate);

            ddlBank.ClearSelection();
            if (ddlBank.Items.FindByValue(oComPayementBO.BankID.ToString()) != null)
            {
                ddlBank.Items.FindByValue(oComPayementBO.BankID.ToString()).Selected = true;

                BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));

                if (ddlBranch.Items.FindByValue(oComPayementBO.BranchID.ToString()) != null)
                {
                    ddlBranch.ClearSelection();
                    ddlBranch.Items.FindByValue(oComPayementBO.BranchID.ToString()).Selected = true;
                }
            }
            txtBankReference.Text = oComPayementBO.BankReference;

            ddlFixedCostCentre.ClearSelection();
            if (ddlFixedCostCentre.Items.FindByValue(oComPayementBO.FixedCostCentreID.ToString()) != null)
            {
                ddlFixedCostCentre.Items.FindByValue(oComPayementBO.FixedCostCentreID.ToString()).Selected = true;
            }
            txtPaymentAmount.Text = UtilBO.CurrencyFormat(oComPayementBO.CompensationAmount);

            if (oComPayementBO.DeliveredToStakeHolder.ToLower() == "yes".ToLower())
            {
                rblDeliveredToStakeholder.SelectedValue = "Yes";
                dpcDeliveredDate.Enabled = true;
            }
            else
            {
                rblDeliveredToStakeholder.SelectedValue = "No";
                dpcDeliveredDate.Enabled = false;
            }
            CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();
            CompensationFinancialBO oCompensationFinancial = oCompensationFinancialBLL.GetCompensationFinancialById(ViewStateCompPaymentID, SessionHHID);
            if (oCompensationFinancial != null)
            {
                ClearSummery();
                if (oCompensationFinancial.LandPaidValuation != 0)
                {
                    chkLandValuation.Checked = true;
                    lblLandPaid.Text = (Convert.ToDecimal(lblLandPaid.Text) - oCompensationFinancial.LandPaidValuation).ToString();
                    txtLandPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandPaidValuation);
                    txtLandPending.Enabled = true;
                }

                if (oCompensationFinancial.ResPaidValuation != 0)
                {
                    chkReplacementHouseValue.Checked = true;
                    lblResidentialPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblResidentialPaid.Text) - oCompensationFinancial.ResPaidValuation);
                    txtResidentialPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResPaidValuation);
                    txtResidentialPending.Enabled = true;
                }
                if (oCompensationFinancial.FixturePaidValuation != 0)
                {
                    chkFixtureValuation.Checked = true;
                    lblFixturesPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblFixturesPaid.Text) - oCompensationFinancial.FixturePaidValuation);
                    txtFixturesPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixturePaidValuation);
                    txtFixturesPending.Enabled = true;
                }

                if (oCompensationFinancial.CropPaidValuation != 0)
                {
                    chkCropsValuation.Checked = true;
                    lblCropsPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblCropsPaid.Text) - oCompensationFinancial.CropPaidValuation);
                    txtCropsPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropPaidValuation);
                    txtCropsPending.Enabled = true;
                }
                if (oCompensationFinancial.CulturePropPaidValuation != 0)
                {
                    chkCulturePropertyValue.Checked = true;
                    lblCulturePaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblCulturePaid.Text) - oCompensationFinancial.CulturePropPaidValuation);
                    txtCulturePending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropPaidValuation);
                    txtCulturePending.Enabled = true;
                }

                if (oCompensationFinancial.DamagedCropPaidValuation != 0)
                {
                    chkDamagedCropValue.Checked = true;
                    lblDamagedPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblDamagedPaid.Text) - oCompensationFinancial.DamagedCropPaidValuation);
                    txtDamagedPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.DamagedCropPaidValuation);
                    txtDamagedPending.Enabled = true;
                }
                if (oCompensationFinancial.FacilitationAllowancePaid != 0)
                {
                    chkFacilitation.Checked = true;
                    lblFacilitationPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblFacilitationPaid.Text) - oCompensationFinancial.FacilitationAllowancePaid);
                    txtFacilitationPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FacilitationAllowancePaid);
                    txtFacilitationPending.Enabled = true;
                }

                if (oCompensationFinancial.NegotiatedAmountPaid != 0)
                {
                    chkNegotiatedAmount.Checked = true;
                    lblNegotiatedPaid.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(lblNegotiatedPaid.Text) - oCompensationFinancial.NegotiatedAmountPaid);
                    txtNegotiatedPending.Text = UtilBO.CurrencyFormat(oCompensationFinancial.NegotiatedAmountPaid);
                    txtNegotiatedPending.Enabled = true;
                }
            }
            CompensationType();
        }

        /// <summary>
        /// Enable or disable Buttons
        /// </summary>
        /// <param name="val"></param>
        private void EnableButtons(bool val)
        {
            pnlPaymentMode.Visible = val;
            pnlSummery.Visible = val;
        }

        #endregion

        #region Change Request for Payment Verification
        /// <summary>
        /// check Approval Exists For Payment Verification
        /// </summary>
        public void checkApprovalExistsForPaymentVerification()
        {
            #region Enable ChangeRequest Button
            //lblStatusValuationPCI.Text = string.Empty;
            //lblStatusValuationPCI.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.PaymentVerificationCode;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "CRFND";

            if (objWorkFlowBO != null)
            {
                lnkPaymentVerification.Attributes.Clear();
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}');", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                lnkPaymentVerification.Attributes.Add("onclick", paramChangeRequest);

                if (grdPaymentDetails.Rows.Count > 0)
                {
                    lnkPaymentVerification.Visible = true;
                }
                else
                    lnkPaymentVerification.Visible = false;
            }
            else
            {
                //----If APPROVER does Not Exists then disable the Button
                //lnkValuationPCI.Visible = false;
                lnkPaymentVerification.Visible = false;
                lblPaytVerification.Text = "Approver Not Defined";
            }
            #endregion

            //getAppoverReqStatusPaymentVerification();
        }

        /// <summary>
        /// Check request status
        /// </summary>
        public void getAppoverReqStatusPaymentVerification()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();

            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "CRFND";
            objHouseHold.Workflowcode = UtilBO.PaymentVerificationCode;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                //New Code Added Here
                int ApproverStatus = CheckAllApproverLevels("CRFND", UtilBO.PaymentVerificationCode);

                if (ApproverStatus == 3)// (objHouseHold.ApproverStatus == 3)
                {
                    //PENDING
                    // lblPaytVerification.Visible = true;
                    lblPaytVerification.Text = "Pending Approval";
                    lnkPaymentVerification.Visible = true;
                    //lnkValuationPCI.Visible = false;
                }
                else if (ApproverStatus == 2)//(objHouseHold.ApproverStatus == 2)
                {
                    //DECLINED
                    // lblPaytVerification.Visible = false;
                    lblPaytVerification.Text = "Payment Veification Declined";
                    //lnkValuationPCI.Visible = false;
                    if (grdPaymentDetails.Rows.Count > 0)
                    {
                        lnkPaymentVerification.Visible = true;
                    }
                    else
                        lnkPaymentVerification.Visible = false;
                }
                else if (ApproverStatus == 1) //(objHouseHold.ApproverStatus == 1)
                {
                    //APPROVED
                    lblPaytVerification.Text = "<font class='StatusApproved'>Approved</font>";
                    lnkPaymentVerification.Visible = false;

                    btnSummerySave.Visible = false;
                    btnSummeryClear.Visible = false;

                    //lblPaytVerification.ForeColor = System.Drawing.Color.Blue;

                    //----------------------Checking Approver Status of Closing Payment----------------
                    PAP_HouseholdBLL oHouseHoldBLL = new PAP_HouseholdBLL();
                    PAP_HouseholdBO oHouseHold = new PAP_HouseholdBO();

                    oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                    int HHID = Convert.ToInt32(Session["HH_ID"]);
                    oHouseHold.HhId = HHID;
                    oHouseHold.PageCode = "CREND";
                    oHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalFL;

                    oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);
                }
                else
                {
                    if (grdPaymentDetails.Rows.Count > 0)
                    {
                        lnkPaymentVerification.Visible = true;
                    }
                    else
                        lnkPaymentVerification.Visible = false;
                }
            }
            else
            {
                //lnkValuationPCI.Visible = false;
            }
        }
        #endregion Change Request for Payment Verification

        #region Fund Request, Package Document Status & Grievance Request Verification
        private bool checkFundRequestStatus()
        {
            //-----------------------Checking the Fund Request Verification Approver Status----------
            //Fund Verification Status
            BatchBLL oBatchBLL = new BatchBLL();
            BatchBO oBatchBO = new BatchBO();
            oBatchBO = oBatchBLL.GetPaymentRequestByHHID(SessionHHID);
            if (oBatchBO != null)
            {
                if (oBatchBO.RequestStatus != null && oBatchBO.RequestStatus.Substring(0, 4).ToUpper() == "APPR")
                    return true;
            }
            return true;  //false; //All Time True Made
        }

        private bool checkPackegeReview()
        {
            //-----------------------Checking the Documentation Verification Approver Status----------
            //Document Verification Status

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();

            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "CPREV";
            objHouseHold.Workflowcode = UtilBO.PackagePaymentRequestCode;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);
            if (objHouseHold != null)
            {
                if (objHouseHold.ApproverStatus == 1)
                    return true;
            }
            //USP_TRN_APPROVALSTATUSPENDING
            return true;//false;//Change is Made All Time true
        }

        private bool checkGrievanceRequet()
        {
            //----------------------Checking the Grievance Approval------------------------------------

            int HHID = 0;
            string Status = string.Empty;

            if (Session["HH_ID"] != null)
                HHID = Convert.ToInt32(Session["HH_ID"]);

            GrievancesBLL oGrievancesBLL = new GrievancesBLL();
            GrievanceList lstGrievance = new GrievanceList();
            GrievancesBO oGrievancesBO = new GrievancesBO();

            oGrievancesBO = oGrievancesBLL.getGrievanceOverAllStatus(HHID);

            if (oGrievancesBO.Status == "1" || oGrievancesBO.Status.Trim() == "")
                return true;

            return false;
        }

        private bool checkPaymentRequest()
        {
            //----------------------Checking Approver Status of Payment Verification----------------
            PAP_HouseholdBLL oHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO oHouseHold = new PAP_HouseholdBO();

            oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            oHouseHold.HhId = HHID;
            oHouseHold.PageCode = "CRFND";
            oHouseHold.Workflowcode = UtilBO.PaymentVerificationCode;

            oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);
            if (oHouseHold != null)
            {
                if (oHouseHold.ApproverStatus == 1)// || oHouseHold.ApproverStatus == 3)
                    return true;
            }
            return false;
        }

        private int checkPaymentRequestStatus()
        {
            //----------------------Checking Approver Status of Payment Verification----------------
            PAP_HouseholdBLL oHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO oHouseHold = new PAP_HouseholdBO();

            oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            oHouseHold.HhId = HHID;
            oHouseHold.PageCode = "CRFND";
            oHouseHold.Workflowcode = UtilBO.PaymentVerificationCode;

            oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);
            if (oHouseHold != null)
            {
                return oHouseHold.ApproverStatus;
            }
            return 0;
        }
        #endregion Fund Request, Package Document Status & Grievance Request Verification

        private void EnablePayVerificationReqButton(bool Status)
        {
            if (Status)
            {
                checkApprovalExistsForPaymentVerification();
                btnPaymentSave.Visible = true;
                btnPaymentClear.Visible = true;
            }
            else
            {
                lnkPaymentVerification.Visible = false;
            }
        }

        /// <summary>
        /// Check request status
        /// </summary>
        private void dependencyStatusCheck()
        {
            checkApprovalExistsForPaymentVerification();
            //checkApprovalExitOrNot();
            StringBuilder strBuild = new StringBuilder();

            if (!checkPackegeReview())
                strBuild.Append("<li> Package Review </li>");

            if (!checkFundRequestStatus())
                strBuild.Append("<li> Fund Request </li>");

            if (strBuild.Length > 0)
            {
                strBuild.Insert(0, "<b>Check the Following Approvals: <ol>");
                strBuild.Append("</ol></b>");
            }

            if (checkFundRequestStatus() && checkPackegeReview() && CountCheckBoxes > 0)// && checkGrievanceRequet())
            {
                //Check box Count Added Here: When Atleast one Approved Status(Checkbox) is Required for Payment
                pnlPaymentMode.Visible = true;
                lblPaymentStatusMessage.Text = string.Empty;
                lblPaymentStatusMessage.Visible = false;
            }
            else
            {
                pnlPaymentMode.Visible = false;
                lblPaymentStatusMessage.Text = strBuild.ToString();//"<b><u>Following Approvals are Pending :</u> </br> 1. Package Review  </br> 2. Fund Request</b>";
                lblPaymentStatusMessage.Visible = true;
            }
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindFixedCostCentre()
        {
            FixedCostCentreBLL objFixedCostCentreBLL = new FixedCostCentreBLL();
            ddlFixedCostCentre.DataSource = objFixedCostCentreBLL.GetFixedCostCentres("");
            ddlFixedCostCentre.DataValueField = "FixedCostCentreID";
            ddlFixedCostCentre.DataTextField = "FixedCostCentre";
            ddlFixedCostCentre.DataBind();
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindBanks()
        {
            BankBLL objBankBLL = new BankBLL();
            ddlBank.DataSource = objBankBLL.GetBanks();
            ddlBank.DataValueField = "BankID";
            ddlBank.DataTextField = "BankName";
            ddlBank.DataBind();
        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        protected void BindBankBranches(int bankID)
        {
            ListItem firstListItem = new ListItem(ddlBranch.Items[0].Text, ddlBranch.Items[0].Value);

            ddlBranch.Items.Clear();

            if (bankID > 0)
            {
                BranchBLL objBranchBLL = new BranchBLL();
                ddlBranch.DataSource = objBranchBLL.GetActiveBranches(bankID);

                ddlBranch.DataTextField = "BRANCHNAME";
                ddlBranch.DataValueField = "BankBranchId";
                ddlBranch.DataBind();
            }

            ddlBranch.Items.Insert(0, firstListItem);
            ddlBranch.SelectedIndex = 0;
        }

        /// <summary>
        /// Check All Approver Levels
        /// </summary>
        /// <param name="PageCode"></param>
        /// <param name="WorkFlowCode"></param>
        /// <returns></returns>
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

        #region Check Boxes & Their Events

        /// <summary>
        /// Call Respective methos to calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkLandValuation_CheckedChanged(object sender, EventArgs e)
        {
            txtLandPending.Enabled = chkLandValuation.Checked;
            if (chkLandValuation.Checked)
                txtLandPending.Text = (Convert.ToDecimal(txtCashLand.Text) - Convert.ToDecimal(lblLandPaid.Text)).ToString();
            else
                txtLandPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkReplacementHouseValue_CheckedChanged(object sender, EventArgs e)
        {
            txtResidentialPending.Enabled = chkReplacementHouseValue.Checked;
            if (chkReplacementHouseValue.Checked)
                txtResidentialPending.Text = (Convert.ToDecimal(txtResStructure.Text) - Convert.ToDecimal(lblResidentialPaid.Text)).ToString();
            else
                txtResidentialPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkFixtureValuation_CheckedChanged(object sender, EventArgs e)
        {
            txtFixturesPending.Enabled = chkFixtureValuation.Checked;
            if (chkFixtureValuation.Checked)
                txtFixturesPending.Text = (Convert.ToDecimal(txtFixture.Text) - Convert.ToDecimal(lblFixturesPaid.Text)).ToString();
            else
                txtFixturesPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkCropsValuation_CheckedChanged(object sender, EventArgs e)
        {
            txtCropsPending.Enabled = chkCropsValuation.Checked;
            if (chkCropsValuation.Checked)
                txtCropsPending.Text = (Convert.ToDecimal(txtCrops.Text) - Convert.ToDecimal(lblCropsPaid.Text)).ToString();
            else
                txtCropsPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkCulturePropertyValue_CheckedChanged(object sender, EventArgs e)
        {
            txtCulturePending.Enabled = chkCulturePropertyValue.Checked;
            if (chkCulturePropertyValue.Checked)
                txtCulturePending.Text = (Convert.ToDecimal(txtCulutralProperty.Text) - Convert.ToDecimal(lblCulturePaid.Text)).ToString();
            else
                txtCulturePending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkDamagedCropValue_CheckedChanged(object sender, EventArgs e)
        {
            txtDamagedPending.Enabled = chkDamagedCropValue.Checked;
            if (chkDamagedCropValue.Checked)
                txtDamagedPending.Text = (Convert.ToDecimal(txtDamaged.Text) - Convert.ToDecimal(lblDamagedPaid.Text)).ToString();
            else
                txtDamagedPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkFacilitation_CheckedChanged(object sender, EventArgs e)
        {
            SetFacilitationPending();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SetFacilitationPending()
        {
            txtFacilitationPending.Enabled = chkFacilitation.Checked;
            if (chkFacilitation.Checked)
            {
                decimal facilitationAmount = 0;
                decimal facilitationPaid = 0;

                decimal.TryParse(txtFacilitation.Text.Trim(), out facilitationAmount);
                decimal.TryParse(lblFacilitationPaid.Text.Trim(), out facilitationPaid);

                txtFacilitationPending.Text = (facilitationAmount - facilitationPaid).ToString();
            }
            else
            {
                txtFacilitationPending.Text = "";
            }

            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkFinalCompensation_CheckedChanged(object sender, EventArgs e)
        {
            CalculateTotalPayAmount();
        }

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkNegotiatedAmount_CheckedChanged(object sender, EventArgs e)
        {
            txtNegotiatedPending.Enabled = chkNegotiatedAmount.Checked;
            if (chkNegotiatedAmount.Checked)
                txtNegotiatedPending.Text = (Convert.ToDecimal(lblNegotiatedAmount.Text) - Convert.ToDecimal(lblNegotiatedPaid.Text)).ToString();
            else
                txtNegotiatedPending.Text = "";
            CalculateTotalPayAmount();
        }

        /// <summary>
        ///  to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateTotalPayAmount()
        {
            //ViewStateTotalPayAmount;
            decimal TotalAmount = 0, Land = 0, ReplacementHouseValue = 0, Fixture = 0, Crops = 0;
            decimal Culture = 0, Damaged = 0, Final = 0, Negotiated = 0, Facilitation = 0;

            if (chkLandValuation.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtLandPending.Text))
                    Land = Convert.ToDecimal(txtLandPending.Text);
                TotalAmount = Land;
            }
            if (chkReplacementHouseValue.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtResidentialPending.Text))
                    ReplacementHouseValue = Convert.ToDecimal(txtResidentialPending.Text);
                TotalAmount += ReplacementHouseValue;
            }
            if (chkFixtureValuation.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtFixturesPending.Text))
                    Fixture = Convert.ToDecimal(txtFixturesPending.Text);
                TotalAmount += Fixture;
            }
            if (chkCropsValuation.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtCropsPending.Text))
                    Crops = Convert.ToDecimal(txtCropsPending.Text);
                TotalAmount += Crops;
            }
            if (chkCulturePropertyValue.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtCulturePending.Text))
                    Culture = Convert.ToDecimal(txtCulturePending.Text);
                TotalAmount += Culture;
            }
            if (chkDamagedCropValue.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtDamagedPending.Text))
                    Damaged = Convert.ToDecimal(txtDamagedPending.Text);
                TotalAmount += Damaged;
            }
            if (chkFacilitation.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtFacilitationPending.Text))
                    Facilitation = Convert.ToDecimal(txtFacilitationPending.Text);
                TotalAmount += Facilitation;
            }
            //if (chkFinalCompensation.Checked == true)
            //{
            //    if (!string.IsNullOrEmpty(lblTotalAmount.Text))
            //        Final = Convert.ToDecimal(lblTotalAmount.Text);

            //    TotalAmount = Final;

            //    //Disable others
            //    DisableAllCheckBox();
            //    //chkFinalCompensation.Style.Add("display", "none"); //"visibility" "hidden"
            //    chkNegotiatedAmount.Style.Add("display", "none"); //"visibility" "hidden"

            //    //chkLandValuation.Style.Add("display", "none"); //"visibility" "hidden"
            //    //chkFixtureValuation.Style.Add("display", "none"); //"visibility" "hidden"
            //    //chkCropsValuation.Style.Add("display", "none"); //"visibility" "hidden"
            //    //chkReplacementHouseValue.Style.Add("display", "none"); //"visibility" "hidden"
            //    //chkDamagedCropValue.Style.Add("display", "none"); //"visibility" "hidden"
            //    //chkCulturePropertyValue.Style.Add("display", "none"); //"visibility" "hidden"
            //}
            //else
            //{
            //    LoadSummery();
            //}
            if (chkNegotiatedAmount.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtNegotiatedPending.Text))
                    Negotiated = Convert.ToDecimal(txtNegotiatedPending.Text);
                TotalAmount = Negotiated;

                //Disable others
                DisableAllCheckBox();
                chkFinalCompensation.Style.Add("display", "none"); //"visibility" "hidden"

                //Disable Other Checkboxes
                //chkLandValuation.Style.Add("display", "none"); //"visibility" "hidden"
                //chkFixtureValuation.Style.Add("display", "none"); //"visibility" "hidden"
                //chkCropsValuation.Style.Add("display", "none"); //"visibility" "hidden"
                //chkReplacementHouseValue.Style.Add("display", "none"); //"visibility" "hidden"
                //chkDamagedCropValue.Style.Add("display", "none"); //"visibility" "hidden"
                //chkCulturePropertyValue.Style.Add("display", "none"); //"visibility" "hidden"

            }
            //else
            //{
            //    LoadSummery();
            //}
            ViewStateTotalPayAmount = TotalAmount;
            lblTestToalAmount.Text = UtilBO.CurrencyFormat(TotalAmount);
            txtPaymentAmount.Text = UtilBO.CurrencyFormat(TotalAmount);
        }
        #endregion Check Boxes & Their Events

        /// <summary>
        /// Call Respective methos to Calculate data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtFacilitation_TextChanged(object sender, EventArgs e)
        {
            if (txtFacilitation.Text.Trim() != "")
                chkFacilitation.Checked = true;
            else
                chkFacilitation.Checked = false;

            SetFacilitationPending();
        }

    }
}