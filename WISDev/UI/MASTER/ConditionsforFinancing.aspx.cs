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
    public partial class ConditionsforFinancing : System.Web.UI.Page
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
                Master.PageHeader = "Conditions for Financing";
                ViewState["FINANCECONDITIONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                ConditionsTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FINANCE) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdConditions.Columns[2].Visible = false;
                    grdConditions.Columns[3].Visible = false;
                    grdConditions.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdConditions.Rows)
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
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            FinanceConditionBLL BLLobj = new FinanceConditionBLL();
            grdConditions.DataSource = BLLobj.GetAllFinanceConditions();
            grdConditions.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdConditions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "EditRow")
            {
                ViewState["FINANCECONDITIONID"] = e.CommandArgument;
                GetFinanceCondDetail();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                FinanceConditionBLL BLLobj = new FinanceConditionBLL();
                message = BLLobj.DeleteFinanceCondition(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                ClearAll();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
    
        /// to fetch details from database
        /// </summary>
    
        private void GetFinanceCondDetail()
        {
            FinanceConditionBLL BLLobj = new FinanceConditionBLL();
            int financeConditionID = 0;

            if (ViewState["FINANCECONDITIONID"] != null)
                financeConditionID = Convert.ToInt32(ViewState["FINANCECONDITIONID"]);

            FinanceConditionBO BOobj = new FinanceConditionBO();
            BOobj = BLLobj.GetfinanceConditionID(financeConditionID);

            ConditionsTextBox.Text = BOobj.FINANCECONDITION;
            ConditionsIDTextBox.Text = BOobj.FINANCECONDITIONID.ToString();
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
                ViewState["FINANCECONDITIONID"] = "0";
            }
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdConditions.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(false,false);
        }
        /// <summary>
        /// to insert data to database
        /// </summary>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

             if (ConditionsIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                FinanceConditionBLL BLLobj = new FinanceConditionBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    FinanceConditionBO FinanceConditionBOobj = new FinanceConditionBO();
                    FinanceConditionBOobj.FINANCECONDITION = ConditionsTextBox.Text;

                    FinanceConditionBOobj.CREATEDBY = Convert.ToInt32(uID);

                    FinanceConditionBLL FBLLobj = new FinanceConditionBLL();
                    message = FBLLobj.Insert(FinanceConditionBOobj);

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
             else if (ConditionsIDTextBox.Text.ToString().Trim() != string.Empty)
             {
                 FinanceConditionBLL BLLobj = new FinanceConditionBLL();

                 try
                 {
                     string uID = Session["USER_ID"].ToString();
                     FinanceConditionBO FinanceConditionBOobj = new FinanceConditionBO();
                     FinanceConditionBOobj.FINANCECONDITION = ConditionsTextBox.Text;
                     FinanceConditionBOobj.FINANCECONDITIONID = Convert.ToInt32(ConditionsIDTextBox.Text);
                     FinanceConditionBOobj.CREATEDBY = Convert.ToInt32(uID);

                     FinanceConditionBLL FBLLobj = new FinanceConditionBLL();
                     message = FBLLobj.Update(FinanceConditionBOobj);

                     if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                     {
                         message = "Data updated successfully";
                         ClearAll();
                         BindGrid(true, true);
                         SetUpdateMode(false);
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
                string financeConditionID = ((Literal)gr.FindControl("litFcondID")).Text;
                FinanceConditionBLL objroleBLL = new FinanceConditionBLL();

                message = objroleBLL.ObsoleteFcond(Convert.ToInt32(financeConditionID), Convert.ToString(chk.Checked));
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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            ConditionsTextBox.Text = string.Empty;
            ConditionsIDTextBox.Text = string.Empty;
            ViewState["FINANCECONDITIONID"] = 0;
        }
        /// <summary>
        ///calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
    }
}