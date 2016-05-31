using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS.UI.MASTER
{
    public partial class Parameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                //Master.PageHeader = "Parameter";
                ViewState["PARAMETERID"] = 0;
                BindGrid();
                GetAvailableOptions();
                txtParameter.Attributes.Add("onchange", "setDirtyText();");
                btnSave.Attributes.Add("onclick", "isDirty = 0;");
                //if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_Location) == false)
                //{
                //    btnSave.Visible = false;
                //    //btnClear.Visible = false;
                //    //btnShowAdd.Visible = false;
                //    //btnShowSearch.Visible = false;
                //    //pnlSearch.Visible = true;
                //    pnlParameterDetails.Visible = false;
                //    grdParameter.Columns[grdParameter.Columns.Count - 1].Visible = false;
                //    grdParameter.Columns[grdParameter.Columns.Count - 2].Visible = false;
                //    grdParameter.Columns[grdParameter.Columns.Count - 3].Visible = false;
                //}
            }
            //if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            //{
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                //                                               CreateStartupScript());
            //}

        }

        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
        //private string CreateStartupScript()
        //{
        //    StringBuilder stb = new StringBuilder();

        //    stb.Append("\n<script language=\"javascript\">\n<!--\n");

        //    stb.Append("var LOGIN_BUTTON_ID = \"");
        //    if (hfVisible.Value.Trim() == "1")
        //        stb.Append(btnParameterSearch.ClientID);
        //    else
        //        stb.Append(btnSave.ClientID);
        //    stb.Append("\";\n");

        //    stb.Append("-->\n</script>\n");

        //    return stb.ToString();
        //}

        /// <summary>
        /// to get district names
        /// </summary>
        /// <returns></returns>
        private void GetAvailableOptions()
        {
            ParameterBLL BLLobj = new ParameterBLL();

            ddlAvailableOptions.DataSource = BLLobj.GetOptionAvailable();
            ddlAvailableOptions.DataTextField = "AvailableOptions";
            ddlAvailableOptions.DataValueField = "AvailableOptionsID";
            ddlAvailableOptions.DataBind();

        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            int AvailableOptionID = Convert.ToInt32(ddlAvailableOptions.SelectedValue);
            ParameterBLL ParameterBLLobj = new ParameterBLL();
            grdParameter.DataSource = ParameterBLLobj.GetAllParameters(0);
            grdParameter.DataBind();

        }

        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <returns></returns>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdParameter.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// to get county names based on ID
        /// </summary>
        /// <returns></returns>
        private void GetParameterById()
        {
            ParameterBLL ParameterBLLobj = new ParameterBLL();

            ParameterBO ParameterBOobj = ParameterBLLobj.GetParameterById(Convert.ToInt32(ViewState["PARAMETERID"]));

            if (ParameterBOobj != null)
            {
                txtParameter.Text = ParameterBOobj.ParameterName;
                ddlAvailableOptions.SelectedValue = Convert.ToString(ParameterBOobj.AvailableOptionsID);
            }



            //ddlAvailableOptions.ClearSelection();
            //if (ddlAvailableOptions.Items.FindByText(ParameterBOobj.AvailableOptions.ToString()) != null)
            //    ddlAvailableOptions.Items.FindByText(ParameterBOobj.AvailableOptions.ToString()).Selected = true;
            ParameterBOobj = null;
            ParameterBLLobj = null;
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

                string PARAMETERID = ((Literal)gr.FindControl("litPARAMETERID")).Text;

                ParameterBLL ParameterBLLobj = new ParameterBLL();
                message = ParameterBLLobj.ObsoleteParameter(Convert.ToInt32(PARAMETERID), Convert.ToString(chk.Checked), 1);
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearData();
                BindGrid();
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Obsoleted", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// to save data to database
        /// </summary>
        /// <returns></returns>
        private void SaveParameter()
        {
            ParameterBLL ParameterBLLobj = new ParameterBLL();
            ParameterBO ParameterBOobj = new ParameterBO();

            string message = "";


            string uID = string.Empty;
            uID = "1"; // Session["USER_ID"].ToString();

            ParameterBOobj.AvailableOptionsID = Convert.ToInt32(ddlAvailableOptions.SelectedItem.Value);
            ParameterBOobj.ParameterName = txtParameter.Text.Trim();
            ParameterBOobj.CreatedBy = Convert.ToInt32(uID);

            try
            {
                message = ParameterBLLobj.AddParameter(ParameterBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data saved successfully";

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                ClearData();
                BindGrid();
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                ParameterBLLobj = null;
            }
        }

        /// <summary>
        /// to update data to database
        /// </summary>
        /// <returns></returns>
        private void UpdateParameter()
        {
            ParameterBLL ParameterBLLobj = new ParameterBLL();
            ParameterBO ParameterBOobj = new ParameterBO();
            string message = "";

            try
            {
                if (ViewState["PARAMETERID"] != null)
                    ParameterBOobj.ParameterID = Convert.ToInt32(ViewState["PARAMETERID"].ToString());

                string uID = string.Empty;
                uID = "1"; // Session["USER_ID"].ToString();

                ParameterBOobj.AvailableOptionsID = Convert.ToInt32(ddlAvailableOptions.SelectedItem.Value);
                ParameterBOobj.ParameterName = txtParameter.Text.Trim();
                ParameterBOobj.UpdatedBy = Convert.ToInt32(uID);

                message = ParameterBLLobj.UpdateParameter(ParameterBOobj);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";

                ClearData();
                SetUpdateMode(false);
                BindGrid();

                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ParameterBLLobj = null;
            }
        }

        /// <summary>
        /// calls save method
        /// </summary>
        /// <returns></returns>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (btnSave.Text == "Save")
                {
                    SaveParameter();
                }
                if (btnSave.Text == "Update")
                {
                    UpdateParameter();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// calls clear method
        /// </summary>
        /// <returns></returns>
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
            BindGrid();
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearData()
        {
            ddlAvailableOptions.ClearSelection();
            txtParameter.Text = string.Empty;

            SetUpdateMode(false);



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
                ViewState["PARAMETERID"] = "0";
            }
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdParameter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["PARAMETERID"] = e.CommandArgument;
                GetParameterById();
                SetUpdateMode(true);
                pnlParameterDetails.Visible = true;
            }
            else if (e.CommandName == "DeleteRow")
            {
                // ViewState["CDAPBUDGETID"] = e.CommandArgument;
                ParameterBLL ParameterBLLobj = new ParameterBLL();
                message = ParameterBLLobj.DeleteParameter(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                SetUpdateMode(false);
                ClearData();
                BindGrid();
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        //protected void ChangePage(object sender, GridViewPageEventArgs e)
        //{
        //    grdParameter.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}


        }
    }
