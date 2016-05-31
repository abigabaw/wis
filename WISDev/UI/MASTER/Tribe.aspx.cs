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
    public partial class Tribe : System.Web.UI.Page
    {
        TribeBO TribeBOObj = null;
        TribeBLL TribeBLLObj = null;

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
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "Tribe";
                ViewState["TRIBEID"] = 0;
                BindGrid(false, false);
               // txtTribe.Attributes.Add("onchange", "isDirty = 1;");
                txtTribe.Attributes.Add("onchange", "setDirtyText();");
               

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gvTribe.Columns[3].Visible = false;
                    gvTribe.Columns[4].Visible = false;
                    gvTribe.Columns[5].Visible = false;
                    foreach (GridViewRow grRow in gvTribe.Rows)
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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTribe_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["TRIBEID"] = e.CommandArgument;
                GetTribeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["TRIBEID"] = e.CommandArgument;
                TribeBLL TribeBLLObj = new TribeBLL();
                message = TribeBLLObj.DeleteTribeById(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(false);
                BindGrid(false, true);
                ClearData();
            }

            else if (e.CommandName == "ViewROW")
            {
                ViewState["TRIBEID"] = e.CommandArgument;
                getClanDetails();
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        /// <summary>
        /// To get the details from database
        /// </summary>
        private void getClanDetails()
        {
            int TribeID = 0;

            if (ViewState["TRIBEID"] != null)
            {
                TribeID = Convert.ToInt32(ViewState["TRIBEID"]);
                Response.Redirect("Clans.aspx?TribeID=" + TribeID);
            }
        }

        /// <summary>
        /// To change the page
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            gvTribe.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            try
            {
                TribeBLL TribeBLLObj = new TribeBLL();
                gvTribe.DataSource = TribeBLLObj.FetchALLTribeList();
                gvTribe.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
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
                string TRIBEID = ((Literal)gr.FindControl("litTRIBEID")).Text;
                TribeBLL TribeBLLObj = new TribeBLL();
                message = TribeBLLObj.Obsoletetribe(Convert.ToInt32(TRIBEID), Convert.ToString(chk.Checked));
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
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTribeDetails()
        {
            TribeBOObj = new TribeBO();

            string message = "";

            string Tribe = string.Empty;
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            Tribe = txtTribe.Text.ToString().Trim();
            TribeBOObj.TribeName = Tribe;
            TribeBOObj.CreatedBy = Convert.ToInt32(uID);

            TribeBLLObj = new TribeBLL();

            try
            {
                message = TribeBLLObj.InsertIntoTribeMaster(TribeBOObj);
                txtTribe.Text = string.Empty;

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                BindGrid(true, true);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                TribeBLLObj = null;
            }
        }

        /// <summary>
        /// Edit Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTribeDetails()
        {
            TribeBLL TribeBLLObj = new TribeBLL();
            string message = "";

            try
            {
                TribeBO TribeBOObj = new TribeBO();

                if (ViewState["TRIBEID"] != null)
                    TribeBOObj.TribeID = Convert.ToInt32(ViewState["TRIBEID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                TribeBOObj.TribeName = txtTribe.Text.ToString().Trim();
                TribeBOObj.UpdatedBy = Convert.ToInt32(uID);

                message = TribeBLLObj.EDITTribe(TribeBOObj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                txtTribe.Text = string.Empty;
                SetUpdateMode(false);
                BindGrid(true, true);

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                TribeBLLObj = null;
            }
        }

        /// <summary>
        /// Get details from Database
        /// </summary>
        private void GetTribeDetails()
        {
            TribeBLL TribeBLLObj = new TribeBLL();
            int TribeID = 0;

            if (ViewState["TRIBEID"] != null)
                TribeID = Convert.ToInt32(ViewState["TRIBEID"]);

            TribeBO TribeBOObj = new TribeBO();
            TribeBOObj = TribeBLLObj.GetTribeById(TribeID);

            txtTribe.Text = TribeBOObj.TribeName;
        }

        /// <summary>
        /// Save and Update Data into Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveTribeDetails();
                }
                if (btnSave.Text == "Update")
                {
                    EditTribeDetails();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearData();

            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                txtTribe.Text = string.Empty;
                btnSave.Text = "Save";
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
                ViewState["TRIBEID"] = "0";
            }
        }

        /// <summary>
        /// Set link attributes to Branch link 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTribe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlAnchor lnkClan = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkClan");
                int CLANID = (int)DataBinder.Eval(e.Row.DataItem, "TRIBEID");

                lnkClan.Attributes.Add("onclick", "OpenClans(" + CLANID + ");");
            }
        }
    }

}