using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS
{
    public partial class FixedCostCentre : System.Web.UI.Page
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
                Master.PageHeader = "Fixed Cost Centre";
                ViewState["FCCID"] = 0;
                BindGrid(false, false);
                txtFixedCostCentre.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FixedCostCentre) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    grdFixedCostCentre.Columns[grdFixedCostCentre.Columns.Count - 1].Visible = false;
                    grdFixedCostCentre.Columns[grdFixedCostCentre.Columns.Count - 2].Visible = false;
                    grdFixedCostCentre.Columns[grdFixedCostCentre.Columns.Count - 3].Visible = false;
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
            stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            FixedCostCentreBLL objFixedCostCentreBLL = new FixedCostCentreBLL();
            grdFixedCostCentre.DataSource = objFixedCostCentreBLL.GetAllFixedCostCentres("");
            grdFixedCostCentre.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            FixedCostCentreBO objFixedCostCentre = new FixedCostCentreBO();
            FixedCostCentreBLL objroleBLL = new FixedCostCentreBLL();

            objFixedCostCentre.FixedCostCentreID = Convert.ToInt32(ViewState["FCCID"]);
            objFixedCostCentre.FixedCostCentre = txtFixedCostCentre.Text.Trim();
            objFixedCostCentre.FixedCostCentreDescription = txtDescription.Text.Trim();

            if (objFixedCostCentre.FixedCostCentreDescription.Length >= 500)
                objFixedCostCentre.FixedCostCentreDescription = objFixedCostCentre.FixedCostCentreDescription.Substring(0, 499);

            string AlertMessage = string.Empty;
            string message = string.Empty;
            AlertMessage = "alert('" + message + "');";

            if (objFixedCostCentre.FixedCostCentreID == 0)
            {
                objFixedCostCentre.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objroleBLL.AddFixedCostCentre(objFixedCostCentre);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                }
            }
            else
            {
                objFixedCostCentre.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objroleBLL.UpdateFixedCostCentre(objFixedCostCentre);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    SetUpdateMode(false);
                }
            }

            BindGrid(true, false);

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtFixedCostCentre.Text = "";
            txtDescription.Text = "";
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// to link to other page on click of link  in grid
        /// </summary>
        /// <returns></returns>
        protected void grdFixedCostCentre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    ViewState["FCCID"] = e.CommandArgument;
                    FixedCostCentreBLL objroleBLL = new FixedCostCentreBLL();
                    FixedCostCentreBO objFixedCostCentre = objroleBLL.GetFixedCostCentreByFixedCostCentreID(Convert.ToInt32(ViewState["FCCID"]));
                    txtFixedCostCentre.Text = objFixedCostCentre.FixedCostCentre;
                    txtDescription.Text = objFixedCostCentre.FixedCostCentreDescription;
                    SetUpdateMode(true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
                }

                else if (e.CommandName == "DeleteRow")
                {
                    FixedCostCentreBLL objroleBLL = new FixedCostCentreBLL();
                    message = objroleBLL.DeleteFixedCostCentre(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data Deleted successfully";
                    SetUpdateMode(false);
                    BindGrid(false, true);
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

                string roleID = ((Literal)gr.FindControl("litFixedCostCentreID")).Text;
                FixedCostCentreBLL objroleBLL = new FixedCostCentreBLL();
                message = objroleBLL.ObsoleteFixedCostCentre(Convert.ToInt32(roleID), Convert.ToString(chk.Checked));
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
        /// to display pageno in grid
        /// </summary>
        /// <returns></returns>
        protected void grdFixedCostCentre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFixedCostCentre.PageIndex = e.NewPageIndex;
            BindGrid(false, true);
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
                ViewState["FCCID"] = "0";
            }
        }
    }
}
