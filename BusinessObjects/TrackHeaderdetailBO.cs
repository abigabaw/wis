using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class TrackHeaderdetailBO
    {
        private int trackhdrId = -1;
        private string description = String.Empty;
        private string workflowcode = String.Empty;
        private int workflowdefinationID = -1;
        private int workflowapproverID = -1;
        private int hHID = 0;
        private int elementID = 0;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }

        public int WorkflowdefinationID
        {
            get
            {
                return workflowdefinationID;
            }
            set
            {
                workflowdefinationID = value;
            }
        }

        public int WorkflowapproverID
        {
            get
            {
                return workflowapproverID;
            }
            set
            {
                workflowapproverID = value;
            }
        }

        public int TrackHdrId
        {
            get
            {
                return trackhdrId;
            }
            set
            {
                trackhdrId = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string WorkflowCode
        {
            get
            {
                return workflowcode;
            }
            set
            {
                workflowcode = value;
            }
        }
        public int WorkFlowId
        {
            get;
            set;
        }

        public string IsDeleted { get; set; }

        //public int CreatedBy { get; set; }

        //public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string ActionTakenDate { get; set; }

        public int ApproverLevel { get; set; }
    }
}
