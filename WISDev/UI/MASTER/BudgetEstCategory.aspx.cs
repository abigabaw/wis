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
    public partial class Category : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new System.Data.DataTable();
            if (!IsPostBack)
            {
                Master.PageHeader = "Budget Estimation Category";
                ViewState["BGT_CATEGORYID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                CategoryTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_BUDGET_EST_CATEGORY) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCategory.Columns[grdCategory.Columns.Count - 1].Visible = false;
                    grdCategory.Columns[grdCategory.Columns.Count - 2].Visible = false;
                    grdCategory.Columns[grdCategory.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in grdCategory.Rows)
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
            CategoryBLL CategoryBLLobj = new CategoryBLL();
            grdCategory.DataSource = CategoryBLLobj.GetALLCategory();
            grdCategory.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["BGT_CATEGORYID"] = e.CommandArgument;
                GetCategoryDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                CategoryBLL CategoryBLLobj = new CategoryBLL();
                message = CategoryBLLobj.DeleteCategory(Convert.ToInt32(e.CommandArgument));
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
        /// used to fetch details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetCategoryDetails()
        {

            CategoryBLL CategoryBLLobj = new CategoryBLL();
            int CATEGORYID = 0;

            if (ViewState["BGT_CATEGORYID"] != null)
                CATEGORYID = Convert.ToInt32(ViewState["BGT_CATEGORYID"]);

            CategoryBO CategoryBOobj = new CategoryBO();
            CategoryBOobj = CategoryBLLobj.GetCategoryById(CATEGORYID);

            CategoryTextBox.Text = CategoryBOobj.BGT_CATEGORYNAME;
        }
        /// <summary>
        /// Used to save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ViewState["BGT_CATEGORYID"].ToString() == "0")
            {
                CategoryBLL CategoryBLLobj = new CategoryBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CategoryBO CategoryBOobj = new CategoryBO();
                    CategoryBOobj.BGT_CATEGORYNAME = CategoryTextBox.Text.ToString().Trim();
                    CategoryBOobj.CREATEDBY = Convert.ToInt32(uID);

                    CategoryBLL BLLobj = new CategoryBLL();
                    message = BLLobj.Insert(CategoryBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    CategoryBLLobj = null;
                }

            }
            else
            {
                CategoryBLL CategoryBLLobj = new CategoryBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CategoryBO CategoryBOobj = new CategoryBO();
                    CategoryBOobj.BGT_CATEGORYNAME = CategoryTextBox.Text.ToString().Trim();
                    CategoryBOobj.BGT_CATEGORYID = Convert.ToInt32(ViewState["BGT_CATEGORYID"]);
                    CategoryBOobj.CREATEDBY = Convert.ToInt32(uID);

                   CategoryBLL BLLobj = new CategoryBLL();
                    message = BLLobj.Edit(CategoryBOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CategoryBLLobj = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Used to change the text of buttton based on the condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                ViewState["BGT_CATEGORYID"] = "0";
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            CategoryTextBox.Text = string.Empty;
            ViewState["BGT_CATEGORYID"] = 0;
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
                string BGT_CATEGORYID = ((Literal)gr.FindControl("litCATEGORYID")).Text;

                CategoryBLL CategoryBLLobj = new CategoryBLL();
                message = CategoryBLLobj.ObsoleteCategory(Convert.ToInt32(BGT_CATEGORYID), Convert.ToString(chk.Checked));
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
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// used to change page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCategory.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        /// <summary>
        /// Set link attributes to  link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkCropRate = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkSubCategory");
                int BGT_CATEGORYID = (int)DataBinder.Eval(e.Row.DataItem, "BGT_CATEGORYID");

                lnkCropRate.Attributes.Add("onclick", "OpenSubCategory(" + BGT_CATEGORYID + ");");
            }
        }
    }
}