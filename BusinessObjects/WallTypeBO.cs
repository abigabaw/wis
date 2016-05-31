using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class WallTypeBO
    {
        public int WallTypeID
        {
            get;
            set;
        }

        public string WallTypeName
        {
            get;
            set;
        }

        //public int CreatedBy
        //{
        //    get;
        //    set;
        //}
        public int UserID
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