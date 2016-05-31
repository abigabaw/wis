using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**
 * 
 * @version		 0.1 Concern BUsinessObject
 * @package		 Concern
 * @copyright	 Copyright © 2013 - All rights reserved.
 * @author		 Ramu.S
 * @Created Date 17-April-203
 * @Updated By
 * @Updated Date
 * 
 */
namespace WIS_BusinessObjects
{
    public class PaymentBO
    {
        //MST_MODEOFPAYMENT Table
        public int ModeOfPaymentId { get; set;}

        public string ModeOfPayment { get; set; }

        public string PaymentStatus { get; set; }

        public string GRIEVOVERRIDE { get; set; }

        public string FILECLOSINGCOMMENTS { get; set; }

        public class CompensationPayementBO
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

            public  static decimal TotalCompensationAmount { get; set; }

            //Common Fields
            public string IsDeleted { get; set; }

            public int CreatedBy { get; set; }

            public string CreatedDate { get; set; }

            public int UpdatedBy { get; set; }

            public string UpdatedDate { get; set; }

            public string FundReqStatus { get; set; }

            public string BatchNos { get; set; }
        }

        //TRN_PAP_VALUATION_SUMMARY
        public decimal GrandTotal { get; set; }

        public string NegotiatedAmountApproved { get; set; }

        public decimal NegotiatedAmount { get; set; }

        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        //Temp Variable
    }
}