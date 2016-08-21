using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;

namespace WIS.UI
{
    public partial class UploadDocumentList : System.Web.UI.Page
    {
        /// <summary>
        /// Set page header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                Master.PageHeader = "View Uploaded Document";
                int DocserviceID = 0;
                int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                string ProjectCode = Request.QueryString["ProjectCode"].ToString();
                string DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
                try { DocserviceID = Convert.ToInt32(Request.QueryString["DOCSERVICEID"]); }
                catch { }
                if (DocserviceID > 0)
                {
                    txtDocserviceID.Text = DocserviceID.ToString();
                }
                BindGrid();
                ProjectCodeTextBox.Text = ProjectCode;
                HHIDTextBox.Text = HHID.ToString();
                PAP_HouseholdBLL objHouseholdLogic = new PAP_HouseholdBLL();
                PAP_HouseholdBO objHousehold = objHouseholdLogic.GetHouseHoldData(HHID);
                if (objHousehold != null)
                {
                    HHIDTextBoxDISP.Text = objHousehold.HhId.ToString();
                }
                ViewState["Search_Status"] = 0;
                ProjectCodeTextBox.Enabled = false;
                HHIDTextBox.Enabled = false;
            }
            this.contentPanel1.Attributes["src"] = null;
        }
       
        //Grid Page Change
        protected void ChangePage(object sender, GridViewPageEventArgs e)
        {
            grdUploadDocument.PageIndex = e.NewPageIndex;
            // Refresh the list
            if (ViewState["Search_Status"].ToString() == "0")
                BindGrid();
            else
                BindGridForSearch();
        }
        /// <summary>
        /// Bind Data to Grid
        /// </summary>
        private void BindGrid()
        {
            int HHID = 0;
            string DocumentCode = string.Empty;
            if ((Request.QueryString["HHID"]) != null)
            {
                HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            }

            if (Request.QueryString["DOCUMENT_CODE"] != null)
            {
                DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
            }
            else
            {
                DocumentCode = "All";
            }

            string ProjectCode = Request.QueryString["ProjectCode"].ToString();
            int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            int DocserviceID = 0;
            if (txtDocserviceID.Text.Trim().Length > 0)
                DocserviceID = Convert.ToInt32(txtDocserviceID.Text.Trim());
            //grdUploadDocument.DataSource = UploadDocumentBLLobj.GetALLUploadDocument(HHID, DocumentCode, ProjectID.ToString(), DocserviceID);
            //grdUploadDocument.DataBind();
            WIS_BusinessObjects.UploadDocumentList uplist = UploadDocumentBLLobj.GetALLUploadDocument(HHID, DocumentCode, ProjectID.ToString(), DocserviceID, Convert.ToInt32(Session["USER_ID"]));
            WIS_BusinessObjects.UploadDocumentList uplist1 = new WIS_BusinessObjects.UploadDocumentList();
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
            grdUploadDocument.AllowPaging = true;
        }

        /// <summary>
        /// Set target
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
        /// To vie Document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdUploadDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string paramChangeRequest = string.Empty;
            if (e.CommandName == "ViewRow")
            {
                int PAPDOCUMENTID = 0;

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                Literal LtlDocumentPath = (Literal)row.FindControl("LtlDocumentPath");

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
                                paramChangeRequest = string.Format("ViewUploadDocument({0},'{1}');", PAPDOCUMENTID, ProjectCode);
                                ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocumentView", paramChangeRequest, true);
                            }

                        }
                    }
                }
            }
              
            //if(!string.IsNullOrEmpty(paramChangeRequest))
            //    ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocument1", paramChangeRequest, true);
            #region unwantedCode
            //  //if (FilePath != null)
            //  //{
            //  string ProjectCode = Request.QueryString["ProjectCode"].ToString();
            //   paramChangeRequest = string.Format("ViewDocumentUploadBYHHID({0},'{1}');", PAPDOCUMENTID, ProjectCode);

            //  //ClientScript.RegisterStartupScript(this.GetType(), "ViewUploadDocument1", paramChangeRequest, true);

            //  //}

            ////  paramChangeRequest = string.Format("Hello();");//, PAPDOCUMENTID, ProjectCode);
            //}
            #endregion
        }

        /// <summary>
        /// To Search Documents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchUplaodButton_Click(object sender, EventArgs e)
        {
            BindGridForSearch();
            ViewState["Search_Status"] = 1;
        }

        /// <summary>
        /// To Search Documents and bind it to grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BindGridForSearch()
        {
            int HHID = 0;
            string DocumentCode = string.Empty;
            string KeyWord = string.Empty;

            if ((Request.QueryString["HHID"]) != null)
            {
                HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            }

            if (Request.QueryString["DOCUMENT_CODE"] != null)
            {
                DocumentCode = Request.QueryString["DOCUMENT_CODE"].ToString();
            }
            else
            {
                DocumentCode = "All";
            }
            int DocserviceID = 0;
            if (txtDocserviceID.Text.Trim().Length > 0)
                DocserviceID = Convert.ToInt32(txtDocserviceID.Text.Trim());

            string ProjectCode = Request.QueryString["ProjectCode"].ToString();
            int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
            if (txtSearchKeyword.Text.ToString().Trim() == string.Empty)
            {
                KeyWord = "ALL";
            }
            else
            {
                KeyWord = txtSearchKeyword.Text.ToString().Trim();
            }
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            //grdUploadDocument.DataSource = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, ProjectID.ToString(), DocserviceID);
            //grdUploadDocument.DataBind();
            WIS_BusinessObjects.UploadDocumentList uplist = UploadDocumentBLLobj.GetSearchDocument(KeyWord, HHID, DocumentCode, ProjectID.ToString(), DocserviceID, Convert.ToInt32(Session["USER_ID"]));
            WIS_BusinessObjects.UploadDocumentList uplist1 = new WIS_BusinessObjects.UploadDocumentList();
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
            grdUploadDocument.AllowPaging = true;
        }

        /// <summary>
        /// To clear search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadClear_Click(object sender, EventArgs e)
        {
            txtSearchKeyword.Text = string.Empty;
            ViewState["Search_Status"] = 0;
            BindGridForSearch();
        }

    }
}