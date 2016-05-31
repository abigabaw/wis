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
    public partial class Livelihood : System.Web.UI.Page
    {
        LivelihoodBO objLivelihood = null;
      
        public int count = 0;
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
                Master.PageHeader = "Livelihood";
                ViewState["ITEMID"] = 0;
                BindGrid(false, false);
               // livelihoodTextBox.Attributes.Add("onchange", "isDirty = 1;");
                livelihoodTextBox.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdlivelihood.Columns[2].Visible = false;
                    grdlivelihood.Columns[3].Visible = false;
                    grdlivelihood.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdlivelihood.Rows)
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
            LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
            grdlivelihood.DataSource = objLivelihoodBLL.GetALLLivelihood();
            grdlivelihood.DataBind();
        }
        /// <summary>
        /// calls method save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            try
            {
                if (btnSave.Text == "Save")
                {

                    message = Insert();
                    
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        cleardetails();
                    }
                    BindGrid(true, true);
                }
                else
                {
                    string uID = Session["USER_ID"].ToString();
                   
                    int itemid = Convert.ToInt32(ViewState["ITEMID"]);
                    objLivelihood = new LivelihoodBO();
                    objLivelihood.ITEMNAME = livelihoodTextBox.Text.ToString();

                    objLivelihood.Createdby = Convert.ToInt32(uID);
                    LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
                    message = objLivelihoodBLL.UpdateLivelihood(objLivelihood, itemid);
                    
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        cleardetails();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
                    //BindGrid(true, true);
                    //cleardetails();
                }
                btnSave.Text = "Save";
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
                Response.Write(errorMsg);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string Insert()
        {
            string message = string.Empty;
            string uID = Session["USER_ID"].ToString();
            objLivelihood = new LivelihoodBO();
            
            objLivelihood.ITEMNAME = livelihoodTextBox.Text.ToString();
            objLivelihood.Createddate = System.DateTime.Now;
            objLivelihood.Isdeleted = "False";
            objLivelihood.Createdby = Convert.ToInt32(Session["USER_ID"]);
            LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
            try
            {
                message = objLivelihoodBLL.AddLivelihood(objLivelihood);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    cleardetails();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
            }
            finally
            {
                objLivelihoodBLL = null;
            }
            return message;
        }

        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            cleardetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void cleardetails()
        {
            livelihoodTextBox.Text = string.Empty;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdlivelihood_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {                        
                ViewState["ITEMID"] = e.CommandArgument;
                int itemid = Convert.ToInt32(ViewState["ITEMID"]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                if (row != null)
                {
                    livelihoodTextBox.Text = row.Cells[1].Text.ToString();
                    
                }
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
                message = objLivelihoodBLL.DeleteLivelihood(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                cleardetails();
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
                ViewState["ITEMID"] = ((Literal)gr.FindControl("litITEMID")).Text;
                LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
                message = objLivelihoodBLL.ObsoleteLivelihood(Convert.ToInt32(ViewState["ITEMID"]), Convert.ToString(chk.Checked));
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

        protected void grdlivelihood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// To delete row based on ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdlivelihood_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int itemid = Int32.Parse(grdlivelihood.DataKeys[e.RowIndex].Value.ToString());
            LivelihoodBLL objLivelihoodBLL = new LivelihoodBLL();
            try
            {
                objLivelihoodBLL.DeleteLivelihood(itemid);
                BindGrid(true, true);
            }
            catch (Exception ee)
            {
                string errorMsg = ee.Message.ToString();
            }
            finally
            {
                objLivelihoodBLL = null;
            }

            grdlivelihood.EditIndex = -1;
            BindGrid(true, true);
        }
        /// <summary>
        /// To display pageno in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdlivelihood_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdlivelihood.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
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
                ViewState["ITEMID"] = "0";
            }
        }
    }
}