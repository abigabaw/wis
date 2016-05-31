using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Text;

namespace WIS.UI.MASTER
{
    public partial class OptionAvailable : System.Web.UI.Page
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Option Available";
                ViewState["CONCERNID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                OptionAvailableTextBox.Attributes.Add("onchange", "setDirtyText(this," + SaveButton.ClientID + ");");


                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_OPTION_GROUP) == false)
                {

                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdOptionAvailable.Columns[2].Visible = false;
                    grdOptionAvailable.Columns[3].Visible = false;
                    grdOptionAvailable.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdOptionAvailable.Rows)
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
        private void BindGrid(bool addRow, bool deleteRow)
        {
            OptionAvailableBLL optionavailobj = new OptionAvailableBLL();
            grdOptionAvailable.DataSource = optionavailobj.GetAllOptionAvail();
            grdOptionAvailable.DataBind();
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
                string CONCERNID = ((Literal)gr.FindControl("litOptionAvailableID")).Text;
                OptionAvailableBLL optionavailobj = new OptionAvailableBLL();
                message = optionavailobj.Obsoleteoptionavail(Convert.ToInt32(CONCERNID), Convert.ToString(chk.Checked));
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

        protected void grdOptionAvailable_RowCommand(object sender, GridViewCommandEventArgs e)
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
                OptionAvailableBLL optionavailobj = new OptionAvailableBLL();
                message = optionavailobj.Deleteoptionavail(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                clear();
                SetUpdateMode(false);
                BindGrid(false, true);

            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        private void GetConcernDetails(int ConcernID)
        {
            OptionAvailableBLL optionavailobj = new OptionAvailableBLL();


            OptionAvailableBO optionavailbo = new OptionAvailableBO();
            optionavailbo = optionavailobj.GetAllOptionById(ConcernID);

            OptionAvailableTextBox.Text = optionavailbo.OptionAvailable;
            OptionAvailableIDTextBox.Text = optionavailbo.ID.ToString();

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (OptionAvailableIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                OptionAvailableBLL optionavailsal = new OptionAvailableBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OptionAvailableBO objOptionAvailable = new OptionAvailableBO();
                    objOptionAvailable.OptionAvailable = OptionAvailableTextBox.Text.ToString().Trim(); ;
                    objOptionAvailable.UserID = Convert.ToInt32(uID);

                    optionavailsal = new OptionAvailableBLL();
                    message = optionavailsal.Insert(objOptionAvailable);

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
                    optionavailsal = null;
                }
            }
            //edit the data in the textbox exiting in the Grid
            else if (OptionAvailableIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                OptionAvailableBLL optionavailsal = new OptionAvailableBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OptionAvailableBO objoptionavail = new OptionAvailableBO();
                    objoptionavail.OptionAvailable = OptionAvailableTextBox.Text.ToString().Trim();
                    objoptionavail.ID = Convert.ToInt32(OptionAvailableIDTextBox.Text.ToString().Trim());
                    objoptionavail.UserID = Convert.ToInt32(uID);

                    optionavailsal = new OptionAvailableBLL();
                    message = optionavailsal.editoptionavail(objoptionavail);

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
                    optionavailsal = null;
                }
            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            clear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        public void clear()
        {
            OptionAvailableIDTextBox.Text = "";
            OptionAvailableTextBox.Text = "";
        }

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdOptionAvailable.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
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
                OptionAvailableIDTextBox.Text = String.Empty;
            }
        }
    }
}