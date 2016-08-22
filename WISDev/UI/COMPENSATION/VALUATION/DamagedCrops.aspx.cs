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
    public partial class DamagedCrops : System.Web.UI.Page
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
            ValuationMenu1.HighlightMenu = ValuationMenu.MenuValue.DamagedCrops;
            ViewMasterCopy1.HighlightMenu = ViewMasterCopy.MenuValue.DamagedCropDetails;

            calDateDamaged.Format = UtilBO.DateFormat;

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
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Valuation - Damaged Crops";
                }
                else
                {
                    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                }

                ViewState["DAMAGED_CROPID"] = 0;
                BindGrid();
                getProjectDtaes();
                GetCropName();
                GetCropType();
                GetCropDescription();
                GetDamagedBy();

                txtbxQuantity.Attributes.Add("onblur", "CalculateAmount();");
                txtbxCropRate.Attributes.Add("onblur", "CalculateAmount();");
                txtbxAmountPaid.Attributes.Add("onKeyDown", "doCheck();");
                //txtbxCropRate.Attributes.Add("onKeyDown", "doCheck();");

                ddlCropName.Attributes.Add("onchange", "ddlCropName_IndexChanged(this);");
                ddlCropDescription.Attributes.Add("onchange", "ddlCropName_IndexChanged(this);");
                //lnkViewPhoto.Visible = false;                

                txtbxDamagedCropFormRefNo.Attributes.Add("onchange", "setDirtyText();");
                DateDamaged.Attributes.Add("onchange", "setDirtyText();");
                txtbxQuantity.Attributes.Add("onchange", "setDirtyText();");
                txtbxCropRate.Attributes.Add("onchange", "setDirtyText();");


                projectFrozen();
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_VALUATION) == false)
                {
                    lnkDamageCrops.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = false;
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
                lnkDamageCrops.Visible = false;
                btnSave.Visible = false;
                btnClear.Visible = false;
                grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = false;
                grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = false;
                grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = false;
            }
        }
        /// <summary>
        /// getProjectDtaes
        /// </summary>
        private void getProjectDtaes()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));

            if (objProject.ProjectStartDate != DateTime.MinValue)
                hfProjStartDate.Value = Convert.ToString(objProject.ProjectStartDate.ToString(UtilBO.DateFormat));

            if (objProject.ProjectEndDate != DateTime.MinValue)
                hfProjEndDate.Value = Convert.ToString(objProject.ProjectEndDate.ToString(UtilBO.DateFormat));

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
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = false;
                    checkApprovalExitOrNot();
                    getApprrequtStatusDamageCrops();
                }
                else
                {
                    lnkDamageCrops.Visible = false;
                }
            }
        }
        /// <summary>
        ///  checkApprovalExitOrNot
        /// </summary>
        public void checkApprovalExitOrNot()
        {
            #region Enable ChangeRequest Button
            StatusDamageCrops.Text = "";
            StatusDamageCrops.Visible = false;

            WorkFlowBO objWorkFlowBO = new WorkFlowBO();
            WorkFlowBLL objWorkFlowBLL = new WorkFlowBLL();

            string ChangeRequestCode = UtilBO.WorkflowChangeRequestHH;

            objWorkFlowBO = objWorkFlowBLL.getWOrkFlowApprovalID(Convert.ToInt32(Session["PROJECT_ID"]), ChangeRequestCode);

            int userID = Convert.ToInt32(Session["USER_ID"]);
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
            int HHID = Convert.ToInt32(Session["HH_ID"]);

            if (objWorkFlowBO != null)
            {
                string paramChangeRequest = string.Format("OpenChangeRequest('{0}',{1},{2},{3},'{4}')", ChangeRequestCode, ProjectID, userID, HHID, "HVDAC");
                lnkDamageCrops.Attributes.Add("onclick", paramChangeRequest);
                lnkDamageCrops.Visible = true;
            }
            else
            {
                lnkDamageCrops.Visible = false;
            }
            #endregion
            getApprrequtStatusDamageCrops();

        }
        /// <summary>
        ///  ChangeRequestStatusDamageCrops
        /// </summary>
        public void ChangeRequestStatusDamageCrops()
        {
            int Count;
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVDAC";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            Count = objHouseHoldBLL.ChangeRequestStatus(objHouseHold);
        }
        /// <summary>
        ///  getApprrequtStatusDamageCrops
        /// </summary>
        public void getApprrequtStatusDamageCrops()
        {
            PAP_HouseholdBLL objHouseHoldBLL = new PAP_HouseholdBLL();
            PAP_HouseholdBO objHouseHold = new PAP_HouseholdBO();
            objHouseHold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
            int householdID = Convert.ToInt32(Session["HH_ID"]);
            objHouseHold.HhId = householdID;
            objHouseHold.PageCode = "HVDAC";
            objHouseHold.Workflowcode = UtilBO.WorkflowChangeRequestHH;

            objHouseHold = objHouseHoldBLL.ApprovalChangerequestStatus(objHouseHold);

            if ((objHouseHold) != null)
            {
                if (objHouseHold.ApproverStatus == 3)
                {
                    lnkDamageCrops.Visible = false;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = false;
                    StatusDamageCrops.Visible = true;
                    StatusDamageCrops.Text = "Pending Approval";
                }
                if (objHouseHold.ApproverStatus == 2)
                {
                    lnkDamageCrops.Visible = true;
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = false;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = false;
                    StatusDamageCrops.Visible = false;
                    StatusDamageCrops.Text = string.Empty;
                }
                if (objHouseHold.ApproverStatus == 1)
                {
                    lnkDamageCrops.Visible = false;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 1].Visible = true;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 2].Visible = true;
                    grdDamagedCrops.Columns[grdDamagedCrops.Columns.Count - 4].Visible = true;
                    StatusDamageCrops.Visible = false;
                    //StatusLabel.Text = "Pending Approval";
                }
            }
        }
        #endregion
        /// <summary>
        /// to fetch values to dropdownlist
        /// </summary>
        private void GetDamagedBy()
        {
            DamagedCropsBLL BLLobj = new DamagedCropsBLL();

            ddlDamagedBy.DataSource = BLLobj.GetDamagedBy();
            ddlDamagedBy.DataTextField = "CROPDAMAGEDBYOTHER";
            ddlDamagedBy.DataValueField = "CROPDAMAGEDBYID";
            ddlDamagedBy.DataBind();
        }
        /// <summary>
        /// to fetch values to dropdownlist
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
        /// to fetch values to dropdownlist
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
        /// to fetch values to dropdownlist
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
        private void BindGrid()
        {
            string hhid = Session["HH_ID"].ToString();
            DamagedCropsBLL DamagedCropsBLLobj = new DamagedCropsBLL();
            grdDamagedCrops.DataSource = DamagedCropsBLLobj.GetDamagedCrops(hhid);
            grdDamagedCrops.DataBind();
        }
        /// <summary>
        /// to save details to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int count = 0;

            if (DAMAGED_CROPIDTextBox.Text.ToString().Trim() == string.Empty)
            {
                DamagedCropsBLL BLLobj = new DamagedCropsBLL();
                DamagedCropsBO DamagedCropsobj = new DamagedCropsBO();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();

                    DamagedCropsobj.HHID = Convert.ToInt32(hhid);
                    DamagedCropsobj.DMGCRPFORMREFNO = txtbxDamagedCropFormRefNo.Text;

                    DamagedCropsobj.CROPID = Convert.ToInt32(ddlCropName.SelectedValue);

                    DamagedCropsobj.CROPTYPEID = Convert.ToInt32(ddlCropType.SelectedValue);
                    DamagedCropsobj.CROPDESCRIPTIONID = Convert.ToInt32(ddlCropDescription.SelectedValue);

                    DamagedCropsobj.DATEDAMAGED = Convert.ToDateTime(DateDamaged.Text.ToString());

                    DamagedCropsobj.CROPDAMAGEDBYID = Convert.ToInt32(ddlDamagedBy.SelectedValue);
                    if (DamagedByTextBox.Text != string.Empty)
                    {
                        DamagedCropsobj.CROPDAMAGEDBYOTHER = DamagedByTextBox.Text;
                    }
                    else
                    {
                        DamagedCropsobj.CROPDAMAGEDBYOTHER = "";
                    }

                    DamagedCropsobj.QUANTITY = Convert.ToDecimal(txtbxQuantity.Text);
                    DamagedCropsobj.CROPRATE = Convert.ToDecimal(txtbxCropRate.Text);

                    DamagedCropsobj.AMOUNTPAID = DamagedCropsobj.QUANTITY * DamagedCropsobj.CROPRATE;
                    if (CommentsTextBox.Text.Length > 1000)
                        DamagedCropsobj.COMMENTS = CommentsTextBox.Text.Substring(0, 1000);
                    else
                        DamagedCropsobj.COMMENTS = CommentsTextBox.Text;
                    DamagedCropsobj.ISDELETED = "False";
                    DamagedCropsobj.CREATEDBY = Convert.ToInt32(uID);

                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;

                        if (fileBytes != null)
                        {
                            DamagedCropsobj.Photo = fileBytes;
                        }
                    }

                    DamagedCropsBLL DamagedCropsBLLobj = new DamagedCropsBLL();
                    count = DamagedCropsBLLobj.Insert(DamagedCropsobj);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "ShowSaveMessage('');", true);

                    BindGrid();
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

            else if (DAMAGED_CROPIDTextBox.Text.ToString().Trim() != string.Empty)
            {
                DamagedCropsBLL BLLobj = new DamagedCropsBLL();

                try
                {
                    string uID = Session["USER_ID"].ToString();
                    string hhid = Session["HH_ID"].ToString();
                    DamagedCropsBO DamagedCropsobj = new DamagedCropsBO();

                    DamagedCropsobj.HHID = Convert.ToInt32(hhid);
                    DamagedCropsobj.DAMAGED_CROPID = Convert.ToInt32(DAMAGED_CROPIDTextBox.Text);
                    DamagedCropsobj.DMGCRPFORMREFNO = txtbxDamagedCropFormRefNo.Text;
                    DamagedCropsobj.CROPID = Convert.ToInt32(ddlCropName.SelectedValue);

                    DamagedCropsobj.CROPTYPEID = Convert.ToInt32(ddlCropType.SelectedValue);
                    DamagedCropsobj.CROPDESCRIPTIONID = Convert.ToInt32(ddlCropDescription.SelectedValue);
                    DamagedCropsobj.DATEDAMAGED =Convert.ToDateTime (DateDamaged.Text.ToString());
                    DamagedCropsobj.CROPDAMAGEDBYID = Convert.ToInt32(ddlDamagedBy.SelectedValue);
                    DamagedCropsobj.CROPDAMAGEDBYOTHER = DamagedByTextBox.Text;
                    DamagedCropsobj.QUANTITY = Convert.ToDecimal(txtbxQuantity.Text);
                    DamagedCropsobj.CROPRATE = Convert.ToDecimal(txtbxCropRate.Text);

                    DamagedCropsobj.AMOUNTPAID = DamagedCropsobj.QUANTITY * DamagedCropsobj.CROPRATE;
                    
                    if (CommentsTextBox.Text.Length > 1000)
                        DamagedCropsobj.COMMENTS = CommentsTextBox.Text.Substring(0, 1000);
                    else
                        DamagedCropsobj.COMMENTS = CommentsTextBox.Text;

                    DamagedCropsobj.ISDELETED = "False";
                    DamagedCropsobj.CREATEDBY = Convert.ToInt32(uID);                        

                    if (photoFileUpload.HasFile)
                    {
                        byte[] fileBytes = photoFileUpload.FileBytes;

                        if (fileBytes != null)
                        {
                            DamagedCropsobj.Photo = fileBytes;
                        }
                    }

                    DamagedCropsBLL DamagedCropsBLLobj = new DamagedCropsBLL();
                    count = BLLobj.Update(DamagedCropsobj);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Updated", "ShowUpdateMessage('');", true);

                    BindGrid();
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
            ChangeRequestStatusDamageCrops();
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
                ViewState["DAMAGED_CROPID"] = "0";
            }
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
                //lnkViewPhoto.Visible = false;
            }
        }
        /// <summary>
        /// Clear the search Fiels and Set Data to Grid Data souce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearData()
        {
            txtbxDamagedCropFormRefNo.Text = string.Empty;
            ddlCropName.ClearSelection();
            ddlCropType.ClearSelection();
            ddlCropDescription.ClearSelection();
            ddlDamagedBy.ClearSelection();
            DateDamaged.Text = "";
            DamagedByTextBox.Text = string.Empty;
            updDamagedBy.Update();
            txtbxQuantity.Text = string.Empty;
            txtbxCropRate.Text = string.Empty;
            txtbxAmountPaid.Text = string.Empty;
            CommentsTextBox.Text = string.Empty;
            //lnkViewPhoto.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearDateField", "ClearDateField('" + DateDamaged.ClientID + "');", true);
            ViewState["DAMAGED_CROPID"] = "0";
            DAMAGED_CROPIDTextBox.Text = string.Empty;
        }
        /// <summary>
        /// Set edit mode for edit comand
        /// Delete data from the database for delete comand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DamagedCrops_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditRow")
            {
                ViewState["DAMAGED_CROPID"] = e.CommandArgument;
                GetData();
                //lnkViewPhoto.Visible = true;
                popupData();
                SetUpdateMode(true);

            }
            else if (e.CommandName == "DeleteRow")
            {
                int damagedcrop = Convert.ToInt32(e.CommandArgument);
                DeleteData(damagedcrop);
                BindGrid();
                ClearData();
                SetUpdateMode(false);
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

            if (ViewState["DAMAGED_CROPID"] != null)
            {
                perStu = ViewState["DAMAGED_CROPID"].ToString();
            }
            string PhotoModule = "DAMAGEDCROPS";

            string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, perStu);

            //lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
        }
        /// <summary>
        /// to delete data from database
        /// </summary>
        /// <param name="damageCropId"></param>
        private void DeleteData(int damageCropId)
        {
            DamagedCropsBLL DamagedCropsBLLobj = new DamagedCropsBLL();
           // int damageCropId = 0;
            int Result = 0;
            //if (ViewState["DAMAGED_CROPID"] != null)
            //    damageCropId = Convert.ToInt32(ViewState["DAMAGED_CROPID"].ToString());

            Result = DamagedCropsBLLobj.DeleteData(damageCropId);
        }
        /// <summary>
        /// to fetch details and assign to textbox
        /// </summary>
        private void GetData()
        {
            DamagedCropsBLL DamagedCropsBLLobj = new DamagedCropsBLL();
            int damageCropId = 0;

            if (ViewState["DAMAGED_CROPID"] != null)
                damageCropId = Convert.ToInt32(ViewState["DAMAGED_CROPID"]);

            DamagedCropsBO BOobj = new DamagedCropsBO();
            BOobj = DamagedCropsBLLobj.GetData(damageCropId);

            DAMAGED_CROPIDTextBox.Text = BOobj.DAMAGED_CROPID.ToString();

            txtbxDamagedCropFormRefNo.Text = BOobj.DMGCRPFORMREFNO;

            ddlCropName.ClearSelection();
            if (ddlCropName.Items.FindByValue(BOobj.CROPID.ToString()) != null)
                ddlCropName.Items.FindByValue(BOobj.CROPID.ToString()).Selected = true;

            ddlCropType.ClearSelection();
            if (ddlCropType.Items.FindByValue(BOobj.CROPTYPEID.ToString()) != null)
                ddlCropType.Items.FindByValue(BOobj.CROPTYPEID.ToString()).Selected = true;

            ddlCropDescription.ClearSelection();
            if (ddlCropDescription.Items.FindByValue(BOobj.CROPDESCRIPTIONID.ToString()) != null)
                ddlCropDescription.Items.FindByValue(BOobj.CROPDESCRIPTIONID.ToString()).Selected = true;

            DateDamaged.Text = BOobj.DATEDAMAGED.ToString(UtilBO.DateFormat);

            ddlDamagedBy.ClearSelection();
            if (ddlDamagedBy.Items.FindByValue(BOobj.CROPDAMAGEDBYID.ToString()) != null)
                ddlDamagedBy.Items.FindByValue(BOobj.CROPDAMAGEDBYID.ToString()).Selected = true;

            DamagedByTextBox.Text = BOobj.CROPDAMAGEDBYOTHER;
            updDamagedBy.Update();

            txtbxQuantity.Text = UtilBO.CurrencyFormat(BOobj.QUANTITY);

            txtbxCropRate.Text = UtilBO.CurrencyFormat(BOobj.CROPRATE);

            txtbxAmountPaid.Text = UtilBO.CurrencyFormat(BOobj.AMOUNTPAID);

            CommentsTextBox.Text = BOobj.COMMENTS;
        }
        /// <summary>
        /// to change page in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdDamagedCrops.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        /// <summary>
        /// to set controls in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdDamagedCrops_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dateDamaged = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DATEDAMAGED"));
                if (dateDamaged != DateTime.MinValue)
                    ((Literal)e.Row.FindControl("litDateDamaged")).Text = dateDamaged.ToString(UtilBO.DateFormat);

                Literal litCropRate = (Literal)e.Row.FindControl("litCropRate");
                Literal litAmountPaid = (Literal)e.Row.FindControl("litAmountPaid");

                decimal CropRate = (decimal)DataBinder.Eval(e.Row.DataItem, "CROPRATE");
                decimal AmountPaid = (decimal)DataBinder.Eval(e.Row.DataItem, "AMOUNTPAID");

                litCropRate.Text = UtilBO.CurrencyFormat(CropRate);
                litAmountPaid.Text = UtilBO.CurrencyFormat(AmountPaid);

                Literal DamagedCropID = (Literal)e.Row.FindControl("damagedCropId");
                System.Web.UI.HtmlControls.HtmlAnchor lnkUPloadPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkUPloadPhoto");
                System.Web.UI.HtmlControls.HtmlAnchor lnkViewPhoto = (System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("lnkViewPhoto");

                int ProjectID = 0;
                int HHID = 0;
                int userID = 0;
                string ProjectCode = string.Empty;
                string PermanentStructId = DamagedCropID.Text.ToString();

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
                string PhotoModule = "DAMAGEDCROPS";

                string paramPhoto = string.Format("OpenUploadPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkUPloadPhoto.Attributes.Add("onclick", paramPhoto);

                string paramPhotoView = string.Format("OpenViewPhoto({0},{1},{2},'{3}','{4}',{5});", ProjectID, HHID, userID, ProjectCode, PhotoModule, PermanentStructId);

                lnkViewPhoto.Attributes.Add("onclick", paramPhotoView);
            
            }
        }
        /// <summary>
        /// calls EnableDamagedBy_TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDamagedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDamagedBy_TextBox();
        }
        /// <summary>
        /// to enable textbox based on condition
        /// </summary>
        private void EnableDamagedBy_TextBox()
        {
            if (ddlDamagedBy.SelectedValue.ToLower() == "other" || ddlDamagedBy.SelectedItem.Text.ToLower() == "other")
            {
                DamagedByTextBox.Enabled = true;
            }
            else
                DamagedByTextBox.Enabled = false;
        }

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

    }
}