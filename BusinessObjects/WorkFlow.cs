/**
 * 
 * @version      :    Version Number Page Name
 * @package      :    WindowType/Structure
 * @copyright    :    Copyright © 2013 - All rights reserved.
 * @author       :    Iranna Shirol
 * @Created Date :    19 Apr 2013 
 * @Updated By   :
 * @Updated Date : 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UETCL_WIS.BO
{
    public class WorkFlow
    {
        private int userID = -1;
        private int roleID;
        private string roleName = String.Empty; //MODULENAME
        private int projectID;
        private int moduleID; //MODULEID
        private string moduleName = String.Empty; //MODULENAME
        private int workflowID; //WORKFLOWID
        private int approvalID; //WORKFLOWID
        private string workDesc = String.Empty; //DESCRIPTION
        private int highaultorityID;
        private string trigger;
        private int afterDays;
        private int level;
        private int workFlowDefID;
        private int workApprovalID;
        private string workIsDeleted;
        private string wcreatedDate;
        private string wUpdatedDate;
        private int  wUpdatedBy;

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        //workApprovalID
        public int WorkApprovalID
        {
            get
            {
                return workApprovalID;
            }
            set
            {
                workApprovalID = value;
            }
        }

        public int WorkFlowDefID
        {
            get
            {
                return workFlowDefID;
            }
            set
            {
                workFlowDefID = value;
            }
        }
        public int RoleID
        {
            get
            {
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }
        public int ApprovalID
        {
            get
            {
                return approvalID;
            }
            set
            {
                approvalID = value;
            }
        }
        public string RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }
        public int ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }

        public int ModuleID
        {
            get
            {
                return moduleID;
            }
            set
            {
                moduleID = value;
            }
        }
        public string ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }
        public int WorkflowID
        {
            get
            {
                return workflowID;
            }
            set
            {
                workflowID = value;
            }
        }
        public string WorkDesc
        {
            get
            {
                return workDesc;
            }
            set
            {
                workDesc = value;
            }
        }
        public int HighaultorityID
        {
            get
            {
                return highaultorityID;
            }
            set
            {
                highaultorityID = value;
            }
        }
        public string Trigger
        {
            get{
                return trigger;
            }
            set{
                trigger = value;
            }
        }
        public int AfterDays
        {
            get
            {
                return afterDays;
            }
            set
            {
                afterDays = value;
            }
        }
        public int LEVEL
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        public string WorkIsDeleted
        {
            get
            {
                return workIsDeleted;
            }
            set
            {
                workIsDeleted = value;
            }  
        }
        public string WCreatedDate
        {
            get
            {
                return wcreatedDate;
            }
            set
            {
                wcreatedDate = value;
            }  
        }
        public string WUpdatedDate
        {
            get
            {
                return wUpdatedDate;
            }
            set
            {
                wUpdatedDate = value;
            }  
        }
        public int WUpdatedBy
        {
            get
            {
                return WUpdatedBy;
            }
            set
            {
                WUpdatedBy = value;
            }  
        }
    }
}