using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class BatchBLL
    {
        #region Declaration Scetion
        BatchDAL oBatchDAL;
        #endregion

        #region DEFINED CONSTANTS

        private const string REQUEST_STATUS_SUBMITTED = "Submitted";
        private const string REQUEST_STATUS_PENDING = "Request Pending";
        private const string REQUEST_STATUS_APPROVED = "Approved";
        private const string REQUEST_STATUS_DECLINED = "Declined";

        public static string RequestStatus_Submitted
        {
            get { return REQUEST_STATUS_SUBMITTED; }
        }

        public static string RequestStatus_Pending
        {
            get { return REQUEST_STATUS_PENDING; }
        }

        public static string RequestStatus_Approved
        {
            get { return REQUEST_STATUS_APPROVED; }
        }

        public static string RequestStatus_Declined
        {
            get { return REQUEST_STATUS_DECLINED; }
        }
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get Batches from database
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BatchList GetBatches(int projectID)
        {
            return (new BatchDAL()).GetBatches(projectID);
        }
         /// <summary>
        /// TO Get Payment Request By HHID from database
         /// </summary>
         /// <param name="householdID"></param>
         /// <returns></returns>
        public BatchBO GetPaymentRequestByHHID(int householdID)
        {
            return (new BatchDAL()).GetPaymentRequestByHHID(householdID);
        }
        /// <summary>
        /// TO Get Payment Request By HHID and projectid from database
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestByHHID(int ProjectId, int HHID)
        {
            return (new BatchDAL()).GetPaymentRequestByHHID(ProjectId, HHID);
        }
        /// <summary>
        /// To get payment request by projectid,hhid,ElementId,Status,ApprovalLevel
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="HHID"></param>
        /// <param name="ElementId"></param>
        /// <param name="Status"></param>
        /// <param name="ApprovalLevel"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestByHHID(int ProjectId, int HHID, int ElementId, string Status, int ApprovalLevel)
        {
            return (new BatchDAL()).GetPaymentRequestByHHID(ProjectId, HHID, ElementId, Status, ApprovalLevel);
        }
        /// <summary>
        /// To Get Payment Batches from database
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNO"></param>
        /// <param name="ToDate"></param>
        /// <param name="FromData"></param>
        /// <returns></returns>
        public BatchList GetPaymentBatches(int projectID, string BatchNO, string ToDate, string FromData)
        {
            return (new BatchDAL()).GetPaymentBatches(projectID, BatchNO, ToDate, FromData);
        }
        /// <summary>
        /// To Get Submited Payment details 
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public BatchList GetSubmitedPayment(int projectID)
        {
            oBatchDAL = new BatchDAL();
            return oBatchDAL.GetSubmitedPayment(projectID);
        }
        /// <summary>
        /// To Get Payment Request Batch details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public BatchList GetPaymentRequestBatch(int projectID, int BatchNo)
        {
            //FOR PAYMENT REQUEST BATCH
            oBatchDAL = new BatchDAL();
            return oBatchDAL.GetPaymentRequestBatch(projectID, BatchNo);
        }
        /// <summary>
        /// To Get Payment Pending Batch details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public BatchList GetPaymentPendingBatch(int projectID, int BatchNo)
        {
            //FOR PAYMENT REQUEST BATCH
            oBatchDAL = new BatchDAL();
            return oBatchDAL.GetPaymentPendingBatch(projectID, BatchNo);
        }
        /// <summary>
        /// TO Get Payment Amount Approved details
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public BatchBO GetPaymentAmountApproved(BatchBO oBatchBO)
        {
            return (new BatchDAL()).GetPaymentAmountApproved(oBatchBO);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To add batch details to database
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public BatchBO AddBatch(BatchBO oBatchBO)
        {
            oBatchDAL = new BatchDAL();
            return oBatchDAL.AddBatch(oBatchBO);
        }
        /// <summary>
        /// To Update Payment Submit details
        /// </summary>
        /// <param name="oBatchBO"></param>
        /// <returns></returns>
        public string UpdatePaymentSubmit(BatchBO oBatchBO)
        {
            oBatchDAL = new BatchDAL();
            return oBatchDAL.UpdatePaymentRequest(oBatchBO);
        }
        /// <summary>
        /// To Delete Payment Request details from database
        /// </summary>
        /// <param name="PaymentRequestId"></param>
        /// <returns></returns>
        public int DeletePaymentRequest(int PaymentRequestId)
        {
            oBatchDAL = new BatchDAL();
            return oBatchDAL.DeletePaymentRequest(PaymentRequestId);
        }
        /// <summary>
        /// To CloseBatch details
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="UserId"></param>
        /// <param name="BatchNo"></param>
        /// <returns></returns>
        public string CloseBatch(int HHID, int UserId,int BatchNo)
        {
            oBatchDAL = new BatchDAL();
            return oBatchDAL.CloseBatch(HHID, UserId, BatchNo);
        }
        /// <summary>
        /// To decline batch by ID
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <param name="PaymentRequestId"></param>
        /// <param name="HHID"></param>
        public void DeclineBatchHHID(int BatchNo, int PaymentRequestId, int HHID)
        {
            oBatchDAL = new BatchDAL();
            oBatchDAL.DeclineBatchHHID(BatchNo, PaymentRequestId, HHID);
        }

        
        #endregion

        /// <summary>
        /// To Add Comments
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <param name="PaymentRequestId"></param>
        /// <param name="HHID"></param>
        public void AddBatchComments(BatchBO oBatchBO)
        {
            oBatchDAL = new BatchDAL();
            oBatchDAL.AddBatchComments(oBatchBO);
        }
        
    }
}