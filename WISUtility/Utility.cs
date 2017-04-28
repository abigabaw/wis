using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace WIS_Utility
{
    public class Utility
    {
        //static String DomainName = "corp.ktwo.co.in/DC=corp,DC=ktwo,DC=co,DC=in";

        public static Boolean IsValidUser(String DomainName, String UserName, String UserPassword)
        {
            if (!String.IsNullOrEmpty(DomainName))
            {

                try
                {
                    string LDAPPath = "LDAP://" + DomainName + "/OU=All Users,DC=uetcl,DC=com";
                    if (Exists(LDAPPath, UserName, UserPassword))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception)
                {
                    return false;
                }

            }
            return false;


        }

        // Edwin Baguma: 23/02/2016
        public static Boolean IsActive(string userName, string password) // DirectoryEntry de)
        {
            /***
            if (de.NativeGuid == null) return false;

            int flags = (int)de.Properties["userAccountControl"].Value;

            return !Convert.ToBoolean(flags & 0x0002);
            ***/

            string LDAPDomainName = System.Configuration.ConfigurationManager.AppSettings.Get("LDAPDomainName");
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + LDAPDomainName + "/OU=All Users,DC=uetcl,DC=com", userName, password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException) { }
            return authentic;

        } // End:

        public static Boolean Exists(string objectPath, string UserName, string PassWord)
        {
            Boolean found = false;
            DirectoryEntry searchRoot = new DirectoryEntry(objectPath, UserName, PassWord);
            DirectorySearcher DirectorySearcherObject = new DirectorySearcher(searchRoot);
            SearchResult SearchResultObject = DirectorySearcherObject.FindOne();

            if (IsActive(UserName, PassWord))
            {


                try
                {
                    if (SearchResultObject != null)
                    {
                        string Email = SearchResultObject.Properties["mail"].ToString();
                        return found = true;
                    }
                    else
                    {
                        return found;
                    }
                }
                catch (Exception)
                {
                    return found;
                }
            }
            else
            {
                return found;
            }
        }

    }
}
