using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.IO;
using System.Text;

namespace WIS
{
    public partial class PermanenetBuilding : System.Web.UI.Page
    {
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.PermanentBuildings;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.PermanentBuilding;
            Page.Response.Cache.SetNoStore();
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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Permanent Buildings";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ViewState["PERM_STRUCTUREID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data

                GetStructureTypeData();

                GetRoofMaterial();

                GetWallmaterial();

                GetFloorMaterial();

                GetWindowMaterial();

                GetOccupantStatus();

                GetConditionData();

                txtbxDepreciatedValue.Attributes.Add("onblur", "CalculateAmount();");
                txtbxReplacementValue.Attributes.Add("onblur", "CalculateAmount();");
                txtbxReplacementUplift.Attributes.Add("onKeyDown", "doCheck();");

                txtbxLength.Attributes.Add("onchange", "surfacearea();");
                txtbxWidth.Attributes.Add("onchange", "surfacearea();");
                txtbxSurfaceArea.Attributes.Add("onKeyDown", "doCheck();");


                txtbxDepreciatedValue.Attributes.Add("onchange", "setDirtyText();");
                txtbxReplacementValue.Attributes.Add("onchange", "setDirtyText();");


                RbtnSelf.Attributes.Add("onclick", "EnableDisableOtherOwner(0);");
                RbtnOther.Attributes.Add("onclick", "EnableDisableOtherOwner(1);");
                RdbtnSelfoccupied.Attributes.Add("onclick", "EnableDisableOtherOccupant(0);");
                RdbtnOccupantOther.Attributes.Add("onclick", "EnableDisableOtherOccupant(1);");

                ddlOccupantStatus.Attributes.Add("onchange", "EnableDisableOtherOccupantStatus(this);");

                popupData();
                //lnkViewPhoto.Visible = false;                

                projectFrozen();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    lnkPermBuild.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = false;
                }
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
                lnkPermBuild.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = false;
                grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = false;
                grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = false;
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
            stb.Append(btnSave.ClientID);
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
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = false;

