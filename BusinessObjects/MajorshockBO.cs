using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class MajorshockBO
    {
       
        private int COP_MECHANISMID;

        public int COP_MECHANISMID1
        {
            get { return COP_MECHANISMID; }
            set { COP_MECHANISMID = value; }
        }
        private int SUPPORTID;

        public int SUPPORTID1
        {
            get { return SUPPORTID; }
            set { SUPPORTID = value; }
        }
        private int CREATEDBY;

        public int CREATEDBY1
        {
            get { return CREATEDBY; }
            set { CREATEDBY = value; }
        }
        private int SHOCKID;
        public int SHOCKID1
        {
            get { return SHOCKID; }
            set { SHOCKID = value; }
        }
       
        private string ISDELETED = string.Empty;

        public string ISDELETED1
        {
            get { return ISDELETED; }
            set { ISDELETED = value; }
        }
        private int PAP_SHOCKID;

        public int PAP_SHOCKID1
        {
            get { return PAP_SHOCKID; }
            set { PAP_SHOCKID = value; }
        }

        private int HHID;

        public int HHID1
        {
            get { return HHID; }
            set { HHID = value; }
        }

        private string SHOCKEXPERIENCED;
        private string SUPPORTEDBY;
        private string COP_MECHANISM;

        public string COP_MECHANISM1
        {
            get { return COP_MECHANISM; }
            set { COP_MECHANISM = value; }
        }

        public string SUPPORTEDBY1
        {
            get { return SUPPORTEDBY; }
            set { SUPPORTEDBY = value; }
        }         
                           

        

public string SHOCKEXPERIENCED1
{
  get { return SHOCKEXPERIENCED; }
  set { SHOCKEXPERIENCED = value; }
}
        
        
        public string ConcernName { get; set; }
    }
}
