using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;


namespace WIS
{
    public partial class ComparisionDataReports : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["PROJECT_CODE"] != null)
                    Master.PageHeader = Session["PROJECT_CODE"].ToString() + " - Comparison Report";
                else
                    Master.PageHeader = "Comparision Report";

                LandInfoBLL objLandInfoBLL = new LandInfoBLL();
                PublicLandInfoBO objLandInfo = objLandInfoBLL.GetLandInfo(Convert.ToInt32(Session["HH_ID"]));
                string LandType = "";

                if (objLandInfo != null)
                {
                    LandType = "CMPLNDINFOPUB";
                }
                else
                {
                    LandType = "CMPLNDINFOPRIV";
                }
                ddlReportType.Items.FindByValue("CMPLNDINFO").Value = LandType;
            }
        }
    }
}