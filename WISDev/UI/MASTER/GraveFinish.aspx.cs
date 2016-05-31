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
    public partial class GraveFinish : System.Web.UI.Page
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
                Master.PageHeader = "Grave Finish";
                ViewState["GRV_FINISHID"] = 0;
                BindGrid(false, false);
                //txtGrave.Attributes.Add("onchange", "isDirty = 1;");
                txtGrave.Attributes.Add("onchange", "setDirtyText();");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_STRUCTURE) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    grdGraveFinish.Columns[3].Visible = false;
                    grdGraveFinish.Columns[4].Visible = false;
                    grdGraveFinish.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdGraveFinish.Rows)
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
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            string AlertMessage = string.Empty;

            GraveFinishBO objGF = new GraveFinishBO();

            objGF.GraveFinishID = int.Parse(ViewState["GRV_FINISHID"].ToString());
            objGF.GraveFinishType = txtGrave.Text.Trim();
            objGF.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

            GraveFinishBLL objGFBLL = new GraveFinishBLL();

            if (objGF.GraveFinishID == 0)
            {
                message = objGFBLL.AddGrave(objGF);
                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data added successfully";
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            else
            {
                message = objGFBLL.UpdateGrave(objGF);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    SetUpdateMode(false);
                    ClearDetails();
                    BindGrid(true, false);
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
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

                string GRV_FINISHID = ((Literal)gr.FindControl("litGraveFinishID")).Text;
                GraveFinishBLL objGFBLL = new GraveFinishBLL();

                message = objGFBLL.ObsoleteGraveFinish(Convert.ToInt32(GRV_FINISHID), Convert.ToString(chk.Checked));
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            GraveFinishBO objGF = new GraveFinishBO();
                GraveFinishBLL objGFBLL = new GraveFinishBLL();
                grdGraveFinish.DataSource = objGFBLL.GetAllGraveFinish();
                grdGraveFinish.DataBind();        
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>

        protected void ClearDetails()
        {
            txtGrave.Text = string.Empty;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void grdGraveFinish_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {

                ViewState["GRV_FINISHID"] = e.CommandArgument;
                GraveFinishBO objGF = new GraveFinishBO();
                GraveFinishBLL objGFBLL = new GraveFinishBLL();
                objGF = objGFBLL.GetGraveByID(Convert.ToInt32(ViewState["GRV_FINISHID"]));

                if (objGF != null)
                {
                    txtGrave.Text = objGF.GraveFinishType;
                }

                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;

                GraveFinishBLL objGFBLL = new GraveFinishBLL();
                
                message = objGFBLL.DeleteGrave(Convert.ToInt32(e.CommandArgument));
                
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                
                ClearDetails();
                SetUpdateMode(false);
                BindGrid(false, true);
                
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
        }
        /// <summary>
        /// To display pageno in grid
        /// </summary>
        protected void grdGraveFinish_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdGraveFinish.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["GRV_FINISHID"] = "0";
            }
        }       
    }
}