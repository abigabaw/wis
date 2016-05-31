using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;

namespace WIS
{
    public partial class OccupationMaster : System.Web.UI.Page
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
                Master.PageHeader = "Social Occupation";
                ViewState["OCCUPATIONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data
               // MainOccupationTextBox.Attributes.Add("onchange", "isDirty = 1;");
                MainOccupationTextBox.Attributes.Add("onchange", "setDirtyText();");
               
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdOccupation.Columns[2].Visible = false;
                    grdOccupation.Columns[3].Visible = false;
                    grdOccupation.Columns[4].Visible = false;
                    foreach (GridViewRow grRow in grdOccupation.Rows)
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
                ViewState["OCCUPATIONID"] = ((Literal)gr.FindControl("litOCCUPATIONID")).Text;
                OccupationBLL OccupationBLLOBJ = new OccupationBLL();
                message = OccupationBLLOBJ.ObsoleteOccupation(Convert.ToInt32(ViewState["OCCUPATIONID"]), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                AllClear();
                BindGrid(true, true);
                SetUpdateMode(false);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// To save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //int count = 0;
            string AlertMessage = string.Empty;
            string message = string.Empty;

            if (MainOccupationTextBox.Text.ToString().Trim() == string.Empty)
            {
                //errMsgMainOccupationLabel.Text = "Enter the Concern";
            }
            else if (MainOccupationIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                OccupationBLL OccupationBLLOBJ = new OccupationBLL();
                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OccupationBO objOccupation = new OccupationBO();
                    objOccupation.OCCUPATIONNAME = MainOccupationTextBox.Text.ToString().Trim();
                    objOccupation.UserID = Convert.ToInt32(uID);

                    OccupationBLL OccupationBLLobj = new OccupationBLL();
                    message = OccupationBLLobj.Insert(objOccupation);

                    //if (count == -1)
                    //{
                    //    msgSaveLabel.Text = "Data saved successfully"; //messageSaveLable
                    //    BindGrid(true, true);
                    //}
                    //else
                    //{
                    //    msgSaveLabel.Text = "Data not saved successfully";
                    //}

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        AllClear();
                        // ClearDetails();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    OccupationBLLOBJ = null;
                }
            }
            //edit the data in the textbox exiting in the Grid
            else if (MainOccupationIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                ConcernBLL concernBLLOBJ = new ConcernBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    OccupationBO objOccupation = new OccupationBO();
                    objOccupation.OCCUPATIONNAME = MainOccupationTextBox.Text.ToString().Trim();
                    objOccupation.OCCUPATIONID = Convert.ToInt32(MainOccupationIDTextBox.Text.ToString().Trim());
                    objOccupation.UserID = Convert.ToInt32(uID);

                    OccupationBLL OccupationBLLobj = new OccupationBLL();
                    message = OccupationBLLobj.EDITOccupation(objOccupation);

                    //if (count == -1)
                    //{
                    //    msgSaveLabel.Text = "Data saved successfully"; //messageSaveLable
                    //    BindGrid(true, true);
                    //}
                    //else
                    //{
                    //    msgSaveLabel.Text = "Data not saved successfully";
                    //}

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        // ClearDetails();
                        AllClear();
                        BindGrid(true, true);
                        SetUpdateMode(false);
                    }
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
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        /// <summary>
        /// Calls clear method
        /// </summary>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            MainOccupationTextBox.Text = string.Empty;
            //errMsgMainOccupationLabel.Text = string.Empty;
            AllClear();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void AllClear()
        {
            MainOccupationTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            OccupationBLL OccupationBLLobj = new OccupationBLL();
            grdOccupation.DataSource = OccupationBLLobj.GetALLOccupation();
            grdOccupation.DataBind();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdOccupation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["OCCUPATIONID"] = e.CommandArgument;
                GetOCCUPATIONDetails();
                SetUpdateMode(true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
            }
            else if (e.CommandName == "DeleteRow")
            {
               // ViewState["OCCUPATIONID"] = e.CommandArgument;
                OccupationBLL OccupationBLLobj = new OccupationBLL();
                message = OccupationBLLobj.DeleteOccupation(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                AllClear();
                BindGrid(true, true);
                SetUpdateMode(false);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
           
        }
        /// <summary>       
        /// Delete the data in the Grid
        /// </summary>
       
        private void DeleteOCCUPATION()
        {
            OccupationBLL OccupationBLLobj = new OccupationBLL();
            int OCCUPATIONID = 0;
            if (ViewState["OCCUPATIONID"] != null)
                OCCUPATIONID = Convert.ToInt32(ViewState["OCCUPATIONID"].ToString());

            //Result = OccupationBLLobj.DeleteOccupation(OCCUPATIONID);
        }
        /// <summary>       
        /// get the Grid value into textBox
        /// </summary>
       
        
        private void GetOCCUPATIONDetails()
        {
            OccupationBLL OccupationBLLobj = new OccupationBLL();
            int OCCUPATIONID = 0;

            if (ViewState["OCCUPATIONID"] != null)
                OCCUPATIONID = Convert.ToInt32(ViewState["OCCUPATIONID"]);

            OccupationBO OccupationObj = new OccupationBO();
            OccupationObj = OccupationBLLobj.GetOccupationId(OCCUPATIONID);

            MainOccupationTextBox.Text = OccupationObj.OCCUPATIONNAME;
            MainOccupationIDTextBox.Text = OccupationObj.OCCUPATIONID.ToString();
            //int ConcernID_test = Convert.ToInt32(ConcernObj.ConcernID);
        }

        /// <summary>       
        /// To change page in grid
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdOccupation.PageIndex = e.NewPageIndex;
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
                ViewState["NVR_ATT_SCH_REASONID"] = "0";
            }
        }
    }
}