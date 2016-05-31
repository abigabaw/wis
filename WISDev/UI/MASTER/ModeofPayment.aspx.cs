using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class ModeofPayment : System.Web.UI.Page
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
                Master.PageHeader = "Mode of Payment";
                ViewState["ModeofPaymentID"] = 0;
                BindGrid();
                txtModeofPaymentID.Text = "0";
                rdoTypeCash.Checked = true;
                txtModeofPayment.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_MODE_OF_PAYMENT) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdModeofPayment.Columns[2].Visible = false;
                    grdModeofPayment.Columns[3].Visible = false;
                    grdModeofPayment.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdModeofPayment.Rows)
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            ModeofPaymentBLL ModeofPaymentBLLOBJ = new ModeofPaymentBLL();
            ModeofPaymentBO ModeofPaymentBOObj = new ModeofPaymentBO();

            int uID = Convert.ToInt32(Session["USER_ID"].ToString());
            if (txtModeofPaymentID.Text.ToString().Trim() == "0")
            {
                try
                {
                    if (rdoTypeInKind.Checked)
                        ModeofPaymentBOObj.PaymentType = rdoTypeInKind.Text;
                    else
                        ModeofPaymentBOObj.PaymentType = rdoTypeCash.Text;

                    ModeofPaymentBOObj.ModeofPayment = txtModeofPayment.Text.ToString().Trim();
                    ModeofPaymentBOObj.UserID = uID;
                    message = ModeofPaymentBLLOBJ.InsertModeofPayment(ModeofPaymentBOObj);

                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";

                    if (message != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                        Clear();
                        BindGrid();
                        txtModeofPaymentID.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ModeofPaymentBLLOBJ = null;
                }
            }
            else
            {
                try
                {
                    if (rdoTypeInKind.Checked)
                        ModeofPaymentBOObj.PaymentType = rdoTypeInKind.Text;
                    else
                        ModeofPaymentBOObj.PaymentType = rdoTypeCash.Text;

                    ModeofPaymentBOObj.ModeofPayment = txtModeofPayment.Text.ToString().Trim();
                    ModeofPaymentBOObj.ModeofPaymentID = Convert.ToInt32(txtModeofPaymentID.Text.ToString().Trim());
                    ModeofPaymentBOObj.UserID = Convert.ToInt32(uID);

                    message = ModeofPaymentBLLOBJ.EDITMODEOFPAYMENT(ModeofPaymentBOObj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        Clear();
                        SetUpdateMode(false);
                        BindGrid();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    ModeofPaymentBLLOBJ = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            ModeofPaymentBLL ModeofPaymentBLLobj = new ModeofPaymentBLL();
            grdModeofPayment.DataSource = ModeofPaymentBLLobj.GetModeofPayment();
            grdModeofPayment.DataBind();
        }
        /// <summary>
        ///change Page in the Grid
        /// </summary>
       
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdModeofPayment.PageIndex = e.NewPageIndex;
            // Refresh the list
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
                ModeofPaymentBLL ModeofPaymentBLLobj = new ModeofPaymentBLL();

                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                string modeofPaymentID = ((Literal)gr.FindControl("litModeofPaymentID")).Text;

                message = ModeofPaymentBLLobj.ObsoleteModeofPayment(Convert.ToInt32(modeofPaymentID), Convert.ToString(chk.Checked));

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
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdModeofPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["ModeofPaymentID"] = e.CommandArgument;
                GetModeofPaymentIDByID();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ModeofPaymentBLL ModeofPaymentBLLobj = new ModeofPaymentBLL();

                message = ModeofPaymentBLLobj.DeleteModeofPayment(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                Clear();
                SetUpdateMode(false);
                BindGrid();

            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To fetch details from database based on ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetModeofPaymentIDByID()
        {
            ModeofPaymentBLL ModeofPaymentBLLobj = new ModeofPaymentBLL();
            int ModeofPaymentID = 0;

            if (ViewState["ModeofPaymentID"] != null)
                ModeofPaymentID = Convert.ToInt32(ViewState["ModeofPaymentID"]);

            ModeofPaymentBO ModeofPaymentObj = new ModeofPaymentBO();
            ModeofPaymentObj = ModeofPaymentBLLobj.GetModeofPaymentID(ModeofPaymentID);

            rdoTypeInKind.Checked = false;
            rdoTypeCash.Checked = false;
            if (ModeofPaymentObj.PaymentType.ToUpper() == "IN KIND")
                rdoTypeInKind.Checked = true;
            else
                rdoTypeCash.Checked = true;

            txtModeofPayment.Text = ModeofPaymentObj.ModeofPayment;
            txtModeofPaymentID.Text = ModeofPaymentObj.ModeofPaymentID.ToString();
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();

            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        public void Clear()
        {
            txtModeofPayment.Text = "";
            txtModeofPaymentID.Text = "";
            rdoTypeInKind.Checked = false;
            rdoTypeCash.Checked = true;
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
                ViewState["ModeofPaymentID"] = "0";
            }
        }
    }
}