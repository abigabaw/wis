using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class Vaccination : System.Web.UI.Page
    {
        DataTable dt = new System.Data.DataTable();
        
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
                Master.PageHeader = "Vaccination";
                ViewState["VACCINATIONID"] = 0;  // ViewState ID
                BindGrid(false, false); //Calling the Grid Data

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIAL) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                    grdVaccination.Columns[2].Visible = false;
                    grdVaccination.Columns[3].Visible = false;
                    grdVaccination.Columns[4].Visible = false;
                }
            }
        }

        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {
            VaccinationBLL VaccinationBLLobj = new VaccinationBLL();
            grdVaccination.DataSource = VaccinationBLLobj.GetALLVaccination();
            grdVaccination.DataBind();
        }

        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdVaccination_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            if (e.CommandName == "EditRow")
            {
                ViewState["VACCINATIONID"] = e.CommandArgument;
                GetVaccinationDetails();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                //ViewState["VACCINATIONID"] = e.CommandArgument;
                VaccinationBLL VaccinationBLLobj = new VaccinationBLL();
                message = VaccinationBLLobj.DeleteVaccination(Convert.ToInt32(e.CommandArgument));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data Deleted successfully";
                SetUpdateMode(true);
                BindGrid(false, true);
            }

            if (message != "")
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

            //{
            //    ViewState["VACCINATIONID"] = e.CommandArgument;
            //    DeleteVaccination();
            //    BindGrid(false, true);
            //}

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
               string VACCINATIONID = ((Literal)gr.FindControl("litVACCINATIONID")).Text;
               int VACCINATIONID_ = Convert.ToInt32(VACCINATIONID);
                VaccinationBLL VaccinationBLLobj = new VaccinationBLL();
                message = VaccinationBLLobj.Obsoletevaccination(VACCINATIONID_, Convert.ToString(chk.Checked));
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

        /// <summary>
        /// Get the details.
        /// </summary>
        private void GetVaccinationDetails()
        {
            VaccinationBLL VaccinationBLLobj = new VaccinationBLL();
            int VaccinationID = 0;

            if (ViewState["VACCINATIONID"] != null)
                VaccinationID = Convert.ToInt32(ViewState["VACCINATIONID"]);

            VaccinationBO Vaccinationobj = new VaccinationBO();
            Vaccinationobj = VaccinationBLLobj.GetVaccinationById(VaccinationID);

            VaccinationTextBox.Text = Vaccinationobj.VACCINATIONNAME;
            VACCINATIONIDTextBox.Text = Vaccinationobj.VACCINATIONID.ToString();
        }

        /// <summary>
        /// To change the page
        /// </summary>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdVaccination.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid(true, false);
        }

        /// <summary>
        /// Save the data into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string AlertMessage = string.Empty;
            string message = string.Empty;           
           
            if (VACCINATIONIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                VaccinationBLL VaccinationBLLobj = new VaccinationBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    VaccinationBO Vaccinationobj = new VaccinationBO();
                    Vaccinationobj.VACCINATIONNAME = VaccinationTextBox.Text.ToString().Trim();
                    Vaccinationobj.Createdby = Convert.ToInt32(uID);

                    VaccinationBLL BLLobj = new VaccinationBLL();
                    message = BLLobj.Insert(Vaccinationobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        ClearAll();
                        BindGrid(true, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    VaccinationBLLobj = null;
                }
            
            }
            //edit the data in the textbox exiting in the Grid
            else if (VACCINATIONIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                VaccinationBLL VaccinationBLLobj = new VaccinationBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    VaccinationBO Vaccinationobj = new VaccinationBO();
                    Vaccinationobj.VACCINATIONNAME = VaccinationTextBox.Text.ToString().Trim();
                    Vaccinationobj.VACCINATIONID = Convert.ToInt32(VACCINATIONIDTextBox.Text.ToString().Trim());
                    Vaccinationobj.Createdby = Convert.ToInt32(uID);

                    VaccinationBLL BLLobj = new VaccinationBLL();
                    message = BLLobj.EditVaccination(Vaccinationobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        ClearAll();
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
                    VaccinationBLLobj = null;
                }
            }
            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
        
        /// <summary>
        /// Call ClearDetails method to Clear data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }

        /// <summary>
        /// Clear the Input Fields and set Default data.
        /// </summary>
        private void ClearAll()
        {
            VaccinationTextBox.Text = string.Empty;
            VACCINATIONIDTextBox.Text = string.Empty;
            ViewState["VACCINATIONID"] = 0;
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
                ViewState["VACCINATIONID"] = "0";
            }
        }
    }
}