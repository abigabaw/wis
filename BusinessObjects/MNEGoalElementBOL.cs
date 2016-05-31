using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class MNEGoalElementBOL
    {
        public int GoalElementID { get; set; }

        public string GoalElement { get; set; }

        public int CreatedBy { get; set; }

        public string ErrorMessage { get; set; }

        public string ISDELETED { get; set; }
    }
}
