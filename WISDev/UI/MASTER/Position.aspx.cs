using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.MASTER
{
    public partial class Position : System.Web.UI.Page
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
                Master.PageHeader = "Position";
                ViewState["PositionID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                txtPositionID.Text = "0";
               // txtPosition.Attributes.Add("onchange", "isDirty = 1;");
                txtPosition.Attributes.Add("onchange", "setDirtyText();");
               
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_POSITION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdPosition.Columns[2].Visible = false;
                    grdPosition.Columns[3].Visible = false;
                    grdPosition.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdPosition.Rows)
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
            string AlertMessage = string.Empty;
            string message = string.Empty;
            PositionBLL PositionBLLOBJ = new PositionBLL();
            PositionBO PositionBOObj = new PositionBO();

            int uID = Convert.ToInt32(Session["USER_ID"].ToString());
            if (txtPositionID.Text.ToString().Trim() == "0")
            {               
                try
                {                   
                    PositionBOObj.PositionName = txtPosition.Text.ToString().Trim();
                    PositionBOObj.UserID = uID;
                    message = PositionBLLOBJ.InsertPosition(PositionBOObj);

                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                    {
                        Clear();
                        BindGrid(true, true);
                        txtPositionID.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    PositionBLLOBJ = null;
                }
            }
            else
            {
                try
                {
                    PositionBOObj.PositionName = txtPosition.Text.ToString().Trim();
                    PositionBOObj.PositionID = Convert.ToInt32(txtPositionID.Text.ToString().Trim());
                    PositionBOObj.UserID = Convert.ToInt32(uID);

                    message = PositionBLLOBJ.EDITPOSITION(PositionBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        Clear();
                        SetUpdateMode(false);
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    PositionBLLOBJ = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            PositionBLL PositionBLLobj = new PositionBLL();
            grdPosition.DataSource = PositionBLLobj.GetAllPositions();
            grdPosition.DataBind();
        }
        /// <summary>
        ///change Page in the Grid
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdPosition.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
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
                PositionBLL PositionBLLobj = new PositionBLL();

                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                string positionID = ((Literal)gr.FindControl("litPositionID")).Text;

                message = PositionBLLobj.ObsoletePosition(Convert.ToInt32(positionID), Convert.ToString(chk.Checked));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                Clear();
                SetUpdateMode(false);
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPosition_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["PositionID"] = e.CommandArgument;
                GetPositionByID();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                PositionBLL PositionBLLobj = new PositionBLL();

                message = PositionBLLobj.DeletePosition(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                Clear();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }
        /// <summary>
        /// To get position from database based on ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPositionByID()
        {
            PositionBLL PositionBLLobj = new PositionBLL();
            int PositionID = 0;

            if (ViewState["PositionID"] != null)
                PositionID = Convert.ToInt32(ViewState["PositionID"]);

            PositionBO PositionObj = new PositionBO();
            PositionObj = PositionBLLobj.GetPositionById(PositionID);

            txtPosition.Text = PositionObj.PositionName;
            txtPositionID.Text = PositionObj.PositionID.ToString();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        public void Clear()
        {
            txtPosition.Text = "";
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
                ViewState["PositionID"] = "0";
                txtPositionID.Text = "0";
            }
        }

    }
}