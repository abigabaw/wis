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
    public partial class StructureType : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        StructureTypeBO objStructureType;
        StructureTypeBLL objStructureTypeBLL;
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
                Master.PageHeader = "Structure Type";
                ViewState["StructureTypeID"] = 0;
                BindGrid(false, false);
                //txtStructureType.Attributes.Add("onchange", "setDirtyText();");
                txtStructureType.Attributes.Add("onchange", "setDirtyText();");

               // ClearDetails();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdStructureType.Columns[3].Visible = false;
                    grdStructureType.Columns[4].Visible = false;
                    grdStructureType.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdStructureType.Rows)
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

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        #region Clear TextBoxes

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
            txtStructureType.Text = "";
            ViewState["StructureTypeID"] = 0;
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
            objStructureTypeBLL = new StructureTypeBLL();
            objStructureType = new StructureTypeBO();

            objStructureType.StructureTypeName = string.Empty;
            objStructureType.StructureTypeID = 0;

            grdStructureType.DataSource = objStructureTypeBLL.GetAllStructureType();//(objStructureType);
            grdStructureType.DataBind();
        }

        protected void grdStructureType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["StructureTypeID"] = e.CommandArgument;
                GetStructureTypeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteStructureType(e.CommandArgument.ToString());
                BindGrid(false, true);
            }
        }

        protected void grdStructureType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdStructureType.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }

        #endregion

        /// <summary>
        /// Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Edit Record
        private void GetStructureTypeDetails()
        {
            objStructureTypeBLL = new StructureTypeBLL();
            int StructureTypeID = 0;

            if (ViewState["StructureTypeID"] != null)
                StructureTypeID = Convert.ToInt32(ViewState["StructureTypeID"].ToString());

            objStructureType = new StructureTypeBO();
            objStructureType = objStructureTypeBLL.GetStructureTypeById(StructureTypeID);

            txtStructureType.Text = objStructureType.StructureTypeName;
        }
        #endregion

        /// <summary>
        /// Delete Data from Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Delete Record
        private void DeleteStructureType(string structureTypeID)
        {
            objStructureTypeBLL = new StructureTypeBLL();
            string message = string.Empty;

            message = objStructureTypeBLL.DeleteStructureType(Convert.ToInt32(structureTypeID));

            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";
            
            ClearDetails();
            SetUpdateMode(false);
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

            objStructureType = new StructureTypeBO();
            objStructureTypeBLL = new StructureTypeBLL();

            //Assignement
            objStructureType.StructureTypeName = txtStructureType.Text.Trim();

            if (ViewState["StructureTypeID"] != null)
                objStructureType.StructureTypeID = Convert.ToInt32(ViewState["StructureTypeID"].ToString());

            objStructureType.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objStructureType.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            if (objStructureType.StructureTypeID < 1)
            {
                //If StructureTypeID does Not exists then SaveStructureType
                objStructureType.StructureTypeID = -1;//For New StructureType
                message = objStructureTypeBLL.AddStructureType(objStructureType);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully.";                
                }
            }
            else
            {
                //If StructureTypeID exists then UpdateStructureType
                message = objStructureTypeBLL.UpdateStructureType(objStructureType); //For Updating StructureType
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully.";
                }
            }
            ClearDetails();
            BindGrid(true, false);
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            SetUpdateMode(false);
           
        }
        #endregion

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
                
                string structureTypeID = ((Literal)gr.FindControl("litStructureTypeID")).Text;
                StructureTypeBLL oStructureTypeBLL = new StructureTypeBLL();

                message = oStructureTypeBLL.ObsoleteStructureType(Convert.ToInt32(structureTypeID), Convert.ToString(chk.Checked));
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
                ViewState["StructureTypeID"] = "0";
            }
        }
    }
}