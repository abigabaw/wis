using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PAP_RelationBO
    {
        private int relationID = -1;
        private int householdID = -1;
        private int holderTypeID = -1;
        private string holderName = String.Empty; 
        private string resideOnAffectedLand = "NO";
        private DateTime dateOfBirth;
        private int literacyLevelID = -1;
        private string literacyStatus = String.Empty;
        private string isDeleted = "False";
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string sex = String.Empty;

        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
            }
        }

        public int RelationID
        {
            get
            {
                return relationID;
            }
            set
            {
                relationID = value;
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

        public string HolderName
        {
            get
            {
                return holderName;
            }
            set
            {
                holderName = value;
            }
        }

        public string ResideOnAffectedLand
        {
            get
            {
                return resideOnAffectedLand;
            }
            set
            {
                resideOnAffectedLand = value;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
            }
        }

        public int LiteracyLevelID
        {
            get
            {
                return literacyLevelID;
            }
            set
            {
                literacyLevelID = value;
            }
        }

        public int CurrentSchoolStatusID { get; set; }

        public int NeverAttendedSchoolID { get; set; }

        public int SchoolDropReasonID { get; set; }

        public string CurrentSchoolStatus { get; set; }

        public string NeverAttendedSchool { get; set; }

        public string SchoolDropReason { get; set; }

        public string LiteracyStatus
        {
            get
            {
                return literacyStatus;
            }
            set
            {
                literacyStatus = value;
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
                isDeleted = value;
            }
        }
    }
}