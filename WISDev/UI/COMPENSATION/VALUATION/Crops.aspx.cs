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
    public partial class Crops : System.Web.UI.Page
    {
        /// <summary>
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.Crops;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.CropDetails;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Crops";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }
                ViewState["PAP_CROPID"] = 0;  // ViewState ID
                txtbxPAP_CROPID.Text = string.Empty;
                BindGrid(false, false); //Calling the Grid Data

                GetCropName();
                GetCropType();
                GetCropDescription();

                txtbxQuantity.Attributes.Add("onblur", "CalculateAmount();");
                txtbxCropRate.Attributes.Add("onblur", "CalculateAmount();");
                txtbxCropvaluation.Attributes.Add("onKeyDown", "doCheck();");
                //txtbxCropRate.Attributes.Add("onKeyDown", "doCheck();");

                ddlCropName.Attributes.Add("onchange", "ddlCropName_IndexChanged(this);");
                ddlCropDescription.Attributes.Add("onchange", "ddlCropName_IndexChanged(this);");
                popupData();
                //lnkViewPhoto.Visible = false;

                txtbxQuantity.Attributes.Add("onchange", "setDirtyText();");
                txtbxCropRate.Attributes.Add("onchange", "setDirtyText();");


                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = false;
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
                lnkValuationCrop.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = false;
                grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = false;
                grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = false;
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
                    grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = false;
                    getApprrequtStatusValuationCrop();
                    checkApprovalExitOrNot();
                }
                else
                {
                    lnkValuationCrop.Visible = false;
                }
            }
        }
        /// <summary>
        ///  checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusValuationCrop.Text = "";
            StatusValuationCrop.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestApprovalHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);
            //string pageCode = "HH-LU";

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HV-CO");
                lnkValuationCrop.Attributes.Add("onclick", paramChangeRequest);
                lnkValuationCrop.Visible = true;
            }
            else
            {
                lnkValuationCrop.Visible = false;
            }
            #endregion
            getApprrequtStatusValuationCrop();

        }
        /// <summary>
        ///  ChangeRequestStatusValuationCrop
        /// </summary>
        public void ChangeRequestStatusValuationCrop()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HV-CO";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        ///  getApprrequtStatusValuationCropn
        /// </summary>
        public void getApprrequtStatusValuationCrop()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HV-CO";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestApprovalHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkValuationCrop.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = false;
                    StatusValuationCrop.Visible = true;
                    StatusValuationCrop.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkValuationCrop.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = false;
                    grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = false;
                    StatusValuationCrop.Visible = false;
                    StatusValuationCrop.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkValuationCrop.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdCrops.Columns[grdCrops.Columns.Count - 1].Visible = true;
                    grdCrops.Columns[grdCrops.Columns.Count - 2].Visible = true;
                    grdCrops.Columns[grdCrops.Columns.Count - 4].Visible = true;
                    StatusValuationCrop.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion

        #region WebService
        [System.Web.Services.WebMethod]
        public static string[] GetUnitName(string Units)
        {
            string[] Ids = Units.Split('|');
            int cropID = Convert.ToInt32(Ids[0]);
            int CropDesID = Convert.ToInt32(Ids[1]);
            CropNameBLL objCropNameBLL = new CropNameBLL();
            CropNameBO objCropname = objCropNameBLL.GetCropNameById(cropID);
            string[] arrCrop = { "", "" };
            string unitMeasureName = "";
            string cropRate = "";

            CropRateBLL objCropRateBLL = new CropRateBLL();
            CropRateBO objCropRateBO = objCropRateBLL.GetCropRateByDistrict(cropID, CropDesID, Convert.ToInt32(HttpContext.Current.Session["HH_ID"]));

            if (objCropname != null)
            {
                unitMeasureName = objCropname.UnitName;
                if (objCropRateBO != null) cropRate = objCropRateBO.CropRate;
            }

            arrCrop[0] = unitMeasureName;
            arrCrop[1] = cropRate;

            return arrCrop;
        }
        #endregion WebService
        /// <summary>
        /// To assign values to dropdownlist
        /// </summary>
        private void GetCropDescription()
        {
            CropDescriptionBLL BLLobj = new CropDescriptionBLL();

            ddlCropDescription.DataSource = BLLobj.GetCropDescription();
            ddlCropDescription.DataTextField = "CropDesName";
            ddlCropDescription.DataValueField = "CropDesID";
            ddlCropDescription.DataBind();
        }
        /// <summary>
        /// To assign values to dropdownlist
        /// </summary>
        private void GetCropType()
        {
            CropTypeBLL BLLobj = new CropTypeBLL();

            ddlCropType.DataSource = BLLobj.GetCropDetails();
            ddlCropType.DataTextField = "CropType";
            ddlCropType.DataValueField = "CROPTYPEID";
            ddlCropType.DataBind();
        }
        /// <summary>
        /// To assign values to dropdownlist
        /// </summary>
        private void GetCropName()
        {
            CropNameBLL BLLobj = new CropNameBLL();

            ddlCropName.DataSource = BLLobj.GetCropNameDetails();
            ddlCropName.DataTextField = "CropName";
            ddlCropName.DataValueField = "CROPID";
            ddlCropName.DataBind();
        }
        /// <summary>
        /// Set Grid Data source
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>e
        private void BindGrid(bool addRow, bool deleteRow)
        {
            CropsBLL CropsBLLobj = new CropsBLL();
            grdCrops.DataSource = CropsBLLobj.GetCrops(Convert.ToInt32(Session["HH_ID"]));
            grdCrops.DataBind();
        }
        /// <summary>
        /// save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;

            if (txtbxPAP_CROPID.Text.ToString().Trim() == string.Empty)
            {
                CropsBLL BLLobj = new CropsBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    CropsBO Cropsobj = new CropsBO();

                    Cropsobj.HHID = Convert.ToInt32(hhid);
                    Cropsobj.CROPID = Convert.ToInt32(ddlCropName.SelectedValue);
                    Cropsobj.CROPTYPEID = Convert.ToInt32(ddlCropType.SelectedValue);
                    Cropsobj.CROPDESCRIPTIONID = Convert.ToInt32(ddlCropDescription.SelectedValue);
                    Cropsobj.QUANTITY = Convert.ToDecimal(txtbxQuantity.Text);
                    Cropsobj.CROPRATE = Convert.ToDecimal(txtbxCropRate.Text);

                    if (CommentsTextBox.Text.Length > 1000)
                        Cropsobj.COMMENTS = CommentsTextBox.Text.Substring(0, 1000);
                    else
                        Cropsobj.COMMENTS = CommentsTextBox.Text;
                    Cropsobj.ISDELETED = "False";
                    Cropsobj.CREATEDBY = Convert.ToInt32(uID);

                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;

                        if (fileBytes != null)
                        {
                            Cropsobj.Photo = fileBytes;
                        }
                    }

                    CropsBLL CropsBLLobj1 = new CropsBLL();
                    count = CropsBLLobj1.Insert(Cropsobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);

                    BindGrid(true, true);
                    ClearData();
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
            else if (txtbxPAP_CROPID.Text.ToString() != string.Empty)
            {
                CropsBLL BLLobj = new CropsBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    CropsBO Cropsobj = new CropsBO();

                    Cropsobj.HHID = Convert.ToInt32(hhid);

                    Cropsobj.PAP_CROPID = Convert.ToInt32(txtbxPAP_CROPID.Text);
                    Cropsobj.CROPID = Convert.ToInt32(ddlCropName.SelectedValue);

                    Cropsobj.CROPTYPEID = Convert.ToInt32(ddlCropType.SelectedValue);
                    Cropsobj.CROPDESCRIPTIONID = Convert.ToInt32(ddlCropDescription.SelectedValue);

                    Cropsobj.QUANTITY = Convert.ToDecimal(txtbxQuantity.Text);
                    Cropsobj.CROPRATE = Convert.ToDecimal(txtbxCropRate.Text);
                
                   Cropsobj.CROPVALUATION = Cropsobj.QUANTITY * Cropsobj.CROPRATE;
                
                    if (CommentsTextBox.Text.Length > 1000)
                        Cropsobj.COMMENTS = CommentsTextBox.Text.Substring(0, 1000);
                    else
                        Cropsobj.COMMENTS = CommentsTextBox.Text;

                    Cropsobj.ISDELETED = "False";
                    Cropsobj.CREATEDBY = Convert.ToInt32(uID);
                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;
                        Cropsobj.Photo = fileBytes;
                    }
                    CropsBLL CropsBLLobj1 = new CropsBLL();
                    count = CropsBLLobj1.UpdateData(Cropsobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);

                    BindGrid(true, true);
                    ClearData();
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
            ChangeRequestStatusValuationCrop();
            projectFrozen();
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
                ViewState["PAP_CROPID"] = "0";
            }
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearData()
        {
            ddlCropName.ClearSelection();
            ddlCropType.ClearSelection();
            ddlCropDescription.ClearSelection();
            txtbxQuantity.Text = string.Empty;
            txtbxCropRate.Text = string.Empty;
            txtbxCropvaluation.Text = string.Empty;
            CommentsTextBox.Text = string.Empty;
           // lnkViewPhoto.Visible = false;
            txtbxPAP_CROPID.Text = string.Empty;
            //lnkViewPhoto.Visible = false;
            lblUnitMeasure.Text = "";
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
               // lnkViewPhoto.Visible = false;
            }
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCrops_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["PAP_CROPID"] = e.CommandArgument;
                popupData();
                //lnkViewPhoto.Visible = true;
                GetData();
                SetUpdateMode(true);
            }
            else if (e.CommandName == "DeleteRow")
            {
                string cropid = e.CommandArgument.ToString();
                DeleteData(cropid);
                SetUpdateMode(false);
                BindGrid(false, true);
                ClearData();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Data deleted successfully');", true);
            }
        }

        public void popupData()
        {
            //Add by Ramu.S For Upload Photo and View Photo;

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
            else
            {
                ProjectCode = "";
            }
            if (ViewState["PAP_CROPID"] != null)
            {
                perStu = ViewState["PAP_CROPID"].ToString();
            }
            string PhotoModule = "PAPCROP";

            string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
        }
        /// <summary>
        /// to delete data
        /// </summary>
        /// <param name="cropid"></param>
        private void DeleteData(string cropid)
        {
            CropsBLL CropsBLLobj = new CropsBLL();

            int Result = 0;
            if (cropid != null)
                Result = CropsBLLobj.DeleteData(cropid);
        }
        /// <summary>
        /// to fetch data and assign to textbox
        /// </summary>
        private void GetData()
        {
            CropsBLL BLLobj = new CropsBLL();
            int CropId = 0;

            if (ViewState["PAP_CROPID"] != null)
                CropId = Convert.ToInt32(ViewState["PAP_CROPID"]);

            CropsBO BOobj = new CropsBO();

            BOobj = BLLobj.GetData(CropId);

            txtbxPAP_CROPID.Text = BOobj.PAP_CROPID.ToString();

            ddlCropName.ClearSelection();
            if (ddlCropName.Items.FindByValue(BOobj.CROPID.ToString()) != null)
                ddlCropName.Items.FindByValue(BOobj.CROPID.ToString()).Selected = true;

            ddlCropType.ClearSelection();
            if (ddlCropType.Items.FindByValue(BOobj.CROPTYPEID.ToString()) != null)
                ddlCropType.Items.FindByValue(BOobj.CROPTYPEID.ToString()).Selected = true;

            ddlCropDescription.ClearSelection();
            if (ddlCropDescription.Items.FindByValue(BOobj.CROPDESCRIPTIONID.ToString()) != null)
                ddlCropDescription.Items.FindByValue(BOobj.CROPDESCRIPTIONID.ToString()).Selected = true;

            lblUnitMeasure.Text = GetUnitName(ddlCropName.SelectedValue + "|" + ddlCropDescription.SelectedValue)[0];

            txtbxQuantity.Text =UtilBO.CurrencyFormat(BOobj.QUANTITY);

            txtbxCropRate.Text =UtilBO.CurrencyFormat(BOobj.CROPRATE);

            txtbxCropvaluation.Text = UtilBO.CurrencyFormat(BOobj.QUANTITY * BOobj.CROPRATE);
          

            CommentsTextBox.Text = BOobj.COMMENTS;
        }
        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdCrops_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litDimensions = (Literal)e.Row.FindControl("litDimensions");
                decimal Quantity = (decimal)DataBinder.Eval(e.Row.DataItem, "QUANTITY");
                decimal CropRate = (decimal)DataBinder.Eval(e.Row.DataItem, "CROPRATE");

                litDimensions.Text = UtilBO.CurrencyFormat(Quantity * CropRate);

                Literal litCroprate = (Literal)e.Row.FindControl("litCroprate");
                litCroprate.Text = UtilBO.CurrencyFormat(CropRate);

                Literal PAPCropID = (Literal)e.Row.FindControl("PapCropId");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = PAPCropID.Text.ToString();

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
                string PhotoModule = "PAPCROP";

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
        protected void Changepage(object sender, GridViewPageEventArgs e)
        {
            grdCrops.PageIndex = e.NewPageIndex;
            BindGrid(true, false);
        }

    

     
    }
}