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
    public partial class Clans : System.Web.UI.Page
    {
        ClansBO ClansBOObj = null;
        ClansBLL ClansBLLObj = null;
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
                ViewState["TRIBE_ID"] = Request.QueryString["id"];
                Master.PageHeader = "Clans";
                ViewState["CLANID"] = 0;
                BindGrid();
                GetClansname();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gvClans.Columns[2].Visible = false;
                    gvClans.Columns[3].Visible = false;
                    gvClans.Columns[4].Visible = false;
                }
            }
        }
        /// <summary>
        /// used to fetch details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetClansname()
        {
            TribeBO objtribe = (new TribeBLL()).GetTribeById(Convert.ToInt32(ViewState["TRIBE_ID"]));
            if (objtribe != null)
                Master.PageHeader = "Clans for " + objtribe.TribeName;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvClans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["CLANID"] = e.CommandArgument;
                GetClansDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                //ViewState["CLANID"] = e.CommandArgument;
                ClansBLL ClansBLLObj = new ClansBLL();
                message = ClansBLLObj.DeleteClansDetails(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                
                ClearData();
                SetUpdateMode(false);
                BindGrid();
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
                ViewState["CLANID"] = ((Literal)gr.FindControl("litCLANID")).Text;
                ClansBLL ClansBLLObj = new ClansBLL();
                message = ClansBLLObj.Obsoleteclan(Convert.ToInt32(ViewState["CLANID"]), Convert.ToString(chk.Checked));
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
        /// <summary>
        /// used to change page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            gvClans.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid()
        {
            try
            {
                ClansBLL ClansBLLObj = new ClansBLL();
                gvClans.DataSource = ClansBLLObj.FetchALLClansList(Convert.ToInt32(ViewState["TRIBE_ID"]));
                gvClans.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Used to save details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClansDetails()
        {
            ClansBOObj = new ClansBO();

            string message = "";
            int count = 0;

            string Clans = string.Empty;
            string uID = string.Empty;
            uID = Session["USER_ID"].ToString();

            ClansBLLObj = new ClansBLL();

            try
            {
                ClansBOObj.CLANNAME = txtClans.Text.ToString().Trim();
                ClansBOObj.TRIBEID = Convert.ToInt32(ViewState["TRIBE_ID"]);
                ClansBOObj.CreatedBy = Convert.ToInt32(uID);

                count = ClansBLLObj.InsertIntoClansMaster(ClansBOObj);
                txtClans.Text = string.Empty;

                if (count == -1)
                {
                    message = "Data saved successfully";
                    BindGrid();
                }
                else
                {
                    message = "Unable to save data";
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                ClansBLLObj = null;
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Used to update details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClansDetails()
        {
            ClansBLL ClansBLLObj = new ClansBLL();
            string message = "";

            try
            {
                ClansBO ClansBOObj = new ClansBO();

                if (ViewState["CLANID"] != null)
                    ClansBOObj.CLANID = Convert.ToInt32(ViewState["CLANID"].ToString());

                string uID = string.Empty;
                uID = Session["USER_ID"].ToString();

                ClansBOObj.CLANNAME = txtClans.Text.ToString().Trim();

                int TrbID = 0;
                TrbID = Convert.ToInt32(Request.QueryString["TribeID"]);
                ClansBOObj.TRIBEID = TrbID;

                ClansBOObj.UpdatedBy = Convert.ToInt32(uID);

                int count = ClansBLLObj.EDITClans(ClansBOObj);

                txtClans.Text = string.Empty;

                SetUpdateMode(false);
                if (count == -1)
                {
                    message = "Data updated successfully";
                    BindGrid();
                }
                else
                {
                    message = "Unable to update data";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                ClansBLLObj = null;
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// used to fetch details
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void GetClansDetails()
        {
            ClansBLL ClansBLLObj = new ClansBLL();
            int ClanID = 0;

            if (ViewState["CLANID"] != null)
                ClanID = Convert.ToInt32(ViewState["CLANID"]);

            ClansBO ClansBOObj = new ClansBO();
            ClansBOObj = ClansBLLObj.GetClansById(ClanID);

            txtClans.Text = ClansBOObj.CLANNAME;
        }
        /// <summary>
        /// calls save method
        /// </summary>
    
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveClansDetails();
                }
                if (btnSave.Text == "Update")
                {
                    EditClansDetails();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// calls cleardetails  method
        /// </summary>
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
        /// used to set text of button based on condition
        /// </summary>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["CLANID"] = "0";
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            txtClans.Text = string.Empty;
        }
    }
}