using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class SocialSupport : System.Web.UI.Page
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
                Master.PageHeader= "Social Support";
                ViewState["SUPPORTEDBYID"] = 0;
                BindGrid(false, false);
                txtSupportedBy.Attributes.Add("onchange","isDirty = 1;");
                txtSupportedBy.Attributes.Add("onchange", "setDirtyText();");
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    gvSupportedBy.Columns[2].Visible = false;
                    gvSupportedBy.Columns[3].Visible = false;
                    gvSupportedBy.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in gvSupportedBy.Rows)
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
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();
            gvSupportedBy.DataSource = SocialSupportBLLObj.GetALLSchoolDetails();
            gvSupportedBy.DataBind();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearDetials();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearDetials()
        {
            txtSupportedBy.Text = string.Empty;
            txtSupportedByID.Text = string.Empty;
        }
        /// <summary>
        /// calls SaveDetails  to save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SocialSupportBO SocialSupportBOObj = new SocialSupportBO();

            SocialSupportBOObj.SupportedBy = txtSupportedBy.Text.ToString().Trim();            
            SaveDetails(SocialSupportBOObj);
            //ClearDetials();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveDetails(SocialSupportBO SocialSupportBOObj)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (txtSupportedByID.Text.ToString().Trim() == string.Empty)                
            {
                try
                {
                    SocialSupportBOObj = new SocialSupportBO();
                    SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();

                    SocialSupportBOObj.SupportedBy = txtSupportedBy.Text.ToString().Trim();
                    SocialSupportBOObj.SUPPORTEDBYID = Convert.ToInt32(ViewState["SUPPORTEDBYID"]);
                    SocialSupportBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = SocialSupportBLLObj.InsertSupportDetails(SocialSupportBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearDetials();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    SocialSupportBOObj = new SocialSupportBO();
                    SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();

                    SocialSupportBOObj.SupportedBy = txtSupportedBy.Text.ToString().Trim();
                    int SUPPORTEDBYID = Convert.ToInt32(ViewState["SUPPORTEDBYID"]);
                    SocialSupportBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

                    message = SocialSupportBLLObj.EditSupportDetails(SocialSupportBOObj, SUPPORTEDBYID);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearDetials();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    SocialSupportBOObj = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// To display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSupportedBy_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupportedBy.PageIndex = e.NewPageIndex;
            BindGrid(true,false);
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
                ViewState["SUPPORTEDBYID"] = ((Literal)gr.FindControl("litSUPPORTEDBYID")).Text;
                SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();
                message = SocialSupportBLLObj.ObsoleteSocialSupport(Convert.ToInt32(ViewState["SUPPORTEDBYID"]), Convert.ToString(chk.Checked));
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
        protected void gvSupportedBy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if(e.CommandName=="EditRow")
            {
                ViewState["SUPPORTEDBYID"] = e.CommandArgument;
                int SupportEditID = Convert.ToInt32(ViewState["SUPPORTEDBYID"]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                if (row != null)
                {                   
                    txtSupportedBy.Text = row.Cells[1].Text.ToString();
                }
                SetUpdateMode(true);

                GetSupportDeatails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if(e.CommandName=="DeleteRow")
            {
                SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();
                message = SocialSupportBLLObj.DeleteSupportRow(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                ClearDetials();
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);           
        }
        /// <summary>
        /// to fetch details from database and assign to textbox
        /// </summary>
        private void GetSupportDeatails()
        {
            SocialSupportBLL SocialSupportBLLObj = new SocialSupportBLL();
            SocialSupportBO SocialSupportBOObj = new SocialSupportBO();

            int SUPPORTEDBYID = 0;

            if (ViewState["SUPPORTEDBYID"] != null)
                SUPPORTEDBYID = Convert.ToInt32(ViewState["SUPPORTEDBYID"].ToString());

            SocialSupportBOObj = new SocialSupportBO();

            SchoolStatusList SchoolStatusListobj = new SchoolStatusList();
            SocialSupportBOObj = SocialSupportBLLObj.GetSupportById(SUPPORTEDBYID);

            txtSupportedBy.Text = SocialSupportBOObj.SupportedBy;
            txtSupportedByID.Text = SocialSupportBOObj.SUPPORTEDBYID.ToString();
        }
        /// <summary>
        /// to change text of thebutton based on condition
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
                ViewState["SUPPORTEDBYID"] = "0";
            }
        }
    }
}