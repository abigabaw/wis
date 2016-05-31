using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class UploadDocPop : System.Web.UI.Page
    {
        public string contentType = string.Empty;

        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        UploadDocumentBO objUploadDocument;
        UploadDocumentBLL objUploadDocumentBLL;
        #endregion

        #region page load
        /// <summary>
        /// Check User permitions
        /// Set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Response.Cache.SetNoStore();            

            if (!IsPostBack)
            {
                Master.PageHeader = "Upload Document";
                btnShowUpload.Attributes.Add("onclick", "SetVisible(0);");
                btnShowSearch.Attributes.Add("onclick", "SetVisible(1);");
                int DocserviceID = 0;
                int ProjectID = 0;
                int userID = 0;

                if (Session["Project_ID"] != null)
                {
                    ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
                }

                if (Session["USER_ID"] != null)
                {
                    userID = Convert.ToInt32(Request.QueryString["userID"]);
                }

                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                
                string ProjectCode = Request.QueryString["ProjectCode"].ToString();
                string DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
                ViewState["PAPDOCUMENTID"] = 0;
                if (Request.QueryString["DOCSERVICEID"] != null)
                {
                    DocserviceID = Convert.ToInt32(Request.QueryString["DOCSERVICEID"]);
                }
                else
                {
                    DocserviceID = 0;
                }

                if (userID == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    userIDTextBox.Text = userID.ToString();
                }
                if (ProjectID == 0)
                {
                    ProjectID = 0;
                    upProjectIDTextBox.Text = "0";
                    ProjectCodeTextBox.Text = ProjectCode;
                    upProjectIDTextBox.Visible = false;
                }
                else
                {
                    upHHIDTextBox.Text = HHID.ToString();
                    PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
                    PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHousaeHoldData(HHID);
                    if (objHousehold != null)
                    {
                        upHHIDTextBoxDisp.Text = objHousehold.HhId.ToString();
                    }
                    ProjectCodeTextBox.Text = ProjectCode;
                }
                if (HHID == 0)
                {
                    HHID = 0;

                    upProjectIDTextBox.Text = ProjectID.ToString();
                    ProjectCodeTextBox.Text = ProjectCode;
                    //DocTypeDropDownList.Visible = false;
                    upProjectIDTextBox.Visible = false;
                    upHHIDTextBox.Visible = false;
                    upHHIDTextBoxDisp.Visible = false;
                }
                else
                {
                    upProjectIDTextBox.Text = ProjectID.ToString();
                    ProjectCodeTextBox.Text = ProjectCode;
                    upProjectIDTextBox.Visible = false;
                }
                if (DocserviceID > 0)
                {
                    txtDocserviceID.Text = DocserviceID.ToString();
                }
                BindGrid(false, false);
                screenIntilation();

                if (CheckAuthorization.HasUpdatePrivilege(UtilBO.PrivilegeCode.PRIV_PACKAGE_CLOSING_INFO) == false)
                {
                    SaveButton.Visible = false;
                    ClearButton.Visible = false;
                }
            }
            this.contentPanel1.Attributes["src"] = null;
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "MasterJS"))
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MasterJS",
                                                               CreateStartupScript());
            }
            // Response.Write(ProjectID);
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

        #region for Screen Load
        /// <summary>
        /// Bind Data to Drop downs
        /// </summary>
        public void screenIntilation()
        {
            objUploadDocumentBLL = new UploadDocumentBLL();
            objUploadDocument = new UploadDocumentBO();
            UploadDocumentList objUploadDocumentList = new UploadDocumentList();
            objUploadDocumentList = objUploadDocumentBLL.GetUploadDocument();

            DocTypeDropDownList.DataSource = objUploadDocumentList;//dt.Tables[0];
            DocTypeDropDownList.DataTextField = "DocumentType";
            DocTypeDropDownList.DataValueField = "DocumentTypeID";
            DocTypeDropDownList.DataBind();
            DocTypeDropDownList.Items.Insert(0, new ListItem("-- Select --", "0"));
            DocTypeDropDownList.SelectedIndex = 0;

            string DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
            DocTypeDropDownList.ClearSelection();
            if (DocumentCode.Length > 0)
            {
                for (int i = 0; i < objUploadDocumentList.Count; i++)
                {
                    if (DocumentCode == objUploadDocumentList[i].DocumentCode)
                    {
                        if (DocTypeDropDownList.Items.FindByValue(objUploadDocumentList[i].DocumentTypeID.ToString()) != null)
                        {
                            DocTypeDropDownList.Items.FindByValue(objUploadDocumentList[i].DocumentTypeID.ToString()).Selected = true;
                            DocTypeDropDownList.Enabled = false;
                        }
                        else
                        {
                            DocTypeDropDownList.Items[0].Selected = true;
                        }
                    }
                }
            }
            else
            {
                DocTypeDropDownList.Items[0].Selected = true;
            }
        }
        #endregion

        #region for save Document
        /// <summary>
        /// Save Document in to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Check to see if file was uploaded
            if (fileMyFile.PostedFile != null)
            {
                // Get a reference to PostedFile object
                HttpPostedFile myFile = fileMyFile.PostedFile;

                // Get size of uploaded file
                int nFileLen = myFile.ContentLength;

                // make sure the size of the file is > 0
                if (nFileLen > 0)
                {
                    // Allocate a buffer for reading of the file
                    byte[] myData = new byte[nFileLen];

                    // Read uploaded file from the Stream
                    myFile.InputStream.Read(myData, 0, nFileLen);

                    // Create a name for the file to store
                    string strFilename = Path.GetFileName(myFile.FileName);

                    // Write data into a file
                    int HHID = Convert.ToInt32(upHHIDTextBox.Text);
                    if (HHID <= 0)
                    {
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + ProjectCodeTextBox.Text + "\\" + Path.GetFileName(myFile.FileName);
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
                        string main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + ProjectCodeTextBox.Text + "\\" + upHHIDTextBox.Text + "\\" + Path.GetFileName(myFile.FileName);
                        if (File.Exists(main_strPath_user))
                        {
                            string NewFilename = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                            string fileexte = Path.GetExtension(myFile.FileName);
                            int i = 1;
                            string filecheck = Path.GetFileNameWithoutExtension(myFile.FileName) + "_Ver";
                            while (File.Exists(main_strPath_user))
                            {
                                NewFilename = filecheck + (i + 1).ToString();
                                main_strPath_user = ConfigurationManager.AppSettings["main_strPath_user"].ToString() + Session["PROJECT_CODE"].ToString() + "\\" + upHHIDTextBox.Text + "\\" + NewFilename + fileexte;
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
            string uID = userIDTextBox.Text;
            // userIDTextBox.Text 
            //Session["USER_ID"].ToString();
            string pID = upProjectIDTextBox.Text;
            //upProjectIDTextBox.Text;
            int HHID = Convert.ToInt32(upHHIDTextBox.Text);
            //upHHIDTextBox.Text

            // Store it in database
            HttpPostedFile myFile = fileMyFile.PostedFile; // to get user selected file
            string strFilename = Path.GetFileName(myFile.FileName);
            string ServerProposalFilePath = strNewFileName; // Store it in database file Name
            string filepath = System.IO.Path.GetFullPath(fileMyFile.PostedFile.FileName);
            //create Object for save Data
            objUploadDocument.HHID = HHID;
            objUploadDocument.ProjectID = Convert.ToInt32(upProjectIDTextBox.Text);
            if (txtDocserviceID.Text.Trim().Length > 0)
            {
                objUploadDocument.DOCSERVICEID = Convert.ToInt32(txtDocserviceID.Text.Trim());
            }
            else
            {
                objUploadDocument.DOCSERVICEID = 0;
            }
            objUploadDocument.KeyWord = txtKeyword.Text.ToString().Trim();
            objUploadDocument.Description = txtDescription.Text.ToString().Trim();
            objUploadDocument.DocumentTypeID = Convert.ToInt32(DocTypeDropDownList.SelectedItem.Value.ToString());
            objUploadDocument.DocumentPath = ServerProposalFilePath;
            objUploadDocument.UserID = Convert.ToInt32(uID);

            message = objUploadDocumentBLL.InsertUploadDocument(objUploadDocument);
            AlertMessage = "alert('" + message + "');";
            if (string.IsNullOrEmpty(message) || message == "" || message == "null")
            {
                message = "Document uploaded successfully";

                //Copy the file to file server path
                //pID 
                //ProjectCodeTextBox.Text 
                string projectName = ProjectCodeTextBox.Text;
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
                BindGrid(false, false);
                txtKeyword.Text = string.Empty;
                txtDescription.Text = string.Empty;

            }

            AlertMessage = "alert('" + message + "');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
        }
#endregion

        #region for Grid Bind 
        //Grid Page Change
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdUploadDocument.PageIndex = e.NewPageIndex;
            // Refresh the list
            if (pnlSearchDocument.Visible == true)
            {
                BindGridForSearch();
            }
            else
                BindGrid(true, false);
        }
        /// <summary>
        /// Bind Data to grid
        /// </summary>
        /// <param name="addRow"></param>
        /// <param name="deleteRow"></param>
        private void BindGrid(bool addRow, bool deleteRow)
        {

            int HHID;
            string ProjectID = "0";
            string DocumentCode = string.Empty;
            if ((Request.QueryString["HHID"]) != null)
            {
                HHID = Convert.ToInt32(Request.QueryString["HHID"]);

            }
            else
            {
                HHID = 0;
                DocumentCode = "All";
            }
            if ((Request.QueryString["DOCUMENT_CODE"]) != null)
            {
                DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
            }
            else
            {
                DocumentCode = "All";
            }
            if (Session["Project_ID"] != null)
            {
                ProjectID = Convert.ToString(Request.QueryString["ProjectID"]);
            }
            string ProjectCode = Request.QueryString["ProjectCode"].ToString();
            //Convert.ToInt32(Session["HHID"].ToString());
            int DocserviceID = 0;
            if (txtDocserviceID.Text.Trim().Length > 0)
                DocserviceID = Convert.ToInt32(txtDocserviceID.Text.Trim());
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            UploadDocumentList uplist = UploadDocumentBLLobj.GetALLUploadDocument(HHID, DocumentCode, ProjectID, DocserviceID, Convert.ToInt32(Session["USER_ID"]));
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            if (browser.Type.ToUpper().Contains("IE") || true)
            {
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
                                string path = ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + sFileName;
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
                                string path = ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + upbo.HHID.ToString() + "/" + sFileName;
                                upbo.DocumentPath = path;
                            }
                        }
                    }
                    uplist1.Add(upbo);
                }
                grdUploadDocument.DataSource = uplist1;
                grdUploadDocument.DataBind();
                grdUploadDocument.Columns[grdUploadDocument.Columns.Count-3].Visible = false;
            }
            else
            {
                grdUploadDocument.Columns[grdUploadDocument.Columns.Count - 2].Visible = false;
                grdUploadDocument.DataSource = uplist;
                grdUploadDocument.DataBind();
            }
        }

        #endregion

        #region Delete the upload Document
        /// <summary>
        /// Set target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUploadDocument_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HyperLinkUplod= e.Row.FindControl("HyperLink1") as HyperLink;
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
        /// view or delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUploadDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewRow")
            {
                int PAPDOCUMENTID = 0;

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal LtlDocumentPath = (Literal)row.FindControl("LtlDocumentPath");

                ViewState["PAPDOCUMENTID"] = e.CommandArgument;

                PAPDOCUMENTID = Convert.ToInt32(ViewState["PAPDOCUMENTID"].ToString());

                string FilePath = LtlDocumentPath.Text.ToString();

                string[] spiltValue = FilePath.Split('.');
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
                                    string path = ConfigurationManager.AppSettings["TempFolder"].ToString() + sFileName;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewFile", "ViewFile('" + path + "');", true);

                                }
                                else
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewFile", "alert('File does not exist in the file system.');", true);
                                //File.Copy(FilePath, sTempPath + sFileName, true);
                                //this.contentPanel1.Attributes["src"] = ConfigurationManager.AppSettings["TempFolder"].ToString() + sFileName;
                            }
                        }
                        else 
                        {
                            this.contentPanel1.Attributes["src"] = null;
                            if (FilePath != null)
                            {
                                string ProjectCode = Session["PROJECT_CODE"].ToString();
                                string paramChangeRequest = string.Format("ViewUploadDocument({0},'{1}');", PAPDOCUMENTID, ProjectCode);
                                ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocument", paramChangeRequest, true);
                            }

                        }
                    }
                }
            }
            if (e.CommandName == "DeleteRow")
            {
                string message = string.Empty;
                int PAPDOCUMENTID = 0;
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal LtlDocumentPath = (Literal)row.FindControl("LtlDocumentPath"); //Literal value avaiable in the grid
                Literal LilProjectCode = (Literal)row.FindControl("LilProjectCode");
                Literal LitHHID = (Literal)row.FindControl("LitHHID");

                ViewState["PAPDOCUMENTID"] = e.CommandArgument;
               

                if(ViewState["PAPDOCUMENTID"]!=null)
                    PAPDOCUMENTID = Convert.ToInt32(ViewState["PAPDOCUMENTID"].ToString());

                UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
                message = UploadDocumentBLLobj.DeleteDocument(Convert.ToInt32(ViewState["PAPDOCUMENTID"]), Convert.ToInt32(Session["USER_ID"]));
                if (string.IsNullOrEmpty(message) || message == "" || message == "null")
                {
                    message = "Document Deleted successfully";
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
                BindGrid(true, true);
            }
        }
        #endregion
        /// <summary>
        /// Show search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSearch_Click(object sender, EventArgs e)
        {
            pnlSearchDocument.Visible = true;
            pnlUploadDocuments.Visible = false;
            txtSearchKeyword.Text = string.Empty;
            BindGrid(true, false);
        }

        /// <summary>
        /// to show upload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowUpload_Click(object sender, EventArgs e)
        {
            pnlUploadDocuments.Visible = true;
            pnlSearchDocument.Visible = false;
            txtSearchKeyword.Text = string.Empty;
            BindGrid(true, false);
        }

        /// <summary>
        /// Call Bind Grid For Search to get data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchUplaodButton_Click(object sender, EventArgs e)
        {
            BindGridForSearch();
        }

        /// <summary>
        /// Bind Search Data
        /// </summary>
        private void BindGridForSearch()
        {
            int HHID;
            string DocumentCode = string.Empty;
            string KeyWord = string.Empty;
            if ((Request.QueryString["HHID"]) != null)
            {
                HHID = Convert.ToInt32(Request.QueryString["HHID"]);

            }
            else
            {
                HHID = 0;
                DocumentCode = "All";
            }
            if ((Request.QueryString["DOCUMENT_CODE"]) != null)
            {
                DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
            }
            else
            {
                DocumentCode = "All";
            }
            string ProjectCode = Request.QueryString["ProjectCode"].ToString();
            //Convert.ToInt32(Session["HHID"].ToString());
            int DocserviceID = 0;
            if (txtDocserviceID.Text.Trim().Length > 0)
                DocserviceID = Convert.ToInt32(txtDocserviceID.Text.Trim());
            if (txtSearchKeyword.Text.ToString().Trim() == string.Empty)
            {
                KeyWord = "ALL";
            }
            else
            {
                KeyWord = txtSearchKeyword.Text.ToString().Trim();
            }
            // = txtSearchKeyword.Text.ToString().Trim();
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            //grdUploadDocument.DataSource = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, upProjectIDTextBox.Text, DocserviceID);
            //grdUploadDocument.DataBind();
            UploadDocumentList uplist = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, upProjectIDTextBox.Text, DocserviceID, Convert.ToInt32(Session["USER_ID"]));
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            if (browser.Type.ToUpper().Contains("IE") || true)
            {
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
                                string path = ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + sFileName;
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
                                string path = ConfigurationManager.AppSettings["TempFolder"].ToString() + upbo.Projectcode + "/" + upbo.HHID.ToString() + "/" + sFileName;
                                upbo.DocumentPath = path;
                            }
                        }
                    }
                    uplist1.Add(upbo);
                }
                grdUploadDocument.DataSource = uplist1;
                grdUploadDocument.DataBind();
                grdUploadDocument.Columns[grdUploadDocument.Columns.Count - 3].Visible = false;
            }
            else
            {
                grdUploadDocument.Columns[grdUploadDocument.Columns.Count - 2].Visible = false;
                grdUploadDocument.DataSource = uplist;
                grdUploadDocument.DataBind();
            }
        }

        /// <summary>
        /// Clear Upload Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadClear_Click(object sender, EventArgs e)
        {
            txtSearchKeyword.Text = string.Empty;
            BindGrid(true, false);
        }

        /// <summary>
        /// To Clear data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
    }
}