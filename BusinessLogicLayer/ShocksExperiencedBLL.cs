using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ShocksExperiencedBLL
    {
        #region Declaration Scetion
        ShocksExperiencedDAL objShocksExperiencedDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get ALL Shocks Experienced
        /// </summary>
        /// <returns></returns>
        public ShocksExperiencedList GetALLShocksExperienced()//(ShocksExperienced oShocksExperienced)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();
            return objShocksExperiencedDAL.GetALLShocksExperienced();// (oShocksExperienced);
        }

        /// <summary>
        /// To Get Shocks Experienced
        /// </summary>
        /// <returns></returns>
        public ShocksExperiencedList GetShocksExperienced()//(ShocksExperienced oShocksExperienced)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();
            return objShocksExperiencedDAL.GetShocksExperienced();// (oShocksExperienced);
        }

        /// <summary>
        /// To Get Shocks Experienced By Id
        /// </summary>
        /// <param name="ShockID"></param>
        /// <returns></returns>
        public ShocksExperiencedBO GetShocksExperiencedById(int ShockID)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();
            return objShocksExperiencedDAL.GetShocksExperiencedById(ShockID);
        }
        #endregion


        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Shocks Experienced
        /// </summary>
        /// <param name="oShocksExperienced"></param>
        /// <returns></returns>
        public string AddShocksExperienced(ShocksExperiencedBO oShocksExperienced)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();

            return objShocksExperiencedDAL.SaveShocksExperienced(oShocksExperienced);
        }

        /// <summary>
        /// To Update Shocks Experienced
        /// </summary>
        /// <param name="oShocksExperienced"></param>
        /// <returns></returns>
        public string UpdateShocksExperienced(ShocksExperiencedBO oShocksExperienced)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();

            return objShocksExperiencedDAL.UpdateShocksExperienced(oShocksExperienced);
        }

        /// <summary>
        /// To Delete Shocks Experienced
        /// </summary>
        /// <param name="ShockID"></param>
        /// <returns></returns>
        public string DeleteShocksExperienced(int ShockID)
        {
            objShocksExperiencedDAL = new ShocksExperiencedDAL();
            return objShocksExperiencedDAL.DeleteShocksExperienced(ShockID);
        }
        #endregion

        /// <summary>
        /// To Obsolete shock experienced id
        /// </summary>
        /// <param name="ShockID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoleteshockexperiencedid(int ShockID, string IsDeleted)
        {
            ShocksExperiencedDAL objShocksExperiencedDAL = new ShocksExperiencedDAL();
            return objShocksExperiencedDAL.Obsoleteshockexperiencedid(ShockID, IsDeleted);
        }
    }
}