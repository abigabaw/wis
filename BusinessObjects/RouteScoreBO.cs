using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class RouteScoreBO
    {
        public int RouteScoreId { get; set; }
        public int RouteId { get; set; }
        public int CriteriaId { get; set; }
        public int ScoreId { get; set; }

        public int FactorId { get; set; }

        public string IsDeleted { get; set; }
        public int UserId { get; set; }
        public string oDate { get; set; }

    }
}
