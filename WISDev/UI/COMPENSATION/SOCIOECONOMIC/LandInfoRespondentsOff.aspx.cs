using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class LandInfoRespondentsOff : System.Web.UI.Page
    {

        #region Global Declaration
        DwellingBLL objDwellingBLL;
        DwellingList objDwellingList;

        RoofTypeBLL objRoofTypeBLL;
        RoofTypeList objRoofTypeList;

        TenureStructureBLL objTenureStructureBLL;
        TenureStructureList objTenureStructureList;

        FloorTypeBLL objFloorTypeBLL;
        FloorTypeList objFloorTypeList;

        WallTypeBLL objWallTypeBLL;
        WallTypeList objWallTypeList;

        LandLivingOffBO objSurveyLandLivingOff;
        SurveyBLL objSurveyBLL;
        LandLivingOffList objLandLivingOffList;
        #endregion
        /// <summary>
        /// Set Page header,Call  bindDDLDwelling() to bind the Dwelling data to the dropDown
        /// call bindDDLTenure() to bind the Tenure data to the DropDown 
        /// call  bindDDLRoofs() to bind the DDlRoofs data to the dropdown 
        /// call bindDDLWall() to bind the Wall data to the respective dropdown
        /// call bindDDLFloor() to bind the Floor data to the respective dropdown
        /// call BindGrid() method to Bind the data to the GridView
        /// to set the status of the Link button lnkLandInfoResOFF,lnkLogout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region PageEvents
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.OffAffectedLand;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.LivingoffAffectedLand;

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
            if (!Page.IsPostBack)
            {
                Master.PageHeader = "Socio-Economic - Land Information - Respondents living off Affected Plot";

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Land Information - Respondents living off Affected Plot";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ViewState["LIVINGOFFID"] = 0;
                bindDDLDwelling();
                bindDDLTenure();
                bindDDLRoofs();
                bindDDLWall();
                bindDDLFloor();
                BindGrid(false, false);
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkLandInfoResOFF.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = false;
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
                lnkLandInfoResOFF.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = false;
                grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = false;
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
        /// <summary>
        /// to save the data to the database
        /// call  ClearDetails() to clear the Details Fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objSurveyLandLivingOff = new LandLivingOffBO();
            objSurveyBLL = new SurveyBLL();

            try
            {
                objSurveyLandLivingOff.LivingOffID = Convert.ToInt32(ViewState["LIVINGOFFID"]);
                objSurveyLandLivingOff.HouseholdID = Convert.ToInt32(Session["HH_ID"]);

                objSurveyLandLivingOff.DwellingID = Convert.ToInt32(ddlDwelling.SelectedValue);
                objSurveyLandLivingOff.NoofRooms = txtNoRooms.Text.Trim();
                objSurveyLandLivingOff.Str_TenureID = Convert.ToInt32(ddlTenureType.SelectedValue);
                objSurveyLandLivingOff.RoofID = Convert.ToInt32(ddlRoof.SelectedValue);
                objSurveyLandLivingOff.WallID = Convert.ToInt32(ddlWalls.SelectedValue);
                objSurveyLandLivingOff.FloorID = Convert.ToInt32(ddlFloor.SelectedValue);
                objSurveyLandLivingOff.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                
                if (!string.IsNullOrEmpty(txttenure.Text))
                    objSurveyLandLivingOff.Tenure = Convert.ToInt32(txttenure.Text);
                else
                    objSurveyLandLivingOff.Tenure = 0;

                int i = objSurveyBLL.AddLandLivingOFF(objSurveyLandLivingOff);

                if (btnSave.Text == "Save")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data saved successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data updated successfully');", true);
                }
                ChangeRequestStatusLandInfoResOFF();
                ClearDetails();
                BindGrid(true, false);
                SetUpdateMode(false);
                projectFrozen();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        #endregion

        #region Frozen / Approval / Decline / Pending
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusLandInfoResOFF();
                }
            }
        }

        public void ChangeRequestStatusLandInfoResOFF()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HLIOF";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusLandInfoResOFF.Text = "";
            StatusLandInfoResOFF.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HLIOF");
                lnkLandInfoResOFF.Attributes.Add("onclick", paramChangeRequest);
                lnkLandInfoResOFF.Visible = true;
            }
            else
            {
                lnkLandInfoResOFF.Visible = false;
            }
            #endregion
            getApprrequtStatusLandInfoResOFF();
        }

        public void getApprrequtStatusLandInfoResOFF()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HLIOF";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkLandInfoResOFF.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = false;
                    StatusLandInfoResOFF.Visible = true;
                    StatusLandInfoResOFF.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkLandInfoResOFF.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = false;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = false;
                    StatusLandInfoResOFF.Visible = false;
                    StatusLandInfoResOFF.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkLandInfoResOFF.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 1].Visible = true;
                    grdLandInfoRespondentsOff.Columns[grdLandInfoRespondentsOff.Columns.Count - 2].Visible = true;
                    StatusLandInfoResOFF.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        #region GridEvents
        /// <summary>
        /// For Edit and delete command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLandInfoRespondentsOff_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["LIVINGOFFID"] = e.CommandArgument;
                objSurveyBLL = new SurveyBLL();
                objSurveyLandLivingOff = objSurveyBLL.GetLivingOFFByID(Convert.ToInt32(ViewState["LIVINGOFFID"]));
                //bindDDLDwelling();
                //bindDDLTenure();
                //bindDDLRoofs();
                //bindDDLWall();
                //bindDDLFloor();

                ddlDwelling.ClearSelection();
                if (ddlDwelling.Items.FindByValue(objSurveyLandLivingOff.DwellingID.ToString()) != null)
                    ddlDwelling.Items.FindByValue(objSurveyLandLivingOff.DwellingID.ToString()).Selected = true;
                ddlTenureType.ClearSelection();
                if (ddlTenureType.Items.FindByValue(objSurveyLandLivingOff.Str_TenureID.ToString()) != null)
                    ddlTenureType.Items.FindByValue(objSurveyLandLivingOff.Str_TenureID.ToString()).Selected = true;
                ddlRoof.ClearSelection();
                if (ddlRoof.Items.FindByValue(objSurveyLandLivingOff.RoofID.ToString()) != null)
                    ddlRoof.Items.FindByValue(objSurveyLandLivingOff.RoofID.ToString()).Selected = true;
                ddlWalls.ClearSelection();
                if (ddlWalls.Items.FindByValue(objSurveyLandLivingOff.WallID.ToString()) != null)
                    ddlWalls.Items.FindByValue(objSurveyLandLivingOff.WallID.ToString()).Selected = true;
                ddlFloor.ClearSelection();
                if (ddlFloor.Items.FindByValue(objSurveyLandLivingOff.FloorID.ToString()) != null)
                    ddlFloor.Items.FindByValue(objSurveyLandLivingOff.FloorID.ToString()).Selected = true;

                txtNoRooms.Text = objSurveyLandLivingOff.NoofRooms;
                
                //ddlTenureType.ClearSelection();
                //if (ddlTenureType.Items.FindByValue(objSurveyLandLivingOff.Str_Tenure.ToString()) != null)
                //    ddlTenureType.Items.FindByValue(objSurveyLandLivingOff.Str_Tenure.ToString()).Selected = true;

                if (objSurveyLandLivingOff.Tenure > 0)
                    txttenure.Text = objSurveyLandLivingOff.Tenure.ToString();

                SetUpdateMode(true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                int livingoffID = Convert.ToInt32(e.CommandArgument);
                objSurveyBLL = new SurveyBLL();
                objSurveyBLL.DeleteLivingOFF(livingoffID);
                ClearDetails();
                BindGrid(false, true);
                SetUpdateMode(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }
        /// <summary>
        /// to changing the page indexs in the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLandInfoRespondentsOff_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLandInfoRespondentsOff.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
        }
        #endregion

        #region Method
       /// <summary>
        /// to bind the data from the database to the dropdown ddlDwelling
       /// </summary>
        private void bindDDLDwelling()
        {
            objDwellingBLL = new DwellingBLL();
            objDwellingList = objDwellingBLL.GetDwelling();
            ddlDwelling.DataSource = objDwellingList;
            ddlDwelling.DataTextField = "dwellingtype";
            ddlDwelling.DataValueField = "dwellingid";
            ddlDwelling.DataBind();
        }
        /// <summary>
        /// to bind the data from the database to the dropdown ddlTenureType
        /// </summary>

        private void bindDDLTenure()
        {
            objTenureStructureBLL = new TenureStructureBLL();
            objTenureStructureList = objTenureStructureBLL.GetTenureStructures("");
            ddlTenureType.DataSource = objTenureStructureList;
            ddlTenureType.DataTextField = "STR_TENURE";
            ddlTenureType.DataValueField = "STR_TENUREID";
            ddlTenureType.DataBind();
        }
        /// <summary>
        /// to bind the data from the database to the dropdown ddlRoof
        /// </summary>
        private void bindDDLRoofs()
        {
            objRoofTypeBLL = new RoofTypeBLL();
            objRoofTypeList = objRoofTypeBLL.GetRoofType();
            ddlRoof.DataSource = objRoofTypeList;
            ddlRoof.DataTextField = "RoofTypeName";
            ddlRoof.DataValueField = "RoofTypeID";
            ddlRoof.DataBind();
        }

        /// <summary>
        ///  to bind the data from the database to the dropdown ddlWalls
        /// </summary>
        private void bindDDLWall()
        {
            objWallTypeBLL = new WallTypeBLL();
            objWallTypeList = objWallTypeBLL.GetWallType();
            ddlWalls.DataSource = objWallTypeList;
            ddlWalls.DataTextField = "WallTypeName";
            ddlWalls.DataValueField = "WallTypeID";
            ddlWalls.DataBind();
        }
        /// <summary>
        /// to bind the data from the database to the dropdown ddlFloor
        /// </summary>
        private void bindDDLFloor()
        {
            objFloorTypeBLL = new FloorTypeBLL();
            objFloorTypeList = objFloorTypeBLL.GetFloorType();
            ddlFloor.DataSource = objFloorTypeList;
            ddlFloor.DataTextField = "FloorTypeName";
            ddlFloor.DataValueField = "FloorTypeID";
            ddlFloor.DataBind();
        }
        /// <summary>
        /// to get the data from the database and bind it to the gridview
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>

        private void BindGrid(bool addRow, bool deleteRow)
        {
            try
            {
                objSurveyBLL = new SurveyBLL();
                objLandLivingOffList = new LandLivingOffList();
                objLandLivingOffList = objSurveyBLL.GetLivingOFF(Convert.ToInt32(Session["HH_ID"]));
                grdLandInfoRespondentsOff.DataSource = objLandLivingOffList;
                grdLandInfoRespondentsOff.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// to clear the data fields
        /// </summary>
        private void ClearDetails()
        {
            ddlDwelling.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoof.SelectedIndex = 0;
            ddlTenureType.SelectedIndex = 0;
            ddlWalls.SelectedIndex = 0;
            txtNoRooms.Text = "";
            txttenure.Text = string.Empty;
        }
        /// <summary>
        /// to set the Status of the Panel
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
                ViewState["LIVINGOFFID"] = "0";
            }
        }
        #endregion
    }
}