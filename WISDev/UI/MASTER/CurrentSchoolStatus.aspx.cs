using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;


namespace WIS
{
    public partial class CurrentSchoolStatus : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
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
                Master.PageHeader = "Current School Status";
                ViewState["CurrentSchoolStatusID"] = 0;
                BindGrid();
                txtCurrSchlStatus.Attributes.Add("onchange", "setDirtyText();");
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_EDUCATION) == false)
                {
                    
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    gvCurSchlStatus.Columns[3].Visible = false;
                    gvCurSchlStatus.Columns[4].Visible = false;
                    gvCurSchlStatus.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in gvCurSchlStatus.Rows)
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e

        private void BindGrid()
        {
            CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();
            gvCurSchlStatus.DataSource = CurrentSchoolStatusBLLObj.GetAllSchoolDetails();
            gvCurSchlStatus.DataBind();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>

        protected void ClearButton_Click(object sender, EventArgs e)
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
        private void ClearDetails()
        {
            txtCurrSchlStatus.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtCurrSchlStatusID.Text = string.Empty;
        }
        /// <summary>
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CurrentSchoolStatusBO CurrentSchoolStatusBOObj = new CurrentSchoolStatusBO();

            CurrentSchoolStatusBOObj.CurrentSchoolStatus = txtCurrSchlStatus.Text.ToString().Trim();
            CurrentSchoolStatusBOObj.Description = txtDescription.Text.ToString().Trim();
            SaveDetails(CurrentSchoolStatusBOObj);
            BindGrid();
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDetails(CurrentSchoolStatusBO CurrentSchoolStatusBOObj)
        {
            //int count = 0;
            string AlertMessage = string.Empty;
            string message = string.Empty;
            try
            {
                if (txtCurrSchlStatusID.Text.ToString().Trim() == string.Empty)
                {
                    try
                    {
                        CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();
                        CurrentSchoolStatusBOObj = new CurrentSchoolStatusBO();

                        CurrentSchoolStatusBOObj.CurrentSchoolStatus = txtCurrSchlStatus.Text.ToString().Trim();
                        string sDes = "";
                        if (txtDescription.Text.ToString().Trim().Length > 100)
                            sDes = txtDescription.Text.ToString().Trim().Substring(0,100);
                        else
                            sDes = txtDescription.Text.ToString().Trim();
                        CurrentSchoolStatusBOObj.Description = sDes;
                        CurrentSchoolStatusBOObj.CurrentSchoolStatusID = Convert.ToInt32(ViewState["CurrentSchoolStatusID"]);
                        CurrentSchoolStatusBOObj.Createdby = Convert.ToInt32(Session["USER_ID"]);

                        message = CurrentSchoolStatusBLLObj.InsertSchoolStatusDetails(CurrentSchoolStatusBOObj);

                        AlertMessage = "alert('" + message + "');";

                        if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        {
                            message = "Data saved successfully";
                            BindGrid();
                            ClearDetails();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else //if (Convert.ToInt32(ViewState["CurrentSchoolStatusID"]) != 0)
                {
                    CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();
                    CurrentSchoolStatusBOObj = new CurrentSchoolStatusBO();

                    int EditCurSchStatusID = Convert.ToInt32(ViewState["CurrentSchoolStatusID"]);

                    CurrentSchoolStatusBOObj.CurrentSchoolStatus = txtCurrSchlStatus.Text.ToString().Trim();
                    string sDes = "";
                    if (txtDescription.Text.ToString().Trim().Length > 100)
                        sDes = txtDescription.Text.ToString().Trim().Substring(0, 100);
                    else
                        sDes = txtDescription.Text.ToString().Trim();
                    CurrentSchoolStatusBOObj.Description = sDes;
                    CurrentSchoolStatusBOObj.CurrentSchoolStatusID = Convert.ToInt32(ViewState["CurrentSchoolStatusID"]);
                    CurrentSchoolStatusBOObj.Createdby = Convert.ToInt32(Session["USER_ID"]);

                    message = CurrentSchoolStatusBLLObj.EditCurSchStatus(CurrentSchoolStatusBOObj, EditCurSchStatusID);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        BindGrid();
                        ClearDetails();
                        SetUpdateMode(false);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                CurrentSchoolStatusBOObj = null;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCurSchlStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CurrentSchoolStatusID"] = e.CommandArgument;
                int CurSchlStatusEditID = Convert.ToInt32(ViewState["SUPPORTEDBYID"]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                if (row != null)
                {
                    txtCurrSchlStatus.Text = row.Cells[1].Text.ToString();
                    txtDescription.Text = row.Cells[2].Text.ToString();
                }
                GetCurSchlStatusDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteCurSchlStatus(Convert.ToInt32(e.CommandArgument));
                ClearDetails();
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

                string CurrentSchoolStatusID = ((Literal)gr.FindControl("litSchoolStatusID")).Text;
                CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();
                message = CurrentSchoolStatusBLLObj.ObsoleteSchoolStatus(Convert.ToInt32(CurrentSchoolStatusID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To delete currentschool status based on ID
        /// </summary>
        private void DeleteCurSchlStatus(int CurrentSchoolStatusID)
        {
            string message = string.Empty;
            CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();

            message = CurrentSchoolStatusBLLObj.DeleteCurSchlStatus(CurrentSchoolStatusID);
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To fetch currentschool status and asssign to textbox
        /// </summary>
        private void GetCurSchlStatusDetails()
        {
            CurrentSchoolStatusBLL CurrentSchoolStatusBLLObj = new CurrentSchoolStatusBLL();

            int CurrentSchoolStatusID = 0;

            if (ViewState["CurrentSchoolStatusID"] != null)
                CurrentSchoolStatusID = Convert.ToInt32(ViewState["CurrentSchoolStatusID"].ToString());

            CurrentSchoolStatusBO CurrentSchoolStatusBOObj = CurrentSchoolStatusBLLObj.GetCurSchlStatusById(CurrentSchoolStatusID);

            txtCurrSchlStatus.Text = CurrentSchoolStatusBOObj.CurrentSchoolStatus;
            txtDescription.Text = CurrentSchoolStatusBOObj.Description;
            txtCurrSchlStatusID.Text = CurrentSchoolStatusBOObj.CurrentSchoolStatusID.ToString();
        }
        /// <summary>
        /// To set pageno in grid
        /// </summary>
        protected void gvCurSchlStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCurSchlStatus.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                SaveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                SaveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["CurrentSchoolStatusID"] = "0";
            }
        }
    }
}