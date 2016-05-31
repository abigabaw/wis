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
    public partial class Relationship : System.Web.UI.Page
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
                Master.PageHeader = "Relationship";
                ViewState["RELATIONSHIPID"] = 0;
                BindGrid(true, false);
                //txtrel.Attributes.Add("onchange","isDirty = 1;");
                txtrel.Attributes.Add("onchange", "setDirtyText();");
                
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    grdRelationship.Columns[2].Visible = false;
                    grdRelationship.Columns[3].Visible = false;
                    grdRelationship.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdRelationship.Rows)
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
            string AlertMessage = string.Empty;
            string message = string.Empty;

            RelationshipBO objRel = new RelationshipBO();
            objRel.RELATIONSHIPID = int.Parse(ViewState["RELATIONSHIPID"].ToString());
            objRel.RELATIONSHIP = txtrel.Text.Trim();
            objRel.UserID = Convert.ToInt32(Session["USER_ID"].ToString());
            RelationshipBLL objRelBLL = new RelationshipBLL();
            if (objRel.RELATIONSHIPID == 0)
            {
                message = objRelBLL.AddRelation(objRel);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Relationship added successfully');", true);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    //BindGrid(true, true);
                }
            }
            else
            {

                message = objRelBLL.UpdateRelationship(objRel);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('Relationship updated successfully');", true);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                    ClearDetails();
                    SetUpdateMode(false);
                }
                btn_Save.Text = "Save";
                ViewState["RELATIONSHIPID"] = 0;
            }

            //ClearDetails();
            //BindGrid(true, false);             
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
            RelationshipBO objRel = new RelationshipBO();
            RelationshipBLL objRElBLL = new RelationshipBLL();
            grdRelationship.DataSource = objRElBLL.GetALLRelationship();
            grdRelationship.DataBind();
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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtrel.Text = string.Empty;
            BindGrid(true, true);
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRelationship_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {

                ViewState["RELATIONSHIPID"] = e.CommandArgument;
                RelationshipBO objRel = null;
                RelationshipBLL objrelBLL = new RelationshipBLL();
                objRel = objrelBLL.GetRelationshipByID(Convert.ToInt32(ViewState["RELATIONSHIPID"]));

                if (objRel != null)
                {
                    txtrel.Text = objRel.RELATIONSHIP;
                }
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }

            else if (e.CommandName == "DeleteRow")
            {
               // ViewState["RELATIONSHIPID"] = e.CommandArgument;
                RelationshipBLL objrelBLL = new RelationshipBLL();
                message = objrelBLL.DeleteRelation(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(true);
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
                ViewState["RELATIONSHIPID"] = ((Literal)gr.FindControl("litRELATIONSHIPID")).Text;
                RelationshipBLL objrelBLL = new RelationshipBLL();
                message = objrelBLL.ObsoleteRelationship(Convert.ToInt32(ViewState["RELATIONSHIPID"]), Convert.ToString(chk.Checked));
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
        /// To display pageno in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRelationship_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRelationship.PageIndex = e.NewPageIndex;
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
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
                ViewState["RELATIONSHIPID"] = "0";
            }
        }
    }
}