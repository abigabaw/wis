using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class Consultant : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtAddress.Attributes.Add("maxlength", TxtAddress.MaxLength.ToString());

            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.Consultant;
            
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Consultants";

                ViewState["CONSULTANTID"] = 0;

                BindConsultantTypes();
                BindGrid(false, false);
                txtConsultantname.Attributes.Add("onchange", "setDirty();");
                btn_Save.Attributes.Add("onclick", "isDirty = 0;");
                btn_Clear.Attributes.Add("onclick", "isDirty = 0;");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    grdConsultant.Columns[grdConsultant.Columns.Count - 1].Visible = false;
                    grdConsultant.Columns[grdConsultant.Columns.Count - 2].Visible = false;
                    grdConsultant.Columns[grdConsultant.Columns.Count - 3].Visible = false;
                }
            }
        }
        /// <summary>
        /// method to bind values from database to  consultant type dropdownlist 
        /// </summary>
        private void BindConsultantTypes()
        {
            DrpConsultantType.DataSource = (new ConsultantTypeBLL()).GetConsultantType();
            DrpConsultantType.DataTextField = "ConsultantType";
            DrpConsultantType.DataValueField = "ConsultantTypeID";
            DrpConsultantType.DataBind();
        }
        /// <summary>
        /// Saves details to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string message = "";
            ConsultantBO objCon = new ConsultantBO();
            objCon.ConsultID = int.Parse(ViewState["CONSULTANTID"].ToString());
            objCon.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            objCon.ConsultName = txtConsultantname.Text.Trim();
            objCon.ConsultType = DrpConsultantType.SelectedItem.Value;
            objCon.EmailAddress = TxtEmailaddress.Text.Trim();
            string strMax = TxtAddress.Text.ToString().Trim();
            if (strMax.Trim().Length >= 399)
            {
                strMax = TxtAddress.Text.ToString().Trim().Substring(0, 399);
            }
            objCon.Address = strMax;
            objCon.ConNumber = TxtContactNum.Text.Trim();
            objCon.ConPerson = TxtContactPerson.Text.Trim();

            ConsultantBLL objConBLL = new ConsultantBLL();
            if (objCon.ConsultID == 0)
            {
                objCon.CreatedBy = Convert.ToInt32(Session["USER_ID"]);
                message = objConBLL.AddConsultant(objCon);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                }
            }
            else
            {
                objCon.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);
                objConBLL.UpdateConsultant(objCon);
                message = "Data updated successfully";
                SetUpdateMode(false);
            }

            ClearDetails();
            BindGrid(true, false);

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// Calls cleardetails method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            ConsultantBO objCon = new ConsultantBO();
            ConsultantBLL objConBLL = new ConsultantBLL();
            grdConsultant.DataSource = objConBLL.GetConsultant(Convert.ToInt32(Session["PROJECT_ID"]));
            grdConsultant.DataBind();

        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        protected void ClearDetails()
        {
            txtConsultantname.Text = string.Empty;
            TxtContactNum.Text = string.Empty;
            TxtContactPerson.Text = string.Empty;
            TxtEmailaddress.Text = string.Empty;
            TxtAddress.Text = string.Empty;
            DrpConsultantType.ClearSelection();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdConsultant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {

                ViewState["CONSULTANTID"] = e.CommandArgument;
                ConsultantBO objCon = new ConsultantBO();
                ConsultantBLL objConBLL = new ConsultantBLL();
                objCon = objConBLL.GetConsultantByID(Convert.ToInt32(ViewState["CONSULTANTID"]));

                if (objCon != null)
                {
                    txtConsultantname.Text = objCon.ConsultName;

                    DrpConsultantType.ClearSelection();
                    if (DrpConsultantType.Items.FindByValue(objCon.ConsultType) != null)
                        DrpConsultantType.Items.FindByValue(objCon.ConsultType).Selected = true;

                    TxtEmailaddress.Text = objCon.EmailAddress;
                    TxtAddress.Text = objCon.Address;
                    TxtContactNum.Text = objCon.ConNumber;
                    TxtContactPerson.Text = objCon.ConPerson;
                }
                SetUpdateMode(true);
            }

            else if (e.CommandName == "DeleteRow")
            {
                ConsultantBLL objConBLL = new ConsultantBLL();
                objConBLL.DeleteConsultant(Convert.ToInt32(e.CommandArgument));
                ClearDetails();
                SetUpdateMode(false);
                BindGrid(false, true);
            }
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
                ViewState["CONSULTANTID"] = "0";
            }
        }

        //protected void grdConsultant_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        int consultantID = (int)DataBinder.Eval(e.Row.DataItem, "ConsultID");
        //        CheckBox chkObsolete = (CheckBox)e.Row.FindControl("chkObsolete");
        //        string isDeleted = DataBinder.Eval(e.Row.DataItem, "IsDeleted").ToString();

        //        chkObsolete.Attributes.Add("onclick", string.Format("MakeObsolete(this,{0});", consultantID));
        //        if (isDeleted == "TRUE")
        //            chkObsolete.Checked = true;
        //            //chkObsolete.Attributes.Add("onclick", string.Format("RevokeObsolete({0});", consultantID));                
        //    }
        //}
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

                string consultantID = ((Literal)gr.FindControl("litConsultantID")).Text;

                ConsultantBLL objConBLL = new ConsultantBLL();
                objConBLL.ObsoleteConsultant(Convert.ToInt32(consultantID), Convert.ToString(chk.Checked));

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