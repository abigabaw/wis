using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class Bank : System.Web.UI.Page
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
                Master.PageHeader = "Bank";
                ViewState["BANK_ID"] = 0;
                BindBanks(false, false);
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //txtBankName.Attributes.Add("onchange", "isDirty = 1;");
                txtBankName.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BANK) == false)
                {
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSearch.Visible = false;
                    btnClearSearch.Visible = false;
                    grdBanks.Columns[grdBanks.Columns.Count - 1].Visible = false;
                    grdBanks.Columns[grdBanks.Columns.Count - 2].Visible = false;
                    grdBanks.Columns[grdBanks.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in grdBanks.Rows)
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
                stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindBanks(bool addRow, bool deleteRow)
        {
            BankBLL objBankBLL = new BankBLL();
            grdBanks.DataSource = objBankBLL.GetAllBanks("");
            grdBanks.DataBind();
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BankBO objBank = new BankBO();

            objBank.BankID = Convert.ToInt32(ViewState["BANK_ID"]);
            objBank.BankName = txtBankName.Text.Trim();
           
            BankBLL objBankBLL = new BankBLL();
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (objBank.BankID == 0)
            {
                objBank.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objBankBLL.AddBank(objBank);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                }
            }
            else
            {
                objBank.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objBankBLL.UpdateBank(objBank);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            BindBanks(true, false);
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
                string BANK_ID = ((Literal)gr.FindControl("litBankID")).Text;
                BankBLL objBankBLL = new BankBLL();
                message = objBankBLL.ObsoleteBank(Convert.ToInt32(BANK_ID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                ClearDetails();
                BindBanks(false, true);
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
        protected void ClearDetails()
        {
            txtBankName.Text = "";
            ViewState["BANK_ID"] = 0;
            txtSearchBankName.Text = "";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string bankName = txtSearchBankName.Text.Trim();
            BankBLL objBankBLL = new BankBLL();
            grdBanks.DataSource = objBankBLL.GetAllBanks(bankName);
            grdBanks.DataBind();
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchGrid()
        {
            string bankName = txtSearchBankName.Text.Trim();
            BankBLL objBankBLL = new BankBLL();
            grdBanks.DataSource = objBankBLL.GetAllBanks(bankName);
            grdBanks.DataBind();
        }

        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearDetails(); 
            string bankName = txtSearchBankName.Text.Trim();
            BankBLL objBankBLL = new BankBLL();
            grdBanks.DataSource = objBankBLL.GetAllBanks(bankName);
            grdBanks.DataBind();
        }

        /// <summary>
        /// Show and Hide the Search or Add banks panels baced on Selection
        /// </summary>
        /// <param name="showAdd"></param>
        /// <param name="showSearch"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlBankDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlBankDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBanks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ShowHideSections(true, false);
                    ViewState["BANK_ID"] = e.CommandArgument;
                    BankBLL objBankBLL = new BankBLL();
                    BankBO objBank = objBankBLL.GetBankByBankID(Convert.ToInt32(ViewState["BANK_ID"]));

                    txtBankName.Text = objBank.BankName;
                    btnSave.Text = "Update";
                    btnClear.Text = "Cancel";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
                }

                else if (e.CommandName == "DeleteRow")
                {
                    BankBLL objBankBLL = new BankBLL();
                 
                  message= objBankBLL.DeleteBank(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";
                    ClearDetails();
                    BindBanks(false, true);
                }
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Change index of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBanks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBanks.PageIndex = e.NewPageIndex;
            SearchGrid();
        }

        /// <summary>
        /// Show Add Banks Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindBanks(false, false);
            btnSave.Text = "Save";
            ClearDetails();
        }

        /// <summary>
        /// Show search Banks Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }

        /// <summary>
        /// Set link attributes to Branch link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBanks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkBank = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkBank");
                int BankID = (int)DataBinder.Eval(e.Row.DataItem, "bankID");

                lnkBank.Attributes.Add("onclick", "OpenBranchBank(" + BankID + ");");
            }
        }
    }
}