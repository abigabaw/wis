using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ProjectFinance : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.FinancierInfo;

            if (!IsPostBack)
            {   
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Financiers Information";

                ViewState["FINANCE_ID"] = 0;
                BindFinanciers();

                getReasonofFinance();
                getNatureoffinance();
                getConditionsforfinance();
                txtFinancierName.Attributes.Add("onchange", "setDirty();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdFinances.Columns[grdFinances.Columns.Count - 1].Visible = false;
                    grdFinances.Columns[grdFinances.Columns.Count - 2].Visible = false;
                    grdFinances.Columns[grdFinances.Columns.Count - 3].Visible = false;
                }
            }
        }
        /// <summary>
        /// To fetch values to Condition dropdownlist
        /// </summary>
        private void getConditionsforfinance()
        {
            FinanceConditionBLL BLLobj = new FinanceConditionBLL();
            ddlCondition.DataSource = BLLobj.GetFinanceCondition();
            ddlCondition.DataTextField = "FINANCECONDITION";
            ddlCondition.DataValueField = "FINANCECONDITIONID";
            ddlCondition.DataBind();
            
        }
        /// <summary>
        /// To fetch values to Nature dropdownlist
        /// </summary>
        private void getNatureoffinance()
        {
            NatureofFinancingBLL BLLobj = new NatureofFinancingBLL();
            ddlNature.DataSource = BLLobj.GetNatureOfFinance();
            ddlNature.DataTextField = "FINANCENATURE";
            ddlNature.DataValueField = "FINANCENATUREID";
            ddlNature.DataBind();
        }
        /// <summary>
        /// To fetch values to Reason dropdownlist
        /// </summary>
        private void getReasonofFinance()
        {
            ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
            ddlReason.DataSource = BLLobj.GetReasonForFinance();
            ddlReason.DataTextField = "FINANCEREASON";
            ddlReason.DataValueField = "FINANCEREASONID";
            ddlReason.DataBind();
        }
        /// <summary>
        /// To bind data to grid from datasource
        /// </summary>
        protected void BindFinanciers()
        {
            grdFinances.DataSource = (new ProjectBLL()).GetProjectFinanciers(Convert.ToInt32(Session["PROJECT_ID"]));
            grdFinances.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            FinancierBO objFin = new FinancierBO();

            objFin.FinancierID = Convert.ToInt32(ViewState["FINANCE_ID"]);
            objFin.FinancierName = txtFinancierName.Text.Trim();
            objFin.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            objFin.FINANCECONDITIONID = Convert.ToInt32(ddlCondition.SelectedValue);
            objFin.FINANCENATUREID = Convert.ToInt32(ddlNature.SelectedValue);
            objFin.FINANCEREASONID = Convert.ToInt32(ddlReason.SelectedValue);
            

            ProjectBLL objProjectBLL = new ProjectBLL();

            if (objFin.FinancierID > 0)
            {
                objFin.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objProjectBLL.UpdateProjectFinancier(objFin);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }

                SetUpdateMode(false);
            }
            else
            {
                objFin.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objProjectBLL.AddProjectFinancier(objFin);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
            }

            ClearFields();
            BindFinanciers();

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Calls ClearFilds method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// clears input fileds and loads default values
        /// </summary>
        protected void ClearFields()
        {
            txtFinancierName.Text = "";
            ddlCondition.ClearSelection();
            ddlNature.ClearSelection();
            ddlReason.ClearSelection();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFinances_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "EditRow")
            {
                //ShowHideSections(true, false);
                ViewState["FINANCE_ID"] = e.CommandArgument;

                ProjectBLL objProjectBLL = new ProjectBLL();
                FinancierBO objFinancier = objProjectBLL.GetProjectFinancierByID(Convert.ToInt32(ViewState["FINANCE_ID"]));
                txtFinancierName.Text = objFinancier.FinancierName;
                
                ddlCondition.ClearSelection();
                if (ddlCondition.Items.FindByValue(objFinancier.FINANCECONDITIONID.ToString()) != null)
                    ddlCondition.Items.FindByValue(objFinancier.FINANCECONDITIONID.ToString()).Selected = true;

                ddlNature.ClearSelection();
                if (ddlNature.Items.FindByValue(objFinancier.FINANCENATUREID.ToString()) != null)
                    ddlNature.Items.FindByValue(objFinancier.FINANCENATUREID.ToString()).Selected = true;

                ddlReason.ClearSelection();
                if (ddlReason.Items.FindByValue(objFinancier.FINANCEREASONID.ToString()) != null)
                    ddlReason.Items.FindByValue(objFinancier.FINANCEREASONID.ToString()).Selected = true;

                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {

                string financeID = e.CommandArgument.ToString();
                ProjectBLL objProjectBLL = new ProjectBLL();
                message = objProjectBLL.DeleteProjectForFinance(Convert.ToInt32(financeID));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                ClearFields();
                SetUpdateMode(false);
                BindFinanciers();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdFinances_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFinances.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindFinanciers();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
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
                ViewState["FINANCE_ID"] = "0";
            }
        }
        /// <summary>
        /// Update Database Make data as Obsoluted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string financeID = ((Literal)gr.FindControl("litProjectFinanceID")).Text;
                ProjectBLL objProjectBLL = new ProjectBLL();

                message = objProjectBLL.ObsoleteProjectFinance(Convert.ToInt32(financeID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindFinanciers();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}