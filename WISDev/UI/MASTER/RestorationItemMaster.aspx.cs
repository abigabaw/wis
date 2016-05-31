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
    public partial class RestorationItemMaster : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        LivelihoodRestoreItemsBO objItem;
        LivelihoodRestoreItemsBLL objLivelihoodRestoreItemsBLL;
        #endregion

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
                Master.PageHeader = "Items";
                ViewState["Liv_Rest_ItemID"] = 0;
                BindGrid(false, false);
                // ClearDetails();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdItem.Columns[3].Visible = false;
                    grdItem.Columns[4].Visible = false;
                    grdItem.Columns[5].Visible = false;
                }
            }
        }
    

        #region Load Default Values
        //Required when Data Loaded in Controls
        #endregion

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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            //Clearing TextBoxes
            txtItemName.Text = string.Empty;

            //Clearing Viewstate Values 
            ViewState["Liv_Rest_ItemID"] = "0";
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e

     
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objLivelihoodRestoreItemsBLL = new LivelihoodRestoreItemsBLL();
            objItem = new LivelihoodRestoreItemsBO();

            grdItem.DataSource = objLivelihoodRestoreItemsBLL.GetLiveRestItems_All();//(objItem);
            grdItem.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["Liv_Rest_ItemID"] = e.CommandArgument;
                GetItemDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteItem(e.CommandArgument.ToString());
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// used to display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>e
        protected void grdItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdItem.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        /// <summary>
        /// To fetch details from database and assign to textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>e

        
        private void GetItemDetails()
        {
            objLivelihoodRestoreItemsBLL = new LivelihoodRestoreItemsBLL();
            int ItemID = 0;

            if (ViewState["Liv_Rest_ItemID"] != null)
                ItemID = Convert.ToInt32(ViewState["Liv_Rest_ItemID"].ToString());

            objItem = new LivelihoodRestoreItemsBO();
            objItem = objLivelihoodRestoreItemsBLL.GetLiveRestItemsById(ItemID);

            txtItemName.Text = objItem.Liv_Rest_ItemName;
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

                string ItemID = ((Literal)gr.FindControl("litItemID")).Text;
                objLivelihoodRestoreItemsBLL = new LivelihoodRestoreItemsBLL();

                message = objLivelihoodRestoreItemsBLL.ObsoleteLiveRestItem(Convert.ToInt32(ItemID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
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
        /// Delete details from  Database based on itemid
        /// </summary>
        private void DeleteItem(string ItemID)
        {
            objLivelihoodRestoreItemsBLL = new LivelihoodRestoreItemsBLL();
            string message = string.Empty;

            message = objLivelihoodRestoreItemsBLL.DeleteLiveRestItem(Convert.ToInt32(ItemID));
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";

            ClearDetails();
            BindGrid(false, true);
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            objItem = new LivelihoodRestoreItemsBO();
            objLivelihoodRestoreItemsBLL = new LivelihoodRestoreItemsBLL();

            //Assignement
            objItem.Liv_Rest_ItemName = txtItemName.Text.Trim();

            if (ViewState["Liv_Rest_ItemID"] != null)
                objItem.Liv_Rest_ItemID = Convert.ToInt32(ViewState["Liv_Rest_ItemID"].ToString());

            objItem.IsDeleted = "False";

            if (Session["USER_ID"] != null)
                objItem.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());


            if (objItem.Liv_Rest_ItemID < 1)
            {
                //If ItemID does Not exists then SaveItem
                objItem.Liv_Rest_ItemID = -1;//For New Item
                message = objLivelihoodRestoreItemsBLL.AddLiveRestItem(objItem);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            else
            {
                if (Session["USER_ID"] != null)
                    objItem.UpdatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                //If ItemID exists then UpdateItem
                message = objLivelihoodRestoreItemsBLL.UpdateLiveRestItem(objItem); //For Updating Item

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }

            }

            if (message != "")
            {
                AlertMessage = "alert('" + message + "');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            }

            SetUpdateMode(false);
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
                ViewState["Liv_Rest_ItemID"] = "0";
            }
        }
    }
}