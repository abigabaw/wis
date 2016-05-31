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
    public partial class FloorType : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        FloorTypeBO objFloorType;
        FloorTypeBLL objFloorTypeBLL;
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
                Master.PageHeader = "Floor Type";
                ViewState["FloorTypeID"] = 0;
                BindGrid(false, false);
                //txtFloorType.Attributes.Add("onchange", "isDirty = 1;");
                txtFloorType.Attributes.Add("onchange", "setDirtyText();");
                // ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdFloorType.Columns[3].Visible = false;
                    grdFloorType.Columns[4].Visible = false;
                    grdFloorType.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdFloorType.Rows)
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
            txtFloorType.Text = string.Empty;

            //Setting Default Index Selected Value "0" to DropDowns

            //Clearing Viewstate Values 
            ViewState["FloorTypeID"] = "0";
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objFloorTypeBLL = new FloorTypeBLL();
            objFloorType = new FloorTypeBO();

            objFloorType.FloorTypeName = string.Empty;
            objFloorType.FloorTypeID = 0;

            grdFloorType.DataSource = objFloorTypeBLL.GetAllFloorType();//(objFloorType);
            grdFloorType.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdFloorType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["FloorTypeID"] = e.CommandArgument;
                GetFloorTypeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteFloorType(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// To show pageno in the grid
        /// </summary>

        protected void grdFloorType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFloorType.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }


        /// <summary>
        /// To edit details
        /// </summary>
        private void GetFloorTypeDetails()
        {
            objFloorTypeBLL = new FloorTypeBLL();
            int FloorTypeID = 0;

            if (ViewState["FloorTypeID"] != null)
                FloorTypeID = Convert.ToInt32(ViewState["FloorTypeID"].ToString());

            objFloorType = new FloorTypeBO();
            objFloorType = objFloorTypeBLL.GetFloorTypeById(FloorTypeID);

            txtFloorType.Text = objFloorType.FloorTypeName;
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

                string floorTypeID = ((Literal)gr.FindControl("litFloorTypeID")).Text;
                objFloorTypeBLL = new FloorTypeBLL();

                message = objFloorTypeBLL.ObsoleteFloorType(Convert.ToInt32(floorTypeID), Convert.ToString(chk.Checked));
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
        /// To delete details
        /// </summary
        private void DeleteFloorType(string floorTypeID)
        {
            objFloorTypeBLL = new FloorTypeBLL();
            string message = string.Empty;
            try
            {
                int UserID_ = Convert.ToInt32(Session["USER_ID"].ToString());
                message = objFloorTypeBLL.DeleteFloorType(Convert.ToInt32(floorTypeID), UserID_);
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

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objFloorType = new FloorTypeBO();
            objFloorTypeBLL = new FloorTypeBLL();

            //Assignement
            objFloorType.FloorTypeName = txtFloorType.Text.Trim();

            if (ViewState["FloorTypeID"] != null)
                objFloorType.FloorTypeID = Convert.ToInt32(ViewState["FloorTypeID"].ToString());

            objFloorType.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objFloorType.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objFloorType.FloorTypeID < 1)
            {
                //If FloorTypeID does Not exists then SaveFloorType
                objFloorType.FloorTypeID = -1;//For New FloorType
                message = objFloorTypeBLL.AddFloorType(objFloorType);

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
                //If FloorTypeID exists then UpdateFloorType
                message = objFloorTypeBLL.UpdateFloorType(objFloorType); //For Updating FloorType

                AlertMessage = "alert('" + message + "');";

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
            SetUpdateMode(false);
        }
   
        /// <summary>
        /// to change text of the button based on condition
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
                ViewState["FloorTypeID"] = "0";
            }
        }
    }
}