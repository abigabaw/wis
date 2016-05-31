using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class GoalName : System.Web.UI.Page
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
                Master.PageHeader = "M&amp;E Goal";
                BindGrid();
                ViewState["GOALID"] = 0;
                
                txtGoalName.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MNE) == false)
                {
                    btnSave.Visible = false;
                    btnClr.Visible = false;
                    gvGoalName.Columns[2].Visible = false;
                    gvGoalName.Columns[3].Visible = false;
                    gvGoalName.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in gvGoalName.Rows)
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveGoalNameDetails();
            ClearData();
            BindGrid();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtGoalName.Text = string.Empty;
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      


        private void SaveGoalNameDetails()
        {
            MNEGoalBO MNEGoalBOObj = null;
            string message = "";
            if (Convert.ToInt32(ViewState["GOALID"]) == 0)
            {
                try
                {
                    MNEGoalBOObj = new MNEGoalBO();
                    MNEGoalBLL MNEGoalBLLObj = new MNEGoalBLL();
                    MNEGoalBOObj.GoalName = txtGoalName.Text.ToString().Trim();
                    MNEGoalBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = MNEGoalBLLObj.InsertMNEGoalDetails(MNEGoalBOObj);
                    ClearData();
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                    if (message != "")
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "alert('" + message + "');", true);
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
                    MNEGoalBOObj = new MNEGoalBO();
                    MNEGoalBLL MNEGoalBLLObj = new MNEGoalBLL();

                    MNEGoalBOObj.GoalID = Convert.ToInt32(ViewState["GOALID"]);
                    MNEGoalBOObj.GoalName = txtGoalName.Text.ToString().Trim();
                    MNEGoalBOObj.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                    message = MNEGoalBLLObj.UpdateMNEGoalDetails(MNEGoalBOObj);
                    ClearData();
                    SetUpdateMode(false);
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    if (message != "")
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    MNEGoalBOObj = null;
                }
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtGoalName.Text = string.Empty;
            ViewState["GOALID"] = 0;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            MNEGoalBLL MNEGoalBLLObj = new MNEGoalBLL();
            gvGoalName.DataSource = MNEGoalBLLObj.GetAllGoalNameDetails();
            gvGoalName.DataBind();
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btnSave.Text = "Update";
                btnClr.Text = "Cancel";
            }
            else
            {
                btnSave.Text = "Save";
                btnClr.Text = "Clear";
                ViewState["GOALID"] = "0";
            }
        }


        /// <summary>
        /// to display pageno in grid
        /// </summary>
        protected void gvGoalName_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGoalName.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGoalName_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["GOALID"] = e.CommandArgument;

                MNEGoalBLL objMNEGoalBLL = new MNEGoalBLL();
                MNEGoalBO objMNEGoalBO = new MNEGoalBO();
                objMNEGoalBO = objMNEGoalBLL.GetMNEGoalNameDetailsbyID(Convert.ToInt32(ViewState["GOALID"]));
                txtGoalName.Text = objMNEGoalBO.GoalName;

                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                MNEGoalBLL objMNEGoalBLL = new MNEGoalBLL();
                message = objMNEGoalBLL.DeleteGoalName(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                if (message != "")
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "alert('" + message + "');", true);
                ClearData();
                SetUpdateMode(false);
                BindGrid();
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
                
                int GoalID =Convert.ToInt32(((Literal)gr.FindControl("litGOALID")).Text);
                MNEGoalBLL objMNEGoalBLL = new MNEGoalBLL();

                 message = objMNEGoalBLL.ObsoleteGoalName(GoalID, Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   

        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static string DeleteRecord(string args)
        //{
        //    string[] s = args.ToString().Split('|');
        //    int Id = Convert.ToInt32(s[0].ToString().Trim());
        //    MNEGoalElementBLL MNEGoalElementBLLObj = new MNEGoalElementBLL();
        //    string message = MNEGoalElementBLLObj.DeleteGoalElement(Convert.ToInt32(Id));
        //    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
        //        message = s[1].ToString();

        //    return message;
        //}
    }
}

