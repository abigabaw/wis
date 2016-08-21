using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;


namespace WIS
{
    public partial class CulturProperties : System.Web.UI.Page
    {   /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.CultureProperties;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.CultureProperty;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Culture Properties";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ViewState["CULTURALPROPID"] = 0;
                CULTURALPROPIDtxtbx.Text = "";
                BindGrid();

                GetCulturalPropertyType();

                Linkpanel.Visible = false;

                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    lnkValuationCulturProperties.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    lnkUPloadDoc.Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = false;
                }
            }

            if (Mode == "Readonly")
            {
                ValuationMenu1.Visible = false;
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                ImageButton imgSearch = (ImageButton)HouseholdSummary1.FindControl("imgSearch");
                imgSearch.Visible = false;
                lnkValuationCulturProperties.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                lnkUPloadDoc.Visible = false;
                grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = false;
                grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = false;
            }
        }
        /// <summary>
        /// set the Default button and retuns the script.
        /// </summary>
        /// <returns></returns>
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
        ///  checks whether project is Frozen
        /// </summary>
        public void projectFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                if (Session["FROZEN"].ToString() == "Y")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusCulturProperties();
                }
                else
                {
                    lnkValuationCulturProperties.Visible = false;
                }
            }
        }
        /// <summary>
        ///  ccheckApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusCulturProperties.Text = "";
            StatusCulturProperties.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVCUP");
                lnkValuationCulturProperties.Attributes.Add("onclick", paramChangeRequest);
                lnkValuationCulturProperties.Visible = true;
            }
            else
            {
                lnkValuationCulturProperties.Visible = false;
            }
            #endregion
            getApprrequtStatusCulturProperties();

        }
        /// <summary>
        ///  ChangeRequestStatusCulturProperties
        /// </summary>
        public void ChangeRequestStatusCulturProperties()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVCUP";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        ///  getApprrequtStatusCulturProperties
        /// </summary>
        public void getApprrequtStatusCulturProperties()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVCUP";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkValuationCulturProperties.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = false;
                    StatusCulturProperties.Visible = true;
                    StatusCulturProperties.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkValuationCulturProperties.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = false;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = false;
                    StatusCulturProperties.Visible = false;
                    StatusCulturProperties.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkValuationCulturProperties.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 2].Visible = true;
                    grdCultureProperties.Columns[grdCultureProperties.Columns.Count - 4].Visible = true;
                    StatusCulturProperties.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// GetCulturalPropertyType
        /// </summary>
        private void GetCulturalPropertyType()
        {
            CulturePropertiesBLL BLLobj = new CulturePropertiesBLL();

            ddlCulturePropertyType.DataSource = BLLobj.GetCulturalPropertyType();
            ddlCulturePropertyType.DataTextField = "CULTUREPROPTYP";
            ddlCulturePropertyType.DataValueField = "CULTUREPROPTYPEID";
            ddlCulturePropertyType.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid()
        {
            CulturePropertiesBLL CulturePropertiesBLLobj = new CulturePropertiesBLL();
            grdCultureProperties.DataSource = CulturePropertiesBLLobj.GetCultureProp(Convert.ToInt32(Session["HH_ID"]));
            grdCultureProperties.DataBind();
        }
        /// <summary>
        /// to save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
          
            if (CULTURALPROPIDtxtbx.Text == string.Empty)
            {

                CulturePropertiesBLL BLLobj = new CulturePropertiesBLL();

                try
                {
                        
                        string uID = Session["USER_ID"].ToString();
                        string hhid = Session["HH_ID"].ToString();
                        CulturPropertiesBO CulturPropertiesobj = new CulturPropertiesBO();

                        CulturPropertiesobj.HHID = Convert.ToInt32(hhid);
                        CulturPropertiesobj.CULTUREPROPTYPEID = Convert.ToInt32(ddlCulturePropertyType.SelectedValue);
                        if (txtbxDescription.Text != string.Empty)
                        {
                            if (txtbxDescription.Text.Length > 250)
                                CulturPropertiesobj.CULTUREPROPDESCRIPTION = txtbxDescription.Text.Substring(0, 250);
                            else
                                CulturPropertiesobj.CULTUREPROPDESCRIPTION = txtbxDescription.Text;
                        }
                        else
                        {
                            CulturPropertiesobj.CULTUREPROPDESCRIPTION = "";
                        }
                        if (txtbxlength.Text.Trim() != "")
                            CulturPropertiesobj.CULT_DIMEN_LENGTH = Convert.ToInt32(txtbxlength.Text);
                        if (txtbxwidth.Text.Trim() != "")
                            CulturPropertiesobj.CULT_DIMEN_WIDTH = Convert.ToInt32(txtbxwidth.Text);
                        if (txtbxDepreciatedValue.Text.Trim() != "")
                            CulturPropertiesobj.CULT_DEPRECIATEDVALUE = Convert.ToDecimal(txtbxDepreciatedValue.Text);
                        if (txtbxValuationAmount.Text.Trim() != "")
                            CulturPropertiesobj.CULT_VALUATIONAMOUNT = Convert.ToDecimal(txtbxValuationAmount.Text);

                        CulturPropertiesobj.ISDELETED = "False";
                        CulturPropertiesobj.CREATEDBY = Convert.ToInt32(uID);

                        //if (photoFileUpload.HasFile)
                        //{
                        //    byte[] fileBytes = photoFileUpload.FileBytes;
                        //    CulturPropertiesobj.Photo = fileBytes;
                        //}

                        CulturePropertiesBLL CulturePropertiesBLLobj = new CulturePropertiesBLL();
                        count = CulturePropertiesBLLobj.Insert(CulturPropertiesobj);
                        ChangeRequestStatusCulturProperties();
                        projectFrozen();
                        ClearData();
                        BindGrid();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);
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
            else if (CULTURALPROPIDtxtbx.Text != string.Empty)
            {

                CulturePropertiesBLL BLLobj = new CulturePropertiesBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    CulturPropertiesBO CulturPropertiesobj = new CulturPropertiesBO();

                    CulturPropertiesobj.HHID = Convert.ToInt32(hhid);
                    CulturPropertiesobj.CULTURALPROPID = Convert.ToInt32(CULTURALPROPIDtxtbx.Text);

                    CulturPropertiesobj.CULTUREPROPTYPEID = Convert.ToInt32(ddlCulturePropertyType.SelectedValue);
                    if (txtbxDescription.Text.Length > 250)
                        CulturPropertiesobj.CULTUREPROPDESCRIPTION = txtbxDescription.Text.Substring(0, 250);
                    else
                        CulturPropertiesobj.CULTUREPROPDESCRIPTION = txtbxDescription.Text;

                    if (txtbxlength.Text.Trim() != "")
                        CulturPropertiesobj.CULT_DIMEN_LENGTH = Convert.ToInt32(txtbxlength.Text);
                    if (txtbxwidth.Text.Trim() != "")
                        CulturPropertiesobj.CULT_DIMEN_WIDTH = Convert.ToInt32(txtbxwidth.Text);
                    if (txtbxDepreciatedValue.Text.Trim() != "")
                        CulturPropertiesobj.CULT_DEPRECIATEDVALUE = Convert.ToDecimal(txtbxDepreciatedValue.Text);
                    if (txtbxValuationAmount.Text.Trim() != "")
                        CulturPropertiesobj.CULT_VALUATIONAMOUNT = Convert.ToDecimal(txtbxValuationAmount.Text);

                    CulturPropertiesobj.ISDELETED = "False";
                    CulturPropertiesobj.CREATEDBY = Convert.ToInt32(uID);

                    //if (photoFileUpload.HasFile)
                    //{
                    //    byte[] fileBytes = photoFileUpload.FileBytes;
                    //    CulturPropertiesobj.Photo = fileBytes;
                    //}

                    CulturePropertiesBLL CulturePropertiesBLLobj= new CulturePropertiesBLL();
                    count = CulturePropertiesBLLobj.Update(CulturPropertiesobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);
                    ClearData();
                    BindGrid();
                    SetUpdateMode(false);

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
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearData()
        {
            ddlCulturePropertyType.ClearSelection();
            CULTURALPROPIDtxtbx.Text = string.Empty;
            txtbxDescription.Text = string.Empty;
            txtbxlength.Text = string.Empty;
            txtbxwidth.Text = string.Empty;
            txtbxDepreciatedValue.Text = string.Empty;
            txtbxValuationAmount.Text = string.Empty;
            Linkpanel.Visible = false;
          
        }
        /// <summary>
        /// calls cleardata method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdateMode(false);
              
                Linkpanel.Visible = false;
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CultureProperties_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["CULTURALPROPID"] = e.CommandArgument;
                GetData();
                uploadPopWindow();
                Linkpanel.Visible = true;
              
                SetUpdateMode(true);
            }

        }
        /// <summary>
        /// uploadPopWindow
        /// </summary>
        public void uploadPopWindow()
        {
           //Add By Ramu For upload Document
                string userName = (Session["userName"].ToString());
                int userID = Convert.ToInt32(Session["USER_ID"]);
                int ProjectID = 0;
                string ProjectCode = string.Empty;
                string perStu = string.Empty;

                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                int HHID = 0;
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }

                if (Session["PROJECT_CODE"] != null)
                {
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }

                if (ViewState["CULTURALPROPID"] != null)
                {
                    perStu = ViewState["CULTURALPROPID"].ToString();
                }
                string PhotoModule = "PAPCP";

                string DocumentCode = "CULTPROP";

                string param = string.Format("OpenUploadDocumnet({0},{1},{2},'{3}','{4}','{5}');", ProjectID, HHID, userID, ProjectCode, DocumentCode, perStu);

                string paramView = string.Format("OpenUploadDocumnetlist({0},{1},{2},'{3}','{4}','{5}');", ProjectID, HHID, userID, ProjectCode, DocumentCode, perStu);

               
                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);
                             
                lnkUPloadDoc.Attributes.Add("onclick", param);

                lnkUPloadDoclist.Attributes.Add("onclick", paramView);

                //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
    }
        /// <summary>
        /// to change text of thebutton based on condition
        /// </summary>
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
                ViewState["CULTURALPROPID"] = "0";
            }
        }
        /// <summary>
        /// To fetch data and assign to textbox
        /// </summary>
        private void GetData()
        {

            CulturePropertiesBLL BLLobj = new CulturePropertiesBLL();
            int culTURALPROPID = 0;

            if (ViewState["CULTURALPROPID"] != null)
                culTURALPROPID = Convert.ToInt32(ViewState["CULTURALPROPID"]);

            CulturPropertiesBO BOobj = new CulturPropertiesBO();

            BOobj = BLLobj.GetData(culTURALPROPID);

            CULTURALPROPIDtxtbx.Text = BOobj.CULTURALPROPID.ToString();

            ddlCulturePropertyType.ClearSelection();
            if (ddlCulturePropertyType.Items.FindByValue(BOobj.CULTUREPROPTYPEID.ToString()) != null)
                ddlCulturePropertyType.Items.FindByValue(BOobj.CULTUREPROPTYPEID.ToString()).Selected = true;
          
           
            txtbxDescription.Text = BOobj.CULTUREPROPDESCRIPTION;
            if (BOobj.CULT_DIMEN_LENGTH.ToString() == "0")
            {
                txtbxlength.Text = "";
            }
            else
            {
                txtbxlength.Text = BOobj.CULT_DIMEN_LENGTH.ToString();
            }
            if (BOobj.CULT_DIMEN_WIDTH.ToString() == "0")
            {
                txtbxwidth.Text = "";
            }
            else
            {
                txtbxwidth.Text = BOobj.CULT_DIMEN_WIDTH.ToString();
            }
            if (BOobj.CULT_DEPRECIATEDVALUE.ToString() == "0")
            {
                txtbxwidth.Text = "";
            }
            else
            {
                txtbxDepreciatedValue.Text = BOobj.CULT_DEPRECIATEDVALUE.ToString();
            }
            if (BOobj.CULT_DEPRECIATEDVALUE.ToString() == "0")
            {
                txtbxwidth.Text = "";
            }
            else
            {
                txtbxDepreciatedValue.Text = BOobj.CULT_DEPRECIATEDVALUE.ToString();
            }
            if (BOobj.CULT_VALUATIONAMOUNT.ToString() == "0")
            {
                txtbxValuationAmount.Text = "";

            }
            else
            {
                txtbxValuationAmount.Text = BOobj.CULT_VALUATIONAMOUNT.ToString();
            }

        }
        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCultureProperties_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                System.Web.UI.HtmlControls.HtmlAnchor lnkMeeting = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkMeeting");

                int CULTURALPROPID = (int)DataBinder.Eval(e.Row.DataItem, "CULTURALPROPID");

                lnkMeeting.Attributes.Add("onclick", "OpenMeeting(" + CULTURALPROPID + ");");


                Literal litValuationAmount = (Literal)e.Row.FindControl("litValuationAmount");

                decimal ValuationAmount = (decimal)DataBinder.Eval(e.Row.DataItem, "CULT_VALUATIONAMOUNT");
               
                litValuationAmount.Text = UtilBO.CurrencyFormat(ValuationAmount);

                //Literal PAPFenceID = (Literal)e.Row.FindControl("litUserID");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = CULTURALPROPID.ToString();

                if (Session["PROJECT_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                }
                if (Session["HH_ID"] != null)
                {
                    HHID = Convert.ToInt32(Session["HH_ID"]);
                }
                if (Session["USER_ID"] != null)
                {
                    userID = Convert.ToInt32(Session["USER_ID"]);
                }
                if (Session["PROJECT_CODE"] != null)
                {
                    ProjectCode = Session["PROJECT_CODE"].ToString();
                }
                string PhotoModule = "PAPCP";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
              
            }
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdCultureProperties.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGrid();
        }

    }
}