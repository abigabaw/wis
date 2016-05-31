using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class StructureConditionBO
    {
        public int StructureConditionID
        {
            get;
            set;
        }

        public string StructureConditionName
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public string IsDeleted
        {
            get;
            set;
        }
    }
}