using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS.UI.MASTER;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class HIVContracted : System.Web.UI.Page
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
                Master.PageHeader = "HIV Contracted";
                ViewState["ContractedID"] = 0;  // ViewState ID
                BindGrid(false, false);
               // HIVCTextBox.Attributes.Add("onchange", "isDirty = 1;");
                HIVCTextBox.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_HIV_CONTRACTED) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdHIVC.Columns[grdHIVC.Columns.Count - 1].Visible = false;
                    grdHIVC.Columns[grdHIVC.Columns.Count - 2].Visible = false;
                    grdHIVC.Columns[grdHIVC.Columns.Count - 3].Visible = false;
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
            HIVContractedBLL HIVContractedBLLobj = new HIVContractedBLL();
            grdHIVC.DataSource = HIVContractedBLLobj.GetALLHIVContracted();
            grdHIVC.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            HIVContractedBLL HIVContractedBLLobj = null;

            try
            {
                HIVContractedBLLobj = new HIVContractedBLL();
                HIVContractedBO objHIVContracted = new HIVContractedBO();

                if (Convert.ToInt32(ViewState["ContractedID"]) > 0)
                {
                    objHIVContracted.ContractedThrough = HIVCTextBox.Text.Trim();
                    objHIVContracted.ContractedID = Convert.ToInt32(ViewState["ContractedID"]);

                    message = HIVContractedBLLobj.EDITHIVC(objHIVContracted);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }

                    SetUpdateMode(false);
                }
                else
                {
                    objHIVContracted.ContractedThrough = HIVCTextBox.Text.Trim();
                    objHIVContracted.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                    message = HIVContractedBLLobj.insertHIVC(objHIVContracted);

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
                HIVContractedBLLobj = null;
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            HIVCTextBox.Text = string.Empty;
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdHIVC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["ContractedID"] = e.CommandArgument;
                Get_HIVC_Details();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                HIVContractedBLL HIVContractedBLLobj = new HIVContractedBLL();
                string message = string.Empty;

                message = HIVContractedBLLobj.DeleteHIVC(Convert.ToInt32(e.CommandArgument));
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
        
        private void Get_HIVC_Details()
        {
            HIVContractedBLL HIVContractedBLLobj = new HIVContractedBLL();
            int ContractedID = 0;

            if (ViewState["ContractedID"] != null)
                ContractedID = Convert.ToInt32(ViewState["ContractedID"]);

            HIVContractedBO objHIVContracted = new HIVContractedBO();
            objHIVContracted = HIVContractedBLLobj.GetContractedID(ContractedID);

            HIVCTextBox.Text = objHIVContracted.ContractedThrough;
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

                string ContractedID = ((Literal)gr.FindControl("litContractedID")).Text;
                int ContractedID_ = Convert.ToInt32(ContractedID);
                HIVContractedBLL HIVContractedBLLobj = new HIVContractedBLL();
                message = HIVContractedBLLobj.ObsoleteHIVC(ContractedID_, Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));
               
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
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            {
                grdHIVC.PageIndex = e.NewPageIndex;
                // Refresh the list
                BindGrid(true, false);
            }
        }

        protected void grdHIVC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}