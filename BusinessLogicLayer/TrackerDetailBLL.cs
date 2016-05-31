using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class TrackerDetailBLL
    {
        /// <summary>
        /// To Get Approval Tracker Details By ID
        /// </summary>
        /// <param name="trackerDetailID"></param>
        /// <returns></returns>
        public TrackerDetailBO GetApprovalTrackerDetailsByID(int trackerDetailID)
        {
            return (new MyTasks_ApprovalDAL()).GetApprovalTrackerDetailsByID(trackerDetailID);
        }
    }
}
