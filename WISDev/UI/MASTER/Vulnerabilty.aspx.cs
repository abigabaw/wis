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
    public partial class Vulnerabilty : System.Web.UI.Page
    {
        #region for Declraction
        VulnerabilityBO VulnerabilityBOObj = null;
        VulnerabilityBLL VulnerabilityBLLObj = null;
        #endregion 

        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region for pageload Screen Intialliation
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Disability";
                ViewState["VulnerabilityID"] = 0;
                BindGrid();
                //txtVulnerability.Attributes.Add("onchange", "isDirty = 1;");
                txtVulnerability.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    dv_Details.Columns[2].Visible = false;
                    dv_Details.Columns[3].Visible = false;
                    dv_Details.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in dv_Details.Rows)
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
        
        #endregion

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        #region for Bind data from database Table
        private void BindGrid()
        {
            VulnerabilityBLLObj = new VulnerabilityBLL();
            dv_Details.DataSource = VulnerabilityBLLObj.GetALLVulnerability();
            dv_Details.DataBind();
        }
        #endregion

        /// <summary>
        /// Save, clear, update the data.
        /// </summary>
        #region for Save, clear, update(Insert) the Record
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ViewState["VulnerabilityID"].ToString() == "0")
            {
                message = Insert();

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearFields();
                    BindGrid();
                }
            }
            else
            {
                string uID = Session["USER_ID"].ToString();
                int reasonid = Convert.ToInt32(ViewState["VulnerabilityID"]);
                VulnerabilityBOObj = new VulnerabilityBO();
                VulnerabilityBOObj.VulnerabilityType = txtVulnerability.Text.ToString();
                VulnerabilityBOObj.CreatedBy = Convert.ToInt32(uID);
                VulnerabilityBLLObj = new VulnerabilityBLL();
                message = VulnerabilityBLLObj.Update(VulnerabilityBOObj, reasonid);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearFields();
                    BindGrid();
                    SetUpdateMode(false);
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }

        private void ClearFields()
        {
            txtVulnerability.Text = string.Empty;
            ViewState["VulnerabilityID"] = 0;
        }

        private string Insert()
        {
            string message = string.Empty;
            try
            {
                string uID = Session["USER_ID"].ToString();
                VulnerabilityBOObj = new VulnerabilityBO();
                VulnerabilityBOObj.VulnerabilityType = txtVulnerability.Text.ToString();
                VulnerabilityBOObj.CreatedBy = Convert.ToInt32(uID); ;
                VulnerabilityBLLObj = new VulnerabilityBLL();
                message = VulnerabilityBLLObj.insert(VulnerabilityBOObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                VulnerabilityBLLObj = null;
            }
            return message;
        }
        #endregion

        /// <summary>
        /// Edit,delete and soft delete the data.
        /// </summary>
        #region for Grid data(Edit Delete and absoult record)
        protected void dv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["VulnerabilityID"] = e.CommandArgument;
                int reasonID = Convert.ToInt32(ViewState["VulnerabilityID"]);
                GetVulnerabiltyDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                VulnerabilityBLL VulnerabilityBLLObj = new VulnerabilityBLL();
                message = VulnerabilityBLLObj.Deletevulnerability(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(true);
                ClearFields();
                BindGrid();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
             }

        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
               string VulnerabilityID = ((Literal)gr.FindControl("litVulnerabilityID")).Text;
               int VulnerabilityID_ = Convert.ToInt32(VulnerabilityID);
                VulnerabilityBLL VulnerabilityBLLObj = new VulnerabilityBLL();
                message = VulnerabilityBLLObj.Obsoletevulnerability(VulnerabilityID_, Convert.ToString(chk.Checked));
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
        
        //to get record for the grid for update
        private void GetVulnerabiltyDetails()
        {
            VulnerabilityBLLObj = new VulnerabilityBLL();
            int VulnerabilityID = 0;

            if (ViewState["VulnerabilityID"] != null)
                VulnerabilityID = Convert.ToInt32(ViewState["VulnerabilityID"]);

            VulnerabilityBOObj = new VulnerabilityBO();
            VulnerabilityBOObj = VulnerabilityBLLObj.GetVulnerablitybyID(VulnerabilityID);

            txtVulnerability.Text = VulnerabilityBOObj.VulnerabilityType;
        }
        #endregion

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        //Claer the text field
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// To change the page.
        /// </summary>
        #region Change Page Event
        protected void dv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dv_Details.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion 

        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        //function used for Change Button Name Save Clear into Update and Clear
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
                ViewState["VulnerabilityID"] = "0";
            }
        }
    }
}