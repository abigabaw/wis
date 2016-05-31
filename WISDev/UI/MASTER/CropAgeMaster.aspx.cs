using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public partial class CropAgeMaster : System.Web.UI.Page
    {
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
                Master.PageHeader = "Crop Age";
                ViewState["CROPAGEID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
                CropAgeIDTextBox.Text = "0";
                //CropAgeTextBox.Attributes.Add("onchange","isDirty = 1;");
                CropAgeTextBox.Attributes.Add("onchange", "setDirtyText();");


                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_CROP) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdCropAge.Columns[2].Visible = false;
                    grdCropAge.Columns[3].Visible = false;
                    grdCropAge.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdCropAge.Rows)
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
        ///  save details to database 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string message = "";
            int count = 0;

            if (ViewState["CROPAGEID"].ToString() == "0")
            {
                CropAgeBLL CropAgeBLLOBJ = new CropAgeBLL();
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropAgeBO objCropAge = new CropAgeBO();
                    objCropAge.CROPAGE = CropAgeTextBox.Text.ToString().Trim();
                    objCropAge.UserID = Convert.ToInt32(uID);

                    CropAgeBLL CropAgeBLLLobj = new CropAgeBLL();
                    message = CropAgeBLLLobj.InsertCropAge(objCropAge);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data saved successfully";
                  

                    CropAgeIDTextBox.Text = "0";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    CropAgeBLLOBJ = null;
                }
            }
            else if (Convert.ToInt32(ViewState["CROPAGEID"].ToString()) > 0)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    CropAgeBO objCropAge = new CropAgeBO();
                    objCropAge.CROPAGE = CropAgeTextBox.Text.ToString().Trim();
                    objCropAge.CROPAGEID = Convert.ToInt32(ViewState["CROPAGEID"].ToString());
                    objCropAge.UserID = Convert.ToInt32(uID);

                    CropAgeBLL CropAgeBLLobj = new CropAgeBLL();
                    message = CropAgeBLLobj.EDITCROPAGE(objCropAge);

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
                    concernBLLOBJ = null;
                }
            }
            SetUpdateMode(false);
            ClearData();
            BindGrid(true, true);
        }
        /// <summary>
        /// calls clear method
        /// </summary>
        /// <returns></returns>
        protected void ClearButton_Click(object sender, EventArgs e)
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
            CropAgeIDTextBox.Text = string.Empty;
            CropAgeTextBox.Text = string.Empty;
            ViewState["CROPAGEID"] = 0;
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            CropAgeBLL CropAgeBLLobj = new CropAgeBLL();
            grdCropAge.DataSource = CropAgeBLLobj.GetCropAge();
            grdCropAge.DataBind();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCropAge_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CROPAGEID"] = e.CommandArgument;
                GetCropAgeDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;

                try
                {
                    CropAgeBLL CropAgeBLLobj = new CropAgeBLL();

                    message = CropAgeBLLobj.DeleteCropAge(Convert.ToInt32(e.CommandArgument));

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data deleted successfully";

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                    ClearData();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                SetUpdateMode(false);
                BindGrid(false, true);
            }
        }
        /// <summary>
        /// get the Grid value into textBox
        /// </summary>
        
        private void GetCropAgeDetails()
        {
            CropAgeBLL CropAgeBLLobj = new CropAgeBLL();
            int CROPAGEID = 0;

            if (ViewState["CROPAGEID"] != null)
                CROPAGEID = Convert.ToInt32(ViewState["CROPAGEID"]);

            CropAgeBO CropAgeObj = new CropAgeBO();
            CropAgeObj = CropAgeBLLobj.GetCropAgeById(CROPAGEID);

            CropAgeTextBox.Text = CropAgeObj.CROPAGE;
            CropAgeIDTextBox.Text = CropAgeObj.CROPAGEID.ToString();
            //int ConcernID_test = Convert.ToInt32(ConcernObj.ConcernID);
        }
        /// <summary>
        /// Change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCropAge.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
        protected void SetUpdateMode(bool updateMode)
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
                ViewState["CROPAGEID"] = "0";
                CropAgeIDTextBox.Text = "0";
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
                string cropAgeID = ((Literal)gr.FindControl("ltlCropAgeId")).Text;
                CropAgeBLL objCropAgeBLL = new CropAgeBLL();
                message = objCropAgeBLL.ObsoleteCropAge(Convert.ToInt32(cropAgeID), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data updated successfully";
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
                }

                BindGrid(true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}