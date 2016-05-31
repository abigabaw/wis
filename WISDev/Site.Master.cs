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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        RolePrivilegesBO result = null;
        RolePrivilegesList PrivilegeList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] != null)
            {
                userNameLabel.Text = "Welcome " + Session["userName"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            CheckPrivilege();
            /*
             System.Web.HttpBrowserCapabilities browser = Request.Browser;
             //A link tag to external CSS file
             HtmlLink linkCss = new HtmlLink();
             if (browser.Browser == "IE" || browser.Browser == "Firefox" || browser.Browser == "Google Chorme")
            {   
                //Defining attributes and values of the link tag
                linkCss.Attributes.Add("href", "Styles/MenuCSSForIE.css");
            }
             if (browser.Browser == "Safari" || browser.Browser == "Chrome" )
            {
                //Defining attributes and values of the link tag
                linkCss.Attributes.Add("href", "Styles/Site.css");
            }
            linkCss.Attributes.Add("type", "text/css");
            linkCss.Attributes.Add("rel", "Stylesheet");
            //Add HtmlLink instance to the header of the current page
            Page.Header.Controls.Add(linkCss);
            */
        }

        /// <summary>
        /// Chek User permitions and set his menu controls
        /// </summary>
        private void CheckPrivilege()
        {
            RolePrivilegesBLL privBLL = new RolePrivilegesBLL();
            PrivilegeList = privBLL.GetROLEPRIId(Convert.ToInt32(Session["USER_ID"]));
            
            MenuItem navMenuItem = null;

            for(int mnuItmIDX=NavigationMenu.Items.Count-1; mnuItmIDX > 0; mnuItmIDX--)
            {
                navMenuItem = NavigationMenu.Items[mnuItmIDX];
                CheckChildNavigationMenus(navMenuItem);
                if (navMenuItem.Value.ToUpper() != "Help".ToUpper()
                   && navMenuItem.Value.ToUpper() != "AboutUs".ToUpper()
                    && navMenuItem.Value.ToUpper() != "onlinehelp".ToUpper())
                {
                    result = HasViewPermission(navMenuItem.Value);

                    if ((result != null && result.CanView == "N" && result.CanUpdate == "N") || navMenuItem.ChildItems.Count == 0)
                    {
                        NavigationMenu.Items.RemoveAt(mnuItmIDX);
                    }
                    else if (result != null && Session["PROJECT_ID"] == null)
                    {
                        if (result.ProjectDependent != null)
                        {
                            if (result.ProjectDependent == "Y")
                            {
                                //navMenuItem.NavigateUrl = ResolveUrl(projectURL + projectParams);
                                navMenuItem.Selectable = false;
                                navMenuItem.ToolTip = "Select a Project to continue.";
                                //NavigationMenu.Items.RemoveAt(mnuItmIDX);
                            }
                        }
                    }
                    else if (result != null && Session["HH_ID"] == null)
                    {
                        if (result.PAPDependent != null)
                        {
                            if (result.PAPDependent == "Y")
                            {
                                //navMenuItem.NavigateUrl = ResolveUrl("~/UI/Compensation/PAPList.aspx");
                                navMenuItem.Selectable = false;
                                navMenuItem.ToolTip = "Select a PAP to continue.";
                                //NavigationMenu.Items.RemoveAt(mnuItmIDX);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Chek User permitions and set his menu controls
        /// </summary>
        void CheckChildNavigationMenus(MenuItem item)
        {
            for (int childItmIDX = item.ChildItems.Count - 1; childItmIDX >= 0; childItmIDX--)
            {
                // Recursively call the CheckChildNavigationMenus method to
                // traverse the tree and display all child menu items.
                CheckChildNavigationMenus(item.ChildItems[childItmIDX]);
                if (item.ChildItems[childItmIDX].Value.ToUpper() != "Help".ToUpper()
                    && item.ChildItems[childItmIDX].Value.ToUpper() != "AboutUs".ToUpper()
                     && item.ChildItems[childItmIDX].Value.ToUpper() != "onlinehelp".ToUpper())
                {
                    result = HasViewPermission(item.ChildItems[childItmIDX].Value);

                    if (result != null && result.CanView == "N" && result.CanUpdate == "N")
                    {
                        item.ChildItems.RemoveAt(childItmIDX);
                    }
                    else if (Session["PROJECT_ID"] == null)
                    {
                        if (result != null && result.ProjectDependent != null)
                            if (result.ProjectDependent == "Y")
                            {
                                //item.ChildItems[childItmIDX].NavigateUrl = ResolveUrl(projectURL + projectParams);
                                item.ChildItems[childItmIDX].Selectable = false;
                                item.ChildItems[childItmIDX].ToolTip = "Select a Project to continue.";
                                //item.ChildItems.RemoveAt(childItmIDX);
                            }
                    }
                    else if (result != null && Session["HH_ID"] == null)
                    {
                        if (result.PAPDependent != null)
                            if (result.PAPDependent == "Y")
                            {
                                //item.ChildItems[childItmIDX].NavigateUrl = ResolveUrl("~/UI/Compensation/PAPList.aspx");
                                item.ChildItems[childItmIDX].Selectable = false;
                                item.ChildItems[childItmIDX].ToolTip = "Select a PAP to continue.";
                                //item.ChildItems.RemoveAt(childItmIDX);
                            }
                    }
                }
            }
        }


        /// <summary>
        /// Chek User permitions
        /// </summary>
        private RolePrivilegesBO HasViewPermission(string menuValue)
        {
            result = PrivilegeList.Find(
                    delegate(RolePrivilegesBO priv)
                    {
                        return priv.MenuName.ToUpper() == menuValue.ToUpper();
                    }
                );

            return result;
            //if (result != null && (result.CanView == "Y" || result.CanUpdate == "Y"))
            //{
            //    return true;
            //}
            //else
            //    return false;
        }

        public string PageHeader
        {
            set
            {
                lblPageHeader.Text = value;
            }
        }

        /*public PageHeaderCaption PageHeader
        {
            set
            {
                switch (value)
                {
                    case PageHeaderCaption.Household:
                        lblPageHeader.Text = Session["PROJECT_CODE"].ToString() + " - Socio-Economic - Household Details";
                        break;
                }
            }
        }*/

        /// <summary>
        /// Clear sessions
        /// redirect to login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public enum PageHeaderCaption
        {
            Household,
            HouseholdRelations
        }
    }
}
