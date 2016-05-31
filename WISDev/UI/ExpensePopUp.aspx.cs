using System;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using System.Web.UI;

namespace WIS
{
    public partial class ExpensePopUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
           
            }
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
                if (Extension.ToUpper().Contains(".xlsx".ToUpper()) || Extension.ToUpper().Contains(".xlsm".ToUpper()) ||
                    Extension.ToUpper().Contains(".xls".ToUpper()))
                {
                    string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPathExpense"];
                    string FilePath = FolderPath + FileName;
                    FileUpload.SaveAs(FilePath);
                    hdnFilePath.Value = FilePath;
                    ClientScript.RegisterStartupScript(this.GetType(), "UploadComplete", "ExpenseUploadCompleted();", true);
                    //Import_To_Grid(FilePath, Extension);

                    //AddPAPBLL objAddPAPBLL = new AddPAPBLL();
                    //objAddPAPBLL.ExcelDataImportintoGrid(pFileName, fileExtension, Convert.ToInt32(Session["PROJECT_ID"]), Convert.ToInt32(Session["USER_ID"]));

                    //SetGridSource(true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", "alert('Invalid File.Please Upload an Excel File');", true);
                }
            }
        }

        protected void btnCancelUpload_Click1(object sender, EventArgs e)
        {
            
        }

        

       

       
    }
}