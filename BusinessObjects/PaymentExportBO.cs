using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PaymentExportBO
    {

        public int CompPaymentId { get; set; }

        public int HHID { get; set; }

        public string CompensationType { get; set; }

        public int ModeOfPaymentId { get; set; }

        public string ModeOfPayment { get; set; }

        public decimal CompensationAmount { get; set; }

        public string DeliveredToStakeHolder { get; set; }

        public string DeliveredDate { get; set; }

        public int BankID { get; set; }

        public int BranchID { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string BankCode { get; set; }

        public string FixedCostCentre { get; set; }

        public int FixedCostCentreID { get; set; }

        public string BankReference { get; set; }

        public static decimal TotalCompensationAmount { get; set; }

        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string FundReqStatus { get; set; }

        public string PROJECTNAME { get; set; }

        public string Papname { get; set; }

        public string TReference { get; set; }

        public string SEGMENTNAME { get; set; }
    }
}
