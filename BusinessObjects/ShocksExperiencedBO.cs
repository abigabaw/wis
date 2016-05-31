using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ShocksExperiencedBO
    {
        public int ShocksExperiencedID
        {
            get;
            set;
        }

        public string ShocksExperience
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