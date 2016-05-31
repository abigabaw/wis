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
    public partial class LandInfoRespondents : System.Web.UI.Page
    {
        #region PageLoad
        /// <summary>
        /// Set Page header,Call BindTenureType() to get the data from the database and bind it to the dropdown ddlTenureType
        /// call BindUse() to get the data from the database and bind it to the dropdown DrpUse
        /// call BindDistrict() to get the District names from the database and bind it to the dropdown DrpDistrict 
        /// call getMemberClaim() to get the and set the memberClaim details to the textbox's
        /// to set status of the link buttons lnkLandHoldings,lnkMCE
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.OtherLandHoldings;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.OtherLandHoldings;
            ViewMasterCopy2.HighlightMenu = ViewMasterCopy.MenuValue.MemberClaimsandEasements;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Other Land Holdings";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ChkHoldings.Attributes.Add("onclick", string.Format("EnableHolding(this,'{0}');", txtenterholdings.ClientID));
                ChkEasements.Attributes.Add("onclick", string.Format("EnableEasement(this,'{0}');", txtentereaseemnts.ClientID));
                ViewState["LND_HOLDINGID"] = 0;
                
                BindGrid(false, false);
                BindTenureType();
                BindType();
                BindUse();
                BindDistrict();
                getMemberClaim();
                txttotal.Attributes.Add("onblur", "setDirtyText();");
                btn_SaveRes.Attributes.Add("onclick", "isDirty = 0;");
                btn_ClearRes.Attributes.Add("onclick", "isDirty = 0;");
                txttotal.Attributes.Add("onchange", "CheckDecimal(e, src);");
                //ChkHoldings.Attributes.Add("onload", string.Format("EnableHolding(this,'{0}');", txtenterholdings.ClientID));
                //ChkEasements.Attributes.Add("onload", string.Format("EnableEasement(this,'{0}');", txtentereaseemnts.ClientID));
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkLandHoldings.Visible = false;
                    lnkMCE.Visible = false;
                    btn_SaveRes.Visible = false;
                    btn_ClearRes.Visible = false;
                    btn_Savemambers.Visible = false;
                    btnClearMembers.Visible = false;
                    grdLandInfoRespondents.Columns[12].Visible = false;
                    grdLandInfoRespondents.Columns[13].Visible = false;
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
                lnkLandHoldings.Visible = false;
                lnkMCE.Visible = false;
                btn_SaveRes.Visible = false;
                btn_ClearRes.Visible = false;
                btn_Savemambers.Visible = false;
                btnClearMembers.Visible = false;
                grdLandInfoRespondents.Columns[12].Visible = false;
                grdLandInfoRespondents.Columns[13].Visible = false;
            }
        }
        #endregion

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btn_SaveRes.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region Frozen / Approval / Decline / Pending
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btn_ClearRes.Visible = false;
                    btn_SaveRes.Visible = false;

                    btn_Savemambers.Visible = false;
                    btnClearMembers.Visible = false;
                    grdLandInfoRespondents.Columns[12].Visible = false;
                    grdLandInfoRespondents.Columns[13].Visible = false;

                    checkApprovalExitOrNot();
                    getApprrequtStatusLandHoldings();
                    getApprrequtStatusMCE();
                }
            }
        }
        /// <summary>
        /// to check the Approval Exit or Not
        /// </summary>

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusLandHoldings.Text = "";
            StatusLandHoldings.Visible = false;

            StatusMCE.Text = "";
            StatusMCE.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHLHH");
                lnkLandHoldings.Attributes.Add("onclick", paramChangeRequest);
                string paramChangeRequest1 = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHMCE");
                lnkMCE.Attributes.Add("onclick", paramChangeRequest1);
                lnkLandHoldings.Visible = true;
                lnkMCE.Visible = true;
            }
            else
            {
                lnkLandHoldings.Visible = false;
                lnkMCE.Visible = false;
            }
            #endregion
            getApprrequtStatusLandHoldings();
            getApprrequtStatusMCE();

        }
        /// <summary>
        /// to check the status of the Change Request landHoldings
        /// </summary>
        public void ChangeRequestStatusLandHoldings()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHLHH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the Approvar request Sattus Land Holdings
        /// </summary>
        public void getApprrequtStatusLandHoldings()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHLHH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkLandHoldings.Visible = false;
                    btn_SaveRes.Visible = false;
                    btn_ClearRes.Visible = false;
                    StatusLandHoldings.Visible = true;
                    StatusLandHoldings.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkLandHoldings.Visible = true;
                    btn_SaveRes.Visible = false;
                    btn_ClearRes.Visible = false;
                    StatusLandHoldings.Visible = false;
                    StatusLandHoldings.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkLandHoldings.Visible = false;
                    btn_SaveRes.Visible = true;
                    btn_ClearRes.Visible = true;
                    grdLandInfoRespondents.Columns[12].Visible = true;
                    grdLandInfoRespondents.Columns[13].Visible = true;
                    StatusLandHoldings.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        /// <summary>
        /// to get the Change Request Status of MCE
        /// </summary>
        public void ChangeRequestStatusMCE()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHMCE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the Approvar Request Status of MCE
        /// </summary>
        public void getApprrequtStatusMCE()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHMCE";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkMCE.Visible = false;
                    btn_Savemambers.Visible = false;
                    btnClearMembers.Visible = false;
                    StatusMCE.Visible = true;
                    StatusMCE.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkMCE.Visible = true;
                    btn_Savemambers.Visible = false;
                    btnClearMembers.Visible = false;
                    StatusMCE.Visible = false;
                    StatusMCE.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkMCE.Visible = false;
                    btn_Savemambers.Visible = true;
                    btnClearMembers.Visible = true;
                    StatusMCE.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        #region Bind Methods
        private void BindType()
        {
            TypeBLL objTypeBLL = new TypeBLL();
            DrpType.DataSource = objTypeBLL.GetLandType();
            DrpType.DataTextField = "LANDTYPE";
            DrpType.DataValueField = "LND_TYPEID";
            DrpType.DataBind();
            //DrpType.Items.Insert(0, "--Select--");
        }
        /// <summary>
        /// to get the data from the database and bind it to the ddlTenureType dropdown
        /// </summary>
        private void BindTenureType()
        {
            TenureStructureList objTenureStructureList;
            TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
            objTenureStructureList = objTenureStructureBLL.GetTenureStructures("");
            ddlTenureType.DataSource = objTenureStructureList;
            ddlTenureType.DataTextField = "STR_TENURE";
            ddlTenureType.DataValueField = "STR_TENUREID";
            ddlTenureType.DataBind();
        }
        /// <summary>
        ///  to get the data from the database and bind it to the DrpUse dropdown
        /// </summary>
        private void BindUse()
        {
            LandUseBLL objUseBLL = new LandUseBLL();
            DrpUse.DataSource = objUseBLL.GetLandUse();
            DrpUse.DataTextField = "LANDUSE";
            DrpUse.DataValueField = "LND_USEID";
            DrpUse.DataBind();
        }
        /// <summary>
        /// to get the data from the database and bind it to the DrpDistrict dropdown
        /// </summary>
        private void BindDistrict()
        {
            DrpDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            DrpDistrict.DataTextField = "DistrictName";
            DrpDistrict.DataValueField = "DistrictID";
            DrpDistrict.DataBind();
            //DrpDistrict.SelectedIndex = 0;
        }
        /// <summary>
        /// to get the data from the database and bind it to the DrpCounty dropdown
        /// </summary>
        /// <param name="districtID"></param>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(DrpCounty.Items[0].Text, DrpCounty.Items[0].Value);
            DrpCounty.Items.Clear();

            if (districtID != "0")
            {
                DrpCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                DrpCounty.DataTextField = "CountyName";
                DrpCounty.DataValueField = "CountyID";
                DrpCounty.DataBind();
            }

            DrpCounty.Items.Insert(0, firstListItem);
            DrpCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// to get the data from the database and bind it to the DrpSubCounty dropdown
        /// </summary>
        /// <param name="countyID"></param>

        private void BindSubCounty(string countyID)
        {
            ListItem firstListItem = new ListItem(DrpSubCounty.Items[0].Text, DrpSubCounty.Items[0].Value);
            DrpSubCounty.Items.Clear();

            if (countyID != "0")
            {
                DrpSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                DrpSubCounty.DataTextField = "SubCountyName";
                DrpSubCounty.DataValueField = "SubCountyID";
                DrpSubCounty.DataBind();
            }
            DrpSubCounty.Items.Insert(0, firstListItem);
            DrpSubCounty.SelectedIndex = 0;
        }
        /// <summary>
        /// to get the data from the database and bind it to the DrpVillage dropdown
        /// </summary>
        /// <param name="subCounty"></param>
        private void BindVillages(string subCounty)
        {
            ListItem firstListItem = new ListItem(DrpVillage.Items[0].Text, DrpVillage.Items[0].Value);
            DrpVillage.Items.Clear();

            if (subCounty != "0")
            {
                DrpVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                DrpVillage.DataTextField = "VillageName";
                DrpVillage.DataValueField = "VillageID";
                DrpVillage.DataBind();
            }

            DrpVillage.Items.Insert(0, firstListItem);
            DrpVillage.SelectedIndex = 0;
        }
        #endregion

        #region Grid Methods/Events
        /// <summary>
        /// to bind the gridView
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            LandInfoRespondents objLIRGF = new LandInfoRespondents();
            LandInfoRespondentsBLL objLIRBLL = new LandInfoRespondentsBLL();
            if (Session["HH_ID"] != null)
            {
                grdLandInfoRespondents.DataSource = objLIRBLL.GetLandInfoRespondents(Convert.ToInt32(Session["HH_ID"]));
                grdLandInfoRespondents.DataBind();
            }

        }
        /// <summary>
        /// to Edit And Delete Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdLandInfoRespondents_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {

                ViewState["LND_HOLDINGID"] = e.CommandArgument;
                getLandInfoRespondant();

                btn_SaveRes.Text = "Update";
                btn_ClearRes.Text = "Cancel";
                //SetUpdateMode(true);
                BindGrid(true, true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                int landholdID = Convert.ToInt32(e.CommandArgument);
                LandInfoRespondentsBLL objLIRBLL = new LandInfoRespondentsBLL();
                objLIRBLL.DeleteLandInfoRespondents(landholdID);
                BindGrid(false, true);
                ClearLandHolding();
                btn_SaveRes.Text = "Save";
                btn_ClearRes.Text = "Clear";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }
        /// <summary>
        /// to get the  data from the database on change in the index of the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLandInfoRespondents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLandInfoRespondents.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
        }
        #endregion

        #region Save Button(s)
        /// <summary>
        /// To save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_SaveRes_Click(object sender, EventArgs e)
        {
            ChangeRequestStatusLandHoldings();

            LandInfoRespondentsBO objLIR = new LandInfoRespondentsBO();
            objLIR.HOLDINGID = int.Parse(ViewState["LND_HOLDINGID"].ToString());
            objLIR.HID = int.Parse(Session["HH_ID"].ToString());
            objLIR.updatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (DrpType.SelectedValue != "0")
                objLIR.LND_TYPEID = Convert.ToInt32(DrpType.SelectedValue);
            else
                objLIR.LND_TYPEID = 0;//DBNull

            if (DrpUse.SelectedValue != "0")
                objLIR.LND_USEID = Convert.ToInt32(DrpUse.SelectedValue);
            else
                objLIR.LND_USEID = 0;//DBNull

            if (DrpDistrict.SelectedValue != "0")
                objLIR.DISTRICT = DrpDistrict.SelectedItem.Text;
            else
                objLIR.DISTRICT = string.Empty;

            if (DrpCounty.SelectedValue != "0")
                objLIR.COUNTY = DrpCounty.SelectedItem.Text;
            else
                objLIR.COUNTY = string.Empty;

            if (DrpSubCounty.SelectedValue != "0")
                objLIR.SUBCOUNTY = DrpSubCounty.SelectedItem.Text;
            else
                objLIR.SUBCOUNTY = string.Empty;

            if (DrpVillage.SelectedValue != "0")
                objLIR.VILLAGE = DrpVillage.SelectedItem.Text;
            else
                objLIR.VILLAGE = string.Empty;

            if (ddlTenureType.SelectedValue != "0")
                objLIR.TenureId = Convert.ToInt32(ddlTenureType.SelectedValue.ToString());
            else
                objLIR.TenureId = 0;

            if (!string.IsNullOrEmpty(txttenure.Text))
                objLIR.TENURE = txttenure.Text.Trim();
            else
                objLIR.TENURE = string.Empty;

            if (!string.IsNullOrEmpty(txttotal.Text))
                objLIR.TOTALSIZE = Convert.ToDecimal(txttotal.Text.Trim());
            else
                objLIR.TOTALSIZE = -1;

            if (ChkPrimary.Checked == true)
                objLIR.ISPRIMARYRESIDENCE = "Yes";
            else if (ChkPrimary.Checked == false)
                objLIR.ISPRIMARYRESIDENCE = "No";

            if (ChkAffected.Checked == true)
                objLIR.ISAFFECTED = "Yes";
            else if (ChkAffected.Checked == false)
                objLIR.ISAFFECTED = "No";


            LandInfoRespondentsBLL objLIRBLL = new LandInfoRespondentsBLL();
            if (objLIR.HOLDINGID == 0)
            {
                objLIRBLL.AddLandInfoRespondents(objLIR);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
            }
            else
            {
                objLIRBLL.UpdateLandInfoRespondents(objLIR);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Data updated successfully');", true);
                btn_SaveRes.Text = "Save";
                ViewState["LND_HOLDINGID"] = 0;
            }
            projectFrozen();
            ClearLandHolding();
           
            BindGrid(true, false);
        }
        /// <summary>
        /// to save the data members to the database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btn_Savemambers_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;
            MemberClaimsBO MCBO = new MemberClaimsBO();
            //MCBO.HOLDINGID = int.Parse(ViewState["LND_HOLDINGID"].ToString());

            if (ChkHoldings.Checked == true)
            {
                MCBO.HASCLAIM = "Yes";
                txtenterholdings.Enabled = true;
            }
            else if (ChkHoldings.Checked == false)
            {
                MCBO.HASCLAIM = "No";
                txtenterholdings.Enabled = false;
            }


            if (ChkEasements.Checked == true)
            {
                MCBO.OTHEREASEMENT = "Yes";
                txtentereaseemnts.Enabled = true;
            }
            else if (ChkEasements.Checked == false)
            {
                MCBO.OTHEREASEMENT = "No";
                txtentereaseemnts.Enabled = false;
            }


            if (Chkrighttopick.Checked == true)
                MCBO.PICKFROMOTHPEOPLELAND = "Yes";
            else if (Chkrighttopick.Checked == false)
                MCBO.PICKFROMOTHPEOPLELAND = "No";

            if (Chkotherland.Checked == true)
                MCBO.OTHPEOPLEACCESSWATER = "Yes";
            else if (Chkotherland.Checked == false)
                MCBO.OTHPEOPLEACCESSWATER = "No";

            if (Chkwaterother.Checked == true)
                MCBO.OTHPEOPLEPICK = "Yes";
            else if (Chkwaterother.Checked == false)
                MCBO.OTHPEOPLEPICK = "No";

            if (Chkrighttoacess.Checked == true)
                MCBO.ACCESSWATERFRMOTHPEOPLE = "Yes";
            else if (Chkrighttoacess.Checked == false)
                MCBO.ACCESSWATERFRMOTHPEOPLE = "No";

            if (Session["HH_ID"] != null)
                MCBO.HHID = Convert.ToInt32(Session["HH_ID"].ToString());

            MCBO.CLAIMDETAILS = txtenterholdings.Text;
            MCBO.OTHEREASEMENTDETAILS = txtentereaseemnts.Text;
            MCBO.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            MemberClaimsBLL objMCBLL = new MemberClaimsBLL();
            message = objMCBLL.AddMember(MCBO);

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Data saved successfully";
                //ClearDetails();
                projectFrozen();
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            
            projectFrozen();
            ChangeRequestStatusMCE();

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Members respondents updated successfully');", true);
        }
        #endregion

        #region Check Box Checked Changed
        //protected void ChkHoldings_CheckedChanged(object sender, EventArgs e)
        //{
        //    //   MemberClaimsBO MCBO = new MemberClaimsBO();
        //    if (ChkHoldings.Checked == true)
        //    {
        //        //  MCBO.HASCLAIM = "Yes";
        //        txtenterholdings.Enabled = true;
        //    }
        //    else if (ChkHoldings.Checked == false)
        //    {
        //        // MCBO.HASCLAIM = "No";
        //        txtenterholdings.Enabled = false;
        //    }
        //}

        //protected void ChkEasements_CheckedChanged(object sender, EventArgs e)
        //{
        //    //MemberClaimsBO MCBO = new MemberClaimsBO();
        //    if (ChkEasements.Checked == true)
        //    {
        //        //  MCBO.OTHEREASEMENT = "Yes";
        //        txtentereaseemnts.Enabled = true;
        //    }
        //    else if (ChkEasements.Checked == false)
        //    {
        //        // MCBO.OTHEREASEMENT = "No";
        //        txtentereaseemnts.Enabled = false;
        //    }
        //}
        #endregion

        #region Dropdown Selected Index Changed
        /// <summary>
        /// to retrieve the district data from the database on selected counties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DrpDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(DrpDistrict.SelectedItem.Value);
            BindSubCounty(DrpCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(DrpSubCounty.SelectedItem.Value);
            uplVillage.Update();
        }
        /// <summary>
        /// to retrieve the Subcounties  data from the database on selected counties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DrpCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounty(DrpCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(DrpSubCounty.SelectedItem.Value);
            uplVillage.Update();
        }
        /// <summary>
        /// to retrieve the Villages  data from the database on selected Subcounties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DrpSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(DrpSubCounty.SelectedItem.Value);
        }
        #endregion

        #region Clear Button
        /// <summary>
        /// To clear the datafields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ClearRes_Click(object sender, EventArgs e)
        {
            ClearLandHolding();
        }
        /// <summary>
        /// to clear The data Member fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClearMembers_Click(object sender, EventArgs e)
        {
            ClearMemberClaims();
        }
        /// <summary>
        /// to clear the land holding data 
        /// </summary>

        private void ClearLandHolding()
        {
            txttenure.Text = string.Empty;
            txttotal.Text = string.Empty;
            ChkPrimary.Checked = false;
            ChkAffected.Checked = false;

            DrpType.ClearSelection();
            DrpUse.ClearSelection();
            ddlTenureType.ClearSelection();

            DrpDistrict.ClearSelection();
            BindCounties(DrpDistrict.SelectedItem.Value);
            BindSubCounty(DrpCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(DrpSubCounty.SelectedItem.Value);
            uplVillage.Update();
            ViewState["LND_HOLDINGID"] = "0";
            btn_ClearRes.Text = "Clear";
            btn_SaveRes.Text = "Save";
        }
        /// <summary>
        /// to clear the Member Claims
        /// </summary>

        private void ClearMemberClaims()
        {
            ChkHoldings.Checked = false;
            txtenterholdings.Text = string.Empty;

            Chkrighttopick.Checked = false;
            Chkotherland.Checked = false;
            Chkrighttoacess.Checked = false;
            Chkwaterother.Checked = false;

            ChkEasements.Checked = false;
            txtentereaseemnts.Text = string.Empty;
        }
        #endregion

        #region getMethods
        /// <summary>
        /// to get the data of the Member claim
        /// </summary>
        private void getMemberClaim()
        {
            MemberClaimsBLL oMemberClaimsBLL = new MemberClaimsBLL();
            MemberClaimsBO oMemberClaimsBO = new MemberClaimsBO();
            oMemberClaimsBO = oMemberClaimsBLL.getMemberClaim(Convert.ToInt32(Session["HH_ID"]));

            if (oMemberClaimsBO != null)
            {
                if (oMemberClaimsBO.HASCLAIM.ToLower() == "Yes".ToLower())
                {
                    ChkHoldings.Checked = true;
                    txtenterholdings.Enabled = true;
                }
                else
                {
                    ChkHoldings.Checked = false;
                    txtenterholdings.Enabled = false;
                }

                if (oMemberClaimsBO.OTHEREASEMENT.ToLower() == "Yes".ToLower())
                {
                    ChkEasements.Checked = true;
                    txtentereaseemnts.Enabled = true;
                }
                else
                {
                    ChkEasements.Checked = false;
                    txtentereaseemnts.Enabled = false;
                }

                if (oMemberClaimsBO.PICKFROMOTHPEOPLELAND.ToLower() == "Yes".ToLower())
                    Chkrighttopick.Checked = true;
                else
                    Chkrighttopick.Checked = false;

                if (oMemberClaimsBO.OTHPEOPLEACCESSWATER.ToLower() == "Yes".ToLower())
                    Chkotherland.Checked = true;
                else
                    Chkotherland.Checked = false;

                if (oMemberClaimsBO.OTHPEOPLEPICK.ToLower() == "Yes".ToLower())
                    Chkwaterother.Checked = true;
                else
                    Chkwaterother.Checked = false;

                if (oMemberClaimsBO.ACCESSWATERFRMOTHPEOPLE.ToLower() == "Yes".ToLower())
                    Chkrighttoacess.Checked = true;
                else
                    Chkrighttoacess.Checked = false;

                txtenterholdings.Text = oMemberClaimsBO.CLAIMDETAILS;
                txtentereaseemnts.Text = oMemberClaimsBO.OTHEREASEMENTDETAILS;
            }
        }
        /// <summary>
        /// to get the Land info Respondant
        /// </summary>
        private void getLandInfoRespondant()
        {
            LandInfoRespondentsBO LIR = new LandInfoRespondentsBO();
            LandInfoRespondentsBLL objLIRBLL = new LandInfoRespondentsBLL();
            LIR = objLIRBLL.GetLandInfoRespondentsByID(Convert.ToInt32(ViewState["LND_HOLDINGID"]));

            if (LIR != null)
            {
                DrpType.ClearSelection();
                if (LIR.LND_TYPEID > 0)
                    DrpType.SelectedValue = LIR.LND_TYPEID.ToString();

                DrpUse.ClearSelection();
                if (LIR.LND_USEID > 0)
                    DrpUse.SelectedValue = LIR.LND_USEID.ToString();

                DrpDistrict.ClearSelection();
                if (DrpDistrict.Items.FindByText(LIR.DISTRICT) != null)
                    DrpDistrict.Items.FindByText(LIR.DISTRICT).Selected = true;

                BindCounties(DrpDistrict.SelectedItem.Value);
                DrpCounty.ClearSelection();
                if (DrpCounty.Items.FindByText(LIR.COUNTY) != null)
                    DrpCounty.Items.FindByText(LIR.COUNTY).Selected = true;

                BindSubCounty(DrpCounty.SelectedItem.Value);
                DrpSubCounty.ClearSelection();
                if (DrpSubCounty.Items.FindByText(LIR.SUBCOUNTY) != null)
                    DrpSubCounty.Items.FindByText(LIR.SUBCOUNTY).Selected = true;

                BindVillages(DrpSubCounty.SelectedItem.Value);
                DrpVillage.ClearSelection();
                if (DrpVillage.Items.FindByText(LIR.VILLAGE) != null)
                    DrpVillage.Items.FindByText(LIR.VILLAGE).Selected = true;

                //DrpType.SelectedItem.Value = Convert.ToString(LIR.LND_TYPEID);
                //DrpUse.SelectedItem.Value = Convert.ToString(LIR.LND_USEID);
                //DrpDistrict.SelectedItem.Value = Convert.ToString(LIR.DISTRICT);
                //DrpCounty.SelectedItem.Value = Convert.ToString(LIR.COUNTY);
                //DrpSubCounty.SelectedItem.Value = Convert.ToString(LIR.SUBCOUNTY);
                //DrpVillage.SelectedItem.Value = Convert.ToString(LIR.VILLAGE);

                ddlTenureType.ClearSelection();
                if (ddlTenureType.Items.FindByValue(LIR.TenureId.ToString()) != null)
                    ddlTenureType.SelectedValue = LIR.TenureId.ToString();

                if (LIR.ISPRIMARYRESIDENCE.ToLower() == "Yes".ToLower())
                    ChkPrimary.Checked = true;
                else
                    ChkPrimary.Checked = false;

                if (LIR.ISAFFECTED.ToLower() == "Yes".ToLower())
                    ChkAffected.Checked = true;
                else
                    ChkAffected.Checked = false;

                if (LIR.TOTALSIZE != -1)
                {
                    txttotal.Text = Convert.ToString(LIR.TOTALSIZE);
                }
                else
                    txttotal.Text = string.Empty;

                if (LIR.TENURE != null)
                    txttenure.Text = LIR.TENURE;
                else
                    txttenure.Text = string.Empty;
            }
        }
        #endregion
    }
}