using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_HealthBLL
    {
        /// <summary>
        /// To Add Disability
        /// </summary>
        /// <param name="objDisability"></param>
        /// <returns></returns>
        public string AddDisability(PAP_DisabilityBO objDisability)
        {
            return (new PAP_HealthDAL()).AddDisability(objDisability);
        }

        /// <summary>
        /// To Update Disability
        /// </summary>
        /// <param name="objDisability"></param>
        /// <returns></returns>
        public string UpdateDisability(PAP_DisabilityBO objDisability)
        {
            return (new PAP_HealthDAL()).UpdateDisability(objDisability);
        }

        /// <summary>
        /// To Get Disabilities
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_DisabilityList GetDisabilities(int householdID)
        {
            return (new PAP_HealthDAL()).GetDisabilities(householdID);
        }

        /// <summary>
        /// To Get Disability By ID
        /// </summary>
        /// <param name="PAPDisabilityID"></param>
        /// <returns></returns>
        public PAP_DisabilityBO GetDisabilityByID(int PAPDisabilityID)
        {
            return (new PAP_HealthDAL()).GetDisabilityByID(PAPDisabilityID);
        }

        /// <summary>
        /// To Obsolete Disability
        /// </summary>
        /// <param name="disabilityID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteDisability(int disabilityID, string IsDeleted, int updatedBy)
        {
            return (new PAP_HealthDAL()).ObsoleteDisability(disabilityID, IsDeleted, updatedBy);
        }

        /// <summary>
        /// To Delete Disability
        /// </summary>
        /// <param name="disabilityID"></param>
        public void DeleteDisability(int disabilityID)
        {
            (new PAP_HealthDAL()).DeleteDisability(disabilityID);
        }

        /// <summary>
        /// To Get Health Info By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_HealthBO GetHealthInfoByID(int householdID)
        {
            return (new PAP_HealthDAL()).GetHealthInfoByID(householdID);
        }

        /// <summary>
        /// To Update Health Info
        /// </summary>
        /// <param name="objPAPHealth"></param>
        public void UpdateHealthInfo(PAP_HealthBO objPAPHealth)
        {
            (new PAP_HealthDAL()).UpdateHealthInfo(objPAPHealth);
        }
    }
}