using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class OtherFixtures : System.Web.UI.Page
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
            OtherFixturesMenu1.HighlightMenu = OtherFixturesMenu.MenuValue.OtherFixture;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.OtherFixtures;

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
                //GetFenceDescription();

                BindGrid(); //Calling the Grid Data
                lengthTextBox.Attributes.Add("onchange", "surfacearea();");
                heightTextBox.Attributes.Add("onchange", "surfacearea();");
                surfaceareaTextBox.Attributes.Add("onKeyDown", "doCheck();");

               // popupData();
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
        /// Set link attributes to Branch link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFence_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litDimensions = (Literal)e.Row.FindControl("litDimensions");
                decimal fenceLength = (decimal)DataBinder.Eval(e.Row.DataItem, "DIMEN_LENGTH");
                decimal fenceHeight = (decimal)DataBinder.Eval(e.Row.DataItem, "DIMEN_WIDTH");

                litDimensions.Text = (fenceLength * fenceHeight).ToString();

                Literal litDepreciatedValue = (Literal)e.Row.FindControl("litDepreciatedValue");
                // Literal litAmountPaid = (Literal)e.Row.FindControl("litAmountPaid");

                decimal DepreciatedValue = (decimal)DataBinder.Eval(e.Row.DataItem, "DEPRECIATEDVALUE");
                //decimal AmountPaid = (decimal)DataBinder.Eval(e.Row.DataItem, "AMOUNTPAID");

                litDepreciatedValue.Text = UtilBO.CurrencyFormat(DepreciatedValue);
                // litAmountPaid.Text = UtilBO.CurrencyFormat(AmountPaid);


                Literal PAPOtherFixID = (Literal)e.Row.FindControl("litUserID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = PAPOtherFixID.Text.ToString();

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
                string PhotoModule = "PAPOHFIX";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            }
        }
        
        /// <summary>
        /// To clear the fields
        /// </summary>
        private void clearfields()
        {
            ViewState["PAP_FENCEID"] = 0;
            fixtureDescription.Text = "";    

            lengthTextBox.Text = string.Empty;
            heightTextBox.Text = string.Empty;
            depreciatedvalueTextBox.Text = string.Empty;
            //lnkViewPhoto.Visible = false;
            surfaceareaTextBox.Text = string.Empty;
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
                //lnkViewPhoto.Visible = false;
            }
        }
        /// <summary>
        /// used to fetch details and assign to textbox
        /// </summary>
        private void Getfencedatarow()
        {
            OtherFixturesBLL fenceBLLobj = new OtherFixturesBLL();
            int Pap_fenceid = 0;

            if (ViewState["PAP_FENCEID"] != null)
                Pap_fenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);
            OtherFenceBO FenceBOobj = new OtherFenceBO();

            FenceBOobj = fenceBLLobj.Getfencedatarow(Pap_fenceid);
            //if( fixtureDescription.Text != "")
            fixtureDescription.Text = FenceBOobj.Otherfencedescription.ToString();
            lengthTextBox.Text = FenceBOobj.DIMEN_LENGTH.ToString();
            heightTextBox.Text = FenceBOobj.DIMEN_WIDTH.ToString();
            surfaceareaTextBox.Text = (FenceBOobj.DIMEN_LENGTH * FenceBOobj.DIMEN_WIDTH).ToString();
            depreciatedvalueTextBox.Text = FenceBOobj.Depreciatedvalue.ToString();
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
                //popupData();
                SetUpdateMode(true);
                //lnkViewPhoto.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {

                int Pap_fenceid = Convert.ToInt32(e.CommandArgument);
                OtherFixturesBLL fenceBLLobj = new OtherFixturesBLL();
                fenceBLLobj.Delete(Pap_fenceid);
                BindGrid();
                SetUpdateMode(false);

                clearfields();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            OtherFixturesBLL fenceBLLobj = new OtherFixturesBLL();
            grdFence.DataSource = fenceBLLobj.GetFencedata(Convert.ToInt32(Session["HH_ID"]));
            grdFence.DataBind();
        }

        /// <summary>
        /// Change index of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFence_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFence.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// To save data into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            OtherFixturesBLL fenceBLLobj = new OtherFixturesBLL();
            OtherFenceBO FenceBOobj = null;
            if (Convert.ToInt32(ViewState["PAP_FENCEID"]) > 0)
            {
                // update 
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();

                    FenceBOobj = new OtherFenceBO();

                    FenceBOobj.Pap_otherfenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);

                    //FenceBOobj.Fenceid = Convert.ToInt32(fenceDropDownList.SelectedItem.Value);
                    if(fixtureDescription.Text.Trim() !=null)
                        FenceBOobj.Otherfencedescription = fixtureDescription.Text;
                    if (lengthTextBox.Text != string.Empty)
                        FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(0);

                    if (heightTextBox.Text != string.Empty)
                        FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(heightTextBox.Text);
                    else
                        FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(0);

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
                    FenceBOobj.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                    FenceBOobj.HouseholdID = Convert.ToInt32(hhid);
                    //if (photoFileUpload.HasFile)
                    //{
                    //    byte[] fileBytes = photoFileUpload.FileBytes;
                    //    FenceBOobj.Photo = fileBytes;
                    //}
                    OtherFixturesBLL fenceupdateBLLobj = new OtherFixturesBLL();
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
                    FenceBOobj = new OtherFenceBO();

                    FenceBOobj.Pap_otherfenceid = Convert.ToInt32(ViewState["PAP_FENCEID"]);

                   // FenceBOobj.Fenceid = Convert.ToInt32(fenceDropDownList.SelectedValue);
                    if (fixtureDescription.Text.Trim() != null)
                        FenceBOobj.Otherfencedescription = fixtureDescription.Text;
                    if (lengthTextBox.Text != string.Empty)
                        FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        FenceBOobj.DIMEN_LENGTH = Convert.ToDecimal(0);

                    if (heightTextBox.Text != string.Empty)
                        FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(heightTextBox.Text);
                    else
                        FenceBOobj.DIMEN_WIDTH = Convert.ToDecimal(0);

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

                    //if (photoFileUpload.HasFile)
                    //{
                    //    byte[] fileBytes = photoFileUpload.FileBytes;
                    //    FenceBOobj.Photo = fileBytes;
                    //}
                    OtherFixturesBLL fencesaveBLLobj = new OtherFixturesBLL();
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

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVOFX");
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
        /// ChangeRequestStatusFence
        /// </summary>
        public void ChangeRequestStatusFence()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVOFX";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// getApprrequtStatusDamageCrops
        /// </summary>
        public void getApprrequtStatusDamageCrops()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVOFX";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

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

    }
}