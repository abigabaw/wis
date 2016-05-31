using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CropRate : System.Web.UI.Page
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
                ViewState["CROPID"] = Request.QueryString["id"];
                ViewState["CROPRATEID"] = 0;
                BindGrid();
                FillDistricts();
                GetCropName();
                GetCropDescription();
                txtCropRate.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCropRate.Columns[grdCropRate.Columns.Count - 1].Visible = false;
                    grdCropRate.Columns[grdCropRate.Columns.Count - 2].Visible = false;
                    grdCropRate.Columns[grdCropRate.Columns.Count - 3].Visible = false;
                }
            }
        }
        /// <summary>
        /// to get crop names
        /// </summary>
        /// <returns></returns>
        private void GetCropName()
        {
            CropNameBO objCrop = (new CropNameBLL()).GetCropNameById(Convert.ToInt32(ViewState["CROPID"]));

            if (objCrop != null)
                Master.PageHeader = "Rates for " + objCrop.CropName;
        }
        /// <summary>
        /// to get crop description
        /// </summary>
        /// <returns></returns>
        private void GetCropDescription()
        {
            CropDescriptionBLL BLLobj = new CropDescriptionBLL();

            ddlCropDescription.DataSource = BLLobj.GetCropDescription();
            ddlCropDescription.DataTextField = "CropDesName";
            ddlCropDescription.DataValueField = "CropDesID";
            ddlCropDescription.DataBind();
        }
        /// <summary>
        /// To bind details to dropdownlist
        /// </summary>
        private void FillDistricts()
        {
            MasterBLL objMasterBLL = new MasterBLL();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataSource = objMasterBLL.LoadDistrictData();
            ddlDistrict.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            CropRateBO objCRBO = new CropRateBO();
            CropRateBLL objCRBLL = new CropRateBLL();
            grdCropRate.DataSource = objCRBLL.GetCropRate(Convert.ToInt32(ViewState["CROPID"]));
            grdCropRate.DataBind();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtCropRate.Text = string.Empty;
            ddlDistrict.ClearSelection();
            ddlCropDescription.ClearSelection();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCropRate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CROPRATEID"] = e.CommandArgument;
                CropRateBO objCRBO = new CropRateBO();
                CropRateBLL objCRBLL = new CropRateBLL();
                objCRBO = objCRBLL.GetCropRateByID(Convert.ToInt32(ViewState["CROPRATEID"]));
                if (objCRBO != null)
                {
                    txtCropRate.Text = objCRBO.CropRate;

                    ddlDistrict.ClearSelection();
                    if (ddlDistrict.Items.FindByValue(objCRBO.DistrictID.ToString()) != null)
                        ddlDistrict.Items.FindByValue(objCRBO.DistrictID.ToString()).Selected = true;

                    ddlCropDescription.ClearSelection();
                    if (ddlCropDescription.Items.FindByValue(objCRBO.CropDescriptionID.ToString()) != null)
                        ddlCropDescription.Items.FindByValue(objCRBO.CropDescriptionID.ToString()).Selected = true;
                }
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;

                CropRateBLL objCRBLL = new CropRateBLL();
                message = objCRBLL.DeleteCropRate(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                ClearDetails();
                BindGrid();
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
                ViewState["CROPRATEID"] = "0";
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
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            string message = "";

            CropRateBO objCRBO = new CropRateBO();

            objCRBO.CropRateID = int.Parse(ViewState["CROPRATEID"].ToString());
            objCRBO.CropID = int.Parse(ViewState["CROPID"].ToString());
            objCRBO.DistrictID = Convert.ToInt32(ddlDistrict.SelectedItem.Value);
            objCRBO.CropDescriptionID = Convert.ToInt32(ddlCropDescription.SelectedItem.Value);
            objCRBO.CropRate = txtCropRate.Text.Trim();

            CropRateBLL objCRBLL = new CropRateBLL();
            if (objCRBO.CropRateID == 0)
            {
                objCRBO.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = objCRBLL.AddCropRate(objCRBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully.";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            else
            {
                objCRBO.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = objCRBLL.UpdateCropRate(objCRBO);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully.";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
                SetUpdateMode(false);
            }

            ClearDetails();
            BindGrid();
        }
        /// <summary>
        /// to go to new page 
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCropRate.PageIndex = e.NewPageIndex;
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
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;

                string cropRateID = ((Literal)gr.FindControl("ltlObsolete")).Text;
                CropRateBLL objCropRateBLL = new CropRateBLL();
                message = objCropRateBLL.ObsoleteCropRate(Convert.ToInt32(cropRateID), Convert.ToString(chk.Checked));
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