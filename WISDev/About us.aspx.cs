using System;
using System.Web.UI;
using WIS_BusinessObjects;
using WIS_BusinessLogic;
using System.Text;

namespace WIS
{
    public partial class About_us : System.Web.UI.Page
    {
        /// <summary>
        /// set PAge Header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBuildVersion();
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = "About Us";
            }
        }

        /// <summary>
        /// Get Build Version
        /// </summary>
        private void getBuildVersion()
        {
            WIS_ConfigBO WIS_ConfigBO = new WIS_ConfigBO();
            WIS_ConfigBLL WIS_ConfigBLL = new WIS_ConfigBLL();
            WIS_ConfigBO = WIS_ConfigBLL.getBuildVersion();

            if (WIS_ConfigBO != null)
            {
                lblVersionBuild.Text = WIS_ConfigBO.BUILDVERSION.ToString();
                    lblDataVersion.Text = WIS_ConfigBO.BUILDDATE.ToString().Trim();
                    lblCopyright.Text = WIS_ConfigBO.BUILDCOPY.ToString().Trim();
            }
        }
    }
}