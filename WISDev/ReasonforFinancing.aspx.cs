using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
/**
 * 
 * @version		 Reason For Financing UI code 
 * @package		  Reason For Financing
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  15-05-2013
 * @Updated By
 * @Updated Date
 *  
 */

namespace WIS
{
    public partial class ReasonforFinancing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Reason for Financing";
                ViewState["FINANCEREASONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FINANCE) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdReason.Columns[2].Visible = false;
                    grdReason.Columns[3].Visible = false;
                    grdReason.Columns[4].Visible = false;
                }
            }
        }

        private void BindGrid(bool addRow, bool deleteRow)
        {
            ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
            grdReason.DataSource = BLLobj.GetReasonForFinance();
            grdReason.DataBind();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
             string AlertMessage = string.Empty;
            string message = string.Empty;

            if (ReasonIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
                    BOobj.FINANCEREASON = ReasonTextBox.Text;

                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    ReasonforFinancingBLL ReasonforFinancingBLLobj = new ReasonforFinancingBLL();
                    message = ReasonforFinancingBLLobj.Insert(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                    //BindGrid(true, true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Vaccination details added successfully');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    BLLobj = null;
                }
            }
            else if (ReasonIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
                    BOobj.FINANCEREASON = ReasonTextBox.Text;
                    BOobj.FINANCEREASONID = Convert.ToInt32(ReasonIDTextBox.Text);
                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    ReasonforFinancingBLL ReasonforFinancingBLLobj = new ReasonforFinancingBLL();
                    message = ReasonforFinancingBLLobj.Update(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                    //BindGrid(true, true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Vaccination details added successfully');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    BLLobj = null;
                }
            }

        }

        private void ClearAll()
        {
            ReasonIDTextBox.Text = string.Empty;
            ReasonTextBox.Text = string.Empty;
            ViewState["FINANCEREASONID"] = 0;
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        private void SetUpdateMode(bool updateMode)
        {
            if (updateMode)
            {
                SaveButton.Text = "Update";
                ClearButton.Text = "Cancel";
            }
            else
            {
                SaveButton.Text = "Save";
                ClearButton.Text = "Clear";
                ViewState["FINANCEREASONID"] = "0";
            }
        }

        protected void grdReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "EditRow")
            {
                ViewState["FINANCEREASONID"] = e.CommandArgument;
                GetReasonFoprFinanceDetail();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
                message = BLLobj.DeleteReasonForFinance(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";

                SetUpdateMode(false);
                BindGrid(false, true);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }

        private void GetReasonFoprFinanceDetail()
        {
            ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();
            int ReasonForFinanceID = 0;

            if (ViewState["FINANCEREASONID"] != null)
                ReasonForFinanceID = Convert.ToInt32(ViewState["FINANCEREASONID"]);

            ReasonforFinancingBO BOobj = new ReasonforFinancingBO();
            BOobj = BLLobj.GetReasonForFinanceID(ReasonForFinanceID);

            ReasonTextBox.Text = BOobj.FINANCEREASON;
            ReasonIDTextBox.Text = BOobj.FINANCEREASONID.ToString();
        }

        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {

        }

        protected void IsObsolete_CheckedChanged(Object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gr = (GridViewRow)chk.Parent.Parent;
                string financeReasonID = ((Literal)gr.FindControl("litReasonID")).Text;
                ReasonforFinancingBLL BLLobj = new ReasonforFinancingBLL();

                message = BLLobj.ObsoleteReasonForFin(Convert.ToInt32(financeReasonID), Convert.ToString(chk.Checked));
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
    }
}