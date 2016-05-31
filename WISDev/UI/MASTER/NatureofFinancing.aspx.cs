using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class NatureofFinancing : System.Web.UI.Page
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
                Master.PageHeader = "Nature of Financing";
                ViewState["FINANCENATUREID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                NatureTextBox.Attributes.Add("onchange", "setDirtyText();");

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_FINANCE) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdNature.Columns[2].Visible = false;
                    grdNature.Columns[3].Visible = false;
                    grdNature.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdNature.Rows)
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
            NatureofFinancingBLL BLLobj = new NatureofFinancingBLL();
            grdNature.DataSource = BLLobj.GetnatureAllfinance("");
            grdNature.DataBind();
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (NatureIDTextBox.Text.ToString().Trim() == string.Empty)
            {
               NatureofFinancingBLL  BLLobj = new NatureofFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    NatureofFinancingBO BOobj = new NatureofFinancingBO();
                    BOobj.FINANCENATURE = NatureTextBox.Text;

                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    NatureofFinancingBLL NatureofFinancingBLLobj = new NatureofFinancingBLL();
                    message = NatureofFinancingBLLobj.Insert(BOobj);

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
            else if (NatureIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                NatureofFinancingBLL BLLobj = new NatureofFinancingBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    NatureofFinancingBO BOobj = new NatureofFinancingBO();
                    BOobj.FINANCENATURE = NatureTextBox.Text;
                    BOobj.FINANCENATUREID = Convert.ToInt32(NatureIDTextBox.Text);
                    BOobj.CREATEDBY = Convert.ToInt32(uID);

                    NatureofFinancingBLL NatureofFinancingBLLobj = new NatureofFinancingBLL();
                    message = NatureofFinancingBLLobj.Update(BOobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                        SetUpdateMode(false);
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
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            NatureIDTextBox.Text = string.Empty;
            NatureTextBox.Text = string.Empty;
            ViewState["FINANCENATUREID"] = 0;
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// To change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdNature.PageIndex = e.NewPageIndex;
            BindGrid(true, true);

        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdNature_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;

            if (e.CommandName == "EditRow")
            {
                ViewState["FINANCENATUREID"] = e.CommandArgument;
                GetNatureFinanceDetail();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                NatureofFinancingBLL BLLobj = new NatureofFinancingBLL();
                message = BLLobj.DeleteNatureFinance(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data deleted successfully";
                ClearAll();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
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
                ViewState["FINANCENATUREID"] = "0";
            }
        }
        /// <summary>
        /// to fetch details from database
        /// </summary>
        private void GetNatureFinanceDetail()
        {
            NatureofFinancingBLL BLLobj = new NatureofFinancingBLL();
            int NatureFinanceID = 0;

            if (ViewState["FINANCENATUREID"] != null)
                NatureFinanceID = Convert.ToInt32(ViewState["FINANCENATUREID"]);

            NatureofFinancingBO BOobj = new NatureofFinancingBO();
            BOobj = BLLobj.GetNatureFinanceID(NatureFinanceID);

            NatureTextBox.Text = BOobj.FINANCENATURE;
            NatureIDTextBox.Text = BOobj.FINANCENATUREID.ToString();
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
                string financeNatureID = ((Literal)gr.FindControl("litNatureID")).Text;
                NatureofFinancingBLL NatureofFinancBLL = new NatureofFinancingBLL();

                message = NatureofFinancBLL.ObsoleteFcond(Convert.ToInt32(financeNatureID), Convert.ToString(chk.Checked));
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