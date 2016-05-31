using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class UnitBO
    {
        public int UnitID
        {
            get;
            set;
        }

        public string UnitName
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