using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
    public class LiveRestBudVillagesBLL
    {
        #region Declaration Scetion

        LiveRestBudVillagesDAL oLiveRestBudVillagesDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get Live Rest Bud Villages
        /// </summary>
        /// <returns></returns>
        public LiveRestBudVillagesList GetLiveRestBudVillages()
        {
            oLiveRestBudVillagesDAL = new LiveRestBudVillagesDAL();
            return oLiveRestBudVillagesDAL.GetLiveRestBudVillages();
        }

        /// <summary>
        /// To Get Live Rest Bud Villages
        /// </summary>
        /// <param name="LivRestBudgetId"></param>
        /// <returns></returns>
        public LiveRestBudVillagesList GetLiveRestBudVillagesById(int LivRestBudgetId)
        {
            oLiveRestBudVillagesDAL = new LiveRestBudVillagesDAL();
            return oLiveRestBudVillagesDAL.GetLiveRestBudVillagesById(LivRestBudgetId);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Live Rest Bud Villages
        /// </summary>
        /// <param name="oLiveRestBudVillagesBO"></param>
        /// <returns></returns>
        public string AddLiveRestBudVillages(LiveRestBudVillagesBO oLiveRestBudVillagesBO)
        {
            oLiveRestBudVillagesDAL = new LiveRestBudVillagesDAL();

            return oLiveRestBudVillagesDAL.AddLiveRestBudVillages(oLiveRestBudVillagesBO);
        }

        /// <summary>
        /// To Update Live Rest Bud Villages
        /// </summary>
        /// <param name="oLiveRestBudVillagesBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestBudVillages(LiveRestBudVillagesBO oLiveRestBudVillagesBO)
        {
            oLiveRestBudVillagesDAL = new LiveRestBudVillagesDAL();

            return oLiveRestBudVillagesDAL.UpdateLiveRestBudVillages(oLiveRestBudVillagesBO);
        }
        
        /// <summary>
        /// To Delete Live Rest Bud Villages
        /// </summary>
        /// <param name="PaymentRequestId"></param>
        /// <returns></returns>
        public int DeleteLiveRestBudVillages(int PaymentRequestId)
        {
            oLiveRestBudVillagesDAL = new LiveRestBudVillagesDAL();
            return oLiveRestBudVillagesDAL.DeleteLiveRestBudVillages(PaymentRequestId);
        }
        #endregion
    }
}