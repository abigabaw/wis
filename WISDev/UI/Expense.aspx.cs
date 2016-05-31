using System;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Web.UI;
using System.Data;
using WIS_BusinessObjects;



namespace WIS
{
    public partial class Expense : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            calDateOfBirth.Format = UtilBO.DateFormat;
            if(!IsPostBack)
            {
                if (Session["USER_ID"] != null)
                {
                    string uID = Session["USER_ID"].ToString();
                }
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Consolidated Detail Expense";
                    
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                ViewState["PROJECTEXPENSEID"] = 0;

                if (Session["PROJECT_ID"] != null)
                {
                    int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                   
                }
                //btnSave.Visible = false;
                //btnCancel.Visible = false;
                grdExpense.AllowPaging = true;
                BindGrid();
                dpDateofexpense.Attributes.Add("readonly", "readonly");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CONS_PROJECT_EXPENSES) == false)
                {
                    Btn_ImportFromExcel.Visible = false;
                    btnSaveExpence.Visible = false;
                    btnClearExpence.Visible = false;
                    grdExpense.Columns[grdExpense.Columns.Count - 1].Visible = false;
                    grdExpense.Columns[grdExpense.Columns.Count - 2].Visible = false;
                }
            }
        }
        /// <summary>
        /// Bind data to Grid
        /// </summary>
        private void BindGrid()
        {
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            ExpenseBLL ExpenseBLLobj = new ExpenseBLL();
            grdExpense.DataSource = ExpenseBLLobj.GetAllExpenseData(ProjectID);
            grdExpense.DataBind();
            //btnSave.Visible = false;
            //btnCancel.Visible= false;
         
        }

        /// <summary>
        /// To edit and delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ViewState["PROJECTEXPENSEID"] = e.CommandArgument;
                    ExpenseBLL objExpenseBLL = new ExpenseBLL();
                    ExpenseBO objExpense = objExpenseBLL.GetExpenseByID(Convert.ToInt32(ViewState["PROJECTEXPENSEID"]));

                    txtExpenseAmt.Text = UtilBO.CurrencyFormat(objExpense.EXPENSEAMOUNT);//objExpense.EXPENSEAMOUNT.ToString();
                    txtExpensetype.Text = objExpense.EXPENSETYPE.ToString();
                    txtAccountcode.Text = objExpense.ACCOUNTCODE;
                    if (objExpense.DATEOFEXPENSE != null)
                        dpDateofexpense.Text = objExpense.DATEOFEXPENSE.ToString(UtilBO.DateFormat);
                    btnSaveExpence.Text = "Update";
                    btnClearExpence.Text = "Cancel";
                }
                else if (e.CommandName == "DeleteRow")
                {
                    ExpenseBLL objExpenseBLL = new ExpenseBLL();

                    message = objExpenseBLL.DeleteExpense(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";
                    Clearfields();
                    grdExpense.AllowPaging = true;
                    BindGrid();
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
        /// To change Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdExpense.PageIndex = e.NewPageIndex;
            BindGrid();
        }       

        //private void Import_To_Grid(string FilePath, string Extension)
        //{
        //    ExpenseBLL objExpensePopup = new ExpenseBLL();
        //    DataTable dtExpenses = objExpensePopup.ExcelDataImportintoGrid(FilePath, Extension, Convert.ToInt32(Session["PROJECT_ID"]), Convert.ToInt32(Session["USER_ID"]));

        //    grdExpense.DataSource = dtExpenses;
        //    grdExpense.DataBind();
        //    

        //    //DataTable dtExpen = (DataTable)ViewState["EXCEL_DATA"];
        //}

        /// <summary>
        /// To Load expence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoadExpense_Click(object sender, EventArgs e)
        {
            ExpenseBLL objExpensePopup = new ExpenseBLL();
            string filePath = hdnFilePath.Value;
            string fileExtension = "xlsx";

            DataTable dtExpenses = objExpensePopup.ExcelDataImportintoGrid(filePath, fileExtension, Convert.ToInt32(Session["PROJECT_ID"]), Convert.ToInt32(Session["USER_ID"]));

            int validcount = 0;
            string[] cols = new string[4] { "ExpenseType", "AccountCode", "ExpenseAmount", "DateOfExpense" };
            for (int i = 0; i < dtExpenses.Columns.Count; i++)
            {
                foreach (string col in cols)
                {
                    if (dtExpenses.Columns[i].ToString().ToUpper() == col.ToUpper())
                    {
                        validcount++;
                    }
                }
            }
            if (validcount == 4)
            {
                if (!dtExpenses.Columns.Contains("PROJECTEXPENSEID"))
                    dtExpenses.Columns.Add("PROJECTEXPENSEID", typeof(int));
                grdExpense.Columns[grdExpense.Columns.Count - 1].Visible = false;
                grdExpense.Columns[grdExpense.Columns.Count - 2].Visible = false;
                grdExpense.DataSource = dtExpenses;
                grdExpense.DataBind();
                if (grdExpense.Rows.Count > 0)
                {
                    pnlSave.Visible = true;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;
                }
                grdExpense.AllowPaging = false;
                //updExpense.Update();

                ViewState["EXCEL_DATA"] = dtExpenses;
            }
            else
            {
                pnlSave.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid File.Please Upload a valid Excel file');", true);
            }
        }

        /// <summary>
        /// Format fields data in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime expenseDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DATEOFEXPENSE"));
                if (expenseDate != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litExpenseDate")).Text = expenseDate.ToString(UtilBO.DateFormat);
            }         
        }

        /// <summary>
        /// Save and Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            string uID = Session["USER_ID"].ToString();
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);

            DataTable dtExpen = (DataTable)ViewState["EXCEL_DATA"];
            DataTable Dt = null;
            ExpenseBLL ExpenseBLLobj = new ExpenseBLL();
            Dt = ExpenseBLLobj.savedata(dtExpen, ProjectID, uID);
            //grdExpense.AllowPaging = true;
            message = "Expense Data added successfully";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            pnlSave.Visible = false;
            Clearfields();
            grdExpense.AllowPaging = true;
            BindGrid();
        }

        /// <summary>
        /// Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ////btnSave.Visible = false;
            ////btnCancel.Visible = false;
            //grdExpense.DataSource = null;
            //grdExpense.DataBind();
            ////updExpense.Update();
            pnlSave.Visible = false;
            Clearfields();
            grdExpense.AllowPaging = true;
            BindGrid();
        }

        /// <summary>
        /// Save and Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveExpence_Click(object sender, EventArgs e)
        {
            ExpenseBO objExpense = new ExpenseBO();


            objExpense.PROJECTEXPENSEID = Convert.ToInt32(ViewState["PROJECTEXPENSEID"]);

            ExpenseBLL objExpenseBLL = new ExpenseBLL();
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (objExpense.PROJECTEXPENSEID == 0)
            {
                objExpense.PROJECTID = Convert.ToInt32(Session["PROJECT_ID"]);
                objExpense.EXPENSETYPE = txtExpensetype.Text.Trim();
                objExpense.ACCOUNTCODE = txtAccountcode.Text.Trim();
                objExpense.EXPENSEAMOUNT = Convert.ToDecimal(txtExpenseAmt.Text.Trim());
                objExpense.DATEOFEXPENSE = Convert.ToDateTime(dpDateofexpense.Text.ToString());
                objExpense.UPDATEDBY = Convert.ToInt32(Session["USER_ID"]);
                message = objExpenseBLL.AddExpense(objExpense);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully.";
                    Clearfields();
                }
            }
            else
            {
                objExpense.PROJECTID = Convert.ToInt32(Session["PROJECT_ID"]);
                objExpense.EXPENSETYPE = txtExpensetype.Text.Trim();
                objExpense.ACCOUNTCODE = txtAccountcode.Text.Trim();
                objExpense.EXPENSEAMOUNT = Convert.ToDecimal(txtExpenseAmt.Text.Trim());
                objExpense.DATEOFEXPENSE = Convert.ToDateTime(dpDateofexpense.Text.ToString());
                objExpense.UPDATEDBY = Convert.ToInt32(Session["USER_ID"]);
                message = objExpenseBLL.UpdateExpense(objExpense);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data Updated successfully.";
                    Clearfields();
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            grdExpense.AllowPaging = true;
            BindGrid();

        }
        /// <summary>
        /// To clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearExpence_Click(object sender, EventArgs e)
        {
            Clearfields();
        }

        /// <summary>
        /// to Clear fields
        /// </summary>
        private void Clearfields()
        {
            ViewState["PROJECTEXPENSEID"] = 0;
            txtAccountcode.Text = string.Empty;
            txtExpenseAmt.Text = string.Empty;
            txtExpensetype.Text = string.Empty;
            dpDateofexpense.Text = "";
            btnSaveExpence.Text = "Save";
            btnClearExpence.Text = "Clear";
        }    
      
       
    }
}