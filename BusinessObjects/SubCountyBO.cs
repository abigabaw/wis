using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class SubCountyBO
    {
        private int subCountyID = -1;
        private int countyID = -1;
        private string subCountyName = String.Empty;
        private string districtName = string.Empty;
        private string countyName = String.Empty;

        public int CountyID
        {
            get { return countyID; }
            set { countyID = value; }
        }
      

        public string CountyName
        {
            get
            {
                return countyName;
            }
            set
            {
                countyName = value;
            }
        }
        public int DistrictID { get; set; }

        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        public int SubCountyID
        {
            get
            {
                return subCountyID;
            }
            set
            {
                subCountyID = value;
            }
        }

        public string SubCountyName
        {
            get
            {
                return subCountyName;
            }
            set
            {
                subCountyName = value;
            }

        }
        public string IsDeleted { get; set; }
       

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }


    }

}
