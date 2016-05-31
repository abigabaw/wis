using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class RouteSelectionFactorsBO
    {

        public int FactorId { get; set; }
        public string FactorName { get; set; }

        public string IsDeleted { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
    }
}
