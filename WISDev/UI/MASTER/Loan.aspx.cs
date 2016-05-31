using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class Loan : System.Web.UI.Page
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
                ViewState["ENCUMBRANCEID"] = 0;
                Master.PageHeader = "Purpose of Encumbrance";
                BindGrid();
                SetGridSource(true);
                GrdPurposeofencumbrance.DataBind();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //txtPurposeofencumbrance.Attributes.Add("onchange","isDirty = 1;");
                txtPurposeofencumbrance.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_TENURE) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnSearch.Visible = false;
                    btnClear.Visible = false;
                    btnShowSearch.Visible = false;
                    GrdPurposeofencumbrance.Columns[2].Visible = false;
                    GrdPurposeofencumbrance.Columns[3].Visible = false;
                    GrdPurposeofencumbrance.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in GrdPurposeofencumbrance.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)grRow.FindControl("IsObsolete");
                            chk.Enabled = false;
                        }
                    }
                }
            }
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnSearch.ClientID);
            else
                stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e

        private void BindGrid()
        {
            LoanBLL objLoanBLL = new LoanBLL();
            GrdPurposeofencumbrance.DataSource = objLoanBLL.GetLoan("");
            GrdPurposeofencumbrance.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";
            LoanBO objLoan = new LoanBO();
            objLoan.EncumbranceId = Convert.ToInt32(ViewState["ENCUMBRANCEID"]);
            objLoan.Encumbrancepurpose = txtPurposeofencumbrance.Text.Trim();
            LoanBLL objLoanBLL = new LoanBLL();

            if (objLoan.EncumbranceId == 0)
            {
                objLoan.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objLoanBLL.AddLoan(objLoan);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";
            }
            else
            {
                objLoan.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objLoanBLL.UpdateLoan(objLoan);
               // SetUpdateMode(false);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                SetUpdateMode(false);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            ClearDetails();
            BindGrid();
        }
        /// <summary>
        /// To search details and display in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SetGridSource(false);
            GrdPurposeofencumbrance.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void SetGridSource(bool showRecentRecords)
        {
            string EncumbrancepurposeName;
            EncumbrancepurposeName = txtSearch.Text.Trim();
            LoanBLL objLoanBLL = new LoanBLL();
            LoanList objLoanList = new LoanList();
            objLoanList = objLoanBLL.GetLoan(EncumbrancepurposeName);
            if (objLoanList.Count > 0)
            {
                GrdPurposeofencumbrance.DataSource = objLoanList;
                GrdPurposeofencumbrance.DataBind();
            }
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
            BindGrid();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtPurposeofencumbrance.Text = "";
            txtSearch.Text = "";
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        /// <summary>
        /// Show search county Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlPurposeofencumbranceDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlPurposeofencumbranceDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPurposeofencumbrance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["ENCUMBRANCEID"] = e.CommandArgument;
                LoanBLL objLoanBLL = new LoanBLL();
                LoanBO objLoan = objLoanBLL.GetLoanByLoanID(Convert.ToInt32(ViewState["ENCUMBRANCEID"]));
                txtPurposeofencumbrance.Text = objLoan.Encumbrancepurpose;
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                LoanBLL objLoanBLL = new LoanBLL();
                objLoanBLL.DeleteLoan(Convert.ToInt32(e.CommandArgument));
                SetUpdateMode(false);
                BindGrid();
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
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

                string ENCUMBRANCEID = ((Literal)gr.FindControl("litENCUMBRANCEID")).Text;
                LoanBLL objLoanBLL = new LoanBLL();
                message = objLoanBLL.ObsoleteLoan(Convert.ToInt32(ENCUMBRANCEID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid();

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
        protected void GrdPurposeofencumbrance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdPurposeofencumbrance.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Show Add county Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindGrid();
            btn_Save.Text = "Save";
            ClearDetails();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["ENCUMBRANCEID"] = "0";
            }
        }
    }
}

/**
 * 
 * @version          :Tenure Master
 * @package          :Loan
 * @copyright        :Copyright © 2013 - All rights reserved.
 * @author           :Hanamant Singannavar
 * @Created Date     :18-Apr-2013 
 * @Updated By
 * @Updated Date
 * 
 */