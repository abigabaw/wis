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
    public partial class Literacy_Status : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        LiteracyStatusBO LitStatusBoobj;
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                //Retrieving UserName from Session
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Literacy Status";
                ViewState["LitStatusID"] = 0;
                BindGrid(false, false);
                ClearDetails();
          
                txtLiteracyStatus.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_EDUCATION) == false)
                {

                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLitStatus.Columns[3].Visible = false;
                    grdLitStatus.Columns[4].Visible = false;
                    grdLitStatus.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdLitStatus.Rows)
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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearDetails()
        {
            txtDescription.Text = string.Empty;
            txtLiteracyStatus.Text = string.Empty;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            LiteracyStatusBLL LiteracyStatusBLLobj = new LiteracyStatusBLL();
            grdLitStatus.DataSource = LiteracyStatusBLLobj.GetAllLiteracyStatus();
            grdLitStatus.DataBind();
        }
        /// <summary>
        /// To call insert method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            LiteracyStatusBLL LiteracyStatusBLLobj = null;

            try
            {
                if (btnSave.Text == "Save")
                {
                    message = InsertLitStatusDetail();

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearDetails();
                    }
                    BindGrid(true, true);
                }
                else
                {
                    int litStatusID = Convert.ToInt32(ViewState["LitStatusID"]);
                    LitStatusBoobj = new LiteracyStatusBO();
                    LitStatusBoobj.LTR_STATUS = txtLiteracyStatus.Text.ToString();
                    LitStatusBoobj.DESCRIPTION = txtDescription.Text.ToString();
                    LitStatusBoobj.CREATEDBY = Convert.ToInt32(Session["USER_ID"]);
                    LiteracyStatusBLLobj = new LiteracyStatusBLL();
                    message = LiteracyStatusBLLobj.Update(LitStatusBoobj, litStatusID);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                btnSave.Text = "Save";
                ClearDetails();
                SetUpdateMode(false);
            }
            catch (Exception ee)
            {
                throw ee;
            }

            finally
            {
                LiteracyStatusBLLobj = null;
            }

            BindGrid(true, false);

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string InsertLitStatusDetail()
        {
            string message = string.Empty;
            string uID = Session["USER_ID"].ToString();
            LitStatusBoobj = new LiteracyStatusBO();

            //Assignement of Data
            LitStatusBoobj.LTR_STATUS = txtLiteracyStatus.Text.Trim();
            LitStatusBoobj.DESCRIPTION = txtDescription.Text.Trim();
            LitStatusBoobj.ISDELETED = "False";

            LitStatusBoobj.CREATEDBY = Convert.ToInt32(uID);//By Default Admin
            LitStatusBoobj.CREATEDDATE = DateTime.Now;

            LiteracyStatusBLL LitStatusBLL = new LiteracyStatusBLL();

            try
            {
                message = LitStatusBLL.Insert(LitStatusBoobj);
            }
            catch (Exception ee)
            {
                throw ee;
            }

            finally
            {
                LitStatusBLL = null;
            }
            return message;
            //            BindGrid(true, true);
            //            ClearDetails();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('LiteracyStatus details added successfully');", true);
        }
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLitStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["LitStatusID"] = e.CommandArgument;
                int literacyStatusID = Convert.ToInt32(ViewState["LitStatusID"]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                if (row != null)
                {
                    txtLiteracyStatus.Text = row.Cells[1].Text.ToString();
                    if (row.Cells[2].Text.ToString().Trim() != "&nbsp;")
                        txtDescription.Text = row.Cells[2].Text.ToString();
                    else
                        txtDescription.Text = string.Empty;
                }
                SetUpdateMode(true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                int literacyStatusID = Convert.ToInt32(e.CommandArgument);
                LiteracyStatusBLL LiteracyStatusBLLobj = new LiteracyStatusBLL();
                message = LiteracyStatusBLLobj.Delete(literacyStatusID);

                ClearDetails();
                SetUpdateMode(false);
                BindGrid(false, true);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
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

                string LitStatusID = ((Literal)gr.FindControl("litLiteracyStatusID")).Text;
                LiteracyStatusBLL LiteracyStatusBLLobj = new LiteracyStatusBLL();
                message = LiteracyStatusBLLobj.ObsoleteLiteracyStatus(Convert.ToInt32(LitStatusID), Convert.ToString(chk.Checked));
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
       
        private void DeleteUser()
        {

        }
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        protected void grdLitStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataSet myDataSet = GetViewState();
            //DataTable myDataTable = myDataSet.Tables[0];
            //myGridView.DataSource = SortDataTable(myDataTable, true);

            //myGridView.PageIndex = e.NewPageIndex;
            //myGridView.DataBind();
            BindGrid(false, false);
            grdLitStatus.PageIndex = e.NewPageIndex;
            grdLitStatus.DataBind();
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
                ViewState["LitStatusID"] = "0";
            }
        }
    }
}