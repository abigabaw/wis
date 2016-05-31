using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LandLivingOnBO
        {
            private int livingOnID = -1;
            private int householdID = -1;
            private string whereLivedBefore = String.Empty;
            private string preferredVillege = String.Empty;
            private string isOtherLandHold = String.Empty;
            private string whichLandHold = String.Empty;
            private string requireTransport = String.Empty;
            private string movenearRelatives = String.Empty;
            private string buriedFamilyMemonLand = String.Empty;
            private string howmanyBuried = String.Empty;
            private string relocateAncestors = String.Empty;
            private string district = String.Empty;
            private string county = String.Empty;
            private string subcounty = String.Empty;
            private string village = String.Empty;
            private string isDeleted = String.Empty;
            public int createdBy = -1;
            public int updatedBy = -1;

            public int LivingOnID
            {
                get
                {
                    return livingOnID;
                }
                set
                {
                    livingOnID = value;
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

            public string WhereLivedBefore
            {
                get
                {
                    return whereLivedBefore;
                }
                set
                {
                    whereLivedBefore = value;
                }
            }

            public string PreferredVillege
            {
                get
                {
                    return preferredVillege;
                }
                set
                {
                    preferredVillege = value;
                }
            }

            public string IsOtherLandHold
            {
                get
                {
                    return isOtherLandHold;
                }
                set
                {
                    isOtherLandHold = value;
                }
            }

            public string WhichLandHold
            {
                get
                {
                    return whichLandHold;
                }
                set
                {
                    whichLandHold = value;
                }
            }

            public string RequireTransport
            {
                get
                {
                    return requireTransport;
                }
                set
                {
                    requireTransport = value;
                }
            }

            public string MovenearRelatives
            {
                get
                {
                    return movenearRelatives;
                }
                set
                {
                    movenearRelatives = value;
                }
            }

            public string BuriedFamilyMemonLand
            {
                get
                {
                    return buriedFamilyMemonLand;
                }
                set
                {
                    buriedFamilyMemonLand = value;
                }
            }

            public string HowmanyBuried
            {
                get
                {
                    return howmanyBuried;
                }
                set
                {
                    howmanyBuried = value;
                }
            }

            public string RelocateAncestors
            {
                get
                {
                    return relocateAncestors;
                }
                set
                {
                    relocateAncestors = value;
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