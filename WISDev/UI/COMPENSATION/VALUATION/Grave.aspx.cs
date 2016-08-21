using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class other_fixtures : System.Web.UI.Page
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
            OtherFixturesMenu1.HighlightMenu = OtherFixturesMenu.MenuValue.Grave;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.Grave;

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

                ViewState["PAP_GRAVEID"] = 0;

                // To get type of Grave from MST_GRAVEFINISH
                GetGraveFinish();
                BindGrid();

                lengthTextBox.Attributes.Add("onchange", "surfacearea();");
                widthTextBox.Attributes.Add("onchange", "surfacearea();");
                surfaceareaTextBox.Attributes.Add("onKeyDown", "doCheck();");

                popupData();
                //lnkViewPhoto.Visible = false;

                lengthTextBox.Attributes.Add("onblur", "setDirtyText();");
                widthTextBox.Attributes.Add("onblur", "setDirtyText();");
                depreciatedvalueTextBox.Attributes.Add("onchange", "setDirtyText();");

                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    lnkGrave.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = false;
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
                lnkGrave.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = false;
                grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = false;
                grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = false;
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
                    grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusGrave();                    
                }
                else
                {
                    lnkGrave.Visible = false;
                }
            }
        }
        /// <summary>
        /// checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusGrave.Text = "";
            StatusGrave.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HV-GR");
                lnkGrave.Attributes.Add("onclick", paramChangeRequest);
                lnkGrave.Visible = true;
            }
            else
            {
                lnkGrave.Visible = false;
            }
            #endregion
            getApprrequtStatusGrave();

        }
        /// <summary>
        /// ChangeRequestStatusGrave
        /// </summary>

        public void ChangeRequestStatusGrave()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HV-GR";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// getApprrequtStatusGrave
        /// </summary>
        public void getApprrequtStatusGrave()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HV-GR";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkGrave.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = false;
                    StatusGrave.Visible = true;
                    StatusGrave.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkGrave.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = false;
                    grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = false;
                    StatusGrave.Visible = false;
                    StatusGrave.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkGrave.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdGrave.Columns[grdGrave.Columns.Count - 1].Visible = true;
                    grdGrave.Columns[grdGrave.Columns.Count - 2].Visible = true;
                    grdGrave.Columns[grdGrave.Columns.Count - 4].Visible = true;
                    StatusGrave.Visible = false;
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
            GraveBLL gravesaveBLLobj = new GraveBLL();
            grdGrave.DataSource = gravesaveBLLobj.GetGravedata(Convert.ToInt32(Session["HH_ID"]));
            grdGrave.DataBind();
        }

        /// <summary>
        /// fetch values and assign to dropdownlist
        /// </summary>
        private void GetGraveFinish()
        {
            GraveBLL GraveBLLobj = new GraveBLL();

            gravefinishDropDownList.DataSource = GraveBLLobj.GetGraveFinish();
            gravefinishDropDownList.DataTextField = "Grv_finishtype";
            gravefinishDropDownList.DataValueField = "Grv_finishid";
            gravefinishDropDownList.DataBind();
            gravefinishDropDownList.Items.Insert(0, "--Select--");
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
                ViewState["PAP_GRAVEID"] = e.CommandArgument;
                popupData();
                //lnkViewPhoto.Visible = true;
                Getdatarow();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {

                int Pap_graveid = Convert.ToInt32(e.CommandArgument);
                GraveBLL gravesaveBLLobj = new GraveBLL();
                gravesaveBLLobj.Delete(Pap_graveid);
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
            else
            {
                HHID = 0;
            }
            if (Session["PROJECT_CODE"] != null)
            {
                ProjectCode = Session["PROJECT_CODE"].ToString();
            }
            else
            {
                ProjectCode = "";
            }
            if (ViewState["PAP_GRAVEID"] != null)
            {
                perStu = ViewState["PAP_GRAVEID"].ToString();
            }
            string PhotoModule = "PAPGRAVE";

            string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
        }

        /// <summary>
        /// to fetch details and assign to textbox
        /// </summary>
        private void Getdatarow()
        {
            GraveBLL gravesaveBLLobj = new GraveBLL();
            int Pap_graveid = 0;

            if (ViewState["PAP_GRAVEID"] != null)
                Pap_graveid = Convert.ToInt32(ViewState["PAP_GRAVEID"]);
            GraveBO GraveBOobj = new GraveBO();
            GraveBOobj = gravesaveBLLobj.Getdatarow(Pap_graveid);

            gravefinishDropDownList.ClearSelection();
            if (gravefinishDropDownList.Items.FindByValue(GraveBOobj.Grv_finishid.ToString()) != null)
                gravefinishDropDownList.Items.FindByValue(GraveBOobj.Grv_finishid.ToString()).Selected = true;

            lengthTextBox.Text = Convert.ToString(GraveBOobj.Grv_dimen_length);
            widthTextBox.Text = Convert.ToString(GraveBOobj.Grv_dimen_width);
            surfaceareaTextBox.Text = (GraveBOobj.Grv_dimen_length * GraveBOobj.Grv_dimen_width).ToString();

            depreciatedvalueTextBox.Text = GraveBOobj.Depreciatedvalue.ToString();
            otherfinishTextBox.Text = GraveBOobj.Othergravefinish.ToString();

        }

        /// <summary>
        /// to save data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            GraveBLL gravesaveBLLobj = new GraveBLL();
            GraveBO GraveBOobj = null;
            if (Convert.ToInt32(ViewState["PAP_GRAVEID"]) > 0)
            {
                // update 
                try
                {
                    // string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    GraveBOobj = new GraveBO();

                    GraveBOobj.Pap_graveid = Convert.ToInt32(ViewState["PAP_GRAVEID"]);

                    GraveBOobj.Grv_finishid = Convert.ToInt32(gravefinishDropDownList.SelectedValue);
                    if (otherfinishTextBox.Text != string.Empty)
                    {
                        GraveBOobj.Othergravefinish = otherfinishTextBox.Text;
                    }
                    else
                    {
                        GraveBOobj.Othergravefinish = "";
                    }

                    if (lengthTextBox.Text != string.Empty)
                        GraveBOobj.Grv_dimen_length = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        GraveBOobj.Grv_dimen_length = Convert.ToDecimal(0);

                    if (widthTextBox.Text != string.Empty)
                        GraveBOobj.Grv_dimen_width = Convert.ToDecimal(widthTextBox.Text);
                    else
                        GraveBOobj.Grv_dimen_width = Convert.ToDecimal(0);

                    if (depreciatedvalueTextBox.Text != string.Empty)
                    {
                        GraveBOobj.Depreciatedvalue = Convert.ToDecimal(depreciatedvalueTextBox.Text);
                    }
                    else
                    {
                        GraveBOobj.Depreciatedvalue = 0;
                    }

                    // GraveBOobj.Othergravefinish = otherfinishTextBox.Text.ToString().Trim();

                    //  GraveBOobj.Grv_dimen_length = Convert.ToInt32(lengthTextBox.Text.ToString().Trim());
                    //  GraveBOobj.Grv_dimen_width = Convert.ToInt32(widthTextBox.Text.ToString().Trim());
                    SetUpdateMode(false);
                    // GraveBOobj.Depreciatedvalue = Convert.ToInt32(depreciatedvalueTextBox.Text.ToString().Trim());

                    GraveBOobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    GraveBOobj.HouseholdID = Convert.ToInt32(hhid);
                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        GraveBOobj.Photo = fileBytes;
                    }
                    GraveBLL gravesaveBLL = new GraveBLL();
                    count = gravesaveBLLobj.EditGRAVE(GraveBOobj);
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
                    gravesaveBLLobj = null;

                }

            }
            else
            {
                // insert

                try
                {
                    // string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    GraveBOobj = new GraveBO();
                    GraveBOobj.Grv_finishid = Convert.ToInt32(gravefinishDropDownList.SelectedValue);
                    // GraveBOobj.Othergravefinish = otherfinishTextBox.Text.ToString().Trim();
                    if (otherfinishTextBox.Text != string.Empty)
                    {
                        GraveBOobj.Othergravefinish = otherfinishTextBox.Text;
                    }
                    else
                    {
                        GraveBOobj.Othergravefinish = "";
                    }

                    if (lengthTextBox.Text != string.Empty)
                        GraveBOobj.Grv_dimen_length = Convert.ToDecimal(lengthTextBox.Text);
                    else
                        GraveBOobj.Grv_dimen_length = Convert.ToDecimal(0);

                    if (widthTextBox.Text != string.Empty)
                        GraveBOobj.Grv_dimen_width = Convert.ToDecimal(widthTextBox.Text);
                    else
                        GraveBOobj.Grv_dimen_width = Convert.ToDecimal(0);

                    if (depreciatedvalueTextBox.Text != string.Empty)
                    {
                        GraveBOobj.Depreciatedvalue = Convert.ToDecimal(depreciatedvalueTextBox.Text);
                    }
                    else
                    {
                        GraveBOobj.Depreciatedvalue = 0;
                    }

                    //GraveBOobj.Grv_dimen_length = Convert.ToInt32(lengthTextBox.Text.ToString().Trim());
                    // GraveBOobj.Grv_dimen_width = Convert.ToInt32(widthTextBox.Text.ToString().Trim());
                    // GraveBOobj.Depreciatedvalue = Convert.ToInt32(depreciatedvalueTextBox.Text.ToString().Trim());
                    GraveBOobj.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    GraveBOobj.HouseholdID = Convert.ToInt32(hhid);
                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        GraveBOobj.Photo = fileBytes;
                    }

                    GraveBLL graveBLLobj = new GraveBLL();
                    count = graveBLLobj.Insert(GraveBOobj);
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
                    gravesaveBLLobj = null;
                }
            }
            ChangeRequestStatusGrave();
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
            ViewState["PAP_GRAVEID"] = 0;
            gravefinishDropDownList.ClearSelection();
            gravefinishDropDownList.Items[0].Selected = true;
            otherfinishTextBox.Text = string.Empty;
            lengthTextBox.Text = string.Empty;
            widthTextBox.Text = string.Empty;
            depreciatedvalueTextBox.Text = string.Empty;
            surfaceareaTextBox.Text = string.Empty;
            //lnkViewPhoto.Visible = false;
        }

        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdGrave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGrave.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdGrave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litDimensions = (Literal)e.Row.FindControl("litDimensions");
                decimal fenceLength = (decimal)DataBinder.Eval(e.Row.DataItem, "Grv_dimen_length");
                decimal fenceWidth = (decimal)DataBinder.Eval(e.Row.DataItem, "Grv_dimen_width");

                litDimensions.Text = (fenceLength * fenceWidth).ToString();

                Literal litDepreciatedValue = (Literal)e.Row.FindControl("litDepreciatedValue");
               // Literal litAmountPaid = (Literal)e.Row.FindControl("litAmountPaid");

                decimal DepreciatedValue = (decimal)DataBinder.Eval(e.Row.DataItem, "DEPRECIATEDVALUE");
                //decimal AmountPaid = (decimal)DataBinder.Eval(e.Row.DataItem, "AMOUNTPAID");

                litDepreciatedValue.Text = UtilBO.CurrencyFormat(DepreciatedValue);
               // litAmountPaid.Text = UtilBO.CurrencyFormat(AmountPaid);

                Literal GraveID = (Literal)e.Row.FindControl("litUserID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = GraveID.Text.ToString();

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
                string PhotoModule = "PAPGRAVE";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);

            }
        }
    }
}
