using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class UploadDocument : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        UploadDocumentBO objUploadDocument;
        UploadDocumentBLL objUploadDocumentBLL;
        #endregion

        string ProjectCode = string.Empty;

        #region for page load
        /// <summary>
        /// Check User permitions
        /// Set Page Header,set attributes to link buttons. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region for Read only add by anjan
            // For Read only format  
            string Mode = string.Empty;
            if (Request.QueryString["ProjectID"] != null)
            {
                Session["PROJECT_ID"] = Convert.ToInt32(Request.QueryString["ProjectID"]);
                if (Request.QueryString["HHID"] != null)
                {
                    Session["HH_ID"] = Convert.ToInt32(Request.QueryString["HHID"]);
                    PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
                    PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHousaeHoldData(Convert.ToInt32(Request.QueryString["HHID"]));

                    if (objHousehold != null)
                    {
                        Session["HH_IDForDisplay"] = objHousehold.HhId;
                    }
                }
                else
                {
                    Session["HH_ID"] = null;
                    Session["HH_IDForDisplay"] = null;
                }
                Session["PROJECT_CODE"] = Request.QueryString["ProjectCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
            }
            // end
            #endregion
            if (!IsPostBack)
            {
                Master.PageHeader = " Upload Documents";
                btnShowUpload.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                //if (Session["PROJECT_CODE"] != null)
                //{
                //    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Upload Document";
                //    ProjectCodeTextBox.Text = Session["PROJECT_CODE"].ToString();
                  
                //}
                //else
                //{   
                //    Response.Redirect("~/UI/Project/ViewProjects.aspx");
                //}

                BindProjects();
                if (Session["PROJECT_ID"] != null)
                {
                    drpProject.ClearSelection();
                    if (drpProject.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                        drpProject.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;

                    drpProjectsearch.ClearSelection();
                    if (drpProjectsearch.Items.FindByValue(Session["PROJECT_ID"].ToString()) != null)
                        drpProjectsearch.Items.FindByValue(Session["PROJECT_ID"].ToString()).Selected = true;

                    if (drpProject.SelectedIndex > 0)
                        imgSearch.Enabled = true;

                    if (drpProjectsearch.SelectedIndex > 0)
                        imgSearchHHID.Enabled = true;

                    if (Session["HH_ID"] == null)
                    {
                        HHIDTextBox.Text = "0";
                        txtHHID.Text = "0";
                        HHIDTextBoxDISP.Text = "0";
                        txtHHIDDISP.Text = "0";
                    }
                    else
                    {
                        HHIDTextBox.Text = Session["HH_ID"].ToString();
                        txtHHID.Text = Session["HH_ID"].ToString();
                    }
                    if (Session["HH_IDForDisplay"] == null)
                    {
                        HHIDTextBoxDISP.Text = "0";
                        txtHHIDDISP.Text = "0";
                    }
                    else
                    {
                        HHIDTextBoxDISP.Text = Session["HH_IDForDisplay"].ToString();
                        txtHHIDDISP.Text = Session["HH_IDForDisplay"].ToString();
                    }

                }
                if (Session["ModePos"] != null)
                {
                    if (Session["ModePos"].ToString() == "Search")
                    {
                        this.contentPanel1.Attributes["src"] = null;
                        pnlUploadDocuments.Visible = false;
                        pnlSearchDocument.Visible = true;
                    }
                    else
                    {
                        this.contentPanel1.Attributes["src"] = null;
                        pnlUploadDocuments.Visible = true;
                        pnlSearchDocument.Visible = false;
                    }
                }
                BindGrid();
                screenIntilation();
                this.contentPanel1.Attributes["src"] = null;

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.UPLOAD_DOCUMENT) == false)
                {
                    btnShowUpload.Visible = false;
                    btnShowSearch.Visible = false;
                    pnlSearchDocument.Visible = true;
                    pnlUploadDocuments.Visible = false;
                    grdUploadDocument.Columns[grdUploadDocument.Columns.Count - 1].Visible = false;
                }
            } 
            this.contentPanel1.Attributes["src"] = null;
            #region For Read only format Added By Mr. Anaja
            // For Read only format 
            if (Mode == "Readonly")
            {
                Master.PageHeader = " Supporting Documents";
                Label userNameLabel = (Label)Master.FindControl("userNameLabel");
                userNameLabel.Visible = false;
                LinkButton lnkLogout = (LinkButton)Master.FindControl("lnkLogout");
                lnkLogout.Visible = false;
                Menu NavigationMenu = (Menu)Master.FindControl("NavigationMenu");
                NavigationMenu.Visible = false;
                btnShowUpload.Visible = false;
                btnShowSearch.Visible = false;
                pnlSearchDocument.Visible = false;
                pnlUploadDocuments.Visible = false;
                grdUploadDocument.Columns[grdUploadDocument.Columns.Count - 1].Visible = false;
            }
            //End

            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// Set Default Button using Java script
        /// </summary>
        /// <returns></returns>
        private string CreateStartupScript()
        {
            StringBuilder stb = new StringBuilder();

            stb.Append("\n<script language=\"javascript\">\n<!--\n");

            stb.Append("var LOGIN_BUTTON_ID = \"");
            if (hfVisible.Value.Trim() == "1")
                stb.Append(btnShowSearch.ClientID);
            else
                stb.Append(btnShowUpload.ClientID);
            stb.Append("\";\n");

            stb.Append("-->\n</script>\n");

            return stb.ToString();
        }

        #region Screen load

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        private void BindProjects()
        {
            ProjectList list = (new ProjectBLL()).GetProjectNames(Convert.ToInt32(Session["USER_ID"]));
            drpProject.DataSource = list;
            drpProject.DataTextField = "ProjectName";
            drpProject.DataValueField = "ProjectID";
            drpProject.DataBind();

            drpProjectsearch.DataSource = list;
            drpProjectsearch.DataTextField = "ProjectName";
            drpProjectsearch.DataValueField = "ProjectID";
            drpProjectsearch.DataBind();
        }

        /// <summary>
        /// Bind Data to Drop Downs
        /// </summary>
        public void screenIntilation()
        {
            objUploadDocumentBLL = new UploadDocumentBLL();
            objUploadDocument = new UploadDocumentBO();
            UploadDocumentList objUploadDocumentList = new UploadDocumentList();
            objUploadDocumentList = objUploadDocumentBLL.GetUploadDocument();

            ListItem lstItem = DocTypeDropDownList.Items[0];
            DocTypeDropDownList.Items.Clear();

            DocTypeDropDownList.DataSource = objUploadDocumentList;//dt.Tables[0];
            DocTypeDropDownList.DataTextField = "DocumentType";
            DocTypeDropDownList.DataValueField = "DocumentTypeID";
            DocTypeDropDownList.DataBind();
            DocTypeDropDownList.Items.Insert(0, lstItem);
        }
        #endregion

        // Writes file to current folder
        private void WriteToFile(string strNewFileName, ref byte[] Buffer)
        {
           //Decleraton local Varabiles
            string message = string.Empty;
            string AlertMessage = string.Empty;

            //created object to BO layer and BLL layer
            objUploadDocument = new UploadDocumentBO();
            objUploadDocumentBLL = new UploadDocumentBLL();

            //get the value using seesion and UI Screen
            string uID = Session["USER_ID"].ToString();
            string pID = Session["PROJECT_ID"].ToString();
            //;
            string ProjectCode = Session["PROJECT_CODE"].ToString();
            //Session["PROJECT_CODE"].ToString();
            int HHID = 0;
            if (Session["HH_ID"] == null)
            {
                HHID = 0;
            }
            else
            {
                HHID = Convert.ToInt32(Session["HH_ID"].ToString());
            }
            //;

            // Store it in database
            HttpPostedFile myFile = fileMyFile.PostedFile; // to get user selected file
            string strFilename = Path.GetFileName(myFile.FileName);
            string ServerProposalFilePath = strNewFileName; // Store it in database file Name

            objUploadDocument.HHID = HHID;
            objUploadDocument.ProjectID = Convert.ToInt32(pID);
            objUploadDocument.DocumentTypeID = Convert.ToInt32(DocTypeDropDownList.SelectedItem.Value.ToString());
            objUploadDocument.DocumentPath = ServerProposalFilePath;  
            objUploadDocument.UserID = Convert.ToInt32(uID);
            objUploadDocument.KeyWord = txtKeyword.Text.ToString().Trim();
            
            string strMax = txtDescription.Text.ToString().Trim();
            if (strMax.Trim().Length >= 500)
            {
                strMax = txtDescription.Text.ToString().Trim().Substring(0, 500);
            }

            objUploadDocument.Description = strMax;

            message = objUploadDocumentBLL.InsertUploadDocument(objUploadDocument);
            AlertMessage = "alert('" + message + "');";
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Document uploaded successfully";

                //Copy the file to file server path
                string projectName = ProjectCode;
                string activeDir = ConfigurationManager.AppSettings["activeDir"].ToString(); //Path is defined in webapp
                string newPath = System.IO.Path.Combine(activeDir, projectName);
                if (!Directory.Exists(newPath))
                    System.IO.Directory.CreateDirectory(newPath);
                string newFileName = System.IO.Path.GetRandomFileName();
                newPath = System.IO.Path.Combine(newPath, newFileName);

                //string strPath_user = ConfigurationManager.AppSettings["strPath_user"].ToString() + projectName + "\\" + strFilename;
                //string cpy_strPath_user = ConfigurationManager.AppSettings["cpy_strPath_user"].ToString() + projectName + "\\";
                //File.Copy(strPath_user, cpy_strPath_user + strNewFileName, true);

                // To save file in destination path
                if (HHID == 0)
                {
                    string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + projectName + "\\";
                    fileMyFile.SaveAs(main_strPath_user + strNewFileName);
                }
                else
                {
                    string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + projectName + "\\" + HHID.ToString() + "\\";
                    newPath = System.IO.Path.Combine(activeDir, projectName);
                    string HHIDpath = System.IO.Path.Combine(newPath, HHID.ToString());
                    if (!Directory.Exists(HHIDpath))
                        System.IO.Directory.CreateDirectory(HHIDpath);
                    fileMyFile.SaveAs(main_strPath_user + strNewFileName);
                }
                // end
                BindGrid();
                Clear();
             }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
       
        //Grid Page Change
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdUploadDocument.PageIndex = e.NewPageIndex;
            // Refresh the list
            BindGridForSearch();
        }
        #region Donot Used
        //when user change project name its uesd to featch other Data
        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpProject.SelectedIndex > 0)
            {
                ProjectBO ProjectBOobj = (new ProjectBLL()).GetProjectByProjectID(Convert.ToInt32(drpProject.SelectedValue));
                drpProjectsearch.ClearSelection();
                drpProjectsearch.SelectedIndex = drpProject.SelectedIndex;
                Session["PROJECT_ID"] = drpProject.SelectedValue;
                Session["PROJECT_CODE"] = ProjectBOobj.ProjectCode;
                Session["HH_ID"] = null;
                HHIDTextBox.Text = "0";
                HHIDTextBoxDISP.Text = "0";
                imgSearch.Enabled = true;
                BindGridForSearch();
            }
            else
                imgSearch.Enabled = false;
        }
        #endregion
        //reload project name and project code to the text box
        protected void drpProjectsearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpProjectsearch.SelectedIndex > 0)
            {
                ProjectBO ProjectBOobj = (new ProjectBLL()).GetProjectByProjectID(Convert.ToInt32(drpProjectsearch.SelectedValue));
                Session["PROJECT_ID"] = drpProjectsearch.SelectedValue;
                Session["PROJECT_CODE"] = ProjectBOobj.ProjectCode;
                Session["HH_ID"] = null;
                HHIDTextBox.Text = "";
                HHIDTextBoxDISP.Text = "";
                imgSearchHHID.Enabled = true;
                BindGridForSearch();
            }
            else
                imgSearchHHID.Enabled = false;
        }

        #region for Bind grid 
          //if the user search the record and other information
        private void BindGrid()
        {
            int HHID = 0;
            if (Session["HH_ID"] != null)
                HHID = Convert.ToInt32(Session["HH_ID"]);
            else
                HHID = 0;
            string projectID = drpProject.SelectedValue;
            string DocumentCode = string.Empty;
            //if (drpProject.SelectedIndex > 0)
            //{
            //    if (HHIDTextBox.Text.Trim().Length > 0)
            //        HHID = Convert.ToInt32(HHIDTextBox.Text.Trim());
            //}

            DocumentCode = "All";
          
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            //grdUploadDocument.DataSource = UploadDocumentBLLobj.GetALLUploadDocument(HHID, DocumentCode, projectID, 0);
            //grdUploadDocument.DataBind();
            UploadDocumentList uplist = UploadDocumentBLLobj.GetALLUploadDocument(HHID, DocumentCode, projectID, 0, Convert.ToInt32(Session["USER_ID"]));
            UploadDocumentList uplist1 = new UploadDocumentList();
            foreach (UploadDocumentBO upbo in uplist)
            {
                string FilePath = upbo.DocumentPath;
                if (FilePath != null)
                {
                    if (upbo.HHID <= 0)
                    {
                        string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\");
                        string sFileName = Path.GetFileName(FilePath);
                        if (File.Exists(FilePath))
                        {
                            File.Copy(FilePath, sTempPath + upbo.Projectcode + @"\" + sFileName, true);
                            string path = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + sFileName;
                            upbo.DocumentPath = path;
                        }
                    }
                    else
                    {
                        string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\");
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\");
                        string sFileName = Path.GetFileName(FilePath);
                        if (File.Exists(FilePath))
                        {
                            File.Copy(FilePath, sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\" + sFileName, true);
                            string path = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + upbo.HHID.ToString() + "/" + sFileName;
                            upbo.DocumentPath = path;
                        }
                    }
                }
                uplist1.Add(upbo);
            }
            grdUploadDocument.DataSource = uplist1;
            grdUploadDocument.DataBind();
            grdUploadDocument.AllowPaging = true;
        }
        #endregion
        #region Delete the upload Document
        /// <summary>
        /// Set targrt to links 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUploadDocument_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HyperLinkUplod = e.Row.FindControl("HyperLink1") as HyperLink;
                Literal LtlDocumentPath = e.Row.FindControl("LtlDocumentPath") as Literal;
                if (!string.IsNullOrEmpty(LtlDocumentPath.Text))
                {
                    string sFileName = Path.GetExtension(LtlDocumentPath.Text);
                    if (UtilBO.CheckExtection(sFileName))
                    {
                        //HyperLinkUplod.Target = "";
                    }
                    else
                        HyperLinkUplod.Target = "_blank";
                }
            }
        }

        /// <summary>
        /// To view or delete document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUploadDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRow")
            {
                int PAPDOCUMENTID = 0;
                this.contentPanel1.Attributes["src"] = null;

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal LtlDocumentPath = (Literal)row.FindControl("LtlDocumentPath");
                Literal LtlProjectCode = (Literal)row.FindControl("LtlProjectCode");

                ViewState["PAPDOCUMENTID"] = e.CommandArgument;

                PAPDOCUMENTID = Convert.ToInt32(ViewState["PAPDOCUMENTID"].ToString());

                string FilePath = LtlDocumentPath.Text.ToString();

                string[] spiltValue  = FilePath.Split('.');
                if (spiltValue.Length > 0)
                {
                    string s = spiltValue[spiltValue.Length - 1];
                    if (s.Length > 0)
                    {
                        if (s.Trim().ToLower() != "")// "tif")
                        {
                            if (FilePath != null)
                            {
                                string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
                                string sFileName = Path.GetFileName(FilePath);
                                if (File.Exists(FilePath))
                                {
                                    File.Copy(FilePath, sTempPath + sFileName, true);
                                    string path = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + sFileName;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewFile", "ViewFile('" + path + "');", true);

                                }
                                else
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewFile", "alert('File does not exist in the file system.');", true);
                                //this.contentPanel1.Attributes["src"] = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + sFileName;                                
                            }
                        }
                        else
                        {
                            this.contentPanel1.Attributes["src"] = null;
                            if (FilePath != null)
                            {
                                string ProjectCode = Session["PROJECT_CODE"].ToString();
                                string paramChangeRequest = string.Format("ViewUploadDocument({0},'{1}');", PAPDOCUMENTID, LtlProjectCode.Text);
                                ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocument", paramChangeRequest, true);
                            }

                        }
                    }
                }
                //foreach (string s in spiltValue)
                //{
                //    if (s.Trim().ToLower() == "tif")
                //    {
                //        if (FilePath != null)
                //        {
                //            //File.OpenRead(FilePath);
                //            this.contentPanel1.Attributes["src"] = FilePath.ToString();
                //            //FileStream fs = File.Open(FilePath, FileMode.Open);
                           
                //        }
                //    }
                //    else if (s.Trim().ToLower() == "pdf" || s.Trim().ToLower() == "txt" || s.Trim().ToLower() == "JPG" || s.Trim().ToLower() == "gif" || s.Trim().ToLower() == "jpeg" || s.Trim().ToLower() == "png"
                //        || s.Trim().ToLower() == "doc" || s.Trim().ToLower() == "xlxs" || s.Trim().ToLower() == "docx" || s.Trim().ToLower() == "xlx")
                //    {
                //        this.contentPanel1.Attributes["src"] = null;
                //        if (FilePath != null)
                //        {
                //            string ProjectCode = Session["PROJECT_CODE"].ToString();
                //            string paramChangeRequest = string.Format("ViewUploadDocument({0},'{1}')", PAPDOCUMENTID, ProjectCode);
                //            ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocument", paramChangeRequest, true);
                //        }
                       
                //    }
                //}
            }
            if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal LtlDocumentPath = (Literal)row.FindControl("LtlDocumentPath"); //Literal value avaiable in the grid
                Literal LilProjectCode = (Literal)row.FindControl("LilProjectCode");
                Literal LitHHID = (Literal)row.FindControl("LitHHID");

                int PAPDOCUMENTID = Convert.ToInt32(e.CommandArgument.ToString());

                UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
                message = UploadDocumentBLLobj.DeleteDocument(PAPDOCUMENTID, Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Document deleted successfully";
                    try
                    {
                        string FilePath = LtlDocumentPath.Text.ToString();
                        string sFileName = Path.GetFileName(FilePath);
                        if (LitHHID.Text.Trim() == "0")
                        {
                            string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + LilProjectCode.Text.ToString() + "\\" + sFileName;
                            if (main_strPath_user != null)
                            {
                                if (File.Exists(main_strPath_user))
                                    File.Delete(main_strPath_user);
                            }
                        }
                        else
                        {
                            string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + LilProjectCode.Text.ToString() + "\\" + LitHHID.Text.ToString() + "\\" + sFileName;
                            if (main_strPath_user != null)
                            {
                                if (File.Exists(main_strPath_user))
                                    File.Delete(main_strPath_user);
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + ex + "');", true);
                    }
                }

                if (message != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('" + message + "');", true);                    
                }
                BindGrid();
            }
        }
        #endregion

        #region to clear the Data
        //Button action 
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            Clear(); //Funtion clear Call
            HHIDTextBox.Text = "0";
            HHIDTextBoxDISP.Text = "0";
            BindGrid();
        }

        //function call For clear Data
        public void Clear()
        {
            this.contentPanel1.Attributes["src"] = null;   
            screenIntilation();
            txtKeyword.Text = string.Empty;
            txtDescription.Text = string.Empty;
            HHIDTextBox.Text = "0";
        }
