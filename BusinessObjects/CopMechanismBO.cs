using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CopMechanismBO
    {       
        public int CopMechanismID
        {
            get;
            set;
        }

        public string CopMechanismName
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