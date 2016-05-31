using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class DistrictBO
    {
        private int districtID = -1;
        private string districtName = String.Empty;

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
        
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
    }
}
