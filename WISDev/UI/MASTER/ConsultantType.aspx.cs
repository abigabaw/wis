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
    public partial class ConsultantType : System.Web.UI.Page
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
                ViewState["consultantTypeID"] = 0;  // ViewState ID
                BindGrid(false, false);
                consultanttypeTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CONSULTANT_TYPE) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdconsultanttype.Columns[grdconsultanttype.Columns.Count - 1].Visible = false;
                    grdconsultanttype.Columns[grdconsultanttype.Columns.Count - 2].Visible = false;
                    grdconsultanttype.Columns[grdconsultanttype.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in grdconsultanttype.Rows)
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
            ConsultantTypeBLL ConsultantTypeBLLobj = new ConsultantTypeBLL();
            grdconsultanttype.DataSource = ConsultantTypeBLLobj.GetALLConsultantType();
            grdconsultanttype.DataBind();
        }
        /// <summary>
        /// to insert data to database
        /// </summary>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            ConsultantTypeBLL ConsultantTypeBLLobj = null;

            try
            {
                ConsultantTypeBLLobj = new ConsultantTypeBLL();
                ConsultantTypeBO objconsultantType = new ConsultantTypeBO();

                if (Convert.ToInt32(ViewState["consultantTypeID"]) > 0)
                {
                    objconsultantType.ConsultantType = consultanttypeTextBox.Text.Trim();
                    objconsultantType.ConsultantTypeID = Convert.ToInt32(ViewState["consultantTypeID"]);

                    message = ConsultantTypeBLLobj.EDITConsultantType(objconsultantType);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }

                    SetUpdateMode(false);
                }
                else
                {
                     objconsultantType.ConsultantType = consultanttypeTextBox.Text.Trim();
                    objconsultantType.CreatedBy =  Convert.ToInt32(Session["USER_ID"]);
                   
                    message = ConsultantTypeBLLobj.insert(objconsultantType);

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
                ConsultantTypeBLLobj = null;
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
                ViewState["consultantTypeID"] = "0";
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            consultanttypeTextBox.Text = string.Empty;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdconsultanttype_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["consultantTypeID"] = e.CommandArgument;
                GetConsultantTypeDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ConsultantTypeBLL ConsultantTypeBLLobj = new ConsultantTypeBLL();
                string message = string.Empty;

                message = ConsultantTypeBLLobj.DeleteConsultantType(Convert.ToInt32(e.CommandArgument));
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
        private void GetConsultantTypeDetails()
        {
            ConsultantTypeBLL ConsultantTypeBLLobj = new ConsultantTypeBLL();
            int consultantTypeID = 0;

            if (ViewState["consultantTypeID"] != null)
                consultantTypeID = Convert.ToInt32(ViewState["consultantTypeID"]);

            ConsultantTypeBO objconsultantType = new ConsultantTypeBO();
            objconsultantType = ConsultantTypeBLLobj.GetConsultantTypeId(consultantTypeID);

            consultanttypeTextBox.Text = objconsultantType.ConsultantType;
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

                string consultantTypeID = ((Literal)gr.FindControl("litconsultantTypeID")).Text;
                int consultantTypeID_ = Convert.ToInt32(consultantTypeID);
                ConsultantTypeBLL ConsultantTypeBLLobj = new ConsultantTypeBLL();
                message = ConsultantTypeBLLobj.ObsoleteconsultantType(consultantTypeID_, Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
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
        ///calls clear method
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
        ///used to change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            {
                grdconsultanttype.PageIndex = e.NewPageIndex;
                // Refresh the list
                BindGrid(true, false);
            }
        }

        protected void grdconsultanttype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}