using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class NewLocationBO
    {
        public int NewPlotID { get; set; }

        public int HHID { get; set; }

        public int NewPlotStatusId { get; set; }

        public string NewPlotNo { get; set; }

        public string DistanceFromOldPlot { get; set; }

        public string District { get; set; }

        public string County { get; set; }

        public string SubCounty { get; set; }

        public string Parish { get; set; }

        public string Village { get; set; }

        public DateTime DateOfSettlement { get; set; }


        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }


    }
}