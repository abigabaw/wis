using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class TribeBO
    {
        private int _TribeID;

        public int TribeID
        {
            get { return _TribeID; }
            set { _TribeID = value; }
        }

        private string _TribeName;

        public string TribeName
        {
            get { return _TribeName; }
            set { _TribeName = value; }
        }

        private string _IsDeleted;

        public string IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        private int _CreatedBy;

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private DateTime _CreatedDate;

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }


        private int _UpdatedBy;

        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }

        private DateTime _UpdatedDate;

        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }


    }
}