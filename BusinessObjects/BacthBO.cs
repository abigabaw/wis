using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class BatchBO
    {
        //TRN_CMP_BATCH Table
        public string CMP_BatchNo { get; set; }

        public string BatchCreatedDate { get; set; }

        public string BatchSubmittedDate { get; set; }

        public string BatchStatus { get; set; }  

        //TRN_CMP_PAYREQ_BATCHES Table
        public int Payt_RequestID { get; set; }

        //PAYT_REQUESTDATE Table
        public int HHID { get; set; }
        public int HHID_DISP { get; set; }

        public string Payt_RequestDate { get; set; }

        public string RequestStatus { get; set; }

        public string RequestStatusShow { get; set; }

        public string Payt_Description { get; set; }

        public decimal Amt_Requested { get; set; }

        public string Comments { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal InKindValue { get; set; }


        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        //Temp Variable
        public string dbMessage { get; set; }

        public string PAPName { get; set; }

        public string PlotRef { get; set; }

        public string Designation { get; set; }

        public int TOTALCount { get; set; }
        public int TOTALApproved { get; set; }
        public int TOTALDeclined { get; set; }
        public int TOTALPending { get; set; }
        public int StausLevel { get; set; }
    }
}