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
                DirectoryEntry DirectoryEntryObject;
                DirectorySearcher DirectorySearcherObject;
                SearchResult SearchResultObject;

                try
                {
                    String LDAPPath = "LDAP://" + DomainName;

                    DirectoryEntryObject = new DirectoryEntry(LDAPPath, UserName, UserPassword, AuthenticationTypes.Secure);
                    DirectorySearcherObject = new DirectorySearcher(DirectoryEntryObject);

                    // Edwin Baguma: 23/02/2016
                    if (IsActive(DirectoryEntryObject)){
                        SearchResultObject = DirectorySearcherObject.FindOne();
                    }
                    else
                    {
                        SearchResultObject = null;
                        
                    } // End:
                    
                    if (SearchResultObject != null)
                    {
                        string Email = SearchResultObject.Properties["mail"].ToString();

                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    SearchResultObject = null;
                    DirectorySearcherObject = null;
                    DirectoryEntryObject = null;
                }
            }
            return false;
        }

        // Edwin Baguma: 23/02/2016
        public static Boolean IsActive(DirectoryEntry de)
        {
            if (de.NativeGuid == null) return false;

            int flags = (int)de.Properties["userAccountControl"].Value;

            return !Convert.ToBoolean(flags & 0x0002);
        } // End:

    }
}
