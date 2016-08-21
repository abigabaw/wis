using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WIS_BusinessLogic;
using WIS_BusinessObjects;
using System.Web.UI.HtmlControls;

namespace WIS
{
    public partial class SitePopupMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Mode = Request.QueryString["Mode"];

           if (Mode == "Response"){ LogOutBox.Style.Remove("display"); } 

            if (Session["userName"] != null){ userNameLabel.Text = "Welcome: " + Session["userName"].ToString(); }
            else { Response.Redirect("~/Login.aspx"); }

            //if (Session["USER_ID"] == null)
            //{
            //    Response.Write("<script language='javascript'>alert('Session Expired. Please relogin.');window.close();</script>");
            //    Response.End();
            //}
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            if (Session["UnlockStatus"] != null && Session["USER_ID"] != null)
            {
                SharedAuthorizationBO objbo1 = new SharedAuthorizationBO();
                objbo1.TRACKERHEADERID = Convert.ToInt32(Session["UnlockStatus"].ToString());
                objbo1.LockStatus = "N";
                objbo1.UpdateBy = Convert.ToInt32(Session["USER_ID"]);
                (new SharedApprovalsBLL()).UPdateLockStatus(objbo1);
            }

            Session.Clear();

            if (Session["USER_ID"] != null)
            {
                if (Cache["PRIV_" + Session["USER_ID"]] != null)
                {
                    Cache.Remove("PRIV_" + Session["USER_ID"]);
                }

                Session["USER_ID"] = null;
            }

            Session["userName"] = null;
            Session["PROJECT_ID"] = null;

            Response.Redirect("~/Login.aspx");
        }

        public string PageHeader { set { lblPageHeader.Text = value; } }
    }
}
