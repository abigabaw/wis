using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

/**
 * 
 * @version		  Neighbour UI code 
 * @package		  Neighbour
 * @copyright	  Copyright @ 2013 - All rights reserved.
 * @author		  Rekha.M
 * @Created Date  24-04-2013
 * @Updated By   Mahalakshmi
 * @Updated Date  27-05-2013
 *  
 */
namespace WIS
{
    public partial class Neighbours : System.Web.UI.Page
    {
        #region Page Load
        /// <summary>
        /// Set Page header,Call BindGrid() to get the data from the database to fill tha gridview
        /// call GetDirection() to set the direction 
        /// to set the status of the Link button lnkLogout
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
            Page.Response.Cache.SetNoStore();
            CompSocioEconomyMenu1.HighlightMenu = CompSocioEconomyMenu.MenuValue.Neighbours;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.NeighbourDetails;

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
            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                {
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Neighbours";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ViewState["PAP_NEIGHBOURID"] = 0;  // ViewState ID
                BindGrid(); //Calling the Grid Data
                GetDirection();

                rdoBoundaryDisputesYes.Attributes.Add("onclick", "ShowHideBoundaryDisputes(1);");
                rdoBoundaryDisputesNo.Attributes.Add("onclick", "ShowHideBoundaryDisputes(0);");
                txtNeibrName.Attributes.Add("Onchange", "setDirtyText();");
                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_SOCIO_ECONOMIC) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdNeighbor.Columns[6].Visible = false;
                    grdNeighbor.Columns[7].Visible = false;
                }

               
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
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdNeighbor.Columns[6].Visible = false;
                grdNeighbor.Columns[7].Visible = false;
            }
        }
        #endregion

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
        /// to check the status of the Frozen/Approval / Decline / Pending
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdNeighbor.Columns[6].Visible = false;
                    grdNeighbor.Columns[7].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusNeighbours();
                }
            }
        }
        /// <summary>
        /// to get the status of the change request for the Neighbours
        /// </summary>
        public void ChangeRequestStatusNeighbours()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHNEH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        /// to check the status of the Approvar that Exist or Not
        /// </summary>

        public void checkApprovalExitOrNot()
        {
            
            #region Enable ChangeRequest Button
            
            StatusNeighbours.Text = "";
            StatusNeighbours.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HHNEH");
                lnkNeighbours.Attributes.Add("onclick", paramChangeRequest);
                lnkNeighbours.Visible = true;
            }
            else
            {
                lnkNeighbours.Visible = false;
            }
            #endregion
            getApprrequtStatusNeighbours();

        }
        /// <summary>
        /// to get the status of the Aprovar request for the neighbour
        /// </summary>
        public void getApprrequtStatusNeighbours()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HHNEH";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkNeighbours.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusNeighbours.Visible = true;
                    StatusNeighbours.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkNeighbours.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    StatusNeighbours.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkNeighbours.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdNeighbor.Columns[6].Visible = true;
                    grdNeighbor.Columns[7].Visible = true;
                    StatusNeighbours.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// to get the Directions
        /// </summary>
        private void GetDirection()
        {
           
            ddldirectionDropDownList.Items.Clear();
            ddldirectionDropDownList.Items.Add(new ListItem("-- Select --", "0"));
            ddldirectionDropDownList.Items.Add(new ListItem("East", "East"));
            ddldirectionDropDownList.Items.Add(new ListItem("North", "North"));
            ddldirectionDropDownList.Items.Add(new ListItem("North-East", "North-East"));
            ddldirectionDropDownList.Items.Add(new ListItem("North-West", "North-West"));
            ddldirectionDropDownList.Items.Add(new ListItem("South", "South"));
            ddldirectionDropDownList.Items.Add(new ListItem("South-East", "South-East"));
            ddldirectionDropDownList.Items.Add(new ListItem("South-West", "South-West"));
            ddldirectionDropDownList.Items.Add(new ListItem("West", "West"));
            ddldirectionDropDownList.SelectedIndex = 0;
        }

        #region Save / Clear / Update
        /// <summary>
        /// to save the data to the database and clear the data fields by calling  clearfields() methods
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = "";
            bool dataUpdated = false;

            ChangeRequestStatusNeighbours();
           if (txtNeighbrID.Text.ToString().Trim() == string.Empty)
            {
                neighbourBLL neighbourBLLobj= new neighbourBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    NeighbourBO Neighbourobj = new NeighbourBO();
                    
                    Neighbourobj.TRN_PAP_NEIGHBOURNAme1 = txtNeibrName.Text.ToString().Trim();
                    Neighbourobj.DIRECTION1 =ddldirectionDropDownList.SelectedItem.ToString().Trim();
                    if (RadioButton1.Checked == true)
                    {
                       
                        Neighbourobj.BOUNDARIESCONFIRMED1 = RadioButton1.Text.ToString();
                        //RadioButton2.Checked = false;
                    }
                    else
                    {
                        Neighbourobj.BOUNDARIESCONFIRMED1 = RadioButton2.Text.ToString();
                        //RadioButton1.Checked = false;
                    }

                    if (rdoBoundaryDisputesYes.Checked == true)
                    {

                        Neighbourobj.BOUNDARY_DISPUTE = RadioButton1.Text.ToUpper();
                        if(txtBoundaryDisputes.Text.Length>800)
                            Neighbourobj.DISPUTE_DETAILS = txtBoundaryDisputes.Text.Substring(0,800);
                        else
                            Neighbourobj.DISPUTE_DETAILS = txtBoundaryDisputes.Text;

                    }
                    else
                    {
                        Neighbourobj.BOUNDARY_DISPUTE = RadioButton2.Text.ToUpper();
                        Neighbourobj.DISPUTE_DETAILS = "";
                    }

                    Neighbourobj.CREATEDBY1 = Convert.ToInt32(uID);
                    Neighbourobj.HHID1 = Convert.ToInt32(hhid);
                    neighbourBLL BLLobj = new neighbourBLL();
                    message = BLLobj.Insert(Neighbourobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data saved successfully";
                        dataUpdated = true;
                    }

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                    if (dataUpdated)
                    {
                        projectFrozen();
                        getApprrequtStatusNeighbours();
                    }
                    BindGrid();
                    clearfields();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    neighbourBLLobj = null;
                }
            }
            else if (txtNeighbrID.Text.ToString().Trim() != string.Empty)
            {
                neighbourBLL neighbourBLLobj = new neighbourBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                   
                    NeighbourBO Neighbourobj = new NeighbourBO();

                    Neighbourobj.TRN_PAP_NEIGHBOURNAme1 = txtNeibrName.Text.ToString().Trim();
                    Neighbourobj.PAP_NEIGHBOURID1 = Convert.ToInt32(txtNeighbrID.Text.ToString().Trim());
                    Neighbourobj.DIRECTION1 = ddldirectionDropDownList.SelectedItem.ToString();
                    if (RadioButton1.Checked == true)
                    {

                        Neighbourobj.BOUNDARIESCONFIRMED1 = RadioButton1.Text.ToString();
                        //RadioButton2.Checked = false;
                    }
                    else
                    {
                        Neighbourobj.BOUNDARIESCONFIRMED1 = RadioButton2.Text.ToString();
                        //RadioButton1.Checked = false;
                    }
                    string radbtn = Neighbourobj.BOUNDARIESCONFIRMED1.ToString();

                    if (rdoBoundaryDisputesYes.Checked == true)
                    {
                        Neighbourobj.BOUNDARY_DISPUTE = RadioButton1.Text.ToUpper();
                        Neighbourobj.DISPUTE_DETAILS = txtBoundaryDisputes.Text;
                    }
                    else
                    {
                        Neighbourobj.BOUNDARY_DISPUTE = RadioButton2.Text.ToUpper();
                        Neighbourobj.DISPUTE_DETAILS = "";
                    }

                    Neighbourobj.BOUNDARIESCONFIRMED1 = radbtn.ToString();
                    Neighbourobj.CREATEDBY1 = Convert.ToInt32(uID);
                    Neighbourobj.HHID1 = Convert.ToInt32(hhid);

                    neighbourBLL BLLobj = new neighbourBLL();
                    message = BLLobj.EditNeighbr(Neighbourobj);

                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    {
                        message = "Data updated successfully";
                        dataUpdated = true;
                    }

                    if (message != "")
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);

                    if (dataUpdated)
                    {
                        projectFrozen();
                        getApprrequtStatusNeighbours();
                    }

                    SetUpdateMode(false);
                    BindGrid();
                    clearfields();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    neighbourBLLobj = null;
                }
            }
        }
        /// <summary>
        /// to clear the data 
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
        /// to clear the data 
        /// </summary>
        private void clearfields()
        {
            txtNeighbrID.Text = string.Empty;
            txtNeibrName.Text = string.Empty;
            ddldirectionDropDownList.Items.Clear();
            GetDirection();
            // RadioButton1.Checked = false;
            // RadioButton2.Checked = false;
            if (RadioButton1.Checked == true)
            {
                RadioButton1.Checked = false;
            }
            else if (RadioButton2.Checked == true)
            {
                RadioButton2.Checked = false;
            }

            if (rdoBoundaryDisputesYes.Checked == true)
            {
                rdoBoundaryDisputesYes.Checked = false;
            }
            else if (rdoBoundaryDisputesNo.Checked == true)
            {
                rdoBoundaryDisputesNo.Checked = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideBoundaryDisputes", "ShowHideBoundaryDisputes(0);", true);
            txtBoundaryDisputes.Text = "";
            this.Response.Clear();
            this.Response.Cache.SetNoStore();
        }
        /// <summary>
        /// to set the status of the Panel
        /// </summary>
        /// <param name="updateMode"></param>
        private void SetUpdateMode(bool updateMode)
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
                ViewState["PAP_NEIGHBOURID"] = "0";
            }
        }

        #endregion

        #region Data Bind / Page Index / Delete
        /// <summary>
        /// to get the data from the database and bind it to the GridView
        /// </summary>
        private void BindGrid()
        {
            neighbourBLL neighbourBLLobj = new neighbourBLL();
            if (grdNeighbor == null)
                grdNeighbor = new GridView();
            grdNeighbor.DataSource = neighbourBLLobj.GetneigbrDetails(Convert.ToInt32(Session["HH_ID"]));
            grdNeighbor.DataBind();
            
        }
        /// <summary>
        /// to set the pageindex for the gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdNeighbor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdNeighbor.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to Edit and Delete Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdNeighr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PAP_NEIGHBOURID"] = e.CommandArgument;
                GetNeighbor();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                int Pap_NeighbrID = Convert.ToInt32(e.CommandArgument);
                neighbourBLL neighbourBLLobj = new neighbourBLL();
                neighbourBLLobj.Delete(Pap_NeighbrID);
                clearfields();
                SetUpdateMode(false);
                BindGrid();                
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Deleted successfully');", true);
            }         
        }       
        /// <summary>
        /// to get the NeighBour Data
        /// </summary>
        private void GetNeighbor()
        {
            neighbourBLL neighbourBLLobj = new neighbourBLL();
            int Pap_NeighbrID = 0;

            if (ViewState["PAP_NEIGHBOURID"] != null)
                Pap_NeighbrID = Convert.ToInt32(ViewState["PAP_NEIGHBOURID"]);

            NeighbourBO Neighbourobj = new NeighbourBO();
            Neighbourobj = neighbourBLLobj.GetNeighbrById(Pap_NeighbrID);
            
            txtNeighbrID.Text = Neighbourobj.PAP_NEIGHBOURID1.ToString();
            txtNeibrName.Text = Neighbourobj.TRN_PAP_NEIGHBOURNAme1.ToString();
            ddldirectionDropDownList.ClearSelection();
            if (ddldirectionDropDownList.Items.FindByValue(Neighbourobj.DIRECTION1.ToString()) != null)
                ddldirectionDropDownList.Items.FindByValue(Neighbourobj.DIRECTION1.ToString()).Selected = true;
           string RadioButtn = Neighbourobj.BOUNDARIESCONFIRMED1.ToString();

           RadioButton1.Checked = false;
           RadioButton2.Checked = false;

           if (RadioButtn == "Yes")
            {
                RadioButton1.Checked = true;
            }
           else if (RadioButtn == "No")
            {
                RadioButton2.Checked = true;
            }

           rdoBoundaryDisputesYes.Checked = false;
           rdoBoundaryDisputesNo.Checked = false;

           if (Neighbourobj.BOUNDARY_DISPUTE.ToString().ToUpper() == "YES")
           {
               rdoBoundaryDisputesYes.Checked = true;
               txtBoundaryDisputes.Text = Neighbourobj.DISPUTE_DETAILS.ToString();
               ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideBoundaryDisputes", "ShowHideBoundaryDisputes(1);", true);
           }
           else if (Neighbourobj.BOUNDARY_DISPUTE.ToString().ToUpper() == "NO")
           {
               rdoBoundaryDisputesNo.Checked = true;
               txtBoundaryDisputes.Text = Neighbourobj.DISPUTE_DETAILS.ToString();
               ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowHideBoundaryDisputes", "ShowHideBoundaryDisputes(0);", true);
           }


        }
        #endregion
        /// <summary>
        /// to delete the User
        /// </summary>
        /// <param name="args"></param>

        [System.Web.Services.WebMethod (EnableSession = true ) ]
        
        public static void DeleteUser(string args)
        {
            int Pap_NeighbrID = Convert.ToInt32(args.Trim());
            neighbourBLL neighbourBLLobj = new neighbourBLL();
            neighbourBLLobj.Delete(Pap_NeighbrID);
            (new Neighbours()).BindGrid();
        }
    }
}