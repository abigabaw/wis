using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ProjectRouteBLL
    {
        /// <summary>
        /// To Add Project Routes
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public int AddProjectRoutes(ProjectRouteBO objProjectRoute)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return  objProjectRouteDAL.AddProjectRoutes(objProjectRoute);            
        }

        /// <summary>
        /// To Get Project Route by Id
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ProjectRouteList GetProjectRoutebyId(int ProjectId)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return objProjectRouteDAL.GetProjectRoutebyId(ProjectId);
        }

        /// <summary>
        /// To get WOrk Flow Approval ID
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteBO getWOrkFlowApprovalID(ProjectRouteBO objProjectRoute)
        {
            ProjectRouteDAL ProjectRouteDALObj = new ProjectRouteDAL();
            return ProjectRouteDALObj.getWOrkFlowApprovalID(objProjectRoute);
        }

        /// <summary>
        /// To Add Approval Track header
        /// </summary>
        /// <param name="objApprovalHeaderSave"></param>
        /// <returns></returns>
        public string AddApprovalTrackheader(ProjectRouteBO objApprovalHeaderSave)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return objProjectRouteDAL.AddApprovalTrackheader(objApprovalHeaderSave);  
        }

        /// <summary>
        /// To get Final Route Approval Detial
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteList getFinalRouteApprovalDetial(ProjectRouteBO objProjectRoute)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return objProjectRouteDAL.getFinalRouteApprovalDetial(objProjectRoute);
        }
        //After clicking Approval Button checking status 
        /// <summary>
        /// To find Route Status after Save
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public ProjectRouteBO findRouteStatusafterSave(ProjectRouteBO objProjectRoute)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return objProjectRouteDAL.findRouteStatusafterSave(objProjectRoute);
        }

        /// <summary>
        /// To Get Approved Comments
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public ProjectRouteList GetApprovedComments(int ProjectID)
        {
            ProjectRouteDAL objProjectRouteDAL = new ProjectRouteDAL();
            return objProjectRouteDAL.GetApprovedComments(ProjectID);
        }
    }
}