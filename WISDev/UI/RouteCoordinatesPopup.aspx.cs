using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS.UI
{
    public partial class RouteCoordinatesPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Check uploaded file is valid or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string FileName = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName);
                string Extension = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName);
                string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPathRoute"];
                string FilePath = FolderPath + FileName;
                FileUpload.SaveAs(FilePath);
                hdnFilePath.Value = FilePath;
                ClientScript.RegisterStartupScript(this.GetType(), "UploadComplete", "RouteCoordinateUploadCompleted();", true);
                //Import_To_Grid(FilePath, Extension);

                //AddPAPBLL objAddPAPBLL = new AddPAPBLL();
                //objAddPAPBLL.ExcelDataImportintoGrid(pFileName, fileExtension, Convert.ToInt32(Session["PROJECT_ID"]), Convert.ToInt32(Session["USER_ID"]));

                //SetGridSource(true);
            }
        }
    }
}