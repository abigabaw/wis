using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;

namespace WIS.UI.PROJECT
{
    public partial class Project : System.Web.UI.Page
    {
        /// <summary>
        /// Set Page header,Call BindGrid() method
        /// Check User Permitions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            caldpProjStartDate.Format = UtilBO.DateFormat;
            caldpProjEndDate.Format = UtilBO.DateFormat;
        }
    }
}