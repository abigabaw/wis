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
    public partial class WindowType : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        WindowTypeBO objWindowType;
        WindowTypeBLL objWindowTypeBLL;
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
                Master.PageHeader = "Window Type";
                ViewState["WindowTypeID"] = 0;
                BindGrid(false, false);
                //txtWindowType.Attributes.Add("onchange", "isDirty = 1;");
                txtWindowType.Attributes.Add("onchange", "setDirtyText();");
                //ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdWindowType.Columns[3].Visible = false;
                    grdWindowType.Columns[4].Visible = false;
                    grdWindowType.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdWindowType.Rows)
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
            txtWindowType.Text = string.Empty;

            SetUpdateMode(false);

            //Clearing Viewstate Values 
            ViewState["WindowTypeID"] = "0";
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
            objWindowTypeBLL = new WindowTypeBLL();
            objWindowType = new WindowTypeBO();
            objWindowType.WindowTypeName = string.Empty;
            objWindowType.WindowTypeID = 0;
            grdWindowType.DataSource = objWindowTypeBLL.GetAllWindowType();//(objWindowType);
            grdWindowType.DataBind();
        }

        protected void grdWindowType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["WindowTypeID"] = e.CommandArgument;
                GetWindowTypeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteWindowType(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        protected void grdWindowType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWindowType.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion

        /// <summary>
        /// Edit Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Edit Record
        private void GetWindowTypeDetails()
        {
            objWindowTypeBLL = new WindowTypeBLL();
            int WindowTypeID = 0;

            if (ViewState["WindowTypeID"] != null)
                WindowTypeID = Convert.ToInt32(ViewState["WindowTypeID"].ToString());

            objWindowType = new WindowTypeBO();
            objWindowType = objWindowTypeBLL.GetWindowTypeById(WindowTypeID);

            txtWindowType.Text = objWindowType.WindowTypeName;
        }
        #endregion

        /// <summary>
        /// Update Database Make data as Obsoluted
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

                string windowTypeID = ((Literal)gr.FindControl("litWindowTypeID")).Text;
                objWindowTypeBLL = new WindowTypeBLL();

                message = objWindowTypeBLL.ObsoleteWindowType(Convert.ToInt32(windowTypeID), Convert.ToString(chk.Checked));
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

        private void DeleteWindowType(string windowTypeID)
        {
            WindowTypeBO oWindowType = new WindowTypeBO();
            objWindowTypeBLL = new WindowTypeBLL();
            string message = string.Empty;

            try
            {
                oWindowType.WindowTypeID = Convert.ToInt32(windowTypeID);
                oWindowType.UserID = Convert.ToInt32(Session["USER_ID"].ToString());
                message = objWindowTypeBLL.DeleteWindowType(oWindowType);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
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

            objWindowType = new WindowTypeBO();
            objWindowTypeBLL = new WindowTypeBLL();

            //Assignement
            objWindowType.WindowTypeName = txtWindowType.Text.Trim();

            if (ViewState["WindowTypeID"] != null)
                objWindowType.WindowTypeID = Convert.ToInt32(ViewState["WindowTypeID"].ToString());

            objWindowType.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objWindowType.UserID = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objWindowType.WindowTypeID < 1)
            {
                //If WindowTypeID does Not exists then SaveWindowType
                objWindowType.WindowTypeID = -1;//For New WindowType
                message = objWindowTypeBLL.AddWindowType(objWindowType);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Window Type Added Successfully');", true);
            }
            else
            {
                //If WindowTypeID exists then UpdateWindowType
                message = objWindowTypeBLL.UpdateWindowType(objWindowType); //For Updating WindowType
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Window Type updated successfully');", true);
            }
            // ClearDetails();
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            //BindGrid(true, false);
        }

        #endregion
        //add by Victory
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
                ViewState["WindowTypeID"] = "0";
            }
        }
    }
}