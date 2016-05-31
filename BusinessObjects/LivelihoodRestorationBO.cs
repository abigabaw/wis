using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name : PAP_LIV_REST_PLAN
    public class LivelihoodRestorationBO
    {
        public int Liv_Rest_PlanID { get; set; }

        public int Liv_Rest_LocationID { get; set; }

        public int Liv_Rest_ItemID { get; set; }

        public int Liv_Rest_RecdID { get; set; }

        public int UnitID { get; set; }

        public decimal Planned { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime PlannedDate { get; set; }

        public decimal Received { get; set; }

        public DateTime ReceivedDate { get; set; }
        //Derived Objects from other Table
        public string UnitName { get; set; }

        public string ItemName { get; set; }

        //public BatchBO BatchBO { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}