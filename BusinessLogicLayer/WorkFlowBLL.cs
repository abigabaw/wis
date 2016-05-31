using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class WorkFlowBLL
    {
        /// <summary>
        /// To Get All Module from database
        /// </summary>
        /// <returns></returns>
        public WorkFlowList getAllModule()
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.getAllModule();
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get Work Flow By Module Id
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public WorkFlowList GetWorkFlowByModuleId(int ModuleID)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.GetWorkFlowByModuleId(ModuleID);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get All Projects Person User
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList getAllProjectPersonUser(string projectID)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.getAllProjectPersonUser(projectID);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get All Employee Name
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList getAllEmpName(string projectID)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.getAllEmpName(projectID);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get Approval Role User ID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public WorkFlowList getApprovalRoleUserID(int projectID, int roleID)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.getApprovalRoleUserID(projectID, roleID);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Insert Work Flow Definition
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int InsertWorkFlow(WorkFlowBO objWorkFlow)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.InsertWorkFlow(objWorkFlow);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get Work Flow Definition
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public WorkFlowList GetWorkFlowDefinition(string projectID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.GetWorkFlowDefinition(projectID);
        }

        /// <summary>
        /// To Get Work Flow Definition by ID
        /// </summary>
        /// <param name="WorkFlowDefID"></param>
        /// <returns></returns>
        public WorkFlowBO GetWorkFlowDefByID(int WorkFlowDefID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.GetWorkFlowDefByID(WorkFlowDefID);
        }

        /// <summary>
        ///  To Delete Work Flow Definition
        /// </summary>
        /// <param name="WorkFlowDefID"></param>
        /// <returns></returns>
        public string DeleteWorkFlowDef(int WorkFlowDefID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.DeleteWorkFlowDef(WorkFlowDefID);
        }

        /// <summary>
        /// To Edit Work Flow Definition
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int EditWorkFlowDef(WorkFlowBO objWorkFlow)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.EditWorkFlowDef(objWorkFlow);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get Approver
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkflowDefID"></param>
        /// <returns></returns>
        public WorkFlowList GetApprover(string projectID, string WorkflowDefID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.GetApprover(projectID, WorkflowDefID);
        }

        /// <summary>
        /// To Get Work Flow By Id
        /// </summary>
        /// <param name="WORKAPPROVALID"></param>
        /// <returns></returns>
        public WorkFlowBO GetWorkFlowById(int WORKAPPROVALID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.GetWorkFlowById(WORKAPPROVALID);
        }

        /// <summary>
        /// To Delete Approver
        /// </summary>
        /// <param name="WORKFLOWDEFID"></param>
        /// <returns></returns>
        public int DeleteApprover(int WORKFLOWDEFID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.DeleteApprover(WORKFLOWDEFID);
        }

        /// <summary>
        /// To Insert approaver By role and Level
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int InsertAPPROVALADD(WorkFlowBO objWorkFlow)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.InsertAPPROVALADD(objWorkFlow);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }
        
        /// <summary>
        /// To Edit Approver
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int EditAPPROVALADD(WorkFlowBO objWorkFlow)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.EDITAPPROVER(objWorkFlow);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To Get All Saved WORK FLOW Data
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public WorkFlowBO getALLSaveWORKFLOWData(string pID)
        {
            WorkflowDAL WorkflowDALObj = new WorkflowDAL();
            return WorkflowDALObj.getALLSaveWORKFLOWData(pID);
        }

        /// <summary>
        /// To Update Work flow
        /// </summary>
        /// <param name="objWorkFlow"></param>
        /// <returns></returns>
        public int UpdateWorkflow(WorkFlowBO objWorkFlow)
        {
            WorkflowDAL WorkflowDAL = new WorkflowDAL(); //Data pass -to Database Layer

            try
            {
                return WorkflowDAL.UpdateWorkflow(objWorkFlow);
            }
            catch
            {
                throw;
            }
            finally
            {
                WorkflowDAL = null;
            }
        }

        /// <summary>
        /// To get WOrk Flow Approval ID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="workflowCode"></param>
        /// <returns></returns>
        public WorkFlowBO getWOrkFlowApprovalID(int projectID, string workflowCode)
        {
            WorkflowDAL ProjectRouteDALObj = new WorkflowDAL();
            return ProjectRouteDALObj.getWOrkFlowApprovalID(projectID, workflowCode);
        }

        /// <summary>
        /// To Get Total Count Approval
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowList getTotalcountapproval(WorkFlowBO objProjectRoute)
        {
            WorkflowDAL ProjectRouteDALObj = new WorkflowDAL();
            return ProjectRouteDALObj.getTotalcountapproval(objProjectRoute);
        }

        /// <summary>
        /// Approval Status Check
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowBO ApprovalStatuscheck(WorkFlowBO objProjectRoute)
        {
            WorkflowDAL ProjectRouteDALObj = new WorkflowDAL();
            return ProjectRouteDALObj.ApprovalStatuscheck(objProjectRoute);
        }

        /// <summary>
        /// Approval Status Checklist
        /// </summary>
        /// <param name="objProjectRoute"></param>
        /// <returns></returns>
        public WorkFlowList ApprovalStatuschecklist(WorkFlowBO objProjectRoute)
        {
            WorkflowDAL ProjectRouteDALObj = new WorkflowDAL();
            return ProjectRouteDALObj.ApprovalStatuschecklist(objProjectRoute);
        }
    }
}