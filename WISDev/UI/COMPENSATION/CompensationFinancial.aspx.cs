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
    public partial class CompensationFinancial : System.Web.UI.Page
    {
        #region Global Declaration & Page Load
        CompensationFinancialBO oCompensationFinancial;
        CompensationFinancialBLL oCompensationFinancialBLL;

        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            calDeliveryDate.Format = UtilBO.DateFormat;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Compensation Financial";

                txtValuationAfterMaxCap.Attributes.Add("readonly", "readonly");
                //txtValuationAfterMaxCap.Attributes.Add("disabled", "disabled");
                //chkMaxCapCase.Attributes.Add("disabled", "disable");
                chkMaxCapCase.Enabled = false;

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - " + "Compensation Financial";
                }
                ClearItems();
                int HH_ID = 0;
                if (Session["PROJECT_ID"] != null)
                {
                    BindDerivedBy();
                }
                if (Session["HH_ID"] != null)
                {
                    HH_ID = Convert.ToInt32(Session["HH_ID"]);
                    if (HH_ID > 0)
                    {
                        LoadData();
                    }
                }

                Load_RoundOffValue();//getting the Round Off Value From the DB

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMPENSATION_FINANCIALS) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
            }
        }
        #endregion Global Declaration & Page Load
        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
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

        #region Clear & Disable Methods
        /// <summary>
        /// To Clear data
        /// </summary>
        private void ClearItems()
        {
            //Land
            txtLandValuation.Text = string.Empty;
            txtLandDAinPercentage.Text = string.Empty;//ViewStatePercentage.ToString();//
            txtLandDAinAmount.Text = string.Empty;
            txtLandTotal.Text = string.Empty;
            txtAcreageDifferencePayment.Text = string.Empty;
            txtLandComments.Text = string.Empty;

            //Residential Structure
            txtRSDepreciatedValue.Text = string.Empty;
            txtRSReplacementValue.Text = string.Empty;
            txtReplacementUplift.Text = string.Empty;
            txtRSDAinPercentage.Text = string.Empty;//ViewStatePercentage.ToString();//string.Empty;
            txtRSDAinPercentage.Text = string.Empty;
            txtResidentialDAinAmount.Text = string.Empty;
            txtRSMovingAllowance.Text = string.Empty;
            txtRSLabourCost.Text = string.Empty;
            txtRSPaymentHighHouseValue.Text = string.Empty;
            txtResidentialStructureComments.Text = string.Empty;

            //Fixtures
            txtFixturesValuation.Text = string.Empty;
            txtFixturesDAinPercentage.Text = string.Empty;//ViewStatePercentage.ToString();//string.Empty;
            txtFixturesDAinAmount.Text = string.Empty;
            txtFixturesComments.Text = string.Empty;

            //Crops
            txtCropsValuation.Text = string.Empty;
            chkMaxCapCase.Checked = false;
            //txtValuationAfterMaxCap.Attributes.Add("disabled", "disabled");
            txtValuationAfterMaxCap.Text = string.Empty;
            txtCropsDAinPercentage.Text = string.Empty;//ViewStatePercentage.ToString();//string.Empty;
            txtCropsDAinAmount.Text = string.Empty;
            txtCropsComments.Text = string.Empty;

            //Others
            txtCompensationForCultureProperty.Text = string.Empty;
            txtOtherDamagedCrops.Text = string.Empty;
            txtOthersTotal.Text = string.Empty;

            //Summary
            txtSummaryLandValues.Text = string.Empty;
            txtHouseValues.Text = string.Empty;
            txtReplacementHouseValue.Text = string.Empty;
            txtSummeryFixturesValue.Text = string.Empty;
            txtCropsValue.Text = string.Empty;
            txtDamagedCropsValue.Text = string.Empty;
            txtCulturePropertyValue.Text = string.Empty;
            txtNegotiatedAmount.Text = string.Empty;
            txtInKindLand.Text = string.Empty;
            ddlResidentialStructure.ClearSelection();
            ddlResidentialStructure.SelectedIndex = 0;
            txtFacilitationallowances.Text = string.Empty;

            //Package Delivery Info
            dpDeliveryDate.Text = DateTime.Now.ToString(UtilBO.DateFormat);
            ddlDeliveredBy.ClearSelection();
            txtPackageDeliveryInfoComments.Text = string.Empty;
            DisableTextBoxes();
        }
        /// <summary>
        /// To Disable all the text boxes
        /// </summary>
        private void DisableTextBoxes()
        {
            txtLandValuation.Attributes.Add("readonly", "readonly");
            txtLandValuation.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtCropsValuation.Attributes.Add("readonly", "readonly");
            txtCropsValuation.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtFixturesValuation.Attributes.Add("readonly", "readonly");
            txtFixturesValuation.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtHouseValues.Attributes.Add("readonly", "readonly");
            txtHouseValues.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtReplacementHouseValue.Attributes.Add("readonly", "readonly");
            txtReplacementHouseValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtSummaryLandValues.Attributes.Add("readonly", "readonly");
            txtSummaryLandValues.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtSummeryFixturesValue.Attributes.Add("readonly", "readonly");
            txtSummeryFixturesValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtCompensationForCultureProperty.Attributes.Add("readonly", "readonly");
            txtCompensationForCultureProperty.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtOtherDamagedCrops.Attributes.Add("readonly", "readonly");
            txtOtherDamagedCrops.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtRSDepreciatedValue.Attributes.Add("readonly", "readonly");
            txtRSDepreciatedValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtNegotiatedAmount.Attributes.Add("readonly", "readonly");
            txtNegotiatedAmount.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtCropsValue.Attributes.Add("readonly", "readonly");
            txtCropsValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtDamagedCropsValue.Attributes.Add("readonly", "readonly");
            txtDamagedCropsValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtCulturePropertyValue.Attributes.Add("readonly", "readonly");
            txtCulturePropertyValue.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtOthersTotal.Attributes.Add("readonly", "readonly");
            txtOthersTotal.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtLandTotal.Attributes.Add("readonly", "readonly");
            txtLandTotal.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtFixturesDAinAmount.Attributes.Add("readonly", "readonly");
            txtFixturesDAinAmount.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtCropsDAinAmount.Attributes.Add("readonly", "readonly");
            txtCropsDAinAmount.Attributes.Add("onKeyDown", "PreventKeyDown();");

            txtRSReplacementValue.Attributes.Add("readonly", "readonly");
            txtReplacementUplift.Attributes.Add("readonly", "readonly");
        }
        #endregion
        /// <summary>
        /// Parameters HHID,ViewStatePercentage
        /// </summary>
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
            //set
            //{
            //    ViewState["HH_ID"] = value;
            //}
        }

        private int ViewStatePercentage
        {
            get
            {
                if (ViewState["Percentage"] != null)
                    return Convert.ToInt32(ViewState["Percentage"]);
                else
                    return 0;
                //return 30;
            }
            //set
            //{
            //    ViewState["Percentage"] = value;
            //}
        }
        #endregion

        #region Buttons Save & Clear
        /// <summary>
        /// To save and Update Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            message = AddData();

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {

                message = "Data saved successfully";
                btnSave.Text = "Update";
                // ClearItems();
            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// to Add data to Datat base
        /// </summary>
        /// <returns></returns>
        private string AddData()
        {
            oCompensationFinancial = new WIS_BusinessObjects.CompensationFinancialBO();
            string strMax = string.Empty;

            //oCompensationFinancial.Cmp_FinancialID = "";
            if (Session["HH_ID"] != null)
            {
                oCompensationFinancial.HHID = Convert.ToInt32(Session["HH_ID"]);
            }
            oCompensationFinancialBLL = new CompensationFinancialBLL();

            oCompensationFinancial.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
            oCompensationFinancial.IsDeleted = "False";
            //LAND SECTION
            #region Land Section
            if (!string.IsNullOrEmpty(txtLandValuation.Text))
                oCompensationFinancial.LandValuation = Convert.ToDecimal(txtLandValuation.Text.Trim());

            if (!string.IsNullOrEmpty(txtLandDAinPercentage.Text))
                oCompensationFinancial.LandDA = Convert.ToDecimal(txtLandDAinPercentage.Text.Trim());

            if (!string.IsNullOrEmpty(txtAcreageDifferencePayment.Text))
                oCompensationFinancial.LandDiffPayment = Convert.ToDecimal(txtAcreageDifferencePayment.Text.Trim());

            if (!string.IsNullOrEmpty(txtLandValuation.Text) && !string.IsNullOrEmpty(txtLandDAinAmount.Text))
                oCompensationFinancial.LandTotalValuation = Convert.ToDecimal(txtLandValuation.Text.Trim()) + Convert.ToDecimal(txtLandDAinAmount.Text.Trim());

            strMax = txtLandComments.Text.Trim();
            if (strMax.Trim().Length > 1000)
            {
                strMax = txtLandComments.Text.Trim().Substring(0, 999);
            }

            oCompensationFinancial.LandValComments = strMax;
            #endregion

            //RESIDENTIAL STRUCTURE
            #region Residencial Structure
            if (!string.IsNullOrEmpty(txtRSDepreciatedValue.Text))
                oCompensationFinancial.ResDepreciatedValue = Convert.ToDecimal(txtRSDepreciatedValue.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSReplacementValue.Text))
                oCompensationFinancial.ResReplacementValue = Convert.ToDecimal(txtRSReplacementValue.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSDAinPercentage.Text))
                oCompensationFinancial.ResDA = Convert.ToDecimal(txtRSDAinPercentage.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSMovingAllowance.Text))
                oCompensationFinancial.ResMovingAllowance = Convert.ToDecimal(txtRSMovingAllowance.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSLabourCost.Text))
                oCompensationFinancial.ResLabourCost = Convert.ToDecimal(txtRSLabourCost.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSPaymentHighHouseValue.Text))
                oCompensationFinancial.ResPayment = Convert.ToDecimal(txtRSPaymentHighHouseValue.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSReplacementValue.Text)) // && !string.IsNullOrEmpty(txtResidentialDAinAmount.Text))//Calculated mannually
                oCompensationFinancial.ResTotalValuation = Convert.ToDecimal(txtRSReplacementValue.Text.Trim()); // +Convert.ToDecimal(txtResidentialDAinAmount.Text.Trim());

            strMax = txtResidentialStructureComments.Text.Trim();
            if (strMax.Trim().Length >= 1000)
            {
                strMax = txtLandComments.Text.Trim().Substring(0, 999);
            }
            oCompensationFinancial.ResComments = strMax;
            #endregion

            //FIXTURES
            #region Fixture Section
            if (!string.IsNullOrEmpty(txtFixturesValuation.Text))
                oCompensationFinancial.FixtureValuation = Convert.ToDecimal(txtFixturesValuation.Text.Trim());

            if (!string.IsNullOrEmpty(txtFixturesDAinPercentage.Text))
                oCompensationFinancial.FixtureDA = Convert.ToDecimal(txtFixturesDAinPercentage.Text.Trim());

            if (!string.IsNullOrEmpty(txtFixturesDAinAmount.Text) && !string.IsNullOrEmpty(txtFixturesValuation.Text))
                oCompensationFinancial.FixtureTotalValuation = Convert.ToDecimal(txtFixturesDAinAmount.Text.Trim()) + Convert.ToDecimal(txtFixturesValuation.Text.Trim());

            if (!string.IsNullOrEmpty(txtRSDepreciatedValue.Text))
                oCompensationFinancial.ResDepreciatedValue = Convert.ToDecimal(txtRSDepreciatedValue.Text.Trim());

            strMax = txtFixturesComments.Text.Trim();
            if (strMax.Trim().Length >= 1000)
            {
                strMax = txtLandComments.Text.Trim().Substring(0, 999);
            }
            oCompensationFinancial.FixtureComments = strMax;
            #endregion

            //CROPS
            #region Crops Section
            if (!string.IsNullOrEmpty(txtCropsValuation.Text))
                oCompensationFinancial.CropValuation = Convert.ToDecimal(txtCropsValuation.Text.Trim());

            //CheckBox
            if (chkMaxCapCase.Checked)
                oCompensationFinancial.CropMaxCapCase = "Yes";
            else
                oCompensationFinancial.CropMaxCapCase = "No";

            if (!string.IsNullOrEmpty(txtValuationAfterMaxCap.Text))
                oCompensationFinancial.CropValAftMaxCap = Convert.ToDecimal(txtValuationAfterMaxCap.Text.Trim());

            if (!string.IsNullOrEmpty(txtCropsDAinPercentage.Text))
                oCompensationFinancial.CropDA = Convert.ToDecimal(txtCropsDAinPercentage.Text.Trim());

            if (chkMaxCapCase.Checked)
            {
                if (!string.IsNullOrEmpty(txtValuationAfterMaxCap.Text) && !string.IsNullOrEmpty(txtCropsDAinAmount.Text))
                    oCompensationFinancial.CropTotalValuation = Convert.ToDecimal(txtValuationAfterMaxCap.Text.Trim()) + Convert.ToDecimal(txtCropsDAinAmount.Text.Trim());
            }
            else
            {
                if (!string.IsNullOrEmpty(txtCropsValuation.Text) && !string.IsNullOrEmpty(txtCropsDAinAmount.Text))
                    oCompensationFinancial.CropTotalValuation = Convert.ToDecimal(txtCropsValuation.Text.Trim()) + Convert.ToDecimal(txtCropsDAinAmount.Text.Trim());
            }
            strMax = txtCropsComments.Text.Trim();
            if (strMax.Trim().Length >= 1000)
            {
                strMax = txtLandComments.Text.Trim().Substring(0, 999);
            }
            oCompensationFinancial.CropComments = strMax;

            #endregion

            //OTHERS
            #region Others
            if (!string.IsNullOrEmpty(txtCulturePropertyValue.Text))
                oCompensationFinancial.CulturePropValuation = Convert.ToDecimal(txtCulturePropertyValue.Text.Trim());

            if (!string.IsNullOrEmpty(txtOtherDamagedCrops.Text))
                oCompensationFinancial.DamagedCropValuation = Convert.ToDecimal(txtOtherDamagedCrops.Text.Trim());

            if (!string.IsNullOrEmpty(txtCulturePropertyValue.Text) && !string.IsNullOrEmpty(txtOtherDamagedCrops.Text))
                oCompensationFinancial.TotalOtherValuation = Convert.ToDecimal(txtCulturePropertyValue.Text.Trim()) + Convert.ToDecimal(txtOtherDamagedCrops.Text.Trim());
            #endregion

            //SUMMERY
            #region Summery Section
            if (!string.IsNullOrEmpty(txtNegotiatedAmount.Text))
                oCompensationFinancial.NegotiatedAmount = Convert.ToDecimal(txtNegotiatedAmount.Text.Trim());

            if (!string.IsNullOrEmpty(txtInKindLand.Text))
                oCompensationFinancial.LandInKindCompensation = Convert.ToDecimal(txtInKindLand.Text.Trim());

            if (ddlResidentialStructure.SelectedItem.ToString() == "0")
                oCompensationFinancial.ResInKindCompensation = "-1";
            else
                oCompensationFinancial.ResInKindCompensation = ddlResidentialStructure.SelectedItem.ToString();
            if (txtFacilitationallowances.Text.Trim() != "")
                oCompensationFinancial.FacilitationAllowance = Convert.ToDecimal(txtFacilitationallowances.Text.Trim());
            //if (!string.IsNullOrEmpty(txtRSDepreciatedValue.Text))
            //    oCompensationFinancial.ResDepreciatedValue = Convert.ToDecimal(txtRSDepreciatedValue.Text.Trim());
            #endregion
            string message = oCompensationFinancialBLL.AddCompensationFinancial(oCompensationFinancial);
            if (message == "null" || message == null || message == "")
            {
                CompensationFinancialBO objCompensationFinancialBO = new CompensationFinancialBO();
                if (dpDeliveryDate.Text != null)
                    objCompensationFinancialBO.DeliveryDate = Convert.ToDateTime(dpDeliveryDate.Text);

                if (Convert.ToInt32(ddlDeliveredBy.SelectedValue.ToString()) > 0)
                {
                    objCompensationFinancialBO.DeliveredBy = Convert.ToInt32(ddlDeliveredBy.SelectedValue.ToString());
                }
                objCompensationFinancialBO.HHID = SessionHHID;
                objCompensationFinancialBO.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

                if (rdoActionRejected.Checked)
                    objCompensationFinancialBO.PAPAction = "N";
                else
                    objCompensationFinancialBO.PAPAction = "Y";
                string sDeliveryComments = txtPackageDeliveryInfoComments.Text.Trim();
                if (sDeliveryComments.Trim().Length >= 1000)
                {
                    sDeliveryComments = sDeliveryComments.Trim().Substring(0, 999);
                }
                objCompensationFinancialBO.DeliveryComments = sDeliveryComments;

                oCompensationFinancialBLL.AddPackageDeliveryInfo(objCompensationFinancialBO);
            }
            return message;
        }
        /// <summary>
        /// Call Clear method to Clear the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearItems();
        }
        #endregion

        #region Load Data
        /// <summary>
        /// Get the data from data base
        /// </summary>
        private void LoadData()
        {
            CompensationFinancialBO oCompensationFinancial = new CompensationFinancialBO();
            //oCompensationFinancial.Cmp_FinancialID = "";
            oCompensationFinancial.HHID = Convert.ToInt32(Session["HH_ID"]);
            CompensationFinancialBLL oCompensationFinancialBLL = new CompensationFinancialBLL();

            oCompensationFinancial = oCompensationFinancialBLL.GetCompensationFinancial(Convert.ToInt32(Session["HH_ID"]));

            if (oCompensationFinancial != null)
            {
                //LAND SECTION
                #region Land Section
                txtLandValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandValuation);//oCompensationFinancial.LandValuation.ToString();
                if (oCompensationFinancial.LandDA > 0)
                    txtLandDAinPercentage.Text = oCompensationFinancial.LandDA.ToString();
                txtAcreageDifferencePayment.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandDiffPayment); //oCompensationFinancial.LandDiffPayment.ToString();

                //if (!string.IsNullOrEmpty(txtLandValuation.Text) && !string.IsNullOrEmpty(txtLandDAinAmount.Text))
                //    oCompensationFinancial.LandTotalValuation = Convert.ToDecimal(txtLandValuation.Text.Trim()) + Convert.ToDecimal(txtLandDAinAmount.Text.Trim());

                txtLandTotal.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation); //oCompensationFinancial.LandTotalValuation.ToString();
                txtLandDAinAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.LandTotalValuation - oCompensationFinancial.LandValuation);//(oCompensationFinancial.LandTotalValuation - oCompensationFinancial.LandValuation).ToString();
                txtLandComments.Text = oCompensationFinancial.LandValComments;
                #endregion

                //RESIDENTIAL STRUCTURE
                #region Residencial Structure
                txtRSDepreciatedValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResDepreciatedValue);//oCompensationFinancial.ResDepreciatedValue.ToString();
                txtRSReplacementValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResReplacementValue);//oCompensationFinancial.ResReplacementValue.ToString();
                if (oCompensationFinancial.ResDA > 0)
                    txtRSDAinPercentage.Text = oCompensationFinancial.ResDA.ToString();

                txtRSMovingAllowance.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResMovingAllowance); //oCompensationFinancial.ResMovingAllowance.ToString();
                txtRSLabourCost.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResLabourCost); //oCompensationFinancial.ResLabourCost.ToString();
                txtRSPaymentHighHouseValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResPayment); //oCompensationFinancial.ResPayment.ToString();

                //if (!string.IsNullOrEmpty(txtRSReplacementValue.Text) && !string.IsNullOrEmpty(txtResidentialDAinAmount.Text))//Calculated mannually
                //    oCompensationFinancial.ResTotalValuation = Convert.ToDecimal(txtRSReplacementValue.Text.Trim()) + Convert.ToDecimal(txtResidentialDAinAmount.Text.Trim());

                //txtResidentialDAinAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResTotalValuation - oCompensationFinancial.ResReplacementValue); //(oCompensationFinancial.ResTotalValuation - oCompensationFinancial.ResReplacementValue).ToString();

                txtResidentialStructureComments.Text = oCompensationFinancial.ResComments;
                #endregion

                //FIXTURES
                #region Fixture Section

                txtFixturesValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixtureValuation); //oCompensationFinancial.FixtureValuation.ToString();
                if (oCompensationFinancial.FixtureDA > 0)
                    txtFixturesDAinPercentage.Text = oCompensationFinancial.FixtureDA.ToString();

                //if (!string.IsNullOrEmpty(txtFixturesDAinAmount.Text) && !string.IsNullOrEmpty(txtFixturesValuation.Text))
                //    oCompensationFinancial.FixtureTotalValuation = Convert.ToDecimal(txtFixturesDAinAmount.Text.Trim()) + Convert.ToDecimal(txtFixturesValuation.Text.Trim());

                txtFixturesDAinAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FixtureTotalValuation - oCompensationFinancial.FixtureValuation); //(oCompensationFinancial.FixtureTotalValuation - oCompensationFinancial.FixtureValuation).ToString();

                txtRSDepreciatedValue.Text = UtilBO.CurrencyFormat(oCompensationFinancial.ResDepreciatedValue); //oCompensationFinancial.ResDepreciatedValue.ToString();
                txtFixturesComments.Text = oCompensationFinancial.FixtureComments;
                #endregion

                //CROPS
                #region Crops Section
                txtCropsValuation.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropValuation); //oCompensationFinancial.CropValuation.ToString();

                //CheckBox

                if (oCompensationFinancial.CropMaxCapCase != null)
                {
                    if (oCompensationFinancial.CropMaxCapCase.ToUpper() == "YES")
                    {
                        chkMaxCapCase.Checked = true;
                    }
                    else
                    {
                        chkMaxCapCase.Checked = false;
                    }
                }
                else
                {
                    chkMaxCapCase.Checked = false;
                }

                txtValuationAfterMaxCap.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropValAftMaxCap); //oCompensationFinancial.CropValAftMaxCap.ToString();

                if (oCompensationFinancial.CropDA > 0)
                    txtCropsDAinPercentage.Text = oCompensationFinancial.CropDA.ToString();

                //if (!string.IsNullOrEmpty(txtCropsValuation.Text) && !string.IsNullOrEmpty(txtCropsDAinAmount.Text))
                //    oCompensationFinancial.CropTotalValuation = Convert.ToDecimal(txtCropsValuation.Text.Trim()) + Convert.ToDecimal(txtCropsDAinAmount.Text.Trim());

                if (oCompensationFinancial.CropMaxCapCase.ToUpper() == "YES")
                {
                    if (!string.IsNullOrEmpty(txtValuationAfterMaxCap.Text) && !string.IsNullOrEmpty(txtCropsDAinAmount.Text))
                      oCompensationFinancial.CropTotalValuation = Convert.ToDecimal(txtValuationAfterMaxCap.Text.Trim()) + Convert.ToDecimal(txtCropsDAinAmount.Text.Trim());
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtCropsValuation.Text) && !string.IsNullOrEmpty(txtCropsDAinAmount.Text))
                        oCompensationFinancial.CropTotalValuation = Convert.ToDecimal(txtCropsValuation.Text.Trim()) + Convert.ToDecimal(txtCropsDAinAmount.Text.Trim());
                }

                txtCropsDAinAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CropTotalValuation - oCompensationFinancial.CropValuation); //(oCompensationFinancial.CropTotalValuation - oCompensationFinancial.CropValuation).ToString();

                txtCropsComments.Text = oCompensationFinancial.CropComments;

                #endregion

                //OTHERS
                #region Others
                txtCompensationForCultureProperty.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropValuation); //oCompensationFinancial.CulturePropValuation.ToString();

                txtOtherDamagedCrops.Text = UtilBO.CurrencyFormat(oCompensationFinancial.DamagedCropValuation); //oCompensationFinancial.DamagedCropValuation.ToString();

                //if (!string.IsNullOrEmpty(txtCulturePropertyValue.Text) && !string.IsNullOrEmpty(txtOtherDamagedCrops.Text))
                //    oCompensationFinancial.TotalOtherValuation = Convert.ToDecimal(txtCulturePropertyValue.Text.Trim()) + Convert.ToDecimal(txtOtherDamagedCrops.Text.Trim());
                txtOthersTotal.Text = UtilBO.CurrencyFormat(oCompensationFinancial.CulturePropValuation + oCompensationFinancial.DamagedCropValuation); //(oCompensationFinancial.CulturePropValuation + oCompensationFinancial.DamagedCropValuation).ToString();
                #endregion

                //SUMMERY
                #region Summery Section
                txtNegotiatedAmount.Text = UtilBO.CurrencyFormat(oCompensationFinancial.NegotiatedAmount);

                decimal RDAllowance = (oCompensationFinancial.ResDepreciatedValue * oCompensationFinancial.FixtureDA) / 100;
                decimal SummeryFixturesValue = (((oCompensationFinancial.FixtureValuation * oCompensationFinancial.FixtureDA) / 100) + oCompensationFinancial.FixtureValuation);
                txtSummeryFixturesValue.Text = UtilBO.CurrencyFormat(SummeryFixturesValue); //SummeryFixturesValue.ToString();
                decimal DamagedCropsValue = (((oCompensationFinancial.CropValuation * oCompensationFinancial.CropDA) / 100) + oCompensationFinancial.CropValuation);
                txtDamagedCropsValue.Text = UtilBO.CurrencyFormat(DamagedCropsValue);// DamagedCropsValue.ToString();
                txtDamagedCropsValue.Text = txtOtherDamagedCrops.Text;
                txtCulturePropertyValue.Text = txtCompensationForCultureProperty.Text;
                txtInKindLand.Text = oCompensationFinancial.LandInKindCompensation.ToString().Trim();

                ddlResidentialStructure.ClearSelection();
                if (string.IsNullOrEmpty(oCompensationFinancial.ResInKindCompensation))
                    ddlResidentialStructure.SelectedValue = "0";
                else
                    ddlResidentialStructure.SelectedValue = oCompensationFinancial.ResInKindCompensation.ToString();

                txtFacilitationallowances.Text = UtilBO.CurrencyFormat(oCompensationFinancial.FacilitationAllowance);
                //if (!string.IsNullOrEmpty(txtRSDepreciatedValue.Text))
                //    oCompensationFinancial.ResDepreciatedValue = Convert.ToDecimal(txtRSDepreciatedValue.Text.Trim());
                #endregion

                //PACKAGE DELIVERY INFO
                #region Package Delivery Section
                CompensationFinancialBO ooCompensationFinancial = new CompensationFinancialBO();
                ooCompensationFinancial = oCompensationFinancialBLL.getPackageDeliveryInfo(SessionHHID);

                if (ooCompensationFinancial != null)
                {
                    if (ooCompensationFinancial.DeliveryDate != DateTime.MinValue)
                    {
                        dpDeliveryDate.Text = ooCompensationFinancial.DeliveryDate.ToString(UtilBO.DateFormat);
                    }
                    else
                    {
                        dpDeliveryDate.Text = "";
                    }

                    if (ooCompensationFinancial.PAPAction == "Y")
                        rdoActionAccepted.Checked = true;
                    else if (ooCompensationFinancial.PAPAction == "N")
                        rdoActionRejected.Checked = true;

                    ddlDeliveredBy.SelectedValue = ooCompensationFinancial.DeliveredBy.ToString();
                    txtPackageDeliveryInfoComments.Text = ooCompensationFinancial.DeliveryComments;
                }
                #endregion Package Delivery Section

                hdnDoCalc.Value = "1";

                btnSave.Text = "Update";
            }
            else
            {
                FinalValuationBLL oFinalValuationBLL = new FinalValuationBLL();
                FinalValuationBO oFinalValuationBO = new FinalValuationBO();

                if (SessionHHID > 0)
                {
                    oFinalValuationBO = oFinalValuationBLL.getFinalValuatin(SessionHHID);

                    if (oFinalValuationBO != null)
                    {
                        txtLandValuation.Text = UtilBO.CurrencyFormat(oFinalValuationBO.LandValue); //oFinalValuationBO.LandValue.ToString();
                        txtCropsValuation.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CropValue); //oFinalValuationBO.CropValue.ToString();
                        txtFixturesValuation.Text = UtilBO.CurrencyFormat(oFinalValuationBO.FixtureValue); //oFinalValuationBO.FixtureValue.ToString();
                        txtHouseValues.Text = UtilBO.CurrencyFormat(oFinalValuationBO.HouseValue); //oFinalValuationBO.HouseValue.ToString();
                        txtReplacementHouseValue.Text = UtilBO.CurrencyFormat(oFinalValuationBO.ReplacementValue); //oFinalValuationBO.ReplacementValue.ToString();

                        txtCompensationForCultureProperty.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CulturalpropertyValue); //oFinalValuationBO.CulturalpropertyValue.ToString();
                        txtOtherDamagedCrops.Text = UtilBO.CurrencyFormat(oFinalValuationBO.DamagedcropValue);// oFinalValuationBO.DamagedcropValue.ToString();
                        txtOthersTotal.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CulturalpropertyValue + oFinalValuationBO.DamagedcropValue); //(oFinalValuationBO.CulturalpropertyValue + oFinalValuationBO.DamagedcropValue).ToString();

                        txtDamagedCropsValue.Text = UtilBO.CurrencyFormat(oFinalValuationBO.DamagedcropValue); //oFinalValuationBO.DamagedcropValue.ToString();
                        txtCulturePropertyValue.Text = UtilBO.CurrencyFormat(oFinalValuationBO.CulturalpropertyValue); //oFinalValuationBO.CulturalpropertyValue.ToString();

                        txtRSDepreciatedValue.Text = UtilBO.CurrencyFormat(oFinalValuationBO.HouseValue); //oFinalValuationBO.HouseValue.ToString();
                        txtRSReplacementValue.Text = UtilBO.CurrencyFormat(oFinalValuationBO.ReplacementValue); //oFinalValuationBO.ReplacementValue.ToString();
                        txtNegotiatedAmount.Text = UtilBO.CurrencyFormat(oFinalValuationBO.NegotiatedAmount); //oFinalValuationBO.NegotiatedAmount.ToString();
                        txtRSLabourCost.Text = UtilBO.CurrencyFormat(oFinalValuationBO.ResLabourCost);
                        //txtFacilitationallowances.Text = "0";
                        txtFacilitationallowances.Text = UtilBO.CurrencyFormat(oFinalValuationBO.GOUAllowance);

                        if (oFinalValuationBO.Crop_Max_Cap_Case.ToLower() == "yes")
                            chkMaxCapCase.Checked = true;
                        else
                            chkMaxCapCase.Checked = false;
                        txtValuationAfterMaxCap.Text = UtilBO.CurrencyFormat(oFinalValuationBO.Crop_Val_Aft_Max_Cap);

                        hdnDoCalc.Value = "1";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CalTotal", "CalcLandTotal();CalcResidenitalTotal();CalcFixtureTotal();CalcCropTotal();CalcOtherTotal();FindTotalAmount();", true);
                    }
                }
            }

        }
        /// <summary>
        /// Bind values to Drop down ddlDeliveredBy
        /// </summary>
        private void BindDerivedBy()//These are Project Users
        {
            ListItem firstListItem = new ListItem(ddlDeliveredBy.Items[0].Text, ddlDeliveredBy.Items[0].Value);
            ProjectPersonalBLL objProjPersonalLogic = new ProjectPersonalBLL();

            if (Session["PROJECT_ID"] != null)
            {
                //Edwin: 30MAY2016 - 
                ProjectPersonalList ProjectPersonnels = objProjPersonalLogic.GetAllPersonnel(Convert.ToInt32(Session["PROJECT_ID"]));
                ddlDeliveredBy.ClearSelection();
                ddlDeliveredBy.Items.Clear();
                if (ProjectPersonnels != null)
                {
                    ddlDeliveredBy.DataSource = ProjectPersonnels;
                    ddlDeliveredBy.DataTextField = "Username";
                    ddlDeliveredBy.DataValueField = "UserID";
                    ddlDeliveredBy.DataBind();
                }
                ddlDeliveredBy.Items.Insert(0, firstListItem);
            }
        }
        /// <summary>
        /// Get round of Value
        /// </summary>
        private void Load_RoundOffValue()
        {
            WIS_ConfigBLL oWIS_ConfigBLL = new WIS_ConfigBLL();
            string roundOffValue = oWIS_ConfigBLL.getRoundOffLimit();
            if (!string.IsNullOrEmpty(roundOffValue))
            {
                hdnRoundOffValue.Value = roundOffValue;
            }
        }
        #endregion Load Data
    }
}