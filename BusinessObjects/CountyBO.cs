using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CountyBO
    {
        private int countyID = -1;
        private string countyName = String.Empty;
        private string districtName = string.Empty;

        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        public int CountyID
        {
            get
            {
                return countyID;
            }
            set
            {
                countyID = value;
            }
        }

        public int DistrictID { get; set; }

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

        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
    }
}
