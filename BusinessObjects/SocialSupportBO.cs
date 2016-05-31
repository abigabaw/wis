using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class SocialSupportBO
    {
        private int supportedByID;
        private string supportedBy = string.Empty;
        private string isdeletedBy = string.Empty;
        private DateTime createddate = DateTime.Now;
        private int createdby = 1;

        public int SUPPORTEDBYID
        {
            get { return supportedByID; }
            set { supportedByID = value; }
        }  

        public DateTime Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }

        public int CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }

        public string SupportedBy
        {
            get { return supportedBy; }
            set { supportedBy = value; }
        }        

      
        public string IsDeleted
        {
            get;
            set;
        }

    }
}