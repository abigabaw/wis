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
    public partial class GrievancesMaster : System.Web.UI.Page
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
                Master.PageHeader = "Consultant Type";
                ViewState["GRIEVANCECATEGID"] = 0;  // ViewState ID
                BindGrid(false, false);

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.Grievances_Category) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdGrievancesCategory.Columns[grdGrievancesCategory.Columns.Count - 1].Visible = false;
                    grdGrievancesCategory.Columns[grdGrievancesCategory.Columns.Count - 2].Visible = false;
                    grdGrievancesCategory.Columns[grdGrievancesCategory.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in grdGrievancesCategory.Rows)
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
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            GrievancesMasterBLL GrievancesMasterBLLobj = new GrievancesMasterBLL();
            grdGrievancesCategory.DataSource = GrievancesMasterBLLobj.GetALLGrievancesCategory();
            grdGrievancesCategory.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            GrievancesMasterBLL GrievancesMasterBLLobj = null;

            try
            {
                GrievancesMasterBLLobj = new GrievancesMasterBLL();
                GrievancesMasterBO objGrievancesMaster = new GrievancesMasterBO();

                if (Convert.ToInt32(ViewState["GRIEVANCECATEGID"]) > 0)
                {
                    objGrievancesMaster.GrievancesCategory = txtGrievancesCategory.Text.Trim();
                    objGrievancesMaster.GRIEVANCECATEGID = Convert.ToInt32(ViewState["GRIEVANCECATEGID"]);

                    message = GrievancesMasterBLLobj.EDITGrievancesCategory(objGrievancesMaster);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }

                    SetUpdateMode(false);
                }
                else
                {
                    objGrievancesMaster.GrievancesCategory = txtGrievancesCategory.Text.Trim();
                    objGrievancesMaster.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                    message = GrievancesMasterBLLobj.insert(objGrievancesMaster);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                }

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                clearfields();
                BindGrid(true, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GrievancesMasterBLLobj = null;
            }
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
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
                ViewState["GRIEVANCECATEGID"] = "0";
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            txtGrievancesCategory.Text = string.Empty;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdGrievancesCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["GRIEVANCECATEGID"] = e.CommandArgument;
                GetGrievancesCategoryDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                GrievancesMasterBLL GrievancesMasterBLLobj = new GrievancesMasterBLL();
                string message = string.Empty;

                message = GrievancesMasterBLLobj.DeleteGrievancesCategory(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                SetUpdateMode(false);
                clearfields();
                BindGrid(false, true);
            }
        }

        /// <summary>
        /// get the Grid value into textBox
        /// </summary>
       
        private void GetGrievancesCategoryDetails()
        {
            GrievancesMasterBLL GrievancesMasterBLLobj = new GrievancesMasterBLL();
            int GRIEVANCECATEGID = 0;

            if (ViewState["GRIEVANCECATEGID"] != null)
                GRIEVANCECATEGID = Convert.ToInt32(ViewState["GRIEVANCECATEGID"]);

            GrievancesMasterBO objGrievancesMaster = new GrievancesMasterBO();
            objGrievancesMaster = GrievancesMasterBLLobj.GetGrievancesCategoryId(GRIEVANCECATEGID);

            txtGrievancesCategory.Text = objGrievancesMaster.GrievancesCategory;
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

                string GRIEVANCECATEGID = ((Literal)gr.FindControl("litGRIEVANCECATEGID")).Text;
                int GRIEVANCECATEGID_ = Convert.ToInt32(GRIEVANCECATEGID);
                GrievancesMasterBLL GrievancesMasterBLLobj = new GrievancesMasterBLL();
                message = GrievancesMasterBLLobj.ObsoleteconsultantType(GRIEVANCECATEGID_, Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                //Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                BindGrid(false, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// To change page in grid 
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            {
                grdGrievancesCategory.PageIndex = e.NewPageIndex;
                // Refresh the list
                BindGrid(true, false);
            }
        }

        protected void grdGrievancesCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}