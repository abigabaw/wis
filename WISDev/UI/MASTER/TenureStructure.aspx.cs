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
    public partial class TenureStructure : System.Web.UI.Page
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
                Master.PageHeader = "Structure Tenure";
                ViewState["STR_TENUREID"] = 0;
                BindGrid(false, false);
                //txtStructuretenure.Attributes.Add("onchange", "isDirty = 1;");
                txtStructuretenure.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_TENURE) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnSearch.Visible = false;
                    btnShowSearch.Visible = false;
                    GrdStructureTenure.Columns[2].Visible = false;
                    GrdStructureTenure.Columns[3].Visible = false;
                    GrdStructureTenure.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in GrdStructureTenure.Rows)
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
            TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
            GrdStructureTenure.DataSource = objTenureStructureBLL.GetAllTenureStructures("");
            GrdStructureTenure.DataBind();
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtStructuretenure.Text = "";
            txtSearch.Text = "";
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Show Add Banks Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindGrid(false, false);
            btn_Save.Text = "Save";
            ClearDetails();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlStructureTenureDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlStructureTenureDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }

        /// <summary>
        /// Search banks data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string TenureStructureName;
            TenureStructureName = txtSearch.Text.Trim();
            TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
            GrdStructureTenure.DataSource = objTenureStructureBLL.GetTenureStructures(TenureStructureName);
            GrdStructureTenure.DataBind();
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";
            TenureStructureBO objTenureStructure = new TenureStructureBO();
            objTenureStructure.Str_TenureId = Convert.ToInt32(ViewState["STR_TENUREID"]);
            objTenureStructure.Str_Tenure = txtStructuretenure.Text.Trim();

            TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
            if (objTenureStructure.Str_TenureId == 0)
            {
                objTenureStructure.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objTenureStructureBLL.AddTenureStructure(objTenureStructure);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";
            }
            else
            {
                objTenureStructure.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objTenureStructureBLL.UpdateTenureStructure(objTenureStructure);
                SetUpdateMode(false);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            ClearDetails();
            BindGrid(true, false);
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdStructureTenure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["STR_TENUREID"] = e.CommandArgument;
                TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
                TenureStructureBO objTenureStructure = objTenureStructureBLL.GetTenureStructureItem(Convert.ToInt32(ViewState["STR_TENUREID"]));
                txtStructuretenure.Text = objTenureStructure.Str_Tenure;
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
                message = objTenureStructureBLL.DeleteTenureStructure(Convert.ToInt32(e.CommandArgument));
                SetUpdateMode(false);
                BindGrid(false, true);
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

                string tenureID = ((Literal)gr.FindControl("litTenureStructureID")).Text;
                TenureStructureBLL objTenureStructureBLL = new TenureStructureBLL();
                message = objTenureStructureBLL.ObsoleteTenureStructure(Convert.ToInt32(tenureID), Convert.ToString(chk.Checked));
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
        /// To change the page index
        /// </summary>
        
        protected void GrdStructureTenure_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdStructureTenure.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
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
                ViewState["STR_TENUREID"] = "0";
            }
        }
    }
}

