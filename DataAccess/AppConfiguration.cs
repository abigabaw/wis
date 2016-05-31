using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WIS_DataAccess
{
    public class AppConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["UETCL_WIS"].ConnectionString;
            }
        }

        public static string FromMailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("FromMailAddress");
            }
        }
    }
}