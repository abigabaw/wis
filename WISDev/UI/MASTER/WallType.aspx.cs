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
    public partial class WallType : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        WallTypeBO objWallType;
        WallTypeBLL objWallTypeBLL;
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
                Master.PageHeader = "Wall Type";
                ViewState["WallTypeID"] = 0;
                BindGrid(false, false);
                //txtWallType.Attributes.Add("onchange", "isDirty = 1;");
                txtWallType.Attributes.Add("onchange", "setDirtyText();");
             //   ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdWallType.Columns[3].Visible = false;
                    grdWallType.Columns[4].Visible = false;
                    grdWallType.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdWallType.Rows)
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
            txtWallType.Text = string.Empty;
            ViewState["WallTypeID"] = "0";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
        }

        #endregion

        /// <summary>
        /// Load the data into grid and bind
        /// </summary>
        #region Load Grid / Bind Grid
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objWallTypeBLL = new WallTypeBLL();
            objWallType = new WallTypeBO();

            objWallType.WallTypeName = string.Empty;
            objWallType.WallTypeID = 0;

            grdWallType.DataSource = objWallTypeBLL.GetAllWallType();
            grdWallType.DataBind();
        }

        protected void grdWallType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["WallTypeID"] = e.CommandArgument;
                GetWallTypeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteWallType(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        protected void grdWallType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWallType.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion

        /// <summary>
        /// Edit the data.
        /// </summary>
        #region Edit Record
        private void GetWallTypeDetails()
        {
            objWallTypeBLL = new WallTypeBLL();
            int WallTypeID = 0;

            if (ViewState["WallTypeID"] != null)
                WallTypeID = Convert.ToInt32(ViewState["WallTypeID"].ToString());

            objWallType = new WallTypeBO();
            objWallType = objWallTypeBLL.GetWallTypeById(WallTypeID);

            txtWallType.Text = objWallType.WallTypeName;
        }
        #endregion

        /// <summary>
        /// Delete the data.
        /// </summary>
        #region Delete Record
        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string wallTypeID = ((Literal)gr.FindControl("litWallTypeID")).Text;
                objWallTypeBLL = new WallTypeBLL();

                message = objWallTypeBLL.ObsoleteWallType(Convert.ToInt32(wallTypeID), Convert.ToString(chk.Checked));
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

        private void DeleteWallType(string wallTypeID)
        {
            WallTypeBO oWallType = new WallTypeBO();
            objWallTypeBLL = new WallTypeBLL();
            string message = string.Empty;
            try
            {
                oWallType.WallTypeID = Convert.ToInt32(wallTypeID);
                oWallType.UserID = Convert.ToInt32(Session["USER_ID"].ToString());
                message = objWallTypeBLL.DeleteWallType(oWallType);

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
        /// Save the data.
        /// </summary>
        #region Save Record
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objWallType = new WallTypeBO();
            objWallTypeBLL = new WallTypeBLL();

            //Assignement
            objWallType.WallTypeName = txtWallType.Text.Trim();

            if (ViewState["WallTypeID"] != null)
                objWallType.WallTypeID = Convert.ToInt32(ViewState["WallTypeID"].ToString());

            objWallType.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objWallType.UserID = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objWallType.WallTypeID < 1)
            {
                //If WallTypeID does Not exists then SaveWallType
                objWallType.WallTypeID = -1;//For New WallType
                message = objWallTypeBLL.AddWallType(objWallType);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Wall Type Added Successfully');", true);
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
                //If WallTypeID exists then UpdateWallType
                message = objWallTypeBLL.UpdateWallType(objWallType); //For Updating WallType
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Wall Type updated successfully');", true);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            //ClearDetails();
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            //BindGrid(true, false);
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
                ViewState["WallTypeID"] = "0";
            }
        }
    }
}