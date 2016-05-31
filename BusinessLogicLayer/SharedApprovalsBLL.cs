using System;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class SharedApprovalsBLL
    {
        /// <summary>
        /// To Get My Task Details
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public MyTasks_ApprovalList GetMyTaskApprovalDetail(int UserRoleId, int AssigntoId)
        {
            SharedApprovalsDAL objMyTaskDAL = new SharedApprovalsDAL();
            return objMyTaskDAL.GetMyTaskApprovalDetail(UserRoleId, AssigntoId);
        }

        /// <summary>
        /// To Get My Track Hdr Details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        /// <param name="USERIDIN_"></param>
        /// <returns></returns>
        public TrackerHeaderList GetMyTrackHdrDetails(string ProjectId, string ModuleId, string Status, int USERIDIN_, int AssigntoId)
        {
            SharedApprovalsDAL objMyTaskDAL = new SharedApprovalsDAL();
            return objMyTaskDAL.GetMyTrackHdrDetails(ProjectId, ModuleId, Status, USERIDIN_, AssigntoId);
        }
        
        /// <summary>
        /// To Get Users
        /// </summary>
        /// <returns></returns>
        public ProjectPersonalList GetApproverUsers(int AssigntoId)
        {
            SharedApprovalsDAL objMyTaskDAL = new SharedApprovalsDAL();
            return objMyTaskDAL.GetApproverUsers(AssigntoId);
        }
        
        /// <summary>
        /// To UPdate Lock Status
        /// </summary>
        /// <param name="objApprovalCDPABUG"></param>
        /// <returns></returns>
        public void UPdateLockStatus(SharedAuthorizationBO objBo)
        {
            SharedApprovalsDAL objMyTaskDAL = new SharedApprovalsDAL();
            objMyTaskDAL.UPdateLockStatus(objBo);
        }

        
        /// <summary>
        /// To Get Lock Status
        /// </summary>
        /// <param name="WorkflowdefinationID"></param>
        /// <returns></returns>
        public SharedAuthorizationBO GetLockStatus(int TRACKERHEADERID)
        {
            SharedApprovalsDAL objMyTaskDAL = new SharedApprovalsDAL();
            return objMyTaskDAL.GetLockStatus(TRACKERHEADERID);
        }
    }
}
