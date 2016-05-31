using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PAP_HealthBO
    {
        private int healthID = -1;
        private int householdID = -1;
        private string nearestHealthCentre = String.Empty;
        private string distanceToHealthCentre = String.Empty;
        private string usedByFamily = String.Empty;
        private string nonUseReason = String.Empty;
        private int noOfBirth = 0;
        private int noOfDeath = 0;
        private string reasonForDeath = String.Empty;
        private string commonDiseases = String.Empty;
        private string practiceFamilyPlanning = String.Empty;
        private string heardOfAIDS = String.Empty;
        private string howContracted = String.Empty;
        private string howAvoided = String.Empty;

        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";

        public int HealthID
        {
            get
            {
                return healthID;
            }
            set
            {
                healthID = value;
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

        public string NearestHealthCentre
        {
            get
            {
                return nearestHealthCentre;
            }
            set
            {
                nearestHealthCentre = value;
            }
        }

        public string DistanceToHealthCentre
        {
            get
            {
                return distanceToHealthCentre;
            }
            set
            {
                distanceToHealthCentre = value;
            }
        }

        public string UsedByFamily
        {
            get
            {
                return usedByFamily;
            }
            set
            {
                usedByFamily = value;
            }
        }

        public string NonUseReason
        {
            get
            {
                return nonUseReason;
            }
            set
            {
                nonUseReason = value;
            }
        }

        public int NoOfBirth
        {
            get
            {
                return noOfBirth;
            }
            set
            {
                noOfBirth = value;
            }
        }

        public int NoOfDeath
        {
            get
            {
                return noOfDeath;
            }
            set
            {
                noOfDeath = value;
            }
        }

        public string ReasonForDeath
        {
            get
            {
                return reasonForDeath;
            }
            set
            {
                reasonForDeath = value;
            }
        }

        public string CommonDiseases
        {
            get
            {
                return commonDiseases;
            }
            set
            {
                commonDiseases = value;
            }
        }

        public string PracticeFamilyPlanning
        {
            get
            {
                return practiceFamilyPlanning;
            }
            set
            {
                practiceFamilyPlanning = value;
            }
        }

        public string HeardOfAIDS
        {
            get
            {
                return heardOfAIDS;
            }
            set
            {
                heardOfAIDS = value;
            }
        }

        public string HowContracted
        {
            get
            {
                return howContracted;
            }
            set
            {
                howContracted = value;
            }
        }

        public string HowAvoided
        {
            get
            {
                return howAvoided;
            }
            set
            {
                howAvoided = value;
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