using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CropRateBO
    {
        private int croprateID = -1;
        private int cropID = 1;
        private string districtName = String.Empty;
        private int districtID = 1;
        private string croprateName = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        private string isDeleted = string.Empty;
        private int CROPDESCRIPTIONID = 0;
        private string CROPDESCRIPTION = String.Empty;

        public string CropDescription
        {
            get { return CROPDESCRIPTION; }
            set { CROPDESCRIPTION = value; }
        }

        public int CropDescriptionID
        {
            get
            {
                return CROPDESCRIPTIONID;
            }
            set
            {
                CROPDESCRIPTIONID = value;
            }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }


        public int CropRateID
        {
            get
            {
                return croprateID;
            }
            set
            {
                croprateID = value;
            }
        }

        public int CropID
        {
            get
            {
                return cropID;
            }
            set
            {
                cropID = value;
            }
        }

        public string DistrictName
        {
            get
            {
                return districtName;
            }
            set
            {
                districtName = value;
            }
        }

        public int DistrictID
        {
            get
            {
                return districtID;
            }
            set
            {
                districtID = value;
            }
        }

        public string CropRate
        {
            get
            {
                return croprateName;
            }
            set
            {
                croprateName = value;
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
    }
}