using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CDAP_Budget : System.Web.UI.Page
    {

        #region GlobalDEclaration

        ITEMBLL objITEMBLL;
        ItemList objItemList;
        UnitBLL objUnitBLL;
        UnitList objUnitList;
        CDAPBudgetBLL objCDAPBudgetBLL;
        CDAPBudgetBO objCDAPBudgetBO;
        CDAPBudgetList objCDAPBudgetList;

        #endregion

        #region PageEvents
        /// <summary>
        /// Check User permitions,Project Frozen or Not
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
            if (Session["PROJECT_CODE"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Community Development - CDAP Implementation - Budget";

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - CDAP Implementation - Budget";
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/UI/Project/ViewProjects.aspx?mode=redirCDAP"));
                }

                #region Upload Documets
                int userID = Convert.ToInt32(Session["USER_ID"]);
                int ProjectID = 0;
                string ProjectCode = string.Empty;

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
                string DocumentCode = "CDPAD";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);

                //if (HHID != 0)
                //{
                //    string DocumentCode = "CDPAD";

                //    string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                //    string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}');", ProjectID, HHID, userID, ProjectCode, DocumentCode);

                //    lnkUPloadDoc.Attributes.Add("onclick", param);

                //    lnkUPloadDoclist.Attributes.Add("onclick", paramView);
                //}
                //else
                //{
                //    lnkUPloadDoc.Visible = false;
                //    lnkUPloadDoclist.Visible = false;
                //}


                //End of code
                #endregion
                txtQuantity.Attributes.Add("onblur", "CalculateAmount();");
                txtRateperUnit.Attributes.Add("onblur", "CalculateAmount();");
                txtAmount.Attributes.Add("onKeyDown", "doCheck();");
                ViewState["CDAP_BUDGID"] = 0;
                BindItem();
                BindUnit();
                BindGrid();

                btnApproval.Visible = false;
                CheckPendings();
                if (Session["FROZEN"] != null)
                {
                    if (Session["FROZEN"].ToString() == "Y")
                    {
                        //btnSave.Visible = false;
                        //btnClear.Visible = false;
                        grdCDAPBudget.Columns[6].Visible = false;
                        grdCDAPBudget.Columns[7].Visible = false;
                        btnApproval.Visible = false;
                    }
                }

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_COMMUNITY_DEVELOPMENT) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnApproval.Visible = false;
                    lnkUPloadDoc.Visible = false;
                    grdCDAPBudget.Columns[6].Visible = false;
                    grdCDAPBudget.Columns[7].Visible = false;
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
                btnApproval.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                lnkUPloadDoc.Visible = false;
                grdCDAPBudget.Columns[6].Visible = false;
                grdCDAPBudget.Columns[7].Visible = false;
            }
        }
        /// <summary>
        /// Check CDAP Request Status
        /// </summary>
        private void CheckPendings()
        {
            objCDAPBudgetBLL = new CDAPBudgetBLL();
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"].ToString());
            string Status = "ALL";
            objCDAPBudgetList = objCDAPBudgetBLL.GetCDAPBudget(ProjectID, Status);
            int iCount = 0;
            for (int i = 0; i < objCDAPBudgetList.Count; i++)
            {
                if (objCDAPBudgetList[i].FundReqStatus.ToString().ToUpper() == "Pending Approval".ToUpper()
                    || objCDAPBudgetList[i].FundReqStatus.ToString().ToUpper() == "Declined".ToUpper())
                {
                    iCount++;
                }
            }
            if (iCount > 0)
            {
                checkApprovalExitOrNot();
                //btnApproval.Visible = true;
            }
            else
                btnApproval.Visible = false;
        }
        /// <summary>
        /// Bind Sub Item Based on Main Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catID = Convert.ToInt32(ddlItem.SelectedValue);
            BindSubItem(catID);
        }
        /// <summary>
        /// Calculate total amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtRateperUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string s = txtQuantity.Text.Trim();
                string t = txtRateperUnit.Text.Trim();
                if (!(string.IsNullOrEmpty(s) && string.IsNullOrEmpty(t)))
                {
                    int Quant = Convert.ToInt32(txtQuantity.Text.Trim());
                    decimal RateperUnit = Convert.ToDecimal(txtRateperUnit.Text.Trim());
                    decimal Amount = (Quant * RateperUnit);
                    txtAmount.Text = Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
        }
        /// <summary>
        /// Save and Update in to data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objCDAPBudgetBO = new CDAPBudgetBO();
            objCDAPBudgetBLL = new CDAPBudgetBLL();
            try
            {
                string i = "";
                objCDAPBudgetBO.Cdap_budgid = Convert.ToInt32(ViewState["CDAP_BUDGID"]);
                objCDAPBudgetBO.Cdap_categoryid = Convert.ToInt32(ddlItem.SelectedValue);
                objCDAPBudgetBO.Cdap_subcategoryid = Convert.ToInt32(ddlItemDesc.SelectedValue);
                objCDAPBudgetBO.Unit = Convert.ToDecimal(ddlUnit.SelectedValue);
                objCDAPBudgetBO.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                objCDAPBudgetBO.Rateperunit = Convert.ToDecimal(txtRateperUnit.Text.Trim());
                objCDAPBudgetBO.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                objCDAPBudgetBO.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);

                i = objCDAPBudgetBLL.AddCDAPBudget(objCDAPBudgetBO);
                if (i == "I")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data saved successfully');", true);
                }
                else if (i == "U")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Data updated successfully');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertError", "alert('Error: " + ex.Message + "');", true);
            }
            ClearDetails();
            BindGrid();
            SetUpdateMode(false);
            checkApprovalExitOrNot();
            CheckPendings();
        }
        /// <summary>
        /// to Clear Data
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

        #endregion

        #region Gridevents
        /// <summary>
        /// to Edit and Delete Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCDAPBudget_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CDAP_BUDGID"] = e.CommandArgument;
                objCDAPBudgetBLL = new CDAPBudgetBLL();
                objCDAPBudgetBO = new CDAPBudgetBO();
                objCDAPBudgetBO = objCDAPBudgetBLL.GetCDAPBudgetItem(Convert.ToInt32(ViewState["CDAP_BUDGID"]));
                BindItem();
                ddlItem.ClearSelection();
                if (ddlItem.Items.FindByValue(objCDAPBudgetBO.Cdap_categoryid.ToString()) != null)
                    ddlItem.Items.FindByValue(objCDAPBudgetBO.Cdap_categoryid.ToString()).Selected = true;
                int catid = Convert.ToInt32(ddlItem.SelectedValue);
                BindSubItem(catid);
                ddlItemDesc.ClearSelection();
                if (ddlItemDesc.Items.FindByValue(objCDAPBudgetBO.Cdap_subcategoryid.ToString()) != null)
                    ddlItemDesc.Items.FindByValue(objCDAPBudgetBO.Cdap_subcategoryid.ToString()).Selected = true;
                BindUnit();
                ddlUnit.ClearSelection();
                if (ddlUnit.Items.FindByValue(objCDAPBudgetBO.Unit.ToString()) != null)
                    ddlUnit.Items.FindByValue(objCDAPBudgetBO.Unit.ToString()).Selected = true;
                txtQuantity.Text = Convert.ToString(objCDAPBudgetBO.Quantity);
                txtRateperUnit.Text = UtilBO.CurrencyFormat(objCDAPBudgetBO.Rateperunit);
                txtAmount.Text = UtilBO.CurrencyFormat(objCDAPBudgetBO.Quantity * objCDAPBudgetBO.Rateperunit);
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["CDAP_BUDGID"] = e.CommandArgument;
                objCDAPBudgetBLL = new CDAPBudgetBLL();
                objCDAPBudgetBLL.DeleteCDAPBudget(Convert.ToInt32(ViewState["CDAP_BUDGID"]));
                ClearDetails();
                BindGrid();
                btnApproval.Visible = false;
            }
        }
        /// <summary>
        /// To change page Index of Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCDAPBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCDAPBudget.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To bind data to Drop down ddlItem
        /// </summary>
        private void BindItem()
        {
            objITEMBLL = new ITEMBLL();
            objItemList = objITEMBLL.GetItem();
            ddlItem.Items.Clear();
            ListItem lst;
            if (objItemList.Count > 0)
            {
                ddlItem.DataSource = objItemList;
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemcatId";
                ddlItem.DataBind();
                lst = new ListItem("--Select--", "0");
                ddlItem.Items.Insert(0, lst);
            }
            else
            {
                lst = new ListItem("No Items", "0");
                ddlItem.Items.Insert(0, lst);
                ddlItem.DataBind();
            }

        }
        /// <summary>
        /// To bind data to Drop down ddlItemDesc
        /// </summary>
        /// <param name="CatID"></param>
        private void BindSubItem(int CatID)
        {
            objITEMBLL = new ITEMBLL();
            objItemList = objITEMBLL.GetSubItem(CatID);
            ddlItemDesc.Items.Clear();
            ListItem lst;
            if (objItemList.Count > 0)
            {
                ddlItemDesc.DataSource = objItemList;
                ddlItemDesc.DataTextField = "ItemsubcatName";
                ddlItemDesc.DataValueField = "ItemsubcatId";
                ddlItemDesc.DataBind();
                lst = new ListItem("--Select--", "0");
                ddlItemDesc.Items.Insert(0, lst);
            }
            else
            {
                lst = new ListItem("No Sub Items", "0");
                ddlItemDesc.Items.Insert(0, lst);
            }
        }
        /// <summary>
        /// To bind data to Drop down ddlUnit
        /// </summary>
        private void BindUnit()
        {
            objUnitBLL = new UnitBLL();
            objUnitList = objUnitBLL.GetUnit();
            ddlUnit.Items.Clear();
            ListItem lst;
            if (objUnitList.Count > 0)
            {
                ddlUnit.DataSource = objUnitList;
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataValueField = "UnitID";
                ddlUnit.DataBind();
                lst = new ListItem("--Select--", "0");
                ddlUnit.Items.Insert(0, lst);
            }
            else
            {
                lst = new ListItem("--Select--", "0");
                ddlUnit.Items.Insert(0, lst);
            }
        }
        /// <summary>
        /// get data from data base abd bind it to grid
        /// </summary>
        private void BindGrid()
        {
            objCDAPBudgetBLL = new CDAPBudgetBLL();
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"].ToString());
            string Status = "ALL";
            objCDAPBudgetList = objCDAPBudgetBLL.GetCDAPBudget(ProjectID, Status);
            grdCDAPBudget.DataSource = objCDAPBudgetList;
            grdCDAPBudget.DataBind();

        }
        /// <summary>
        /// set update mode to buttons
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
                ViewState["CDAP_BUDGID"] = 0;
            }
        }
        /// <summary>
        /// to Clear data
        /// </summary>
        private void ClearDetails()
        {
            ddlItem.SelectedIndex = 0;
            int catID = Convert.ToInt32(ddlItem.SelectedValue);
            BindSubItem(catID);
            ddlUnit.SelectedIndex = 0;
            txtQuantity.Text = "";
            txtRateperUnit.Text = "";
            txtAmount.Text = "";
            ViewState["CDAP_BUDGID"] = 0;
        }
        #endregion
        /// <summary>
        /// set visible to Edit and delete images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCDAPBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal FundReqStatus = (Literal)e.Row.FindControl("litFundReqStatus");
                if (FundReqStatus.Text.ToLower() == "Approved".ToLower() || FundReqStatus.Text.ToLower() == "Sent for Approval".ToLower())
                {
                    ImageButton Edit = (ImageButton)e.Row.FindControl("imgEdit");
                    ImageButton Delete = (ImageButton)e.Row.FindControl("imgDelete");
                    Edit.Visible = false;
                    Delete.Visible = false;
                }
                if (FundReqStatus.Text.ToLower() == "Pending Approval".ToLower())
                {
                    checkApprovalExitOrNot();
                }
                else
                {
                    btnApproval.Visible = false;
                }
            }
        }
        /// <summary>
        /// Send request to Approver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            int getResult;
            openEmailPopupWindow();
            getResult = sentforapproval();
            if (getResult == -1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approval Notification has been sent');", true);
            }
            CheckPendings();
        }
        /// <summary>
        /// Check Approval Exit Or Not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowCdapBudget;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                btnApproval.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Approver is not Defined');", true);
                btnApproval.Visible = false;
            }
            #endregion
        }
        /// <summary>
        /// To open Email Pop up Window
        /// </summary>
        public void openEmailPopupWindow()
        {
            int HHIDIN_ = 0;
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowCdapBudget;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = HHIDIN_;                  //Convert.ToInt32(Session["HH_ID"]);
            string pageCode = "CDAPB";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, pageCode);
                ClientScript.RegisterStartupScript(this.GetType(), "CDAPBREQ", paramChangeRequest, true);
            }
        }
        /// <summary>
        /// to check how many sent for approval
        /// </summary>
        /// <returns></returns>
        public int sentforapproval()
        {
            int Result;
            int projectID = Convert.ToInt32(Session["PROJECT_ID"]);
            //objCDAPBudgetBO = new CDAPBudgetBO();
            objCDAPBudgetBLL = new CDAPBudgetBLL();
            Result = objCDAPBudgetBLL.SendforApproval(projectID);
            return Result;
        }

    }
}