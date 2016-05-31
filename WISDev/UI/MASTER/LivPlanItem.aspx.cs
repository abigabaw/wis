using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class LivPlanItem : System.Web.UI.Page
    {
        System.Data.DataTable dt = new System.Data.DataTable();
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
                Master.PageHeader = "LivPlanItem";
                ViewState["LivPlanItemID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                LivPlanItemTextBox.Attributes.Add("onchange", "setDirtyText(this," + SaveButton.ClientID + ");");


                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_LivPlanItemMst) == false)
                {

                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdLivPlanItem.Columns[2].Visible = false;
                    grdLivPlanItem.Columns[3].Visible = false;
                    grdLivPlanItem.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdLivPlanItem.Rows)
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
            grdLivPlanItem.DataSource = LivPlanItemBLLobj.GETALLLivPlanItem();
            grdLivPlanItem.DataBind();
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
                string LivPlanItemID = ((Literal)gr.FindControl("litLivPlanItemID")).Text;
                LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
                message = LivPlanItemBLLobj.ObsoleteLivPlanItem(Convert.ToInt32(LivPlanItemID), Convert.ToString(chk.Checked));
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdLivPlanItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["LivPlanItemID"] = e.CommandArgument;
                int concrnID = Convert.ToInt32(ViewState["LivPlanItemID"]);
                GetLivPlanItemDetails(concrnID);
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
                message = LivPlanItemBLLobj.DeleteLivPlanItem(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                clear();
                SetUpdateMode(false);
                BindGrid(false, true);

            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

        }

        /// <summary>
        /// get the Grid value into textBox
        /// </summary>       
        private void GetLivPlanItemDetails(int LivPlanItemID)
        {
            LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
            //int LivPlanItemID = 0;

            //if (ViewState["LivPlanItemID"] != null)
            //    LivPlanItemID = Convert.ToInt32(ViewState["LivPlanItemID"]);

            LivPlanItemBO LivPlanItemObj = new LivPlanItemBO();
            LivPlanItemObj = LivPlanItemBLLobj.GetLivPlanItemById(LivPlanItemID);

            LivPlanItemTextBox.Text = LivPlanItemObj.LivPlanItemName;
            LivPlanItemIDTextBox.Text = LivPlanItemObj.LivPlanItemID.ToString();
            //int LivPlanItemID_test = Convert.ToInt32(LivPlanItemObj.LivPlanItemID);
        }

        /// <summary>
        /// save data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // int count = 0;
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (LivPlanItemIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                LivPlanItemBLL LivPlanItemBLLOBJ = new LivPlanItemBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    LivPlanItemBO objLivPlanItem = new LivPlanItemBO();
                    objLivPlanItem.LivPlanItemName = LivPlanItemTextBox.Text.ToString().Trim(); ;
                    objLivPlanItem.UserID = Convert.ToInt32(uID);

                    LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
                    message = LivPlanItemBLLobj.Insert(objLivPlanItem);

                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        clear();
                        // ClearDetails();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    LivPlanItemBLLOBJ = null;
                }
            }
            //edit the data in the textbox exiting in the Grid
            else if (LivPlanItemIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                LivPlanItemBLL LivPlanItemBLLOBJ = new LivPlanItemBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    LivPlanItemBO objLivPlanItem = new LivPlanItemBO();
                    objLivPlanItem.LivPlanItemName = LivPlanItemTextBox.Text.ToString().Trim();
                    objLivPlanItem.LivPlanItemID = Convert.ToInt32(LivPlanItemIDTextBox.Text.ToString().Trim());
                    objLivPlanItem.UserID = Convert.ToInt32(uID);

                    LivPlanItemBLL LivPlanItemBLLobj = new LivPlanItemBLL();
                    message = LivPlanItemBLLobj.EDITLivPlanItem(objLivPlanItem);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        clear();
                        SetUpdateMode(false);
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    LivPlanItemBLLOBJ = null;
                }
            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        ///calls clear method
        /// </summary>

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        public void clear()
        {
            LivPlanItemTextBox.Text = "";
            LivPlanItemIDTextBox.Text = "";
        }
        /// <summary>
        /// change Page in the Grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdLivPlanItem.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
        /// <summary>
        /// to change text of button based on condition
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
                ViewState["LivPlanItemID"] = "0";
                LivPlanItemIDTextBox.Text = String.Empty;
            }
        }
    }
}