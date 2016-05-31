using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CropName : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                string userName = (Session["USERNAME"].ToString());
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Crop Name";
                ViewState["CROPID"] = 0;
                Getunitofmeasure();
                BindGrid(false, false);
                //txtCropName.Attributes.Add("onchange", "isDirty = 1;");
                //ddlUnitOfmeasure.Attributes.Add("onchange", "isDirty = 1;");
                txtCropName.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gvCropName.Columns[gvCropName.Columns.Count - 1].Visible = false;
                    gvCropName.Columns[gvCropName.Columns.Count - 2].Visible = false;
                    gvCropName.Columns[gvCropName.Columns.Count - 3].Visible = false; 
                    foreach (GridViewRow grRow in gvCropName.Rows)
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
        /// To bind details to dropdownlist
        /// </summary>
        private void Getunitofmeasure()
        {
            UnitBLL BLLobj = new UnitBLL();

            ddlUnitOfmeasure.DataSource = BLLobj.GetUnit();
            ddlUnitOfmeasure.DataTextField = "UnitName";
            ddlUnitOfmeasure.DataValueField = "UnitID";
            ddlUnitOfmeasure.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRaow)
        {
            CropNameBLL CropNameBLLObj = new CropNameBLL();
            gvCropName.DataSource = CropNameBLLObj.GetAllCropNameDetails();
            gvCropName.DataBind();
        }
        /// <summary>
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            CropNameBO CropNameBOObj = new CropNameBO();
            CropNameBOObj.CropName = txtCropName.Text.ToString().Trim();
            CropNameBOObj.UnitMeasure = Convert.ToInt32(ddlUnitOfmeasure.SelectedValue);
            CropNameBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
            SaveCropNameDetails(CropNameBOObj);
            ClearCropName();
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCropNameDetails(CropNameBO CropNameBOObj)
        {
            string message = "";

            if (txtCropNameID.Text.ToString().Trim() == string.Empty)
            {
                try
                {
                    CropNameBOObj = new CropNameBO();
                    CropNameBLL CropNameBLLObj = new CropNameBLL();

                    CropNameBOObj.CropName = txtCropName.Text.ToString().Trim();
                    CropNameBOObj.CROPID = Convert.ToInt32(ViewState["CROPID"]);
                    CropNameBOObj.UnitMeasure = Convert.ToInt32(ddlUnitOfmeasure.SelectedValue);
                    CropNameBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = CropNameBLLObj.InsertCropNameDetails(CropNameBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                    BindGrid(true, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    CropNameBOObj = new CropNameBO();
                    CropNameBLL CropNameBLLObj = new CropNameBLL();

                    CropNameBOObj.CropName = txtCropName.Text.ToString().Trim();
                    int CROPID = Convert.ToInt32(ViewState["CROPID"]);
                    CropNameBOObj.UnitMeasure = Convert.ToInt32(ddlUnitOfmeasure.SelectedItem.Value);
                    CropNameBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = CropNameBLLObj.UpdateCropNameDetails(CropNameBOObj, CROPID);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ClearCropName();
                    BindGrid(true, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CropNameBOObj = null;
                }
            }

           
            SetUpdateMode(false);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearCropName();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearCropName()
        {
            txtCropName.Text = string.Empty;
            txtCropNameID.Text = string.Empty;
            ddlUnitOfmeasure.ClearSelection();
        }
        /// <summary>
        /// To set pageno in grid
        /// </summary>
        protected void gvCropName_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCropName.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCropName_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CROPID"] = e.CommandArgument;
                int CropEditID = Convert.ToInt32(ViewState["CROPID"]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //if (row != null)
                //{
                //    txtCropName.Text = row.Cells[1].Text.ToString();
                //    ddlUnitOfmeasure.SelectedItem.Text = row.Cells[2].Text.ToString();
                //}
                SetUpdateMode(true);

                GetCropNameDeatails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                CropNameBLL CropNameBLLObj = new CropNameBLL();
                string message = string.Empty;

                message = CropNameBLLObj.DeleteCropTypeRow(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                ClearCropName();
                SetUpdateMode(false);
                BindGrid(false, true);
                
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

                string cropID = ((Literal)gr.FindControl("litCROPID")).Text;
                CropNameBLL CropNameBLLObj = new CropNameBLL();
                message = CropNameBLLObj.ObsoleteCropName(Convert.ToInt32(cropID), Convert.ToString(chk.Checked));
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
        /// To fetch details and assign to textbox
        /// </summary>
        private void GetCropNameDeatails()
        {
            int CROPID = 0;

            if (ViewState["CROPID"] != null)
                CROPID = Convert.ToInt32(ViewState["CROPID"].ToString());

            CropNameBLL CropNameBLLObj = new CropNameBLL();

            CropNameBO CropNameBOObj = CropNameBLLObj.GetCropNameById(CROPID);

            txtCropName.Text = CropNameBOObj.CropName;
            txtCropNameID.Text = CropNameBOObj.CROPID.ToString();
            ddlUnitOfmeasure.ClearSelection();
            if (ddlUnitOfmeasure.Items.FindByText(CropNameBOObj.UnitName.ToString()) != null)
                ddlUnitOfmeasure.Items.FindByText(CropNameBOObj.UnitName.ToString()).Selected = true;

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
                ViewState["CROPID"] = "0";
            }
        }
        /// <summary>
        /// to open new page on click on link in grid
        /// </summary>
        protected void gvCropName_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkCropRate = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkCropRate");
                int cropID = (int)DataBinder.Eval(e.Row.DataItem, "CROPID");

                lnkCropRate.Attributes.Add("onclick", "OpenCropRate(" + cropID + ");");
            }
        }

        protected void gvCropName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}