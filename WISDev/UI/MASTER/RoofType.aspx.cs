using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class RoofType : System.Web.UI.Page
    {
        #region Declaration

        RoofTypeBO objRoofType;
        RoofTypeBLL objRoofTypeBLL;
        #endregion

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
                Master.PageHeader = "Roof Type";
                ViewState["RoofTypeID"] = 0;
                BindGrid(false, false);
                txtRoofType.Attributes.Add("onchange", "isDirty = 1;");
                // ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdRoofType.Columns[3].Visible = false;
                    grdRoofType.Columns[4].Visible = false;
                    grdRoofType.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdRoofType.Rows)
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
  

        #region Load Default Values
        //Required when Data Loaded in Controls
        #endregion


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
            txtRoofType.Text = string.Empty;

            //Setting Default Index Selected Value "0" to DropDowns

            //Clearing Viewstate Values 
            ViewState["RoofTypeID"] = "0";
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
      
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objRoofTypeBLL = new RoofTypeBLL();
            objRoofType = new RoofTypeBO();

            objRoofType.RoofTypeName = string.Empty;
            objRoofType.RoofTypeID = 0;

            grdRoofType.DataSource = objRoofTypeBLL.GetAllRoofType();//(objRoofType);
            grdRoofType.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRoofType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["RoofTypeID"] = e.CommandArgument;
                GetRoofTypeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteRoofType(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        ///To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRoofType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRoofType.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }

        /// <summary>
        ///To fetch details from database and assign to textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param

     
        private void GetRoofTypeDetails()
        {
            objRoofTypeBLL = new RoofTypeBLL();
            int RoofTypeID = 0;

            if (ViewState["RoofTypeID"] != null)
                RoofTypeID = Convert.ToInt32(ViewState["RoofTypeID"].ToString());

            objRoofType = new RoofTypeBO();
            objRoofType = objRoofTypeBLL.GetRoofTypeById(RoofTypeID);

            txtRoofType.Text = objRoofType.RoofTypeName;
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

                string roofTypeID = ((Literal)gr.FindControl("litRoofTypeID")).Text;
                objRoofTypeBLL = new RoofTypeBLL();

                message = objRoofTypeBLL.ObsoleteRoofType(Convert.ToInt32(roofTypeID), Convert.ToString(chk.Checked));
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
        /// To delete roof type based on ID
        /// </summary>
        private void DeleteRoofType(string roofTypeID)
        {
            objRoofTypeBLL = new RoofTypeBLL();
           int UserID_=0;
            string message = string.Empty;

            if(Session["USER_ID"]!=null)
                UserID_ = Convert.ToInt32(Session["USER_ID"].ToString());

            message = objRoofTypeBLL.DeleteRoofType(Convert.ToInt32(roofTypeID), UserID_);

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data deleted successfully";
            ClearDetails();
            BindGrid(false, true);
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objRoofType = new RoofTypeBO();
            objRoofTypeBLL = new RoofTypeBLL();

            objRoofType.RoofTypeName = txtRoofType.Text.Trim();

            if (ViewState["RoofTypeID"] != null)
                objRoofType.RoofTypeID = Convert.ToInt32(ViewState["RoofTypeID"].ToString());

            objRoofType.IsDeleted = "False";

            objRoofType.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objRoofType.RoofTypeID < 1)
            {
                objRoofType.RoofTypeID = -1;//For New RoofType
                message = objRoofTypeBLL.AddRoofType(objRoofType);
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
                message = objRoofTypeBLL.UpdateRoofType(objRoofType); //For Updating RoofType
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    BindGrid(true, false);
                    ClearDetails();
                }

            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);

            SetUpdateMode(false);
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>s

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
                ViewState["RoofTypeID"] = "0";
            }
        }
    }
}