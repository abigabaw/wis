using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class CulturePropertiesMasterBO
    {
        private int _CulturePropTypeID;

        public int CulturePropTypeID
        {
            get { return _CulturePropTypeID; }
            set { _CulturePropTypeID = value; }
        }
        private string _CulturePropTypeName;

        public string CulturePropTypeName
        {
            get { return _CulturePropTypeName; }
            set { _CulturePropTypeName = value; }
        }
        private string _Isdeleted;

        public string Isdeleted
        {
            get { return _Isdeleted; }
            set { _Isdeleted = value; }
        }
        private string _CreatedBy;

        public string CreatedBy
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
        private int _Obsolete;

        public int Obsolete
        {
            get { return _Obsolete; }
            set { _Obsolete = value; }
        }

    }
}
