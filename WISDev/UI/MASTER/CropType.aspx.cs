using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CropType : System.Web.UI.Page
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
                Master.PageHeader = "Crop Type";
                ViewState["CROPTYPEID"] = 0;
                BindGrid();
                txtCropType.Attributes.Add("onchange", "isDirty = 1;");
                //Getunitofmeasure();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gvCropType.Columns[2].Visible = false;
                    gvCropType.Columns[3].Visible = false;
                    gvCropType.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in gvCropType.Rows)
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

        //private void Getunitofmeasure()
        //{
        //    UnitBLL BLLobj = new UnitBLL();

        //    ddlUnitOfmeasure.DataSource = BLLobj.GetUnit();
        //    ddlUnitOfmeasure.DataTextField = "UnitName";
        //    ddlUnitOfmeasure.DataValueField = "UnitID";
        //    ddlUnitOfmeasure.DataBind();
        //}
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            CropTypeBLL CropTypeBLLObj = new CropTypeBLL();
            gvCropType.DataSource = CropTypeBLLObj.GetAllCropDetails();
            gvCropType.DataBind();
        }
        /// <summary>
        /// calls save method
        /// </summary>
        /// <returns></returns>
        protected void btnSave_Click(object sender, EventArgs e)
        {            
            CropTypeBO CropTypeBOObj = new CropTypeBO();
            CropTypeBOObj.CropType = txtCropType.Text.ToString().Trim();
            //CropTypeBOObj.UnitMeasure =Convert.ToInt32( ddlUnitOfmeasure.SelectedValue);
            CropTypeBOObj.Createdby = Convert.ToInt32(Session["USER_ID"].ToString());
            SaveCropTypeDetails(CropTypeBOObj);
            ClearCropType();
        }
        /// <summary>
        /// To save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveCropTypeDetails(CropTypeBO CropTypeBOObj)
        {
            string message = "";
           
            if (txtCropTypeID.Text.ToString().Trim() == string.Empty)
            {
                try
                {
                    CropTypeBOObj = new CropTypeBO();
                    CropTypeBLL CropTypeBLLObj = new CropTypeBLL();

                    CropTypeBOObj.CropType = txtCropType.Text.ToString().Trim();
                    CropTypeBOObj.CROPTYPEID = Convert.ToInt32(ViewState["CROPTYPEID"]);
                    //CropTypeBOObj.UnitMeasure= Convert.ToInt32(ddlUnitOfmeasure.SelectedValue);
                    CropTypeBOObj.Createdby = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = CropTypeBLLObj.InsertCropTypeDetails(CropTypeBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";                    

                    BindGrid();
                    ClearCropType();
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
                    CropTypeBOObj = new CropTypeBO();
                    CropTypeBLL CropTypeBLLObj = new CropTypeBLL();

                    CropTypeBOObj.CropType = txtCropType.Text.ToString().Trim();
                    //CropTypeBOObj.UnitMeasure = Convert.ToInt32(ddlUnitOfmeasure.SelectedItem.Value);
                    int CROPTYPEID = Convert.ToInt32(ViewState["CROPTYPEID"]);
                    CropTypeBOObj.Createdby = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = CropTypeBLLObj.EditCropTypeDetails(CropTypeBOObj, CROPTYPEID);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    BindGrid();
                    ClearCropType();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    CropTypeBOObj = null;
                }

                SetUpdateMode(false);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearCropType();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearCropType()
        {
            txtCropType.Text = string.Empty;
            txtCropTypeID.Text = string.Empty;
            //ddlUnitOfmeasure.ClearSelection();
        }
        /// <summary>
        /// To set pageno in grid
        /// </summary>
        protected void gvCropType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCropType.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCropType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="EditRow")
            {
               ViewState["CROPTYPEID"]= e.CommandArgument;
               int CropEditID = Convert.ToInt32(ViewState["CROPTYPEID"]);
               GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
               if (row != null)
               {
                   txtCropType.Text = row.Cells[1].Text.ToString();
                   //ddlUnitOfmeasure.SelectedItem.Text = row.Cells[2].Text.ToString();
               }

               GetCropTypeDeatails();
               SetUpdateMode(true);
               ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if(e.CommandName=="DeleteRow")
            {
                CropTypeBLL CropTypeBLLObj = new CropTypeBLL();
                string message = string.Empty;

                try
                {
                    message = CropTypeBLLObj.DeleteCropTypeRow(Convert.ToInt32(e.CommandArgument));

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ClearCropType();
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
        private void GetCropTypeDeatails()
        {
            CropTypeBO CropTypeBOObj = new CropTypeBO();

            int CROPTYPEID = 0;

            if (ViewState["CROPTYPEID"] != null)
                CROPTYPEID = Convert.ToInt32(ViewState["CROPTYPEID"].ToString());

            CropTypeBLL CropTypeBLLOBj = new CropTypeBLL();         

            CropTypeBOObj = CropTypeBLLOBj.GetCropTypeById(CROPTYPEID);

            txtCropType.Text = CropTypeBOObj.CropType;

            //ddlUnitOfmeasure.ClearSelection();
            //if (ddlUnitOfmeasure.Items.FindByValue(CropTypeBOObj.UnitMeasure.ToString()) != null)
            //    ddlUnitOfmeasure.Items.FindByValue(CropTypeBOObj.UnitMeasure.ToString()).Selected = true;

            txtCropTypeID.Text = CropTypeBOObj.CROPTYPEID.ToString();
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
                ViewState["CROPTYPEID"] = "0";
                
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
                string CROPTYPEID = ((Literal)gr.FindControl("ltlObsolete")).Text;
                CropTypeBLL objCropTypeBLL = new CropTypeBLL();
                message = objCropTypeBLL.ObsoleteCropType(Convert.ToInt32(CROPTYPEID), Convert.ToString(chk.Checked));
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