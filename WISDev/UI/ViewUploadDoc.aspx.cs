using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Web.UI.HtmlControls;

namespace WIS.UI
{
    public partial class ViewUploadDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string papDocumentID = (Request.QueryString["papDocumentID"]);
            string ProjectCode = (Request.QueryString["ProjectCode"]);
            UploadDocumentBLL UploadDocumentBLLobj = new UploadDocumentBLL();
            UploadDocumentBO UploadDocumentBOObj = new UploadDocumentBO();
            string sTempPath = ConfigurationManager.AppSettings["TempDirectory"].ToString();
            UploadDocumentBOObj = UploadDocumentBLLobj.getFilePath(papDocumentID, ProjectCode);
            if (File.Exists(UploadDocumentBOObj.DocumentPath))
            {
                string sFileName = Path.GetFileName(UploadDocumentBOObj.DocumentPath);
                File.Copy(UploadDocumentBOObj.DocumentPath, sTempPath + sFileName, true);
                this.spncontent.Style.Add("display", "");
                this.contentPanel1.Attributes["src"] = ConfigurationManager.AppSettings["TempFolder"].ToString() + sFileName;
                lblMessage.Visible = false;
            }
            else
            {
                this.spncontent.Style.Add("display", "none");
                lblMessage.Visible = true;
            }
            
        }
    }
}