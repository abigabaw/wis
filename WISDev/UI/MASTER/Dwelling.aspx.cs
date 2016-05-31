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
    public partial class Dwelling : System.Web.UI.Page
    {
        DwellingBO DwellingBOobj = null;
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
                Master.PageHeader = "Dwelling";
                ViewState["DwellingID"] = 0;
                BindGrid(false, false);
                //txtDwelling.Attributes.Add("onchange", "isDirty = 1;");
                txtDwelling.Attributes.Add("onchange", "setDirtyText();");
               

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gv_Details.Columns[2].Visible = false;
                    gv_Details.Columns[3].Visible = false;
                    gv_Details.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in gv_Details.Rows)
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
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ConcernIDTextBox.Text == string.Empty)
            {

                message = Insert();

                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    cleardetails();
                    BindGrid(true, true);
                }

                //BindGrid(true, true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Dwelling details added successfully');", true);
                //cleardetails();
            }
            else
            {
                string uID = Session["USER_ID"].ToString();
                int reasonid = Convert.ToInt32(ViewState["DwellingID"]);
                DwellingBOobj = new DwellingBO();
                DwellingBOobj.DwellingType = txtDwelling.Text.ToString();
                DwellingBOobj.Createdby = Convert.ToInt32(uID);
                DwellingBLL DwellingBLLObj = new DwellingBLL();

                message = DwellingBLLObj.Update(DwellingBOobj, reasonid);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    // ClearDetails();
                    cleardetails();
                    BindGrid(true, true);
                    SetUpdateMode(false);
                }

                //BindGrid(true, true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Dwelling details updated successfully');", true);
                //cleardetails();
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void cleardetails()
        {
            txtDwelling.Text = string.Empty;
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string Insert()
        {
            string message = string.Empty;
            string uID = Session["USER_ID"].ToString();
            DwellingBOobj = new DwellingBO();
            DwellingBOobj.DwellingType = txtDwelling.Text.ToString();
            DwellingBOobj.Createdby = Convert.ToInt32(uID);
            DwellingBLL DwellingBLLObj = new DwellingBLL();
            try
            {

                message = DwellingBLLObj.Insert(DwellingBOobj);

            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
                Response.Write(errorMsg);

            }
            finally
            {
                DwellingBLLObj = null;
            }
            return message;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            DwellingBLL DwellingBLLObj = new DwellingBLL();
            gv_Details.DataSource = DwellingBLLObj.GetAllDwelling();
            gv_Details.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["DwellingID"] = e.CommandArgument;
                int reasonID = Convert.ToInt32(ViewState["DwellingID"]);
                GetDwellingDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DwellingBLL DwellingBLLObj = new DwellingBLL();
                message = DwellingBLLObj.DeleteDwelling(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(false);
                cleardetails();
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
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
                ViewState["DwellingID"] = ((Literal)gr.FindControl("litDwellingID")).Text;
                DwellingBLL DwellingBLLObj = new DwellingBLL();
                message = DwellingBLLObj.ObsoleteDwelling(Convert.ToInt32(ViewState["DwellingID"]), Convert.ToString(chk.Checked));
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
        /// to get details 
        /// </summary>
        /// <returns></returns>
        private void GetDwellingDetails()
        {
            DwellingBLL DwellingBLLObj = new DwellingBLL();
            int DwellingID = 0;

            if (ViewState["DwellingID"] != null)
                DwellingID = Convert.ToInt32(ViewState["DwellingID"]);

            DwellingBOobj = new DwellingBO();
            DwellingBOobj = DwellingBLLObj.GetDwellingbyID(DwellingID);

            txtDwelling.Text = DwellingBOobj.DwellingType;
            ConcernIDTextBox.Text = DwellingBOobj.DwellingID.ToString();
            //int ConcernID_test = Convert.ToInt32(ConcernObj.ConcernID);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            cleardetails();
            ConcernIDTextBox.Text = string.Empty;
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// To shoe pageno in grid
        /// </summary>
        protected void gv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Details.PageIndex = e.NewPageIndex;
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
                ViewState["DwellingID"] = "0";
            }
        }
    }
}