using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CDAPBudgetMasterBO
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
    }
}
