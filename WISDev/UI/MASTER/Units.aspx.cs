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
    public partial class Units : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        UnitBO objUnit;
        UnitBLL objUnitBLL;
        #endregion

        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Units";
                ViewState["UnitID"] = 0;
                BindGrid(false, false);
                //txtUnit.Attributes.Add("onchange", "isDirty = 1;");
                txtUnit.Attributes.Add("onchange", "setDirtyText();");
                // ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdUnit.Columns[3].Visible = false;
                    grdUnit.Columns[4].Visible = false;
                    grdUnit.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdUnit.Rows)
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

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        #region Clear

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        protected void ClearDetails()
        {
            //Clearing TextBoxes
            txtUnit.Text = string.Empty;

            //Clearing Viewstate Values 
            ViewState["UnitID"] = "0";
        }

        #endregion

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        #region Load Grid / Bind Grid
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objUnitBLL = new UnitBLL();
            objUnit = new UnitBO();

            objUnit.UnitName = string.Empty;
            objUnit.UnitID = 0;

            grdUnit.DataSource = objUnitBLL.GetAllUnit();//(objUnit);
            grdUnit.DataBind();
        }

        protected void grdUnit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["UnitID"] = e.CommandArgument;
                GetUnitDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteUnit(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }

        protected void grdUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUnit.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion

        /// <summary>
        /// edit Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Edit Record
        private void GetUnitDetails()
        {
            objUnitBLL = new UnitBLL();
            int UnitID = 0;

            if (ViewState["UnitID"] != null)
                UnitID = Convert.ToInt32(ViewState["UnitID"].ToString());

            objUnit = new UnitBO();
            objUnit = objUnitBLL.GetUnitById(UnitID);

            txtUnit.Text = objUnit.UnitName;
        }
        #endregion

        /// <summary>
        /// delete Data from Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Delete Record
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string unitID = ((Literal)gr.FindControl("litUnitID")).Text;
                objUnitBLL = new UnitBLL();

                message = objUnitBLL.ObsoleteUnit(Convert.ToInt32(unitID), Convert.ToString(chk.Checked));
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
        private void DeleteUnit(string unitID)
        {
            objUnitBLL = new UnitBLL();
            string message = string.Empty;

            message = objUnitBLL.DeleteUnit(Convert.ToInt32(unitID));
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";
            
            ClearDetails();
            BindGrid(false, true);
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        #endregion

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Save Record
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objUnit = new UnitBO();
            objUnitBLL = new UnitBLL();

            //Assignement
            objUnit.UnitName = txtUnit.Text.Trim();

            if (ViewState["UnitID"] != null)
                objUnit.UnitID = Convert.ToInt32(ViewState["UnitID"].ToString());

            objUnit.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objUnit.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objUnit.UnitID < 1)
            {
                //If UnitID does Not exists then SaveUnit
                objUnit.UnitID = -1;//For New Unit
                message = objUnitBLL.AddUnit(objUnit);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            else
            {
                //If UnitID exists then UpdateUnit
                message = objUnitBLL.UpdateUnit(objUnit); //For Updating Unit

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }

            }

            if (message != "")
            {
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }

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
                ViewState["UnitID"] = "0";
            }
        }
    }
}