                    checkApprovalExitOrNot();
                    getApprrequtStatusPermBuild();
                }
                else
                {
                    lnkPermBuild.Visible = false;
                }
            }
        }

        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusPermBuild.Text = "";
            StatusPermBuild.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVPBU");
                lnkPermBuild.Attributes.Add("onclick", paramChangeRequest);
                lnkPermBuild.Visible = true;
            }
            else
            {
                lnkPermBuild.Visible = false;
            }
            #endregion
            getApprrequtStatusPermBuild();

        }

        public void ChangeRequestStatusPermBuild()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVPBU";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }

        public void getApprrequtStatusPermBuild()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVPBU";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPermBuild.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = false;
                    StatusPermBuild.Visible = true;
                    StatusPermBuild.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPermBuild.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = false;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = false;
                    StatusPermBuild.Visible = false;
                    StatusPermBuild.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPermBuild.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 1].Visible = true;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 2].Visible = true;
                    grdPermanentBuilding.Columns[grdPermanentBuilding.Columns.Count - 4].Visible = true;
                    StatusPermBuild.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        private void GetConditionData()
        {
            StructureConditionBLL BLLobj = new StructureConditionBLL();


            ddlRoofCondition.DataSource = BLLobj.GetStructureCondition();
            ddlRoofCondition.DataTextField = "StructureConditionName";
            ddlRoofCondition.DataValueField = "StructureConditionID";
            ddlRoofCondition.DataBind();

            ddlWallsCondition.DataSource = BLLobj.GetStructureCondition();
            ddlWallsCondition.DataTextField = "StructureConditionName";
            ddlWallsCondition.DataValueField = "StructureConditionID";
            ddlWallsCondition.DataBind();

            ddlFloorCondition.DataSource = BLLobj.GetStructureCondition();
            ddlFloorCondition.DataTextField = "StructureConditionName";
            ddlFloorCondition.DataValueField = "StructureConditionID";
            ddlFloorCondition.DataBind();

            ddlWindowsCondition.DataSource = BLLobj.GetStructureCondition();
            ddlWindowsCondition.DataTextField = "StructureConditionName";
            ddlWindowsCondition.DataValueField = "StructureConditionID";
            ddlWindowsCondition.DataBind();

        }

        /// <summary>
        /// To get the occupant status
        /// </summary>
        private void GetOccupantStatus()
        {
            PermanentStructureBLL BLLobj = new PermanentStructureBLL();
            ddlOccupantStatus.DataSource = BLLobj.GetOccupantstatus();
            ddlOccupantStatus.DataTextField = "OCCUPANTSTATUS";
            ddlOccupantStatus.DataValueField = "OCCUPANTSTATUSID";
            ddlOccupantStatus.DataBind();

            //OccupationBLL BLLobj = new OccupationBLL();

            //ddlOccupantStatus.DataSource = BLLobj.GetOccupation();
            //ddlOccupantStatus.DataTextField = "OccupationName";
            //ddlOccupantStatus.DataValueField = "OccupationID";
            //ddlOccupantStatus.DataBind();

        }

        /// <summary>
        /// To get the window material
        /// </summary>
        private void GetWindowMaterial()
        {
            WindowTypeBLL BLLobj = new WindowTypeBLL();

            ddlWindowsMaterial.DataSource = BLLobj.GetWindowType();
            ddlWindowsMaterial.DataTextField = "WindowTypeName";
            ddlWindowsMaterial.DataValueField = "WindowTypeID";
            ddlWindowsMaterial.DataBind();

        }

        /// <summary>
        ///  To get the floor material
        /// </summary>
        private void GetFloorMaterial()
        {
            FloorTypeBLL BLLobj = new FloorTypeBLL();

            ddlFloorMaterial.DataSource = BLLobj.GetFloorType();
            ddlFloorMaterial.DataTextField = "FloorTypeName";
            ddlFloorMaterial.DataValueField = "FloorTypeID";
            ddlFloorMaterial.DataBind();

        }

        /// <summary>
        ///  To get the wall material
        /// </summary>
        private void GetWallmaterial()
        {
            WallTypeBLL BLLobj = new WallTypeBLL();

            ddlWallsMaterial.DataSource = BLLobj.GetWallType();
            ddlWallsMaterial.DataTextField = "WallTypeName";
            ddlWallsMaterial.DataValueField = "WallTypeID";
            ddlWallsMaterial.DataBind();


        }

        /// <summary>
        ///  To get the roof material
        /// </summary>
        private void GetRoofMaterial()
        {
            RoofTypeBLL BLLobj = new RoofTypeBLL();


            ddlRoofMaterial.DataSource = BLLobj.GetRoofType();
            ddlRoofMaterial.DataTextField = "RoofTypeName";
            ddlRoofMaterial.DataValueField = "RoofTypeID";
            ddlRoofMaterial.DataBind();

        }

        /// <summary>
        ///  To get the structure type
        /// </summary>
        private void GetStructureTypeData()
        {
            StructureTypeBLL BLLobj = new StructureTypeBLL();

            ddlBuidingType.DataSource = BLLobj.GetStructureType();
            ddlBuidingType.DataTextField = "StructureTypeName";
            ddlBuidingType.DataValueField = "StructureTypeID";
            ddlBuidingType.DataBind();

        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            string hhid = Session["HH_ID"].ToString();
            PermanentStructureBLL PermanentStructureBLLobj = new PermanentStructureBLL();
            grdPermanentBuilding.DataSource = PermanentStructureBLLobj.GetPermanentStructure(hhid);
            grdPermanentBuilding.DataBind();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PermanentBuilding_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PERM_STRUCTUREID"] = e.CommandArgument;
                GetData();
                //lnkViewPhoto.Visible = true;
                popupData();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string structurId = e.CommandArgument.ToString();
                DeletePermanentStruct(structurId);
                SetUpdateMode(false);
                BindGrid(false, true);
                Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }

        public void popupData()
        {
            //Add by Ramu.S For Upload Photo and View Photo;

            //string userName = (Session["userName"].ToString());
            //int userID = Convert.ToInt32(Session["USER_ID"]);
            //int ProjectID = 0;
            //string ProjectCode = string.Empty;
            //string perStu = string.Empty;

            //if (Session["PROJECT_ID"] != null)
            //{
            //    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            //    ProjectCode = Session["PROJECT_CODE"].ToString();
            //}

            //int HHID = 0;
            //if (Session["HH_ID"] != null)
            //{
            //    HHID = Convert.ToInt32(Session["HH_ID"]);
            //}

            //if (Session["PROJECT_CODE"] != null)
            //{
            //    ProjectCode = Session["PROJECT_CODE"].ToString();
            //}

            //if (ViewState["PERM_STRUCTUREID"] != null)
            //{
            //    perStu = ViewState["PERM_STRUCTUREID"].ToString();
            //}
            //string PhotoModule = "PAPPB";

            //string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            //SetUpdateMode(true);
        }

        /// <summary>
        /// To delete the data from database
        /// </summary>
        /// <param name="structurId"></param>
        private void DeletePermanentStruct(string structurId)
        {
            PermanentStructureBLL PermanentStructureBLLobj = new PermanentStructureBLL();
            PermanentStructureBLLobj.DeletePermanentStruct(structurId);
        }
        /// <summary>
        /// To fetch details and assign to textbox
        /// </summary>
        private void GetData()
        {
            PermanentStructureBLL PermanentStructureBLLobj = new PermanentStructureBLL();
            int STRUCTUREID = 0;

            if (ViewState["PERM_STRUCTUREID"] != null)
                STRUCTUREID = Convert.ToInt32(ViewState["PERM_STRUCTUREID"]);

            PermanentStructureBO BOobj = new PermanentStructureBO();
            BOobj = PermanentStructureBLLobj.GetSTRUCTUREID(STRUCTUREID);

            perstructIDTextBox.Text = BOobj.PermanentStructureID.ToString();


            //byte[] papPhotoBytes = (byte[])BOobj.Photo;

            // System.IO.MemoryStream objImage = (System.IO.MemoryStream)(RetrieveImage(BOobj.Photo as byte[]));
            // photoImage.ImageUrl = objImage.ToString();

            ddlBuidingType.ClearSelection();
            if (ddlBuidingType.Items.FindByValue(BOobj.StructureTypeID.ToString()) != null)
                ddlBuidingType.Items.FindByValue(BOobj.StructureTypeID.ToString()).Selected = true;

            otherSpecifyTextBox.Text = BOobj.OtherStructureType.ToString();

            ddlRoofMaterial.ClearSelection();
            if (ddlRoofMaterial.Items.FindByValue(BOobj.RoofID.ToString()) != null)
                ddlRoofMaterial.Items.FindByValue(BOobj.RoofID.ToString()).Selected = true;

            ddlWallsMaterial.ClearSelection();
            if (ddlWallsMaterial.Items.FindByValue(BOobj.WallID.ToString()) != null)
                ddlWallsMaterial.Items.FindByValue(BOobj.WallID.ToString()).Selected = true;

            ddlFloorMaterial.ClearSelection();
            if (ddlFloorMaterial.Items.FindByValue(BOobj.FloorID.ToString()) != null)
                ddlFloorMaterial.Items.FindByValue(BOobj.FloorID.ToString()).Selected = true;

            ddlWindowsMaterial.ClearSelection();
            if (ddlWindowsMaterial.Items.FindByValue(BOobj.WindowID.ToString()) != null)
                ddlWindowsMaterial.Items.FindByValue(BOobj.WindowID.ToString()).Selected = true;

            ddlRoofCondition.ClearSelection();
            if (ddlRoofCondition.Items.FindByValue(BOobj.RoofConditionID.ToString()) != null)
                ddlRoofCondition.Items.FindByValue(BOobj.RoofConditionID.ToString()).Selected = true;

            ddlWallsCondition.ClearSelection();
            if (ddlWallsCondition.Items.FindByValue(BOobj.WallConditionID.ToString()) != null)
                ddlWallsCondition.Items.FindByValue(BOobj.WallConditionID.ToString()).Selected = true;

            ddlFloorCondition.ClearSelection();
            if (ddlFloorCondition.Items.FindByValue(BOobj.FloorConditionID.ToString()) != null)
                ddlFloorCondition.Items.FindByValue(BOobj.FloorConditionID.ToString()).Selected = true;

            ddlWindowsCondition.ClearSelection();
            if (ddlWindowsCondition.Items.FindByValue(BOobj.WindowConditionID.ToString()) != null)
                ddlWindowsCondition.Items.FindByValue(BOobj.WindowConditionID.ToString()).Selected = true;

            if (BOobj.Owner == "Self")
            {
                RbtnSelf.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick123", "EnableDisableOtherOwner(0);", true);
            }
            else
            {
                RbtnOther.Checked = true;
                txtbxOther.Text = BOobj.OwnerName.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick43", "EnableDisableOtherOwner(1);", true);
            }

            if (BOobj.Occupant == "Self")
            {
                RdbtnSelfoccupied.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick21", "EnableDisableOtherOccupant(0);", true);
            }
            else
            {
                RdbtnOccupantOther.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick67", "EnableDisableOtherOccupant(1);", true);
                txtbxOccupantOther.Text = BOobj.OtherOccupantName;
            }

            ddlOccupantStatus.ClearSelection();
            if (ddlOccupantStatus.Items.FindByValue(BOobj.OccupantStatusID.ToString()) != null)
                ddlOccupantStatus.Items.FindByValue(BOobj.OccupantStatusID.ToString()).Selected = true;

            txtbxEnterStatus.Text = BOobj.OtherOccupantStatus.ToString();
            txtbxLength.Text = BOobj.DimensionLength.ToString();
            txtbxWidth.Text = BOobj.DimensionWidth.ToString();
            txtbxNoofrooms.Text = BOobj.NoOfRooms.ToString();
            decimal j =Convert.ToDecimal(BOobj.DimensionLength.ToString());
            decimal k =Convert.ToDecimal(BOobj.DimensionWidth.ToString());
            decimal res = j * k;
            txtbxSurfaceArea.Text =Convert.ToString(res);//BOobj.SurfaceArea.ToString();


            ddlStructureType.ClearSelection();
            if (ddlStructureType.Items.FindByValue(BOobj.StructureType.ToString()) != null)
                ddlStructureType.Items.FindByValue(BOobj.StructureType.ToString()).Selected = true;

            txtbxDepreciatedValue.Text = BOobj.DepreciatedValue.ToString();
            txtbxReplacementValue.Text = BOobj.ReplacementValue.ToString();
            txtbxComments.Text = BOobj.AdditionalComments.ToString();
            if (txtbxDepreciatedValue.Text.Trim().Length > 0 && txtbxReplacementValue.Text.Trim().Length > 0)
                txtbxReplacementUplift.Text = (Convert.ToDouble(txtbxDepreciatedValue.Text.Trim()) - Convert.ToDouble(txtbxReplacementValue.Text.Trim())).ToString();
            //txtNeighbrID.Text = Neighbourobj.PAP_NEIGHBOURID1.ToString();
            //txtNeibrName.Text = Neighbourobj.TRN_PAP_NEIGHBOURNAme1.ToString();
            //ddldirectionDropDownList.SelectedItem.Text = Neighbourobj.DIRECTION1.ToString();

        }

        /// <summary>
        /// To save the data into database
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChangeRequestStatusPermBuild();
            SaveData();
            projectFrozen();
          
        }
        
        /// <summary>
        /// To save the data into database
        /// </summary>
        private void SaveData()
        {
            int count = 0;

            if (perstructIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                PermanentStructureBLL BLLobj = new PermanentStructureBLL();
                PermanentStructureBO PermanentStructureobj = new PermanentStructureBO();

                string uID = Session["USER_ID"].ToString();
                string hhid = Session["HH_ID"].ToString();

                try
                {
                    PermanentStructureobj.HouseholdID = Convert.ToInt32(hhid);
                    PermanentStructureobj.StructureTypeID = Convert.ToInt32(ddlBuidingType.SelectedValue);
                    if (otherSpecifyTextBox.Text != string.Empty)
                    {
                        PermanentStructureobj.OtherStructureType = otherSpecifyTextBox.Text;
                    }
                    else
                    {
                        PermanentStructureobj.OtherStructureType = "";
                    }
                    PermanentStructureobj.RoofID = Convert.ToInt32(ddlRoofMaterial.SelectedValue);
                    PermanentStructureobj.WallID = Convert.ToInt32(ddlWallsMaterial.SelectedValue);
                    PermanentStructureobj.FloorID = Convert.ToInt32(ddlFloorMaterial.SelectedValue);
                    PermanentStructureobj.WindowID = Convert.ToInt32(ddlWindowsMaterial.SelectedValue);

                    PermanentStructureobj.RoofConditionID = Convert.ToInt32(ddlRoofCondition.SelectedValue);
                    PermanentStructureobj.WallConditionID = Convert.ToInt32(ddlWallsCondition.SelectedValue);
                    PermanentStructureobj.FloorConditionID = Convert.ToInt32(ddlFloorCondition.SelectedValue);
                    PermanentStructureobj.WindowConditionID = Convert.ToInt32(ddlWindowsCondition.SelectedValue);

                    if (RbtnSelf.Checked == true || (RbtnSelf.Checked == false && RbtnOther.Checked == false))
                    {
                        PermanentStructureobj.Owner = "Self";
                    }
                    else if (RbtnOther.Checked == true)
                    {
                        PermanentStructureobj.Owner = "Others";
                        PermanentStructureobj.OwnerName = txtbxOther.Text;
                    }

                    if (RdbtnSelfoccupied.Checked == true || (RdbtnSelfoccupied.Checked == false && RdbtnOccupantOther.Checked == false))
                    {
                        PermanentStructureobj.Occupant = "Self";
                    }
                    else if (RdbtnOccupantOther.Checked == true)
                    {
                        PermanentStructureobj.Occupant = "Others";
                        PermanentStructureobj.OtherOccupantName = txtbxOccupantOther.Text;
                    }
                    PermanentStructureobj.OccupantStatusID = Convert.ToInt32(ddlOccupantStatus.SelectedValue.ToString());
                    if (txtbxEnterStatus.Text != string.Empty)
                    {
                        PermanentStructureobj.OtherOccupantStatus = txtbxEnterStatus.Text;
                    }
                    else
                    {
                        PermanentStructureobj.OtherOccupantStatus = "";
                    }

                    if (txtbxLength.Text != string.Empty)
                        PermanentStructureobj.DimensionLength = Convert.ToDecimal(txtbxLength.Text);
                    else
                        PermanentStructureobj.DimensionLength = Convert.ToDecimal(0);

                    if (txtbxWidth.Text != string.Empty)
                        PermanentStructureobj.DimensionWidth = Convert.ToDecimal(txtbxWidth.Text);
                    else
                        PermanentStructureobj.DimensionWidth = Convert.ToDecimal(0);

                    if (txtbxNoofrooms.Text != string.Empty)
                    {
                        PermanentStructureobj.NoOfRooms = Convert.ToInt32(txtbxNoofrooms.Text);
                    }
                    else
                    {
                        PermanentStructureobj.NoOfRooms = 0;
                    }
                    if (txtbxSurfaceArea.Text != string.Empty)
                    {
                        PermanentStructureobj.SurfaceArea = Convert.ToDecimal(txtbxSurfaceArea.Text);
                    }
                    else
                    {
                        PermanentStructureobj.SurfaceArea = 0;
                    }
                    if (txtbxDepreciatedValue.Text.Trim() != string.Empty)
                    {
                        PermanentStructureobj.DepreciatedValue = Convert.ToDecimal(txtbxDepreciatedValue.Text);
                       
                    }
                    else
                    {
                        PermanentStructureobj.DepreciatedValue = 0;
                       
                    }
                    PermanentStructureobj.ReplacementValue = Convert.ToDecimal(txtbxReplacementValue.Text);

                    if (txtbxComments.Text != string.Empty)
                    {
                        if (txtbxComments.Text.Length > 1000)
                            PermanentStructureobj.AdditionalComments = txtbxComments.Text.Substring(0, 1000);
                        else
                            PermanentStructureobj.AdditionalComments = txtbxComments.Text;
                    }
                    else
                        PermanentStructureobj.AdditionalComments = "";

                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;

                        if (fileBytes != null)
                        {
                            PermanentStructureobj.Photo = fileBytes;
                        }

                    }
                    PermanentStructureobj.StructureType = ddlStructureType.SelectedValue.ToString();
                    PermanentStructureobj.IsDeleted = "False";
                    PermanentStructureobj.CreatedBy = Convert.ToInt32(uID);

                    PermanentStructureBLL PBLLobject = new PermanentStructureBLL();
                    count = PBLLobject.Insert(PermanentStructureobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);
                    Clear();
                    BindGrid(true, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    BLLobj = null;
                }
            }

            else if (perstructIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                PermanentStructureBLL BLLobj = new PermanentStructureBLL();
                string hhid = Session["HH_ID"].ToString();
                string uID = Session["USER_ID"].ToString();

                try
                {
                    if (photoFileUpload.HasFile)
                    {

                        PermanentStructureBO PermanentStructureobj1 = new PermanentStructureBO();
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        PermanentStructureobj1.PermanentStructureID = Convert.ToInt32(perstructIDTextBox.Text);
                        PermanentStructureobj1.HouseholdID = Convert.ToInt32(hhid);
                        PermanentStructureobj1.UpdatedBy = Convert.ToInt32(uID);
                        PermanentStructureobj1.Photo = fileBytes;


                        PermanentStructureBLL BLLobj1 = new PermanentStructureBLL();
                        count = BLLobj1.Updatephoto(PermanentStructureobj1);

                    }



                    PermanentStructureBO PermanentStructureobj = new PermanentStructureBO();

                    PermanentStructureobj.HouseholdID = Convert.ToInt32(hhid);
                    PermanentStructureobj.PermanentStructureID = Convert.ToInt32(perstructIDTextBox.Text);
                    PermanentStructureobj.StructureTypeID = Convert.ToInt32(ddlBuidingType.SelectedValue);
                    if (otherSpecifyTextBox.Text != string.Empty)
                    {
                        PermanentStructureobj.OtherStructureType = otherSpecifyTextBox.Text;
                    }
                    else
                    {
                        PermanentStructureobj.OtherStructureType = "";
                    }
                    PermanentStructureobj.RoofID = Convert.ToInt32(ddlRoofMaterial.SelectedValue);
                    PermanentStructureobj.WallID = Convert.ToInt32(ddlWallsMaterial.SelectedValue);
                    PermanentStructureobj.FloorID = Convert.ToInt32(ddlFloorMaterial.SelectedValue);
                    PermanentStructureobj.WindowID = Convert.ToInt32(ddlWindowsMaterial.SelectedValue);
                    PermanentStructureobj.RoofConditionID = Convert.ToInt32(ddlRoofCondition.SelectedValue);
                    PermanentStructureobj.WallConditionID = Convert.ToInt32(ddlWallsCondition.SelectedValue);
                    PermanentStructureobj.FloorConditionID = Convert.ToInt32(ddlFloorCondition.SelectedValue);
                    PermanentStructureobj.WindowConditionID = Convert.ToInt32(ddlWindowsCondition.SelectedValue);
                    if (RbtnSelf.Checked == true)
                    {
                        PermanentStructureobj.Owner = RbtnSelf.Text;
                        //RbtnOther.Checked = false;

                    }
                    else if (RbtnOther.Checked == true)
                    {
                        PermanentStructureobj.Owner = RbtnOther.Text;
                        PermanentStructureobj.OwnerName = txtbxOther.Text;
                        //RbtnSelf.Checked = false;
                    }

                    if (RdbtnSelfoccupied.Checked == true)
                    {
                        PermanentStructureobj.Occupant = RdbtnSelfoccupied.Text;
                        //RdbtnOccupantOther.Checked = false;

                    }
                    else if (RdbtnOccupantOther.Checked == true)
                    {
                        PermanentStructureobj.Occupant = RdbtnOccupantOther.Text;
                        PermanentStructureobj.OtherOccupantName = txtbxOccupantOther.Text.Trim();
                        //RdbtnSelfoccupied.Checked = false;

                    }

                    PermanentStructureobj.OccupantStatusID = Convert.ToInt32(ddlOccupantStatus.SelectedValue.ToString());

                    if (txtbxEnterStatus.Text != string.Empty)
                    {
                        PermanentStructureobj.OtherOccupantStatus = txtbxEnterStatus.Text;
                    }
                    else
                    {
                        PermanentStructureobj.OtherOccupantStatus = "";
                    }

                    PermanentStructureobj.DimensionLength = Convert.ToDecimal(txtbxLength.Text);
                    PermanentStructureobj.DimensionWidth = Convert.ToDecimal(txtbxWidth.Text);
                    if (txtbxNoofrooms.Text != string.Empty)
                    {
                        PermanentStructureobj.NoOfRooms = Convert.ToInt32(txtbxNoofrooms.Text);
                    }
                    else
                    {
                        PermanentStructureobj.NoOfRooms = 0;
                    }
                    if (txtbxSurfaceArea.Text != string.Empty)
                    {
                        PermanentStructureobj.SurfaceArea = Convert.ToDecimal(txtbxSurfaceArea.Text);
                    }
                    else
                    {
                        PermanentStructureobj.SurfaceArea = 0;
                    }
                    PermanentStructureobj.DepreciatedValue = Convert.ToDecimal(txtbxDepreciatedValue.Text);
                    PermanentStructureobj.ReplacementValue = Convert.ToDecimal(txtbxReplacementValue.Text);
                    if (txtbxComments.Text != string.Empty)
                    {
                        if (txtbxComments.Text.Length > 1000)
                            PermanentStructureobj.AdditionalComments = txtbxComments.Text.Substring(0, 1000);
                        else
                            PermanentStructureobj.AdditionalComments = txtbxComments.Text;
                    }
                    else
                        PermanentStructureobj.AdditionalComments = "";


                    PermanentStructureobj.StructureType = ddlStructureType.SelectedValue.ToString();
                    PermanentStructureobj.IsDeleted = "False";
                    PermanentStructureobj.CreatedBy = Convert.ToInt32(uID);


                    PermanentStructureBLL PBLLobj = new PermanentStructureBLL();
                    count = PBLLobj.Update(PermanentStructureobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);
                    Clear();
                    BindGrid(true, true);
                    SetUpdateMode(false);

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    BLLobj = null;
                }
            }
        }
        /// <summary>
        /// to change name of button based on condition 
        /// </summary>
        /// <param name="updateMode"></param>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["PERM_STRUCTUREID"] = "0";
            }
        }
        /// <summary>
        /// calls Clear method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        
        /// <summary>
        /// To clear all the fileds
        /// </summary>
        private void Clear()
        {
            perstructIDTextBox.Text = string.Empty;
            ddlBuidingType.ClearSelection();
            otherSpecifyTextBox.Text = string.Empty;
            ddlRoofMaterial.ClearSelection();
            ddlStructureType.ClearSelection();

            if (RbtnSelf.Checked == true)
            {
                RbtnSelf.Checked = false;
            }
            else if (RbtnOther.Checked == true)
            {
                RbtnOther.Checked = false;
            }
            ddlWallsMaterial.ClearSelection();
            ddlFloorMaterial.ClearSelection();
            ddlWindowsMaterial.ClearSelection();
            ddlRoofCondition.ClearSelection();
            ddlWallsCondition.ClearSelection();
            ddlFloorCondition.ClearSelection();
            ddlWindowsCondition.ClearSelection();
            txtbxOther.Text = string.Empty;
            txtbxOccupantOther.Text = string.Empty;
            if (RdbtnSelfoccupied.Checked == true)
            {
                RdbtnSelfoccupied.Checked = false;
            }
            else if (RdbtnOccupantOther.Checked == true)
            {
                RdbtnOccupantOther.Checked = false;
            }

            //lnkViewPhoto.Visible = false;
            txtbxOccupantOther.Text = string.Empty;
            ddlOccupantStatus.ClearSelection();
            txtbxEnterStatus.Text = string.Empty;
            txtbxLength.Text = string.Empty;
            txtbxWidth.Text = string.Empty;
            txtbxNoofrooms.Text = string.Empty;
            txtbxSurfaceArea.Text = string.Empty;
            txtbxDepreciatedValue.Text = string.Empty;
            txtbxReplacementValue.Text = string.Empty;
            txtbxReplacementUplift.Text = string.Empty;
            txtbxComments.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "EnableDisableOtherOwner(0);", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "EnableDisableOtherOccupant(0);", true);
        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdPermanentBuilding.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }

        /// <summary>
        /// Set link attributes to link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPermanentBuilding_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litDepreciatedValue = (Literal)e.Row.FindControl("litDepreciatedValue");
                Literal litReplacementValue = (Literal)e.Row.FindControl("litReplacementValue");

                decimal depreciatedValue = (decimal) DataBinder.Eval(e.Row.DataItem,"DEPRECIATEDVALUE");
                decimal replacementValue = (decimal)DataBinder.Eval(e.Row.DataItem, "REPLACEMENTVALUE");

                litDepreciatedValue.Text = UtilBO.CurrencyFormat(depreciatedValue);
                litReplacementValue.Text = UtilBO.CurrencyFormat(replacementValue);


                Literal PermanentStrucID = (Literal)e.Row.FindControl("PermanentStrucID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = PermanentStrucID.Text.ToString();

                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                }
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }
                if (Session["USER_ID"] != null)
                {
                    userID = Convert.ToInt32(Session["USER_ID"]);
                }
                if (Session["PROJECT_CODE"] != null)
                {
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }
                string PhotoModule = "PAPPB";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            }
        }
    }
}