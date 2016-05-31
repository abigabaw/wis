using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class AffectedLandUserBO
    {
        private int landUserID = -1;
        private int householdID = -1;
        private string landUserName = String.Empty;
        private int statusID = -1;
        private string statusName = String.Empty;
        private string relatedTo = String.Empty;
        private string timeOnLand = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int LandUserID
        {
            get
            {
                return landUserID;
            }
            set
            {
                landUserID = value;
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

        public string LandUserName
        {
            get
            {
                return landUserName;
            }
            set
            {
                landUserName = value;
            }
        }

        public int StatusID
        {
            get
            {
                return statusID;
            }
            set
            {
                statusID = value;
            }
        }

        public string StatusName
        {
            get
            {
                return statusName;
            }
            set
            {
                statusName = value;
            }
        }

        public string RelatedTo
        {
            get
            {
                return relatedTo;
            }
            set
            {
                relatedTo = value;
            }
        }

        public string TimeOnLand
        {
            get
            {
                return timeOnLand;
            }
            set
            {
                timeOnLand = value;
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
