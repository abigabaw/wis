using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class MNEGoalEvalElements : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                Master.PageHeader = "M & E Goal Evaluation Element Details";
                
                lblGoal.Text = Request.QueryString["goalName"];
                ViewState["EVALUATIONID"] = Request.QueryString["EvalID"];
                ViewState["EVALELEMENTID"] = 0;
                BindElements();
                BindGrid();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.MNE_GOAL_EVAL) == false)
                {
                    btnClear.Visible = false;
                    btnSave.Visible = false;
                    gvGoalEvalElement.Columns[gvGoalEvalElement.Columns.Count - 1].Visible = false;
                    gvGoalEvalElement.Columns[gvGoalEvalElement.Columns.Count - 1].Visible = false;

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
            MNEGoalEvalElementsBLL objMNEGoalEvalElementsBLL = new MNEGoalEvalElementsBLL();
            if (Convert.ToInt32(ViewState["EVALELEMENTID"]) == 0)
            {
                try
                {
                    MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = new MNEGoalEvalElementsBO();
                    objMNEGoalEvalElementsBO.EvaluationID = Convert.ToInt32(ViewState["EVALUATIONID"]);
                    objMNEGoalEvalElementsBO.Goal_elementID = Convert.ToInt32(ddlElement.SelectedValue);
                    objMNEGoalEvalElementsBO.Evalelementdescriptionn = txtDescription.Text.Trim();
                    objMNEGoalEvalElementsBO.Createdby = Convert.ToInt32(uID);
                    objMNEGoalEvalElementsBO.Isdeleted = "False";
                    message = objMNEGoalEvalElementsBLL.InsertMNEGoalEvalElements(objMNEGoalEvalElementsBO);

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
                    objMNEGoalEvalElementsBLL = null;
                }

                BindGrid();
            }
            //edit the data in the textbox exiting in the Grid
            else if (Convert.ToInt32(ViewState["EVALELEMENTID"]) > 0)
            {
                try
                {

                    MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = new MNEGoalEvalElementsBO();
                    objMNEGoalEvalElementsBO.EvaluationID = Convert.ToInt32(ViewState["EVALUATIONID"]);
                    objMNEGoalEvalElementsBO.EvalelementID = Convert.ToInt32(ViewState["EVALELEMENTID"]);
                    objMNEGoalEvalElementsBO.Goal_elementID = Convert.ToInt32(ddlElement.SelectedValue);
                    objMNEGoalEvalElementsBO.Evalelementdescriptionn = txtDescription.Text.Trim();
                    objMNEGoalEvalElementsBO.Updatedby = Convert.ToInt32(uID);


                    message = objMNEGoalEvalElementsBLL.UpdateMNEGoalEvalElements(objMNEGoalEvalElementsBO);

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
                    objMNEGoalEvalElementsBLL = null;
                }

                BindGrid();
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            ClearData();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
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
        protected void gvGoalEvalElement_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {
                ViewState["EVALELEMENTID"] = e.CommandArgument;
                GetMNEGoalEval();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteMNEGoalEval(e.CommandArgument.ToString());
                ClearData();
                SetUpdateMode(false);
                BindGrid();
            }
        }
        /// <summary>
        /// To set pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalEvalElement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGoalEvalElement.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid();
        }

        #endregion



        #region Methods
        /// <summary>
        /// To bind values to Element dropdownlist
        /// </summary>
        private void BindElements()
        {
            ddlElement.DataSource = (new MNEGoalElementBLL()).LoadMNEGoalElement();
            ddlElement.DataTextField = "GoalElement";
            ddlElement.DataValueField = "GoalElementID";
            ddlElement.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            MNEGoalEvalElementsBLL objMNEGoalEvalElementsBLL = new MNEGoalEvalElementsBLL();
            gvGoalEvalElement.DataSource = objMNEGoalEvalElementsBLL.GetMNEGoalEvalElements(Convert.ToInt32(ViewState["EVALUATIONID"]));
            gvGoalEvalElement.DataBind();
        }
        /// <summary>
        /// method to soft delete data from database
        /// </summary>
        /// <param name="evalElementID"></param>
        private void DeleteMNEGoalEval(string evalElementID)
        {
            MNEGoalEvalElementsBLL objMNEGoalEvalElementsBLL = new MNEGoalEvalElementsBLL();
            string message = string.Empty;
            try
            {
                message = objMNEGoalEvalElementsBLL.DeleteMNEGoalEvalElements(Convert.ToInt32(evalElementID));

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
        /// method to fetch details and assign to textbox
        /// </summary>
        private void GetMNEGoalEval()
        {
            MNEGoalEvalElementsBLL objMNEGoalEvalElementsBLL = new MNEGoalEvalElementsBLL();
            int EvalelementId = 0;

            if (ViewState["EVALELEMENTID"] != null)
                EvalelementId = Convert.ToInt32(ViewState["EVALELEMENTID"]);

            MNEGoalEvalElementsBO objMNEGoalEvalElementsBO = new MNEGoalEvalElementsBO();

            objMNEGoalEvalElementsBO = objMNEGoalEvalElementsBLL.GetMNEGoalEvalElementsByID(EvalelementId);

            ddlElement.ClearSelection();
            if (ddlElement.Items.FindByValue(objMNEGoalEvalElementsBO.Goal_elementID.ToString()) != null)
                ddlElement.Items.FindByValue(objMNEGoalEvalElementsBO.Goal_elementID.ToString()).Selected = true;
            ViewState["EVALELEMENTID"] = objMNEGoalEvalElementsBO.EvalelementID;
            txtDescription.Text = objMNEGoalEvalElementsBO.Evalelementdescriptionn;
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtDescription.Text = string.Empty;
            ddlElement.SelectedIndex = 0;
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
                ViewState["EVALELEMENTID"] = 0;
            }
        }
        #endregion



    }
}