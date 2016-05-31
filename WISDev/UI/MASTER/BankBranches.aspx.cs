using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class BankBranches : System.Web.UI.Page
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
                ViewState["BANK_ID"] = Request.QueryString["id"];
                ViewState["BankBranchId"] = 0;
                getpageHeader();
                BindBranches();
                txtSwiftCode.Attributes.Add("onblur", "checkswiftcode(this);");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BANK) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdBankBranchs.Columns[grdBankBranchs.Columns.Count - 1].Visible = false;
                    grdBankBranchs.Columns[grdBankBranchs.Columns.Count - 2].Visible = false;
                    grdBankBranchs.Columns[grdBankBranchs.Columns.Count - 3].Visible = false;
                }
            }
        }
        /// <summary>
        /// 
        /// used to get page header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getpageHeader()
        {
            BankBO ObjBankBO = (new BankBLL()).GetBankByBankID(Convert.ToInt32(ViewState["BANK_ID"]));
            if (ObjBankBO != null)
            Master.PageHeader = "Branches for " + ObjBankBO.BankName;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindBranches()
        {
            BranchBLL objBranchBLL = new BranchBLL();
            int BankID = Convert.ToInt32(ViewState["BANK_ID"]);
            grdBankBranchs.DataSource = objBranchBLL.GetAllBranches(BankID);
            grdBankBranchs.DataBind();
        }
        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BranchBO objBranchBO = new BranchBO();
            objBranchBO.BankBranchId = Convert.ToInt32(ViewState["BankBranchId"]);
           objBranchBO.BankID= Convert.ToInt32(ViewState["BANK_ID"]);
           objBranchBO.BranchName = txtBranchName0.Text.Trim();
           objBranchBO.City = txtCity0.Text.Trim();

           objBranchBO.SwiftCode = txtSwiftCode.Text.Trim();
           objBranchBO.BANKCODE = txtBankCode.Text.Trim();

           BranchBLL objBranchBLL = new BranchBLL();
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (objBranchBO.BankBranchId == 0)
            {
                objBranchBO.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objBranchBLL.AddBranch(objBranchBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                }
            }
            else
            {
                objBranchBO.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objBranchBLL.UpdateBranch(objBranchBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            BindGrid();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtBranchName0.Text = "";
            txtCity0.Text = "";
            txtSwiftCode.Text = "";
            txtBankCode.Text = "";
            ViewState["BankBranchId"] = 0;
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }
        /// <summary>
        /// used to change the page
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdBankBranchs.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBankBranchs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["BankBranchId"] = e.CommandArgument;
                BranchBO objBranchBO = new BranchBO();
                BranchBLL objBranchBLL = new BranchBLL();
                objBranchBO = objBranchBLL.GetBranchById(Convert.ToInt32(ViewState["BankBranchId"]));
                if (objBranchBO != null)
                {
                    ViewState["BankBranchId"]= objBranchBO.BankBranchId;
                    txtBranchName0.Text = objBranchBO.BranchName;
                    txtCity0.Text = objBranchBO.City;
                    txtSwiftCode.Text = objBranchBO.SwiftCode;
                    txtBankCode.Text = objBranchBO.BANKCODE;
                }
                SetUpdateMode(true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                int Branch =  Convert.ToInt32(e.CommandArgument);
                BranchBLL objBranchBLL = new BranchBLL();
                message = objBranchBLL.DeleteBranch(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    ClearDetails();
                    message = "Data deleted successfully";
               

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGrid();
            }
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
            BranchBO objBranchBO = new BranchBO();
            BranchBLL objBranchBLL = new BranchBLL();
            int bankID = Convert.ToInt32(ViewState["BANK_ID"]);

            grdBankBranchs.DataSource = objBranchBLL.GetAllBranches(bankID);
            grdBankBranchs.DataBind();
          
        }
        /// <summary>
        /// used to change the text of the save button based on condition
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
                ViewState["BANK_ID"] = "0";
            }
        }
        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearDetails();

            BranchBLL objBranchBLL = new BranchBLL();
            grdBankBranchs.DataSource = objBranchBLL.GetAllBranches(Convert.ToInt32(ViewState["BANK_ID"]));
            grdBankBranchs.DataBind();
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
                ViewState["BankBranchId"] = ((Literal)gr.FindControl("litBankBranchId")).Text;
                BranchBLL objBranchBLL = new BranchBLL();
                message = objBranchBLL.ObsoleteBranch(Convert.ToInt32(ViewState["BankBranchId"]), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Updated successfully";
                BindGrid();
                ClearDetails();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        protected void grdBankBranchs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        }

    }