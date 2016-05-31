using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ClansBO
    {
        private string tribeName = string.Empty;

        public string TribeName
        {
            get { return tribeName; }
            set { tribeName = value; }
        }
        private int _CLANID;

        public int CLANID
        {
            get { return _CLANID; }
            set { _CLANID = value; }
        }

        private int _TRIBEID;

        public int TRIBEID
        {
            get { return _TRIBEID; }
            set { _TRIBEID = value; }
        }

        private string _CLANNAME;

        public string CLANNAME
        {
            get { return _CLANNAME; }
            set { _CLANNAME = value; }
        }

        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private int _UpdatedBy;

        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }
        private string isDeleted;

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
            
    }
}