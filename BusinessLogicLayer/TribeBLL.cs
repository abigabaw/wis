using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class TribeBLL
    {
        /// <summary>
        /// To Insert Into Tribe Master
        /// </summary>
        /// <param name="TribeBOObj"></param>
        /// <returns></returns>
        public string InsertIntoTribeMaster(TribeBO TribeBOObj)
        {

            TribeDAL TribeDALObj = new TribeDAL();
            try
            {
                return TribeDALObj.InsertIntoTribeMaster(TribeBOObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TribeDALObj = null;
            }
        }

        /// <summary>
        /// To Fetch ALL Tribe List
        /// </summary>
        /// <returns></returns>
        public TribeList FetchALLTribeList()
        {
            TribeDAL TribeDALObj = new TribeDAL();
            return TribeDALObj.FetchALLTribeList();
        }

        /// <summary>
        /// To Fetch Tribe List
        /// </summary>
        /// <returns></returns>
        public TribeList FetchTribeList()
        {
            TribeDAL TribeDALObj = new TribeDAL();
            return TribeDALObj.FetchTribeList();
        }

        /// <summary>
        /// To Get Tribe By Id
        /// </summary>
        /// <param name="TribeID"></param>
        /// <returns></returns>
        public TribeBO GetTribeById(int TribeID)
        {
            TribeDAL TribeDALObj = new TribeDAL();
            return TribeDALObj.GetTribeById(TribeID);
        }

        /// <summary>
        /// To Delete Tribe By Id
        /// </summary>
        /// <param name="TribeID"></param>
        /// <returns></returns>
        public string DeleteTribeById(int TribeID)
        {
            TribeDAL TribeDALObj = new TribeDAL();
            return TribeDALObj.DeleteTribeById(TribeID);
        }

        /// <summary>
        /// To EDIT Tribe
        /// </summary>
        /// <param name="TribeBOObj"></param>
        /// <returns></returns>
        public string EDITTribe(TribeBO TribeBOObj)
        {
            TribeDAL TribeDALObj = new TribeDAL();

            try
            {
                return TribeDALObj.EDITTribe(TribeBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                TribeDALObj = null;
            }
        }

        /// <summary>
        /// To Obsolete tribe
        /// </summary>
        /// <param name="TRIBEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoletetribe(int TRIBEID, string IsDeleted)
        {
            TribeDAL TribeDALObj = new TribeDAL();
            return TribeDALObj.Obsoletetribe(TRIBEID, IsDeleted);
        }
    }
}