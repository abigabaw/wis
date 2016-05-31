using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public  class MaxCapBO
    {
        private int maxCapID = -1;
        private decimal maxCapVal;
        private string districtName = string.Empty;
        private int projectID;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        public int MaxCapID
        {
            get;
            set;
        }

        public int DistrictID { get; set; }

        public decimal MaxCapVal{get; set;}      
            
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
    }
}
