using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class WorkflowApprovalBO
    {
        private int workflowapprovalId = 0;
        private string status = String.Empty;
        private int trackerHdrID = 0;
        private int authoriserID = 0;
        private int workFlowDefinationId = 0;
        private int auctiontakenby = 1;
        private string auctiontakedate = String.Empty;
        private string approvercomments = string.Empty;
        private int approvalLevel = 0;
        private string workFlowCode = string.Empty;
        private int projectID = 0;
        private int hHID = 0;
        private string pageCode = string.Empty;
        private int elementID = 0;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

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

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        public string WorkFlowCode
        {
            get { return workFlowCode; }
            set { workFlowCode = value; }
        }
        public int ApprovalLevel
        {
            get { return approvalLevel; }
            set { approvalLevel = value; }
        }
        public string Approvercomments
        {
            get { return approvercomments; }
            set { approvercomments = value; }
        }
         public int Auctiontakenby
        {
            get
            {
                return auctiontakenby;
            }
            set
            {
                auctiontakenby = value;
            }
        }

        public string Auctiontakedate
        {
            get
            {
                return auctiontakedate;
            }
            set
            {
                auctiontakedate = value;
            }
        }

        public int WorkflowapprovalId
        {
            get
            {
                return workflowapprovalId;
            }
            set
            {
                workflowapprovalId = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public int TrackerHdrID
        {
            get
            {
                return trackerHdrID;
            }
            set
            {
                trackerHdrID = value;
            }
        }

        public int AuthoriserID
        {
            get
            {
                return authoriserID;
            }
            set
            {
                authoriserID = value;
            }
        }

        public int WorkFlowDefinationId
        {
            get
            {
                return workFlowDefinationId;
            }
            set
            {
                workFlowDefinationId = value;
            }
        }
    }
}
