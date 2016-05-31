using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ConversationLogBLL
    {
        #region Declaration Scetion
      //  ConversationLogDAL oConversationLogDAL;
        #endregion

      
        #region Get Record(s)
        /// <summary>
        /// To Get Sender Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="PageCode"></param>
        /// <param name="TrackHdrId"></param>
        /// <returns></returns>
        public ConversationLogList GetSenderDetails(int projectID, string WorkFlowCode, string PageCode, string TrackHdrId)
        {
            return (new ConversationLogDAL()).GetSenderDetails(projectID, WorkFlowCode, PageCode, TrackHdrId);
        }

        /// <summary>
        /// To Get Approver Details
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="WorkFlowCode"></param>
        /// <param name="PageCode"></param>
        /// <param name="TrackHdrId"></param>
        /// <returns></returns>
        public ConversationLogList GetApproverDetails(int projectID, string WorkFlowCode, string PageCode, string TrackHdrId, int BatchNo)
        {
            return (new ConversationLogDAL()).GetApproverDetails(projectID, WorkFlowCode, PageCode, TrackHdrId, BatchNo);
        }

        //public BatchBO GetPaymentRequestByHHID(int householdID)
        //{
        //    return (new ConversationLogDAL()).GetPaymentRequestByHHID(householdID);
        //}

        //public BatchList GetPaymentRequestByHHID(int ProjectId, int HHID)
        //{
        //    return (new ConversationLogDAL()).GetPaymentRequestByHHID(ProjectId,HHID);
        //}

        //public BatchList GetPaymentBatches(int projectID)
        //{
        //    return (new ConversationLogDAL()).GetPaymentBatches(projectID);
        //}

        //public BatchList GetSubmitedPayment(int projectID)
        //{
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.GetSubmitedPayment(projectID);
        //}

        //public BatchList GetPaymentRequestBatch(int projectID,int BatchNo)
        //{
        //    //FOR PAYMENT REQUEST BATCH
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.GetPaymentRequestBatch(projectID,BatchNo);
        //}

        //public BatchList GetPaymentPendingBatch(int projectID, int BatchNo)
        //{
        //    //FOR PAYMENT REQUEST BATCH
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.GetPaymentPendingBatch(projectID, BatchNo);
        //}
        #endregion

        #region Add, Update & Delete Record(s)
        //public BatchBO AddBatch(BatchBO oBatchBO)
        //{
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.AddBatch(oBatchBO);
        //}

        //public string UpdatePaymentSubmit(BatchBO oBatchBO)
        //{
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.UpdatePaymentRequest(oBatchBO);
        //}

        //public int DeletePaymentRequest(int PaymentRequestId)
        //{
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.DeletePaymentRequest(PaymentRequestId);
        //}

        //public string CloseBatch(int HHID, int UserId)
        //{
        //    oConversationLogDAL = new ConversationLogDAL();
        //    return oConversationLogDAL.CloseBatch(HHID,UserId);
        //}
        #endregion

        public ConversationLogList GetBatchComments(int BatchNo, int HHID)
        {
            return (new ConversationLogDAL()).GetBatchComments(BatchNo, HHID);
        }
    }
}