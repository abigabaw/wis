using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
namespace WIS
{
    public partial class CulturPropertiesMaster : System.Web.UI.Page
    {
        CulturePropertiesMasterBO CulturePropertiesMasterBObj = null;
        CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = null;
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
                Master.PageHeader = "Culture Property Type";
                ViewState["CONCERNID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.Cul_Property_Type) == false)
                {

                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCultureProp.Columns[2].Visible = false;
                    grdCultureProp.Columns[3].Visible = false;
                    grdCultureProp.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdCultureProp.Rows)
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
                string CulturepropTypeID = ((Literal)gr.FindControl("litCONCERNID")).Text;
                CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
                message = CulturePropertiesMasterBLLObj.ObsoleteCulturePropType(Convert.ToInt32(CulturepropTypeID), Convert.ToString(chk.Checked));
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
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            if (CulturPropertiesIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                try
                {
                    InsertIntoCultureProp();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (CulturPropertiesIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CulturePropertiesMasterBO CulturePropertiesMasterBObj = new CulturePropertiesMasterBO();
                    CulturePropertiesMasterBObj.CulturePropTypeName = CulturPropertiesTextBox.Text.ToString().Trim();
                    CulturePropertiesMasterBObj.CulturePropTypeID = Convert.ToInt32(CulturPropertiesIDTextBox.Text.ToString().Trim());
                    CulturePropertiesMasterBObj.UpdatedBy = Convert.ToInt32(uID);

                    CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
                    message = CulturePropertiesMasterBLLObj.EDITCulturePropByID(CulturePropertiesMasterBObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        clear();
                        SetUpdateMode(false);
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CulturePropertiesMasterBObj = null;
                }
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertIntoCultureProp()
        {
            string ErrorMessage = string.Empty;
            string AlertMessage = string.Empty;
            try
            {
            string uID = Session["USER_ID"].ToString();
            CulturePropertiesMasterBObj = new CulturePropertiesMasterBO();
            CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
            CulturePropertiesMasterBObj.CulturePropTypeName = CulturPropertiesTextBox.Text.ToString().Trim();
            CulturePropertiesMasterBObj.CreatedBy = uID;
            ErrorMessage=CulturePropertiesMasterBLLObj.InsertIntoCultureProp(CulturePropertiesMasterBObj);
            AlertMessage = "alert('" + ErrorMessage + "');";

            if (string.IsNullOrEmpty(ErrorMessage) || ErrorMessage == "" || ErrorMessage == "null")
            {
                ErrorMessage = "Data saved successfully";
                clear();
                BindGrid(true, true);
            }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            AlertMessage = "alert('" + ErrorMessage + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
            List<CulturePropertiesMasterBO> ListObj = new List<CulturePropertiesMasterBO>();
            DataTable dt_CultureProp = new DataTable();
            grdCultureProp.DataSource = CulturePropertiesMasterBLLObj.GetAllCulturePropertyType();
            grdCultureProp.DataBind();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void clear()
        {
            CulturPropertiesTextBox.Text = "";
            CulturPropertiesIDTextBox.Text = "";
        }
        /// <summary>
        /// To set pageno in grid
        /// </summary>

        protected void grdCultureProp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCultureProp.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCultureProp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["CulturePropTypeID"] = e.CommandArgument;
                int CulturePropTypeID = Convert.ToInt32(ViewState["CulturePropTypeID"]);
                GetCulturePropByID(CulturePropTypeID);
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
                message = CulturePropertiesMasterBLLObj.DeleteCulturePropByID(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                clear();
                SetUpdateMode(false);
                BindGrid(false, true);

            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To fetch details based on cultural propertyID
        /// </summary>
        protected void GetCulturePropByID(int CulturePropTypeID)
        {
            CulturePropertiesMasterBLL CulturePropertiesMasterBLLObj = null;
            CulturePropertiesMasterBO CulturePropertiesMasterBObj = null;
            try
            {
                CulturePropertiesMasterBLLObj = new CulturePropertiesMasterBLL();
                CulturePropertiesMasterBObj = new CulturePropertiesMasterBO();
                CulturePropertiesMasterBObj = CulturePropertiesMasterBLLObj.GetCulturePropByID(CulturePropTypeID);
                CulturPropertiesTextBox.Text = Convert.ToString(CulturePropertiesMasterBObj.CulturePropTypeName);
                CulturPropertiesIDTextBox.Text = Convert.ToString(CulturePropertiesMasterBObj.CulturePropTypeID); 
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
                SaveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                SaveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["CulturePropTypeID"] = "0";
                CulturPropertiesIDTextBox.Text = String.Empty;
            }
        }

      

       
       
    }
}