using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class NonPermanentStructureBO
    {
        private int nonPermanentStructureID = -1;
        private int householdID = -1;
        private int structureTypeID = -1;
        private string otherStructureType = String.Empty;
        private int categoryID = -1;
        private int structureConditionID = -1;

        private string owner = String.Empty;
        private string ownerName = String.Empty;
        private string occupant = String.Empty;
        private string otherOccupantName = String.Empty;
        private int occupantStatusID = -1;
        private string otherOccupantStatus = String.Empty;

        private Decimal dimensionLength = -1;
        private Decimal dimensionWidth = -1;
        private int noOfRooms = -1;
        private Decimal surfaceArea = -1;
        private string structurePhoto = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        private string sTR_TYPE = String.Empty;
        private string sTR_CATEGORYNAME = String.Empty;
        private string sTR_CONDITION = String.Empty;

        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public int NonPermanentStructureID { get { return nonPermanentStructureID; } set { nonPermanentStructureID = value; } }

        public int HouseholdID { get { return householdID; } set { householdID = value; } }

        public int StructureTypeID { get { return structureTypeID; } set { structureTypeID = value; } }

        public string OtherStructureType { get { return otherStructureType; } set { otherStructureType = value; } }

        public int CategoryID { get { return categoryID; } set { categoryID = value; } }

        public int StructureConditionID { get { return structureConditionID; } set { structureConditionID = value; } }

        public string Owner { get { return owner; } set { owner = value; } }

        public string OwnerName { get { return ownerName; } set { ownerName = value; } }

        public string Occupant { get { return occupant; } set { occupant = value; } }

        public string OtherOccupantName { get { return otherOccupantName; } set { otherOccupantName = value; } }

        public int OccupantStatusID { get { return occupantStatusID; } set { occupantStatusID = value; } }

        public string OtherOccupantStatus { get { return otherOccupantStatus; } set { otherOccupantStatus = value; } }

        public Decimal DimensionLength { get { return dimensionLength; } set { dimensionLength = value; } }

        public Decimal DimensionWidth { get { return dimensionWidth; } set { dimensionWidth = value; } }

        public int NoOfRooms { get { return noOfRooms; } set { noOfRooms = value; } }

        public Decimal SurfaceArea { get { return surfaceArea; } set { surfaceArea = value; } }

        public string STR_TYPE
        {
            get { return sTR_TYPE; }
            set { sTR_TYPE = value; }
        }

        public string STR_CATEGORYNAME
        {
            get { return sTR_CATEGORYNAME; }
            set { sTR_CATEGORYNAME = value; }
        }

        public string STR_CONDITION
        {
            get { return sTR_CONDITION; }
            set { sTR_CONDITION = value; }
        }

        public string StructurePhoto { get { return structurePhoto; } set { structurePhoto = value; } }

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
