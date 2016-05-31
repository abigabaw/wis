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
    public partial class CDAPBudgetItem : System.Web.UI.Page
    {
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
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "CDAP Budget Item";
                ViewState["CDAPBUDGETID"] = 0;
                BindGrid();
                txtBudgetItem.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdBudgetItem.Columns[3].Visible = false;
                    grdBudgetItem.Columns[4].Visible = false;
                    grdBudgetItem.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdBudgetItem.Rows)
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBudgetItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["CDAPBUDGETID"] = e.CommandArgument;
                GetBudgetItem();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                ITEMBLL objItemBLL = new ITEMBLL();
                message = objItemBLL.DeleteCDAPBudgetMaster(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                BindGrid();
                ClearData();
            }

            else if (e.CommandName == "ViewROW")
            {
                ViewState["CDAPBUDGETID"] = e.CommandArgument;
                getClanDetails();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// used to fetch details
        /// </summary>
        private void getClanDetails()
        {
            int TribeID = 0;

            if (ViewState["CDAPBUDGETID"] != null)
            {
                TribeID = Convert.ToInt32(ViewState["CDAPBUDGETID"]);
                Response.Redirect("Clans.aspx?TribeID=" + TribeID);
            }
        }
        /// <summary>
        /// used to change page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdBudgetItem.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
            try
            {
                ITEMBLL objItemBLL = new ITEMBLL();
                grdBudgetItem.DataSource = objItemBLL.GetAllCDAPBudgetItems();
                grdBudgetItem.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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

                string CDAPBUDGETID = ((Literal)gr.FindControl("litCategoryID")).Text;
                ITEMBLL objItemBLL = new ITEMBLL();
                message = objItemBLL.ObsoleteCDAPBudgetMaster(Convert.ToInt32(CDAPBUDGETID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// used to save data into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBudgetItem()
        {
            ITEMBLL objItemBLL = new ITEMBLL();
            CDAPBudgetMasterBO objBudget = new CDAPBudgetMasterBO();

            string message = "";

            string Tribe = string.Empty;
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            objBudget.CategoryName = txtBudgetItem.Text.Trim();
            objBudget.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = objItemBLL.AddCDAPBudgetMaster(objBudget);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                BindGrid();
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                objItemBLL = null;
            }
        }
        /// <summary>
        /// used to update data into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBudgetItem()
        {
            ITEMBLL objItemBLL = new ITEMBLL();
            CDAPBudgetMasterBO objBudget = new CDAPBudgetMasterBO();
            string message = "";

            try
            {
                if (ViewState["CDAPBUDGETID"] != null)
                    objBudget.CategoryID = Convert.ToInt32(ViewState["CDAPBUDGETID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                objBudget.CategoryName = txtBudgetItem.Text.Trim();
                objBudget.UpdatedBy = Convert.ToInt32(uID);

                message = objItemBLL.UpdateCDAPBudgetMaster(objBudget);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                ClearData();
                SetUpdateMode(false);
                BindGrid();

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objItemBLL = null;
            }
        }
        /// <summary>
        /// used to fetch details from  database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetBudgetItem()
        {
            ITEMBLL objItemBLL = new ITEMBLL();

            CDAPBudgetMasterBO objBudget = objItemBLL.GetCDAPBudgetItemByID(Convert.ToInt32(ViewState["CDAPBUDGETID"]));

            if (objBudget != null)
                txtBudgetItem.Text = objBudget.CategoryName;

            objBudget = null;
            objItemBLL = null;
        }
        /// <summary>
        /// used to save or update data into database based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveBudgetItem();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateBudgetItem();
                }
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
            try
            {
                ClearData();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtBudgetItem.Text = string.Empty;
            SetUpdateMode(false);
        }
        /// <summary>
        /// Used to change the text of buttton based on the condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ViewState["CDAPBUDGETID"] = "0";
            }
        }
        /// <summary>
        /// Set link attributes to  link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBudgetItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkSubCategory = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkSubCategory");
                int categoryID = (int)DataBinder.Eval(e.Row.DataItem, "CategoryID");

                lnkSubCategory.Attributes.Add("onclick", "OpenSubCategories(" + categoryID + ");");
            }
        }
    }
}