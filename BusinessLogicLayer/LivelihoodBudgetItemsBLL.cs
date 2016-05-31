using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
    public class LivelihoodBudgetItemsBLL
    {
        #region Declaration Scetion
       // BatchDAL oBatchDAL;
        LivelihoodBudgetItemsDAL oLiveBudgItemsDAL;
        #endregion

        #region DEFINE CONSTANTS

        //private const string REQUEST_STATUS_SUBMITTED = "Submitted";
        //private const string REQUEST_STATUS_PENDING = "Request Pending";

        //public static string RequestStatus_Submitted
        //{
        //    get { return REQUEST_STATUS_SUBMITTED; }
        //}

        //public static string RequestStatus_Pending
        //{
        //    get { return REQUEST_STATUS_PENDING; }
        //}
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get Livelihood Budget Items
        /// </summary>
        /// <param name="prmLiveBudgItemsBO"></param>
        /// <returns></returns>
        public LivelihoodBudgetItemsList GetLivBudgetItems(LivelihoodBudgetItemsBO prmLiveBudgItemsBO)
        {
            oLiveBudgItemsDAL = new LivelihoodBudgetItemsDAL();

            return oLiveBudgItemsDAL.GetLivBudgetItems_ById(prmLiveBudgItemsBO);
           // return null;
        }
        #endregion

        #region Add, Update & Delete Record(s)
        //public BatchBO AddBatch(BatchBO oBatchBO)
        //{
        //    oBatchDAL = new BatchDAL();

        //    return oBatchDAL.AddBatch(oBatchBO);
        //}

        //public string UpdatePaymentSubmit(BatchBO oBatchBO)
        //{
        //    oBatchDAL = new BatchDAL();

        //    return oBatchDAL.UpdatePaymentRequest(oBatchBO);
        //}

        //public int DeletePaymentRequest(int PaymentRequestId)
        //{
        //    oBatchDAL = new BatchDAL();
        //    return oBatchDAL.DeletePaymentRequest(PaymentRequestId);
        //}
        #endregion
    }
}