using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS.UI.MYACTIVITIES
{
    public partial class Unlock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["HeaderID"] != null)
            {
                SharedAuthorizationBO objbo1 = new SharedAuthorizationBO();
                objbo1.TRACKERHEADERID = Convert.ToInt32(Request.QueryString["HeaderID"]);
                objbo1.LockStatus = "N";
                objbo1.UpdateBy = Convert.ToInt32(Session["USER_ID"]);
                (new SharedApprovalsBLL()).UPdateLockStatus(objbo1);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLock", "window.close();", true);
            }
        }
    }
}