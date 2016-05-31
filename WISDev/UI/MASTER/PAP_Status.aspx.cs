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
    public partial class PAP_Status : System.Web.UI.Page
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
                Master.PageHeader = "PAP Status";
                ViewState["PAPDESIGNATIONID"] = 0;  // ViewState ID
                BindGrid(false, false);
                pstatusTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MST_PROJECT) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdpstatus.Columns[2].Visible = false;
                    grdpstatus.Columns[3].Visible = false;
                    grdpstatus.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdpstatus.Rows)
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
            PstatusBLL PstatusBLLobj = new PstatusBLL();
            grdpstatus.DataSource = PstatusBLLobj.GetAllPstatus("");
            grdpstatus.DataBind();


        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdpstatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PAPDESIGNATIONID"] = e.CommandArgument;
                GetPstatusDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                PstatusBLL PstatusBLLobj = new PstatusBLL();
                string message = string.Empty;
  
                message = PstatusBLLobj.DeletePstatus(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                SetUpdateMode(false);
                clearfields();
                BindGrid(false, true);
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
                
                string PAPDESIGNATIONID = ((Literal)gr.FindControl("litPAPStatusID")).Text;
                PstatusBLL PstatusBLLobj = new PstatusBLL();
                message = PstatusBLLobj.ObsoletePAPStatus(Convert.ToInt32(PAPDESIGNATIONID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                BindGrid(false, true);

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
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            PstatusBLL BLLobj = null;

            try
            {
                BLLobj = new PstatusBLL();
                PstatusBO objPstatus = new PstatusBO();

                if (Convert.ToInt32(ViewState["PAPDESIGNATIONID"]) > 0)
                {
                    objPstatus.PAPDESIGNATION1 = pstatusTextBox.Text.Trim();
                    objPstatus.PAPDESIGNATIONID1 = Convert.ToInt32(ViewState["PAPDESIGNATIONID"]);

                    message = BLLobj.EDITPstatus(objPstatus);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }

                    SetUpdateMode(false);
                }
                else
                {
                    objPstatus.PAPDESIGNATION1 = pstatusTextBox.Text.Trim();
                    objPstatus.CREATEDBY1 = Convert.ToInt32(Session["USER_ID"]);
                    message = BLLobj.insert(objPstatus);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                }

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                clearfields();
                BindGrid(true, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                BLLobj = null;
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            pstatusTextBox.Text = string.Empty;
        }

        protected void grdpstatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdpstatus.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }

        // <summary>
        /// get the Grid value into textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void GetPstatusDetails()
        {
            PstatusBLL PstatusBLLobj = new PstatusBLL();
            int PAPDESIGNATIONID = 0;

            if (ViewState["PAPDESIGNATIONID"] != null)
                PAPDESIGNATIONID = Convert.ToInt32(ViewState["PAPDESIGNATIONID"]);

            PstatusBO PstatusObj = new PstatusBO();
            PstatusObj = PstatusBLLobj.GetPstatusById(PAPDESIGNATIONID);

            pstatusTextBox.Text = PstatusObj.PAPDESIGNATION1;
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
                ViewState["PAPDESIGNATIONID"] = "0";
            }
        }

        //protected void grdpstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string isDeleted = DataBinder.Eval(e.Row.DataItem, "ISDELETED1").ToString();
        //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");

        //        if (isDeleted.ToUpper() == "TRUE")
        //        {
        //            imgDelete.Visible = false;
        //            e.Row.ForeColor = System.Drawing.Color.Red;
        //        }
        //    }
        //}
    }
}