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
    public partial class TenureLand : System.Web.UI.Page
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
                ViewState["LND_TENUREID"] = 0;
                BindGrid();
                Master.PageHeader = "Land Tenure";
                btnShowAdd.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
               // txtLandtenure.Attributes.Add("onchange", "isDirty = 1;");
                txtLandtenure.Attributes.Add("onchange", "setDirtyText();");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_TENURE) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btnShowAdd.Visible = false;
                    btnSearch.Visible = false;
                    btnClear.Visible = false;
                    btnShowSearch.Visible = false;
                    GrdLandTenure.Columns[2].Visible = false;
                    GrdLandTenure.Columns[3].Visible = false;
                    GrdLandTenure.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in GrdLandTenure.Rows)
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {

            TenureLandBLL objTenureLandBLL = new TenureLandBLL();
            GrdLandTenure.DataSource = objTenureLandBLL.GetTenureLand("");
            GrdLandTenure.DataBind();
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtLandtenure.Text = "";
            ViewState["LND_TENUREID"] = 0;
            txtSearch.Text = "";
            btn_Save.Text = "Save";
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
            BindGrid();
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Show Add Banks Panel and hide search panel
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
        /// Search tenureland data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            ShowHideSections(false, true);
        }

        /// <summary>
        /// Show and Hide the Search or Add banks panels baced on Selection
        /// </summary>
        /// <param name="showAdd"></param>
        /// <param name="showSearch"></param>
        private void ShowHideSections(bool showAdd, bool showSearch)
        {
            pnlLandTenureDetails.Visible = false;
            pnlSearch.Visible = false;
            if (showAdd) pnlLandTenureDetails.Visible = true;
            if (showSearch) pnlSearch.Visible = true;
        }

        /// <summary>
        /// Search tenureland data from the database and set filterd data to Grid Data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            TenureLandBO objTenureLand = new TenureLandBO();
            string TenureLandName;
            TenureLandName = txtSearch.Text.Trim();
            TenureLandBLL objTenureLandBLL = new TenureLandBLL();
            GrdLandTenure.DataSource = objTenureLandBLL.GetTenureLand(TenureLandName);
            GrdLandTenure.DataBind();
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";
            TenureLandBO objTenureLand = new TenureLandBO();
            objTenureLand.Lnd_TenureId = Convert.ToInt32(ViewState["LND_TENUREID"]);
            objTenureLand.Lnd_Tenure = txtLandtenure.Text.Trim();

            TenureLandBLL objTenureLandBLL = new TenureLandBLL();
            if (objTenureLand.Lnd_TenureId == 0)
            {
                objTenureLand.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objTenureLandBLL.AddTenureLand(objTenureLand);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";
            }
            else
            {
                objTenureLand.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objTenureLandBLL.UpdateTenureLand(objTenureLand);
                SetUpdateMode(false);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            ClearDetails();
            BindGrid();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdLandTenure_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ShowHideSections(true, false);
                ViewState["LND_TENUREID"] = e.CommandArgument;
                TenureLandBO objTenureLand = null;
                TenureLandBLL objTenureLandBLL = new TenureLandBLL();
                objTenureLand = objTenureLandBLL.GetTenureLandByTenureLand(Convert.ToInt32(ViewState["LND_TENUREID"]));
                txtLandtenure.Text = objTenureLand.Lnd_Tenure;
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                TenureLandBLL objTenureLandBLL = new TenureLandBLL();
                message = objTenureLandBLL.DeleteTenureLand(Convert.ToInt32(e.CommandArgument));
                BindGrid();
                SetUpdateMode(false);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
        }

        /// <summary>
        /// Change index of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdLandTenure_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdLandTenure.PageIndex = e.NewPageIndex;
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

                string LND_TENUREID = ((Literal)gr.FindControl("litTenureLandID")).Text;
                TenureLandBLL objTenureLandBLL = new TenureLandBLL();
                message = objTenureLandBLL.ObsoleteTenureLand(Convert.ToInt32(LND_TENUREID), Convert.ToString(chk.Checked));
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
                ViewState["LND_TENUREID"] = "0";
            }
        }
    }
}

