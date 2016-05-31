using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class Major_shocks : System.Web.UI.Page
    {
        ShocksExperiencedList objShocksExperiencedList;
        CopMechanismList objCopMechanismList;
        SocialSupportList objSocialSupportList;
        /// <summary>
        /// Set Page header,Call getApprrequtStatusMajorSchock() to get the data of the Approvar request status major shocks
        /// call GetTypeofshock() to get the data of the type of shock
        /// call Getcopingmechanism() to get the status of the coping mechanism from the database
        /// call Gethelpedmost() to get the data from the database of helped most
        /// call BindGrid() to get the data from the data base and bind it to the gridview
        /// to set the status of the link button lnkMajorSchock,lnkLogout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Request.QueryString["HHID"] != null)
                    Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                else
                    Session["HH_ID"] = null;
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.MajorShocks;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.ShockDetails;

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            if (Session["USER_ID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["PROJECT_ID"] == null)
            {
                Response.Redirect("~/UI/Project/ViewProjects.aspx");
            }
            if (Session["HH_ID"] == null)
            {
                Response.Redirect("~/UI/Compensation/PAPList.aspx");
            }
            if (!Page.IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Major Shocks";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                // To get type of shock from MST_SHOCKSEXPERIENCED
                GetTypeofshock();

                //To get coping mechanism from the table MST_COPING_MECHANISM
                Getcopingmechanism();

                //To get coping mechanism from the table MST_SUPPORT
                Gethelpedmost();
                
                
                getApprrequtStatusMajorSchock();//Added BY Ramu
                BindGrid(); //Calling the Grid Data
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    lnkMajorSchock.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdshocks.Columns[4].Visible = false;
                    grdshocks.Columns[5].Visible = false;
                }
                //Added BY Ramu
            }
            if (Mode == "Readonly")
            {
                CompSocioEconomyMenu1.Visible = false;
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkMajorSchock.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdshocks.Columns[4].Visible = false;
                grdshocks.Columns[5].Visible = false;
            }
        }

        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            stb.Append(btnSave.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
            
        }
        #region Frozen / Approval / Decline / Pending
        /// <summary>
        /// to check the status of the Frozen/Approval/Decline/Pending
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string Frozen = Session["FROZEN"].ToString();
                if (Frozen == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdshocks.Columns[4].Visible = false;
                    grdshocks.Columns[5].Visible = false;
                    checkApprovalExitOrNot();
                }
            }
        }
        /// <summary>
        /// to get the status of the Change request major shocks
        /// </summary>

        public void ChangeRequestStatusMajorSchock()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-MS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;
            objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to check the status of Approval Exist or not
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusMajorSchock.Text = "";
            StatusMajorSchock.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HH-MS");
                lnkMajorSchock.Attributes.Add("onclick", paramChangeRequest);
                lnkMajorSchock.Visible = true;
            }
            else
            {
                lnkMajorSchock.Visible = false;
            }
            #endregion
            getApprrequtStatusMajorSchock();
        }
        /// <summary>
        /// to get the status of the approvar major Shocks 
        /// </summary>

        public void getApprrequtStatusMajorSchock()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HH-MS";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkMajorSchock.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusMajorSchock.Visible = true;
                    StatusMajorSchock.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkMajorSchock.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusMajorSchock.Visible = false;
                    StatusMajorSchock.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkMajorSchock.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdshocks.Columns[4].Visible = true;
                    grdshocks.Columns[5].Visible = true;
                    StatusMajorSchock.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        //To get Helped most from the table MST_SUPPORT
        public void Gethelpedmost()
        {
            string SUPPORTID = string.Empty;

            if (helpedDropDownList.SelectedValue.ToString() == "")
            {

                SUPPORTID = "0";
                //msgSaveLabel.Text = null; // if value is select then error msg will display
            }
            else
            {
                // SHOCKID = typeofshockDropDownList.SelectedValue.ToString(); // get UI screen Value
                DataSet Ds = new DataSet();

                //  MajorshockBLL help_BLL = new MajorshockBLL(); // Function or object creation for next layer
                SocialSupportBLL help_BLL = new SocialSupportBLL();
                objSocialSupportList = new SocialSupportList();
                //  Ds = help_BLL.Gethelp(SUPPORTID); // data pass through object SHOCKID
                objSocialSupportList = (SocialSupportList)help_BLL.GetSchoolDetails();
                try
                {
                    // if (Ds.Tables[0].Rows.Count > 0) // Data from BLL
                    if (objSocialSupportList.Count > 0)
                    {
                        helpedDropDownList.DataSource = objSocialSupportList;
                        //  helpedDropDownList.DataTextField = Ds.Tables[0].Columns[1].ToString();
                        //helpedDropDownList.DataValueField = Ds.Tables[0].Columns[0].ToString();
                        helpedDropDownList.DataTextField = "SupportedBy";
                        helpedDropDownList.DataValueField = "SUPPORTEDBYID";
                        helpedDropDownList.DataBind();
                      //  helpedDropDownList.Items.Insert(0, "--Select--");
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                }
                finally     // set the finally class nothing but Empty the object 
                {
                    help_BLL = null;
                }
            }
        }


        //To get coping mechanism from the table MST_COPING_MECHANISM
        public void Getcopingmechanism()
        {
            string COP_MECHANISMID = string.Empty;

            if (copingmechDropDownList.SelectedValue.ToString() == "")
            {

                COP_MECHANISMID = "0";
                //msgSaveLabel.Text = null; // if value is select then error msg will display
            }
            else
            {
                // SHOCKID = typeofshockDropDownList.SelectedValue.ToString(); // get UI screen Value
                DataSet Ds = new DataSet();
                //  MajorshockBLL copmech_BLL = new MajorshockBLL(); // Function or object creation for next layer
                CopMechanismBLL copmech_BLL = new CopMechanismBLL();
                //Ds = copmech_BLL.GetCopMech(COP_MECHANISMID); // data pass through object SHOCKID

                objCopMechanismList = copmech_BLL.GetCopMechanism();
                try
                {
                    //if (Ds.Tables[0].Rows.Count > 0) // Data from BLL
                    if (objCopMechanismList.Count > 0)
                    {
                        copingmechDropDownList.DataSource = objCopMechanismList;
                        //copingmechDropDownList.DataTextField = Ds.Tables[0].Columns[1].ToString();
                        //copingmechDropDownList.DataValueField = Ds.Tables[0].Columns[0].ToString();
                        copingmechDropDownList.DataTextField = "CopMechanismName";
                        copingmechDropDownList.DataValueField = "CopMechanismID";
                        copingmechDropDownList.DataBind();
                      //  copingmechDropDownList.Items.Insert(0, "--Select--");
                    }
                }
                catch (Exception ee)
                {
                    throw ee;
                }
            finally     // set the finally class nothing but Empty the object 
                {
                    copmech_BLL = null;
                }
            }
        }

        // To get type of shock from MST_SHOCKSEXPERIENCED
        public void GetTypeofshock()
        {
            string SHOCKID = string.Empty;

            if (typeofshockDropDownList.SelectedValue.ToString() == "")
            {

                SHOCKID = "0";
                //msgSaveLabel.Text = null; // if value is select then error msg will display
            }
            else
            {
                // SHOCKID = typeofshockDropDownList.SelectedValue.ToString(); // get UI screen Value
                DataSet Ds = new DataSet();
                // MajorshockBLL Typeofshock_BLL = new MajorshockBLL(); // Function or object creation for next layer
                ShocksExperiencedBLL Typeofshock_BLL = new ShocksExperiencedBLL();
                objShocksExperiencedList = new ShocksExperiencedList();
                // Ds = Typeofshock_BLL.GetShocksExperienced(SHOCKID); // data pass through object SHOCKID
                objShocksExperiencedList = Typeofshock_BLL.GetShocksExperienced();
                try
                {
                    //if (Ds.Tables[0].Rows.Count > 0) // Data from BLL
                    if (objShocksExperiencedList.Count > 0)
                    {
                        typeofshockDropDownList.DataSource = objShocksExperiencedList;
                        //typeofshockDropDownList.DataTextField = Ds.Tables[0].Columns[1].ToString();
                        //typeofshockDropDownList.DataValueField = Ds.Tables[0].Columns[0].ToString();
                        typeofshockDropDownList.DataTextField = "ShocksExperience";
                        typeofshockDropDownList.DataValueField = "ShocksExperiencedID";
                        typeofshockDropDownList.DataBind();
                        //typeofshockDropDownList.Items.Insert(0, "--Select--");
                    }
                }
                catch (Exception ee)
                {
                   throw ee;
                }
            finally     // set the finally class nothing but Empty the object 
                {
                    Typeofshock_BLL = null;
                }
            }
        }
        /// <summary>
        /// to Edit and Delete the command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PAP_SHOCKID"] = e.CommandArgument;
                GetShock();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                ViewState["PAP_SHOCKID"] = e.CommandArgument;
                int PAP_SHOCKID1 = Convert.ToInt32(ViewState["PAP_SHOCKID"]);
                MajorshockBLL MajorshockBLLobj = new MajorshockBLL();
                MajorshockBLLobj.Delete(PAP_SHOCKID1);
                BindGrid();
                clearfields();
                SetUpdateMode(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }

        }
        /// <summary>
        /// to set the page index of the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdshocks.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid();
        }
        private void GetShock()
        {
            MajorshockBLL MajorshockBLLobj = new MajorshockBLL();
            int PAP_SHOCKID1 = 0;

            if (ViewState["PAP_SHOCKID"] != null)
                PAP_SHOCKID1 = Convert.ToInt32(ViewState["PAP_SHOCKID"]);

            MajorshockBO Majorshockobj = new MajorshockBO();
            Majorshockobj = MajorshockBLLobj.GetPapShochId(PAP_SHOCKID1);

            pap_shockidTextBox.Text = Majorshockobj.PAP_SHOCKID1.ToString();

            typeofshockDropDownList.ClearSelection();
            if (typeofshockDropDownList.Items.FindByValue(Majorshockobj.SHOCKID1.ToString()) != null)
                typeofshockDropDownList.Items.FindByValue(Majorshockobj.SHOCKID1.ToString()).Selected = true;

            helpedDropDownList.ClearSelection();
            if (helpedDropDownList.Items.FindByValue(Majorshockobj.SUPPORTID1.ToString()) != null)
                helpedDropDownList.Items.FindByValue(Majorshockobj.SUPPORTID1.ToString()).Selected = true;

            copingmechDropDownList.ClearSelection();
            if (copingmechDropDownList.Items.FindByValue(Majorshockobj.COP_MECHANISMID1.ToString()) != null)
                copingmechDropDownList.Items.FindByValue(Majorshockobj.COP_MECHANISMID1.ToString()).Selected = true;
        }
        /// <summary>
        /// to save the data to the database 
        /// and clear the data fields by calling clearfields() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string message = string.Empty;
            ChangeRequestStatusMajorSchock();

            MajorshockBLL MajorshockBLLobj = new MajorshockBLL();
         
            if (pap_shockidTextBox.Text == string.Empty)
            {

                try
                {
                  string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    MajorshockBO Majorshockobj = new MajorshockBO();

                    Majorshockobj.COP_MECHANISMID1 = Convert.ToInt32(copingmechDropDownList.SelectedValue.ToString());
                    Majorshockobj.SHOCKID1 = Convert.ToInt32(typeofshockDropDownList.SelectedValue.ToString());
                    Majorshockobj.SUPPORTID1 = Convert.ToInt32(helpedDropDownList.SelectedValue.ToString());
                    Majorshockobj.CREATEDBY1 = Convert.ToInt32(Session["USER_ID"]);
                    Majorshockobj.HHID1 = Convert.ToInt32(hhid);
                    MajorshockBLL BLLobj = new MajorshockBLL();
                    message = BLLobj.Insert(Majorshockobj);
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                    }
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data saved successfully');", true);
                    BindGrid();
                    clearfields();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    MajorshockBLLobj = null;
                }
            }

            //edit the data  existing in the Grid
            else
            {
                MajorshockBLL BLLobj = new MajorshockBLL();

                try
                {
                   string uID = Session["USER_ID"].ToString();
                    MajorshockBO Majorshockobj = new MajorshockBO();
                    Majorshockobj.PAP_SHOCKID1 = Convert.ToInt32(pap_shockidTextBox.Text);
                    Majorshockobj.SHOCKID1 = Convert.ToInt32(typeofshockDropDownList.SelectedValue);
                    Majorshockobj.COP_MECHANISMID1 = Convert.ToInt32(copingmechDropDownList.SelectedValue);
                    Majorshockobj.SUPPORTID1 = Convert.ToInt32(helpedDropDownList.SelectedValue);
                    Majorshockobj.CREATEDBY1 = Convert.ToInt32(Session["USER_ID"]);

                    message = BLLobj.EDITMshock(Majorshockobj);
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                    }
                    BindGrid();
                    clearfields(); 
                    SetUpdateMode(false);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data updated successfully');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    MajorshockBLLobj = null;
                }

            }
            string AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
            projectFrozen(); //Added BY Ramu
           
        }
        /// <summary>
        /// to get the data from the database and bind it to the gridView
        /// </summary>
        private void BindGrid()
        {
            MajorshockBLL MajorshockBLLobj = new MajorshockBLL();
            grdshocks.DataSource = MajorshockBLLobj.GetMshock(Convert.ToInt32(Session["HH_ID"]));
            grdshocks.DataBind();
        }
        /// <summary>
        /// to set the status of the Panel
        /// </summary>
        /// <param name="updateMode"></param>
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
                ViewState["PAP_SHOCKID"] = "0";
                pap_shockidTextBox.Text = string.Empty;
            }
        }
        /// <summary>
        /// to clear the fields by calling  clearfields() method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearfields();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
            }
        }
        /// <summary>
        /// to clear the data fields
        /// </summary>
        private void clearfields()
        {
            typeofshockDropDownList.ClearSelection();
            helpedDropDownList.ClearSelection();
            copingmechDropDownList.ClearSelection();
        }
    }
}



