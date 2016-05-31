using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class StructureCategoryBO
    {
        public int StructureCategoryID
        {
            get;
            set;
        }

        public string StructureCategoryName
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