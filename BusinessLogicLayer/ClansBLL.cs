using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class ClansBLL
    {
        /// <summary>
        /// To Insert Into Clans Master details
        /// </summary>
        /// <param name="ClansBOObj"></param>
        /// <returns></returns>
        public int InsertIntoClansMaster(ClansBO ClansBOObj)
        {

            ClansDAL ClansDALObj = new ClansDAL();
            try
            {
                return ClansDALObj.InsertIntoClansMaster(ClansBOObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ClansDALObj = null;
            }
        }
        /// <summary>
        /// To Fetch ALL Clans List from database
        /// </summary>
        /// <param name="tribeID"></param>
        /// <returns></returns>
        public ClansList FetchALLClansList(int tribeID)
        {
            ClansDAL ClansDALObj = new ClansDAL();
            return ClansDALObj.FetchALLClansList(tribeID);
        }
        /// <summary>
        /// To Fetch  Clans List from database
        /// </summary>
        /// <param name="tribeID"></param>
        /// <returns></returns>
        public ClansList FetchClansList(int tribeID)
        {
            ClansDAL ClansDALObj = new ClansDAL();
            return ClansDALObj.FetchClansList(tribeID);
        }
        /// <summary>
        /// To Get Clans By Id from database
        /// </summary>
        /// <param name="ClansID"></param>
        /// <returns></returns>
        public ClansBO GetClansById(int ClansID)
        {
            ClansDAL ClansDALObj = new ClansDAL();
            return ClansDALObj.GetClansById(ClansID);
        }
        /// <summary>
        /// To delete clan details from database
        /// </summary>
        /// <param name="ClansID"></param>
        /// <returns></returns>
        public string DeleteClansDetails(int ClansID)
        {
            ClansDAL ClansDALObj = new ClansDAL();
            return ClansDALObj.DeleteClansDetails(ClansID);
        }
        /// <summary>
        /// To update clans details to database
        /// </summary>
        /// <param name="ClansBOObj"></param>
        /// <returns></returns>
        public int EDITClans(ClansBO ClansBOObj)
        {
            ClansDAL ClansDALObj = new ClansDAL();

            try
            {
                return ClansDALObj.EDITClans(ClansBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                ClansDALObj = null;
            }
        }
        /// <summary>
        /// To make clans details obsolete
        /// </summary>
        /// <param name="ClansID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsoleteclan(int ClansID, string IsDeleted)
        {
            ClansDAL ClansDALObj = new ClansDAL();
            return ClansDALObj.Obsoleteclan(ClansID, IsDeleted);
        }
    }
}