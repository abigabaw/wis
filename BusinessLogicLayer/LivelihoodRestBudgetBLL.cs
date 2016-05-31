using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
    public class LivelihoodRestBudgetBLL
    {
        #region Declaration Scetion
        LivelihoodRestBudgetDAL oLiveRestBudgetDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get Live Rest Budget
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public LivelihoodRestBudgetList GetLiveRestBudget(int projectID)
        {
            oLiveRestBudgetDAL = new LivelihoodRestBudgetDAL();
            return oLiveRestBudgetDAL.GetLiveRestBudget(projectID);
        }

        /// <summary>
        /// To Get Live Rest Budget By ID
        /// </summary>
        /// <param name="LivRestBudgID"></param>
        /// <returns></returns>
        public LivelihoodRestBudgetBO GetLiveRestBudgetByID(int LivRestBudgID)
        {
            oLiveRestBudgetDAL = new LivelihoodRestBudgetDAL();
            return oLiveRestBudgetDAL.GetLiveRestBudgetById(LivRestBudgID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// TO Add Live Rest Budget
        /// </summary>
        /// <param name="oLivelihoodRestBudgetBO"></param>
        /// <returns></returns>
        public string[] AddLiveRestBudget(LivelihoodRestBudgetBO oLivelihoodRestBudgetBO)
        {
            oLiveRestBudgetDAL = new LivelihoodRestBudgetDAL();

            return oLiveRestBudgetDAL.AddLiveRestBudget(oLivelihoodRestBudgetBO);
        }

        /// <summary>
        /// To Update Live Rest Budget
        /// </summary>
        /// <param name="oLivelihoodRestBudgetBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestBudget(LivelihoodRestBudgetBO oLivelihoodRestBudgetBO)
        {
            oLiveRestBudgetDAL = new LivelihoodRestBudgetDAL();

            return oLiveRestBudgetDAL.UpdateLiveRestBudget(oLivelihoodRestBudgetBO);
        }

        /// <summary>
        /// To Delete Live Rest Budget
        /// </summary>
        /// <param name="PaymentRequestId"></param>
        /// <returns></returns>
        public int DeleteLiveRestBudget(int PaymentRequestId)
        {
            oLiveRestBudgetDAL = new LivelihoodRestBudgetDAL();
            return oLiveRestBudgetDAL.DeleteLiveRestBudget(PaymentRequestId);
        }
        #endregion
    }
}