#endregion

        #region for Tab Button acton
        /// <summary>
        /// to show upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowUpload_Click(object sender, EventArgs e)
        {
            this.contentPanel1.Attributes["src"] = null;
            pnlUploadDocuments.Visible = true;
            pnlSearchDocument.Visible = false;
            txtSearchKeyword.Text = string.Empty;
            txtKeyword.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtHHID.Text = "0";
            txtHHIDDISP.Text = "0";
            HHIDTextBox.Text = "0";
            HHIDTextBoxDISP.Text = "0";
            drpProject.ClearSelection();
            drpProject.SelectedIndex = 0;
            drpProjectsearch.ClearSelection();
            drpProjectsearch.SelectedIndex = 0;
            imgSearch.Enabled = false;
            imgSearchHHID.Enabled = false;
            BindGridForSearch();
        }
        /// <summary>
        /// to show search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            this.contentPanel1.Attributes["src"] = null;
            pnlUploadDocuments.Visible = false;
            pnlSearchDocument.Visible = true;
            txtSearchKeyword.Text = string.Empty;
            txtKeyword.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtHHID.Text = string.Empty;
            txtHHIDDISP.Text = string.Empty;
            HHIDTextBox.Text = string.Empty;
            HHIDTextBoxDISP.Text = string.Empty;
            drpProject.ClearSelection();
            drpProject.SelectedIndex = 0;
            drpProjectsearch.ClearSelection();
            drpProjectsearch.SelectedIndex = 0;
            imgSearch.Enabled = false;
            imgSearchHHID.Enabled = false;
            BindGridForSearch();
        }
        #endregion

        #region for Bind data
        /// <summary>
        /// bind grid data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchUplaodButton_Click(object sender, EventArgs e)
        {
            this.contentPanel1.Attributes["src"] = null;
            BindGridForSearch();
            txtSearchKeyword.Text = string.Empty;
        }

        /// <summary>
        /// bind grid data for search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BindGridForSearch()
        {
            Int32 HHID = 0;
            if (txtHHID.Text.Trim().Length > 0)
                HHID = Convert.ToInt32(txtHHID.Text);

            string DocumentCode = string.Empty;
            string KeyWord = string.Empty;
            DocumentCode = "All";

            if (txtSearchKeyword.Text.ToString().Trim() == string.Empty)
            {
                KeyWord = "ALL";
            }
            else
            {
                KeyWord = txtSearchKeyword.Text.ToString().Trim();
            }
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            //grdUploadDocument.DataSource = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, drpProjectsearch.SelectedValue, 0);
            //grdUploadDocument.DataBind();
            UploadDocumentList uplist = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, drpProjectsearch.SelectedValue, 0, Convert.ToInt32(Session["USER_ID"]));
            UploadDocumentList uplist1 = new UploadDocumentList();
            foreach (UploadDocumentBO upbo in uplist)
            {
                string FilePath = upbo.DocumentPath;
                if (FilePath != null)
                {
                    if (upbo.HHID <= 0)
                    {
                        string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\");
                        string sFileName = Path.GetFileName(FilePath);
                        if (File.Exists(FilePath))
                        {
                            File.Copy(FilePath, sTempPath + upbo.Projectcode + @"\" + sFileName, true);
                            string path = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + sFileName;
                            upbo.DocumentPath = path;
                        }
                    }
                    else
                    {
                        string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\");
                        if (!Directory.Exists(sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\"))
                            System.IO.Directory.CreateDirectory(sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\");
                        string sFileName = Path.GetFileName(FilePath);
                        if (File.Exists(FilePath))
                        {
                            File.Copy(FilePath, sTempPath + upbo.Projectcode + @"\" + upbo.HHID.ToString() + @"\" + sFileName, true);
                            string path = "../" + ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + upbo.HHID.ToString() + "/" + sFileName;
                            upbo.DocumentPath = path;
                        }
                    }
                }
                uplist1.Add(upbo);
            }
            grdUploadDocument.DataSource = uplist1;
            grdUploadDocument.DataBind();
            grdUploadDocument.AllowPaging = true;
        }
       
        /// <summary>
        /// to clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadClear_Click(object sender, EventArgs e)
        {
            this.contentPanel1.Attributes["src"] = null;
            txtSearchKeyword.Text = string.Empty;
            txtKeyword.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtHHID.Text = string.Empty;
            txtHHIDDISP.Text = string.Empty;
            HHIDTextBox.Text = string.Empty;
            HHIDTextBoxDISP.Text = string.Empty;
            drpProject.ClearSelection();
            drpProject.SelectedIndex = 0;
            drpProjectsearch.ClearSelection();
            drpProjectsearch.SelectedIndex = 0;
            imgSearch.Enabled = false;
            imgSearchHHID.Enabled = false;
            BindGridForSearch();
        }

        /// <summary>
        /// to search pap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton b1=(ImageButton) sender;
            if (b1.ID == "imgSearchHHID")
            {
                Session["ModePos"] = "Search";
            }
            else
            {
                Session["ModePos"] = "Upload";
            }
           ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../COMPENSATION/PopUpPAPList.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
           
        }

        /// <summary>
        /// to upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            this.contentPanel1.Attributes["src"] = null;
            upload();
        }
        /// <summary>
        /// To upload documes
        /// </summary>
        public void upload()
        {
           if (fileMyFile.PostedFile != null)
            {
            //    // Get a reference to PostedFile object
               HttpPostedFile myFile = fileMyFile.PostedFile;

            //    // Get size of uploaded file
               int nFileLen = myFile.ContentLength;

            //    // make sure the size of the file is > 0
                if (nFileLen > 0)
                {
            //        // Allocate a buffer for reading of the file
                 byte[] myData = new byte[nFileLen];

            //        // Read uploaded file from the Stream
                 myFile.InputStream.Read(myData, 0, nFileLen);

            //        // Create a name for the file to store
                   string strFilename = Path.GetFileName(myFile.FileName);

            //        // Write data into a file
                   int HHID = 0;
                   if (Session["HH_ID"] == null)
                   {
                       HHID = 0;
                   }
                   else
                   {
                       HHID = Convert.ToInt32(Session["HH_ID"].ToString());
                   }
                   if (HHID <= 0)
                   {
                       string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + Session["PROJECT_CODE"].ToString() + "\\" + Path.GetFileName(myFile.FileName);
                       if (File.Exists(main_strPath_user))
                       {
                           string NewFilename = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                           string fileexte = Path.GetExtension(myFile.FileName);
                           int i = 1;
                           string filecheck = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                           while (File.Exists(main_strPath_user))
                           {
                               NewFilename = filecheck + (i + 1).ToString();
                               main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + Session["PROJECT_CODE"].ToString() + "\\" + NewFilename + fileexte;
                               i++;
                           }
                           WriteToFile(NewFilename + fileexte, ref myData);
                           //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alertmes", "alert('File with the same name already exist in the file system.Rename your file and try again.');", true);
                       }
                       else
                           WriteToFile(strFilename, ref myData);
                   }
                   else
                   {
                       string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + Session["PROJECT_CODE"].ToString() + "\\" + HHID.ToString() + "\\" + Path.GetFileName(myFile.FileName);
                       if (File.Exists(main_strPath_user))
                       {
                           string NewFilename = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                           string fileexte = Path.GetExtension(myFile.FileName);
                           int i = 1;
                           string filecheck = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                           while (File.Exists(main_strPath_user))
                           {
                               NewFilename = filecheck + (i + 1).ToString();
                               main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + Session["PROJECT_CODE"].ToString() + "\\" + HHID.ToString() + "\\" + NewFilename + fileexte;
                               i++;
                           }
                           WriteToFile(NewFilename + fileexte, ref myData);
                           //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alertmes", "alert('File with the same name already exist in the file system.Rename your file and try again.');", true);
                       }
                       else
                           WriteToFile(strFilename, ref myData);
                   }
               }

           }
        }
        #endregion

    }
}