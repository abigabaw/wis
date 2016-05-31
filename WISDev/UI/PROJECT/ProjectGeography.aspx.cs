using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ProjectGeography : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.GeographicalInfo;

            if (!IsPostBack)
            {
                txtKeyGeoFeatures.Attributes.Add("maxlength", txtKeyGeoFeatures.MaxLength.ToString());

                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Geographical Information";
                ViewState["Geography_ID"] = 0;
                BindGrid();

                GetTooltip();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdProjectGeo.Columns[grdProjectGeo.Columns.Count - 1].Visible = false;
                    grdProjectGeo.Columns[grdProjectGeo.Columns.Count - 2].Visible = false;
                }
                
            }
        }
        /// <summary>
        /// To fetch and assign direction message to label
        /// </summary>
        private void GetTooltip()
        {          
            WIS_ConfigBO WIS_ConfigBOobj = new WIS_ConfigBO();

            WIS_ConfigBLL WIS_ConfigBLLobj = new WIS_ConfigBLL();
           WIS_ConfigBOobj= WIS_ConfigBLLobj.getConfiguration("GENERAL_DIRECTION_DETAILS");

           if (WIS_ConfigBOobj != null)
            lblDirectionMessage.Text = WIS_ConfigBOobj.ConfigData;
        }
        /// <summary>
        /// To fetch and assign values to textbox
        /// </summary>
        protected void GetGeographyDetails()
        {
            GeographyBO oGeo = (new ProjectBLL()).GetProjectGeographyByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));

            if (oGeo != null)
            {
                txtGeneralDirection.Text = oGeo.GeneralDirection;
                txtKeyGeoFeatures.Text = oGeo.KeyFeatures;
            }
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                GeographyBO oGeo = new GeographyBO();
                oGeo.GeographicalID = Convert.ToInt32(ViewState["Geography_ID"]);
                oGeo.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                oGeo.GeneralDirection = txtGeneralDirection.Text.Trim();
                string sKeyFeatures = "";
                if (txtKeyGeoFeatures.Text.Trim().Length > 1000)
                    sKeyFeatures = txtKeyGeoFeatures.Text.Trim().Substring(0, 1000);
                else
                    sKeyFeatures = txtKeyGeoFeatures.Text.Trim();
                oGeo.KeyFeatures = sKeyFeatures;
                oGeo.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                ProjectBLL objProjectBLL = new ProjectBLL();
                objProjectBLL.AddProjectGeography(oGeo);
                if (btnSave.Text == "Save")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data updated successfully');", true);
                ClearDetails();
                BindGrid();
            }
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtGeneralDirection.Text = "";
            txtKeyGeoFeatures.Text = "";
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            ProjectBLL ProjectBLLObj = new ProjectBLL();
            grdProjectGeo.DataSource = ProjectBLLObj.GetAllProjectGeoDetails(Convert.ToInt32(Session["PROJECT_ID"]));
            grdProjectGeo.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProjectGeo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ViewState["Geography_ID"] = e.CommandArgument;
                    GeographyBO oGeo = (new ProjectBLL()).GetProjectGeographyByProjectID(Convert.ToInt32(ViewState["Geography_ID"]));

                    if (oGeo != null)
                    {
                        txtGeneralDirection.Text = oGeo.GeneralDirection;
                        txtKeyGeoFeatures.Text = oGeo.KeyFeatures;
                    }
                    btnSave.Text = "Update";
                    btnClear.Text = "Cancel";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
                }

                else if (e.CommandName == "DeleteRow")
                {
                    ProjectBLL ProjectBLLobj = new ProjectBLL();
                    message = ProjectBLLobj.DeleteProjGeo(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data Deleted successfully";
                  
                    BindGrid();
                    ClearDetails();
                }
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearDetails()
        {
            ViewState["Geography_ID"] = 0;
            txtGeneralDirection.Text = "";
            txtKeyGeoFeatures.Text = "";
            btnClear.Text = "Clear";
            btnSave.Text = "Save";
        }
        /// <summary>
        /// To show pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdProjectGeo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProjectGeo.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}