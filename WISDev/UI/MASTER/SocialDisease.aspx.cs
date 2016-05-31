using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;


namespace WIS
{
    public partial class SocialDisease : System.Web.UI.Page
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
                Master.PageHeader = "Disease";

                ViewState["DiSEASEID"] = 0;
                BindGrid();
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //txtDisease.Attributes.Add("onchange", "isDirty = 1;");
                txtDisease.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btn_Save.Visible = false;
                    btnClear.Visible = false;
                    btnSearch.Visible = false;
                    btnShowAdd.Visible = false;
                    btnShowSearch.Visible = false;
                    btn_Clear.Visible = false;

                    GrdSocialdisease.Columns[2].Visible = false;
                    GrdSocialdisease.Columns[3].Visible = false;
                    GrdSocialdisease.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in GrdSocialdisease.Rows)
                    {
                        if (grRow.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chk = (CheckBox)grRow.FindControl("IsObsolete");
                            chk.Enabled = false;
                        }
                    }

                }
            }

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnSearch.ClientID);
            else
                stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Show Add socialdisease Panel and hide search panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            ShowHideSections(true, false);
            BindGrid();
            btn_Save.Text = "Save";
            ClearDetails();
        }
        /// <summary>
        /// Show search socialdisease Panel and hide Add panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }
        /// <summary>
        /// to hide and show panels based on condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlDiseaseDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlDiseaseDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtDisease.Text = "";
            ViewState["DiSEASEID"] = 0;
            txtSearch.Text = "";
            btn_Save.Text = "Save";
            BindGrid();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            DiseaseBLL objDiseaseBLL = new DiseaseBLL();
            GrdSocialdisease.DataSource = objDiseaseBLL.SearchDisease("");
            GrdSocialdisease.DataBind();
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
        /// Calls clear method
        /// </summary>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdSocialdisease_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["DiSEASEID"] = e.CommandArgument;
                DiseaseBLL objDiseaseBLL = new DiseaseBLL();
                DiseaseBO objDisease = objDiseaseBLL.GetDiseaseByDiseaseID(Convert.ToInt32(ViewState["DiSEASEID"]));
                txtDisease.Text = objDisease.DiseaseName;
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);       
                }
            else if (e.CommandName == "DeleteRow")
            {
                DiseaseBLL objDiseaseBLL = new DiseaseBLL();
                message = objDiseaseBLL.DeleteDisease(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                ClearDetails();
                SetUpdateMode(false);
                BindGrid();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
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
                ViewState["DiSEASEID"] = ((Literal)gr.FindControl("litDiseaseId")).Text;
                DiseaseBLL objdiseaseBLL = new DiseaseBLL();
                message = objdiseaseBLL.ObsoleteDisease(Convert.ToInt32(ViewState["DiSEASEID"]), Convert.ToString(chk.Checked));
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
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            DiseaseBO objDisease = new DiseaseBO();
            objDisease.DiseaseID = Convert.ToInt32(ViewState["DiSEASEID"]);
            objDisease.DiseaseName = txtDisease.Text.Trim();

            DiseaseBLL objDiseaseBLL = new DiseaseBLL();
            if (objDisease.DiseaseID == 0)
            {
                objDisease.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objDiseaseBLL.AddDisease(objDisease);


                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully.";
                    ClearDetails();
                    BindGrid();
                }
            }
            else
            {
                objDisease.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objDiseaseBLL.UpdateDisease(objDisease);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully.";
                    ClearDetails();
                    BindGrid();
                    SetUpdateMode(false);
                }
            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To search deatils from database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string DiseaseName;
            DiseaseName = txtSearch.Text.Trim();
            DiseaseBLL objDiseaseBLL = new DiseaseBLL();
            GrdSocialdisease.DataSource = objDiseaseBLL.SearchDisease(DiseaseName);
            GrdSocialdisease.DataBind();
        }
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdSocialdisease_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdSocialdisease.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["DiSEASEID"] = "0";
            }
        }
    }
}

