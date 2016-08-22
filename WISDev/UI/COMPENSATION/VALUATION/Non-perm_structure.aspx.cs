using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;


namespace WIS
{
    public partial class Non_perm_structure : System.Web.UI.Page
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
            //ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.NonPermanentBuildings;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.NonPermanentStructureDetails;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }

            if (!Page.IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Non-Permanent Buildings";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                if (RadioButton2.Checked == true)
                {
                    otherTextBox.Enabled = true;
                }


                if (RadioButton4.Checked == true)
                {
                    otherselfoccupantTextBox.Enabled = true;
                }

                GetBuildingType();
                GetCategory();
                GetCondition();
                GetOccupantstatus();

                BindGrid();

                RadioButton1.Attributes.Add("onclick", "EnableDisableOtherOwner(0);");
                RadioButton2.Attributes.Add("onclick", "EnableDisableOtherOwner(1);");
                RadioButton3.Attributes.Add("onclick", "EnableDisableOtherOccupant(0);");
                RadioButton4.Attributes.Add("onclick", "EnableDisableOtherOccupant(1);");
                occupantstatusDropDownList.Attributes.Add("onchange", "EnableDisableOccupantStatus(this)");

                lengthTextBox.Attributes.Add("onchange", "surfacearea();");
                widthTextBox.Attributes.Add("onchange", "surfacearea();");
                surfaceareaTextBox.Attributes.Add("onKeyDown", "doCheck();");

                popupData();
                //lnkViewPhoto.Visible = false;                

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdNPS.Columns[4].Visible = false;
                    grdNPS.Columns[5].Visible = false;
                }

