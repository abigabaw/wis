using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class WorkFlowBO
    {
        private int userID = -1;
        private string userName = string.Empty;
        private int roleID;
        private string roleName = String.Empty; //MODULENAME
        private int projectID;
        private int moduleID; //MODULEID
        private string moduleName = String.Empty; //MODULENAME
        private int workflowID; //WORKFLOWID
        private string workflowName = String.Empty; //MODULENAME
        private int approvalID; //WORKFLOWID
        private string workDesc = String.Empty; //DESCRIPTION
        private int higherAuthorityID;
        private string trigger;
        private int afterDays;
        private int level;
        private int workFlowDefID;
        private int workApprovalID;
        private string workIsDeleted;
        private string wcreatedDate;
        private string wUpdatedDate;
        private int  wUpdatedBy;
        private int rowNumber;
        private int countApproval;
        private string workflowCode = string.Empty;
        private int approvalstatusID;
        private int eLEMENTID = 0;
        private string higherAuthorityName;
        private string higherAuthorityEmailID;

        public int ELEMENTID
        {
            get { return eLEMENTID; }
            set { eLEMENTID = value; }
        }

        public int ApprovalstatusID
        {
            get { return approvalstatusID; }
            set { approvalstatusID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string WorkflowCode
        {
            get { return workflowCode; }
            set { workflowCode = value; }
        }

        //Start: Edwin 02FEB2017 for Notifying Higher Authority
        public string HigherAuthorityName
        {
            get { return higherAuthorityName;  }
            set { higherAuthorityName = value; }
        }

        public string HigherAuthorityEmailID
        {
            get { return higherAuthorityEmailID; }
            set { higherAuthorityEmailID = value; }
        }
        //End:

        public int CountApproval
        {
            get { return countApproval; }
            set { countApproval = value; }
        }
        public string WorkflowName
        {
            get { return workflowName; }
            set { workflowName = value; }
        }
        public int RowNumber
        {
            get { return rowNumber; }
            set { rowNumber = value; }
        }
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
        public int HigherAuthorityID
        {
            get
            {
                return higherAuthorityID;
            }
            set
            {
                higherAuthorityID = value;
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
                return wUpdatedBy;
            }
            set
            {
                wUpdatedBy = value;
            }  
        }

        #region check exit mail
        private string workFlowApprover = String.Empty;
        private string cellNumber = String.Empty;
        private string emailID = String.Empty;
        private int workFlowDefinitionID = -1;
        private int workFlowApproverID = -1;
        private int project_Id = -1;   

        private int approverUserID = -1;
        private string approverUserName = String.Empty;
        private string projectCode = String.Empty;
        private string projectName = String.Empty;
        private string emailSubject = String.Empty;
        private string emailBody = String.Empty;
        private string smsText = String.Empty;
        private string statusID = String.Empty;
        private string approverComment = string.Empty;
        private string approveddate = string.Empty;
        private string duedate = string.Empty;

        public string Approveddate
        {
            get { return approveddate; }
            set { approveddate = value; }
        }

        public string Duedate
        {
            get { return duedate; }
            set { duedate = value; }
        }
        #region Email Popup
        //Data form EmailMailPOPUP window
        private int hHID;
        private string pageCode = string.Empty;

        public string PageCode
        {
            get { return pageCode; }
            set { pageCode = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
        //End
        #endregion


        public string ApproverComment
        {
            get { return approverComment; }
            set { approverComment = value; }
        }

        public string StatusID
        {
            get { return statusID; }
            set { statusID = "3"; }
        }

        public int WorkFlowApproverID
        {
            get { return workFlowApproverID; }
            set { workFlowApproverID = value; }
        }
        public string SmsText
        {
            get { return smsText; }
            set { smsText = value; }
        }

        public string EmailBody
        {
            get { return emailBody; }
            set { emailBody = value; }
        }
        public string EmailSubject
        {
            get { return emailSubject; }
            set { emailSubject = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        public string ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

        public string ApproverUserName
        {
            get { return approverUserName; }
            set { approverUserName = value; }
        }

        public int ApproverUserID
        {
            get { return approverUserID; }
            set { approverUserID = value; }
        }


        public int WorkFlowDefinitionID
        {
            get { return workFlowDefinitionID; }
            set { workFlowDefinitionID = value; }
        }
        public string EmailID
        {
            get { return emailID; }
            set { emailID = value; }
        }

        public string CellNumber
        {
            get { return cellNumber; }
            set { cellNumber = value; }
        }

        public string WorkFlowApprover
        {
            get { return workFlowApprover; }
            set { workFlowApprover = value; }
        }

        public int Project_Id
        {
            get
            {
                return project_Id;
            }
            set
            {
                project_Id = value;
            }
        }     

        #endregion
    }
}