using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;


/**
 * 
 * @version		 0.1 CardType aspx.cs
 * @package		 CardType
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
 * @Created Date 17-April-2013
 * @Updated By    Mahalakshmi
 * @Updated Date   16-May-2013
 * 
 */


namespace WIS
{
    public partial class CardType : System.Web.UI.Page
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
                Master.PageHeader = "CardType";
                ViewState["CardTypeID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                CardTypeTextBox.Attributes.Add("onchange", "setDirtyText(this," + SaveButton.ClientID + ");");


                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CardType) == false)
                {

                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCardType.Columns[2].Visible = false;
                    grdCardType.Columns[3].Visible = false;
                    grdCardType.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdCardType.Rows)
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
            CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
            grdCardType.DataSource = CardTypeBLLobj.GETALLCardType();
            grdCardType.DataBind();
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
                string CardTypeID = ((Literal)gr.FindControl("litCardTypeID")).Text;
                CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
                message = CardTypeBLLobj.ObsoleteCardType(Convert.ToInt32(CardTypeID), Convert.ToString(chk.Checked));
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
        protected void grdCardType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["CardTypeID"] = e.CommandArgument;
                int concrnID = Convert.ToInt32(ViewState["CardTypeID"]);
                GetCardTypeDetails(concrnID);
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
                message = CardTypeBLLobj.DeleteCardType(Convert.ToInt32(e.CommandArgument));
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
        private void GetCardTypeDetails(int CardTypeID)
        {
            CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
            //int CardTypeID = 0;

            //if (ViewState["CardTypeID"] != null)
            //    CardTypeID = Convert.ToInt32(ViewState["CardTypeID"]);

            CardTypeBO CardTypeObj = new CardTypeBO();
            CardTypeObj = CardTypeBLLobj.GetCardTypeById(CardTypeID);

            CardTypeTextBox.Text = CardTypeObj.CardTypeName;
            CardTypeIDTextBox.Text = CardTypeObj.CardTypeID.ToString();
            //int CardTypeID_test = Convert.ToInt32(CardTypeObj.CardTypeID);
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

            if (CardTypeIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                CardTypeBLL CardTypeBLLOBJ = new CardTypeBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CardTypeBO objCardType = new CardTypeBO();
                    objCardType.CardTypeName = CardTypeTextBox.Text.ToString().Trim(); ;
                    objCardType.UserID = Convert.ToInt32(uID);

                    CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
                    message = CardTypeBLLobj.Insert(objCardType);

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
                    CardTypeBLLOBJ = null;
                }
            }
            //edit the data in the textbox exiting in the Grid
            else if (CardTypeIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                CardTypeBLL CardTypeBLLOBJ = new CardTypeBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CardTypeBO objCardType = new CardTypeBO();
                    objCardType.CardTypeName = CardTypeTextBox.Text.ToString().Trim();
                    objCardType.CardTypeID = Convert.ToInt32(CardTypeIDTextBox.Text.ToString().Trim());
                    objCardType.UserID = Convert.ToInt32(uID);

                    CardTypeBLL CardTypeBLLobj = new CardTypeBLL();
                    message = CardTypeBLLobj.EDITCardType(objCardType);

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
                    CardTypeBLLOBJ = null;
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
            CardTypeTextBox.Text = "";
            CardTypeIDTextBox.Text = "";
        }
        /// <summary>
        /// change Page in the Grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCardType.PageIndex = e.NewPageIndex;
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
                ViewState["CardTypeID"] = "0";
                CardTypeIDTextBox.Text = String.Empty;
            }
        }
    }
}