using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class GOUAllowance : System.Web.UI.Page
    {
        GOUAllowanceBO GOUAllowanceBOobj=null;
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
                Master.PageHeader = "GOU Allowance";
                ViewState["GOUALLOWANCECATEGORYID"] = 0;
                BindGrid();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.GOUAllowance) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gv_Details.Columns[gv_Details.Columns.Count - 1].Visible = false;
                    gv_Details.Columns[gv_Details.Columns.Count - 2].Visible = false;
                    gv_Details.Columns[gv_Details.Columns.Count - 3].Visible = false;
                    foreach (GridViewRow grRow in gv_Details.Rows)
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
                if (ViewState["GOUALLOWANCECATEGORYID"].ToString() == "0")
                {

                    message = Insert();


                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        cleardetails();
                    }

 
                }
                else
                {
                    string uID = Session["USER_ID"].ToString();
                    int reasonid = Convert.ToInt32(ViewState["GOUALLOWANCECATEGORYID"]);
                    GOUAllowanceBOobj = new GOUAllowanceBO();
                    GOUAllowanceBOobj.GOUAllowanceCategory = txtGOUAllowanceCat.Text.ToString();
                    GOUAllowanceBOobj.GOUAllowanceValue = Convert.ToDecimal(txtGOUAllowanceVal.Text);
                    GOUAllowanceBOobj.Createdby = Convert.ToInt32(uID); ;
                    GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
                    message = GOUAllowanceBLLObj.Update(GOUAllowanceBOobj, reasonid);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        cleardetails();
                        SetUpdateMode(false);
                    }
                    
                }
                BindGrid();
              
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
                Response.Write(errorMsg);
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
            GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
            gv_Details.DataSource = GOUAllowanceBLLObj.GetAllGouAllowance();
            gv_Details.DataBind();
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void cleardetails()
        {
            txtGOUAllowanceCat.Text = string.Empty;
            txtGOUAllowanceVal.Text = string.Empty;
        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string Insert()
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            string uID = Session["USER_ID"].ToString();

            GOUAllowanceBOobj = new GOUAllowanceBO();
            GOUAllowanceBOobj.GOUAllowanceCategory = txtGOUAllowanceCat.Text.ToString();
            GOUAllowanceBOobj.GOUAllowanceValue = Convert.ToDecimal(txtGOUAllowanceVal.Text);
            GOUAllowanceBOobj.Createddate = System.DateTime.Now;
            GOUAllowanceBOobj.Isdeleted = false;
            GOUAllowanceBOobj.Createdby = Convert.ToInt32(uID);
            GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
            try
            {

                message = GOUAllowanceBLLObj.Insert(GOUAllowanceBOobj);


            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
                Response.Write(errorMsg);

            }
            finally
            {
                GOUAllowanceBLLObj = null;
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
                ViewState["GOUALLOWANCECATEGORYID"] = "0";
            }
        }
        /// <summary>
        /// to link to other page on click of link  in grid
        /// </summary>
        /// <returns></returns>
        protected void gv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["GOUALLOWANCECATEGORYID"] = e.CommandArgument;

                GetGouAllowanceDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                int reasonid = Convert.ToInt32(e.CommandArgument);
                GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
                message = GOUAllowanceBLLObj.Delete(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                cleardetails();
                SetUpdateMode(false);
                BindGrid();
            }
        }
        /// <summary>
        /// To fetch details
        /// </summary>
        private void GetGouAllowanceDetails()
        {
            GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
            int GOUALLOWANCECATEGORYID = 0;

            if (ViewState["GOUALLOWANCECATEGORYID"] != null)
                GOUALLOWANCECATEGORYID = Convert.ToInt32(ViewState["GOUALLOWANCECATEGORYID"]);

            GOUAllowanceBOobj = new GOUAllowanceBO();
            GOUAllowanceBOobj = GOUAllowanceBLLObj.GetGouAllowancebyID(GOUALLOWANCECATEGORYID);
            txtGOUAllowanceCat.Text = GOUAllowanceBOobj.GOUAllowanceCategory;
            txtGOUAllowanceVal.Text = GOUAllowanceBOobj.GOUAllowanceValue.ToString();
            
        }
        /// <summary>
        /// To change pageno in grid
        /// </summary>
        protected void gv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Details.PageIndex = e.NewPageIndex;
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

                string GOUALLOWANCECATEGORYID = ((Literal)gr.FindControl("litSchoolDropID")).Text;
                GOUAllowanceBLL GOUAllowanceBLLObj = new GOUAllowanceBLL();
                message = GOUAllowanceBLLObj.Obsolete(Convert.ToInt32(GOUALLOWANCECATEGORYID), Convert.ToString(chk.Checked));
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

       
    }
}