using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class MNEGoalElements : System.Web.UI.Page
    {
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
                Master.PageHeader = "M&amp;E Goal Element";
                BindGrid();
                ViewState["GOALELEMENTID"] = 0;

                txtGoalElement.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MNE) == false)
                {
                    btnSave.Visible = false;
                    btnClr.Visible = false;
                    gvGoalElement.Columns[2].Visible = false;
                    gvGoalElement.Columns[3].Visible = false;
                    gvGoalElement.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in gvGoalElement.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)grRow.FindControl("IsObsolete");
                            chk.Enabled = false;
                        }
                    }
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
            MNEGoalElementBOL MNEGoalElementBOObj = null;
            string message = "";
            if (Convert.ToInt32(ViewState["GOALELEMENTID"]) == 0)
            {
                try
                {
                    MNEGoalElementBOObj = new MNEGoalElementBOL();
                    MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();
                    MNEGoalElementBOObj.GoalElement = txtGoalElement.Text.Trim();
                    MNEGoalElementBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = MNEGoalElementBLLObj.InsertMNEGoalElementDetails(MNEGoalElementBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    MNEGoalElementBOObj = new MNEGoalElementBOL();
                    MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();

                    MNEGoalElementBOObj.GoalElementID = Convert.ToInt32(ViewState["GOALELEMENTID"]);
                    MNEGoalElementBOObj.GoalElement = txtGoalElement.Text.ToString().Trim();
                    MNEGoalElementBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = MNEGoalElementBLLObj.UpdateGoalElement(MNEGoalElementBOObj);
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
                    MNEGoalElementBOObj = null;
                }
            }

            BindGrid();
            ClearData();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtGoalElement.Text = string.Empty;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();
            gvGoalElement.DataSource = MNEGoalElementBLLObj.GetAllMNEGoalElementDetails();
            gvGoalElement.DataBind();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClr.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClr.Text = "Clear";
                ViewState["GOALELEMENTID"] = "0";
            }
        }


        /// <summary>
        /// to display pageno in grid
        /// </summary>

        protected void gvGoalElement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGoalElement.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalElement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["GOALELEMENTID"] = e.CommandArgument;

                MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();
                MNEGoalElementBOL MNEGoalElementBOLObj = new MNEGoalElementBOL();
                MNEGoalElementBOLObj = MNEGoalElementBLLObj.GetMNEGoalElementDetailsbyID(Convert.ToInt32(ViewState["GOALELEMENTID"]));
                txtGoalElement.Text = MNEGoalElementBOLObj.GoalElement;

                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();
                message = MNEGoalElementBLLObj.DeleteGoalElement(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                SetUpdateMode(false);
                BindGrid();
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

                int GoalID = Convert.ToInt32(((Literal)gr.FindControl("litGOALID")).Text);
                MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();

                message = MNEGoalElementBLLObj.ObsoleteGoalElement(GoalID, Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calls clear method
        /// </summary>
        ///  <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
    }
}