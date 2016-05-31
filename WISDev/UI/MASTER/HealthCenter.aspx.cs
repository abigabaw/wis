using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class HealthCenter : System.Web.UI.Page
    {
        DataTable dt = new System.Data.DataTable();
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
                Master.PageHeader = "Health Center";
                ViewState["HEALTHCENTERID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                HealthCenterNameTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_HEALTH_CENTER) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdHealtCenter.Columns[grdHealtCenter.Columns.Count - 1].Visible = false;
                    grdHealtCenter.Columns[grdHealtCenter.Columns.Count - 2].Visible = false;
                    grdHealtCenter.Columns[grdHealtCenter.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in grdHealtCenter.Rows)
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
        private void BindGrid(bool addRow, bool deleteRow)
        {
            HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();
            grdHealtCenter.DataSource = HealthCenterBLLobj.GetALLHealthCenter();
            grdHealtCenter.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdHealtCenter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["HEALTHCENTERID"] = e.CommandArgument;
                GetHealthcenterDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {

                HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();
                message = HealthCenterBLLobj.DeleteHealthCenter(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(false);
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }
        /// <summary>
        /// To fetch details from  database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetHealthcenterDetails()
        {

            HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();
            int HealthCenterId = 0;

            if (ViewState["HEALTHCENTERID"] != null)
                HealthCenterId = Convert.ToInt32(ViewState["HEALTHCENTERID"]);

            HealthCenterBO HealthCenterBOobj = new HealthCenterBO();
            HealthCenterBOobj = HealthCenterBLLobj.GetHealthCenterById(HealthCenterId);

            HealthCenterNameTextBox.Text = HealthCenterBOobj.HEALTHCENTERNAME;
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ViewState["HEALTHCENTERID"].ToString() == "0")
            {
                HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    HealthCenterBO HealthCenterBOobj = new HealthCenterBO();
                    HealthCenterBOobj.HEALTHCENTERNAME = HealthCenterNameTextBox.Text.ToString().Trim();
                    HealthCenterBOobj.CREATEDBY = Convert.ToInt32(uID);

                    HealthCenterBLL BLLobj = new HealthCenterBLL();
                    message = BLLobj.Insert(HealthCenterBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    HealthCenterBLLobj = null;
                }

            }
            else
            {
                HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    HealthCenterBO HealthCenterBOobj = new HealthCenterBO();
                    HealthCenterBOobj.HEALTHCENTERNAME = HealthCenterNameTextBox.Text.ToString().Trim();
                    HealthCenterBOobj.HEALTHCENTERID = Convert.ToInt32(ViewState["HEALTHCENTERID"]);
                    HealthCenterBOobj.CREATEDBY = Convert.ToInt32(uID);

                    HealthCenterBLL BLLobj = new HealthCenterBLL();
                    message = BLLobj.Edit(HealthCenterBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    HealthCenterBLLobj = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["HEALTHCENTERID"] = "0";
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            HealthCenterNameTextBox.Text = string.Empty;
            ViewState["HEALTHCENTERID"] = 0;
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
                string HEALTHCENTERID = ((Literal)gr.FindControl("litHEALTHCENTERID")).Text;
                int HEALTHCENTERID_ = Convert.ToInt32(HEALTHCENTERID);
                HealthCenterBLL HealthCenterBLLobj = new HealthCenterBLL();
                message = HealthCenterBLLobj.ObsoleteHealthCenter(HEALTHCENTERID_, Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
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
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        ///To change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdHealtCenter.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
    }
}