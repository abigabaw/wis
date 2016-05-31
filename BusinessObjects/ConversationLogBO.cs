using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ConversationLogBO
    {
        //TRN_CMP_BATCH Table
        public int ProjectId { get; set; }

        public int TrackerHeaderId { get; set; }

        public int TrackerDetailId { get; set; }

        public int RequesterId { get; set; }

        public string RequesterName { get; set; }

        public int ApproverId { get; set; }

        public string ApproverName { get; set; }

        public string ApproverComments { get; set; }

        public string eMailBody { get; set; }

        public string eMailSubject { get; set; }

        public string RequestDateTime { get; set; }

        public string ApprovalDateTime { get; set; }

        public string PageCode{ get; set; }

        public string WorkFlowCode { get; set; }

        //Id's
        public int HHID { get; set; }

        public string WorkFlowDescription { get; set; }

        public string Status { get; set; }

        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        //Temp Variable
        public string dbMessage { get; set; }

        public string PAPName { get; set; }
    }
}