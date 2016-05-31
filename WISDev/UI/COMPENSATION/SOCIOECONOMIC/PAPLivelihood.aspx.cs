using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class PAPLivelihood : System.Web.UI.Page
    {
        PAP_LivelihoodList LivelihoodItems = null;
        /// <summary>
        /// to set the PAge Header,Call  FillBanks() to fill the bank names from the database
        /// call  GetLivelihoodItems() to get the the livlihood Items  from the database
        /// call  BindLivelihoodItems() to Bind the the livlihood Items  from the database
        ///  call  GetBankDetails() to get the the Bank Details  from the database              
           
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
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.Livelihood;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.LivelihoodDetails;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Livelihood";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                FillBanks();
                GetLivelihoodItems();
                BindLivelihoodItems();
                GetBankDetails();
                txtAccountNo.Attributes.Add("onchange", "setDirtyText();");
                txtAccountName.Attributes.Add("onchange", "setDirtyText();");
                btnSave.Attributes.Add("onclick", " isDirty = 0;");
                btnClear.Attributes.Add("onclick", " isDirty = 0;");
                btnSaveBank.Attributes.Add("onclick", " isDirty = 0;");
                btnClearBank.Attributes.Add("onclick", " isDirty = 0;");
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    chkHasBankAccount.Enabled = false;
                    btnSaveBank.Visible = false;
                    btnClearBank.Visible = false;
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
                chkHasBankAccount.Enabled = false;
                btnSaveBank.Visible = false;
                btnClearBank.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
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

        #region Frozen / Approval / Decline / Pending Status
        /// <summary>
        /// to check the status of the Frozen / Approval / Decline / Pending Status
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSaveBank.Visible = false;
                    btnClearBank.Visible = false;

                    checkApprovalExitOrNot();
                    getApprrequtStatusPAPLivehood();
                    getApprrequtStatusBankDetial();
                }
            }
        }
        /// <summary>
        /// to check the status of the approvar exist or not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusPAPLivehood.Text = "";
            StatusPAPLivehood1.Text = "";
            StatusPAPLivehood.Visible = false;
            StatusPAPLivehood1.Visible = false;
            // used to display the Status if you send Request for change data
            // getApprovalChangerequestStatus(); //To make Visible Save and Cancle Button by checking Approve status
            //for checking Change Request Approver Exit or not
            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHSEL");
                lnkPAPLiveHood.Attributes.Add("onclick", paramChangeRequest);
                lnkPAPLiveHood.Visible = true;
                
                string param = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHSBD");
                lnkPAPLivehoodBNKDet.Attributes.Add("onclick", param);
                lnkPAPLivehoodBNKDet.Visible = true;
            }
            else
            {
                lnkPAPLiveHood.Visible = false;
                lnkPAPLivehoodBNKDet.Visible = false;

            }
            #endregion
            getApprrequtStatusPAPLivehood();
            getApprrequtStatusBankDetial();
        }
        /// <summary>
        /// to check the Status of the Change request for PAPLivlohood
        /// </summary>
        public void ChangeRequestStatusPAPLivehood()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHSEL";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to check the status of the change request for the Bank
        /// </summary>
        public void ChangeRequestStatusBank()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHSBD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to get the status of the Approver  for PAPLivelihood        /// </summary>

        public void getApprrequtStatusPAPLivehood()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHSEL";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPAPLiveHood.Visible = false;
                  
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                  
                    StatusPAPLivehood.Visible = true;
                    StatusPAPLivehood.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPAPLiveHood.Visible = true;
                  
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                  
                    StatusPAPLivehood.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPAPLiveHood.Visible = false;
                    StatusPAPLivehood.Visible = false;
                   
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                }
            }
        }
        /// <summary>
        /// to get the Status of the Approvar Bank Details
        /// </summary>
        public void getApprrequtStatusBankDetial()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHSBD";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkPAPLivehoodBNKDet.Visible = false;

                    btnSaveBank.Visible = false;
                    btnClearBank.Visible = false;

                    StatusPAPLivehood1.Visible = true;
                    StatusPAPLivehood1.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkPAPLivehoodBNKDet.Visible = true;

                    btnSaveBank.Visible = false;
                    btnClearBank.Visible = false;

                    StatusPAPLivehood1.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkPAPLivehoodBNKDet.Visible = false;

                    StatusPAPLivehood1.Visible = false;

                    btnSaveBank.Visible = false;
                    btnClearBank.Visible = false;
                }
            }
        }

        #endregion
        /// <summary>
        /// to fill the banks from the batabase
        /// </summary>
        private void FillBanks()
        {
            BankBLL objBankBLL = new BankBLL();
            ddlBank.DataSource = objBankBLL.GetBanks();
            ddlBank.DataValueField = "BankID";
            ddlBank.DataTextField = "BankName";
            ddlBank.DataBind();
        }
        /// <summary>
        /// to bind the livelihood Items
        /// </summary>
        private void BindLivelihoodItems()
        {
            grdLivelihoodItems.DataSource = (new LivelihoodBLL()).GetLivelihood();
            grdLivelihoodItems.DataBind();
        }
        /// <summary>
        /// To get the LivelihoodItems
        /// </summary>
        private void GetLivelihoodItems()
        {
            PAP_LivelihoodBLL objLivelihoodBLL = new PAP_LivelihoodBLL();
            LivelihoodItems = objLivelihoodBLL.GetLivelihoodItemsByID(Convert.ToInt32(Session["HH_ID"]));
        }
        /// <summary>
        /// to get the BankDetails()
        /// </summary>
        private void GetBankDetails()
        {
            PAP_BankBO objPAPBank = new PAP_BankBO();

            objPAPBank = (new PAP_BankBLL()).GetPAPBankByID(Convert.ToInt32(Session["HH_ID"]));
            if (objPAPBank != null)
            {
                chkHasBankAccount.Checked = true;
                btnSaveBank.ValidationGroup = "Bank";
                txtAccountNo.Text = objPAPBank.AccountNo;
                txtAccountName.Text = objPAPBank.AccountHolderName;

                ddlBank.ClearSelection();
                if (ddlBank.Items.FindByValue(objPAPBank.BankID.ToString()) != null)
                    ddlBank.Items.FindByValue(objPAPBank.BankID.ToString()).Selected = true;

                BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));
                ddlBranch.ClearSelection();
                if (ddlBranch.Items.FindByValue(objPAPBank.BranchID.ToString()) != null)
                    ddlBranch.Items.FindByValue(objPAPBank.BranchID.ToString()).Selected = true;

                EnableBankDetails(true);
            }
            else
            {
                chkHasBankAccount.Checked = false;
                btnSaveBank.ValidationGroup = "";
                EnableBankDetails(false);
            }
        }
        decimal TotalCashAmount = 0;

        protected void grdLivelihoodItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int itemID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Itemid"));
                TextBox txtCash = (TextBox)e.Row.FindControl("txtCash");
                TextBox txtInKind = (TextBox)e.Row.FindControl("txtInKind");

                //txtCash.Text = String.Format("{0:N0}", txtCash.Text);
                txtCash.Attributes.Add("onchange", "CalculateTotalCash(this);");

                foreach (PAP_LivelihoodBO objLivelihood in LivelihoodItems)
                {
                    if (objLivelihood.LivelihoodItemID == itemID)
                    {
                        if (objLivelihood.Cash > 0)
                        {
                            //txtCash.Text = objLivelihood.Cash.ToString();
                            txtCash.Text = UtilBO.CurrencyFormat(objLivelihood.Cash);
                            TotalCashAmount += objLivelihood.Cash;
                        }

                        txtInKind.Text = objLivelihood.InKind;
                        break;
                    }
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (e.Row.FindControl("txtTotalCash") is TextBox)
                    {
                        TextBox txtTotalCash = e.Row.FindControl("txtTotalCash") as TextBox;
                        txtTotalCash.Text = UtilBO.CurrencyFormat(TotalCashAmount);
                        //txtTotalCash.Text = TotalCashAmount.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// to save the Bank Details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSaveBank_Click(object sender, EventArgs e)
        {
            if (chkHasBankAccount.Checked)
            {
                PAP_BankBO objPAPBank = new PAP_BankBO();
                objPAPBank.HouseHoldID = Convert.ToInt32(Session["HH_ID"]);
                objPAPBank.BankID = Convert.ToInt32(ddlBank.SelectedItem.Value);
                objPAPBank.BranchID = Convert.ToInt32(ddlBranch.SelectedItem.Value);
                objPAPBank.AccountNo = txtAccountNo.Text.Trim();
                objPAPBank.AccountHolderName = txtAccountName.Text.Trim();
                objPAPBank.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                objPAPBank.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                (new PAP_BankBLL()).UpdatePAPBank(objPAPBank);
            }
            else
            {
                (new PAP_BankBLL()).DeletePAPBank(Convert.ToInt32(Session["HH_ID"]));
            }
            
            ChangeRequestStatusBank();
            projectFrozen();
          
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UpdatedBank", "alert('Data saved successfully');", true);
        }
        /// <summary>
        /// to save the data to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LivelihoodItems = new PAP_LivelihoodList();
            PAP_LivelihoodBO objLivelihood = null;

            foreach (GridViewRow grdRow in grdLivelihoodItems.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    string itemID = ((Literal)(grdRow.FindControl("litItemID"))).Text;
                    TextBox txtCash = (TextBox)grdRow.FindControl("txtCash");
                    TextBox txtInKind = (TextBox)grdRow.FindControl("txtInKind");

                    objLivelihood = new PAP_LivelihoodBO();
                    objLivelihood.LivelihoodItemID = Convert.ToInt32(itemID);
                    objLivelihood.HouseHoldID = Convert.ToInt32(Session["HH_ID"]);

                    if (txtCash.Text.Trim() != "")
                        objLivelihood.Cash = Convert.ToDecimal(txtCash.Text.Trim());

                    objLivelihood.InKind = txtInKind.Text.Trim();
                    objLivelihood.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                    objLivelihood.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                    LivelihoodItems.Add(objLivelihood);
                }
            }

            PAP_LivelihoodBLL objLivelihoodBLL = new PAP_LivelihoodBLL();
            objLivelihoodBLL.UpdateLivelihood(LivelihoodItems);
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UpdatedLivelihood", "alert('Livelihood details updated successfully');", true);
            ChangeRequestStatusPAPLivehood();
            projectFrozen();
         
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UpdatedBank", "alert('Data saved successfully');", true);
            BindLivelihoodItems();
        }
        /// <summary>
        /// to clear the data Fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnClearBank_Click(object sender, EventArgs e)
        {
            ddlBank.ClearSelection();// = Status;
            BindBankBranches(0);
            txtAccountNo.Text = string.Empty;
            txtAccountName.Text = string.Empty;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //Clearing Livelihood Grid Details
            foreach (GridViewRow grdRow in grdLivelihoodItems.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    string itemID = ((Literal)(grdRow.FindControl("litItemID"))).Text;
                    TextBox txtCash = (TextBox)grdRow.FindControl("txtCash");
                    TextBox txtInKind = (TextBox)grdRow.FindControl("txtInKind");                  

                    txtCash.Text = "0";
                    txtInKind.Text = "NONE";                   
                }
            }
            if (grdLivelihoodItems.FooterRow.FindControl("txtTotalCash") is TextBox)
            {
                TextBox txtTotalCash = (TextBox)grdLivelihoodItems.FooterRow.FindControl("txtTotalCash");
                txtTotalCash.Text = string.Empty;
            }
        }
        /// <summary>
        /// to check the status of the CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkHasBankAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHasBankAccount.Checked)
            {
                EnableBankDetails(true);
                btnSaveBank.ValidationGroup = "Bank";
            }
            else
            {
                EnableBankDetails(false);
                btnSaveBank.ValidationGroup = "";
                ddlBank.ClearSelection();
                ddlBranch.ClearSelection();
                txtAccountNo.Text = string.Empty;
                txtAccountName.Text = string.Empty;
            }
        }
        /// <summary>
        /// to Enable Bank Details
        /// </summary>
        /// <param name="Status"></param>
        private void EnableBankDetails(bool Status)
        {
            ddlBank.Enabled = Status;
            ddlBranch.Enabled = Status;
            txtAccountNo.Enabled = Status;
            txtAccountName.Enabled = Status;
            btnSaveBank.Enabled = Status;
            btnClearBank.Enabled = Status;

            spnMandatoryBank.Visible = Status;
            spnMandatoryBranch.Visible = Status;
            spnMandatoryAccNum.Visible = Status;
            spnMandatoryAccName.Visible = Status;
        }
        /// <summary>
        /// bind the BankBranches on selected Bank
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));
        }
        /// <summary>
        /// to bind the Bank Branches
        /// </summary>
        /// <param name="bankID"></param>
        protected void BindBankBranches(int bankID)
        {
            ListItem firstListItem = new ListItem(ddlBranch.Items[0].Text, ddlBranch.Items[0].Value);

            ddlBranch.Items.Clear();

            if (bankID > 0)
            {
                BranchBLL objBranchBLL = new BranchBLL();
                ddlBranch.DataSource = objBranchBLL.GetActiveBranches(bankID);

                ddlBranch.DataTextField = "BRANCHNAME";
                ddlBranch.DataValueField = "BankBranchId";
                ddlBranch.DataBind();
            }

            ddlBranch.Items.Insert(0, firstListItem);
            ddlBranch.SelectedIndex = 0;
        }
    }
}