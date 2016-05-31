using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_LivelihoodBLL
    {
        /// <summary>
        /// To Get Livelihood Items By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_LivelihoodList GetLivelihoodItemsByID(int householdID)
        {
            return (new PAP_LivelihoodDAL()).GetLivelihoodItemsByID(householdID);
        }

        /// <summary>
        /// To Update Livelihood
        /// </summary>
        /// <param name="LivelihoodItems"></param>
        public void UpdateLivelihood(PAP_LivelihoodList LivelihoodItems)
        {
            (new PAP_LivelihoodDAL()).UpdateLivelihood(LivelihoodItems);
        }
    }
}