                projectFrozen();
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
                lnkNonPermStr.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdNPS.Columns[4].Visible = false;
                grdNPS.Columns[5].Visible = false;
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
        /// <summary>
        ///  checks whether project is Frozen
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusNonPermStr();
                }
                else
                {
                    lnkNonPermStr.Visible = false;
                }
            }
        }
        /// <summary>
        /// checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusNonPermStr.Text = "";
            StatusNonPermStr.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVNPS");
                lnkNonPermStr.Attributes.Add("onclick", paramChangeRequest);
                lnkNonPermStr.Visible = true;
            }
            else
            {
                lnkNonPermStr.Visible = false;
            }
            #endregion
            getApprrequtStatusNonPermStr();

        }
        /// <summary>
        /// ChangeRequestStatusNonPermStr
        /// </summary>
        public void ChangeRequestStatusNonPermStr()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVNPS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// getApprrequtStatusNonPermStr
        /// </summary>
        public void getApprrequtStatusNonPermStr()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVNPS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkNonPermStr.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusNonPermStr.Visible = true;
                    StatusNonPermStr.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkNonPermStr.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusNonPermStr.Visible = false;
                    StatusNonPermStr.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkNonPermStr.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    StatusNonPermStr.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            Non_perm_structureBLL BLLobj = new Non_perm_structureBLL();
            grdNPS.DataSource = BLLobj.GetNPS(Convert.ToInt32(Session["HH_ID"]));
            grdNPS.DataBind();
        }
        /// <summary>
        /// to assign values to dropdownlist
        /// </summary>
        private void GetOccupantstatus()
        {
            PermanentStructureBLL BLLobj = new PermanentStructureBLL();
            occupantstatusDropDownList.DataSource = BLLobj.GetOccupantstatus();
            occupantstatusDropDownList.DataTextField = "OCCUPANTSTATUS";
            occupantstatusDropDownList.DataValueField = "OCCUPANTSTATUSID";
            occupantstatusDropDownList.DataBind();
            occupantstatusDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        /// <summary>
        /// to assign values to dropdownlist
        /// </summary>
        private void GetCondition()
        {
            StructureConditionBLL BLLobj = new StructureConditionBLL();

            conditionDropDownList.DataSource = BLLobj.GetStructureCondition();
            conditionDropDownList.DataTextField = "StructureConditionName";
            conditionDropDownList.DataValueField = "StructureConditionID";
            conditionDropDownList.DataBind();
            conditionDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        /// <summary>
        /// to assign values to dropdownlist
        /// </summary>
        private void GetCategory()
        {
            StructureCategoryBLL BLLobj = new StructureCategoryBLL();

            categoryDropDownList.DataSource = BLLobj.GetStructureCategory();
            categoryDropDownList.DataTextField = "StructureCategoryName";
            categoryDropDownList.DataValueField = "StructureCategoryID";
            categoryDropDownList.DataBind();
            categoryDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        /// <summary>
        /// to assign values to dropdownlist
        /// </summary>
        private void GetBuildingType()
        {
            StructureTypeBLL BLLobj = new StructureTypeBLL();

            buildingtypeDropDownList.DataSource = BLLobj.GetStructureType();
            buildingtypeDropDownList.DataTextField = "StructureTypeName";
            buildingtypeDropDownList.DataValueField = "StructureTypeID";
            buildingtypeDropDownList.DataBind();
            buildingtypeDropDownList.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["NONPERM_STRUCTUREID"] = e.CommandArgument;
               // lnkViewPhoto.Visible = true;
                popupData();
                GetNPS1();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string NonPermanentStructureID = e.CommandArgument.ToString();
                Non_perm_structureBLL BLLobj = new Non_perm_structureBLL();
                BLLobj.Delete(NonPermanentStructureID);
                SetUpdateMode(false);
                BindGrid();
                clearfields();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }

        public void popupData()
        {
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

            //if (ViewState["NONPERM_STRUCTUREID"] != null)
            //{
            //    perStu = ViewState["NONPERM_STRUCTUREID"].ToString();
            //}
            //string PhotoModule = "PAPNPB";

            //string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
        }
        /// <summary>
        /// To fetch details and assign to textbox
        /// </summary>
        private void GetNPS1()
        {
            Non_perm_structureBLL BLLobj = new Non_perm_structureBLL();
            int NonPermanentStructureID = 0;

            if (ViewState["NONPERM_STRUCTUREID"] != null)
                NonPermanentStructureID = Convert.ToInt32(ViewState["NONPERM_STRUCTUREID"]);

            NonPermanentStructureBO BOobj = new NonPermanentStructureBO();
            BOobj = BLLobj.GetNPSId(NonPermanentStructureID);

            IDTextBox1.Text = BOobj.NonPermanentStructureID.ToString();

            noofroomsTextBox.Text = BOobj.NoOfRooms.ToString();
            lengthTextBox.Text = BOobj.DimensionLength.ToString();
            widthTextBox.Text = BOobj.DimensionWidth.ToString();
            surfaceareaTextBox.Text = BOobj.SurfaceArea.ToString();
            otherspecifyTextBox.Text = BOobj.OtherStructureType.ToString();

            otherTextBox.Enabled = false;
            if (BOobj.Owner == "Self")
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton2.Checked = true;
                otherTextBox.Enabled = true;
                otherTextBox.Text = BOobj.OwnerName.ToString();
            }


            otherselfoccupantTextBox.Enabled = false;
            if (BOobj.Occupant == "Self")
            {
                RadioButton3.Checked = true;
            }
            else
            {
                RadioButton4.Checked = true;
                otherselfoccupantTextBox.Enabled = true;
                otherselfoccupantTextBox.Text = BOobj.OtherOccupantName.ToString();
            }


            buildingtypeDropDownList.ClearSelection();
            if (buildingtypeDropDownList.Items.FindByValue(BOobj.StructureTypeID.ToString()) != null)
                buildingtypeDropDownList.Items.FindByValue(BOobj.StructureTypeID.ToString()).Selected = true;

            occupantstatusDropDownList.ClearSelection();
            if (occupantstatusDropDownList.Items.FindByValue(BOobj.OccupantStatusID.ToString()) != null)
                occupantstatusDropDownList.Items.FindByValue(BOobj.OccupantStatusID.ToString()).Selected = true;


            categoryDropDownList.ClearSelection();
            if (categoryDropDownList.Items.FindByValue(BOobj.CategoryID.ToString()) != null)
                categoryDropDownList.Items.FindByValue(BOobj.CategoryID.ToString()).Selected = true;

            conditionDropDownList.ClearSelection();
            if (conditionDropDownList.Items.FindByValue(BOobj.StructureConditionID.ToString()) != null)
                conditionDropDownList.Items.FindByValue(BOobj.StructureConditionID.ToString()).Selected = true;
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int count = 0;
            Non_perm_structureBLL BLLobj = new Non_perm_structureBLL();
            NonPermanentStructureBO BOobj = null;

            if (Convert.ToInt32(ViewState["NONPERM_STRUCTUREID"]) > 0)
            {
                // update 

                string uID = Session["USER_ID"].ToString();
                string hhid = Session["HH_ID"].ToString();
                try
                {
                    if (photoFileUpload.HasFile)
                    {
                        NonPermanentStructureBO BOobj1 = new NonPermanentStructureBO();
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        BOobj1.NonPermanentStructureID = Convert.ToInt32(IDTextBox1.Text);
                        BOobj1.HouseholdID = Convert.ToInt32(hhid);
                        BOobj1.UpdatedBy = Convert.ToInt32(uID);
                        BOobj1.Photo = fileBytes;


                        Non_perm_structureBLL pBLLobj = new Non_perm_structureBLL();
                        count = pBLLobj.Updatephoto(BOobj1);
                    }

                    BOobj = new NonPermanentStructureBO();

                    BOobj.NonPermanentStructureID = Convert.ToInt32(ViewState["NONPERM_STRUCTUREID"]);

                    BOobj.StructureTypeID = Convert.ToInt32(buildingtypeDropDownList.SelectedValue);
                    BOobj.CategoryID = Convert.ToInt32(categoryDropDownList.SelectedValue);
                    BOobj.StructureConditionID = Convert.ToInt32(conditionDropDownList.SelectedValue);
                    BOobj.OccupantStatusID = Convert.ToInt32(occupantstatusDropDownList.SelectedValue.ToString());
                    if (noofroomsTextBox.Text != string.Empty)
                    {
                        BOobj.NoOfRooms = Convert.ToInt32(noofroomsTextBox.Text);
                    }
                    else
                    {
                        BOobj.NoOfRooms = 0;
                    }
                    //BOobj.NoOfRooms = Convert.ToInt32(noofroomsTextBox.Text.ToString().Trim());
                    if (surfaceareaTextBox.Text != string.Empty)
                    {
                        BOobj.SurfaceArea = Convert.ToDecimal(surfaceareaTextBox.Text);
                    }
                    else
                    {
                        BOobj.SurfaceArea = 0;
                    }
                    //BOobj.SurfaceArea = Convert.ToInt32(surfaceareaTextBox.Text.ToString().Trim());
                    BOobj.OtherStructureType = otherspecifyTextBox.Text.ToString().Trim();
                    BOobj.DimensionLength = Convert.ToInt32(lengthTextBox.Text.ToString().Trim());
                    BOobj.DimensionWidth = Convert.ToInt32(widthTextBox.Text.ToString().Trim());
                    BOobj.OtherOccupantStatus = occupantstatusTextBox.Text.ToString().Trim();
                    BOobj.CreatedBy = Convert.ToInt32(uID); ;


                    if (RadioButton1.Checked == true)
                    {

                        BOobj.Owner = "Self";
                        //RadioButton2.Checked = false;
                    }
                    else if (RadioButton2.Checked == true)
                    {
                        BOobj.Owner = "Other";
                        BOobj.OwnerName = otherTextBox.Text;
                    }

                    if (RadioButton3.Checked == true)
                    {

                        BOobj.Occupant = "Self";
                        //RadioButton2.Checked = false;
                    }
                    else if (RadioButton4.Checked == true)
                    {
                        BOobj.Occupant = "Other";
                        BOobj.OtherOccupantName = otherselfoccupantTextBox.Text.ToString();
                        //RadioButton1.Checked = false;
                    }
                    //if (photoFileUpload.HasFile)
                    //{
                    //    byte[] fileBytes = photoFileUpload.FileBytes;

                    //    if (fileBytes != null)
                    //    {
                    //        BOobj.Photo = fileBytes;
                    //    }

                    //}

                    BLLobj = new Non_perm_structureBLL();
                    count = BLLobj.EditNPS(BOobj);
                    projectFrozen();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);
                    clearfields();
                    BindGrid();
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
            else
            {
                // insert
                string uID = Session["USER_ID"].ToString();
                string hhid = Session["HH_ID"].ToString();
                //try
                //{                     
                BOobj = new NonPermanentStructureBO();

                BOobj.StructureTypeID = Convert.ToInt32(buildingtypeDropDownList.SelectedValue.ToString());
                if (otherspecifyTextBox.Text != string.Empty)
                {
                    BOobj.OtherStructureType = otherspecifyTextBox.Text;
                }
                else
                {
                    BOobj.OtherStructureType = "";
                }

                BOobj.CategoryID = Convert.ToInt32(categoryDropDownList.SelectedValue.ToString());
                BOobj.StructureConditionID = Convert.ToInt32(conditionDropDownList.SelectedValue.ToString());

                BOobj.OccupantStatusID = Convert.ToInt32(occupantstatusDropDownList.SelectedValue.ToString());
                BOobj.OtherOccupantStatus = occupantstatusTextBox.Text.ToString().Trim();
                if (lengthTextBox.Text != string.Empty)
                    BOobj.DimensionLength = Convert.ToDecimal(lengthTextBox.Text);
                else
                    BOobj.DimensionLength = Convert.ToDecimal(0);

                if (widthTextBox.Text != string.Empty)
                    BOobj.DimensionWidth = Convert.ToDecimal(widthTextBox.Text);
                else
                    BOobj.DimensionWidth = Convert.ToDecimal(0);

                if (noofroomsTextBox.Text != string.Empty)
                {
                    BOobj.NoOfRooms = Convert.ToInt32(noofroomsTextBox.Text);
                }
                else
                {
                    BOobj.NoOfRooms = 0;
                }
                if (surfaceareaTextBox.Text != string.Empty)
                {
                    BOobj.SurfaceArea = Convert.ToDecimal(surfaceareaTextBox.Text);
                }
                else
                {
                    BOobj.SurfaceArea = 0;
                }

                if (RadioButton1.Checked == true || (RadioButton1.Checked == false && RadioButton2.Checked == false))
                {
                    BOobj.Owner = "Self";
                }
                else if (RadioButton2.Checked == true)
                {
                    BOobj.Owner = "Other";
                    BOobj.OwnerName = otherTextBox.Text.ToString();
                }

                if (RadioButton3.Checked == true || (RadioButton3.Checked == false && RadioButton4.Checked == false))
                {

                    BOobj.Occupant = "Self";
                }
                else if (RadioButton4.Checked == true)
                {
                    BOobj.Occupant = "Other";
                    BOobj.OtherOccupantName = otherselfoccupantTextBox.Text.ToString();
                }

                if (photoFileUpload.HasFile)
                {
                    byte[] fileBytes = photoFileUpload.FileBytes;

                    if (fileBytes != null)
                    {
                        BOobj.Photo = fileBytes;
                    }
                }

                if (photoFileUpload.HasFile)
                {
                    byte[] fileBytes = photoFileUpload.FileBytes;

                    if (fileBytes != null)
                    {
                        BOobj.Photo = fileBytes;
                    }
                }

                BOobj.CreatedBy = Convert.ToInt32(uID);
                BOobj.HouseholdID = Convert.ToInt32(hhid);

                Non_perm_structureBLL structureBLLobj = new Non_perm_structureBLL();
                count = structureBLLobj.Insert(BOobj);
                clearfields();
                BindGrid();
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);

                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}

                //finally
                //{
                //    BLLobj = null;
                //}
            }
            ChangeRequestStatusNonPermStr();
            projectFrozen();
            
        }
        /// <summary>
        /// calls clearfields method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }

        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
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
                ViewState["NONPERM_STRUCTUREID"] = "0";
            }
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearfields()
        {
            buildingtypeDropDownList.ClearSelection();
            categoryDropDownList.ClearSelection();
            occupantstatusDropDownList.ClearSelection();
            conditionDropDownList.ClearSelection();
            otherTextBox.Text = string.Empty;
            noofroomsTextBox.Text = string.Empty;
            otherspecifyTextBox.Text = string.Empty;
            otherselfoccupantTextBox.Text = string.Empty;
            if (RadioButton3.Checked == true)
            {
                RadioButton3.Checked = false;
            }
            else if (RadioButton4.Checked == true)
            {
                RadioButton4.Checked = false;
            }
            occupantstatusTextBox.Text = string.Empty;
            lengthTextBox.Text = string.Empty;
            widthTextBox.Text = string.Empty;
            surfaceareaTextBox.Text = string.Empty;
            if (RadioButton1.Checked == true)
            {
                RadioButton1.Checked = false;
            }
            else if (RadioButton2.Checked == true)
            {
                RadioButton2.Checked = false;
            }
            otherTextBox.Enabled = false;
            otherselfoccupantTextBox.Enabled = false;
           // lnkViewPhoto.Visible = false;

        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdNPS.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid();
        }
        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdNPS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal NONPERMSTRUID = (Literal)e.Row.FindControl("litUserID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = NONPERMSTRUID.Text.ToString();

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
                string PhotoModule = "PAPNPB";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            }
        }
    }
}
