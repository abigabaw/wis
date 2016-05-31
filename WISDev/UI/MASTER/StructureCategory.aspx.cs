using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class StructureCategory : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        StructureCategoryBO objStructureCategory;
        StructureCategoryBLL objStructureCategoryBLL;
        #endregion

        #region Page Load
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
                Master.PageHeader = "Structure Category";
                ViewState["StructureCategoryID"] = 0;
                BindGrid(false, false);
                //txtStructureCategory.Attributes.Add("onchange", "isDirty = 1;");
                txtStructureCategory.Attributes.Add("onchange", "setDirtyText();");

               // ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdStructureCategory.Columns[3].Visible = false;
                    grdStructureCategory.Columns[4].Visible = false;
                    grdStructureCategory.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdStructureCategory.Rows)
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
        #endregion

        #region Load Default Values
        //Required when Data Loaded in Controls
        #endregion

        #region Clear
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
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            //Clearing TextBoxes
            txtStructureCategory.Text = string.Empty;

            //Clearing Viewstate Values 
            ViewState["StructureCategoryID"] = "0";
        }

        #endregion

        #region Load Grid / Bind Grid
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objStructureCategoryBLL = new StructureCategoryBLL();
            objStructureCategory = new StructureCategoryBO();

            objStructureCategory.StructureCategoryName = string.Empty;
            objStructureCategory.StructureCategoryID = 0;

            grdStructureCategory.DataSource = objStructureCategoryBLL.GetAllStructureCategory();
            grdStructureCategory.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStructureCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["StructureCategoryID"] = e.CommandArgument;
                GetStructureCategoryDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteStructureCategory(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// To display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdStructureCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStructureCategory.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion

        #region Edit Record
        /// <summary>
        /// To fetch data from database and assign to textbox
        /// </summary>
        private void GetStructureCategoryDetails()
        {
            objStructureCategoryBLL = new StructureCategoryBLL();
            int StructureCategoryID = 0;

            if (ViewState["StructureCategoryID"] != null)
                StructureCategoryID = Convert.ToInt32(ViewState["StructureCategoryID"].ToString());

            objStructureCategory = new StructureCategoryBO();
            objStructureCategory = objStructureCategoryBLL.GetStructureCategoryById(StructureCategoryID);

            txtStructureCategory.Text = objStructureCategory.StructureCategoryName;
        }
        #endregion

        #region Delete Record
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

                string structureCategoryID = ((Literal)gr.FindControl("litStructureCategoryID")).Text;
                objStructureCategoryBLL = new StructureCategoryBLL();

                message = objStructureCategoryBLL.ObsoleteStructureCategoty(Convert.ToInt32(structureCategoryID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
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
        /// To delete record from database based on structureCategoryID
        /// </summary>
        private void DeleteStructureCategory(string structureCategoryID)
        {
            objStructureCategoryBLL = new StructureCategoryBLL();
            string message = string.Empty;

            message = objStructureCategoryBLL.DeleteStructureCategory(Convert.ToInt32(structureCategoryID));

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";
            ClearDetails();
            BindGrid(false, true);
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        #endregion

        #region Save Record
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objStructureCategory = new StructureCategoryBO();
            objStructureCategoryBLL = new StructureCategoryBLL();

            //Assignement
            objStructureCategory.StructureCategoryName = txtStructureCategory.Text.Trim();

            if (ViewState["StructureCategoryID"] != null)
                objStructureCategory.StructureCategoryID = Convert.ToInt32(ViewState["StructureCategoryID"].ToString());

            objStructureCategory.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objStructureCategory.UserID = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objStructureCategory.StructureCategoryID < 1)
            {
                //If StructureCategoryID does Not exists then SaveStructureCategory
                objStructureCategory.StructureCategoryID = -1;//For New StructureCategory
                message = objStructureCategoryBLL.AddStructureCategory(objStructureCategory);

                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully.";
                    ClearDetails();
                    BindGrid(true, false);
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Structure Category Added Successfully!!');", true);
            }
            else
            {
                //If StructureCategoryID exists then UpdateStructureCategory
                message = objStructureCategoryBLL.UpdateStructureCategory(objStructureCategory); //For Updating StructureCategory
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully.";
                    ClearDetails();
                    BindGrid(true, false);
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Structure Category updated successfully!!');", true);
            }
            //ClearDetails();
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            SetUpdateMode(false);
        }
        #endregion
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
                ViewState["StructureCategoryID"] = "0";
            }
        }
    }
}