using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;


namespace WIS
{
    public partial class PackagePaymentRequest : System.Web.UI.Page
    {
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
            calRequestDate.Format = UtilBO.DateFormat;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Package Fund Request";
                }
                else
                    Master.PageHeader = "Package Fund Request";
                ViewState["Valuation_Status"] = "None";
                LoadSummery();
                LoadBatches();
                //GetPaymentStatus();
                getApprReqStatusPakPayment();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PACKAGE_PAYMENT_REQ) == false)
                {
                    btnAddToBatch.Visible = false;
                    DisableAllCheckBox();
                }
                txtAmountRequested.Attributes.Add("readonly", "readonly");
                txtTotalAmount.Attributes.Add("readonly", "readonly");
                txtPaymentFor.Attributes.Add("readonly", "readonly");
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
                btnAddToBatch.Visible = false;
                DisableAllCheckBox();
            }
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
            stb.Append(btnAddToBatch.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Get Payment Status
        /// </summary>
        private void GetPaymentStatus()
        {
            BatchBO objBatchBO = (new BatchBLL()).GetPaymentRequestByHHID(SessionHHID);

            if (objBatchBO != null)
            {
                int BatchNo = 0;

                //   lblRequestStatus.Text = objBatchBO.RequestStatus + " in Batch No. " + objBatchBO.CMP_BatchNo;
                if (objBatchBO.CMP_BatchNo != null)
                    BatchNo = Convert.ToInt32(objBatchBO.CMP_BatchNo);
                if (BatchNo > 0)
                {
                    dpRequestDate.Text = Convert.ToDateTime(objBatchBO.Payt_RequestDate).ToString(UtilBO.DateFormat);
                    pnlPaymentRequest.Visible = false;
                }
                else
                {
                    pnlPaymentRequest.Visible = true;
                }
            }
            //Check Payment is taken Approval for All Payment
            if (getAmountApproved())
            {
                lblRequestStatus.Visible = false;
                pnlPaymentRequest.Visible = true;
            }
            else
            {
                // lblRequestStatus.Text = "Pending";
                lblRequestStatus.Visible = true;
                pnlPaymentRequest.Visible = false;
            }
        }
        /// <summary>
        /// get Amount Approved
        /// </summary>
        /// <returns></returns>
        private bool getAmountApproved()
        {
            BatchBO oBatchBO = new BatchBO();
            oBatchBO.HHID = SessionHHID;

            if (!string.IsNullOrEmpty(txtAmountRequested.Text))
                oBatchBO.Amt_Requested = Convert.ToDecimal(txtAmountRequested.Text);
            else
                oBatchBO.Amt_Requested = 0;

            oBatchBO = (new BatchBLL()).GetPaymentAmountApproved(oBatchBO);

            if (!string.IsNullOrEmpty(txtTotalAmount.Text))
            {
                decimal Total_Amount_Required_To_Be_Paid = Convert.ToDecimal(txtTotalAmount.Text);
                if (oBatchBO.Amt_Requested < Total_Amount_Required_To_Be_Paid)//PaidAmount<ToBePaidAmount
                {
                    //dpRequestDate.CalendarDate = Convert.ToDateTime(objBatchBO.Payt_RequestDate);                   
                    pnlPaymentRequest.Visible = true;
                    lblRequestStatus.Visible = false;

                    return true;
                }
                else // if (oBatchBO.Amt_Requested == TotalAmount)
                {
                    //lblRequestStatus.Text = "*";
                    //  lblRequestStatus.Visible = true;
                    //lblRequestStatus.Visible = true;
                    pnlPaymentRequest.Visible = false;
                    DisableAllCheckBox();
                    //return false;
                }
            }
            return false;
        }
        /// <summary>
        ///  Get PAP Valuation Summery Negotiated Amount
        /// </summary>
        /// <param name="TotalValuation"></param>
        /// <param name="NegotiatedAmount"></param>
        private void GetPAPValuationSummery_NegotiatedAmount(decimal TotalValuation, decimal NegotiatedAmount)
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO oPaymentBO = new PaymentBO();
            oPaymentBO = oPaymentBLL.getPapValuation(SessionHHID);

            if (oPaymentBO != null && oPaymentBO.NegotiatedAmountApproved.ToUpper() == "Y")
            {
                trNegotiatedAmount.Visible = true;
                hdnNegotiatedAmount.Value = "1";
                trNegotiatedAmount.BgColor = "#E4e4e4";
                txtTotalAmount.Text = UtilBO.CurrencyFormat(NegotiatedAmount);
            }
            else
            {
                trNegotiatedAmount.Visible = false;
                hdnNegotiatedAmount.Value = "0";
                //trNegotiatedAmount.Visible = true;
                trNegotiatedAmount.BgColor = "#F0F8FF";
                txtTotalAmount.Text = UtilBO.CurrencyFormat(TotalValuation);
            }
        }

        #region ViewStates
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
        #endregion
        /// <summary>
        /// TO clear data
        /// </summary>
        private void ClearFields()
        {
            dpRequestDate.Text = string.Empty;
            txtPaymentFor.Text = string.Empty;
            txtAmountRequested.Text = string.Empty;
            txtComments.Text = string.Empty;
            ddlBatchList.ClearSelection();
            rbBatch.ClearSelection();
            ChkAll.Checked = false;
            chkLandValuation.Checked = false;
            chkReplacementHouseValue.Checked = false;
            chkFixtureValuation.Checked = false;
            chkCropsValuation.Checked = false;
            chkDamagedCropValue.Checked = false;
            chkCulturePropertyValue.Checked = false;
            chkFacilitationValue.Checked = false;
            chkNegotiatedAmount.Checked = false;
        }
        /// <summary>
        /// To Save Paymet details and Batch 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddToBatch_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            if (ViewState["Valuation_Status"].ToString() != "Request Pending")
            {
                BatchBO oBatchBO, ooBtachBO;
                oBatchBO = new BatchBO();
                BatchBLL oBatchBLL = new BatchBLL();
                oBatchBO.CMP_BatchNo = string.Empty;
                oBatchBO.CreatedBy = SessionUserId;
                oBatchBO.IsDeleted = "False";
                oBatchBO.BatchStatus = "OPEN";

                oBatchBO.HHID = SessionHHID;
                oBatchBO.RequestStatus = BatchBLL.RequestStatus_Pending;
                oBatchBO.Payt_RequestDate = dpRequestDate.Text;

                int BatchNo = 0;
                if (rbBatch.SelectedValue == "1")
                {
                    oBatchBO.CMP_BatchNo = ddlBatchList.SelectedValue;
                    BatchNo = Convert.ToInt32(oBatchBO.CMP_BatchNo);
                }
                else //if (rbBatch.SelectedValue == "2")
                    oBatchBO.CMP_BatchNo = "0";

                //New Column Data
                if (txtComments.Text.Trim().Length > 500)
                    oBatchBO.Comments = txtComments.Text.Trim().Substring(0, 1000);
                else
                    oBatchBO.Comments = txtComments.Text.Trim();

                if (!string.IsNullOrEmpty(txtTotalAmount.Text))
                    oBatchBO.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);

                oBatchBO.Payt_Description = txtPaymentFor.Text;

                if (!string.IsNullOrEmpty(txtAmountRequested.Text))
                    oBatchBO.Amt_Requested = Convert.ToDecimal(txtAmountRequested.Text);

                ooBtachBO = oBatchBLL.AddBatch(oBatchBO);

                if (string.IsNullOrEmpty(ooBtachBO.dbMessage) || ooBtachBO.dbMessage == "" || ooBtachBO.dbMessage == "null")
                {
                    message = string.Format("Payment Request added to Batch No. {0} successfully.", ooBtachBO.CMP_BatchNo);
                    GetPaymentStatus();
                    ClearFields();
                }
                else
                {
                    if (!string.IsNullOrEmpty(ooBtachBO.CMP_BatchNo))
                        message = ooBtachBO.dbMessage;//BatchNo + " Batch Number already Added";
                    //  message = ooBtachBO.CMP_BatchNo + " Batch Number already exists";
                    GetPaymentStatus();
                    ClearFields();
                }
                LoadBatches();
                LoadSummery();
            }
            else
            {
                message = "A payment request is pending for this PAP.";
                ClearFields();
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        /// <summary>
        /// Load Summery Details
        /// </summary>
        private void LoadSummery()
        {
            CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();
            CompensationFinancialBO oCompensationFinancial = oCompensationFinancialBLL.GetCompensationFinancial(Convert.ToInt32(Session["HH_ID"]));
            // Replaced on 08jan2013 ResPayment as ResTotalValuation
            if (oCompensationFinancial != null)
            {
                txtLandValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation); //oCompensationFinancial.LandTotalValuation.ToString();
                txtFixturesValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixtureTotalValuation); //oCompensationFinancial.FixtureTotalValuation.ToString();
                txtCropsValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropTotalValuation); //oCompensationFinancial.CropTotalValuation.ToString();
                //decimal ReplacementHouseValue = oCompensationFinancial.ResReplacementValue + (oCompensationFinancial.ResReplacementValue * oCompensationFinancial.ResDA) / 100;
                txtReplacementHouseValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResPayment);//UtilBO.CurrencyFormat(oCompensationFinancial.ResTotalValuation);ResPayment //oCompensationFinancial.ResTotalValuation.ToString();//ReplacementHouseValue.ToString();
                txtDamagedCropValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.DamagedCropValuation); //oCompensationFinancial.DamagedCropValuation.ToString();
                txtCultureProperty.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropValuation); //oCompensationFinancial.DamagedCropValuation.ToString();
                txtFacilitation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FacilitationAllowance);
                txtFinalCompensation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation + oCompensationFinancial.FixtureTotalValuation + 
                    oCompensationFinancial.CropTotalValuation + oCompensationFinancial.ResPayment +
                    oCompensationFinancial.DamagedCropValuation + oCompensationFinancial.CulturePropValuation + oCompensationFinancial.FacilitationAllowance); //oCompensationFinancial.TotalValuation.ToString();
                txtNegotiatedAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.NegotiatedAmount); //oCompensationFinancial.NegotiatedAmount.ToString();//Newly Added
                txtInKindLand.Text = oCompensationFinancial.LandInKindCompensation.ToString().Trim();

                ddlResidentialStructure.ClearSelection();
                if (string.IsNullOrEmpty(oCompensationFinancial.ResInKindCompensation))
                    ddlResidentialStructure.ClearSelection();
                else
                {
                    if (ddlResidentialStructure.Items.FindByValue(oCompensationFinancial.ResInKindCompensation.ToString()) != null)
                    {
                        ddlResidentialStructure.ClearSelection();
                        ddlResidentialStructure.Items.FindByValue(oCompensationFinancial.ResInKindCompensation.ToString()).Selected = true;
                    }
                }
                txtResidentialStructure.Text = "";
                if (ddlResidentialStructure.SelectedIndex > 0)
                    txtResidentialStructure.Text = ddlResidentialStructure.SelectedItem.Text;
                //Displaying LABEL APPROVAL STATUS
                //--------------------------------------------LAND STATUS-----------------------------------------------------
                if (oCompensationFinancial.Land_Approval_Status != null && oCompensationFinancial.Land_Approval_Status.ToUpper() == "APPROVED")
                    lblLandValuationMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Land_Approval_Status + "</font>";
                else
                    lblLandValuationMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Land_Approval_Status + "</font>";
                //--------------------------------------------FIXTURE STATUS-----------------------------------------------------
                if (oCompensationFinancial.Fixture_Approval_Status != null && oCompensationFinancial.Fixture_Approval_Status.ToUpper() == "APPROVED")
                    lblFixturesValuationMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Fixture_Approval_Status + "</font>";
                else
                    lblFixturesValuationMsg.Text = "<font class='StatusPending'>"+oCompensationFinancial.Fixture_Approval_Status + "</font>";
                //--------------------------------------------CROP STATUS-----------------------------------------------------
                if (oCompensationFinancial.Crop_Approval_Status != null && oCompensationFinancial.Crop_Approval_Status.ToUpper() == "APPROVED")
                    lblCropsValuationMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Crop_Approval_Status + "</font>";
                else
                    lblCropsValuationMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Crop_Approval_Status + "</font>";
                //--------------------------------------------REPLACEMENT STATUS-----------------------------------------------------
                if (oCompensationFinancial.Replacment_Approval_Status != null && oCompensationFinancial.Replacment_Approval_Status.ToUpper() == "APPROVED")
                    lblReplacementHouseValueMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Replacment_Approval_Status + "</font>";
                else
                    lblReplacementHouseValueMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Replacment_Approval_Status + "</font>";
                //--------------------------------------------DAMAGED STATUS-----------------------------------------------------
                if (oCompensationFinancial.Damaged_Approval_Status != null && oCompensationFinancial.Damaged_Approval_Status.ToUpper() == "APPROVED")
                    lblDamagedCropValueMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Damaged_Approval_Status + "</font>";
                else
                    lblDamagedCropValueMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Damaged_Approval_Status + "</font>";
                //--------------------------------------------CULTURE PROPERTIES STATUS-----------------------------------------------------
                if (oCompensationFinancial.Culture_Approval_Status != null && oCompensationFinancial.Culture_Approval_Status.ToUpper() == "APPROVED")
                    lblCulturePropertyMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Culture_Approval_Status + "</font>";
                else
                    lblCulturePropertyMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Culture_Approval_Status + "</font>";
                //--------------------------------------------FACILITATION PROPERTIES STATUS-----------------------------------------------------
                if (oCompensationFinancial.Facilitation_Approval_Status != null && oCompensationFinancial.Facilitation_Approval_Status.ToUpper() == "APPROVED")
                    lblFacilitationMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Facilitation_Approval_Status + "</font>";
                else
                    lblFacilitationMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Facilitation_Approval_Status + "</font>";
                //--------------------------------------------FINAL STATUS-----------------------------------------------------
                if (oCompensationFinancial.Final_Approval_Status != null && oCompensationFinancial.Final_Approval_Status.ToUpper() == "APPROVED")
                    lblFinalCompensationMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Final_Approval_Status + "</font>";
                else
                    lblFinalCompensationMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Final_Approval_Status + "</font>";
                //--------------------------------------------NEGOTIATED STATUS-----------------------------------------------------
                if (oCompensationFinancial.Nego_Amount_Approval_Status != null && oCompensationFinancial.Nego_Amount_Approval_Status.ToUpper() == "APPROVED")
                    lblNegotiatedAmountMsg.Text = "<font class='StatusApproved'>" + oCompensationFinancial.Nego_Amount_Approval_Status + "</font>";
                else
                    lblNegotiatedAmountMsg.Text = "<font class='StatusPending'>" + oCompensationFinancial.Nego_Amount_Approval_Status + "</font>";

                //Disabling Checkbox For Approved & Request Pending & Submitted
                string LAS = oCompensationFinancial.Land_Approval_Status;
                string FAS = oCompensationFinancial.Fixture_Approval_Status;
                string CAL = oCompensationFinancial.Crop_Approval_Status;

                string RAL = oCompensationFinancial.Replacment_Approval_Status;
                string DAL = oCompensationFinancial.Damaged_Approval_Status;
                string CPAL = oCompensationFinancial.Culture_Approval_Status;
                string OPAL = oCompensationFinancial.Facilitation_Approval_Status;
                string FCAL = oCompensationFinancial.Final_Approval_Status;
                string NAAS = oCompensationFinancial.Nego_Amount_Approval_Status;
                int statusCount = 0;
                int chkcount = 7;
                int cl = 1, cf = 1, cc = 1, cd = 1, cr = 1, cneg = 0, ccu = 1, co = 1;
                // start
                if ((txtLandValuation.Text.Trim() == "" || oCompensationFinancial.LandTotalValuation == 0)
                    && (txtInKindLand.Text.Trim()=="" || oCompensationFinancial.LandInKindCompensation == 0))
                {
                    chkLandValuation.Style.Add("display", "none");
                    chkcount--;
                    cl = 0;
                }

                if (txtFixturesValuation.Text.Trim() == "" || oCompensationFinancial.FixtureTotalValuation == 0)
                {
                    chkFixtureValuation.Style.Add("display", "none");
                    chkcount--;
                    cf = 0;
                }

                if (txtCropsValuation.Text.Trim() == "" || oCompensationFinancial.CropTotalValuation == 0)
                {
                    chkCropsValuation.Style.Add("display", "none");
                    chkcount--;
                    cc = 0;
                }

                if ((txtReplacementHouseValue.Text.Trim() == "" || oCompensationFinancial.ResPayment == 0) && (txtResidentialStructure.Text.Trim() == "" || txtResidentialStructure.Text.Trim().ToUpper() == "NA"))
                {
                    chkReplacementHouseValue.Style.Add("display", "none");
                    chkcount--;
                    cr = 0;
                }

                if (txtDamagedCropValue.Text.Trim() == "" || oCompensationFinancial.DamagedCropValuation == 0)
                {
                    chkDamagedCropValue.Style.Add("display", "none");
                    chkcount--;
                    cd = 0;
                }

                if (txtCultureProperty.Text.Trim() == "" || oCompensationFinancial.CulturePropValuation == 0)
                {
                    chkCulturePropertyValue.Style.Add("display", "none");
                    chkcount--;
                    ccu = 0;
                }

                if (txtFacilitation.Text.Trim() == "" || oCompensationFinancial.FacilitationAllowance == 0)
                {
                    chkFacilitationValue.Style.Add("display", "none");
                    chkcount--;
                    co = 0;
                }

                if ((cl + cf + cc + cd + cr + ccu + co) <= 0)
                    ChkAll.Style.Add("display", "none");
                chkcount = 7;
                //if (txtFinalCompensation.Text.Trim() == "" || oCompensationFinancial.TotalValuation == 0)
                //    chkFinalCompensation.Style.Add("display", "none");

                if (txtNegotiatedAmount.Text.Trim() == "" || oCompensationFinancial.NegotiatedAmount == 0)
                    chkNegotiatedAmount.Style.Add("display", "none");
                else
                {
                    cneg++;
                    chkLandValuation.Style.Add("display", "none"); 
                    chkFixtureValuation.Style.Add("display", "none"); 
                    chkCropsValuation.Style.Add("display", "none"); 
                    chkReplacementHouseValue.Style.Add("display", "none");
                    chkDamagedCropValue.Style.Add("display", "none");
                    chkCulturePropertyValue.Style.Add("display", "none");
                    chkFacilitationValue.Style.Add("display", "none");
                    ChkAll.Style.Add("display", "none");
                }
                //end

                if ((!string.IsNullOrEmpty(LAS) && LAS.Length > 3) && (LAS.Substring(0, 3).ToUpper() == "APP" || LAS.Substring(0, 3).ToUpper() == "SUB" || LAS.Substring(0, 3).ToUpper() == "REQ"))//LAS.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkLandValuation.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    cl = 0;
                }

                if ((!string.IsNullOrEmpty(FAS) && FAS.Length > 3) && (FAS.Substring(0, 3).ToUpper() == "APP" || FAS.Substring(0, 3).ToUpper() == "SUB" || FAS.Substring(0, 3).ToUpper() == "REQ"))//FAS.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkFixtureValuation.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    cf = 0;
                }

                if ((!string.IsNullOrEmpty(CAL) && CAL.Length > 3) && (CAL.Substring(0, 3).ToUpper() == "APP" || CAL.Substring(0, 3).ToUpper() == "SUB" || CAL.Substring(0, 3).ToUpper() == "REQ"))//CAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkCropsValuation.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    cc = 0;
                }

                if ((!string.IsNullOrEmpty(RAL) && RAL.Length > 3) && (RAL.Substring(0, 3).ToUpper() == "APP" || RAL.Substring(0, 3).ToUpper() == "SUB" || RAL.Substring(0, 3).ToUpper() == "REQ"))//RAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkReplacementHouseValue.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    cr = 0;
                }

                if ((!string.IsNullOrEmpty(DAL) && DAL.Length > 3) && (DAL.Substring(0, 3).ToUpper() == "APP" || DAL.Substring(0, 3).ToUpper() == "SUB" || DAL.Substring(0, 3).ToUpper() == "REQ"))//DAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkDamagedCropValue.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    cd = 0;
                }

                if ((!string.IsNullOrEmpty(CPAL) && CPAL.Length > 3) && (CPAL.Substring(0, 3).ToUpper() == "APP" || CPAL.Substring(0, 3).ToUpper() == "SUB" || CPAL.Substring(0, 3).ToUpper() == "REQ"))//CPAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkCulturePropertyValue.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    ccu = 0;
                }

                if ((!string.IsNullOrEmpty(OPAL) && OPAL.Length > 3) && (OPAL.Substring(0, 3).ToUpper() == "APP" || OPAL.Substring(0, 3).ToUpper() == "SUB" || OPAL.Substring(0, 3).ToUpper() == "REQ"))//CPAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    chkFacilitationValue.Style.Add("display", "none");
                    statusCount++;
                    chkcount--;
                    co = 0;
                }

                if ((cl + cf + cc + cd + cr + ccu + co) <= 0)
                {
                    ChkAll.Style.Add("display", "none");
                    pnlPaymentRequest.Visible = false;
                }

                if ((!string.IsNullOrEmpty(FCAL) && FCAL.Length > 3) && (FCAL.Substring(0, 3).ToUpper() == "APP" || FCAL.Substring(0, 3).ToUpper() == "SUB" || FCAL.Substring(0, 3).ToUpper() == "REQ"))//FCAL.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    DisableAllCheckBox();
                }

                if ((!string.IsNullOrEmpty(NAAS) && NAAS.Length > 3) && (NAAS.Substring(0, 3).ToUpper() == "APP" || NAAS.Substring(0, 3).ToUpper() == "SUB" || NAAS.Substring(0, 3).ToUpper() == "REQ"))//NAAS.Substring(0, 3).ToUpper() == "DEC" ||
                {
                    DisableAllCheckBox();
                }
                if (statusCount > 0)
                {
                    //chkFinalCompensation.Style.Add("display", "none");
                    chkNegotiatedAmount.Style.Add("display", "none");
                    cneg = 0;
                }
                if (cneg > 0)
                    pnlPaymentRequest.Visible = true;
                GetPAPValuationSummery_NegotiatedAmount((oCompensationFinancial.LandTotalValuation + oCompensationFinancial.FixtureTotalValuation +
                    oCompensationFinancial.CropTotalValuation + oCompensationFinancial.ResPayment +
                    oCompensationFinancial.DamagedCropValuation + oCompensationFinancial.CulturePropValuation + oCompensationFinancial.FacilitationAllowance), oCompensationFinancial.NegotiatedAmount);

                if ((!string.IsNullOrEmpty(LAS) && LAS.Length > 3) && (LAS.Substring(0, 3).ToUpper() == "SUB" || LAS.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(FAS) && FAS.Length > 3) && (FAS.Substring(0, 3).ToUpper() == "SUB" || FAS.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(CAL) && CAL.Length > 3) && (CAL.Substring(0, 3).ToUpper() == "SUB" || CAL.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(RAL) && RAL.Length > 3) && (RAL.Substring(0, 3).ToUpper() == "SUB" || RAL.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(DAL) && DAL.Length > 3) && (DAL.Substring(0, 3).ToUpper() == "SUB" || DAL.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(CPAL) && CPAL.Length > 3) && (CPAL.Substring(0, 3).ToUpper() == "SUB" || CPAL.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(OPAL) && OPAL.Length > 3) && (OPAL.Substring(0, 3).ToUpper() == "SUB" || OPAL.Substring(0, 3).ToUpper() == "REQ") ||
                    (!string.IsNullOrEmpty(NAAS) && NAAS.Length > 3) && (NAAS.Substring(0, 3).ToUpper() == "SUB" || NAAS.Substring(0, 3).ToUpper() == "REQ"))
                {
                    ViewState["Valuation_Status"] = "Request Pending";
                }
                else
                {
                    ViewState["Valuation_Status"] = "None";
                }
            }
            else
            {
                DisableAllCheckBox();
            }
        }

        /// <summary>
        /// To Disable All Check Box
        /// </summary>
        private void DisableAllCheckBox()
        {
            chkLandValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkFixtureValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkCropsValuation.Style.Add("display", "none"); //"visibility" "hidden"
            chkReplacementHouseValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkDamagedCropValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkCulturePropertyValue.Style.Add("display", "none"); //"visibility" "hidden"
            chkFacilitationValue.Style.Add("display", "none");

            ChkAll.Style.Add("display", "none"); //"visibility" "hidden"
            chkNegotiatedAmount.Style.Add("display", "none"); //"visibility" "hidden"
            pnlPaymentRequest.Visible = false;
        }

        /// <summary>
        /// To Load Batches
        /// </summary>
        private void LoadBatches()
        {
            ListItem firstListItem = new ListItem(ddlBatchList.Items[0].Text, ddlBatchList.Items[0].Value);
            ddlBatchList.Items.Clear();

            BatchBO oBatchBO = new BatchBO();
            BatchBLL oBatchBLL = new BatchBLL();
            BatchList oBatchList = new BatchList();
            if (Session["PROJECT_ID"] != null)
                oBatchList = oBatchBLL.GetBatches(Convert.ToInt32(Session["PROJECT_ID"]));
            if (oBatchList != null)
            {
                ddlBatchList.DataSource = oBatchList;
                ddlBatchList.DataTextField = "CMP_BATCHNO";
                ddlBatchList.DataValueField = "CMP_BATCHNO";
                ddlBatchList.DataBind();
            }
            ddlBatchList.Items.Insert(0, firstListItem);
        }

        /// <summary>
        /// Set visible to ddlBatchList based on selecton Existing or new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbBatch.SelectedValue == "1")
            {
                ddlBatchList.Visible = true;
            }
            else
            {
                ddlBatchList.Visible = false;
                ddlBatchList.ClearSelection();
            }
        }

        /// <summary>
        /// get Approval Request Status Pakage Payment
        /// </summary>
        public void getApprReqStatusPakPayment()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();

            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "CPREV";
            objHouseHold.Workflowcode = UtilBO.PackagePaymentRequestCode;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    //PENDING
                    pnlPaymentReqInfo.Visible = false;

                    lblCompPackageStatus.Visible = true;
                    lblCompPackageStatus.Text = "Sent for Package Review";
                    DisableAllCheckBox();
                }
                else if (objHouseHold.ApproverStatus == 2)
                {
                    //DECLINED
                    pnlPaymentReqInfo.Visible = false;

                    lblCompPackageStatus.Visible = true;
                    lblCompPackageStatus.Text = "Package Reviewed and Declined";
                    DisableAllCheckBox();
                }
                else if (objHouseHold.ApproverStatus == 1)
                {
                    //APPROVED
                    pnlPaymentReqInfo.Visible = true;

                    lblCompPackageStatus.Visible = false;
                    lblCompPackageStatus.Text = string.Empty;
                }
            }
            else
            {
                pnlPaymentReqInfo.Visible = false;
                lblCompPackageStatus.Visible = true;
                lblCompPackageStatus.Text = "Pending Package Review";
                DisableAllCheckBox();
            }
        }
    }
}