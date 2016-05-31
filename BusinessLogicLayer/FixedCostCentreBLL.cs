using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class FixedCostCentreBLL
    {
        /// <summary>
        /// To Get Fixed Cost Centres
        /// </summary>
        /// <param name="FixedCostCentreName"></param>
        /// <returns></returns>
        public FixedCostCentreList GetFixedCostCentres(string FixedCostCentreName)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.GetFixedCostCentre(FixedCostCentreName);
        }

        /// <summary>
        /// To Get All Fixed Cost Centres
        /// </summary>
        /// <param name="FixedCostCentreName"></param>
        /// <returns></returns>
        public FixedCostCentreList GetAllFixedCostCentres(string FixedCostCentreName)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.GetAllFixedCostCentre(FixedCostCentreName);
        }

        /// <summary>
        /// To Add Fixed Cost Centre
        /// </summary>
        /// <param name="objFixedCostCentre"></param>
        /// <returns></returns>
        public string AddFixedCostCentre(FixedCostCentreBO objFixedCostCentre)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.AddFixedCostCentre(objFixedCostCentre);
        }

        /// <summary>
        /// To Delete Fixed Cost Centre
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string DeleteFixedCostCentre(int roleID)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.DeleteFixedCostCentre(roleID);
        }

        /// <summary>
        /// To Obsolete Fixed Cost Centre
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFixedCostCentre(int roleID, string IsDeleted)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.ObsoleteFixedCostCentre(roleID, IsDeleted);
        }

        /// <summary>
        /// To Update Fixed Cost Centre
        /// </summary>
        /// <param name="objFixedCostCentre"></param>
        /// <returns></returns>
        public string UpdateFixedCostCentre(FixedCostCentreBO objFixedCostCentre)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.UpdateFixedCostCentre(objFixedCostCentre);
        }

        /// <summary>
        /// To Get Fixed Cost Centre By Fixed CostCentreI D
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public FixedCostCentreBO GetFixedCostCentreByFixedCostCentreID(int roleID)
        {
            FixedCostCentreDAL objFixedCostCentreDAL = new FixedCostCentreDAL();
            return objFixedCostCentreDAL.GetFixedCostCentreByFixedCostCentreID(roleID);
        }    
    }
}
