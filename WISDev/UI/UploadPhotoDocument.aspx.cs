using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Configuration;

namespace WIS.UI
{
    public partial class UploadPhotoDocument : System.Web.UI.Page
    {
        #region Declaration
        //  System.Data.DataTable dt = new System.Data.DataTable();
        UploadPhotoBO objUploadPhotoBO;
        UploadPhotoBLL objUploadPhotoBLL;
        #endregion
        /// <summary>
        /// Set page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.PageHeader = "Upload Photo";
                int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                int userID = Convert.ToInt32(Request.QueryString["userID"]);
                string ProjectCode = Request.QueryString["ProjectCode"].ToString();
                string PhotoModule = Request.QueryString["PhotoModule"].ToString();
                
                if (userID == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }
                GetPhoto();
            }
        }

        /// <summary>
        /// To get photo from data base
        /// </summary>
        private void GetPhoto()
        {
            int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
            int PagePBID = Convert.ToInt32(Request.QueryString["PagePBID"]);
            string PhotoModule = Request.QueryString["PhotoModule"].ToString();
            if (PhotoModule == "PAPPB" || PhotoModule == "PAPNPB" || PhotoModule == "DAMAGEDCROPS" || PhotoModule == "PAPCROP" || PhotoModule == "PAPGRAVE" || PhotoModule == "PAPFENCE" || PhotoModule == "PAPCP" || PhotoModule == "PAPOHFIX")
            {
                photoImage.ImageUrl = "~/ShowImage.ashx?photoModule=" + PhotoModule + "&perStuID=" + PagePBID + "&id=" + HHID;
            }
            else
            {
                photoImage.ImageUrl = "~/ShowImage.ashx?photoModule=" + PhotoModule + "&perStuID=0&id=" + HHID;
            }
            //~/ShowImage.ashx?photoModule=PAP&perStuID=0&id=
        }

        /// <summary>
        /// To Upload photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (photoFileUpload.HasFile)
            {
                string message = string.Empty;
                string AlertMessage = string.Empty;
                byte[] fileBytes = photoFileUpload.FileBytes;

                string FileName = photoFileUpload.FileName.ToString();
                int splitcount = 0;

                string[] spiltValue = FileName.Split('.');
                if (spiltValue.Length > 0)
                {
                    string s = spiltValue[spiltValue.Length - 1];

                    if (s.ToLower() == "jpg" || s.ToLower() == "gif" || s.ToLower() == "png")
                    {
                        splitcount = splitcount + 1;
                    }
                    else
                    {
                        splitcount = 0;
                    }
                }
                if (splitcount == 1)
                {
                    int fileSize = photoFileUpload.PostedFile.ContentLength;
                    Decimal MaxLength = Convert.ToDecimal(ConfigurationManager.AppSettings["UploadImageSize"].ToString());
                    if (fileSize <= MaxLength)
                    {
                        int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                        string PhotoModule = Request.QueryString["PhotoModule"].ToString();
                        int otherHHID = Convert.ToInt32(Request.QueryString["PagePBID"]);
                        objUploadPhotoBO = new UploadPhotoBO();
                        objUploadPhotoBLL = new UploadPhotoBLL();

                        objUploadPhotoBO.Hhid = HHID;
                        objUploadPhotoBO.Photo = fileBytes;
                        objUploadPhotoBO.UserID = Convert.ToInt32(Request.QueryString["userID"]);
                        objUploadPhotoBO.PhotoModule = PhotoModule;
                        objUploadPhotoBO.PKID = otherHHID;
                        message = objUploadPhotoBLL.InsertUploadPhoto(objUploadPhotoBO);

                        AlertMessage = "alert('" + message + "');";
                        if (string.IsNullOrEmpty(message) || message == "" || message == "null" || message == "-1")
                        {
                            message = "Photo uploaded successfully";
                            GetPhoto();
                        }
                        AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    }
                    else
                    {
                        message = "Cannot Upload. Uploaded file size exceeds the size limit.";

                        AlertMessage = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                    }
                }
                else
                {
                    message = "Please Upload Photo of type jpg or gif or png.";

                    AlertMessage = "alert('" + message + "');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Added", AlertMessage, true);
                }
            }
        }

        /// <summary>
        /// To close Current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PhotoModule"] == "PAP")
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "UPLOADPHOTO", "AfterUPLOADIMAGE();", true);
                ClientScript.RegisterStartupScript(this.GetType(), "UPLOADPHOTO", "AfterUPLOADIMAGEPAPINST();", true);
            }
            if (Request.QueryString["PhotoModule"] == "PAPINST" || Request.QueryString["PhotoModule"] == "PAPGROUP")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UPLOADPHOTO", "AfterUPLOADIMAGEPAPINST();", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UPLOADPHOTO", "AfterUPLOADIMAGE();", true);
            }
        }
    }
}