using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class HolderTypeBO
    {
        private int holderTypeID = -1;
        private string holderTypeName = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";

        public int HolderTypeID
        {
            get
            {
                return holderTypeID;
            }
            set
            {
                holderTypeID = value;
            }
        }

        public string HolderTypeName
        {
            get
            {
                return holderTypeName;
            }
            set
            {
                holderTypeName = value;
            }
        }
         
        public int HolderCount { get; set; }

        public int AffectedHolderCount { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }
    }
}
