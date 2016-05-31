using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class MNEGoalBO
    {
        public int GoalID { get; set; }

        public string GoalName { get; set; }

        public int CreatedBy { get; set; }

        public string ErrorMessage { get; set; }

        public string ISDELETED { get; set; }

       
    }
}
