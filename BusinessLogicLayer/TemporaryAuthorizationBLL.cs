using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class TemporaryAuthorizationBLL
    {
        /// <summary>
        /// To Add Temporary Authorization
        /// </summary>
        /// <param name="objAuth"></param>
        /// <returns></returns>
        public string AddTemporaryAuthorization(TemporaryAuthorizationBO objAuth)
        {
            return (new TemporaryAuthorizationDAL()).AddTemporaryAuthorization(objAuth);
        }

        /// <summary>
        /// To Get Temp Authorizations By User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public TemporaryAuthorizationList GetTempAuthorizationsByUser(int userID, int ProjectID)
        {
            return (new TemporaryAuthorizationDAL()).GetTempAuthorizationsByUser(userID, ProjectID);
        }

        /// <summary>
        /// To Get Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <returns></returns>
        public TemporaryAuthorizationBO GetTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID)
        {
            return (new TemporaryAuthorizationDAL()).GetTempAuthorizationsByID(APPROVALTEMPAUTHORISERID);
        }

        /// <summary>
        /// To Delete Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        public void DeleteTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID)
        {
            (new TemporaryAuthorizationDAL()).DeleteTempAuthorizationsByID(APPROVALTEMPAUTHORISERID);
        }

        /// <summary>
        /// To Obsolete Temp Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID, string IsDeleted)
        {
            return (new TemporaryAuthorizationDAL()).ObsoleteTempAuthorizationsByID(APPROVALTEMPAUTHORISERID, IsDeleted);
        }
    }
}
