using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PAPServiceBO
    {
        private int householdID = -1;
        private int serviceID = -1;
        string fieldValue = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int HouseholdID
        {
            get
            {
                return householdID;
            }
            set
            {
                householdID = value;
            }
        }

        public int ServiceID { get { return serviceID; } set { serviceID = value; } }

        public string FieldValue
        {
            get
            {
                return fieldValue;
            }
            set
            {
                fieldValue = value;
            }
        }

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
