using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class ActivityBLL
    {
        /// <summary>
        /// Call GetActivity to Get Activity Details
        /// </summary>
        /// <returns></returns>
        public ActivityList GetActivity()
        {
            return (new ActivityDAL()).GetActivity();
        }
    }
}
