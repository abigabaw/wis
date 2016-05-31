using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;


namespace WIS
{
    public partial class FenceDescription : System.Web.UI.Page
    {
        FenceDescriptionBLL FenceDescriptionBLLObj = null;
        FenceDescriptionBO FenceDescriptionBOObj = null;
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
                Master.PageHeader = "Fence Description";
                ViewState["FenceID"] = 0;
                BindGrid(false, false);
                //txtFenceDescription.Attributes.Add("onchange", "isDirty = 1;");
                txtFenceDescription.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    dv_Details.Columns[3].Visible = false;
                    dv_Details.Columns[4].Visible = false;
                    dv_Details.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in dv_Details.Rows)
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
            FenceDescriptionBLLObj = new FenceDescriptionBLL();
            dv_Details.DataSource = FenceDescriptionBLLObj.GetAllFence();
            dv_Details.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            if (ConcernIDTextBox.Text == string.Empty)
            {
                FenceDescriptionBOObj = new FenceDescriptionBO();
                FenceDescriptionBOObj.FenceDescription = txtFenceDescription.Text.ToString();
                FenceDescriptionBOObj.Createdby = Convert.ToInt32(Session["USER_ID"].ToString());
                FenceDescriptionBLLObj = new FenceDescriptionBLL();
                message = FenceDescriptionBLLObj.insert(FenceDescriptionBOObj);

                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearFields();
                    BindGrid(true, true);
                }
                //BindGrid(true, true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Fence description details added successfully');", true);
                // ClearFields();
            }
            else
            {
                int reasonid = Convert.ToInt32(ViewState["FenceID"]);
                FenceDescriptionBOObj = new FenceDescriptionBO();
                FenceDescriptionBOObj.FenceDescription = txtFenceDescription.Text.ToString();
                FenceDescriptionBOObj.Createdby = Convert.ToInt32(Session["USER_ID"].ToString());
                FenceDescriptionBLLObj = new FenceDescriptionBLL();
                message = FenceDescriptionBLLObj.Update(FenceDescriptionBOObj, reasonid);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    BindGrid(true, true);
                    ClearFields();
                }
            }
                //BindGrid(true, true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Fence description details updated  successfully');", true);
                //ClearFields();
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            SetUpdateMode(false);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearFields()
        {
            txtFenceDescription.Text = string.Empty;
            ViewState["FenceID"] = "0";
            SetUpdateMode(false);
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

                string fenceID = ((Literal)gr.FindControl("litFenceID")).Text;
                FenceDescriptionBLLObj = new FenceDescriptionBLL();

                message = FenceDescriptionBLLObj.ObsoleteFenceDescription(Convert.ToInt32(fenceID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearFields();
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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Insert()
        {
            try
            {
                FenceDescriptionBOObj = new FenceDescriptionBO();
                FenceDescriptionBOObj.FenceDescription = txtFenceDescription.Text.ToString();
                FenceDescriptionBOObj.Createdby = 1;
                FenceDescriptionBLLObj = new FenceDescriptionBLL();
                FenceDescriptionBLLObj.insert(FenceDescriptionBOObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FenceDescriptionBLLObj = null;
            }
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        private void GetVulnerabiltyDetails()
        {
            FenceDescriptionBLLObj = new FenceDescriptionBLL();
            int FenceID = 0;
            if (ViewState["FenceID"] != null)
                FenceID = Convert.ToInt32(ViewState["FenceID"]);
            FenceDescriptionBOObj = new FenceDescriptionBO();
            FenceDescriptionBOObj = FenceDescriptionBLLObj.GetFencebyID(FenceID);
            txtFenceDescription.Text = FenceDescriptionBOObj.FenceDescription;
            ConcernIDTextBox.Text = FenceDescriptionBOObj.FenceID.ToString();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["FenceID"] = e.CommandArgument;
                int reasonID = Convert.ToInt32(ViewState["FenceID"]);
                GetVulnerabiltyDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;

                FenceDescriptionBLLObj = new FenceDescriptionBLL();
                
                message = FenceDescriptionBLLObj.Delete(Convert.ToInt32(e.CommandArgument));
                
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                
                ClearFields();
                SetUpdateMode(false);
                BindGrid(false, true);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
        }
        /// <summary>
        /// To Show page no in the grid
        /// </summary>
        protected void dv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dv_Details.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
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
                ViewState["FenceID"] = "0";
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
    }
}