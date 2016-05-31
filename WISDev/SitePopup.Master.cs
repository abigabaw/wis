using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WIS
{
    public partial class SitePopupMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["USER_ID"] == null)
            //{
            //    Response.Write("<script language='javascript'>alert('Session Expired. Please relogin.');window.close();</script>");
            //    Response.End();
            //}
        }

        public string PageHeader { set { lblPageHeader.Text = value; } }
    }
}
