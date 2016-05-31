using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.MASTER
{
    public partial class LandReceivedMST : System.Web.UI.Page
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
                Master.PageHeader = "Land Received Form";
                ViewState["LandReceivedID"] = 0;  // ViewState ID
                BindGrid(); //Calling the Grid Data

                txtLandReceived.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LAND_RECEIVED_FROM) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdLandReceived.Columns[2].Visible = false;
                    grdLandReceived.Columns[3].Visible = false;
                    grdLandReceived.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdLandReceived.Rows)
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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;
            LandReceivedMSTBLL LandReceivedMSTOBJ = new LandReceivedMSTBLL();
            LandReceivedMSTBO LandReceivedMSTBOObj = new LandReceivedMSTBO();

            int uID = Convert.ToInt32(Session["USER_ID"].ToString());
            if (txtLandReceivedID.Text.ToString().Trim() == string.Empty)
            {

                try
                {

                    LandReceivedMSTBOObj.LandReceived = txtLandReceived.Text.ToString().Trim();
                    LandReceivedMSTBOObj.UserID = uID;
                    message = LandReceivedMSTOBJ.InsertLandReceived(LandReceivedMSTBOObj);

                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                        Clear();
                        BindGrid();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    LandReceivedMSTOBJ = null;
                }
            }
            else
            {
                try
                {

                    LandReceivedMSTBOObj.LandReceived = txtLandReceived.Text.ToString().Trim();
                    LandReceivedMSTBOObj.LandReceivedID = Convert.ToInt32(txtLandReceivedID.Text.ToString().Trim());
                    LandReceivedMSTBOObj.UserID = Convert.ToInt32(uID);

                    message = LandReceivedMSTOBJ.EDITLANDRECEIVED(LandReceivedMSTBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        Clear();
                        SetUpdateMode(false);
                        BindGrid();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    LandReceivedMSTOBJ = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            LandReceivedMSTBLL LandReceivedMSTOBJ = new LandReceivedMSTBLL();
            grdLandReceived.DataSource = LandReceivedMSTOBJ.GetAllLandReceived();
            grdLandReceived.DataBind();
        }
        /// <summary>
        ///change Page in the Grid
        /// </summary>
        
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdLandReceived.PageIndex = e.NewPageIndex;
            // Refresh the list LandReceivedID
            BindGrid();
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
                LandReceivedMSTBLL LandReceivedMSTOBJ = new LandReceivedMSTBLL();

                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                string landReceivedID = ((Literal)gr.FindControl("litLandReceivedID")).Text;

                message = LandReceivedMSTOBJ.ObsoleteLandReceived(Convert.ToInt32(landReceivedID), Convert.ToString(chk.Checked));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLandReceived_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["LandReceivedID"] = e.CommandArgument;
                GetLandReceivedByID();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LandReceivedMSTBLL LandReceivedMSTOBJ = new LandReceivedMSTBLL();

                message = LandReceivedMSTOBJ.DeleteLandReceived(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                txtLandReceived.Text = string.Empty;

                SetUpdateMode(false);
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        ///To fetch details from database based on ID
        /// </summary
        private void GetLandReceivedByID()
        {
            LandReceivedMSTBLL LandReceivedMSTOBJ = new LandReceivedMSTBLL();

            int LandReceivedID = 0;

            if (ViewState["LandReceivedID"] != null)
                LandReceivedID = Convert.ToInt32(ViewState["LandReceivedID"]);

            LandReceivedMSTBO LandReceivedObj = new LandReceivedMSTBO();
            LandReceivedObj = LandReceivedMSTOBJ.GetLandReceivedByID(LandReceivedID);

            txtLandReceived.Text = LandReceivedObj.LandReceived;
            txtLandReceivedID.Text = LandReceivedObj.LandReceivedID.ToString();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        public void Clear()
        {
            txtLandReceived.Text = "";
            txtLandReceivedID.Text = "";
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
                ViewState["PositionID"] = "0";
            }
        }
    }
}