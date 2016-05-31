using System;
using System.Web;
using WIS_BusinessObjects;
using WIS_BusinessLogic;

namespace WIS
{
    public static class CheckAuthorization
    {
        /// <summary>
        /// Check User Has View Pemitions For Selected Page
        /// </summary>
        /// <param name="privilegeValue"></param>
        /// <returns></returns>
        public static bool HasViewPrivilege(string privilegeValue)
        {
            RolePrivilegesList PrivilegeList = null;
            RolePrivilegesBO result = null;
            string userID = string.Empty;
            if ((HttpContext.Current.Session["USER_ID"]) != null)
            {
                userID = HttpContext.Current.Session["USER_ID"].ToString();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~\\Login.aspx");
            }
            bool hasViewPrivilege = false;

            if (HttpContext.Current.Cache["PRIV_" + userID] != null)
            {
                PrivilegeList = (RolePrivilegesList)HttpContext.Current.Cache["PRIV_" + userID];
            }
            else
            {
                PrivilegeList = (new RolePrivilegesBLL()).GetROLEPRIId(Convert.ToInt32(userID));
                HttpContext.Current.Cache.Insert("PRIV_" + userID, PrivilegeList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
            }

            result = PrivilegeList.Find(
                    delegate(RolePrivilegesBO priv)
                    {
                        return priv.MenuName.ToUpper() == privilegeValue.ToUpper();
                    }
                );

            if (result != null && result.CanView == "Y")
            {
                hasViewPrivilege = true;
            }

            return hasViewPrivilege;
        }

        /// <summary>
        /// Check User Has Update Pemitions For Selected Page
        /// </summary>
        /// <param name="privilegeValue"></param>
        /// <returns></returns>
        public static bool HasUpdatePrivilege(string privilegeValue)
        {
            RolePrivilegesList PrivilegeList = null;
            RolePrivilegesBO result = null;
            string userID = string.Empty;
            if ((HttpContext.Current.Session["USER_ID"]) != null)
            {
                userID = HttpContext.Current.Session["USER_ID"].ToString();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~\\Login.aspx");
            }
                bool hasUpdatePrivilege = false;

            if (HttpContext.Current.Cache["PRIV_" + userID] != null)
            {
                PrivilegeList = (RolePrivilegesList) HttpContext.Current.Cache["PRIV_" + userID];
            }
            else
            {
                PrivilegeList = (new RolePrivilegesBLL()).GetROLEPRIId(Convert.ToInt32(userID));
                HttpContext.Current.Cache.Insert("PRIV_" + userID, PrivilegeList, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromHours(12));
            }

            result = PrivilegeList.Find(
                    delegate(RolePrivilegesBO priv)
                    {
                        return priv.MenuName.ToUpper() == privilegeValue.ToUpper();
                    }
                );

            if (result != null && result.CanUpdate == "Y")
            {
                hasUpdatePrivilege = true;
            }

            return hasUpdatePrivilege;
        }
    }
}