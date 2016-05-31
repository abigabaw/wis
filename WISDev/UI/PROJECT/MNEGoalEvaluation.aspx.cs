using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS.UI.PROJECT
{
    public partial class MNEGoalEvaluation : System.Web.UI.Page
    {

        #region PageEvents
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                string userName = (Session["USERNAME"].ToString());
              
            }
            if (!IsPostBack)
            {
                txtNarrative.Attributes.Add("MaxLength", txtNarrative.MaxLength.ToString());
             //   Master.PageHeader = "MNE Goal Evaluation";

                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " M & E Goal Evaluation";
                    string projectID = Session["PROJECT_ID"].ToString();
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/UI/Project/ViewProjects.aspx"));
                }
                ViewState["EVALUATIONID"] = 0;
                BindGrid(false, false);
                BindGoal();
                txtDescription.Attributes.Add("onchange", "setDirty();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                btnClear.Attributes.Add("onclick", "isDirty = 0;");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.MNE_GOAL_EVAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gvGoalEval.Columns[gvGoalEval.Columns.Count - 1].Visible = false;
                    gvGoalEval.Columns[gvGoalEval.Columns.Count - 2].Visible = false;

                }
            }
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            string uID = Session["USER_ID"].ToString();
            string projectID = Session["PROJECT_ID"].ToString();
            MNEGoalEvaluationBLL objMNEGoalEvaluationBLL = new MNEGoalEvaluationBLL();
            if (Convert.ToInt32(ViewState["EVALUATIONID"]) == 0)
            {
                try
                {

                    MNEGoalEvaluationBO objMNEGoalEvaluationBO = new MNEGoalEvaluationBO();
                    objMNEGoalEvaluationBO.ProjectID = Convert.ToInt32(projectID);
                    objMNEGoalEvaluationBO.GoalID = Convert.ToInt32(ddlGoal.SelectedValue);
                    objMNEGoalEvaluationBO.GoalDescription = txtDescription.Text.Trim();
                    if (txtNarrative.Text.Length <= 1999)
                    {
                        objMNEGoalEvaluationBO.GoalNarrative = txtNarrative.Text.Trim();
                    }
                    else {
                        objMNEGoalEvaluationBO.GoalNarrative = txtNarrative.Text.Substring(0, 1999);

                    }
                    objMNEGoalEvaluationBO.CreatedBy = Convert.ToInt32(uID);
                    objMNEGoalEvaluationBO.IsDeleted = "False";
                    message = objMNEGoalEvaluationBLL.InsertMNEGoalEval(objMNEGoalEvaluationBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                    // txtOptionGroup.Text = "0";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objMNEGoalEvaluationBLL = null;
                }

                BindGrid(true, true);
            }
            //edit the data in the textbox exiting in the Grid
            else if (Convert.ToInt32(ViewState["EVALUATIONID"]) > 0)
            {
                try
                {

                    MNEGoalEvaluationBO objMNEGoalEvaluationBO = new MNEGoalEvaluationBO();
                    objMNEGoalEvaluationBO.ProjectID = Convert.ToInt32(projectID);
                    objMNEGoalEvaluationBO.EvaluationID = Convert.ToInt32(ViewState["EVALUATIONID"]);
                    objMNEGoalEvaluationBO.GoalID = Convert.ToInt32(ddlGoal.SelectedValue);
                    objMNEGoalEvaluationBO.GoalDescription = txtDescription.Text.Trim();
                    objMNEGoalEvaluationBO.GoalNarrative = txtNarrative.Text.Trim();
                    objMNEGoalEvaluationBO.UpdatedBy = Convert.ToInt32(uID);

                    message = objMNEGoalEvaluationBLL.UpdateMNEGoalEvaluation(objMNEGoalEvaluationBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objMNEGoalEvaluationBLL = null;
                }

                BindGrid(true, true);
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            ClearData();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        #endregion

        #region GridEvents
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalEval_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {
                ViewState["EVALUATIONID"] = e.CommandArgument;
                GetMNEGoalEval();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteMNEGoalEval(e.CommandArgument.ToString());
                ClearData();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// used to display page no in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalEval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGoalEval.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }

        /// <summary>
        /// to link to another page onclick of lnkEvalElement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalEval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkEvalElement = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkEvalElement");
                int evalID = (int)DataBinder.Eval(e.Row.DataItem, "EvaluationID");
                string goalName = (string)DataBinder.Eval(e.Row.DataItem, "GoalName");
                lnkEvalElement.Attributes.Add("onclick", "OpenEvalElements('" + evalID + "','" +goalName+ "');");              
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            MNEGoalEvaluationBLL objMNEGoalEvaluationBLL = new MNEGoalEvaluationBLL();
            gvGoalEval.DataSource = objMNEGoalEvaluationBLL.GetMNEGoalEvaluation(Convert.ToInt32(Session["PROJECT_ID"]));
            gvGoalEval.DataBind();
        }
        /// <summary>
        /// to bind data to goal dropdownlist
        /// </summary>
        private void BindGoal()
        {
            ddlGoal.DataSource = (new MNEGoalBLL()).GetActiveMNEGoalNames();
            ddlGoal.DataTextField = "GoalName";
            ddlGoal.DataValueField = "GoalID";
            ddlGoal.DataBind();
        }
        /// <summary>
        /// method to softdelete data from database
        /// </summary>
        /// <param name="evaluationID"></param>
        private void DeleteMNEGoalEval(string evaluationID)
        {
            MNEGoalEvaluationBLL objMNEGoalEvaluationBLL = new MNEGoalEvaluationBLL();

            string message = string.Empty;
            try
            {
                message = objMNEGoalEvaluationBLL.DeleteMNEGoalEvaluation(Convert.ToInt32(evaluationID));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// method fetch details from grid and assign to textbox
        /// </summary>
        private void GetMNEGoalEval()
        {
            MNEGoalEvaluationBLL objMNEGoalEvaluationBLL = new MNEGoalEvaluationBLL();
            int EvaluationId = 0;

            if (ViewState["EVALUATIONID"] != null)
                EvaluationId = Convert.ToInt32(ViewState["EVALUATIONID"]);

            MNEGoalEvaluationBO objMNEGoalEvaluationBO = new MNEGoalEvaluationBO();

            objMNEGoalEvaluationBO = objMNEGoalEvaluationBLL.GetMNEGoalEvaluationByID(EvaluationId);

            ddlGoal.ClearSelection();
            if (ddlGoal.Items.FindByValue(objMNEGoalEvaluationBO.GoalID.ToString()) != null)
                ddlGoal.Items.FindByValue(objMNEGoalEvaluationBO.GoalID.ToString()).Selected = true;
            ViewState["EVALUATIONID"] = objMNEGoalEvaluationBO.EvaluationID;
            txtDescription.Text = objMNEGoalEvaluationBO.GoalDescription;
            txtNarrative.Text = objMNEGoalEvaluationBO.GoalNarrative;
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
                ViewState["EVALUATIONID"] = 0;
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlGoal.SelectedIndex = 0;
            txtDescription.Text = string.Empty;
            txtNarrative.Text = string.Empty;
        }

        #endregion

    }
}