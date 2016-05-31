using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

namespace WIS
{
    public partial class AcreageValuation : System.Web.UI.Page
    {

        #region Global Declaration
        MasterBLL objMasterBLL;
        ProprietorList objProprietorList;
        SurveyBLL objSurveyBLL;
        UnitBLL objUnitBLL;
        UnitList objUnitList;
        AffectedAcreageValuationBO objAcreageValuation;
        private const double acrehaconvert = 2.47105;
        private const double acresqmetreconvert = 0.000247105;
        private const double hasqmtreconvert = 0.00010000;

        #endregion

        #region PageEvents

        /// <summary>
        /// Set Page header,Call methods to fill the Drop Downs and Fetch the data from Database
        /// Check User Permitions
        /// Check Mode is readonly or not
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.AcreageValuation;
            // CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.AcreageValuation;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.AffectedAcreageValuation;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS", CreateStartupScript());
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Land Information - Affected Acreage Valuation";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                AddShareOfLandValue();
                BindLandType();
                BindLocationClassification();
                BindProprietor();
                GetHouseholdDetails();
                GetAffectedAcreageValuation();


                //txtRightWay.ReadOnly = true;
                //txtWayleave.ReadOnly = true;
                //txtWayleave.Attributes.Add("onKeyDown", "doCheck();");
                //txtRightWay.Attributes.Add("onKeyDown", "doCheck();");
                txtAcreageHA.Attributes.Add("onKeyDown", "doCheck();");
                //txtAcreageAcres.Attributes.Add("onKeyDown", "doCheck();");
                checkApprovalExitOrNot();
                projectFrozen();
                getApprrequtStatusAcrValuation();
                txtLandOwner.Attributes.Add("onchange", "setDirtyText();");
                txtLandBlock.Attributes.Add("onchange", "setDirtyText();");
                txtLandPlot.Attributes.Add("onchange", "setDirtyText();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");

                if ((new PAP_HouseholdBLL()).IsResident(Convert.ToInt32(Session["HH_ID"])) == false)
                {
                    // Whole Acreage Acres section is available only for Residents.
                    txtAcreageAcres.Enabled = false;
                    txtAcreageHA.Enabled = false;
                }


                int HHID_ = Convert.ToInt32(Session["HH_ID"]);
                ViewDependents(HHID_);
                upnAcreageVal.Update();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkAcrValuation.Visible = false;
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
                lnkAcrValuation.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
        }

