using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name : PAP_LIV_REST_LOC
    public class LivelihoodRestLocationBO
    {
        public int Liv_Rest_LocationID { get; set; }

        public int HHID { get; set; }

        public string NewDistrict { get; set; }

        public string NewCounty { get; set; }

        public string NewSubCounty { get; set; }

        public string NewParish { get; set; }

        public string NewVillage { get; set; }

        public string DistFrmOldLoc { get; set; }

        public DateTime DateOfSettlement { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion

    }
}
