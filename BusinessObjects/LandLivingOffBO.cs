using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LandLivingOffBO
        {
            private int livingOffID = -1;
            private int householdID = -1;
            private int dwellingid = -1;
            private string dwellingtype = String.Empty;
            private string noofrooms = String.Empty;
            private int str_tenureid = -1;
            private string str_tenure = String.Empty;
            private int roofid = -1;
            private string roofType = String.Empty;
            private int wallid = -1;
            private string wallType = String.Empty;
            private int floorid = -1;
            private string floortype = String.Empty;
            public string isDeleted = "FALSE";
            public int createdBy = -1;
            public int updatedBy = -1;

            public int LivingOffID
            {
                get
                {
                    return livingOffID;
                }
                set
                {
                    livingOffID = value;
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

            public int DwellingID
            {
                get
                {
                    return dwellingid;
                }
                set
                {
                    dwellingid = value;
                }
            }

            public string Dwellingtype
            {
                get
                {
                    return dwellingtype;
                }
                set
                {
                    dwellingtype = value;
                }
            }

            public string NoofRooms
            {
                get
                {
                    return noofrooms;
                }
                set
                {
                    noofrooms = value;
                }
            }

            public int Str_TenureID
            {
                get
                {
                    return str_tenureid;
                }
                set
                {
                    str_tenureid = value;
                }
            }

            public string Str_Tenure
            {
                get
                {
                    return str_tenure;
                }
                set
                {
                    str_tenure = value;
                }
            }

            public int Tenure { get; set; }

            public int RoofID
            {
                get
                {
                    return roofid;
                }
                set
                {
                    roofid = value;
                }
            }

            public string RoofType
            {
                get
                {
                    return roofType;
                }
                set
                {
                    roofType = value;
                }
            }

            public int WallID
            {
                get
                {
                    return wallid;
                }
                set
                {
                    wallid = value;
                }
            }

            public string WallType
            {
                get
                {
                    return wallType;
                }
                set
                {
                    wallType = value;
                }
            }

            public int FloorID
            {
                get
                {
                    return floorid;
                }
                set
                {
                    floorid = value;
                }
            }

            public string FloorType
            {
                get
                {
                    return floortype;
                }
                set
                {
                    floortype = value;
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