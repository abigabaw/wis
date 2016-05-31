using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class WelfareIndicator : System.Web.UI.Page
    {

        #region PageEvent

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
                Master.PageHeader = "Welfare Indicator";
                ViewState["WelfareIndicatorID"] = 0;
                BindGrid();
                //txtWelfareIndicator.Attributes.Add("onchange", "isDirty = 1;");
                txtWelfareIndicator.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_GROUP) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdWelfareIndicator.Columns[3].Visible = false;
                    grdWelfareIndicator.Columns[4].Visible = false;
                    grdWelfareIndicator.Columns[5].Visible = false;
                }
            }
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";

            WelfareIndicatorBLL objWelfareIndicatorBLL = new WelfareIndicatorBLL();
            if (Convert.ToInt32(ViewState["WelfareIndicatorID"]) == 0)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    WelfareIndicatorBO objWelfareIndicatorBO = new WelfareIndicatorBO();
                    objWelfareIndicatorBO.Wlf_indicatorname = txtWelfareIndicator.Text.ToString().Trim();
                    objWelfareIndicatorBO.Fieldtype = rbtnfield.SelectedItem.ToString();
                    objWelfareIndicatorBO.AssociatedWith = Convert.ToInt32(ddlAssociatedWith.SelectedItem.Value);
                    objWelfareIndicatorBO.UserID = Convert.ToInt32(uID);
                    message = objWelfareIndicatorBLL.InsertWelfareIndicator(objWelfareIndicatorBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objWelfareIndicatorBLL = null;
                }

                BindGrid();
            }
            //edit the data in the textbox exiting in the Grid
            else if (Convert.ToInt32(ViewState["WelfareIndicatorID"]) > 0)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    WelfareIndicatorBO objWelfareIndicatorBO = new WelfareIndicatorBO();
                    objWelfareIndicatorBO.Wlf_indicatorname = txtWelfareIndicator.Text.ToString().Trim();
                    objWelfareIndicatorBO.Fieldtype = rbtnfield.SelectedItem.ToString();
                    objWelfareIndicatorBO.AssociatedWith = Convert.ToInt32(ddlAssociatedWith.SelectedItem.Value);
                    objWelfareIndicatorBO.Wlf_indicatorID = Convert.ToInt32(ViewState["WelfareIndicatorID"]);
                    objWelfareIndicatorBO.UserID = Convert.ToInt32(uID);

                    message = objWelfareIndicatorBLL.UpdateWelfareIndicator(objWelfareIndicatorBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objWelfareIndicatorBLL = null;
                }

                BindGrid();
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            ClearData();

        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
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
                ViewState["WelfareIndicatorID"] = ((Literal)gr.FindControl("litUserID")).Text;
                WelfareIndicatorBLL objWelfareIndicatorBLL = new WelfareIndicatorBLL();
                message = objWelfareIndicatorBLL.ObsoleteWelfareIndicator(Convert.ToInt32(ViewState["WelfareIndicatorID"]), Convert.ToString(chk.Checked));
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
        #endregion

        #region GridEvent

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdWelfareIndicator_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {
                ViewState["WelfareIndicatorID"] = e.CommandArgument;
                GetWelfareIndicatorDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["WelfareIndicatorID"] = e.CommandArgument;
                DeleteWelfareIndicator();
                SetUpdateMode(false);
                BindGrid();
            }
        }

        /// <summary>
        /// To change the page
        /// </summary>
        
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdWelfareIndicator.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
            WelfareIndicatorBLL objWelfareIndicatorBLL = new WelfareIndicatorBLL();
            grdWelfareIndicator.DataSource = objWelfareIndicatorBLL.GetWelfareIndicator();
            grdWelfareIndicator.DataBind();

            ListItem firstItem = new ListItem(ddlAssociatedWith.Items[0].Text, ddlAssociatedWith.Items[0].Value);

            ddlAssociatedWith.DataSource = objWelfareIndicatorBLL.GetWelfareIndicator();
            ddlAssociatedWith.DataTextField = "Wlf_indicatorname";
            ddlAssociatedWith.DataValueField = "Wlf_indicatorID";
            ddlAssociatedWith.DataBind();
            ddlAssociatedWith.Items.Insert(0, firstItem);
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
                ViewState["WelfareIndicatorID"] = 0;
            }
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtWelfareIndicator.Text = string.Empty;
            rbtnfield.SelectedIndex = 0;
            ddlAssociatedWith.ClearSelection();
        }

        /// <summary>
        /// Delete the data
        /// </summary>
        
        private void DeleteWelfareIndicator()
        {
            WelfareIndicatorBLL objWelfareIndicatorBLL = new WelfareIndicatorBLL();
            int WelfareIndicatorID = 0;
            string message = string.Empty;
            try
            {
                if (ViewState["WelfareIndicatorID"] != null)
                    WelfareIndicatorID = Convert.ToInt32(ViewState["WelfareIndicatorID"].ToString());

                message = objWelfareIndicatorBLL.DeleteWelfareIndicator(WelfareIndicatorID);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the details
        /// </summary>
        
        private void GetWelfareIndicatorDetails()
        {
            WelfareIndicatorBLL objWelfareIndicatorBLL = new WelfareIndicatorBLL();
            int WelfareIndicatorID = 0;

            if (ViewState["WelfareIndicatorID"] != null)
                WelfareIndicatorID = Convert.ToInt32(ViewState["WelfareIndicatorID"]);

            WelfareIndicatorBO objWelfareIndicatorBO = new WelfareIndicatorBO();

            objWelfareIndicatorBO = objWelfareIndicatorBLL.GetWelfareIndicatorById(WelfareIndicatorID);

            txtWelfareIndicator.Text = objWelfareIndicatorBO.Wlf_indicatorname;
            rbtnfield.ClearSelection();
            if (rbtnfield.Items.FindByText(objWelfareIndicatorBO.Fieldtype) != null)
                rbtnfield.Items.FindByText(objWelfareIndicatorBO.Fieldtype).Selected = true;

            ddlAssociatedWith.ClearSelection();
            if (ddlAssociatedWith.Items.FindByValue(objWelfareIndicatorBO.AssociatedWith.ToString()) != null)
                ddlAssociatedWith.Items.FindByValue(objWelfareIndicatorBO.AssociatedWith.ToString()).Selected = true;

            ViewState["WelfareIndicatorID"] = objWelfareIndicatorBO.Wlf_indicatorID;
        }
        #endregion
    }
}