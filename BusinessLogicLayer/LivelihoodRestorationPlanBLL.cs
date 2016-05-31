using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
    public class LivelihoodRestorationPlanBLL
    {
        #region Declaration Scetion
        LivelihoodRestorationPlanDAL oLiveRestPlanDAL;
        #endregion

        #region DEFINE CONSTANTS
       
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get Live Rest Plan
        /// </summary>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        public LivelihoodRestorationList GetLiveRestPlan(int LocationId)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.GetLivelihoodRestorationPlan(LocationId);
        }

        /// <summary>
        /// To Get Live Rest Plan By Id
        /// </summary>
        /// <param name="LivRestPlanId"></param>
        /// <returns></returns>
        public LivelihoodRestorationBO GetLiveRestPlanById(int LivRestPlanId)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.GetLivelihoodRestorationPlanById(LivRestPlanId);
        }

        /// <summary>
        /// To Get Livelihood Rest Received By Plan Id
        /// </summary>
        /// <param name="LivRestPlanId"></param>
        /// <returns></returns>
        public LivelihoodRestorationList GetLivelihoodRestReceivedByPlanId(int LivRestPlanId)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.GetLivelihoodRestReceivedByPlanId(LivRestPlanId);
        }

        /// <summary>
        /// To Get Item Received By Plan Id
        /// </summary>
        /// <param name="LivRestPlanId"></param>
        /// <returns></returns>
        public LivelihoodRestorationBO GetItemReceivedByPlanId(int LivRestPlanId)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.GetItemReceivedByPlanId(LivRestPlanId);
        }
        #endregion

        #region Add, Update & Delete Record(s) for Received Plan
        /// <summary>
        /// To Add Livelihood Restoration Plan
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
        public string AddLivelihoodRestorationPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();

            return oLiveRestPlanDAL.AddLiveRestPlan(oLiveRestPlanBO);
        }

        /// <summary>
        /// To Update Livelihood Restoration Plan
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
        public string UpdateLivelihoodRestorationPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();

            return oLiveRestPlanDAL.UpdateLiveRestPlan(oLiveRestPlanBO);
        }

        //public string UpdatePaymentSubmit(LivelihoodRestorationPlanBO oLivelihoodRestorationPlanBO)
        //{
        //    oLivelihoodRestorationPlanDAL = new LivelihoodRestorationPlanDAL();

        //    return oLivelihoodRestorationPlanDAL.UpdatePaymentRequest(oLivelihoodRestorationPlanBO);
        //}
        /// <summary>
        /// To Delete Livelihood Restoration Plan
        /// </summary>
        /// <param name="LivelihoodRestorationPlanId"></param>
        /// <returns></returns>
        public string DeleteLivRestPlan(int LivelihoodRestorationPlanId)
        { 
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.DeleteLivRestPlan(LivelihoodRestorationPlanId);
        }
       
        #endregion

        #region Add, Update & Delete Record(s) for Received Plan
        /// <summary>
        /// To Add Live Received Plan
        /// </summary>
        /// <param name="oLiveRestPlanBO"></param>
        /// <returns></returns>
        public string AddLiveReceivedPlan(LivelihoodRestorationBO oLiveRestPlanBO)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.AddLiveReceivedPlan(oLiveRestPlanBO);
        }

        /// <summary>
        /// To Delete Item Received
        /// </summary>
        /// <param name="ReceivedId"></param>
        /// <returns></returns>
        public string DeleteItemReceived(int ReceivedId)
        {
            oLiveRestPlanDAL = new LivelihoodRestorationPlanDAL();
            return oLiveRestPlanDAL.DeleteItemReceived(ReceivedId);
        }
        #endregion Add, Update & Delete Record(s) for Received Plan
    }
}