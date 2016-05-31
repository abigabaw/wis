using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class DisabilityBLL
    {
        /// <summary>
        /// To Get Disabilities
        /// </summary>
        /// <returns></returns>
        public DisabilityList GetDisabilities()
        {
            DisabilityDAL objDisabilityDAL = new DisabilityDAL();
            return objDisabilityDAL.GetDisabilities();
        }
    }
}
