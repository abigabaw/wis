using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class RouteCriteriaScoreBO
    {
        public int ScoreId { get; set; }
        public string ScoreDescription { get; set; }

        public string IsDeleted { get; set; }
        public int UserId { get; set; }
        public string oDate { get; set; }
    }
}
