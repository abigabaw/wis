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
    public partial class LivBudgItem : System.Web.UI.Page
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
                ViewState["LIV_BUD_CATEGID"] = Request.QueryString["id"];
                ViewState["LIV_BUD_ITEMID"] = 0;
                GetBudgetCategory();
                BindGrid();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdBudgetItem.Columns[3].Visible = false;
                    grdBudgetItem.Columns[4].Visible = false;
                    grdBudgetItem.Columns[5].Visible = false;
                }
            }
        }
        /// <summary>
        /// To fetch details from database
        /// </summary>
        private void GetBudgetCategory()
        {
            LivBudgCategoryBO LivBudgCategoryBOobj = (new LivBudgCategoryBLL()).GetLivBudCategoryByID(Convert.ToInt32(ViewState["LIV_BUD_CATEGID"]));
            if (LivBudgCategoryBOobj != null)
                Master.PageHeader = "Budget Item For " + LivBudgCategoryBOobj.LIV_BUD_CATEGORYNAME;
            else
                Master.PageHeader = "Budget Item ";
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
                ViewState["LIV_BUD_ITEMID"] = e.CommandArgument;
                GetBudgetItem();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
                message = LivBudgCategoryBLLobj.DeleteBudgetSubItem(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                BindGrid();
                ClearData();
            }

            else if (e.CommandName == "ViewROW")
            {
                ViewState["LIV_BUD_ITEMID"] = e.CommandArgument;
               // getClanDetails();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

       //private void getClanDetails()
       // {
       //     int TribeID = 0;

       //     if (ViewState["LIV_BUD_ITEMID"] != null)
       //     {
       //         TribeID = Convert.ToInt32(ViewState["LIV_BUD_ITEMID"]);
       //         Response.Redirect("Clans.aspx?TribeID=" + TribeID);
       //     }
       // }
        /// <summary>
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdBudgetItem.PageIndex = e.NewPageIndex;
            BindGrid();
        }
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
                grdBudgetItem.DataSource = LivBudgCategoryBLLobj.GetAllBudgetSubItems(Convert.ToInt32(ViewState["LIV_BUD_CATEGID"]));
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

                string LIV_BUD_CATEGID = ((Literal)gr.FindControl("litCategoryID")).Text;
                LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();
                message = LivBudgCategoryBLLobj.ObsoleteBudgetSubItem(Convert.ToInt32(LIV_BUD_CATEGID), Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
            LivBudgItemBO LivBudgItemBOobj = new LivBudgItemBO();
           

            string message = "";

            string Tribe = string.Empty;
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            LivBudgItemBOobj.LIV_BUD_CATEGID = Convert.ToInt32(ViewState["LIV_BUD_CATEGID"]);
            LivBudgItemBOobj.LIV_BUD_ITEMNAME = txtBudCategory.Text.Trim();
            LivBudgItemBOobj.LIV_BUD_ITEMDESC = txtBudgetItem.Text.Trim();
            LivBudgItemBOobj. CREATEDBY = Convert.ToInt32(uID);

            try
            {
                message = LivBudgCategoryBLLobj.InsertBudgetSubItem(LivBudgItemBOobj);

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
            LivBudgItemBO LivBudgItemBOobj = new LivBudgItemBO();
            string message = "";

            try
            {
                if (ViewState["LIV_BUD_ITEMID"] != null)
                    LivBudgItemBOobj.LIV_BUD_ITEMID = Convert.ToInt32(ViewState["LIV_BUD_ITEMID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                LivBudgItemBOobj.LIV_BUD_CATEGID = Convert.ToInt32(ViewState["CATEGORY_ID"]);
                LivBudgItemBOobj.LIV_BUD_ITEMNAME = txtBudCategory.Text.Trim();
                LivBudgItemBOobj.LIV_BUD_ITEMDESC = txtBudgetItem.Text.Trim();
                LivBudgItemBOobj.UPDATEDBY = Convert.ToInt32(uID);

                message = LivBudgCategoryBLLobj.UpdateBudgetSubItem(LivBudgItemBOobj);

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
        /// To fetch details from  database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetBudgetItem()
        {
            LivBudgCategoryBLL LivBudgCategoryBLLobj = new LivBudgCategoryBLL();

            LivBudgItemBO LivBudgItemBOobj = LivBudgCategoryBLLobj.GetBudgetSubItemByID(Convert.ToInt32(ViewState["LIV_BUD_ITEMID"]));

            if (LivBudgItemBOobj != null)
               
                txtBudCategory.Text = LivBudgItemBOobj.LIV_BUD_ITEMNAME;
                txtBudgetItem.Text = LivBudgItemBOobj.LIV_BUD_ITEMDESC;

            LivBudgItemBOobj = null;
            LivBudgCategoryBLLobj = null;
        }
        /// <summary>
        /// calls method save details to the database
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
            txtBudCategory.Text = string.Empty;
            txtBudgetItem.Text = string.Empty;
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
                ViewState["LIV_BUD_ITEMID"] = "0";
            }
        }
        /// <summary>
        /// to link to other page on click of link  in grid
        /// </summary>
        /// <returns></returns>
        protected void grdBudgetItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}