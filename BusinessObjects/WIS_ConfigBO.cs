using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class WIS_ConfigBO
    {
        public string ConfigItem
        {
            get;
            set;
        }

        public string ConfigData
        {
            get;
            set;
        }

        #region for Sending SMS Config 
        //add By ramu.S
        public string MobileNumber
        {
            get;
            set;
        }
        public string MobilePassword
        {
            get;
            set;
        }
        public string SiteUrl
        {
            get;
            set;
        }
        public string MobileStatus
        {
            get;
            set;
        }
        #endregion

        #region for Build Version used in about us

        public string BUILDVERSION
        {
            get;
            set;
        }
        public string BUILDDATE
        {
            get;
            set;
        }
        public string BUILDCOPY
        {
            get;
            set;
        }

        #endregion
    }
}