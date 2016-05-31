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
    public partial class ExpenseAccount : System.Web.UI.Page
    {
        /// <summary>
        /// Check User permitions
        /// Set Page Header,set attributes to link buttons. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_ID"] != null)
                {
                    string uID = Session["USER_ID"].ToString();
                }
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Consolidated Expense";

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
                BindGrid();
            }
        }

        /// <summary>
        /// To load Grid
        /// </summary>
        private void BindGrid()
        {
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            ExpenseBLL ExpenseBLLobj = new ExpenseBLL();
            grdExpense.DataSource = ExpenseBLLobj.GetExpenseDataForACC(ProjectID);
            grdExpense.DataBind();
            //btnSave.Visible = false;
            //btnCancel.Visible= false;

        }

        protected void grdExpense_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// To change PAge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdExpense.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Format data
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
    }
}