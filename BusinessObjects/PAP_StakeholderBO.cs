using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PAP_StakeholderBO
    {
        private int stakeHolderID = -1;
        private int houseHoldID = -1;
        private string stakeholderName = String.Empty;
        private string representation = String.Empty;
        public string residentialAddress = String.Empty;
        public string postalAddress = String.Empty;
        public string telephoneNo = String.Empty;
        public DateTime dateOfSurvey;
        public string district = String.Empty;
        public string county = String.Empty;
        public string subcounty = String.Empty;
        public string parish = String.Empty;
        public string village = String.Empty;
        public int segmentID = -1;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";

        public int StakeHolderID
        {
            get
            {
                return stakeHolderID;
            }
            set
            {
                stakeHolderID = value;
            }
        }

        public int HouseHoldID
        {
            get
            {
                return houseHoldID;
            }
            set
            {
                houseHoldID = value;
            }
        }

        public string StakeholderName
        {
            get
            {
                return stakeholderName;
            }
            set
            {
                stakeholderName = value;
            }
        }

        public string Representation
        {
            get
            {
                return representation;
            }
            set
            {
                representation = value;
            }
        }

        public string ResidentialAddress
        {
            get
            {
                return residentialAddress;
            }
            set
            {
                residentialAddress = value;
            }
        }

        public string PostalAddress
        {
            get
            {
                return postalAddress;
            }
            set
            {
                postalAddress = value;
            }
        }

        public string TelephoneNo
        {
            get
            {
                return telephoneNo;
            }
            set
            {
                telephoneNo = value;
            }
        }

        public DateTime DateOfSurvey
        {
            get
            {
                return dateOfSurvey;
            }
            set
            {
                dateOfSurvey = value;
            }
        }

        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }

        public string County
        {
            get
            {
                return county;
            }
            set
            {
                county = value;
            }
        }

        public string Subcounty
        {
            get
            {
                return subcounty;
            }
            set
            {
                subcounty = value;
            }
        }

        public string Parish
        {
            get
            {
                return parish;
            }
            set
            {
                parish = value;
            }
        }

        public string Village
        {
            get
            {
                return village;
            }
            set
            {
                village = value;
            }
        }

        public int SegmentID
        {
            get
            {
                return segmentID;
            }
            set
            {
                segmentID = value;
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