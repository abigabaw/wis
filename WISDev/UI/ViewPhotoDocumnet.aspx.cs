using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class ViewPhotoDocumnet : System.Web.UI.Page
    {
        /// <summary>
        /// set Page Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
            {
                Master.PageHeader = "View Photo";
                int ProjectID = Convert.ToInt32(Request.QueryString["ProjectID"]);
                int HHID = Convert.ToInt32(Request.QueryString["HHID"]);
                int userID = Convert.ToInt32(Request.QueryString["userID"]);
                string ProjectCode = Request.QueryString["ProjectCode"].ToString();
                string PhotoModule = Request.QueryString["PhotoModule"].ToString();
                if (PhotoModule == "PAPPB" || PhotoModule == "PAPNPB" || PhotoModule == "DAMAGEDCROPS" || PhotoModule == "PAPCROP" || PhotoModule == "PAPGRAVE" || PhotoModule == "PAPFENCE" || PhotoModule == "PAPCP" || PhotoModule == "PAPOHFIX")
                {
                    string perStu = Request.QueryString["perStu"].ToString();
                }
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
            string PhotoModule = Request.QueryString["PhotoModule"].ToString();
            if (PhotoModule == "PAPPB" || PhotoModule == "PAPNPB" || PhotoModule == "DAMAGEDCROPS" || PhotoModule == "PAPCROP" || PhotoModule == "PAPGRAVE" || PhotoModule == "PAPFENCE" || PhotoModule == "PAPCP" || PhotoModule == "PAPOHFIX")
            {
                string perStu = Request.QueryString["perStu"].ToString();
                photoImage.ImageUrl = "~/ShowImage.ashx?photoModule=" + PhotoModule + "&id=" + HHID + "&perStuID=" + perStu + "&dt=" + DateTime.Now.ToString();
            }
            else
            {
                photoImage.ImageUrl = "~/ShowImage.ashx?photoModule=" + PhotoModule + "&id=" + HHID + "&perStuID=0" + "&dt=" + DateTime.Now.ToString();
            }
        }
    }
}