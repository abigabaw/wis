using System;
using System.Web.UI;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.WebControls;
using System.Text;

namespace WIS
{
    public partial class Concerns : System.Web.UI.Page
    {
        /// <summary>
        /// to set the PageHeader, call BindGrid() to bind the data from the database to the gridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.Concern;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.ConcernDetails;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS", CreateStartupScript());
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
                Master.PageHeader = "BIP - Socio-Economic - Concerns";
                ViewState["PapConcernID"] = 0;
                BindGrid();
                GetConcer(false);
                PAPConcernsIDTextBox.Text = "0";

                //PAPConcernsDropDownList.Attributes.Add("onchange", "EnableDisableOtherConcern(this);");
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdSocioConcerns.Columns[3].Visible = false;
                    grdSocioConcerns.Columns[4].Visible = false;
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
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdSocioConcerns.Columns[3].Visible = false;
                grdSocioConcerns.Columns[4].Visible = false;
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

        #region Frozen / Approval / Decline / Pending
        /// <summary>
        /// to Check the Status of the  Frozen / Approval / Decline / Pending
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdSocioConcerns.Columns[3].Visible = false;
                    grdSocioConcerns.Columns[4].Visible = false;
                    checkApprovalExitOrNot();
                }
            }
        }
        /// <summary>
        /// to check the approvar exist or not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusSocailConcerns.Text = "";
            StatusSocailConcerns.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-SC");
                lnkSocailConcerns.Attributes.Add("onclick", paramChangeRequest);
                lnkSocailConcerns.Visible = true;
            }
            else
            {
                lnkSocailConcerns.Visible = false;
            }
            #endregion
            getApprrequtStatusSocailConcerns();
        }
        /// <summary>
        /// to get the status of the request for the social concern
        /// </summary>
        public void ChangeRequestStatusSocailConcerns()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SC";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the status of the Social concern
        /// </summary>
        public void getApprrequtStatusSocailConcerns()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-SC";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkSocailConcerns.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusSocailConcerns.Visible = true;
                    StatusSocailConcerns.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkSocailConcerns.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusSocailConcerns.Visible = false;
                    StatusSocailConcerns.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkSocailConcerns.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdSocioConcerns.Columns[3].Visible = true;
                    grdSocioConcerns.Columns[4].Visible = true;
                    StatusSocailConcerns.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// to get the concern details
        /// </summary>
        /// <param name="lastItemSelected"></param>

        private void GetConcer(bool lastItemSelected)
        {
            ConcernBLL objConcer = new ConcernBLL();
            ConcernList ConcernList = new ConcernList();

            ConcernList = objConcer.GetConcern();
            PAPConcernsDropDownList.Items.Clear();

            PAPConcernsDropDownList.DataSource = ConcernList;
            PAPConcernsDropDownList.DataTextField = "ConcernName";
            PAPConcernsDropDownList.DataValueField = "ConcernID";
            PAPConcernsDropDownList.DataBind();
            PAPConcernsDropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            PAPConcernsDropDownList.SelectedIndex = 0;

            if (PAPConcernsDropDownList.Items.FindByText("Other") != null)
            {
                ListItem LastListItem = new ListItem(PAPConcernsDropDownList.Items.FindByText("Other").Text, PAPConcernsDropDownList.Items.FindByText("Other").Value);
                PAPConcernsDropDownList.Items.Remove(PAPConcernsDropDownList.Items.FindByText("Other"));
                PAPConcernsDropDownList.Items.Add(LastListItem);
                if (lastItemSelected)
                    PAPConcernsDropDownList.Items[PAPConcernsDropDownList.Items.Count - 1].Selected = true;
                else
                    PAPConcernsDropDownList.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// to save the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            string uID = Session["USER_ID"].ToString();
            string HHID = Session["HH_ID"].ToString();

            SocioConcernBO objSocioConcern = new SocioConcernBO();
            if (PAPConcernsIDTextBox.Text != "0")
            {
                objSocioConcern.PapConcernID = Convert.ToInt32(ViewState["PapConcernID"]);
                objSocioConcern.ConcernID = Convert.ToInt32(PAPConcernsDropDownList.SelectedItem.Value.ToString().Trim());
                string strMax = PAPOtherConcernsTextBox.Text.Trim();
                if (strMax.Trim().Length >= 500)
                {
                    strMax = PAPOtherConcernsTextBox.Text.ToString().Trim().Substring(0, 500);
                }
                objSocioConcern.OtherConcern = strMax;
                objSocioConcern.UserID = Convert.ToInt32(uID);
                objSocioConcern.HHID = Convert.ToInt32(HHID);

                SocioConcernBLL SocioConcernobj = new SocioConcernBLL();
                message = SocioConcernobj.EditSocioConcern(objSocioConcern);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")                
                    message = "Data updated successfully";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                PAPConcernsIDTextBox.Text = "0";
                BindGrid();
                SetUpdateMode(false);
                clear();                
            }
            else
            {
                objSocioConcern.ConcernID = Convert.ToInt32(PAPConcernsDropDownList.SelectedItem.Value.ToString().Trim());
                string strMax = PAPOtherConcernsTextBox.Text.Trim();
                if (strMax.Trim().Length >= 500)
                {
                    strMax = PAPOtherConcernsTextBox.Text.ToString().Trim().Substring(0, 500);
                }
                objSocioConcern.OtherConcern = strMax;
                objSocioConcern.UserID = Convert.ToInt32(uID);
                objSocioConcern.HHID = Convert.ToInt32(HHID);

                SocioConcernBLL SocioConcernobj = new SocioConcernBLL();
                message = SocioConcernobj.InsertSocioConcern(objSocioConcern);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";
                    
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                clear();
                BindGrid();
                SetUpdateMode(false);
            }

            ChangeRequestStatusSocailConcerns();
            projectFrozen();            
        }
        /// <summary>
        /// to bind the grid
        /// </summary>

        private void BindGrid()
        {
            SocioConcernBLL objSocioConcernBLL = new SocioConcernBLL();
            SocioConcernList objSocioConcernList = new SocioConcernList();
            int HHID = Convert.ToInt32(Session["HH_ID"].ToString());
            objSocioConcernList = objSocioConcernBLL.getSocioConcern(HHID);
            grdSocioConcerns.DataSource = objSocioConcernList;
            grdSocioConcerns.DataBind();
        }
        /// <summary>
        /// To change the page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdSocioConcerns.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// for Edit and Delete Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdSocioConcerns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["PapConcernID"] = e.CommandArgument;
                GetSocialPAPConcernDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["PapConcernID"] = e.CommandArgument;
                SocioConcernBLL SocioConcernBLLobj = new SocioConcernBLL();
                int HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                message = SocioConcernBLLobj.DeleteSocialConcern(Convert.ToInt32(ViewState["PapConcernID"]), HHID);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                clear();
                SetUpdateMode(false);
                BindGrid();

            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        // get the Grid value into textBox
        private void GetSocialPAPConcernDetails()
        {
            SocioConcernBLL ConcernBLLobj = new SocioConcernBLL();
            int PapConcernID = 0;

            if (ViewState["PapConcernID"] != null)
                PapConcernID = Convert.ToInt32(ViewState["PapConcernID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"].ToString());

            SocioConcernBO SocioConcernBOObj = new SocioConcernBO();
            SocioConcernBOObj = ConcernBLLobj.GetSocioConcernById(PapConcernID, HHID);

            PAPConcernsDropDownList.ClearSelection();
            if (PAPConcernsDropDownList.Items.FindByValue(SocioConcernBOObj.ConcernID.ToString()) != null)
                PAPConcernsDropDownList.Items.FindByValue(SocioConcernBOObj.ConcernID.ToString()).Selected = true;

            PAPOtherConcernsTextBox.Text = SocioConcernBOObj.OtherConcern.ToString();

            PAPConcernsIDTextBox.Text = SocioConcernBOObj.PapConcernID.ToString();
        }
        /// <summary>
        /// to check the status of the Panel
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
                ViewState["PAP_SHOCKID"] = "0";
            }
        }
        /// <summary>
        /// to clear the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        public void clear()
        {
            PAPConcernsDropDownList.Items.Clear();
            GetConcer(false);
            PAPOtherConcernsTextBox.Text = string.Empty;
        }
    }
}