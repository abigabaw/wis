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
    public partial class PackageClosingInfo : System.Web.UI.Page
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
            caldpcSettlementDate.Format = UtilBO.DateFormat;

            Page.Response.Cache.SetNoStore();


            if (Session["PROJECT_CODE"] != null)
            {
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Package Closing Info";
            }
            else
            {
                Response.Redirect(ResolveUrl("~/UI/Project/ViewProjects.aspx"));
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect(ResolveUrl("~/UI/Compensation/PAPList.aspx"));
            }

            if (!IsPostBack)
            {
                string paramUploadDoc = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}','{5}');", Convert.ToInt32(Session["PROJECT_ID"]), Session["HH_ID"], Session["USER_ID"], Session["PROJECT_CODE"].ToString(), "NEW_LOC", 0);
                lnkUploadDoc.Attributes.Add("onclick", paramUploadDoc);
                dpcSettlementDate1.Attributes.Add("readonly", "readonly");
                Clear();
                LoadSummery();
                BindDropDownDistrict();
                LoadNewLocation();
                //getCompensationStatus();

                DisplayNewLocation();

                BindGrid(true, true);
                getFileClosingComments();
                //checkApprovalExistsForPaymentVerification();

                //checkApprovalExitOrNot();
                dependencyStatusCheck();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PACKAGE_CLOSING_INFO) == false)
                {
                    lnkValuationPCI.Visible = false;
                    lnkUploadDoc.Visible = false;
                    btnCompStatusSave.Visible = false;
                    btnNewLocationSave.Visible = false;
                    btnNewLocationClear.Visible = false;
                    ddlCompensationStatus.Enabled = false;
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
                //lnkPaymentVerification.Visible = false;
                lnkValuationPCI.Visible = false;
                lnkUploadDoc.Visible = false;
                btnCompStatusSave.Visible = false;
                btnNewLocationSave.Visible = false;
                btnNewLocationClear.Visible = false;
            }
        }
        /// <summary>
        /// Get Closing Comments
        /// </summary>
        private void getFileClosingComments()
        {
            PaymentBO OPaymentBO = new PaymentBO();
            OPaymentBO = (new PaymentBLL()).GetFileclosingComments(Convert.ToInt32(Session["HH_ID"]));
            if (OPaymentBO != null)
            {
                if(OPaymentBO.FILECLOSINGCOMMENTS != null)
                    TxtFileClosingcomments.Text = OPaymentBO.FILECLOSINGCOMMENTS;
                chkOverrideGriv.Checked = Convert.ToBoolean(OPaymentBO.GRIEVOVERRIDE);
                Span1.Style.Add("display", "");
            }
        }
        /// <summary>
        /// Get New Location Details to Display
        /// </summary>
        private void DisplayNewLocation()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            PAP_HouseholdBLL PAP_HouseholdBLLobj = new PAP_HouseholdBLL();
            string PDP_Present = PAP_HouseholdBLLobj.IsPDP(householdID);

            if (PDP_Present.ToUpper() == "Y")
                pnlNewLocation.Visible = true;
            else
                pnlNewLocation.Visible = false;

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

        #endregion
        
        #region Clear Buttons
        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewLocationClear_Click(object sender, EventArgs e)
        {
            ClearNewLocation();
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSummeryClear_Click(object sender, EventArgs e)
        {
            ClearSummery();
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompStatusClear_Click(object sender, EventArgs e)
        {
            ClearCompensationStatus();
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearSummery()
        {
            txtInKindLand.Text = string.Empty;
            //ddlResidentialStructure.SelectedValue = "0";
            //txtFacilitation.Text = string.Empty;
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear()
        {
            ClearSummery();
            ClearNewLocation();
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearNewLocation()
        {
            txtNewPlotNumber.Text = string.Empty;
            txtDistanceFromOldPlot.Text = string.Empty;

            /*
            ddlDistrict.SelectedIndex = 0;

            ddlCounty.Items.Clear();
            ddlCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlCounty.SelectedIndex = 0;

            ddlSubCounty.Items.Clear();
            ddlSubCounty.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlSubCounty.SelectedIndex = 0;

            ddlVillage.Items.Clear();
            ddlVillage.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlVillage.SelectedIndex = 0;

            ddlParish.Items.Clear();
            ddlParish.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlParish.SelectedIndex = 0;
            */
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();


            //dpcDeliveredDate.DateFormat = "";
            //dpcDeliveredDate.CalendarDate = Convert.ToDateTime(null) ;
        }

        /// <summary>
        /// to Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearCompensationStatus()
        {
            ddlCompensationStatus.SelectedValue = "NP";
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// Load Summery details
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

                lblTestToalAmount.Text = lblTotalAmount.Text;

                if (string.IsNullOrEmpty(oCompensationFinancial.ResInKindCompensation))
                    ddlResidentialStructure.SelectedValue = "0";
                else
                    ddlResidentialStructure.SelectedValue = oCompensationFinancial.ResInKindCompensation.ToString();

                SessionHHID = oCompensationFinancial.HHID;
                //ResidentialStructure();
            }
            LoadCompensationFinancial();
            GetPAPValuationSummery_NegotiatedAmount();
            //DISABLE ALL TEXTBOXES IN SUMMERY FOR EDITING 
            txtCashLand.Enabled = false;
            txtFixture.Enabled = false;
            txtCrops.Enabled = false;
            txtResStructure.Enabled = false;
            txtCulutralProperty.Enabled = false;
            txtDamaged.Enabled = false;
            txtInKindLand.Enabled = false;
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
        /// Get Negotiated Amount
        /// </summary>
        private void GetPAPValuationSummery_NegotiatedAmount()
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO oPaymentBO = new PaymentBO();
            oPaymentBO = oPaymentBLL.getPapValuation(SessionHHID);

            if (oPaymentBO != null && oPaymentBO.NegotiatedAmountApproved.ToUpper() == "Y")
            {
                trNegotiatedAmount.Visible = true;
                trNegotiatedAmount.BgColor = "#E4e4e4";
            }
            else
            {
                trNegotiatedAmount.Visible = false;
                // trNegotiatedAmount.Visible = true;
                // trNegotiatedAmount.BgColor = "#F0F8FF";
            }
        }
        /// <summary>
        /// Load Compensation Financial Details
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

                if (!string.IsNullOrEmpty(oCompensationFinancialBO.ResInKindCompensation))
                    ddlResidentialStructure.SelectedValue = oCompensationFinancialBO.ResInKindCompensation;
                else
                    ddlResidentialStructure.SelectedValue = "0";

                if (ddlResidentialStructure.SelectedIndex > 0)
                {
                    txtResidentialStructure.Text = ddlResidentialStructure.SelectedItem.Text;
                }

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
            }
        }
        /// <summary>
        ///  Load New Location Details
        /// </summary>
        private void LoadNewLocation()
        {
            NewLocationBLL oNewLocationBLL = new NewLocationBLL();
            NewLocationBO oNewLocationBO = new NewLocationBO();
            oNewLocationBO = oNewLocationBLL.GetNewLocation(SessionHHID);

            if (oNewLocationBO != null)
            {
                btnNewLocationClear.Visible = false;
                btnNewLocationSave.Visible = false;
                try
                {
                    if (oNewLocationBO.NewPlotNo != null)
                        txtNewPlotNumber.Text = oNewLocationBO.NewPlotNo;

                    if (oNewLocationBO.DistanceFromOldPlot != null)
                        txtDistanceFromOldPlot.Text = oNewLocationBO.DistanceFromOldPlot;
                    if (oNewLocationBO.DateOfSettlement != DateTime.MinValue)
                        dpcSettlementDate1.Text = (oNewLocationBO.DateOfSettlement.ToString(UtilBO.DateFormat));
                    else
                        dpcSettlementDate1.Text = "";

                    if (oNewLocationBO.District != null)
                    {
                        ddlDistrict.ClearSelection();
                        if (ddlDistrict.Items.FindByText(oNewLocationBO.District) != null)
                            ddlDistrict.Items.FindByText(oNewLocationBO.District).Selected = true;
                    }

                    if (oNewLocationBO.County != null)
                    {
                        BindCounties(ddlDistrict.SelectedValue);

                        ddlCounty.ClearSelection();
                        if (ddlCounty.Items.FindByText(oNewLocationBO.County) != null)
                            ddlCounty.Items.FindByText(oNewLocationBO.County).Selected = true;
                    }

                    if (oNewLocationBO.SubCounty != null)
                    {
                        BindSubCounties(ddlCounty.SelectedValue);

                        ddlSubCounty.ClearSelection();
                        if (ddlSubCounty.Items.FindByText(oNewLocationBO.SubCounty) != null)
                            ddlSubCounty.Items.FindByText(oNewLocationBO.SubCounty).Selected = true;
                    }

                    if (oNewLocationBO.Village != null)
                    {
                        BindVillages(ddlSubCounty.SelectedValue);

                        ddlVillage.ClearSelection();
                        if (ddlVillage.Items.FindByText(oNewLocationBO.Village) != null)
                            ddlVillage.Items.FindByText(oNewLocationBO.Village).Selected = true;

                    }

                    if (oNewLocationBO.Parish != null)
                    {
                        BindParishes(ddlSubCounty.SelectedValue);

                        ddlParish.ClearSelection();
                        if (ddlParish.Items.FindByText(oNewLocationBO.Parish) != null)
                            ddlParish.Items.FindByText(oNewLocationBO.Parish).Selected = true;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                btnNewLocationClear.Visible = true;
                btnNewLocationSave.Visible = true;
            }
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
        private void BindDropDownDistrict()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, (new ListItem("--Select--", "0")));
            ddlDistrict.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
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
            ddlCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
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
            ddlSubCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
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
            ddlParish.SelectedIndex = 0;
        }
        /// <summary>
        /// Bind Drodown Data
        /// </summary>
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
            ddlVillage.SelectedIndex = 0;
        }

        #endregion

        #region GridView
        /// <summary>
        /// Bind Grid Data
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

            if (grdPaymentDetails.Rows.Count > 0)
                EnablePayVerificationReqButton(true);
            else
                EnablePayVerificationReqButton(false);
            if (isent > 0 || ipending > 0)
                HfPaymentStatus.Value = "Pending";
        }

        protected void grdPaymentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

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
        /// To Check Status
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
                Literal FundReqStatus = (Literal)e.Row.FindControl("litFundReqStatus");                
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
            }
        }
        /// <summary>
        /// Set Formt to Number
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
        /// To Save Summary
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
            LoadSummery();
            LoadCompensationFinancial();
            BindGrid(true, true);
        }
        /// <summary>
        /// To Close the PAP 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkValuationPCI_Click(object sender, EventArgs e)
        {
            string FileClosingCom = TxtFileClosingcomments.Text.Trim();
            if (FileClosingCom.Length > 1000)
                FileClosingCom = FileClosingCom.Substring(0, 999);
            PaymentBLL oPaymentBLL = new PaymentBLL();
            string message ="";
            if (FileClosingCom.Length > 0)
                message = oPaymentBLL.SaveFileclosingComments(SessionHHID, FileClosingCom, chkOverrideGriv.Checked);

        }
        /// <summary>
        /// To save Compensation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompStatusSave_Click(object sender, EventArgs e)
        {
            SaveCompensationStatus();
        }
        /// <summary>
        /// To save New Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewLocationSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            NewLocationBO oNewLocationBO = new NewLocationBO();


            if (dpcSettlementDate1.Text != null)
                oNewLocationBO.DateOfSettlement = Convert.ToDateTime(dpcSettlementDate1.Text.ToString());

            oNewLocationBO.HHID = SessionHHID;
            oNewLocationBO.NewPlotNo = txtNewPlotNumber.Text;
            oNewLocationBO.NewPlotStatusId = 1;
            oNewLocationBO.DistanceFromOldPlot = txtDistanceFromOldPlot.Text;
            if (ddlDistrict.Items.Count > 0)
                oNewLocationBO.District = ddlDistrict.SelectedItem.ToString();
            if (ddlCounty.Items.Count > 0)
                oNewLocationBO.County = ddlCounty.SelectedItem.ToString();
            if (ddlSubCounty.Items.Count > 0)
                oNewLocationBO.SubCounty = ddlSubCounty.SelectedItem.ToString();
            if (ddlParish.Items.Count > 0)
                oNewLocationBO.Parish = ddlParish.SelectedItem.ToString();
            if (ddlVillage.Items.Count > 0)
                oNewLocationBO.Village = ddlVillage.SelectedItem.ToString();
            oNewLocationBO.CreatedBy = SessionUserId;
            oNewLocationBO.IsDeleted = "False";

            NewLocationBLL oNewLocationBLL = new NewLocationBLL();
            message = oNewLocationBLL.AddNewLocation(oNewLocationBO);
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

        }
        /// <summary>
        /// To Delete
        /// </summary>
        private void DeleteCompositionPayment()
        {
            PaymentBLL oBatchBLL = new PaymentBLL();
            int CompPaymentId = 0;
            int Result = 0;

            CompPaymentId = ViewStateCompPaymentID;

            Result = oBatchBLL.DeleteCompositionPayment(CompPaymentId);
            BindGrid(false, true);
        }
        /// <summary>
        /// To get Compensation Status
        /// </summary>
        private void getCompensationStatus()
        {
            PaymentBLL oPaymentBLL = new PaymentBLL();
            PaymentBO oPaymentBO;//=new PaymentBO();
            oPaymentBO = oPaymentBLL.getPapValuation(SessionHHID);
            if (oPaymentBO != null)
            {
                if (!string.IsNullOrEmpty(oPaymentBO.PaymentStatus))
                {
                    // AcutalAmountToBePaid = Convert.ToDecimal(lblTotalAmount.Text);

                    if (oPaymentBO.PaymentStatus.ToLower() == "CP".ToLower())
                        ddlCompensationStatus.SelectedValue = "CP";
                    else if (oPaymentBO.PaymentStatus.ToLower() == "PP".ToLower())
                        ddlCompensationStatus.SelectedValue = "PP";
                    else if (oPaymentBO.PaymentStatus.ToLower() == "NP".ToLower())
                        ddlCompensationStatus.SelectedValue = "NP";
                }
                //if (ddlCompensationStatus.SelectedValue == "CP")
                //    EnableButtons(false);
                //else
                //    EnableButtons(true);
            }
        }

        //private void EnableButtons(bool val)
        //{
        //    pnlPaymentMode.Visible = val;
        //    pnlSummery.Visible = val;
        //}

        /// <summary>
        /// To Save Compensation Status
        /// </summary>
        private void SaveCompensationStatus()
        {
            string message = string.Empty, AlertMessage = string.Empty;
            PaymentBLL oPaymentBLL = new PaymentBLL();
            message = oPaymentBLL.UpdatePapValutaion(SessionHHID, ddlCompensationStatus.SelectedValue);

            getCompensationStatus();
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        #endregion
        /// <summary>
        /// To override Griviance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkOverrideGriv_CheckedChanged(object sender, EventArgs e)
        {
            lnkValuationPCI.Visible = chkOverrideGriv.Checked;
        }

        #region Change Request Approval
        /// <summary>
        /// check Approval Exit Or Not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            lblStatusValuationPCI.Text = string.Empty;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalFL;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "CREND"; //Complition of End of File Request

            if (objWorkFlowBO != null)
            {
                lnkValuationPCI.Attributes.Clear();
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}');", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                lnkValuationPCI.Attributes.Add("onclick", paramChangeRequest);

                lnkValuationPCI.Visible = true;
            }
            else
            {
                lnkValuationPCI.Visible = false;
            }
            #endregion
            getAppoverReqStatusPakClos();

        }
        /// <summary>
        /// Get Request status
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
                #region When Approver Defined
                oHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                int HHID = Convert.ToInt32(Session["HH_ID"]);
                oHouseHold.HhId = HHID;
                oHouseHold.PageCode = "CREND";
                oHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalFL;

                oHouseHold = oHouseHoldBLL.ApprovalChangerequestStatus(oHouseHold);//get Status of Request

                #region When Package is NOT Closed

                int ApproverStatus = CheckAllApproverLevels("CREND", UtilBO.WorkflowChangeRequestApprovalFL);

                if (oHouseHold != null && ApproverStatus != 1)//oHouseHold.ApproverStatus != 1)
                {
                    #region When Approval Request is Sent
                    if (checkGrievanceRequet())
                    {
                        if ((oHouseHold) != null)
                        {
                            //When Request is sent for Approval then check the Status of Responce from the Approver
                            if (ApproverStatus == 3)//if (oHouseHold.ApproverStatus == 3)
                            {
                                //PENDING=3
                                lnkValuationPCI.Visible = false;
                                btnCompStatusSave.Visible = false;
                                Span1.Style.Add("display", "");
                                TxtFileClosingcomments.Enabled = false;
                                chkOverrideGriv.Enabled = false;

                                chkOverrideGriv.Visible = true;
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Approval Pending";
                                ddlCompensationStatus.ClearSelection();
                                ddlCompensationStatus.SelectedValue = "CP";
                                AprovedStatus(true);
                            }
                            else if (ApproverStatus == 2) // if (oHouseHold.ApproverStatus == 2)
                            {
                                //DECLINED=2
                                lnkValuationPCI.Visible = true;
                                Span1.Style.Add("display", "");
                                TxtFileClosingcomments.Enabled = true;
                                chkOverrideGriv.Enabled = true;
                                btnCompStatusSave.Visible = false;
                                chkOverrideGriv.Visible = true;
                                ddlCompensationStatus.ClearSelection();
                                ddlCompensationStatus.SelectedValue = "CP";
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Approver Declined";
                            }
                            else if (ApproverStatus == 1) //if (oHouseHold.ApproverStatus == 1)
                            {
                                //APPROVED=1
                                lnkValuationPCI.Visible = false;
                                btnCompStatusSave.Visible = true;
                                getCompensationStatus();

                                chkOverrideGriv.Visible = true;
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Green;
                                lblStatusValuationPCI.Text = "PAP file is closed.";
                                AprovedStatus(true);
                                Span1.Style.Add("display", "");
                                TxtFileClosingcomments.Enabled = false;
                                chkOverrideGriv.Enabled = false;
                                ddlCompensationStatus.ClearSelection();
                                ddlCompensationStatus.SelectedValue = "CP";
                            }
                            else
                            {
                                AprovedStatus(false);
                            }
                        }
                        else
                        {
                            //When Approval Request Not Sent then Execute this Section
                            //All Criteria must be Satisfied for Sending Approval Request
                            if (!checkGrievanceRequet())
                            {
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Grievance is OPEN";
                            
                            }
                            else if (!checkPaymentRequest())
                            {
                                //lblStatusValuationPCI.Text = "Payment Verification Request Pending";
                                lblStatusValuationPCI.Text = "";
                            }
                            else if (oWorkFlowBO == null)
                            {
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Approver Not Defined";
                            }
                            //For Enabling Approver/Save
                            else if (ddlCompensationStatus.SelectedValue.ToLower() != "CP".ToLower())
                            {
                                lnkValuationPCI.Visible = false;
                                btnCompStatusSave.Visible = true;
                            }
                            else if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower())
                            {
                                lnkValuationPCI.Visible = true;
                                btnCompStatusSave.Visible = false;
                                Span1.Style.Add("display", "");
                            }
                        }
                    }
                    #endregion When Approval Request is Sent

                    #region When Request Sent & Status is NOT Approved
                    else
                    {
                        lnkValuationPCI.Visible = false;
                        btnCompStatusSave.Visible = true;
                        //lblStatusValuationPCI.Visible = true;
                        //if (!checkPaymentRequest())
                        //{
                        //    lnkValuationPCI.Visible = false;
                        //    btnCompStatusSave.Visible = true;

                        //    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                        //    lblStatusValuationPCI.Text = "Payment Verification Approval Pending";
                        //}
                        //else 
                        if (!checkGrievanceRequet())
                        {
                            if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower())
                                chkOverrideGriv.Visible = true;
                            lnkValuationPCI.Visible = false;
                            btnCompStatusSave.Visible = true;

                            lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                            lblStatusValuationPCI.Text = "Grievance is OPEN";
                            pnlCompensationStatus.Visible = true;
                        }
                        else if (ddlCompensationStatus.SelectedValue.ToLower() != "CP".ToLower())
                        {
                            lnkValuationPCI.Visible = false;
                            btnCompStatusSave.Visible = true;
                            chkOverrideGriv.Visible = false;
                        }
                        else if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower())
                        {
                            lnkValuationPCI.Visible = false;
                            btnCompStatusSave.Visible = false;
                            chkOverrideGriv.Visible = false;
                        }
                    }
                    #endregion  When Request Sent & Status is NOT Approved
                }
                #endregion When Package is NOT Closed

                #region When Package is Closed
                else if (oHouseHold != null && oHouseHold.ApproverStatus == 1)
                {
                    //Sent for Approval & it is Approved
                    AprovedStatus(true);
                    lnkValuationPCI.Visible = false;
                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Green;
                    lblStatusValuationPCI.Text = "PAP file is closed.";
                    chkOverrideGriv.Visible = false;
                }
                else if (oHouseHold == null && ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower() && HfPaymentStatus.Value.ToString() == "Pending")
                {
                    lblStatusValuationPCI.Text = "Payment Verification Request is Pending";
                    //lblStatusValuationPCI.Text = "";
                    lnkValuationPCI.Visible = false;
                    btnCompStatusSave.Visible = false;
                    chkOverrideGriv.Visible = false;
                }
                else if (oHouseHold == null && ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower() && !checkGrievanceRequet())// && oHouseHold.ApproverStatus == 2)
                {
                    //When Payment Request Approved & Compensation Status is 'Compeletely Paid' and Sending Request for the First Time

                    if (!checkGrievanceRequet())
                    {
                        if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower())
                            chkOverrideGriv.Visible = true;
                        lnkValuationPCI.Visible = false;
                        btnCompStatusSave.Visible = false;
                        lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                        lblStatusValuationPCI.Text = "Grievance is OPEN";
                        pnlCompensationStatus.Visible = true;
                    }
                    else
                    {
                        //First Time File Closure Request is Visible only when
                        //1. When Request is not sent
                        //2. When Completely Paid (CP) is Selected
                        //3. When Payment Verification is Approved
                        //4. When Grivence is NOT Open/Resolved
                        //lblStatusValuationPCI.Text = "Pending Request for Approval";
                        lnkValuationPCI.Visible = true;
                        btnCompStatusSave.Visible = false;
                        pnlCompensationStatus.Visible = true;
                        chkOverrideGriv.Visible = false;
                        Span1.Style.Add("display", "");
                    }
                }
                else if (oHouseHold == null && ddlCompensationStatus.SelectedValue.ToLower() != "CP".ToLower())// && checkPaymentRequest())
                {
                    lblStatusValuationPCI.Text = string.Empty;
                    lnkValuationPCI.Visible = false;
                    btnCompStatusSave.Visible = true;
                    chkOverrideGriv.Visible = false;
                }
                #endregion When Package is Closed

                #endregion When Approver Defined
            }
            else
            {
                #region When Approver Not Defined
                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                lblStatusValuationPCI.Text = "Approver Not Defined";
                #endregion When Approver Not Defined
            }
        }
        private void AprovedStatus(bool Status)
        {
            if (Status)
            {
                ddlCompensationStatus.Enabled = false;
                btnCompStatusSave.Visible = false;
            }
            else
            {
                ddlCompensationStatus.Enabled = true;
                btnCompStatusSave.Visible = true;
            }
        }
        #endregion Change Request Approval

        #region Change Request for Payment Verification
        /// <summary>
        /// check Approval Exit Or Not
        /// </summary>
        public void checkApprovalExistsForPaymentVerification()
        {
            #region Enable ChangeRequest Button
            lblStatusValuationPCI.Text = string.Empty;
            //lblStatusValuationPCI.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.PaymentVerificationCode;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "CRFND";

            //if (objWorkFlowBO != null)
            //{
            //    //lnkPaymentVerification.Attributes.Clear();
            //    //string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}');", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
            //    //lnkPaymentVerification.Attributes.Add("onclick", paramChangeRequest);

            //    //if (grdPaymentDetails.Rows.Count > 0)
            //    //{
            //    //    lnkPaymentVerification.Visible = true;
            //    //}
            //    //else
            //    //    lnkPaymentVerification.Visible = false;
            //}
            //else
            //{
            //    //----If APPROVER does Not Exists then disable the Button
            //    //lnkValuationPCI.Visible = false;
            //    //lnkPaymentVerification.Visible = false;
            //    lblPaytVerification.Text = "Approver Not Defined";
            //}
            #endregion

            getAppoverReqStatusPaymentVerification();
        }
        /// <summary>
        /// Get Request status
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
                if (ApproverStatus == 1) //(objHouseHold.ApproverStatus == 1)
                {
                    //APPROVED
                    //lblPaytVerification.Text = "<font class='StatusApproved'>Approved</font>";
                    //lnkPaymentVerification.Visible = false;
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
                    //---------------Checking Existance of Approver For File Closure---------------------
                    WorkFlowBO objWorkFlowBO = new WorkFlowBO();
                    WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

                    string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalFL;

                    objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

                    if (oHouseHold != null)
                    {
                        ddlCompensationStatus.ClearSelection();
                        getCompensationStatus();
                    }
                    if (oHouseHold != null && oHouseHold.ApproverStatus != 1)//Added New Code on 1 July 2013
                    {
                        if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower() && objWorkFlowBO != null && checkPackegeReview() && checkGrievanceRequet())
                        {
                            if (oHouseHold == null)
                            {
                                lnkValuationPCI.Visible = true;
                                Span1.Style.Add("display", "");
                            }
                            else
                            {
                                if (oHouseHold.ApproverStatus == 3)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                    lblStatusValuationPCI.Text = "Pending Approval";
                                    lnkValuationPCI.Visible = false;
                                    Span1.Style.Add("display", "none");
                                }
                                else if (oHouseHold.ApproverStatus == 2)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                    lblStatusValuationPCI.Text = "Approver Declined";
                                    lnkValuationPCI.Visible = true;
                                    Span1.Style.Add("display", "");
                                }
                                else if (oHouseHold.ApproverStatus == 1)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Green;
                                    lblStatusValuationPCI.Text = "Approved";
                                    lnkValuationPCI.Visible = false;
                                    Span1.Style.Add("display", "");
                                    TxtFileClosingcomments.Enabled = false;
                                }
                            }
                            //lblStatusValuationPCI.Visible = false;
                        }
                        else
                        {
                            lnkValuationPCI.Visible = false;
                            //lblStatusValuationPCI.Visible = true;
                            if (oHouseHold != null)
                            {
                                if (oHouseHold.ApproverStatus == 3)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                    lblStatusValuationPCI.Text = "Pending Approval";
                                }
                                else if (oHouseHold.ApproverStatus == 2)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                    lblStatusValuationPCI.Text = "Declined";
                                }
                                else if (oHouseHold.ApproverStatus == 1)
                                {
                                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Green;
                                    lblStatusValuationPCI.Text = "Approved";
                                }
                            }
                            else if (!checkGrievanceRequet())
                            {
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Grievance Request Pending";
                            }
                            else if (objWorkFlowBO == null)
                            {
                                lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                                lblStatusValuationPCI.Text = "Approver Not Defined";
                            }
                        }
                    }//If already Approved/Package Closed dont execute this part
                    //else if (oHouseHold != null && oHouseHold.ApproverStatus == 1)
                    //{
                    //    AprovedStatus(true);
                    //    //ddlCompensationStatus.Enabled = false;
                    //    lnkValuationPCI.Visible = false;
                    //    lblStatusValuationPCI.Text = "Approved";
                    //    //btnCompStatusSave.Visible = false;
                    //}
                }
                else
                {
                    //if (grdPaymentDetails.Rows.Count > 0)
                    //{
                    //    lnkPaymentVerification.Visible = true;
                    //}
                    //else
                    //    lnkPaymentVerification.Visible = false;
                }
            }
            else
            {
                lnkValuationPCI.Visible = false;
            }
        }
        #endregion Change Request for Payment Verification

        #region Fund Request, Package Document Status & Grievance Request Verification
        /// <summary>
        /// Check Request status
        /// </summary>
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
        /// <summary>
        /// Check Request status
        /// </summary>
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
        /// <summary>
        /// Check Request status
        /// </summary>
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
            if (chkOverrideGriv.Checked)
            {
                return true;
            }
            else
            {
                if (oGrievancesBO.Status == "1" || oGrievancesBO.Status.Trim() == "")
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Check Request status
        /// </summary>
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
        /// <summary>
        /// Check Request status
        /// </summary>
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
            }
            else
            {
                //lnkPaymentVerification.Visible = false;
            }
        }
        /// <summary>
        /// Check Request status
        /// </summary>
        private void dependencyStatusCheck()
        {
            //checkApprovalExistsForPaymentVerification();
            checkApprovalExitOrNot();
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

            if (checkPackegeReview() && CountCheckBoxes > 0)// && checkGrievanceRequet())
            {
                //Check box Count Added Here: When Atleast one Approved Status(Checkbox) is Required for Payment
                pnlPaymentMode.Visible = true;
                pnlCompensationStatus.Visible = true;
                lblPaymentStatusMessage.Text = string.Empty;
                lblPaymentStatusMessage.Visible = false;
            }
            else
            {
                //pnlCompensationStatus.Visible = false;
                //pnlPaymentMode.Visible = false;
                lblPaymentStatusMessage.Text = strBuild.ToString();//"<b><u>Following Approvals are Pending :</u> </br> 1. Package Review  </br> 2. Fund Request</b>";
                lblPaymentStatusMessage.Visible = true;
            }
        }

        #region DropDown Index Changed Events & Methods
        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedValue);
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindCounties();

            BindCounties(ddlDistrict.SelectedValue);
            BindSubCounties(ddlCounty.SelectedValue);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedValue);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedValue);
            uplParish.Update();
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedValue);
            BindParishes(ddlSubCounty.SelectedValue);
        }

        /// <summary>
        /// Call Respective methos to fill data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCompensationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkOverrideGriv.Visible = false;
            chkOverrideGriv.Checked = false;
            if (ddlCompensationStatus.SelectedValue.ToLower() == "CP".ToLower())
            {
                Span1.Style.Add("display", "");
                if (HfPaymentStatus.Value.ToString() == "Pending")
                {
                    lblStatusValuationPCI.ForeColor = System.Drawing.Color.Red;
                    lblStatusValuationPCI.Text = "Payment Verification Approvals Pending.";
                    btnCompStatusSave.Visible = false;
                }
                else
                {
                    Span1.Style.Add("display", "");
                    lnkValuationPCI.Visible = true;
                    getAppoverReqStatusPakClos();
                    btnCompStatusSave.Visible = false;
                }
            }
            else
            {
                Span1.Style.Add("display", "none");
                lblStatusValuationPCI.Text = "";
                btnCompStatusSave.Visible = true;
                lnkValuationPCI.Visible = false;
            }
        }

        #endregion
        /// <summary>
        /// Check all requests
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
    }
}