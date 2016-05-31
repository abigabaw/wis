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
    public partial class Concern : System.Web.UI.Page
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
                Master.PageHeader = "Concern";
                ViewState["CONCERNID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                ConcernTextBox.Attributes.Add("onchange", "setDirtyText(this," + SaveButton.ClientID + ");");
               

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {

                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdConcern.Columns[2].Visible = false;
                    grdConcern.Columns[3].Visible = false;
                    grdConcern.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdConcern.Rows)
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
            ConcernBLL ConcernBLLobj = new ConcernBLL();
            grdConcern.DataSource = ConcernBLLobj.GETALLCONCERN();
            grdConcern.DataBind();
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
                string CONCERNID = ((Literal)gr.FindControl("litCONCERNID")).Text;
                ConcernBLL ConcernBLLobj = new ConcernBLL();
                message = ConcernBLLobj.ObsoleteConcern(Convert.ToInt32(CONCERNID), Convert.ToString(chk.Checked));
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
        protected void grdConcern_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["ConcernID"] = e.CommandArgument;
                int concrnID = Convert.ToInt32(ViewState["ConcernID"]);
                GetConcernDetails(concrnID);
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ConcernBLL ConcernBLLobj = new ConcernBLL();
                message = ConcernBLLobj.DeleteConcern(Convert.ToInt32(e.CommandArgument));
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
        private void GetConcernDetails(int ConcernID)
        {
            ConcernBLL ConcernBLLobj = new ConcernBLL();
            //int ConcernID = 0;

            //if (ViewState["CONCERNID"] != null)
            //    ConcernID = Convert.ToInt32(ViewState["CONCERNID"]);

            ConcernBO ConcernObj = new ConcernBO();
            ConcernObj = ConcernBLLobj.GetConcernById(ConcernID);

            ConcernTextBox.Text = ConcernObj.ConcernName;
            ConcernIDTextBox.Text = ConcernObj.ConcernID.ToString();
            //int ConcernID_test = Convert.ToInt32(ConcernObj.ConcernID);
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

            if (ConcernIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ConcernBO objConcern = new ConcernBO();
                    objConcern.ConcernName = ConcernTextBox.Text.ToString().Trim(); ;
                    objConcern.UserID = Convert.ToInt32(uID);

                    ConcernBLL ConcernBLLobj = new ConcernBLL();
                    message = ConcernBLLobj.Insert(objConcern);

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
                    concernBLLOBJ = null;
                }
            }
                //edit the data in the textbox exiting in the Grid
            else if (ConcernIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ConcernBO objConcern = new ConcernBO();
                    objConcern.ConcernName = ConcernTextBox.Text.ToString().Trim();
                    objConcern.ConcernID = Convert.ToInt32(ConcernIDTextBox.Text.ToString().Trim());
                    objConcern.UserID = Convert.ToInt32(uID);

                    ConcernBLL ConcernBLLobj = new ConcernBLL();
                    message = ConcernBLLobj.EDITCONCERN(objConcern);

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
                    concernBLLOBJ = null;
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
            ConcernTextBox.Text = "";
            ConcernIDTextBox.Text = "";
        }
        /// <summary>
        /// change Page in the Grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdConcern.PageIndex = e.NewPageIndex;
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
                ViewState["CONCERNID"] = "0";
                ConcernIDTextBox.Text = String.Empty;
            }
        }
    }
}