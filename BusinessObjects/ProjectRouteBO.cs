using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ProjectRouteBO
    {
        private int route_ID = -1;
        private int project_Id = -1;              
        private string route_Name = String.Empty;
        private string route_Details = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        //Add By Ramu.S
        private string workFlowApprover = String.Empty;
        private string cellNumber = String.Empty;
        private string emailID = String.Empty;
        private int workFlowDefinitionID = -1;
        private int workFlowApproverID = -1;

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
        private int approvedstatusID = -1;
        private string isFinal = string.Empty;
        private int elementID;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

        public string IsFinal
        {
            get { return isFinal; }
            set { isFinal = value; }
        }

        public int ApprovedstatusID
        {
            get
            {
                return approvedstatusID;
            }
            set
            {
                approvedstatusID = value;
            }
        }

        public string Approveddate
        {
            get { return approveddate; }
            set { approveddate = value; }
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
        
        public int TotalRouteScore { get; set; }

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

        public int Route_ID
        {
            get
            {
                return route_ID;
            }
            set
            {
                route_ID = value;
            }
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


        public string Route_Name
        {
            get
            {
                return route_Name;
            }
            set
            {
                route_Name = value;
            }
        }

        public string Route_Details
        {
            get
            {
                return route_Details;
            }
            set
            {
                route_Details = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }

        public string AppName { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public string ActioDate { get; set; }
        public string Comments { get; set; }
    }
}