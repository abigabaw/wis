using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CropDiameter : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Crop Diameter";
                ViewState["CropDiaMeterID"] = 0;
                BindGrid();
                CropDiameterIDTextBox.Text = "0";
                CropDiameterTextBox.Attributes.Add("onchange", "isDirty = 1;");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCropDiameter.Columns[2].Visible = false;
                    grdCropDiameter.Columns[3].Visible = false;
                    grdCropDiameter.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdCropDiameter.Rows)
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
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            int count = 0;

            if (CropDiameterIDTextBox.Text.ToString().Trim() == "0" || CropDiameterIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                CropDiameterBLL CropDiameterBLLOBJ = new CropDiameterBLL();
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropDiameterBO objCropDiameter = new CropDiameterBO();
                    objCropDiameter.CROPDIAMETER = CropDiameterTextBox.Text.ToString().Trim();
                    objCropDiameter.UserID = Convert.ToInt32(uID);

                    CropDiameterBLL CropDiameterBLLobj = new CropDiameterBLL();
                    message = CropDiameterBLLobj.InsertCropDiameter(objCropDiameter);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                    CropDiameterIDTextBox.Text = "0";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    CropDiameterBLLOBJ = null;
                }

                BindGrid();
            }
            //edit the data in the textbox exiting in the Grid
            else if (CropDiameterIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropDiameterBO objCropDiameter = new CropDiameterBO();
                    objCropDiameter.CROPDIAMETER = CropDiameterTextBox.Text.ToString().Trim();
                    objCropDiameter.CROPDIAMETERID = Convert.ToInt32(CropDiameterIDTextBox.Text.ToString().Trim());
                    objCropDiameter.UserID = Convert.ToInt32(uID);

                    CropDiameterBLL CropDiameterBLLobj = new CropDiameterBLL();
                    message =  CropDiameterBLLobj.EDITCropDiameter(objCropDiameter);


                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        BindGrid();
                        SetUpdateMode(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    concernBLLOBJ = null;
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            ClearData();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearData()
        {
            CropDiameterIDTextBox.Text = string.Empty;
            CropDiameterTextBox.Text = string.Empty;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            CropDiameterBLL CropDiameterBLLobj = new CropDiameterBLL();
            grdCropDiameter.DataSource = CropDiameterBLLobj.GetCropDiameter();
            grdCropDiameter.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCropDiameter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CropDiaMeterID"] = e.CommandArgument;
                GetCropDiameterDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                CropDiameterBLL CropDiameterBLLobj = new CropDiameterBLL();
                string message = string.Empty;
                try
                {
                    message = CropDiameterBLLobj.DeleteCropDiameter(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ClearData();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                SetUpdateMode(false);
                BindGrid();
            }
        }
        /// <summary>
        /// To fetch details and assign to textbox
        /// </summary>
        private void GetCropDiameterDetails()
        {
            CropDiameterBLL CropDiameterBLLobj = new CropDiameterBLL();
            int CROPDIAMETERID = 0;

            if (ViewState["CropDiaMeterID"] != null)
                CROPDIAMETERID = Convert.ToInt32(ViewState["CropDiaMeterID"]);

            CropDiameterBO CropDiameterObj = new CropDiameterBO();
            CropDiameterObj = CropDiameterBLLobj.GetCropDiameterById(CROPDIAMETERID);

            CropDiameterTextBox.Text = CropDiameterObj.CROPDIAMETER;
            CropDiameterIDTextBox.Text = CropDiameterObj.CROPDIAMETERID.ToString();
        }
        /// <summary>
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCropDiameter.PageIndex = e.NewPageIndex;
            BindGrid();
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
                ViewState["CropDiaMeterID"] = "0";
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
                string CropDiaMeterID = ((Literal)gr.FindControl("litUserID")).Text;
                CropDiameterBLL objCropDiameterBLL = new CropDiameterBLL();
                message = objCropDiameterBLL.ObsoleteCropDiameter(Convert.ToInt32(CropDiaMeterID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}