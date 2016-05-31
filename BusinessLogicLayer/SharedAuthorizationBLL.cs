using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class SharedAuthorizationBLL
    {
        /// <summary>
        /// To Add Shared Authorization
        /// </summary>
        /// <param name="objAuth"></param>
        /// <returns></returns>
        public string AddSharedAuthorization(SharedAuthorizationBO pSharedAuth)
        {
            return (new SharedAuthorizationDAL()).AddSharedAuthorization(pSharedAuth);
        }

        /// <summary>
        /// To Get Temp Authorizations By User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public SharedAuthorizationList GetSharedAuthorizationsByUser(int AuthorisedId, int ProjectID)
        {
            return (new SharedAuthorizationDAL()).GetSharedAuthorizationsByUser(AuthorisedId, ProjectID);
        }

        /// <summary>
        /// To Get Shared Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <returns></returns>
        public SharedAuthorizationBO GetSharedAuthorizationsByID(int WorkFlowSharedId)
        {
            return (new SharedAuthorizationDAL()).GetSharedAuthorizationsByID(WorkFlowSharedId);
        }

        /// <summary>
        /// To Delete Shared Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        public void DeleteSharedAuthorizationsByID(int WorkFlowSharedId)
        {
            (new SharedAuthorizationDAL()).DeleteSharedAuthorizationsByID(WorkFlowSharedId);
        }

        /// <summary>
        /// To Obsolete Shared Authorizations By ID
        /// </summary>
        /// <param name="APPROVALTEMPAUTHORISERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteTempAuthorizationsByID(int APPROVALTEMPAUTHORISERID, string IsDeleted)
        {
            return (new SharedAuthorizationDAL()).ObsoleteTempAuthorizationsByID(APPROVALTEMPAUTHORISERID, IsDeleted);
        }
    }
}
