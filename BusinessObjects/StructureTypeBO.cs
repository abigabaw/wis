using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class StructureTypeBO
    {
        public int StructureTypeID
        {
            get;
            set;
        }

        public string StructureTypeName
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