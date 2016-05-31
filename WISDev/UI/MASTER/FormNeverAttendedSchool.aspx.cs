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
    public partial class FormNeverAttendedSchool : System.Web.UI.Page
    {
        NeverAttendedSchoolBO NeverAttendedSchoolBOObj = null;
        NeverAttendedSchoolBLL NeverAttendedSchoolBLLobj = null;
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
                Master.PageHeader = "Never Attended School";
                ViewState["NVR_ATT_SCH_REASONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
               // NeverAttndSchlTextBox.Attributes.Add("onclick", "isDirty=1;");
                NeverAttndSchlTextBox.Attributes.Add("onchange", "setDirtyText();");
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_EDUCATION) == false)
                {

                    saveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdNASchool.Columns[3].Visible = false;
                    grdNASchool.Columns[4].Visible = false;
                    grdNASchool.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdNASchool.Rows)
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
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void saveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (saveButton.Text == "Save")
            {
                message = SaveNASchoolDetails();
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearData();
                }
            }
            if (saveButton.Text == "Update")
            {
                message = EditNASchoolDetails();
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearData();
                    SetUpdateMode(false);
                }
            }

            BindGrid(true, true);

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            NeverAttendedSchoolBLL ConcernBLLobj = new NeverAttendedSchoolBLL();
            grdNASchool.DataSource = ConcernBLLobj.GetAllNeverAttendedSchool();
            grdNASchool.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdNASchool_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["NVR_ATT_SCH_REASONID"] = e.CommandArgument;
                GetNASchoolDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteNASchoolDetails(Convert.ToInt32(e.CommandArgument));
                ClearData();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        ///<summary>
        ///change Page in the Grid
        ///</summary>
       
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdNASchool.PageIndex = e.NewPageIndex;
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
               
                string NVR_ATT_SCH_REASONID = ((Literal)gr.FindControl("litNeverattentedSchoolID")).Text;
                NeverAttendedSchoolBLL ConcernBLLobj = new NeverAttendedSchoolBLL();
                message = ConcernBLLobj.ObsoleteNASchool(Convert.ToInt32(NVR_ATT_SCH_REASONID), Convert.ToString(chk.Checked));
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
        ///<summary>
        ///To fetch details
        ///</summary>
        private void GetNASchoolDetails()
        {
            NeverAttendedSchoolBLL NeverAttendedSchoolBLLobj = new NeverAttendedSchoolBLL();
            int NVRATTSCHREASONID = 0;

            if (ViewState["NVR_ATT_SCH_REASONID"] != null)
                NVRATTSCHREASONID = Convert.ToInt32(ViewState["NVR_ATT_SCH_REASONID"]);

            NeverAttendedSchoolBO NeverAttendedSchoolBOObj = new NeverAttendedSchoolBO();
            NeverAttendedSchoolBOObj = NeverAttendedSchoolBLLobj.GetNASchoolById(NVRATTSCHREASONID);

            NeverAttndSchlTextBox.Text = NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON;
            DescriptionTextBox.Text = NeverAttendedSchoolBOObj.DESCRIPTION;
            //int ConcernID_test = Convert.ToInt32(ConcernObj.ConcernID);
        }
        ///<summary>
        ///To delete details
        ///</summary>

        private void DeleteNASchoolDetails(int NASchoolID)
        {
            string message = string.Empty;
            NeverAttendedSchoolBLL NeverAttendedSchoolBLLobj = new NeverAttendedSchoolBLL();
            message = NeverAttendedSchoolBLLobj.DeleteNASchoolById(NASchoolID);
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                message = "Data Deleted successfully";
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            try
            {
                NeverAttndSchlTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                saveButton.Text = "Save";
                ViewState["NVR_ATT_SCH_REASONID"] = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string EditNASchoolDetails()
        {
            string message = string.Empty;
            NeverAttendedSchoolBLL NeverAttendedSchoolBLLObj = new NeverAttendedSchoolBLL();

            try
            {
                NeverAttendedSchoolBO NeverAttendedSchoolBOObj = new NeverAttendedSchoolBO();

                if (ViewState["NVR_ATT_SCH_REASONID"] != null)
                    NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASONID = Convert.ToInt32(ViewState["NVR_ATT_SCH_REASONID"].ToString());

                string uID = Session["USER_ID"].ToString();

                NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON = NeverAttndSchlTextBox.Text.ToString().Trim();
                NeverAttendedSchoolBOObj.DESCRIPTION = DescriptionTextBox.Text.ToString().Trim();
                NeverAttendedSchoolBOObj.UpdatedBy = Convert.ToInt32(uID);

                //BLL.NeverAttendedSchoolBLL NeverAttendedSchoolBLLObj = new BLL.NeverAttendedSchoolBLL();
                message = NeverAttendedSchoolBLLObj.EDITNASCHOOL(NeverAttendedSchoolBOObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                NeverAttendedSchoolBLLObj = null;
            }
            return message;
        }

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string SaveNASchoolDetails()
        {
            string message = string.Empty;
            NeverAttendedSchoolBOObj = new NeverAttendedSchoolBO();

                string NeverAttndSchl = string.Empty;
                string Description = string.Empty;
                string uID = Session["USER_ID"].ToString();

                NeverAttndSchl = NeverAttndSchlTextBox.Text.ToString().Trim();
                Description = DescriptionTextBox.Text.ToString().Trim();

                NeverAttendedSchoolBOObj.NVR_ATT_SCH_REASON = NeverAttndSchl;
                NeverAttendedSchoolBOObj.DESCRIPTION = Description;
                //NeverAttendedSchoolBOObj.IsDeleted = "false";
                NeverAttendedSchoolBOObj.CreatedBy = Convert.ToInt32(uID);

                NeverAttendedSchoolBLLobj = new NeverAttendedSchoolBLL(); // Data sending to Next layer BAL

                try
                {
                    message = NeverAttendedSchoolBLLobj.InsertIntoNeverAttendedSchool(NeverAttendedSchoolBOObj); // Value passing to Next layer
                }
                catch (Exception ee) // find the Exception
                {
                    throw ee;
                }

                finally // set the finally class nothing but Empty the object emp_mst_Bal
                {
                    NeverAttendedSchoolBLLobj = null;
                }

            return message;
        }
        /// <summary>
        /// to change text of the button based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                saveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                saveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["NVR_ATT_SCH_REASONID"] = "0";
            }
        }
    }
}