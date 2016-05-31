using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ProjectDetails : System.Web.UI.Page
    {
        #region Global Declaration & Page Load
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.ProjectDetails;
            caldpProjectStartDate.Format = UtilBO.DateFormat;
            CaldpProjectEndDate.Format = UtilBO.DateFormat;
            caldpConstructionStartDate.Format = UtilBO.DateFormat;
            CaldpConstructionEndDate.Format = UtilBO.DateFormat;

            if (!IsPostBack)
            {
                string mode = "";
                txtObjective.Attributes.Add("maxlength", txtObjective.MaxLength.ToString());
                if (Request.QueryString["mode"] != null) mode = Request.QueryString["mode"];

                if (mode.ToUpper() == "NEW")
                {
                    Master.PageHeader = "Create Project";
                    ProjectMenu1.Visible = false;
                    ViewState["BANK_ID"] = 0;
                    Session["PROJECT_ID"] = null;
                    Session["PROJECT_CODE"] = null;
                    ProjectMenu1.Mode = mode;
                    pnlSegments.Visible = false;
                    LoadCurrencyTotalEstimate();

                }
                else if (Session["PROJECT_ID"] != null)
                {
                    Master.PageHeader = "Project Details";
                    btnSave.Text = "Update Project";
                    GetProjectDetails();
                    pnlSegments.Visible = true;
                    LoadTypeLine();
                    DisplayEstBudget();
                    ddlProjectStatus.Enabled = true;
                    FillBanks();
                }
                txtProjectCode.Attributes.Add("onchange", "setDirty();");
                txtProjectName.Attributes.Add("onchange", "setDirty();");
                txtLabouCost.Attributes.Add("onchange", "setDirty();");
                txtBulMatCost.Attributes.Add("onchange", "setDirty();");
                //---------//
                txtSegmentName.Attributes.Add("onchange", "setDirty();");
                txtRouteLength.Attributes.Add("onblur", "setDirty();");
                txtRouteLength.Attributes.Add("onchange", "CheckDecimal(e, src);");
                txtEstBudget.Attributes.Add("onchange", "setDirty();");
                txtImplementationPeriod.Attributes.Add("onchange", "setDirty();");
                txtfunder.Attributes.Add("onchange", "setDirty();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
                btnADDSegment.Attributes.Add("onclick", "isDirty = 0;");
                btnSegmentClear.Attributes.Add("onclick", "isDirty = 0;");
                ddlTypeLine.Attributes.Add("onchange", "isDirty = 0;");
                txtDollervalue.Attributes.Add("onchange", "setDirty();");
                txtDollervalue.Attributes.Add("onblur", "CheckDecimal(e, src);");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btnADDSegment.Visible = false;
                    btnClear.Visible = false;
                    btnSave.Visible = false;
                    btnSegmentClear.Visible = false;
                    grdSegmentDetails.Columns[grdSegmentDetails.Columns.Count - 1].Visible = false;
                }
                ViewStateProjectSegmentId = 0;
            }
        }
        #endregion Global Declaration & Page Load

        #region Project Segment

        #region ViewState
        /// <summary>
        /// method to get and set ViewStateProjectSegmentId
        /// </summary>
        private int ViewStateProjectSegmentId
        {
            get
            {
                if (ViewState["ProjectSegmentID"] != null)
                    return Convert.ToInt32(ViewState["ProjectSegmentID"].ToString());
                else
                    return 0;
            }
            set
            {
                ViewState["ProjectSegmentID"] = value;
            }
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Clears input details and load default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSegmentClear_Click(object sender, EventArgs e)
        {
            dpConstructionEndDate.Text = "";
            dpConstructionStartDate.Text = "";
            ClearSegment();
            if (((Button)sender).Text.ToUpper().IndexOf("CANCEL") >= 0)
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// used to add details and save to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnADDSegment_Click(object sender, EventArgs e)
        {
            //Adding New Segment
            string message = string.Empty;
            SegmentBO objProjectSegment = new SegmentBO();

            ProjectBLL oProjectBLL = new ProjectBLL();

            objProjectSegment.SegmentName = txtSegmentName.Text;
            objProjectSegment.RouteLength = Convert.ToDecimal(txtRouteLength.Text).ToString();
            objProjectSegment.LineTypeID = Convert.ToInt32(ddlTypeLine.SelectedValue);

            if (Session["PROJECT_ID"] != null)
                objProjectSegment.ProjectID = Convert.ToInt32(Session["PROJECT_ID"].ToString());

            objProjectSegment.EstBudget = Convert.ToDecimal(txtEstBudget.Text);
            objProjectSegment.ImplementationPeriod = txtImplementationPeriod.Text;
            objProjectSegment.ConstrStartDate = Convert.ToDateTime(dpConstructionStartDate.Text);
            objProjectSegment.ConstrEndDate = Convert.ToDateTime(dpConstructionEndDate.Text);
            objProjectSegment.Funder = txtfunder.Text;
            objProjectSegment.Bankid = Convert.ToInt32(ddlBank.SelectedItem.Value);
            objProjectSegment.Valueofhouse = Convert.ToDecimal(txtValueofhouse.Text);


            objProjectSegment.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
            objProjectSegment.IsDeleted = "False";

            if (ViewStateProjectSegmentId > 0)
            {
                //Updating the Segment
                objProjectSegment.ProjectSegmentID = ViewStateProjectSegmentId;
                message = oProjectBLL.UpdateProjectSegment(objProjectSegment);
                SetUpdateMode(true);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }
            }
            else
            {
                //Adding New Segment
                message = oProjectBLL.SaveProjectSegment(objProjectSegment);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }

            }
            ClearSegment();

            //Loading Segments
            if (Session["PROJECT_ID"] != null)
            {
                int ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                LoadProjectSegmentGV(ProjectId);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// method to fill bank dropdownlist
        /// </summary>
        private void FillBanks()
        {
            BankBLL objBankBLL = new BankBLL();
            ddlBank.DataSource = objBankBLL.GetBanks();
            ddlBank.DataValueField = "BankID";
            ddlBank.DataTextField = "BankName";
            ddlBank.DataBind();
        }
        //// protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        // {
        //     BindBankBranches(Convert.ToInt32(ddlBank.SelectedItem.Value));
        // }
        ////  protected void BindBankBranches(int bankID)
        //  {
        //      ListItem firstListItem = new ListItem(ddlBranch.Items[0].Text, ddlBranch.Items[0].Value);

        //      ddlBranch.Items.Clear();

        //      if (bankID > 0)
        //      {
        //          BranchBLL objBranchBLL = new BranchBLL();
        //          ddlBranch.DataSource = objBranchBLL.GetActiveBranches(bankID);

        //          ddlBranch.DataTextField = "BRANCHNAME";
        //          ddlBranch.DataValueField = "BankBranchId";
        //          ddlBranch.DataBind();
        //      }

        //      ddlBranch.Items.Insert(0, firstListItem);
        //      ddlBranch.SelectedIndex = 0;
        //  }
        /// <summary>
        /// method to set dateformat
        /// </summary>
        /// <param name="InDate"></param>
        /// <returns></returns>
        public string DateFormat(string InDate)
        {
            DateTime dt = Convert.ToDateTime(InDate);
            string ss = dt.ToString("dd/MMM/yyyy"); ;
            string[] dateArray = ss.Split('/');
            return dateArray[0] + "-" + dateArray[1] + "-" + dateArray[2];
        }
        /// <summary>
        /// method to clear segment details
        /// </summary>
        private void ClearSegment()
        {
            txtfunder.Text = "";
            SetUpdateMode(false);
            txtSegmentName.Text = string.Empty;
            ddlBank.ClearSelection();
            //BindBankBranchess(0);           
            txtRouteLength.Text = string.Empty;
            txtLabouCost.Text = string.Empty;
            txtBulMatCost.Text = string.Empty;
            if (ddlTypeLine.Items.Count > 0)
                ddlTypeLine.SelectedIndex = 0;
            txtEstBudget.Text = string.Empty;
            txtImplementationPeriod.Text = string.Empty;
            lblRightofWayMeasurement.Text = "";
            lblWayLeaveMeasurement.Text = "";
            dpConstructionEndDate.Text = "";
            dpConstructionStartDate.Text = "";
            btnADDSegment.Text = "Add Segment";
            btnSegmentClear.Text = "Clear Segment";
            ViewStateProjectSegmentId = 0;
            txtValueofhouse.Text = String.Empty;
        }
        //protected void BindBankBranchess(int bankID)
        //{
        //    ListItem firstListItem = new ListItem(ddlBranch.Items[0].Text, ddlBranch.Items[0].Value);

        //    ddlBranch.Items.Clear();

        //    if (bankID > 0)
        //    {
        //        BranchBLL objBranchBLL = new BranchBLL();
        //        ddlBranch.DataSource = objBranchBLL.GetActiveBranches(bankID);

        //        ddlBranch.DataTextField = "BRANCHNAME";
        //        ddlBranch.DataValueField = "BankBranchId";
        //        ddlBranch.DataBind();
        //    }

        //    ddlBranch.Items.Insert(0, firstListItem);
        //    ddlBranch.SelectedIndex = 0;
        //}
        #endregion

        #region Events
        /// <summary>
        /// To change values in dropdownlist based on index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTypeLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Call the By Line ID
            TypeOfLineBO oTypeOfLine = new TypeOfLineBO();
            TypeOfLineBLL oTypeOfLineBLL = new TypeOfLineBLL();

            oTypeOfLine = oTypeOfLineBLL.GetLineTypebyID(Convert.ToInt32(ddlTypeLine.SelectedValue.ToString()));

            lblRightofWayMeasurement.Text = oTypeOfLine.Rightofwaymeasurement;
            lblWayLeaveMeasurement.Text = oTypeOfLine.Wayleavemeasurement;

        }
        /// <summary>
        /// To fetch projectsegment by ID and assign to textbox
        /// </summary>
        private void GetProjectSegmentByID()
        {
            ProjectBLL oProjectBLL = new ProjectBLL();

            SegmentBO oProjectSegment = oProjectBLL.GetProjectSegmentByID(ViewStateProjectSegmentId);

            if (oProjectSegment != null)
            {
                txtSegmentName.Text = oProjectSegment.SegmentName;
                txtRouteLength.Text = oProjectSegment.RouteLength;
                ddlTypeLine.SelectedValue = oProjectSegment.LineTypeID.ToString();

                lblRightofWayMeasurement.Text = oProjectSegment.RightOfWay;
                lblWayLeaveMeasurement.Text = oProjectSegment.WayLeave;

                txtEstBudget.Text = UtilBO.CurrencyFormat(oProjectSegment.EstBudget);

                txtImplementationPeriod.Text = oProjectSegment.ImplementationPeriod.ToString();
                dpConstructionStartDate.Text = Convert.ToString(oProjectSegment.ConstrStartDate.ToString(UtilBO.DateFormat));
                dpConstructionEndDate.Text = Convert.ToString(oProjectSegment.ConstrEndDate.ToString(UtilBO.DateFormat));
                txtfunder.Text = oProjectSegment.Funder;
                ddlBank.ClearSelection();
                if (ddlBank.Items.FindByValue(oProjectSegment.Bankid.ToString()) != null)
                    ddlBank.Items.FindByValue(oProjectSegment.Bankid.ToString()).Selected = true;

                txtValueofhouse.Text = UtilBO.CurrencyFormat(oProjectSegment.Valueofhouse);
            }

            DisplayEstBudget();
        }
        #endregion
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnADDSegment.Text = "Update Segment";
                btnSegmentClear.Text = "Cancel";
            }
            else
            {
                btnADDSegment.Text = "Add Segment";
                btnSegmentClear.Text = "Clear Segment";
                ViewStateProjectSegmentId = 0;
            }
        }

        #region Load Methods
        /// <summary>
        /// To bind SegmentDetails to grid
        /// </summary>
        /// <param name="ProjectId"></param>
        private void LoadProjectSegmentGV(int ProjectId)
        {
            ProjectBLL objProjectSegmentBLL = new ProjectBLL();
            SegmentBO objProjectSegment = new SegmentBO();

            grdSegmentDetails.DataSource = objProjectSegmentBLL.GetProjectSegment(ProjectId);
            grdSegmentDetails.DataBind();

            if (grdSegmentDetails.Rows.Count > 0)
                p1Grid.Style.Add("display", "");
            else
                p1Grid.Style.Add("display", "none");
            //  ddlCurrencyTypeEstBudget.Items.FindByText(ViewState["BudgetCurrency"].ToString();
            // LoadTypeLine();
        }
        /// <summary>
        /// To assign values to TypeLineDropdownList
        /// </summary>
        private void LoadTypeLine()//(object sender, EventArgs e)
        {
            TypeOfLineBLL oTypeOfLineBLL = new TypeOfLineBLL();
            TypeOfLineLists TypeLineList = new TypeOfLineLists();

            ddlTypeLine.Items.Clear();

            TypeLineList = oTypeOfLineBLL.GetLineType();

            ddlTypeLine.DataTextField = "TYPEOFLINE";
            ddlTypeLine.DataValueField = "LINETYPEID";
            ddlTypeLine.DataSource = TypeLineList;
            ddlTypeLine.DataBind();
            ddlTypeLine.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlTypeLine.SelectedIndex = 0;
        }
        /// <summary>
        /// To assign values to CurrencyDropdownList
        /// </summary>
        private void LoadCurrencyEstimate()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            CurrencyList objCurrencyList = new CurrencyList();
            objCurrencyList = objMasterBLL.LoadCurrency();
            ddlCurrencyTypeEstBudget.Items.Clear();
            ddlCurrencyTypeEstBudget.DataTextField = "CurrencyCode";
            ddlCurrencyTypeEstBudget.DataValueField = "CurrencyID";
            ddlCurrencyTypeEstBudget.DataSource = objCurrencyList;
            ddlCurrencyTypeEstBudget.DataBind();
            //ddlCurrencyTypeEstBudget.Items.Insert(0, "--Select--");
            ddlCurrencyTypeEstBudget.SelectedIndex = 0;
        }
        /// <summary>
        /// To assign values to CurrencyTypeTotalEstBudget dropdownlist
        /// </summary>
        private void LoadCurrencyTotalEstimate()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            CurrencyList objCurrencyList = new CurrencyList();
            objCurrencyList = objMasterBLL.LoadCurrency();
            ddlCurrencyTypeTotalEstBudget.DataTextField = "CurrencyCode";
            ddlCurrencyTypeTotalEstBudget.DataValueField = "CurrencyID";
            ddlCurrencyTypeTotalEstBudget.DataSource = objCurrencyList;
            ddlCurrencyTypeTotalEstBudget.DataBind();
            ddlCurrencyTypeTotalEstBudget.SelectedIndex = 0;
        }

        #endregion

        #region Grid
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdSegmentDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSegmentDetails.PageIndex = e.NewPageIndex;
            if (Session["PROJECT_ID"] != null)
            {
                int ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                LoadProjectSegmentGV(ProjectId);
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdSegmentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewStateProjectSegmentId = Convert.ToInt32(e.CommandArgument);

                GetProjectSegmentByID();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

        }
        /// <summary>
        /// For date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdSegmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime constrStartDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ConstrStartDate"));
                if (constrStartDate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblConstrStartDate")).Text = constrStartDate.ToString(UtilBO.DateFormat);

                DateTime constrEndDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ConstrEndDate"));
                if (constrEndDate != DateTime.MinValue)
                    ((Label)e.Row.FindControl("lblConstrEndDate")).Text = constrEndDate.ToString(UtilBO.DateFormat);


            }
        }
        #endregion Grid
        #endregion

        #region Project Details
        /// <summary>
        /// Method to fetch project details and assign to textbox
        /// </summary>
        protected void GetProjectDetails()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));

            if (!string.IsNullOrEmpty(objProject.ProjectCode))
                Session["PROJECT_CODE"] = objProject.ProjectCode.ToString();
            txtpercentage.Text =Convert.ToString(objProject.PercentageofPAP);
            txtProjectCode.Text = objProject.ProjectCode;
            Master.PageHeader = objProject.ProjectCode + " - Project Details";

            txtProjectName.Text = objProject.ProjectName;
            txtObjective.Text = objProject.Objective;

            if (objProject.ProjectStartDate != DateTime.MinValue)
                dpProjectStartDate.Text = Convert.ToString(objProject.ProjectStartDate.ToString(UtilBO.DateFormat));

            if (objProject.ProjectEndDate != DateTime.MinValue)
                dpProjectEndDate.Text = Convert.ToString(objProject.ProjectEndDate.ToString(UtilBO.DateFormat));

            txtTotalEstBudget.Text = UtilBO.CurrencyFormat(objProject.TotalEstBudget);
            txtLabouCost.Text = UtilBO.CurrencyFormat(objProject.Labourcost);
            txtBulMatCost.Text = UtilBO.CurrencyFormat(objProject.BUILDINGMATCOST);
            txtDollervalue.Text = Convert.ToString(objProject.Dollervalue);
            //String.Format("{0:C0}", objProject.TotalEstBudget).Replace("$", "");

            LoadCurrencyTotalEstimate();
            ddlCurrencyTypeTotalEstBudget.ClearSelection();
            if (ddlCurrencyTypeTotalEstBudget.Items.FindByValue(objProject.BudgetCurrency.ToString()) != null)
            {
                ddlCurrencyTypeTotalEstBudget.Items.FindByValue(objProject.BudgetCurrency.ToString()).Selected = true;
            }

            ViewState["BudgetCurrency"] = objProject.BudgetCurrency;

            //LoadCurrencyEstimate();
            //ddlCurrencyTypeEstBudget.ClearSelection();
            //if (ddlCurrencyTypeEstBudget.Items.FindByValue(objProject.BudgetCurrency.ToString()) != null)
            //{
            //    ddlCurrencyTypeEstBudget.Items.FindByValue(objProject.BudgetCurrency.ToString()).Selected = true;
            //}
            //ddlCurrencyTypeEstBudget.Enabled = false;
            DisplayEstBudget();

            ddlProjectStatus.ClearSelection();

            if (ddlProjectStatus.Items.FindByValue(objProject.ProjectStatus.ToUpper()) != null)
            {
                ddlProjectStatus.Items.FindByValue(objProject.ProjectStatus.ToUpper()).Selected = true;
            }
            //Loading Segments
            if (Session["PROJECT_ID"] != null)
            {
                int ProjectId = Convert.ToInt32(Session["PROJECT_ID"]);
                LoadProjectSegmentGV(ProjectId);
            }
        }
        /// <summary>
        /// Used to save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ProjectBO objProject = new ProjectBO();
                objProject.PercentageofPAP = Convert.ToDecimal(txtpercentage.Text.Trim());
                objProject.ProjectCode = txtProjectCode.Text.Trim();
                objProject.ProjectName = txtProjectName.Text.Trim();

                string sObj = txtObjective.Text.Trim();
                if (sObj.Trim().Length > 1000)
                {
                    sObj = sObj.Substring(0, 1000);
                }
                objProject.Objective = sObj.Trim();

                if (dpProjectStartDate.Text.Trim() != "")
                    objProject.ProjectStartDate = Convert.ToDateTime(dpProjectStartDate.Text.ToString());

                if (dpProjectEndDate.Text.Trim() != "")
                    objProject.ProjectEndDate = Convert.ToDateTime(dpProjectEndDate.Text.ToString());

                objProject.BudgetCurrency = Convert.ToInt32(ddlCurrencyTypeTotalEstBudget.SelectedValue);

                if (txtTotalEstBudget.Text.Trim().Length > 0)

                    objProject.TotalEstBudget = Convert.ToDecimal(txtTotalEstBudget.Text.Trim());
                if (txtLabouCost.Text.Trim().Length > 0)
                    objProject.Labourcost = Convert.ToDecimal(txtLabouCost.Text);
                if (txtBulMatCost.Text.Trim().Length > 0)
                    objProject.BUILDINGMATCOST = Convert.ToDecimal(txtBulMatCost.Text);
                if (txtDollervalue.Text.Trim().Length > 0)
                    objProject.Dollervalue = Convert.ToDecimal(txtDollervalue.Text);
                if (ddlProjectStatus.SelectedIndex > 0)
                    objProject.ProjectStatus = ddlProjectStatus.SelectedItem.Value;
                else
                {
                    ddlProjectStatus.ClearSelection();
                    ddlProjectStatus.SelectedIndex = 1;
                    objProject.ProjectStatus = ddlProjectStatus.SelectedItem.Value;
                }

                if (Session["PROJECT_ID"] != null)
                {
                    objProject.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    objProject.UpdatedBy = (int)Session["USER_ID"];
                }
                else
                {
                    objProject.CreatedBy = (int)Session["USER_ID"];
                }

                ProjectBLL objProjectBLL = new ProjectBLL();

                if (Session["PROJECT_ID"] != null)
                {
                    string msg = objProjectBLL.UpdateProject(objProject);
                    if (msg == "")
                        msg = "Data updated successfully.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + msg + "');", true);
                    ViewState["BudgetCurrency"] = ddlCurrencyTypeTotalEstBudget.SelectedValue;

                    if (txtTotalEstBudget.Text.Trim().Length > 0)
                    {
                        txtTotalEstBudget.Text = UtilBO.CurrencyFormat(Convert.ToDecimal(txtTotalEstBudget.Text));
                    }

                    DisplayEstBudget();
                }
                else
                {
                    string[] result = objProjectBLL.AddProject(objProject);
                    string message = "";

                    if (string.IsNullOrEmpty(result[1]) || result[1] == "" || result[1] == "null")
                    {
                        Session["PROJECT_ID"] = result[0];
                        Session["PROJECT_CODE"] = objProject.ProjectCode;
                        Session["FROZEN"] = "N";

                        message = "alert('Data saved successfully');location.href='ProjectRoute.aspx';";
                    }
                    else
                    {
                        message = result[1];
                        message = "alert('" + message + "');";
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", message, true);
                }
            }
        }
        /// <summary>
        /// calls clear details method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearSegment();
            ClearDetails();
        }
        /// <summary>
        /// To  Display Estimated Budget
        /// </summary>
        private void DisplayEstBudget()
        {
            ddlCurrencyTypeEstBudget.Enabled = true;
            LoadCurrencyEstimate();
            ddlCurrencyTypeEstBudget.ClearSelection();

            if (ddlCurrencyTypeEstBudget.Items.FindByValue(ViewState["BudgetCurrency"].ToString()) != null)
            {
                ddlCurrencyTypeEstBudget.Items.FindByValue(ViewState["BudgetCurrency"].ToString()).Selected = true;
            }
            ddlCurrencyTypeEstBudget.Enabled = false;
        }
        /// <summary>
        /// clears input fields and loads default values
        /// </summary>
        private void ClearDetails()
        {

            txtProjectCode.Text = "";
            txtProjectName.Text = "";
            txtObjective.Text = "";
            txtTotalEstBudget.Text = "";
            dpProjectStartDate.Text = "";
            dpProjectEndDate.Text = "";
            txtDollervalue.Text = "";
            txtpercentage.Text = string.Empty;
            if (ddlCurrencyTypeTotalEstBudget.Items.Count > 0)
                ddlCurrencyTypeTotalEstBudget.SelectedIndex = 0;

            //ddlProjectStatus.ClearSelection();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + dpProjectStartDate.ClientID + "');ClearDateField('" + dpProjectEndDate.ClientID + "');", true);
        }
        #endregion


    }
}