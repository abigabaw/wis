using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class SocioEconomyBLL
    {
        #region "Welfare Indicator"
        /// <summary>
        /// To Get General Welfare Masters
        /// </summary>
        /// <returns></returns>
        public GeneralWelfareMasterList GetGeneralWelfareMasters()
        {
            return (new SocioEconomyDAL()).GetGeneralWelfareMasters();
        }

        /// <summary>
        /// To Update PAP Welfare
        /// </summary>
        /// <param name="WelfareList"></param>
        public void UpdatePAPWelfare(GeneralWelfareList WelfareList)
        {
            (new SocioEconomyDAL()).UpdatePAPWelfare(WelfareList);
        }

        /// <summary>
        /// To Get Welfare Voluntary
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public GeneralWelfareList GetGeneralWelfares(int householdID)
        {
            return (new SocioEconomyDAL()).GetGeneralWelfares(householdID);
        }

        /// <summary>
        /// To Update PAP Welfare Voluntary
        /// </summary>
        /// <param name="objVoluntary"></param>
        public void UpdatePAPWelfareVoluntary(WelfareVoluntaryBO objVoluntary)
        {
            (new SocioEconomyDAL()).UpdatePAPWelfareVoluntary(objVoluntary);
        }

        /// <summary>
        /// To Get Welfare Voluntary
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public WelfareVoluntaryBO GetWelfareVoluntary(int householdID)
        {
            return (new SocioEconomyDAL()).GetWelfareVoluntary(householdID);
        }

        #endregion
    }
}