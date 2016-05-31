using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class GeographyBO
    {
        private int geographicalID = -1;
        private int projectID = -1;
        private string generalDirection = String.Empty;
        private string keyFeatures = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public bool isDeleted = false;

        public int GeographicalID
        {
            get
            {
                return geographicalID;
            }
            set
            {
                geographicalID = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }

        public string GeneralDirection
        {
            get
            {
                return generalDirection;
            }
            set
            {
                generalDirection = value;
            }
        }

        public string KeyFeatures
        {
            get
            {
                return keyFeatures;
            }
            set
            {
                keyFeatures = value;
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

        public bool IsDeleted
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
