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
    public partial class StructureCondition : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        StructureConditionBO objStructureCondition;
        StructureConditionBLL objStructureConditionBLL;
        #endregion

        #region Page Load
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
                Master.PageHeader = "Structure Conditions";
                ViewState["StructureConditionID"] = 0;
                BindGrid(false, false);
                //txtStructureCondition.Attributes.Add("onchange", "isDirty = 1;");
                //ClearDetails();
                txtStructureCondition.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdStructureCondition.Columns[3].Visible = false;
                    grdStructureCondition.Columns[4].Visible = false;
                    grdStructureCondition.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdStructureCondition.Rows)
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
        #endregion

        #region Load Default Values
        //Required when Data Loaded in Controls
        #endregion

        #region Clear
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            //Clearing TextBoxes
            txtStructureCondition.Text = string.Empty;

            //Setting Default Index Selected Value "0" to DropDowns

            //Clearing Viewstate Values 
            ViewState["StructureConditionID"] = "0";
        }

        #endregion

        #region Load Grid / Bind Grid
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objStructureConditionBLL = new StructureConditionBLL();
            objStructureCondition = new StructureConditionBO();

            objStructureCondition.StructureConditionName = string.Empty;
            objStructureCondition.StructureConditionID = 0;

            grdStructureCondition.DataSource = objStructureConditionBLL.GetAllStructureCondition();//(objStructureCondition);
            grdStructureCondition.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStructureCondition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["StructureConditionID"] = e.CommandArgument;
                GetStructureConditionDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteStructureCondition(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// to display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStructureCondition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStructureCondition.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion

        #region Edit Record
        /// <summary>
        /// to fetch details from database and assign to textbox
        /// </summary>
        private void GetStructureConditionDetails()
        {
            objStructureConditionBLL = new StructureConditionBLL();
            int StructureConditionID = 0;

            if (ViewState["StructureConditionID"] != null)
                StructureConditionID = Convert.ToInt32(ViewState["StructureConditionID"].ToString());

            objStructureCondition = new StructureConditionBO();
            objStructureCondition = objStructureConditionBLL.GetStructureConditionById(StructureConditionID);

            txtStructureCondition.Text = objStructureCondition.StructureConditionName;
        }
        #endregion

        #region Delete Record
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

                string structureConditionID = ((Literal)gr.FindControl("litStructureConditionID")).Text;
                objStructureConditionBLL = new StructureConditionBLL();

                message = objStructureConditionBLL.ObsoleteStructureCondition(Convert.ToInt32(structureConditionID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
                BindGrid(false, true);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To soft delete data from database
        /// </summary>
     
        private void DeleteStructureCondition(string conditionID)
        {
            objStructureConditionBLL = new StructureConditionBLL();
            string message = string.Empty;

            message = objStructureConditionBLL.DeleteStructureCondition(Convert.ToInt32(conditionID));

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data deleted successfully";
            ClearDetails();
            BindGrid(false, true);
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        #endregion

        #region Save Record
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objStructureCondition = new StructureConditionBO();
            objStructureConditionBLL = new StructureConditionBLL();

            //Assignement
            objStructureCondition.StructureConditionName = txtStructureCondition.Text.Trim();

            if (ViewState["StructureConditionID"] != null)
                objStructureCondition.StructureConditionID = Convert.ToInt32(ViewState["StructureConditionID"].ToString());

            objStructureCondition.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objStructureCondition.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objStructureCondition.StructureConditionID < 1)
            {
                //If StructureConditionID does Not exists then SaveStructureCondition
                objStructureCondition.StructureConditionID = -1;//For New StructureCondition
                message = objStructureConditionBLL.AddStructureCondition(objStructureCondition);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully.";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            else
            {
                //If StructureConditionID exists then UpdateStructureCondition
                message = objStructureConditionBLL.UpdateStructureCondition(objStructureCondition); //For Updating StructureCondition
                

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully.";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }

            if (message != null)
            {
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }

            ClearDetails();
            SetUpdateMode(false);
        }
        #endregion
        /// <summary>
        /// to change text of the button based on condition
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
                ViewState["StructureConditionID"] = "0";
            }
        }
    }
}