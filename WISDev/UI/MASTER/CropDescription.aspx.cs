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
    public partial class CropDescription : System.Web.UI.Page
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
                //Retrieving UserName from Session
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Crop Description";
                ViewState["CROPDESID"] = 0;  // ViewState ID
                BindGrid(); //Calling the Grid Data
                CropDescriptionIDTextBox.Text = "0";
                //CropDescriptionTextBox.Attributes.Add("onchange", "isDirty = 1;");
                CropDescriptionTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCropDes.Columns[2].Visible = false;
                    grdCropDes.Columns[3].Visible = false;
                    grdCropDes.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdCropDes.Rows)
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

            if (CropDescriptionIDTextBox.Text.ToString().Trim() == "0")
            {
                CropDescriptionBLL CropDescriptionBLLOBJ = new CropDescriptionBLL();
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropDescriptionBO objCropDescription = new CropDescriptionBO();
                    objCropDescription.CROPDESNAME = CropDescriptionTextBox.Text.ToString().Trim();
                    objCropDescription.UserID = Convert.ToInt32(uID);

                    CropDescriptionBLL CropDescriptionBLLobj = new CropDescriptionBLL();
                    message = CropDescriptionBLLobj.InsertCropDescription(objCropDescription);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                    CropDescriptionIDTextBox.Text = "0";

                    BindGrid();

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    CropDescriptionBLLOBJ = null;
                }
            }
            //edit the data in the textbox exiting in the Grid
            else if (CropDescriptionIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropDescriptionBO objCropDesc = new CropDescriptionBO();
                    objCropDesc.CROPDESNAME = CropDescriptionTextBox.Text.ToString().Trim();
                    objCropDesc.CROPDESID = Convert.ToInt32(CropDescriptionIDTextBox.Text.ToString().Trim());
                    objCropDesc.UserID = Convert.ToInt32(uID);

                    CropDescriptionBLL CropDescriptionBLLobj = new CropDescriptionBLL();
                    message = CropDescriptionBLLobj.EDITCropDescr(objCropDesc);

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
            CropDescriptionIDTextBox.Text = string.Empty;
            CropDescriptionTextBox.Text = string.Empty;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            CropDescriptionBLL CropDescriptionBLLobj = new CropDescriptionBLL();
            grdCropDes.DataSource = CropDescriptionBLLobj.GetAllCropDescription();
            grdCropDes.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCropDes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CROPDESID"] = e.CommandArgument;
                GetOCCUPATIONDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = "";
                try
                {
                    CropDescriptionBLL CropDescriptionBLLobj = new CropDescriptionBLL();

                    message = CropDescriptionBLLobj.DeleteCropDESC(Convert.ToInt32(e.CommandArgument));

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data Deleted successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ClearData();
                    SetUpdateMode(false);
                    BindGrid();
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
        private void GetOCCUPATIONDetails()
        {
            CropDescriptionBLL CropDescriptionBLLobj = new CropDescriptionBLL();
            int CROPDESCRIPTIONID = 0;

            if (ViewState["CROPDESID"] != null)
                CROPDESCRIPTIONID = Convert.ToInt32(ViewState["CROPDESID"]);

            CropDescriptionBO CropDescriptionObj = new CropDescriptionBO();
            CropDescriptionObj = CropDescriptionBLLobj.GetCropDescriptionId(CROPDESCRIPTIONID);

            CropDescriptionTextBox.Text = CropDescriptionObj.CROPDESNAME;
            CropDescriptionIDTextBox.Text = CropDescriptionObj.CROPDESID.ToString();
        }
        /// <summary>
        /// To change page in the grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCropDes.PageIndex = e.NewPageIndex;
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
                ViewState["CROPDESID"] = "0";
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

                string cropDescriptionID = ((Literal)gr.FindControl("litCropDescriptionID")).Text;
                CropDescriptionBLL objCropDescriptionBLL = new CropDescriptionBLL();
                message = objCropDescriptionBLL.ObsoleteCropDESC(Convert.ToInt32(cropDescriptionID), Convert.ToString(chk.Checked));
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