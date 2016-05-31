using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class VillageBO
    {
        private int villageID = -1;
        private int subCountyID = -1;
        private int countyID = -1;

        public int CountyID
        {
            get { return countyID; }
            set { countyID = value; }
        }
        private int districtID = -1;

        public int DistrictID
        {
            get { return districtID; }
            set { districtID = value; }
        }
        private string subCountyName = String.Empty;
        private string parish = String.Empty;
        private string districtName = string.Empty;
        private string countyName = String.Empty;

        public string CountyName
        {
            get { return countyName; }
            set { countyName = value; }
        }

        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        public string Parish
        {
            get { return parish; }
            set { parish = value; }
        }

        public string SubCountyName
        {
            get { return subCountyName; }
            set { subCountyName = value; }
        }

        public int SubCountyID
        {
            get { return subCountyID; }
            set { subCountyID = value; }
        }
        private string villageName = String.Empty;

        public int VillageID
        {
            get
            {
                return villageID;
            }
            set
            {
                villageID = value;
            }
        }

        public string VillageName
        {
            get
            {
                return villageName;
            }
            set
            {
                villageName = value;
            }
        }
        public string IsDeleted { get; set; }


        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
    }
}
