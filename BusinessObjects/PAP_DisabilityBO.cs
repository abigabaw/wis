using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PAP_DisabilityBO
    {
        private int _PAPDisabilityID = -1;
        private int householdID = -1;
        private int disabilityID = -1;
        private string healthCareNeeded = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";

        public int PAPDisabilityID
        {
            get
            {
                return _PAPDisabilityID;
            }
            set
            {
                _PAPDisabilityID = value;
            }
        }

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

        public int DisabilityID
        {
            get
            {
                return disabilityID;
            }
            set
            {
                disabilityID = value;
            }
        }

        public string DisabilityName { get; set; }

        public string HealthCareNeeded
        {
            get
            {
                return healthCareNeeded;
            }
            set
            {
                healthCareNeeded = value;
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
