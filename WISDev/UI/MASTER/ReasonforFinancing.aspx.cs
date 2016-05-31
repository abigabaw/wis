using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class ReasonforFinancing : System.Web.UI.Page
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
                Master.PageHeader = "Reason for Financing";
                ViewState["FINANCEREASONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                ReasonTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FINANCE) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdReason.Columns[2].Visible = false;
                    grdReason.Columns[3].Visible = false;
                    grdReason.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdReason.Rows)
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
            ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
            grdReason.DataSource = BLLobj.GetAllReasonForFinance("");
            grdReason.DataBind();
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

            if (ReasonIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
                    BOobj.FINANCEREASON = ReasonTextBox.Text;

                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    ReasonforFinancingBLL ReasonforFinancingBLLobj = new ReasonforFinancingBLL();
                    message = ReasonforFinancingBLLobj.Insert(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                    //BindGrid(true, true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Vaccination details added successfully');", true);
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
            else if (ReasonIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
                    BOobj.FINANCEREASON = ReasonTextBox.Text;
                    BOobj.FINANCEREASONID = Convert.ToInt32(ReasonIDTextBox.Text);
                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    ReasonforFinancingBLL ReasonforFinancingBLLobj = new ReasonforFinancingBLL();
                    message = ReasonforFinancingBLLobj.Update(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                    //BindGrid(true, true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Vaccination details added successfully');", true);
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
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);


        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            ReasonIDTextBox.Text = string.Empty;
            ReasonTextBox.Text = string.Empty;
            ViewState["FINANCEREASONID"] = 0;
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
                ViewState["FINANCEREASONID"] = "0";
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "EditRow")
            {
                ViewState["FINANCEREASONID"] = e.CommandArgument;
                GetReasonFoprFinanceDetail();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
                message = BLLobj.DeleteReasonForFinance(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                ClearAll();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To fetch details from database and assign to textbox
        /// </summary>
        private void GetReasonFoprFinanceDetail()
        {
            ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
            int ReasonForFinanceID = 0;

            if (ViewState["FINANCEREASONID"] != null)
                ReasonForFinanceID = Convert.ToInt32(ViewState["FINANCEREASONID"]);

            ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
            BOobj = BLLobj.GetReasonForFinanceID(ReasonForFinanceID);

            ReasonTextBox.Text = BOobj.FINANCEREASON;
            ReasonIDTextBox.Text = BOobj.FINANCEREASONID.ToString();
        }

        //protected void ChangePage(object sender, GridViewPageEventArgs e)
        //{
        //    

        //}
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
                string financeReasonID = ((Literal)gr.FindControl("litReasonID")).Text;
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                message = BLLobj.ObsoleteReasonForFin(Convert.ToInt32(financeReasonID), Convert.ToString(chk.Checked));
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
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdReason_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReason.PageIndex = e.NewPageIndex;
            BindGrid(true, true);
        }

       
    }
}