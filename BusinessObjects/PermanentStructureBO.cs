using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PermanentStructureBO
    {
        private int permanentStructureID = -1;
        private int householdID = -1;
        private int structureTypeID = -1;
        private string structTypid = string.Empty;
        private string otherStructureType = String.Empty;
        private int roofID = -1;
        private string rooftype;
        private string structureType = "";

        public string StructureType
        {
            get { return structureType; }
            set { structureType = value; }
        }

        public string Rooftype
        {
            get { return rooftype; }
            set { rooftype = value; }
        }
        private int wallID = -1;
        private string walltype;

        public string Walltype
        {
            get { return walltype; }
            set { walltype = value; }
        }
        private int floorID = -1;
        private string floortype;

        public string Floortype
        {
            get { return floortype; }
            set { floortype = value; }
        }
        private int windowID = -1;

        private string windowtype;

        public string Windowtype
        {
            get { return windowtype; }
            set { windowtype = value; }
        }

        private int roofConditionID = -1;
        private int wallConditionID = -1;
        private int floorConditionID = -1;
        private int windowConditionID = -1;

        private string conditionData;

        public string ConditionData
        {
            get { return conditionData; }
            set { conditionData = value; }
        }
        private string owner = String.Empty;
        private string ownerName = String.Empty;
        private string occupant = String.Empty;
        private string otherOccupantName = String.Empty;
        private int occupantStatusID = -1;
        private string occupantStatus;

        public string OccupantStatus
        {
            get { return occupantStatus; }
            set { occupantStatus = value; }
        }
        private string otherOccupantStatus = String.Empty;
        private Decimal dimensionLength = -1;
        private Decimal dimensionWidth = -1;
        private int noOfRooms = -1;
        private Decimal surfaceArea = -1;
        private Decimal depreciatedValue = -1;
        private Decimal replacementValue = -1;
        private string additionalComments = String.Empty;
        private string structurePhoto = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "False";

        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public int PermanentStructureID { get { return permanentStructureID; } set { permanentStructureID = value; } }

        public int HouseholdID { get { return householdID; } set { householdID = value; } }

        public int StructureTypeID { get { return structureTypeID; } set { structureTypeID = value; } }

        public string StructTypid
        {
            get { return structTypid; }
            set { structTypid = value; }
        }

        public string OtherStructureType { get { return otherStructureType; } set { otherStructureType = value; } }

        public int RoofID { get { return roofID; } set { roofID = value; } }

        public int WallID { get { return wallID; } set { wallID = value; } }

        public int FloorID { get { return floorID; } set { floorID = value; } }

        public int WindowID { get { return windowID; } set { windowID = value; } }

        public int RoofConditionID { get { return roofConditionID; } set { roofConditionID = value; } }

        public int WallConditionID { get { return wallConditionID; } set { wallConditionID = value; } }

        public int FloorConditionID { get { return floorConditionID; } set { floorConditionID = value; } }

        public int WindowConditionID { get { return windowConditionID; } set { windowConditionID = value; } }

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

        public Decimal DepreciatedValue { get { return depreciatedValue; } set { depreciatedValue = value; } }

        public Decimal ReplacementValue { get { return replacementValue; } set { replacementValue = value; } }

        public string AdditionalComments { get { return additionalComments; } set { additionalComments = value; } }

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
                isDeleted = value;
            }
        }
    }
}
