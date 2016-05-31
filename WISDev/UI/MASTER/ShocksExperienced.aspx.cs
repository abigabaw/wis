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
    public partial class ShocksExperienced : System.Web.UI.Page
    {
        #region Declaration
        System.Data.DataTable dt = new System.Data.DataTable();
        ShocksExperiencedBO objShocksExperienced;
        ShocksExperiencedBLL objShocksExperiencedBLL;
        #endregion

        #region Page Load
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
                Master.PageHeader = "Shocks Experienced";
                ViewState["ShockID"] = 0;
                BindGrid(false, false);
                ClearDetails();
                //txtShocksExperienced.Attributes.Add("onchange", "isDirty = 1;");
                txtShocksExperienced.Attributes.Add("onchange", "setDirtyText();");
              

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdShocksExperience.Columns[3].Visible = false;
                    grdShocksExperience.Columns[4].Visible = false;
                    grdShocksExperience.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdShocksExperience.Rows)
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

        #region Clear TextBoxes
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
            txtShocksExperienced.Text = string.Empty;
            btnSave.Text = "Save";
            ViewState["ShockID"] = 0;
        }

        #endregion
        
        #region GridView
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            objShocksExperiencedBLL = new ShocksExperiencedBLL();

            grdShocksExperience.DataSource = objShocksExperiencedBLL.GetALLShocksExperienced();
            grdShocksExperience.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdShocksExperience_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["ShockID"] = e.CommandArgument;
                SetUpdateMode(true);
                GetShocksExperiencedDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);

            }
            else if (e.CommandName == "DeleteRow")
            {
              
                ShocksExperiencedBLL objShocksExperiencedBLL = new ShocksExperiencedBLL();
                message = objShocksExperiencedBLL.DeleteShocksExperienced(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                ClearDetails();
                BindGrid(true, true);
                SetUpdateMode(false);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
           
        }
        /// <summary>
        /// To display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdShocksExperience_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdShocksExperience.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        #endregion
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
              
               string ShockID = ((Literal)gr.FindControl("litShocksExperiencedID")).Text;
                ShocksExperiencedBLL objShocksExperiencedBLL = new ShocksExperiencedBLL();
                message = objShocksExperiencedBLL.Obsoleteshockexperiencedid(Convert.ToInt32(ShockID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
                BindGrid(true, true);
                SetUpdateMode(false);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        #region Edit Record
        /// <summary>
        /// To update details into database
        /// </summary>
        private void GetShocksExperiencedDetails()
        {
            objShocksExperiencedBLL = new ShocksExperiencedBLL();
            int ShocksExperiencedID = 0;

            if (ViewState["ShockID"] != null)
                ShocksExperiencedID = Convert.ToInt32(ViewState["ShockID"].ToString());

            objShocksExperienced = new ShocksExperiencedBO();
            objShocksExperienced = objShocksExperiencedBLL.GetShocksExperiencedById(ShocksExperiencedID);

            txtShocksExperienced.Text = objShocksExperienced.ShocksExperience;
        }
        #endregion

        #region Delete Record
        //private string DeleteShocksExperienced()
        //{
        //    objShocksExperiencedBLL = new ShocksExperiencedBLL();
        //    int ShocksExperiencedID = 0;
        //    int Result = 0;
        //    if (ViewState["ShockID"] != null)
        //        ShocksExperiencedID = Convert.ToInt32(ViewState["ShockID"].ToString());

        //    Result = objShocksExperiencedBLL.DeleteShocksExperienced(ShocksExperiencedID);
        //}
        #endregion
        
        #region Save Record
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            objShocksExperienced = new ShocksExperiencedBO();
            objShocksExperiencedBLL = new ShocksExperiencedBLL();

            string AlertMessage = string.Empty;
            string message = string.Empty;

            //Assignement
            objShocksExperienced.ShocksExperience = txtShocksExperienced.Text.Trim();

            if (ViewState["ShockID"] != null)
                objShocksExperienced.ShocksExperiencedID = Convert.ToInt32(ViewState["ShockID"].ToString());

            objShocksExperienced.IsDeleted = "False";

            //if (Session["USER_ID"] != null)
            objShocksExperienced.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
                     
            if (objShocksExperienced.ShocksExperiencedID < 1)
            {
                //If ShocksExperiencedID does Not exists then SaveShocksExperienced
                objShocksExperienced.ShocksExperiencedID = -1;//For New ShocksExperienced
                message = objShocksExperiencedBLL.AddShocksExperienced(objShocksExperienced);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    BindGrid(true, true);
                }
            }
            else
            {
                //If ShocksExperiencedID exists then UpdateShocksExperienced
                message = objShocksExperiencedBLL.UpdateShocksExperienced(objShocksExperienced); //For Updating ShocksExperienced
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, true);
                    SetUpdateMode(false);
                }
            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        #endregion
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
                ViewState["ShockID"] = "0";
            }
        }
    }
}