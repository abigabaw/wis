using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_StakeholderBLL
    {
        /// <summary>
        /// To Get Stake holder By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_StakeholderBO GetStakeholderByID(int householdID)
        {
            return (new PAP_StakeholderDAL()).GetStakeholderByID(householdID);
        }

        /// <summary>
        /// To Update Stake holder
        /// </summary>
        /// <param name="objStakeholder"></param>
        public void UpdateStakeholder(PAP_StakeholderBO objStakeholder)
        {
            (new PAP_StakeholderDAL()).UpdateStakeholder(objStakeholder);
        }
    }
}