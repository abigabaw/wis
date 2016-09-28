using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Drawing;
using System.Text;
using WIS.UI.COMPENSATION.SOCIOECONOMIC;

namespace WIS
{
    public partial class AddPAP : System.Web.UI.Page
    {
        #region Global Declaration & Page Load
        int chkStatus = 0;
        /// <summary>
        /// Check User Permitions,Set Page Header,Set Attribute to Controls
        /// Check project is Frozen or not
        /// Check Project Status
        /// Call respective methods to Get the data From Data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectMenu1.HighlightMenu = ProjectMenu.MenuValue.PAP;

            if (Session["PROJECT_CODE"] != null)
                Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - PAP Information";

            mskPlotReference.Mask = UtilBO.PlotReferenceMask;
            MaskedEditExtender1.Mask = UtilBO.PlotReferenceMask;
            if (!IsPostBack)
            {
                BindDistricts();
                txtPlotReference.Attributes.Add("onblur", "SetUpperCase(this);CheckPlotReference(this);");
                btnADDPAP.Attributes.Add("onclick", "SetVisible(0);");
                btnSearchPAP.Attributes.Add("onclick", "SetVisible(1);");
                txtrightofWay.Attributes.Add("onblur", "CalculateTotalAcres()");
                txtWayleaves.Attributes.Add("onblur", "CalculateTotalAcres()");
                SetGridSource(true, -1);
                ViewState["PAPHHID_ID"] = 0;

                ViewState["IMPSTATUS"] = 0;
                ViewState["PAPDISTRICT"] = null;
                ViewState["PAPCOUNTY"] = null;
                ViewState["PAPSUBCOUNTY"] = null;
                ViewState["PAPPARISH"] = null;
                ViewState["PAPVILLAGE"] = null;
                EnableSaveCancel(false);                
                CheckProjectStatus();
                getFrozen();
                txtSurname.Attributes.Add("onchange", "setDirty();");
                txtfirstname.Attributes.Add("onchange", "setDirty();");
                txtPapUid.Attributes.Add("onchange", "setDirty();");
                txtPlotReference.Attributes.Add("onchange", "setDirty();");
                txtDesignation.Attributes.Add("onchange", "setDirty();");
                txtWayleaves.Attributes.Add("onchange", "setDirty();");
                btn_ImportExcel.Attributes.Add("onclick", "isDirty = 0;");
                btn_Save.Attributes.Add("onclick", "isDirty = 0;");
                btn_Clear.Attributes.Add("onclick", "isDirty = 0;");
                btnGridDataSave.Attributes.Add("onclick", "isDirty = 0;");
                btnGridDataCancel.Attributes.Add("onclick", "isDirty = 0;");
                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false)
                {
                    Response.Redirect("ViewProjects.aspx");
                }
                else if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == false &&
                        CheckAuthorization.HasViewPrivilege(UtilBO.PrivilegeCode.CREATE_PROJECT) == true)
                {
                    btn_ImportExcel.Visible = false;
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    btnADDPAP.Visible = false;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
                    GrdPapInformation.Columns[2].Visible = false;
                    GrdPapInformation.Columns[3].Visible = true;
                    GrdPapInformation.Columns[11].Visible = true;
                    GrdPapInformation.Columns[10].Visible = false;
                }
            }
            //GrdPapInformation.RowDataBound += new GridViewRowEventHandler(GrdPapInformation_RowDataBound);

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
        }
        /// <summary>
        /// Set default Button using Java Script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnSearch.ClientID);
            else
                stb.Append(btn_Save.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }
        /// <summary>
        /// Check Project Status and Set Access to the user
        /// </summary>
        private void CheckProjectStatus()
        {
            ProjectBLL objProjectBLL = new ProjectBLL();
            ProjectBO objProject = objProjectBLL.GetProjectByProjectID(Convert.ToInt32(Session["PROJECT_ID"]));
            if (objProject != null && objProject.projectStatus != null && objProject.projectStatus.ToUpper() == "COMPLETED")
            {
                btnADDPAP.Visible = false;
                btnSearchPAP.Visible = false;
                pnlSearchPAP.Visible = true;
                pnlAddPAP.Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
                GrdPapInformation.Columns[2].Visible = false;
                GrdPapInformation.Columns[3].Visible = true;
                GrdPapInformation.Columns[11].Visible = true;
                GrdPapInformation.Columns[10].Visible = false;
            }
            else
            {
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = true;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = true;
                GrdPapInformation.Columns[2].Visible = true;
                GrdPapInformation.Columns[3].Visible = false;
                GrdPapInformation.Columns[11].Visible = true;
                GrdPapInformation.Columns[10].Visible = false;
            }
        }
        #endregion Global Declaration & Page Load

        #region Loading DropDowns
        /// <summary>
        /// Bind Data to Dropdown ddlDistrict
        /// </summary>
        private void BindDistricts()
        {
            ddlDistrict.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictID";
            ddlDistrict.DataBind();

            ddlDistrictSearch.DataSource = (new MasterBLL()).LoadDistrictData();
            ddlDistrictSearch.DataTextField = "DistrictName";
            ddlDistrictSearch.DataValueField = "DistrictID";
            ddlDistrictSearch.DataBind();
        }
        /// <summary>
        /// Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlDistrictSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCountiesForsearch(ddlDistrictSearch.SelectedItem.Value);
            BindSubCountiesForsearch(ddlCountySearch.SelectedItem.Value);
            uplSubCountySearch.Update();
            BindVillagesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplVillageSearch.Update();
            BindParishesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplParishSearch.Update();
        }
        /// <summary>
        /// Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }
        /// <summary>
        /// Bind Data to Dropdown ddlCounty
        /// </summary>
        private void BindCounties(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCounty.Items[0].Text, ddlCounty.Items[0].Value);

            ddlCounty.Items.Clear();

            if (districtID != "0")
            {
                ddlCounty.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCounty.DataTextField = "CountyName";
                ddlCounty.DataValueField = "CountyID";
                ddlCounty.DataBind();
            }

            ddlCounty.Items.Insert(0, firstListItem);
            ddlCounty.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Dropdown ddlCountySearch
        /// </summary>
        private void BindCountiesForsearch(string districtID)
        {
            ListItem firstListItem = new ListItem(ddlCountySearch.Items[0].Text, ddlCounty.Items[0].Value);

            ddlCountySearch.Items.Clear();

            if (districtID != "0")
            {
                ddlCountySearch.DataSource = (new MasterBLL()).LoadCountyData(districtID);
                ddlCountySearch.DataTextField = "CountyName";
                ddlCountySearch.DataValueField = "CountyID";
                ddlCountySearch.DataBind();
            }

            ddlCountySearch.Items.Insert(0, firstListItem);
            ddlCountySearch.SelectedIndex = 0;
        }

        /// <summary>
        /// Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlCountySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCountiesForsearch(ddlCountySearch.SelectedItem.Value);
            uplSubCountySearch.Update();
            BindVillagesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplVillageSearch.Update();
            BindParishesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplParishSearch.Update();
        }

        /// <summary>
        /// Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
        }

        /// <summary>
        /// Bind Data to Dropdown ddlSubCountySearch
        /// </summary>
        private void BindSubCountiesForsearch(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCountySearch.Items[0].Text, ddlSubCounty.Items[0].Value);

            ddlSubCountySearch.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCountySearch.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCountySearch.DataTextField = "SubCountyName";
                ddlSubCountySearch.DataValueField = "SubCountyID";
                ddlSubCountySearch.DataBind();
            }

            ddlSubCountySearch.Items.Insert(0, firstListItem);
            ddlSubCountySearch.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Dropdown ddlSubCounty
        /// </summary>
        private void BindSubCounties(string countyID)
        {
            ListItem firstListItem = new ListItem(ddlSubCounty.Items[0].Text, ddlSubCounty.Items[0].Value);

            ddlSubCounty.Items.Clear();

            if (countyID != "0")
            {
                ddlSubCounty.DataSource = (new MasterBLL()).LoadSubCountyData(countyID);
                ddlSubCounty.DataTextField = "SubCountyName";
                ddlSubCounty.DataValueField = "SubCountyID";
                ddlSubCounty.DataBind();
            }

            ddlSubCounty.Items.Insert(0, firstListItem);
            ddlSubCounty.SelectedIndex = 0;
        }

        /// <summary>
        ///Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlSubCountySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillagesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplVillageSearch.Update();
            BindParishesForsearch(ddlSubCountySearch.SelectedItem.Value);
            uplParishSearch.Update();
        }

        /// <summary>
        /// Call respective Metheods to Fill the Respective Data
        /// </summary>
        protected void ddlSubCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillages(ddlSubCounty.SelectedItem.Value);
            BindParishes(ddlSubCounty.SelectedItem.Value);
        }

        /// <summary>
        /// Bind Data to Dropdown ddlParishSearch
        /// </summary>
        private void BindParishesForsearch(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlParishSearch.Items[0].Text, ddlParish.Items[0].Value);

            ddlParishSearch.Items.Clear();

            if (subCounty != "0")
            {
                ddlParishSearch.DataSource = (new MasterBLL()).LoadParishData(subCounty);
                ddlParishSearch.DataTextField = "ParishName";
                ddlParishSearch.DataValueField = "ParishID";
                ddlParishSearch.DataBind();
            }

            ddlParishSearch.Items.Insert(0, firstListItem);
            ddlParishSearch.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Dropdown ddlVillageSearch
        /// </summary>
        private void BindVillagesForsearch(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlVillageSearch.Items[0].Text, ddlVillage.Items[0].Value);

            ddlVillageSearch.Items.Clear();

            if (subCounty != "0")
            {
                ddlVillageSearch.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                ddlVillageSearch.DataTextField = "VillageName";
                ddlVillageSearch.DataValueField = "VillageID";
                ddlVillageSearch.DataBind();
            }

            ddlVillageSearch.Items.Insert(0, firstListItem);
            ddlVillageSearch.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Dropdown ddlParish
        /// </summary>
        private void BindParishes(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlParish.Items[0].Text, ddlParish.Items[0].Value);

            ddlParish.Items.Clear();

            if (subCounty != "0")
            {
                ddlParish.DataSource = (new MasterBLL()).LoadParishData(subCounty);
                ddlParish.DataTextField = "ParishName";
                ddlParish.DataValueField = "ParishID";
                ddlParish.DataBind();
            }

            ddlParish.Items.Insert(0, firstListItem);
            ddlParish.SelectedIndex = 0;
        }

        /// <summary>
        /// Bind Data to Dropdown ddlVillage
        /// </summary>
        private void BindVillages(string subCounty)
        {
            ListItem firstListItem = new ListItem(ddlVillage.Items[0].Text, ddlVillage.Items[0].Value);

            ddlVillage.Items.Clear();

            if (subCounty != "0")
            {
                ddlVillage.DataSource = (new MasterBLL()).LoadVillageData(subCounty);
                ddlVillage.DataTextField = "VillageName";
                ddlVillage.DataValueField = "VillageID";
                ddlVillage.DataBind();
            }

            ddlVillage.Items.Insert(0, firstListItem);
            ddlVillage.SelectedIndex = 0;
        }
        #endregion Loading DropDowns

        #region Buttons Events & Functions
        /// <summary>
        /// Set visibilty to Import Panel and hide remaining things
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            pnlPAPInformation.Visible = false;
            pnlFileUpload.Visible = true;
            btnADDPAP.Visible = false;
            btnSearchPAP.Visible = false;
            GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
            GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
            GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
            GrdPapInformation.Columns[1].Visible = false;
            GrdPapInformation.Columns[2].Visible = false;
            GrdPapInformation.Columns[3].Visible = true;
            GrdPapInformation.Columns[11].Visible = false;
            GrdPapInformation.Columns[10].Visible = true;
        }
        /// <summary>
        /// set Imported data to Grid View
        /// Check Excel is Valid or Not
        /// </summary>
        /// <param name="pFileName"></param>
        /// <param name="fileExtension"></param>
        private void Import_To_Grid(string pFileName, string fileExtension)
        {
            ViewState["IMPSTATUS"] = 1;
            if (ViewState["PAPDISTRICT"] == null)
            {
                CountyList oCountyList = (new MasterBLL()).LoadCountyData("0");
                SubCountyList oSubCountyList = (new MasterBLL()).LoadSubCountyData("0");
                ParishList oParishList = (new MasterBLL()).LoadParishData("0");
                VillageList oVillageList = (new MasterBLL()).LoadVillageData("0");
                StringBuilder sbCounty = new StringBuilder();
                foreach (CountyBO oCountybo in oCountyList)
                {
                    sbCounty.Append("'");
                    sbCounty.Append(oCountybo.CountyName);
                    sbCounty.Append("',");
                }
                StringBuilder sbSubCounty = new StringBuilder();
                foreach (SubCountyBO oSubCountyBO in oSubCountyList)
                {
                    sbSubCounty.Append("'");
                    sbSubCounty.Append(oSubCountyBO.SubCountyName);
                    sbSubCounty.Append("',");
                }
                StringBuilder sbParish = new StringBuilder();
                foreach (ParishBO oParishBO in oParishList)
                {
                    sbParish.Append("'");
                    sbParish.Append(oParishBO.ParishName);
                    sbParish.Append("',");
                }
                StringBuilder sbVillage = new StringBuilder();
                foreach (VillageBO oVillageBO in oVillageList)
                {
                    sbVillage.Append("'");
                    sbVillage.Append(oVillageBO.VillageName);
                    sbVillage.Append("',");
                }

                ViewState["PAPDISTRICT"] = sbCounty;
                ViewState["PAPCOUNTY"] = sbCounty;
                ViewState["PAPSUBCOUNTY"] = sbSubCounty;
                ViewState["PAPPARISH"] = sbParish;
                ViewState["PAPVILLAGE"] = sbVillage;
            }


            AddPAPBLL objAddPAPBLL = new AddPAPBLL();
            DataTable dtPAP = objAddPAPBLL.ExcelDataImportintoGrid(pFileName, fileExtension, Convert.ToInt32(Session["PROJECT_ID"]), Convert.ToInt32(Session["USER_ID"]));

            if (dtPAP.Columns.Contains("SURNAME") && dtPAP.Columns.Contains("FIRSTNAME") && dtPAP.Columns.Contains("OTHERNAME")
                && dtPAP.Columns.Contains("PAPNAME") && dtPAP.Columns.Contains("PAP UID") && dtPAP.Columns.Contains("INSTITUTION/ORGANIZATION NAME")
                && dtPAP.Columns.Contains("PAPTYPE") && dtPAP.Columns.Contains("PLOTREFERENCE") && dtPAP.Columns.Contains("DESIGNATION")
                && dtPAP.Columns.Contains("DISTRICT") && dtPAP.Columns.Contains("COUNTY") && dtPAP.Columns.Contains("SUBCOUNTY")
                && dtPAP.Columns.Contains("PARISH") && dtPAP.Columns.Contains("VILLAGE") && dtPAP.Columns.Contains("Landtenure")
                && dtPAP.Columns.Contains("RIGHTOFWAY") && dtPAP.Columns.Contains("WAYLEAVES") && dtPAP.Columns.Contains("TotalSQM")
                && dtPAP.Columns.Contains("TotalHa") && dtPAP.Columns.Contains("TotalAcres") && dtPAP.Columns.Contains("Cropsvalue")
                && dtPAP.Columns.Contains("Housevalue") && dtPAP.Columns.Contains("SubTotal") && dtPAP.Columns.Contains("Disturbance")
                && dtPAP.Columns.Contains("TOTAL") && dtPAP.Columns.Contains("PLOTLATITUDE") && dtPAP.Columns.Contains("PLOTLONGITUDE"))
            {
                if (dtPAP.Columns["INSTITUTION/ORGANIZATION NAME"] != null)
                    dtPAP.Columns["INSTITUTION/ORGANIZATION NAME"].ColumnName = "Institution";
                if (dtPAP.Columns.Contains("PAP UID"))
                    dtPAP.Columns["PAP UID"].ColumnName = "Papuid";
                if (!dtPAP.Columns.Contains("HHID"))
                    dtPAP.Columns.Add("HHID", typeof(string));
                if (!dtPAP.Columns.Contains("IsDeleted"))
                    dtPAP.Columns.Add("IsDeleted", typeof(string));
                foreach (DataRow row in dtPAP.Rows)
                {
                    row["IsDeleted"] = "False";
                }
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
                GrdPapInformation.Columns[1].Visible = false;
                GrdPapInformation.Columns[2].Visible = false;
                GrdPapInformation.Columns[3].Visible = true;
                GrdPapInformation.Columns[11].Visible = false;
                GrdPapInformation.Columns[10].Visible = true;

                GrdPapInformation.DataSource = dtPAP;
                GrdPapInformation.DataBind();
                pnlPAPInformation.Visible = true;
                pnlFileUpload.Visible = false;

                if (GrdPapInformation.Rows.Count > 0)
                    EnableSaveCancel(true);
                else
                    EnableSaveCancel(false);

                if (GrdPapInformation.Rows.Count > 0)
                    p1Grid.Visible = true;
                else
                    p1Grid.Visible = false;
                ViewState["EXCEL_DATA"] = dtPAP;
            }
            else
            {
                EnableSaveCancel(false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid Excel File.Please Upload valid Excel File');", true);
            }
            //SetGridSource(true,-1);
        }
        /// <summary>
        /// Set Uploaded file Data to Grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                chkStatus = 1;
                string FileName = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
                string Extension = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName);
                if (Extension.ToUpper().Contains(".xlsx".ToUpper()) ||
                    Extension.ToUpper().Contains(".xls".ToUpper()))
                {
                    string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = FolderPath + FileName;
                    FileUpload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension);
                }
                else
                {
                    EnableSaveCancel(false);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid File.Please Upload an Excel File');", true);
                }
            }
        }
        /// <summary>
        /// To Enable Save or Cancel
        /// </summary>
        /// <param name="Status"></param>
        private void EnableSaveCancel(bool Status)
        {
            if (Status)
            {
                lgndTitle.InnerText = "Preview Data";
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
                GrdPapInformation.Columns[1].Visible = false;
                GrdPapInformation.Columns[2].Visible = false;
                GrdPapInformation.Columns[3].Visible = true;
                GrdPapInformation.Columns[11].Visible = false;
                GrdPapInformation.Columns[10].Visible = true;
            }
            else
            {
                lgndTitle.InnerText = "PAP List";
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = true;
                GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = true;
                GrdPapInformation.Columns[1].Visible = true;
                GrdPapInformation.Columns[2].Visible = true;
                GrdPapInformation.Columns[3].Visible = false;
                GrdPapInformation.Columns[11].Visible = true;
                GrdPapInformation.Columns[10].Visible = false;
            }
            btnGridDataSave.Visible = Status;
            btnGridDataCancel.Visible = Status;
        }
        /// <summary>
        /// Set Updatemode for Buttons
        /// </summary>
        /// <param name="Mode"></param>
        private void SetUpdatemode(bool Mode)
        {
            if (Mode)
            {
                btn_Save.Text = "Update";
                btn_Clear.Text = "Cancel";
            }
            else
            {
                ViewState["PAPHHID_ID"] = 0;
                btn_Save.Text = "Save";
                btn_Clear.Text = "Clear";
            }
        }
        /// <summary>
        /// To save And UpdateData into data base
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void ReCache(int HHID)
        {
            PapDataCache PapCache = new PapDataCache();
            string householdID = Cache[PapCache.BuildCacheKey("HOUSEHOLD_ID")].ToString();
            PapCache.ClearCache();
            PapCache.CachePAPData(householdID);
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            AddPAPBO objAddPAP = new AddPAPBO();
            string message = string.Empty;

            if (Convert.ToInt32(ViewState["PAPHHID_ID"]) == 0)
            {
                objAddPAP.ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);
                objAddPAP.Surname = txtSurname.Text.Trim();
                objAddPAP.Firstname = txtfirstname.Text.Trim();
                objAddPAP.Othername = txtOthername.Text.Trim();
                objAddPAP.Pap = txtSurname.Text.Trim() + " " + txtfirstname.Text.Trim();
                objAddPAP.Papuid = txtPapUid.Text.Trim();
                objAddPAP.Plot_ref = txtPlotReference.Text.Trim();
                objAddPAP.District = ddlDistrict.SelectedItem.Text;
                objAddPAP.County = ddlCounty.SelectedItem.Text;
                objAddPAP.SubCounty = ddlSubCounty.SelectedItem.Text;
                objAddPAP.Designation = txtDesignation.Text.Trim();

                if (ddlParish.SelectedIndex > 0) objAddPAP.Parish = ddlParish.SelectedItem.Text;

                objAddPAP.Village = ddlVillage.SelectedItem.Text;
                objAddPAP.Right_of_way = txtrightofWay.Text.Trim();
                objAddPAP.Wayleaves = txtWayleaves.Text.Trim();
                objAddPAP.Total = txttotal.Text.Trim();
                objAddPAP.Plotlatitude = txtLatitude.Text.Trim();
                objAddPAP.Plotlongitude = txtLongitude.Text.Trim();
                objAddPAP.CreatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = (new AddPAPBLL()).AddPAP(objAddPAP);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data saved successfully";
                    ClearDetails();
                    SetUpdatemode(false);
                }
            }
            else
            {
                PAP_HouseholdBO objHousehold = new PAP_HouseholdBO();
                objHousehold.HhId = Convert.ToInt32(ViewState["PAPHHID_ID"]);
                objHousehold.ProjectedId = Convert.ToInt32(Session["PROJECT_ID"]);
                objHousehold.Surname = txtSurname.Text.Trim();
                objHousehold.Firstname = txtfirstname.Text.Trim();
                objHousehold.Othername = txtOthername.Text.Trim();
                objHousehold.PapName = txtSurname.Text.Trim() + " " + txtfirstname.Text.Trim();
                objHousehold.Papuid = txtPapUid.Text.Trim();
                objHousehold.PlotReference = txtPlotReference.Text.Trim();
                objHousehold.District = ddlDistrict.SelectedItem.Text;
                objHousehold.County = ddlCounty.SelectedItem.Text;
                objHousehold.SubCounty = ddlSubCounty.SelectedItem.Text;
                objHousehold.Plotlatitude = txtLatitude.Text.Trim();
                objHousehold.Plotlongitude = txtLongitude.Text.Trim();
                objHousehold.Designation = txtDesignation.Text.Trim();

                if (ddlParish.SelectedIndex > 0) objHousehold.Parish = ddlParish.SelectedItem.Text;

                objHousehold.Village = ddlVillage.SelectedItem.Text;
                objHousehold.Rightofway = txtrightofWay.Text.Trim();
                objHousehold.Wayleaves = txtWayleaves.Text.Trim();
                objHousehold.UpdatedBy = Convert.ToInt32(Session["USER_ID"]);

                message = (new AddPAPBLL()).UpdatePAP(objHousehold);

                //Edwin: 20SEP2016 Reload Pap Details
                ReCache(objHousehold.HhId);

                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Data Updated successfully";
                    ClearDetails();
                    SetUpdatemode(false);
                }
            }
            SetGridSource(true, -1);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
        }
        /// <summary>
        /// To clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearDetails();
            if (((Button)sender).Text.ToUpper() == "CANCEL")
            {
                SetUpdatemode(false);
            }
        }
        /// <summary>
        /// To Celar Data
        /// </summary>
        private void ClearDetails()
        {
            txtNameofPAP.Text = "";
            txtPapUid.Text = "";
            txtSurname.Text = "";
            txtfirstname.Text = "";
            txtOthername.Text = "";
            txtPlotReference.Text = "";
            txtDesignation.Text = "";
            ddlDistrict.ClearSelection();
            BindCounties(ddlDistrict.SelectedItem.Value);
            BindSubCounties(ddlCounty.SelectedItem.Value);
            uplSubCounty.Update();
            BindVillages(ddlSubCounty.SelectedItem.Value);
            uplVillage.Update();
            BindParishes(ddlSubCounty.SelectedItem.Value);
            uplParish.Update();
            txtrightofWay.Text = "";
            txtWayleaves.Text = "";
            txttotal.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
        }
        /// <summary>
        /// To Cancel Upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelUpload_Click(object sender, EventArgs e)
        {
            ViewState["IMPSTATUS"] = 0;
            pnlPAPInformation.Visible = true;
            pnlFileUpload.Visible = false;
            btnADDPAP.Visible = true;
            btnSearchPAP.Visible = true;
            EnableSaveCancel(false);
        }
        /// <summary>
        /// To MAke Pap as Obsolute
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
                ViewState["PAPHHID_ID"] = ((Literal)gr.FindControl("litHHID")).Text;
                AddPAPBLL objPAPBLL = new AddPAPBLL();
                message = objPAPBLL.ObsoletePAP(Convert.ToInt32(ViewState["PAPHHID_ID"]), Convert.ToString(chk.Checked));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                    message = "Data updated successfully";
                ClearDetails();
                SetGridSource(true, -1);
                SetUpdatemode(false);
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Buttons Events & Functions

        #region Gridview
        /// <summary>
        /// Set data to Grid 
        /// </summary>
        /// <param name="showRecentRecords"></param>
        /// <param name="newPageIndx"></param>
        protected void SetGridSource(bool showRecentRecords, int newPageIndx)
        {
            PAP_HouseholdBLL objPAPLogic = new PAP_HouseholdBLL();
            GrdPapInformation.DataSource = objPAPLogic.SearchPAPForALL(showRecentRecords,
                Convert.ToInt32(Session["PROJECT_ID"]),
                0,
                String.Empty,
                String.Empty,
                String.Empty,
                String.Empty,
                String.Empty,
                String.Empty,
                String.Empty,
                String.Empty);

            if (newPageIndx != -1) GrdPapInformation.PageIndex = newPageIndx;
            GrdPapInformation.DataBind();
            if (GrdPapInformation.Rows.Count > 0)
            {
                p1Grid.Visible = true;
            }
            else
                p1Grid.Visible = false;
        }
        /// <summary>
        /// To Edit and Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPapInformation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string message = string.Empty;
            try
            {
                if (e.CommandName == "EditRow")
                {
                    if (Session["FROZEN"] != null)
                    {
                        string SessionValue = Session["FROZEN"].ToString();
                        if (SessionValue == "Y")
                        {
                            lblMessage.Visible = true;
                        }
                        else
                            lblMessage.Visible = false;
                    }
                    else
                        lblMessage.Visible = false;

                    ViewState["PAPHHID_ID"] = e.CommandArgument;
                    pnlAddPAP.Visible = true;
                    pnlSearchPAP.Visible = false;
                    PAP_HouseholdBLL objHouseholdBLL = new PAP_HouseholdBLL();
                    PAP_HouseholdBO objHousehold = objHouseholdBLL.GetHouseHoldData(Convert.ToInt32(ViewState["PAPHHID_ID"]));
                    txtSurname.Text = Convert.ToString(objHousehold.Surname);
                    txtfirstname.Text = Convert.ToString(objHousehold.Firstname);
                    txtOthername.Text = Convert.ToString(objHousehold.Othername);
                    txtNameofPAP.Text = Convert.ToString(objHousehold.PapName);
                    txtPapUid.Text = Convert.ToString(objHousehold.Papuid);
                    txtPlotReference.Text = Convert.ToString(objHousehold.PlotReference);
                    ddlDistrict.ClearSelection();
                    if (ddlDistrict.Items.FindByText(Convert.ToString(objHousehold.District.ToUpper())) != null)
                        ddlDistrict.Items.FindByText(Convert.ToString(objHousehold.District.ToUpper())).Selected = true;
                    BindCounties(ddlDistrict.SelectedItem.Value);
                    ddlCounty.ClearSelection();
                    if (ddlCounty.Items.FindByText(Convert.ToString(objHousehold.County.ToUpper())) != null)
                        ddlCounty.Items.FindByText(Convert.ToString(objHousehold.County.ToUpper())).Selected = true;
                    BindSubCounties(ddlCounty.SelectedItem.Value);
                    uplSubCounty.Update();
                    ddlSubCounty.ClearSelection();
                    if (ddlSubCounty.Items.FindByText(Convert.ToString(objHousehold.SubCounty.ToUpper())) != null)
                        ddlSubCounty.Items.FindByText(Convert.ToString(objHousehold.SubCounty.ToUpper())).Selected = true;
                    BindVillages(ddlSubCounty.SelectedItem.Value);
                    uplVillage.Update();
                    ddlVillage.ClearSelection();
                    if (ddlVillage.Items.FindByText(Convert.ToString(objHousehold.Village.ToUpper())) != null)
                        ddlVillage.Items.FindByText(Convert.ToString(objHousehold.Village.ToUpper())).Selected = true;
                    BindParishes(ddlSubCounty.SelectedItem.Value);
                    uplParish.Update();
                    ddlParish.ClearSelection();
                    if (ddlParish.Items.FindByText(Convert.ToString(objHousehold.Parish.ToUpper())) != null)
                        ddlParish.Items.FindByText(Convert.ToString(objHousehold.Parish.ToUpper())).Selected = true;

                    txtDesignation.Text = Convert.ToString(objHousehold.Designation);
                    txtrightofWay.Text = Convert.ToString(objHousehold.Rightofway);
                    txtWayleaves.Text = Convert.ToString(objHousehold.Wayleaves);
                    txtLatitude.Text = Convert.ToString(objHousehold.Plotlatitude);
                    txtLongitude.Text = Convert.ToString(objHousehold.Plotlongitude);
                    if (txtrightofWay.Text.Trim().Length > 0 && txtWayleaves.Text.Trim().Length > 0)
                        txttotal.Text = (Convert.ToDouble(txtrightofWay.Text.Trim()) + Convert.ToDouble(txtrightofWay.Text.Trim())).ToString();
                    SetUpdatemode(true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Added", "setDirty();", true);
                    
                }
                else if (e.CommandName == "DeleteRow")
                {
                    AddPAPBLL objPAPBLL = new AddPAPBLL();
                    message = objPAPBLL.DeletePAP(Convert.ToInt32(e.CommandArgument));
                    if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                        message = "Data Deleted successfully";
                    else
                        message = "Selected PAP is already in use. Connot delete";
                    ClearDetails();
                    SetGridSource(true, -1);
                    SetUpdatemode(false);
                }
                else if (e.CommandName == "AddCoordinates")
                {
                    string[] id= e.CommandArgument.ToString().Split('|');
                    string HHID = id[0];
                    string PapName = id[1];
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'UploadPAPCoordinates.aspx?HHID=" + HHID + "&PapName=" + PapName + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                if (message != "")
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// To Change Page Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPapInformation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
            //string FileName = GrdPapInformation.Caption;
            //string Extension = System.IO.Path.GetExtension(FileName);
            //string FilePath = Server.MapPath(FolderPath + FileName);
            //Import_To_Grid(FilePath, Extension);
            //GrdPapInformation.PageIndex = e.NewPageIndex;
            //GrdPapInformation.DataBind();
            if (btnGridDataSave.Visible)
            {
                if (e.NewPageIndex != -1) GrdPapInformation.PageIndex = e.NewPageIndex;
                GrdPapInformation.DataSource = (DataTable)ViewState["EXCEL_DATA"];
                GrdPapInformation.DataBind();
            }
            else
                SetGridSource(true, e.NewPageIndex);
        }
        /// <summary>
        /// To Check Data Is Valid or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrdPapInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(ViewState["IMPSTATUS"]) == 1)
                {
                    StringBuilder sbCounty = (StringBuilder)ViewState["PAPCOUNTY"];
                    StringBuilder sbSubCounty = (StringBuilder)ViewState["PAPSUBCOUNTY"];
                    StringBuilder sbParish = (StringBuilder)ViewState["PAPPARISH"];
                    StringBuilder sbVillage = (StringBuilder)ViewState["PAPVILLAGE"];
                    string sPapUID = e.Row.Cells[3].Text;
                    string sSurName = e.Row.Cells[4].Text;
                    string sFirstName = e.Row.Cells[5].Text;
                    string sdi = e.Row.Cells[13].Text;
                    string scounty = e.Row.Cells[14].Text;
                    string sSubcounty = e.Row.Cells[15].Text;
                    string sParish = e.Row.Cells[16].Text;
                    string sVillage = e.Row.Cells[17].Text;
                    string sROW = e.Row.Cells[19].Text;
                    string sWL = e.Row.Cells[20].Text;
                    string splot = e.Row.Cells[10].Text;
                    string sdis = e.Row.Cells[12].Text;
                    if (splot.Trim() == "" || splot.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[9].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sdis.Trim() == "" || sdis.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[11].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sROW.Trim() == "" || sROW.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[18].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sWL.Trim() == "" || sWL.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[19].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sPapUID.Trim() == "" || sPapUID.Trim() =="&nbsp;")
                    {
                        e.Row.Cells[2].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sSurName.Trim() == "" || sSurName.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[3].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (sFirstName.Trim() == "" || sFirstName.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[4].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (ddlDistrict.Items.FindByText(sdi.ToUpper().Trim()) == null || sdi.Trim() == "" || sdi.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[12].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (!sbCounty.ToString().Contains("'" + scounty.ToUpper().Trim() + "'") || scounty.Trim() == "" || scounty.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[13].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (!sbSubCounty.ToString().ToUpper().Trim().Contains("'" + sSubcounty.ToUpper().Trim() + "'") || sSubcounty.Trim() == "" || sSubcounty.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[14].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (!sbParish.ToString().Contains("'" + sParish.ToUpper().Trim() + "'") || sParish.Trim() == "" || sParish.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[15].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    if (!sbVillage.ToString().Contains("'" + sVillage.ToUpper().Trim() + "'") || sVillage.Trim() == "" || sVillage.Trim() == "&nbsp;")
                    {
                        e.Row.Cells[16].BackColor = Color.FromArgb(215, 75, 75);
                        e.Row.BackColor = Color.LightPink;
                    }
                    //else
                    //    e.Row.BackColor = Color.Transparent;

                }
            }
        }

        #region Save/Cancel Gridview data
        /// <summary>
        /// To Save Grid View Data in to Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGridDataSave_Click(object sender, EventArgs e)
        {
            string message = "";
            string uID = Session["USER_ID"].ToString();
            int ProjectID = Convert.ToInt32(Session["PROJECT_ID"]);

            DataTable dtPap = (DataTable)ViewState["EXCEL_DATA"];

            AddPAPBLL oAddPapBLL = new AddPAPBLL();
            string Count = oAddPapBLL.SaveExcelData(dtPap, ProjectID, uID);
            ViewState["IMPSTATUS"] = 0;
            EnableSaveCancel(false);
            SetGridSource(true, -1);
            btnADDPAP.Visible = true;
            btnSearchPAP.Visible = true;
            if (!string.IsNullOrEmpty(Count))
            {
                string[] pap = Count.Split('|');
                if (pap.Length > 0)
                {
                    if (pap[0] == "0")
                        lblSpaps.Text = "No PAPs were imported.";
                    else
                        lblSpaps.Text = pap[0] + " - PAP(s) imported successfully out of " + dtPap.Rows.Count + " PAPS.";
                }
                else
                {
                    lblSpaps.Text = "";
                }

                if (pap.Length > 1)
                {
                    if (pap[1].Trim() != "") 
                        lblFpaps.Text = "Following PAPS (PAP UID) could not be imported: " + pap[1];
                    else
                        lblFpaps.Text = "";
                }
                else
                {
                    lblFpaps.Text = "";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDiv", "HideDiv();", true);
            }
        }
        /// <summary>
        /// To Cancel Imported Grid Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGridDataCancel_Click(object sender, EventArgs e)
        {
            ViewState["IMPSTATUS"] = 0;
            SetGridSource(true, -1);
            EnableSaveCancel(false);
            btnADDPAP.Visible = true;
            btnSearchPAP.Visible = true;
        }
        #endregion
        #endregion Gridview
        /// <summary>
        /// Check Frozen
        /// </summary>
        private void getFrozen()
        {
            if (Session["FROZEN"] != null)
            {
                string SessionValue = Session["FROZEN"].ToString();
                if (SessionValue == "Y")
                {
                    pnlSearchPAP.Visible = true;
                    pnlAddPAP.Visible = false;
                    btnADDPAP.Visible = false;
                    btnSearchPAP.Visible = false;
                    pnlPapDetails.Enabled = false;
                    btn_ImportExcel.Visible = false;
                    btn_Save.Visible = false;
                    btn_Clear.Visible = false;
                    lblMessage.Visible = true;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 1].Visible = false;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 2].Visible = false;
                    GrdPapInformation.Columns[GrdPapInformation.Columns.Count - 3].Visible = false;
                    GrdPapInformation.Columns[2].Visible = false;
                    GrdPapInformation.Columns[3].Visible = true;
                    GrdPapInformation.Columns[11].Visible = true;
                    GrdPapInformation.Columns[10].Visible = false;
                }
                else
                    pnlPapDetails.Visible = true;
                //pnlFileUpload.Visible = false;
            }
            else
            {
                pnlPapDetails.Visible = true;
            }
        }
        /// <summary>
        /// Frozen Propety
        /// </summary>
        private int Frozen
        {
            get
            {
                if (Session["FROZEN"] != null)
                {
                    string SessionValue = Session["FROZEN"].ToString();
                    if (SessionValue.ToUpper() == "Y")
                    {
                        return 1;
                    }
                    else
                        return 0;
                }
                return 0;
            }
        }

        //public void Resize_Array(int arrCount)
        //{           

        //    OracleConnection cnn = new OracleConnection(AppConfiguration.ConnectionString);

        //    string[] Slno = new string[0];
        //    string[] PAP = new string[0];
        //    string[] Plot_Ref = new string[0];
        //    string[] District = new string[0];
        //    string[] village = new string[0];
        //    string[] Right_of_way = new string[0];
        //    string[] Wayleaves = new string[0];
        //    string[] Total = new string[0];

        //    Array.Resize(ref Slno, arrCount + 1);
        //    Array.Resize(ref PAP, arrCount + 1);
        //    Array.Resize(ref Plot_Ref, arrCount + 1);
        //    Array.Resize(ref District, arrCount + 1);

        //    Array.Resize(ref village, arrCount + 1);
        //    Array.Resize(ref Right_of_way, arrCount + 1);
        //    Array.Resize(ref Wayleaves, arrCount + 1);
        //    Array.Resize(ref Total, arrCount + 1);

        //   //Resize_Array(1);
        //    Slno[0] = "1";
        //    PAP[0] = "2";
        //    Plot_Ref[0] = "3";
        //    District[0] = "4";
        //    village[0] = "5";
        //    Right_of_way[0] = "6";
        //    Wayleaves[0] = "7";
        //    Total[0] = "8";          


        //    OracleCommand objCom = new OracleCommand();            
        //    objCom.CommandType = CommandType.StoredProcedure;
        //    objCom.CommandText = "USP_TRN_INS_PAPHOUSEHOLD";
        //    // add the parameter you need to pass to the procedure
        //    //OracleParameter param1 = objCom.Parameters.Add("SlNO", Slno);
        //    OracleParameter param1 = objCom.Parameters.Add("PAPNAMEIN", PAP[0]);
        //    OracleParameter param2 = objCom.Parameters.Add("PLOTREFERENCEIN", Plot_Ref[0]);
        //    OracleParameter param3 = objCom.Parameters.Add("DISTRICTIN", District[0]);
        //    OracleParameter param4 = objCom.Parameters.Add("VILLAGEIN", village[0]);
        //    OracleParameter param5 = objCom.Parameters.Add("RIGHTWAYIN", Right_of_way[0]);
        //    OracleParameter param6 = objCom.Parameters.Add("WAYLEAVESIN", Wayleaves[0]);
        //    OracleParameter param7 = objCom.Parameters.Add("ISDELETEDIN", '1');
        //    OracleParameter param8 = objCom.Parameters.Add("USERIDIN", 1);

        //    param1.Size = 200;
        //    param2.Size = 100;
        //    param3.Size = 200;
        //    param4.Size = 100;

        //    param5.Size = 200;
        //    param6.Size = 100;
        //    param7.Size = 200;
        //    param8.Size = 100;           
        //    //param9.Size = 100;   
        //    objCom.ArrayBindCount = Slno.Length;
        //    cnn.Open();
        //    objCom.ExecuteNonQuery();
        //    cnn.Close();
        //}     

        //search for pap Add by Ramu.S
        /// <summary>
        /// To Search Pap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkSearchPAP_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        /// <summary>
        /// To Show Add PAP Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnADDPAP_Click(object sender, EventArgs e)
        {
            pnlAddPAP.Visible = true;
            pnlSearchPAP.Visible = false;
            ClearDetails();
            searchClear();
            SetUpdatemode(false);
            SetGridSource(true, -1);
        }
        /// <summary>
        /// To Show Search Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchPAP_Click(object sender, EventArgs e)
        {
            pnlAddPAP.Visible = false;
            pnlSearchPAP.Visible = true;
            ClearDetails();
            searchClear();
        }
        /// <summary>
        /// To Search Pap data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bindgid();
        }
        /// <summary>
        /// Set Data to Grid
        /// </summary>
        public void Bindgid()
        {
            string district = String.Empty;
            string county = String.Empty;
            string subCounty = String.Empty;
            string parish = String.Empty;
            string village = String.Empty;
            int HHid = 0;
            string PapUid = String.Empty;
            if (txtHHID.Text.Trim() != "")
            {
                HHid = Convert.ToInt32(txtHHID.Text.Trim());
            }
            if (txtPAPUIDSearch.Text.Trim() != "")
            {
                PapUid = txtPAPUIDSearch.Text.Trim();
            }


            if (ddlDistrictSearch.SelectedIndex > 0) district = ddlDistrictSearch.SelectedItem.Text;
            if (ddlCountySearch.SelectedIndex > 0) county = ddlCountySearch.SelectedItem.Text;
            if (ddlSubCountySearch.SelectedIndex > 0) subCounty = ddlSubCountySearch.SelectedItem.Text;
            if (ddlParishSearch.SelectedIndex > 0) parish = ddlParishSearch.SelectedItem.Text;
            if (ddlVillageSearch.SelectedIndex > 0) village = ddlVillageSearch.SelectedItem.Text;
            PAP_HouseholdBLL objPAPLogic = new PAP_HouseholdBLL();
            GrdPapInformation.DataSource = objPAPLogic.SearchPAPForALL(false,
                Convert.ToInt32(Session["PROJECT_ID"]),
                HHid,
                PapUid,
                txtPAPName.Text.Trim(),
                txtPlotReferenceSearch.Text.Trim(),
                district,
                county,
                subCounty,
                parish,
                village);

            //if (newPageIndx != -1) GrdPapInformation.PageIndex = newPageIndx;
            GrdPapInformation.DataBind();
            if (GrdPapInformation.Rows.Count > 0)
            {
                p1Grid.Visible = true;
            }
            else
                p1Grid.Visible = false;
        }
        /// <summary>
        /// To Clear Search Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            searchClear();
        }
        /// <summary>
        /// To Clear Search Data
        /// </summary>
        public void searchClear()
        {
            txtHHID.Text = "";
            txtPAPUIDSearch.Text = "";
            txtPAPName.Text = "";
            txtPlotReferenceSearch.Text = "";

            ListItem lstItem = null;

            lstItem = ddlVillageSearch.Items[0];
            ddlVillageSearch.Items.Clear();
            ddlVillageSearch.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlSubCountySearch.Items[0];
            ddlSubCountySearch.Items.Clear();
            ddlSubCountySearch.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlParishSearch.Items[0];
            ddlParishSearch.Items.Clear();
            ddlParishSearch.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            lstItem = ddlCountySearch.Items[0];
            ddlCountySearch.Items.Clear();
            ddlCountySearch.Items.Add(new ListItem(lstItem.Text, lstItem.Value));

            ddlDistrictSearch.ClearSelection();

            Bindgid();
        }

    }
}