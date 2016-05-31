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
    public partial class Representation : System.Web.UI.Page
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
                Master.PageHeader = "Representation";
                ViewState["RepresentationID"] = 0;
                BindGrid();
                //txtRepresentation.Attributes.Add("onchange", "isDirty = 1;");
                txtRepresentation.Attributes.Add("onchange", "setDirtyText();");
                

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_REPRESENTATION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdRepresentation.Columns[2].Visible = false;
                    grdRepresentation.Columns[3].Visible = false;
                    grdRepresentation.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdRepresentation.Rows)
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
        private void BindGrid()
        {
            RepresentationBLL objRepresentationBLL = new RepresentationBLL();
            grdRepresentation.DataSource = objRepresentationBLL.GetRepresentation();
            grdRepresentation.DataBind();
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdRepresentation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["RepresentationID"] = e.CommandArgument;
                GetRepresentationDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                DeleteRepresentation(e.CommandArgument.ToString());
                SetUpdateMode(false);
                ClearData();
                BindGrid();
            }
        }
        /// <summary>
        /// To fetch details from the database and assign to textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>e
        private void GetRepresentationDetails()
        {
            RepresentationBLL objRepresentationBLL = new RepresentationBLL();
            RepresentationBO objRepresentationBO = new RepresentationBO();

            objRepresentationBO = objRepresentationBLL.GetRepresentationById(Convert.ToInt32(ViewState["RepresentationID"]));

            if (objRepresentationBO != null)
                txtRepresentation.Text = objRepresentationBO.RepresentationName;
        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>e
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdRepresentation.PageIndex = e.NewPageIndex;
            BindGrid();
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
                string representationID = ((Literal)gr.FindControl("litUserID")).Text;

                RepresentationBLL objRepresentationBLL = new RepresentationBLL();
                message = objRepresentationBLL.ObsoleteRepresentation(Convert.ToInt32(representationID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";

            RepresentationBLL objRepresentationBLL = new RepresentationBLL();
            if (Convert.ToInt32(ViewState["RepresentationID"]) == 0)
            {
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    RepresentationBO objRepresentationBO = new RepresentationBO();
                    objRepresentationBO.RepresentationName = txtRepresentation.Text.ToString().Trim();
                    objRepresentationBO.UserID = Convert.ToInt32(uID);
                    message = objRepresentationBLL.InsertRepresentation(objRepresentationBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objRepresentationBLL = null;
                }

                ClearData();
                BindGrid();
            }
            //edit the data in the textbox exiting in the Grid
            else if (Convert.ToInt32(ViewState["RepresentationID"]) > 0)
            {
                try
                {
                    RepresentationBO objRepresentationBO = new RepresentationBO();
                    objRepresentationBO.RepresentationName = txtRepresentation.Text.ToString().Trim();
                    objRepresentationBO.RepresentationID = Convert.ToInt32(ViewState["RepresentationID"]);
                    objRepresentationBO.UserID = Convert.ToInt32(Session["USER_ID"]);

                    message = objRepresentationBLL.UpdateRepresentation(objRepresentationBO);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data updated successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    objRepresentationBLL = null;
                }

                ClearData();
                BindGrid();
                SetUpdateMode(false);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }            
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
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
        protected void ClearData()
        {
            txtRepresentation.Text = string.Empty;
        }
        /// <summary>
        /// To delete a record in database  based on representationID
        /// </summary>
        private void DeleteRepresentation(string representationID)
        {
            RepresentationBLL objRepresentationBLL = new RepresentationBLL();

            string message = string.Empty;
            try
            {
                message = objRepresentationBLL.DeleteRepresentation(Convert.ToInt32(representationID));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                ViewState["RepresentationID"] = 0;
            }
        }
    }
}