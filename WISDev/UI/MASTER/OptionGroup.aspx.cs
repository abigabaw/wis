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
    public partial class OptionGroup : System.Web.UI.Page
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
                Master.PageHeader = "Option Groups";
                ViewState["OptionGroupID"] = 0;
                BindGrid(false, false);
                //txtOptionGroup.Attributes.Add("onchange", "isDirty = 1;");
                txtOptionGroup.Attributes.Add("onchange", "setDirtyText();");
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_GROUP) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdOptionGroup.Columns[2].Visible = false;
                    grdOptionGroup.Columns[3].Visible = false;
                    grdOptionGroup.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdOptionGroup.Rows)
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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";

            OptionGroupBLL objOptionGroupBLL = new OptionGroupBLL();
            if (Convert.ToInt32(ViewState["OptionGroupID"]) == 0)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OptionGroupBO objOptionGroupBO = new OptionGroupBO();
                    objOptionGroupBO.OptionGroupName = txtOptionGroup.Text.ToString().Trim();
                    objOptionGroupBO.UserID = Convert.ToInt32(uID);
                    message = objOptionGroupBLL.InsertOptionGroups(objOptionGroupBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                   // txtOptionGroup.Text = "0";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objOptionGroupBLL = null;
                }

                BindGrid(true, true);
            }
            //edit the data in the textbox exiting in the Grid
            else if (Convert.ToInt32(ViewState["OptionGroupID"]) > 0)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OptionGroupBO objOptionGroupBO = new OptionGroupBO();
                    objOptionGroupBO.OptionGroupName = txtOptionGroup.Text.ToString().Trim();
                    objOptionGroupBO.OptionGroupID = Convert.ToInt32(ViewState["OptionGroupID"]);
                    objOptionGroupBO.UserID = Convert.ToInt32(uID);


                    message = objOptionGroupBLL.UpdateOptionGroups(objOptionGroupBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";
                    txtOptionGroup.Text = "0";
                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    objOptionGroupBLL = null;
                }

                BindGrid(true, true);
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }

            ClearData();

        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }

        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


    

        protected void grdOptionGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditRow")
            {
                ViewState["OptionGroupID"] = e.CommandArgument;
                GetOptionGroupDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteOptionGroup(e.CommandArgument.ToString());
                SetUpdateMode(false);
                ClearData();
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// To change page in the grid
        /// </summary>

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdOptionGroup.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
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
                string OptionGroupID = ((Literal)gr.FindControl("litUserID")).Text;
                OptionGroupBLL objOptionGroupBLL = new OptionGroupBLL();
                message = objOptionGroupBLL.ObsoleteOptionGroup(Convert.ToInt32(OptionGroupID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }

                BindGrid(true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e


        private void BindGrid(bool addRow, bool deleteRow)
        {
            OptionGroupBLL objOptionGroupBLL = new OptionGroupBLL();
            grdOptionGroup.DataSource = objOptionGroupBLL.GetOptionGroup();
            grdOptionGroup.DataBind();
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
                ViewState["OptionGroupID"] = 0;
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearData()
        {
            txtOptionGroup.Text = string.Empty;           
        }
        /// <summary>
        /// To delete Optiongroup from grid
        /// </summary>
        private void DeleteOptionGroup(string optionGroupID)
        {
            OptionGroupBLL objOptionGroupBLL = new OptionGroupBLL();
            string message = string.Empty;

            try
            {
                message = objOptionGroupBLL.DeleteOptionGroup(Convert.ToInt32(optionGroupID));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGrid(false, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To fetch Optiongroup from database
        /// </summary>
        private void GetOptionGroupDetails()
        {
            OptionGroupBLL objOptionGroupBLL = new OptionGroupBLL();
            int OptionGroupID = 0;

            if (ViewState["OptionGroupID"] != null)
                OptionGroupID = Convert.ToInt32(ViewState["OptionGroupID"]);

            OptionGroupBO objOptionGroupBO = new OptionGroupBO();

            objOptionGroupBO = objOptionGroupBLL.GetOptionGroupById(OptionGroupID);
            txtOptionGroup.Text = objOptionGroupBO.OptionGroupName;
        }

    
    }
}