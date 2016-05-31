using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ParishBO
    {
        public int ParishId { get; set; }

        public string ParishName { get; set; }

        public int SubcountyID { get; set; }
        public int CountyID { get; set; }
        public int DistrictID { get; set; }

        public string subcountyName { get; set; }

        public string DistrictName { get; set; }

        public string countyName { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}