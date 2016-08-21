using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;


namespace WIS
{
    public partial class Fence : System.Web.UI.Page
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.OtherFixtures;
            OtherFixturesMenu1.HighlightMenu = OtherFixturesMenu.MenuValue.Fence;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.Fence;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Other Fixtures";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                //Master.PageHeader = "Other Fixtures";
                ViewState["PAP_FENCEID"] = 0;

                // To get type of Grave from MST_FENCE
                GetFenceDescription();

                BindGrid(); //Calling the Grid Data
                lengthTextBox.Attributes.Add("onchange", "surfacearea();");
                heightTextBox.Attributes.Add("onchange", "surfacearea();");
                surfaceareaTextBox.Attributes.Add("onKeyDown", "doCheck();");

                popupData();
               // lnkViewPhoto.Visible = false;

                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    lnkFence.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 1].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 2].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 4].Visible = false;
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
                lnkFence.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdFence.Columns[grdFence.Columns.Count - 1].Visible = false;
                grdFence.Columns[grdFence.Columns.Count - 2].Visible = false;
                grdFence.Columns[grdFence.Columns.Count - 4].Visible = false;
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
                    grdFence.Columns[grdFence.Columns.Count - 1].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 2].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 4].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusDamageCrops();
                }
                else
                {
                    lnkFence.Visible = false;
                }
            }
        }
        /// <summary>
        /// checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusFence.Text = "";
            StatusFence.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVFEN");
                lnkFence.Attributes.Add("onclick", paramChangeRequest);
                lnkFence.Visible = true;
            }
            else
            {
                lnkFence.Visible = false;
            }
            #endregion
            getApprrequtStatusDamageCrops();

        }
        /// <summary>
        ///  ChangeRequestStatusFence
        /// </summary>
        public void ChangeRequestStatusFence()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVFEN";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        ///  getApprrequtStatusDamageCrops
        /// </summary>
        public void getApprrequtStatusDamageCrops()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVFEN";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkFence.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 1].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 2].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 4].Visible = false;
                    StatusFence.Visible = true;
                    StatusFence.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkFence.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 1].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 2].Visible = false;
                    grdFence.Columns[grdFence.Columns.Count - 4].Visible = false;
                    StatusFence.Visible = false;
                    StatusFence.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkFence.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdFence.Columns[grdFence.Columns.Count - 1].Visible = true;
                    grdFence.Columns[grdFence.Columns.Count - 2].Visible = true;
                    grdFence.Columns[grdFence.Columns.Count - 4].Visible = true;
                    StatusFence.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// to assign values to dropdownlist
        /// </summary>
        private void GetFenceDescription()
        {
            FenceBLL FenceBLLobj = new FenceBLL();
          
            fenceDropDownList.DataSource = FenceBLLobj.GetFencedescription();
            fenceDropDownList.DataTextField = "Fencedescription";
            fenceDropDownList.DataValueField = "Fenceid";
            fenceDropDownList.DataBind();
            fenceDropDownList.Items.Insert(0, "--Select--");
        }
        /// <summary>
        /// To save details todatabase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            FenceBLL fenceBLLobj = new FenceBLL();
            FenceBO FenceBOobj = null;
            if (Convert.ToInt32(ViewState["PAP_FENCEID"]) > 0)
            {
                // update 
                try
                {
                  string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();

                    FenceBOobj = new FenceBO();

                    FenceBOobj.Pap_fenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);

                    FenceBOobj.Fenceid = Convert.ToInt32(fenceDropDownList.SelectedItem.Value);
                    if (lengthTextBox.Text != string.Empty)
                        FenceBOobj.Fen_dimen_length = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        FenceBOobj.Fen_dimen_length = Convert.ToDecimal(0);

                    if (heightTextBox.Text != string.Empty)
                        FenceBOobj.Fen_dimen_height = Convert.ToDecimal(heightTextBox.Text);
                    else
                        FenceBOobj.Fen_dimen_height = Convert.ToDecimal(0);

                    if (depreciatedvalueTextBox.Text != string.Empty)
                    {
                        FenceBOobj.Depreciatedvalue = Convert.ToDecimal(depreciatedvalueTextBox.Text);
                    }
                    else
                    {
                        FenceBOobj.Depreciatedvalue = 0;
                    }
                   // FenceBOobj.Fen_dimen_length = Convert.ToInt32(lengthTextBox.Text.ToString().Trim());
                   // FenceBOobj.Fen_dimen_height = Convert.ToInt32(widthTextBox.Text.ToString().Trim());
                   // FenceBOobj.Depreciatedvalue = Convert.ToInt32(depreciatedvalueTextBox.Text.ToString().Trim());

                    FenceBOobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    FenceBOobj.HouseholdID = Convert.ToInt32(hhid);
                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        FenceBOobj.Photo = fileBytes;
                    }
                    FenceBLL fenceupdateBLLobj = new FenceBLL();
                    count = fenceupdateBLLobj.EditFence(FenceBOobj);
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
                    fenceBLLobj = null;

                }

            }
            else
            {
                // insert

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    FenceBOobj = new FenceBO();

                    FenceBOobj.Pap_fenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);

                    FenceBOobj.Fenceid = Convert.ToInt32(fenceDropDownList.SelectedValue);

                    if (lengthTextBox.Text != string.Empty)
                        FenceBOobj.Fen_dimen_length = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        FenceBOobj.Fen_dimen_length = Convert.ToDecimal(0);

                    if (heightTextBox.Text != string.Empty)
                        FenceBOobj.Fen_dimen_height = Convert.ToDecimal(heightTextBox.Text);
                    else
                        FenceBOobj.Fen_dimen_height = Convert.ToDecimal(0);

                    if (depreciatedvalueTextBox.Text != string.Empty)
                    {
                        FenceBOobj.Depreciatedvalue = Convert.ToDecimal(depreciatedvalueTextBox.Text);
                    }
                    else
                    {
                        FenceBOobj.Depreciatedvalue = 0;
                    }
                   // FenceBOobj.Fen_dimen_length = Convert.ToInt32(lengthTextBox.Text.ToString().Trim());
                   // FenceBOobj.Fen_dimen_height = Convert.ToInt32(widthTextBox.Text.ToString().Trim());
                   // FenceBOobj.Depreciatedvalue = Convert.ToInt32(depreciatedvalueTextBox.Text.ToString().Trim());

                    FenceBOobj.CreatedBy = Convert.ToInt32(uID);
                    FenceBOobj.HouseholdID = Convert.ToInt32(hhid);

                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        FenceBOobj.Photo = fileBytes;
                    }
                    FenceBLL fencesaveBLLobj = new FenceBLL();
                    count = fencesaveBLLobj.Insert(FenceBOobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);

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
                    fenceBLLobj = null;
                }
            }
            ChangeRequestStatusFence();
            projectFrozen();
           
        }
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFence_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFence.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            FenceBLL fenceBLLobj = new FenceBLL();
            grdFence.DataSource = fenceBLLobj.GetFencedata(Convert.ToInt32(Session["HH_ID"]));
            grdFence.DataBind();
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
                ViewState["PAP_FENCEID"] = e.CommandArgument;
                Getfencedatarow();
                popupData();
                SetUpdateMode(true);
               // lnkViewPhoto.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                
                int Pap_fenceid = Convert.ToInt32(e.CommandArgument);
                FenceBLL fenceBLLobj = new FenceBLL();
                fenceBLLobj.Delete(Pap_fenceid);
                BindGrid();
                SetUpdateMode(false);
                
                clearfields();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }
        public void popupData()
        {
            //Add by Ramu.S For Upload Photo and View Photo;

            string userName = (Session["userName"].ToString());
            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = 0;
            string ProjectCode = string.Empty;
            string perStu = string.Empty;

            if (Session["PROJECT_ID"] != null)
            {
                ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                ProjectCode = Session["PROJECT_CODE"].ToString();
            }

            int HHID = 0;
            if (Session["HH_ID"] != null)
            {
                HHID = Convert.ToInt32(Session["HH_ID"]);
            }

            if (Session["PROJECT_CODE"] != null)
            {
                ProjectCode = Session["PROJECT_CODE"].ToString();
            }

            if (ViewState["PAP_FENCEID"] != null)
            {
                perStu = ViewState["PAP_FENCEID"].ToString();
            }
            string PhotoModule = "PAPFENCE";

            string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
        }
        /// <summary>
        /// Getfencedatarow
        /// </summary>
        private void Getfencedatarow()
        {
            FenceBLL fenceBLLobj = new FenceBLL();
            int Pap_fenceid = 0;

            if (ViewState["PAP_FENCEID"] != null)
                Pap_fenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);
            FenceBO FenceBOobj = new FenceBO();

            FenceBOobj = fenceBLLobj.Getfencedatarow(Pap_fenceid);

            fenceDropDownList.ClearSelection();
            if (fenceDropDownList.Items.FindByValue(FenceBOobj.Fenceid.ToString()) != null)
                fenceDropDownList.Items.FindByValue(FenceBOobj.Fenceid.ToString()).Selected = true;


            lengthTextBox.Text = FenceBOobj.Fen_dimen_length.ToString();
            heightTextBox.Text = FenceBOobj.Fen_dimen_height.ToString();

            surfaceareaTextBox.Text = (FenceBOobj.Fen_dimen_length * FenceBOobj.Fen_dimen_height).ToString();

            depreciatedvalueTextBox.Text = FenceBOobj.Depreciatedvalue.ToString();
        }
        /// <summary>
        /// calls clearfields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
                //lnkViewPhoto.Visible = false;
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
                ViewState["ITEMID"] = "0";
            }
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearfields()
        {
            ViewState["PAP_FENCEID"] = 0;
            fenceDropDownList.ClearSelection();
            fenceDropDownList.Items[0].Selected = true;

            lengthTextBox.Text = string.Empty;
            heightTextBox.Text = string.Empty;
            depreciatedvalueTextBox.Text = string.Empty;
            //lnkViewPhoto.Visible = false;
            surfaceareaTextBox.Text = string.Empty;
        }
        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litDimensions = (Literal)e.Row.FindControl("litDimensions");
                decimal fenceLength = (decimal)DataBinder.Eval(e.Row.DataItem, "Fen_dimen_length");
                decimal fenceHeight = (decimal)DataBinder.Eval(e.Row.DataItem, "Fen_dimen_height");

                litDimensions.Text = (fenceLength * fenceHeight).ToString();

                Literal litDepreciatedValue = (Literal)e.Row.FindControl("litDepreciatedValue");
                // Literal litAmountPaid = (Literal)e.Row.FindControl("litAmountPaid");

                decimal DepreciatedValue = (decimal)DataBinder.Eval(e.Row.DataItem, "DEPRECIATEDVALUE");
                //decimal AmountPaid = (decimal)DataBinder.Eval(e.Row.DataItem, "AMOUNTPAID");

                litDepreciatedValue.Text = UtilBO.CurrencyFormat(DepreciatedValue);
                // litAmountPaid.Text = UtilBO.CurrencyFormat(AmountPaid);

                Literal PAPFenceID = (Literal)e.Row.FindControl("litUserID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = PAPFenceID.Text.ToString();

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
                string PhotoModule = "PAPFENCE";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            }
        }
    }
}