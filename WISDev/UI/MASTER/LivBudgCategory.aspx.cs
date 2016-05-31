using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.MASTER
{
    public partial class LivBudgCategory : System.Web.UI.Page
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
                Master.PageHeader = "Liv Budget Category";
                ViewState["LIV_BUD_CATEGID"] = 0;
                BindGrid();
                txtBudgetCategory.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdBudgetCategory.Columns[3].Visible = false;
                    grdBudgetCategory.Columns[4].Visible = false;
                    grdBudgetCategory.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdBudgetCategory.Rows)
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

        //protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
        //        imgEdit.Attributes.Add("onclick", "isDirty = 0;");
        //        CheckBox IsObsolete = (CheckBox)e.Row.FindControl("IsObsolete");
        //        IsObsolete.Attributes.Add("onclick", "isDirty = 0;");
        //        ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
        //        imgDelete.Attributes.Add("onclick", "isDirty = 0;");
        //    }
        //}
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            try
            {
                LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
                grdBudgetCategory.DataSource = LivBudgCategoryBLLobj.GetAllLivBudCategory();
                grdBudgetCategory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdBudgetCategory.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdBudgetCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["LIV_BUD_CATEGID"] = e.CommandArgument;
                GetBudgetItem();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
                message = LivBudgCategoryBLLobj.DeleteLivBudCategory(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                BindGrid();
                ClearData();
            }

            else if (e.CommandName == "ViewROW")
            {
                ViewState["LIV_BUD_CATEGID"] = e.CommandArgument;
                //getClanDetails();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// to link to other page on click of link  in grid
        /// </summary>
        /// <returns></returns>

        protected void grdBudgetCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkSubCategory = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkSubCategory");
                int lIV_BUD_CATEGID = (int)DataBinder.Eval(e.Row.DataItem, "lIV_BUD_CATEGID");

                lnkSubCategory.Attributes.Add("onclick", "OpenSubCategories(" + lIV_BUD_CATEGID + ");");
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

                string LIV_BUD_CATEGID = ((Literal)gr.FindControl("litCategoryID")).Text;
                LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
                message = LivBudgCategoryBLLobj.ObsoleteLivBudCategory(Convert.ToInt32(LIV_BUD_CATEGID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBudgetItem()
        {
            LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
            LivBudgCategoryBO LivBudgCategoryBOobj = new LivBudgCategoryBO();

            string message = "";

            string Tribe = string.Empty;
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME = txtBudgetCategory.Text.Trim();
            LivBudgCategoryBOobj.CREATEDBY = Convert.ToInt32(uID);

            try
            {
                message = LivBudgCategoryBLLobj.InsertBudCategory(LivBudgCategoryBOobj);

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
                LivBudgCategoryBLLobj = null;
            }
        }
        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBudgetItem()
        {
            LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
            LivBudgCategoryBO LivBudgCategoryBOobj = new LivBudgCategoryBO();
            string message = "";

            try
            {
                if (ViewState["LIV_BUD_CATEGID"] != null)
                    LivBudgCategoryBOobj.lIV_BUD_CATEGID = Convert.ToInt32(ViewState["LIV_BUD_CATEGID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME = txtBudgetCategory.Text.Trim();
                LivBudgCategoryBOobj.UPDATEDBY = Convert.ToInt32(uID);

                message = LivBudgCategoryBLLobj.UpdateLivBudgCategory(LivBudgCategoryBOobj);

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
                LivBudgCategoryBLLobj = null;
            }
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetBudgetItem()
        {
            LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();

            LivBudgCategoryBO LivBudgCategoryBOobj = LivBudgCategoryBLLobj.GetLivBudCategoryByID(Convert.ToInt32(ViewState["LIV_BUD_CATEGID"]));

            if (LivBudgCategoryBOobj != null)
                txtBudgetCategory.Text = LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME;

            LivBudgCategoryBOobj = null;
            LivBudgCategoryBLLobj = null;
        }
        /// <summary>
        /// To call save method
        /// </summary>
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
        /// Calls clear method
        /// </summary>
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
            txtBudgetCategory.Text = string.Empty;
            SetUpdateMode(false);
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
                ViewState["LIV_BUD_CATEGID"] = "0";
            }
        }
    }
}