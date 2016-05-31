using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CopingMechanism : System.Web.UI.Page
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        CopMechanismBO objCopMechanism;
        CopMechanismBLL objCopMechanismBLL;
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
                Master.PageHeader = "Coping Mechanism";
                ViewState["CopMechanismID"] = 0;
                BindGrid(false, false);
                ClearDetails();
                //txtCopingMechanism.Attributes.Add("onchange", "isDirty = 1;");
                txtCopingMechanism.Attributes.Add("onchange", "setDirtyText();");
               

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCopMechansim.Columns[3].Visible = false;
                    grdCopMechansim.Columns[4].Visible = false;
                    grdCopMechansim.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in grdCopMechansim.Rows)
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
            objCopMechanismBLL = new CopMechanismBLL();
            objCopMechanism = new CopMechanismBO();

            objCopMechanism.CopMechanismName = string.Empty;
            objCopMechanism.CopMechanismID = 0;

            grdCopMechansim.DataSource = objCopMechanismBLL.GetALLCopMechanism();//(objCopMechanism);
            grdCopMechansim.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCopMechansim_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["CopMechanismID"] = e.CommandArgument;
                GetCopMechanismDetails();                
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                //ViewState["CopMechanismID"] = e.CommandArgument;
                CopMechanismBLL objCopMechanismBLL = new CopMechanismBLL();
                message = objCopMechanismBLL.DeleteCopMechanism(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                ClearDetails();
                SetUpdateMode(false);
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
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
                
                string CopMechanismID = ((Literal)gr.FindControl("litCopMechanismID")).Text;
                CopMechanismBLL objCopMechanismBLL = new CopMechanismBLL();
                message = objCopMechanismBLL.ObsoleteCopMechanism(Convert.ToInt32(CopMechanismID), Convert.ToString(chk.Checked));
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
        /// get the Grid value into textBox      
        /// </summary>   
        private void GetCopMechanismDetails()
        {
            objCopMechanismBLL = new CopMechanismBLL();
            int CopMechanismID = 0;

            if (ViewState["CopMechanismID"] != null)
                CopMechanismID = Convert.ToInt32(ViewState["CopMechanismID"].ToString());

            objCopMechanism = new CopMechanismBO();
            objCopMechanism = objCopMechanismBLL.GetCopMechanismById(CopMechanismID);

            txtCopingMechanism.Text = objCopMechanism.CopMechanismName;
        }
        /// <summary>
        ///calls clear method
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
            txtCopingMechanism.Text = "";
            ViewState["CopMechanismID"] = 0;
        }
        /// <summary>
        /// to insert data to database
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            objCopMechanism = new CopMechanismBO();
            objCopMechanismBLL = new CopMechanismBLL();

            objCopMechanism.CopMechanismName = txtCopingMechanism.Text.Trim();

            if (ViewState["CopMechanismID"] != null)
                objCopMechanism.CopMechanismID = Convert.ToInt32(ViewState["CopMechanismID"].ToString());

            objCopMechanism.IsDeleted = "False";

            if (Session["userId"] != null)
                objCopMechanism.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());
            else
                objCopMechanism.CreatedBy = 1; //Default Value

            if (objCopMechanism.CopMechanismID < 1)
            {
                //If CopMechanismID does Not exists then SaveCopMechanism
                objCopMechanism.CopMechanismID = -1;//For New CopMechanism
                message = objCopMechanismBLL.AddCopMechanism(objCopMechanism);

                AlertMessage = "alert('" + message + "');";

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    BindGrid(true, true);
                }
            }
            else
            {
                //If CopMechanismID exists then UpdateCopMechanism
                message = objCopMechanismBLL.UpdateCopMechanism(objCopMechanism); //For Updating CopMechanism

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    BindGrid(true, true);
                    SetUpdateMode(false);
                }
            }
            //ClearDetails();

            //BindGrid(true, false);
            //ViewState["CopMechanismID"] = "-1";

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// to get page numbers in grid
        /// </summary>
        protected void grdCopMechansim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCopMechansim.PageIndex = e.NewPageIndex;
            BindGrid(false, false);
            //            grdCopMechansim.DataBind();
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
                ViewState["CopMechanismID"] = "0";
            }
        }
    }
}