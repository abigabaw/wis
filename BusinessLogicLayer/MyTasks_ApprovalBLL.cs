using System;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class MyTasks_ApprovalBLL
    {
        /// <summary>
        /// To Get My Task Details
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        public MyTasks_ApprovalList GetMyTaskDetails(int UserRoleId)
        {
            MyTasks_ApprovalDAL objMyTaskDAL = new MyTasks_ApprovalDAL();
            return objMyTaskDAL.GetMyTaskApprovalDetail(UserRoleId);
        }

        /// <summary>
        /// To Get My Track Hdr Details
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ModuleId"></param>
        /// <param name="Status"></param>
        /// <param name="USERIDIN_"></param>
        /// <returns></returns>
        public TrackerHeaderList GetMyTrackHdrDetails(string ProjectId, string ModuleId, string Status, int USERIDIN_)
        {
            MyTasks_ApprovalDAL objMyTaskDAL = new MyTasks_ApprovalDAL();
            return objMyTaskDAL.GetMyTrackHdrDetails(ProjectId, ModuleId, Status, USERIDIN_);
        }

        /// <summary>
        /// To Get Final Project Details
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="myActiveHHID"></param>
        /// <returns></returns>
        public ApprovalscoredtlList GetFinalProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int myActiveHHID)//(string WorkflowCode)
        {
            MyTasks_ApprovalDAL objMyTaskDAL = new MyTasks_ApprovalDAL();
            return objMyTaskDAL.GetFinalProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, myActiveHHID);//,ProjectId);
        }

        /// <summary>
        /// To Add Work flow Approval
        /// </summary>
        /// <param name="objWorkflowapproval"></param>
        /// <param name="AppDataCount"></param>
        /// <returns></returns>
        public int AddWorkflowApproval(WorkflowApprovalBO objWorkflowapproval, int AppDataCount)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            return objMyTaskApprovalDAL.AddWorkflowApproval(objWorkflowapproval, AppDataCount);
        }

        
        

        /// <summary>
        /// To UPdate Final Route
        /// </summary>
        /// <param name="objFinalRoute"></param>
        public void UPdateFinalRoute(ApprovalscoredtlBO objFinalRoute)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            objMyTaskApprovalDAL.UPdateFinalRoute(objFinalRoute);
        }

        /// <summary>
        /// To Get Email ID
        /// </summary>
        /// <param name="WorkflowdefinationID"></param>
        /// <returns></returns>
        public ApprovalscoredtlBO GetEmailID(int WorkflowdefinationID)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            return objMyTaskApprovalDAL.GetEmailID(WorkflowdefinationID);
        }

        /// <summary>
        /// To Get CR Project Details
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="WORKFLOWAPPID"></param>
        /// <param name="myActiveHHID"></param>
        /// <param name="TrackerDetailID_"></param>
        /// <returns></returns>
        public ApprovalscoredtlBO GetCRProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, int TrackerDetailID_)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            return objMyTaskApprovalDAL.GetCRProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, TrackerDetailID_);
        }

        /// <summary>
        /// To Get Approved Final Project Details
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="myActiveHHID"></param>
        /// <returns></returns>
        public ApprovalscoredtlList GetApprovedFinalProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int myActiveHHID)//(string WorkflowCode)
        {
            MyTasks_ApprovalDAL objMyTaskDAL = new MyTasks_ApprovalDAL();
            return objMyTaskDAL.GetApprovedFinalProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, myActiveHHID);//,ProjectId);
        }

        /// <summary>
        /// To Get Approval CR Project Details
        /// </summary>
        /// <param name="WorkFlowId"></param>
        /// <param name="ProjectId"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="WORKFLOWAPPID"></param>
        /// <param name="myActiveHHID"></param>
        /// <param name="pageCode"></param>
        /// <param name="Status_id"></param>
        /// <returns></returns>
        public ApprovalscoredtlBO GetApprovalCRProjectDetails(int WorkFlowId, int ProjectId, string WorkFlowCode, int WORKFLOWAPPID, int myActiveHHID, string pageCode, int Status_id)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            return objMyTaskApprovalDAL.GetApprovalCRProjectDetails(WorkFlowId, ProjectId, WorkFlowCode, WORKFLOWAPPID, myActiveHHID, pageCode, Status_id);
        }

        /// <summary>
        /// To UPdate Final Value
        /// </summary>
        /// <param name="objFinalValue"></param>
        public void UPdateFinalValue(ApprovalscoredtlBO objFinalValue)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            objMyTaskApprovalDAL.UPdateFinalValue(objFinalValue);
        }

        /// <summary>
        /// To UPdate Final Value For IndNeg
        /// </summary>
        /// <param name="trackerheader"></param>
        /// <param name="ChangeRequest"></param>
        public void UPdateFinalValueForIndNeg(int trackerheader, string ChangeRequest)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            objMyTaskApprovalDAL.UPdateFinalValueForIndNeg(trackerheader, ChangeRequest);
        }

        #region For Grievance Update
        /// <summary>
        /// To UPdate Grievance
        /// </summary>
        /// <param name="objGrievance"></param>
        ///
        public void UPdateGrievance(GrievancesBO objGrievance)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            objMyTaskApprovalDAL.UPdateGrievance(objGrievance);
        }
        #endregion

        /// <summary>
        /// To UPdate CDAP BUG Status
        /// </summary>
        /// <param name="objApprovalCDPABUG"></param>
        public void UPdateCDAPBUGStatus(ApprovalscoredtlBO objApprovalCDPABUG)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            objMyTaskApprovalDAL.UPdateCDAPBUGStatus(objApprovalCDPABUG);
        }

        /// <summary>
        /// To Get Approval Comments
        /// </summary>
        /// <param name="TrackHeaderID"></param>
        /// <param name="StatusID"></param>
        /// <returns></returns>
        public ApprovalscoredtlBO GetApprovalComments(int TrackHeaderID, int StatusID)
        {
            MyTasks_ApprovalDAL objMyTaskApprovalDAL = new MyTasks_ApprovalDAL();
            return objMyTaskApprovalDAL.GetApprovalComments(TrackHeaderID, StatusID);
        }

    }
}