using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS.UI.MASTER
{
    public partial class LocationClassification : System.Web.UI.Page
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
                Master.PageHeader = "Location";
                ViewState["LOCTNCLASFCTNID"] = 0;  // ViewState ID
                BindGrid(false, false);
                txtLocation.Attributes.Add("onchange", "setDirtyText();");
                txtcompland.Attributes.Add("onchange", "setDirtyText();");
            }
            if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
            {
                btnSave.Visible = false;
                btnClear.Visible = false;

                grdLocation.Columns[grdLocation.Columns.Count - 1].Visible = false;
                grdLocation.Columns[grdLocation.Columns.Count - 2].Visible = false;
                grdLocation.Columns[grdLocation.Columns.Count - 3].Visible = false;
            }

        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {

            LocationClassificationBLL BLLobj = new LocationClassificationBLL();
            grdLocation.DataSource = BLLobj.GetallLOCATION();
            grdLocation.DataBind();
                      
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

                string LOCTNCLASFCTNID = ((Literal)gr.FindControl("litLOCTNCLASFCTNID")).Text;
                int LOCTNCLASFCTNID_ = Convert.ToInt32(LOCTNCLASFCTNID);
                LocationClassificationBLL BLLobj = new LocationClassificationBLL();
                message = BLLobj.ObsoleteLocation(LOCTNCLASFCTNID_, Convert.ToString(chk.Checked), Convert.ToInt32(Session["USER_ID"]));

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
        ///To fetch location details and assign to textbox
        /// </summary>
      
        private void Get_location_Details()
        {
            LocationClassificationBLL BLLobj = new LocationClassificationBLL();
        
            int LOCTNCLASFCTNID = 0;

            if (ViewState["LOCTNCLASFCTNID"] != null)
                LOCTNCLASFCTNID = Convert.ToInt32(ViewState["LOCTNCLASFCTNID"]);

            LocationClassificationBO BOobj = new LocationClassificationBO();
            
            BOobj = BLLobj.GetLOCTNCLASFCTNID(LOCTNCLASFCTNID);

            txtcompland.Text = BOobj.LOCTNCODE;
            txtLocation.Text = BOobj.LOCTNCLASFCTNNAME;
          

        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            LocationClassificationBLL BLLobj = null;
            try
            {
                BLLobj = new LocationClassificationBLL();
                LocationClassificationBO BOobj = new LocationClassificationBO();

                if (Convert.ToInt32(ViewState["LOCTNCLASFCTNID"]) > 0)
                {
                    BOobj.LOCTNCLASFCTNID = Convert.ToInt32(ViewState["LOCTNCLASFCTNID"]);
                    BOobj.LOCTNCLASFCTNNAME = txtLocation.Text.Trim();
                    BOobj.LOCTNCODE = txtcompland.Text.Trim();
                    BOobj.CREATEDBY = Convert.ToInt32(Session["USER_ID"]);
                    message = BLLobj.UPDATElocation(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data Updated Successfully";
                    }
                    SetUpdateMode(false);
                }
                else
                {
                    BOobj.CREATEDBY = Convert.ToInt32(Session["USER_ID"]);
                    BOobj.LOCTNCLASFCTNNAME = txtLocation.Text.Trim();
                    BOobj.LOCTNCODE = txtcompland.Text.Trim();
                    message = BLLobj.INSERTlocation(BOobj);

                    if ( string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data Saved Successfully";
                    }
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
                    BLLobj = null;
                }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void clearfields()
        {
            txtcompland.Text = string.Empty;
            txtLocation.Text = string.Empty;
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["LOCTNCLASFCTNID"] = "0";
            }
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            {
                grdLocation.PageIndex = e.NewPageIndex;
                // Refresh the list
                BindGrid(true, false);
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
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
        protected void grdLocation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["LOCTNCLASFCTNID"] = e.CommandArgument;
                Get_location_Details();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LocationClassificationBLL BLLobj = new LocationClassificationBLL();
                string message = string.Empty;

                message = BLLobj.DeleteLocation(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                SetUpdateMode(false);
                clearfields();
                BindGrid(false, true);
            }
        }
    }
}