        /// <summary>
        /// Set Attribute to lnkViewDependents
        /// </summary>
        /// <param name="HHID_"></param>
        private void ViewDependents(int HHID_)
        {
             string param = string.Format("OpenPAPDependents({0});", HHID_);

             lnkViewDependents.Attributes.Add("onclick", param);
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
            stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        /// <summary>
        /// Get Pap data From database and set to Labels
        /// </summary>
        private void GetHouseholdDetails()
        {
            int householdID = Convert.ToInt32(Session["HH_ID"]);

            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = objHouseHoldBLL.GetHousaeHoldData(householdID);

            lblDistrict.Text = objHouseHold.District;
            lblCounty.Text = objHouseHold.County;
            lblSubCounty.Text = objHouseHold.SubCounty;
            lblParish.Text = objHouseHold.Parish;
            lblVillage.Text = objHouseHold.Village;
            lblStakeholdDesignation.Text = objHouseHold.Designation;
            
            ddlLocClassification.ClearSelection();
            if (ddlLocClassification.Items.FindByValue(objHouseHold.LocClassification) != null)
                ddlLocClassification.Items.FindByValue(objHouseHold.LocClassification).Selected = true;
 
            PstatusBLL objPstatusBLL = new PstatusBLL();
            PstatusBO objPstatus = objPstatusBLL.GetPstatusById(objHouseHold.PapstatusId);

            if (objPstatus != null)
                lblStatus.Text = objPstatus.PAPDESIGNATION1;
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                ChangeRequestStatusAcrValuation();

                objAcreageValuation = new AffectedAcreageValuationBO();

                objSurveyBLL = new SurveyBLL();
                objAcreageValuation.HouseholdID = Convert.ToInt32(Session["HH_ID"]);
                objAcreageValuation.Landowner = txtLandOwner.Text.Trim();
                objAcreageValuation.Landblock = txtLandBlock.Text.Trim();
                objAcreageValuation.Landplot = txtLandPlot.Text.Trim();
                objAcreageValuation.Proprietorid = Convert.ToInt32(ddlCurrentOperation.SelectedValue);
                objAcreageValuation.Wholeacreageacres = txtAcreageAcres.Text.Trim();
                objAcreageValuation.Rowacres = Convert.ToDecimal(lblAcres.Text);
                objAcreageValuation.Rowlandvalueshare = Convert.ToDecimal(ddlRowLandvalshare.SelectedValue);
                objAcreageValuation.Rowrateperacre = Convert.ToDecimal(txtRatePerAcres.Text.Trim());
                objAcreageValuation.Rowlandvalue = Convert.ToDecimal(lblRowLandVal.Text);
                objAcreageValuation.Wlacres = Convert.ToDecimal(lblWayleavesAcres.Text);
                objAcreageValuation.Wllandvalueshare = Convert.ToDecimal(ddlWayleaveShareLandVal.SelectedValue);
                objAcreageValuation.Dimunitionlevel = Convert.ToDecimal(ddlDimunition.SelectedValue);
                objAcreageValuation.Wlrateperacre = Convert.ToDecimal(txtWayleaveRateperAcres.Text.Trim());
                objAcreageValuation.Wllandvalue = Convert.ToDecimal(lblWayleavelandVal.Text);
                objAcreageValuation.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                objAcreageValuation.LocClassification = ddlLocClassification.SelectedValue;

                message = objSurveyBLL.AddAffectedAcreageValuation(objAcreageValuation);

                if (btnSave.Text == "Save")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "ShowSaveMessage('');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "ShowUpdateMessage('');", true);
                }
                projectFrozen();
                SetUpdateMode(true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Error: " + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Call respective method to do calculation for the selected measeure type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMeasure_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateRightOfWayValue();
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRightWay_TextChanged(object sender, EventArgs e)
        {
            RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateRightOfWayValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRatePerAcres_TextChanged(object sender, EventArgs e)
        {
            RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateRightOfWayValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            txtRatePerAcres.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(txtRatePerAcres.Text.Trim())).ToString();
            txtWayleaveRateperAcres.Text = txtRatePerAcres.Text;
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRowLandvalshare_SelectedIndexChanged(object sender, EventArgs e)
        {
            RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateRightOfWayValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
            //lblSubCounty.Text = ddlRowLandvalshare.SelectedItem.Text.ToString() +"---" + ddlRowLandvalshare.SelectedValue.ToString();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtWayleave_TextChanged(object sender, EventArgs e)
        {
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDimunition_SelectedIndexChanged(object sender, EventArgs e)
        {
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlWayleaveShareLandVal_SelectedIndexChanged(object sender, EventArgs e)
        {
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Call respective method to do calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtWayleaveRateperAcres_TextChanged(object sender, EventArgs e)
        {
            WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateWayLeaveValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            txtWayleaveRateperAcres.Text= UtilBO.CurrencyFormat(Convert.ToDecimal(txtWayleaveRateperAcres.Text.Trim())).ToString();
            txtRatePerAcres.Text = txtWayleaveRateperAcres.Text;
            RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
            CalculateRightOfWayValue();
            TotalUnitConversion(ddlMeasure.SelectedItem.Value);
            upnAcreageVal.Update();
        }

        /// <summary>
        /// Update Acreage HA VALUE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtAcreageAcres_TextChanged(object sender, EventArgs e)
        {
            string str1 = txtAcreageAcres.Text.Trim();
            if (string.IsNullOrEmpty(str1) == false)
            {
                double t = Convert.ToDouble(txtAcreageAcres.Text.Trim());
                double Ha = Math.Round((t / acrehaconvert), 4);
                txtAcreageHA.Text = Ha.ToString();
            }
            else
            {
                txtAcreageHA.Text = "";
            }
            upnAcreageHA.Update();
        }
        #endregion

        #region Method
        /// <summary>
        /// SET DATA TO Dropdown ddlLocClassification
        /// </summary>
        private void BindLocationClassification()
        {
            LocationClassificationList ObjLoc = (new LocationClassificationBLL().GetLOCATIONClassification());
            ddlLocClassification.DataSource = ObjLoc;
            ddlLocClassification.DataTextField = "LOCTNCLASFCTNNAME";
            ddlLocClassification.DataValueField = "LOCTNCODE";
            ddlLocClassification.DataBind();
        }

        /// <summary>
        /// get Data and set data to Labels
        /// </summary>
        private void BindLandType()
        {
            LandInfoBLL objLandInfoBLL = new LandInfoBLL();
            PublicLandInfoBO objLandInfo = objLandInfoBLL.GetLandInfo(Convert.ToInt32(Session["HH_ID"]));

            if (objLandInfo != null)
            {
                objMasterBLL = new MasterBLL();

                TenureLandList TenureLands = objMasterBLL.LoadTenureLand();
                foreach (TenureLandBO objTenureLand in TenureLands)
                {
                    if (objTenureLand.Lnd_TenureId == objLandInfo.LND_TENUREID)
                    {
                        lblLandType.Text = objTenureLand.Lnd_Tenure;
                        break;
                    }
                }
            }
            else
            {
                LandInfoPrivateBLL objLFPrivateBLL = new LandInfoPrivateBLL();
                PrivateLandInfoBO objLandInfoPriv = objLFPrivateBLL.GetLandInfoPriv(Convert.ToInt32(Session["HH_ID"]));

                if (objLandInfoPriv != null)
                {
                    objMasterBLL = new MasterBLL();

                    TenureLandList TenureLands = objMasterBLL.LoadTenureLand();
                    foreach (TenureLandBO objTenureLand in TenureLands)
                    {
                        if (objTenureLand.Lnd_TenureId == objLandInfoPriv.Lnd_TENUREIDPriv)
                        {
                            lblLandType.Text = objTenureLand.Lnd_Tenure;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// SET DATA TO Dropdown ddlCurrentOperation
        /// </summary>
        private void BindProprietor()
        {
            objMasterBLL = new MasterBLL();
            objProprietorList = objMasterBLL.LoadProprietorData();
            if (objProprietorList.Count > 0)
            {
                ddlCurrentOperation.DataSource = objProprietorList;
                ddlCurrentOperation.DataTextField = "ProprietorName";
                ddlCurrentOperation.DataValueField = "ProprietorID";
                ddlCurrentOperation.DataBind();
            }
        }

        /// <summary>
        /// SET DATA TO Dropdown ddlMeasure
        /// </summary>
        private void BindUnitRightWay()
        {
            objUnitBLL = new UnitBLL();
            objUnitList = objUnitBLL.GetUnit();
            ddlMeasure.DataSource = objUnitList;
            ddlMeasure.DataTextField = "UnitName";
            ddlMeasure.DataValueField = "UnitID";
            ddlMeasure.DataBind();
        }
        /// <summary>
        /// Set update mode to buttons
        /// </summary>
        /// <param name="updateMode"></param>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClear.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
            }
        }
        /// <summary>
        /// Calculate Right Of Way Value and Update it to respective fields
        /// </summary>
        protected void CalculateRightOfWayValue()
        {
            decimal rateperacres = 0;
            decimal rightOfWay = 0;
            decimal sharevalue = 0;
            string message = string.Empty;
            string AlertMessage = string.Empty;
            if (txtRatePerAcres.Text.Trim() != "")
            {
                rateperacres = Convert.ToDecimal(txtRatePerAcres.Text.Trim());
            }
            //else
            //{
            //    message += "Please Input the rate per acres value\\n";
            //}

            if (lblAcres.Text.Trim() != "")
            {
                rightOfWay = Convert.ToDecimal(lblAcres.Text.Trim());
            }
            ////if (txtRightWay.Text.Trim() != "")
            ////{
            ////    rightOfWay = Convert.ToDecimal(txtRightWay.Text.Trim());
            ////}
            ////else
            ////{
            ////    lblAcres.Text = rightOfWay.ToString();
            ////    lblHA.Text = "0";
            ////    lblSqmtrs.Text = "0";
            ////    //message += "Please Input the right of way value\\n";
            ////}

            if (ddlRowLandvalshare.SelectedIndex != 0)
            {
                sharevalue = Convert.ToDecimal(ddlRowLandvalshare.SelectedValue) / 100;
            }
            //else
            //{
            //    message += "Please Select the Share value\\n";
            //}

            //if (message != "")
            //{
            //    AlertMessage = "alert('" + message + "');";
            //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", AlertMessage, true);
            //}

            //decimal rowlandvalue = Math.Ceiling(rateperacres * rightOfWay * sharevalue);//changes done by ramu as per Request of UETCL team

            decimal rowlandvalue = (rateperacres * rightOfWay * sharevalue);

            lblRowLandVal.Text = UtilBO.CurrencyFormat(rowlandvalue); //rowlandvalue.ToString();

            decimal wayleave = 0;
            if (txtWayleave.Text.Trim() != "")
                wayleave = Convert.ToDecimal(txtWayleave.Text.Trim());
            decimal totlandval = 0;
            totlandval = wayleave + rightOfWay;

            string str1 = txtRightWay.Text.Trim();
            double t = 0, t1 = 0;
            if (string.IsNullOrEmpty(str1) == false)
            {
                t = Convert.ToDouble(txtRightWay.Text.Trim());
            }
            string str2 = txtWayleave.Text.Trim();
            if (string.IsNullOrEmpty(str2) == false)
            {
                t1 = Convert.ToDouble(txtWayleave.Text.Trim());
            }
            lblTotal.Text = (t + t1).ToString();
        }

        /// <summary>
        /// Calculate WayLeave Value and Update it to respective fields
        /// </summary>
        protected void CalculateWayLeaveValue()
        {
            decimal wayleave = 0;
            decimal wayleaverateperAcres = 0;
            decimal dimunition = 0;
            decimal wayleavesharevalue = 0;
            string message1 = string.Empty;
            string AlertMessage = string.Empty;
            if (lblWayleavesAcres.Text.Trim() != "")
            {
                wayleave = Convert.ToDecimal(lblWayleavesAcres.Text.Trim());
            }
           
            //if (txtWayleave.Text.Trim() != "")
            //{
            //    wayleave = Convert.ToDecimal(txtWayleave.Text.Trim());
            //}
            //else
            //{
            //    lblWayleavesAcres.Text = wayleave.ToString();
            //    lblWayleavesHA.Text = "0";
            //    lblWayleaveSqmtrs.Text = "0";
            //    //message1 += "Please Input the wayleave value";
            //}


            if (txtWayleaveRateperAcres.Text.Trim() != "")
            {
                wayleaverateperAcres = Convert.ToDecimal(txtWayleaveRateperAcres.Text.Trim());
            }
            //else
            //{
            //    message1 += "Please Input the wayleave rate per acres value\\n";
            //}

            if (ddlDimunition.SelectedIndex != 0)
            {
                dimunition = Convert.ToDecimal(ddlDimunition.SelectedValue) / 100;
            }
            //else
            //{
            //    message1 += "Please select the Dimunition value\\n";
            //}

            if (ddlWayleaveShareLandVal.SelectedIndex != 0)
            {
                wayleavesharevalue = Convert.ToDecimal(ddlWayleaveShareLandVal.SelectedValue) / 100;
            }
            //else
            //{
            //    message1 += "Please select the wayleave share value\\n";
            //}

            //if (message1 != "")
            //{
            //    AlertMessage = "alert('" + message1 + "');";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", AlertMessage, true);
            //}

            //decimal wayleavevalue = Math.Ceiling(wayleave * wayleaverateperAcres * dimunition * wayleavesharevalue);//changes done by ramu as per Request of UETCL team
            decimal wayleavevalue = (wayleave * wayleaverateperAcres * dimunition * wayleavesharevalue);

            lblWayleavelandVal.Text = UtilBO.CurrencyFormat(wayleavevalue);

            decimal rightOfWay = 0;
            if (txtRightWay.Text.Trim() != "")
                rightOfWay = Convert.ToDecimal(txtRightWay.Text.Trim());
            decimal totlandval = 0;
            totlandval = wayleave + rightOfWay;
            //lblTotal.Text = totlandval.ToString();

            string str1 = txtRightWay.Text.Trim();
            double t = 0, t1 = 0;
            if (string.IsNullOrEmpty(str1) == false)
            {
                t = Convert.ToDouble(txtRightWay.Text.Trim());
            }
            string str2 = txtWayleave.Text.Trim();
            if (string.IsNullOrEmpty(str2) == false)
            {
                t1 = Convert.ToDouble(txtWayleave.Text.Trim());
            }

            lblTotal.Text = (t + t1).ToString();
        }

        /// <summary>
        /// Get Affected Acreage Valuation Data
        /// </summary>
        private void GetAffectedAcreageValuation()
        {
            objSurveyBLL = new SurveyBLL();
            objAcreageValuation = objSurveyBLL.GetAffectedAcreageValuation(Convert.ToInt32(Session["HH_ID"]));

            if (objAcreageValuation != null)
            {
                txtLandOwner.Text = objAcreageValuation.Landowner;
                txtLandBlock.Text = objAcreageValuation.Landblock;
                txtLandPlot.Text = objAcreageValuation.Landplot;
                //BindProprietor();
                ddlCurrentOperation.ClearSelection();
                if (ddlCurrentOperation.Items.FindByValue(objAcreageValuation.Proprietorid.ToString()) != null)
                    ddlCurrentOperation.Items.FindByValue(objAcreageValuation.Proprietorid.ToString()).Selected = true;
                txtAcreageAcres.Text = objAcreageValuation.Wholeacreageacres;

                txtRightWay.Text = Convert.ToString(objAcreageValuation.Rowacres);
                lblAcres.Text = Convert.ToString(objAcreageValuation.Rowacres);
                if (ddlRowLandvalshare.Items.FindByValue(objAcreageValuation.Rowlandvalueshare.ToString()) != null)
                    ddlRowLandvalshare.Items.FindByValue(objAcreageValuation.Rowlandvalueshare.ToString()).Selected = true;
                txtRatePerAcres.Text = UtilBO.CurrencyFormat(objAcreageValuation.Rowrateperacre);
                lblRowLandVal.Text = UtilBO.CurrencyFormat(objAcreageValuation.Rowlandvalue);//Convert.ToString(objAcreageValuation.Rowlandvalue);
                txtWayleave.Text = Convert.ToString(objAcreageValuation.Wlacres);
                lblWayleavesAcres.Text = Convert.ToString(objAcreageValuation.Wlacres);
                if (ddlDimunition.Items.FindByValue(objAcreageValuation.Dimunitionlevel.ToString()) != null)
                    ddlDimunition.Items.FindByValue(objAcreageValuation.Dimunitionlevel.ToString()).Selected = true;
                if (ddlWayleaveShareLandVal.Items.FindByValue(objAcreageValuation.Wllandvalueshare.ToString()) != null)
                    ddlWayleaveShareLandVal.Items.FindByValue(objAcreageValuation.Wllandvalueshare.ToString()).Selected = true;
                txtWayleaveRateperAcres.Text = UtilBO.CurrencyFormat(objAcreageValuation.Wlrateperacre); 

                lblWayleavelandVal.Text = UtilBO.CurrencyFormat(objAcreageValuation.Wllandvalue);// Convert.ToString(objAcreageValuation.Wllandvalue);

                if (txtRightWay.Text.Trim() != "" && txtWayleave.Text.Trim() != "")
                    lblTotal.Text = Math.Round((Convert.ToDecimal(txtRightWay.Text.Trim()) + Convert.ToDecimal(txtWayleave.Text.Trim())), 2).ToString("N2");
                
                string str1 = txtAcreageAcres.Text.Trim();
                if (string.IsNullOrEmpty(str1) == false)
                {
                    double t = Convert.ToDouble(txtAcreageAcres.Text.Trim());
                    double Ha = Math.Round((t / acrehaconvert), 4);
                    txtAcreageHA.Text = Ha.ToString();
                }
                upnAcreageHA.Update();

                RightOfWayUnitConversion(ddlMeasure.SelectedItem.Value);
                WayLeaveUnitConversion(ddlMeasure.SelectedItem.Value);
                TotalUnitConversion(ddlMeasure.SelectedItem.Value);
                if(txtLandOwner.Text.Trim()=="")
                    SetUpdateMode(false);
                else
                    SetUpdateMode(true);
            }
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearDetails()
        {
           
            txtAcreageAcres.Text = "";
            txtAcreageHA.Text = "";
            upnAcreageHA.Update();
            txtLandOwner.Text = "";
            txtLandBlock.Text = "";
            txtLandPlot.Text = "";
            ddlCurrentOperation.SelectedIndex = 0;
            txtRightWay.Text = "";
            lblAcres.Text = "";
            lblHA.Text = "";
            lblSqmtrs.Text = "";
            ddlRowLandvalshare.SelectedIndex = 0;
            txtRatePerAcres.Text = "";
            lblRowLandVal.Text = "";
            txtWayleave.Text = "";
            lblWayleavesAcres.Text = "";
            lblWayleavesHA.Text = "";
            lblWayleaveSqmtrs.Text = "";
            ddlDimunition.SelectedIndex = 0;
            ddlWayleaveShareLandVal.SelectedIndex = 0;
            txtWayleaveRateperAcres.Text = "";
            lblWayleavelandVal.Text = "";
            lblTotal.Text = "";
            lblTotAcres.Text = "";
            lblTotHA.Text = "";
            lblTotSqmtrs.Text = "";
            lblTotlandVal.Text = "";
            ddlMeasure.ClearSelection();
            ddlLocClassification.ClearSelection();
            ddlLocClassification.SelectedIndex = 0;
        }

        #endregion
        /// <summary>
        /// Check Project is Frozen or Not
        /// </summary>
        #region Frozen / Approval / Decline / Pending
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string Frozen = Session["FROZEN"].ToString();
                if (Frozen == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    lnkAcrValuation.Visible = true;
                    checkApprovalExitOrNot();
                }
                else
                {
                    lnkAcrValuation.Visible = false;
                }
            }
        }
        /// <summary>
        /// Check Approver Exist or not for Change request
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusAcrValuation.Text = "";
            StatusAcrValuation.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-AV");
                lnkAcrValuation.Attributes.Add("onclick", paramChangeRequest);
                lnkAcrValuation.Visible = true;
            }
            else
            {
                lnkAcrValuation.Visible = false;
            }
            #endregion
            getApprrequtStatusAcrValuation();

        }
        /// <summary>
        /// Check Change Request Status for Acr Valuation
        /// </summary>
        public void ChangeRequestStatusAcrValuation()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-AV";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// get Approval request Status Acr Valuation
        /// </summary>
        public void getApprrequtStatusAcrValuation()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-AV";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkAcrValuation.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusAcrValuation.Visible = true;
                    StatusAcrValuation.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkAcrValuation.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusAcrValuation.Visible = false;
                    StatusAcrValuation.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkAcrValuation.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusAcrValuation.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        // Updated code
        /// <summary>
        /// Right Of Way Unit Conversion
        /// </summary>
        /// <param name="unitOfMeasure"></param>
        protected void RightOfWayUnitConversion(string unitOfMeasure)
        {
            string str1 = txtRightWay.Text.Trim();
            if (string.IsNullOrEmpty(str1) == false)
            {
                double t = Convert.ToDouble(txtRightWay.Text.Trim());

                if (unitOfMeasure == "HA")
                {
                    //lblHA.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()),2).ToString();
                    //double acre = Math.Round((t * acrehaconvert), 2);
                    //lblAcres.Text = acre.ToString();
                    //double sqmtr1 = Math.Round((t / hasqmtreconvert), 2);
                    //lblSqmtrs.Text = sqmtr1.ToString();

                    lblHA.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()), 4).ToString();
                    double acre = Math.Round((t * acrehaconvert), 11);
                    lblAcres.Text = acre.ToString();
                    double sqmtr1 = Math.Round((t / hasqmtreconvert), 4);
                    lblSqmtrs.Text = sqmtr1.ToString(); //Math.Round(sqmtr1, 4).ToString();
                }
                else if (unitOfMeasure == "Acre")
                {
                    //lblAcres.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()), 2).ToString();
                    //double Ha = Math.Round((t / acrehaconvert), 2);
                    //lblHA.Text = Ha.ToString();
                    //double sqmtr = Math.Round((t / acresqmetreconvert), 2);
                    //lblSqmtrs.Text = sqmtr.ToString();

                    lblAcres.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()), 11).ToString();
                    double Ha = Math.Round((t / acrehaconvert), 4);
                    lblHA.Text = Ha.ToString();
                    double sqmtr = Math.Round((t / acresqmetreconvert), 4);
                    lblSqmtrs.Text = sqmtr.ToString();// Math.Round(sqmtr, 4).ToString(); 
                }
                else if (unitOfMeasure == "Sq Metre")
                {
                    //lblSqmtrs.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()), 2).ToString();
                    //double acr1 = Math.Round((t * acresqmetreconvert), 2);
                    //lblAcres.Text = acr1.ToString();
                    //double ha1 = Math.Round((t * hasqmtreconvert), 2);
                    //lblHA.Text = ha1.ToString();

                    lblSqmtrs.Text = Math.Round(Convert.ToDecimal(txtRightWay.Text.Trim()), 4).ToString();
                    double acr1 = Math.Round((t * acresqmetreconvert), 11);
                    lblAcres.Text = acr1.ToString();
                    double ha1 = Math.Round((t * hasqmtreconvert), 4);
                    lblHA.Text = ha1.ToString();
                }
            }
        }

        /// <summary>
        /// Way Leave Unit Conversion
        /// </summary>
        /// <param name="unitOfMeasure"></param>
        protected void WayLeaveUnitConversion(string unitOfMeasure)
        {
            string str3 = txtWayleave.Text.Trim();
            if (string.IsNullOrEmpty(str3) == false)
            {
                double twl = Convert.ToDouble(txtWayleave.Text.Trim());

                if (unitOfMeasure == "HA")
                {
                    //lblWayleavesHA.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 2).ToString();
                    //double acrewl = Math.Round((twl * acrehaconvert), 2);
                    //lblWayleavesAcres.Text = acrewl.ToString();
                    //double sqmtrwl = Math.Round((twl / hasqmtreconvert), 2);
                    //lblWayleaveSqmtrs.Text = sqmtrwl.ToString();

                    lblWayleavesHA.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 4).ToString();
                    double acrewl = Math.Round((twl * acrehaconvert), 11);
                    lblWayleavesAcres.Text = acrewl.ToString();
                    double sqmtrwl = Math.Round((twl / hasqmtreconvert), 4);
                    lblWayleaveSqmtrs.Text = Math.Round(sqmtrwl, 4).ToString();//sqmtrwl.ToString();
                }
                else if (unitOfMeasure == "Acre")
                {
                    //lblWayleavesAcres.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 2).ToString();
                    //double Hawl = Math.Round((twl / acrehaconvert), 2);
                    //lblWayleavesHA.Text = Hawl.ToString();
                    //double sqmtrwl = Math.Round((twl / acresqmetreconvert), 2);
                    //lblWayleaveSqmtrs.Text = sqmtrwl.ToString();

                    lblWayleavesAcres.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 11).ToString();
                    double Hawl = Math.Round((twl / acrehaconvert), 4);
                    lblWayleavesHA.Text = Hawl.ToString();
                    double sqmtrwl = Math.Round((twl / acresqmetreconvert), 4);
                    lblWayleaveSqmtrs.Text = Math.Round(sqmtrwl, 4).ToString(); //sqmtrwl.ToString();
                }
                else if (unitOfMeasure == "Sq Metre")
                {
                    //lblWayleaveSqmtrs.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 2).ToString();
                    //double acrwl = Math.Round((twl * acresqmetreconvert), 2);
                    //lblWayleavesAcres.Text = acrwl.ToString();
                    //double hawl = Math.Round((twl * hasqmtreconvert), 2);
                    //lblWayleavesHA.Text = hawl.ToString();

                    lblWayleaveSqmtrs.Text = Math.Round(Convert.ToDecimal(txtWayleave.Text.Trim()), 4).ToString();
                    double acrwl = Math.Round((twl * acresqmetreconvert), 11);
                    lblWayleavesAcres.Text = acrwl.ToString();
                    double hawl = Math.Round((twl * hasqmtreconvert), 4);
                    lblWayleavesHA.Text = hawl.ToString();
                }
            }
        }

        /// <summary>
        /// Total Unit Conversion
        /// </summary>
        /// <param name="unitOfMeasure"></param>
        protected void TotalUnitConversion(string unitOfMeasure)
        {
            double ttl = 0;
            double rightOfWayAcres = 0;
            double wayleaveAcres = 0;
            double rightOfWayHA = 0;
            double wayleaveHA = 0;
            double rightOfWaySQM = 0;
            double wayleaveSQM = 0;
            double rightOfWayLandValue = 0;
            double wayleaveLandValue = 0;

            if (lblAcres.Text.Trim() != "") rightOfWayAcres = Convert.ToDouble(lblAcres.Text.Trim());
            if (lblWayleavesAcres.Text.Trim() != "") wayleaveAcres = Convert.ToDouble(lblWayleavesAcres.Text.Trim());

            if (lblHA.Text.Trim() != "") rightOfWayHA = Convert.ToDouble(lblHA.Text.Trim());
            if (lblWayleavesHA.Text.Trim() != "") wayleaveHA = Convert.ToDouble(lblWayleavesHA.Text.Trim());

            if (lblSqmtrs.Text.Trim() != "") rightOfWaySQM = Convert.ToDouble(lblSqmtrs.Text.Trim());
            if (lblWayleaveSqmtrs.Text.Trim() != "") wayleaveSQM = Convert.ToDouble(lblWayleaveSqmtrs.Text.Trim());

            if (lblRowLandVal.Text.Trim() != "") rightOfWayLandValue = Convert.ToDouble(lblRowLandVal.Text.Trim());
            if (lblWayleavelandVal.Text.Trim() != "") wayleaveLandValue = Convert.ToDouble(lblWayleavelandVal.Text.Trim());

            if (lblTotal.Text.Trim() != "")
                ttl = Convert.ToDouble(lblTotal.Text.Trim());

            if (unitOfMeasure == "HA")
            {
                //lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA),2).ToString();
                //lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 2).ToString();
                //lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 2).ToString();

                lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA), 4).ToString();
                lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 11).ToString();
                lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 4).ToString();
            }
            else if (unitOfMeasure == "Acre")
            {
                //lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA), 2).ToString();
                //lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 2).ToString();
                //lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 2).ToString();

                lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA), 4).ToString();
                lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 11).ToString();
                lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 4).ToString();

            }
            else if (unitOfMeasure == "Sq Metre")
            {
                //lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA), 2).ToString();
                //lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 2).ToString();
                //lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 2).ToString();

                lblTotHA.Text = Math.Round((rightOfWayHA + wayleaveHA), 4).ToString();
                lblTotAcres.Text = Math.Round((rightOfWayAcres + wayleaveAcres), 11).ToString();
                lblTotSqmtrs.Text = Math.Round((rightOfWaySQM + wayleaveSQM), 4).ToString();
            }
            double result = rightOfWayLandValue + wayleaveLandValue;
            lblTotlandVal.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(result));//(rightOfWayLandValue + wayleaveLandValue).ToString();
            
            //txtAcreageAcres.Text = lblTotAcres.Text;
            //string str1 = txtAcreageAcres.Text.Trim();
            //if (string.IsNullOrEmpty(str1) == false)
            //{
            //    double t = Convert.ToDouble(txtAcreageAcres.Text.Trim());
            //    double Ha = Math.Round((t / acrehaconvert), 2);
            //    txtAcreageHA.Text = Ha.ToString();
            //}
            //txtAcreageHA.Text = lblTotHA.Text;
        }

        /// <summary>
        /// Fill DropDowns ddlRowLandvalshare,ddlDimunition, and ddlWayleaveShareLandVal
        /// </summary>
        private void AddShareOfLandValue()
        {
            ddlRowLandvalshare.Items.Clear();
            ddlDimunition.Items.Clear();
            ddlWayleaveShareLandVal.Items.Clear();

            ListItem itemlst = new ListItem("Text", "Value");
            List<ListItem> lstShareOfLandValue = new List<ListItem>();
            for (int i = 1; i <= 100; i++)
            {
                //lstShareOfLandValue.Add(new ListItem(i.ToString()+"%",i.ToString()));
                ddlRowLandvalshare.Items.Add(new ListItem(i.ToString() + "%", i.ToString()));
                ddlDimunition.Items.Add(new ListItem(i.ToString() + "%", i.ToString()));
                ddlWayleaveShareLandVal.Items.Add(new ListItem(i.ToString() + "%", i.ToString()));
            }
            //ddlRowLandvalshare.DataSource = lstShareOfLandValue;
            ////ddlRowLandvalshare.DataTextField = lstShareOfLandValue[0].Text;
            ////ddlRowLandvalshare.DataValueField = lstShareOfLandValue[0].Value;
            //ddlRowLandvalshare.DataBind();
        }
    }
}