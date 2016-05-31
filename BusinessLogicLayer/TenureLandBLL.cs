using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class TenureLandBLL
    {
        /// <summary>
        /// To Get Tenure Land
        /// </summary>
        /// <param name="TenureLandName"></param>
        /// <returns></returns>
        public TenureLandList GetTenureLand(string TenureLandName)
        {
            TenureLandDAL objTenureLandDAL = new TenureLandDAL();
            return objTenureLandDAL.GetTenureLand(TenureLandName);
        }

        /// <summary>
        /// To Add Tenure Land
        /// </summary>
        /// <param name="objTenureLand"></param>
        /// <returns></returns>
        public string AddTenureLand(TenureLandBO objTenureLand)
        {
            TenureLandDAL objTenureLandDAL = new TenureLandDAL();
            return objTenureLandDAL.AddTenureLand(objTenureLand);
        }

        /// <summary>
        /// To Delete Tenure Land
        /// </summary>
        /// <param name="TenureLandID"></param>
        /// <returns></returns>
        public string DeleteTenureLand(int TenureLandID)
        {
            TenureLandDAL objTenureLandDAL = new TenureLandDAL();
            return objTenureLandDAL.DeleteTenureLand(TenureLandID);
        }

        /// <summary>
        /// To Update Tenure Land
        /// </summary>
        /// <param name="objTenureLand"></param>
        /// <returns></returns>
        public string UpdateTenureLand(TenureLandBO objTenureLand)
        {
            TenureLandDAL objTenureLandDAL = new TenureLandDAL();
            return objTenureLandDAL.UpdateTenureLand(objTenureLand);
        }

        /// <summary>
        /// To Get Tenure Land By Tenure Land
        /// </summary>
        /// <param name="TenureLandID"></param>
        /// <returns></returns>
        public TenureLandBO GetTenureLandByTenureLand(int TenureLandID)
        {
            TenureLandDAL objTenureLandDAL = new TenureLandDAL();
            return objTenureLandDAL.GetTenureLandByTenureLand(TenureLandID);
        }

        /// <summary>
        /// To Obsolete Tenure Land
        /// </summary>
        /// <param name="TenureLandID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteTenureLand(int TenureLandID, string IsDeleted)
        {
            return (new TenureLandDAL()).ObsoleteTenureLand(TenureLandID, IsDeleted);
        }
    }
}
