using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class SchoolDropReason : System.Web.UI.Page
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        SchoolDropReasonBO SchoolDropReasonBOobj = null;
        public int loginID = 1;
        public int count = 0;
        public int Reasonid = 1;
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
                //Retrieving UserName from Session
                string userName = (Session["userName"].ToString());
                string uID = Session["USER_ID"].ToString();
            }
            if (!IsPostBack)
            {
                Master.PageHeader = "School Drop Reason";
                ViewState["SchooldropreasonID"] = 0;
                BindGrid();
                //txtReasonDropped.Attributes.Add("onclick", "isDirty = 1;");
                txtReasonDropped.Attributes.Add("onchange", "setDirtyText();");
                

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_EDUCATION) == false)
                {

                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    gv_Details.Columns[3].Visible = false;
                    gv_Details.Columns[4].Visible = false;
                    gv_Details.Columns[5].Visible = false;
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
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
            gv_Details.DataSource = SchoolDropBLLObj.GetAllSchoolDropReason();
            gv_Details.DataBind();
        }
        /// <summary>
        /// calls inser method to save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;

            try
            {
                if (ViewState["SchooldropreasonID"].ToString() == "0")
                {

                    message = Insert();

                    AlertMessage = "alert('" + message + "');";

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        cleardetails();
                    }

                    //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('School Drop reason details added successfully');", true);
                }
                else
                {
                    string uID = Session["USER_ID"].ToString();
                    int reasonid = Convert.ToInt32(ViewState["SchooldropreasonID"]);
                    SchoolDropReasonBOobj = new SchoolDropReasonBO();
                    SchoolDropReasonBOobj.Description = txtDescription.Text.ToString();
                    SchoolDropReasonBOobj.Schooldropreason = txtReasonDropped.Text.ToString();
                    SchoolDropReasonBOobj.Createdby = Convert.ToInt32(uID); ;
                    SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
                    message = SchoolDropBLLObj.Update(SchoolDropReasonBOobj, reasonid);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        cleardetails();
                        SetUpdateMode(false);
                    }
                    //BindGrid(true, true);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('School Drop reason details updated successfully');", true);
                    //cleardetails();
                }
                BindGrid();
                //btnSave.Text = "Save";
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
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void cleardetails()
        {
            txtDescription.Text = string.Empty;
            txtReasonDropped.Text = string.Empty;
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

            SchoolDropReasonBOobj = new SchoolDropReasonBO();
            SchoolDropReasonBOobj.Schooldropreason = txtReasonDropped.Text.ToString();
            SchoolDropReasonBOobj.Description = txtDescription.Text.ToString();
            SchoolDropReasonBOobj.Createddate = System.DateTime.Now;
            SchoolDropReasonBOobj.Isdeleted = false;
            SchoolDropReasonBOobj.Createdby = Convert.ToInt32(uID);
            SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
            try
            {

                message = SchoolDropBLLObj.Insert(SchoolDropReasonBOobj);


            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.ToString();
                Response.Write(errorMsg);

            }
            finally
            {
                SchoolDropBLLObj = null;
            }
            return message;

        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Details_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["SchooldropreasonID"] = e.CommandArgument;

                GetSchoolDropDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                int reasonid = Convert.ToInt32(e.CommandArgument);
                SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
                message = SchoolDropBLLObj.Delete(Convert.ToInt32(e.CommandArgument));

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                cleardetails();
                SetUpdateMode(false);
                BindGrid();     
            }
        }
        /// <summary>
        ///To fetch details from database and assign to textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetSchoolDropDetails()
        {
            SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
            int SchooldropreasonID = 0;

            if (ViewState["SchooldropreasonID"] != null)
                SchooldropreasonID = Convert.ToInt32(ViewState["SchooldropreasonID"]);

            SchoolDropReasonBOobj = new SchoolDropReasonBO();
            SchoolDropReasonBOobj = SchoolDropBLLObj.GetschoolDropReasonbyID(SchooldropreasonID);
            txtReasonDropped.Text = SchoolDropReasonBOobj.Schooldropreason;
            txtDescription.Text = SchoolDropReasonBOobj.Description;
            //ConcernIDTextBox.Text = SchoolDropReasonBOobj.SchooldropreasonID.ToString();
        }
        /// <summary>
        ///To delete a row from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_Details_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int reasonid = Int32.Parse(gv_Details.DataKeys[e.RowIndex].Value.ToString());
            SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
            try
            {
                SchoolDropBLLObj.Delete(reasonid);
                BindGrid();
            }
            catch (Exception ee)
            {

                string errorMsg = ee.Message.ToString();
                Response.Write(errorMsg);
            }
            finally
            {
                SchoolDropBLLObj = null;
            }

            gv_Details.EditIndex = -1;
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

                string schooldropreasonID = ((Literal)gr.FindControl("litSchoolDropID")).Text;
                SchoolDropBLL SchoolDropBLLObj = new SchoolDropBLL();
                message = SchoolDropBLLObj.ObsoleteSchoolDrop(Convert.ToInt32(schooldropreasonID), Convert.ToString(chk.Checked));
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
        /// To display pageno in the grid
        /// </summary>
        protected void gv_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid();
            gv_Details.PageIndex = e.NewPageIndex;
            gv_Details.DataBind();
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
                ViewState["SchooldropreasonID"] = "0";
            }
        }
    }
}

