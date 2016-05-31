using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_GroupOwnershipBLL
    {
        /// <summary>
        /// To Update Group Ownership Details
        /// </summary>
        /// <param name="objGroupOwnership"></param>
        /// <returns></returns>
        public string UpdateGroupOwnershipDetails(PAP_GroupOwnershipBO objGroupOwnership)
        {
            return (new PAP_GroupOwnershipDAL()).UpdateGroupOwnershipDetails(objGroupOwnership);
        }

        /// <summary>
        /// To Insert and Update Group Ownership
        /// </summary>
        /// <param name="objGroupOwnership"></param>
        public void InsertandUpdateGroupOwnership(PAP_GroupOwnershipBO objGroupOwnership)
        {
            (new PAP_GroupOwnershipDAL()).InsertandUpdateGroupOwnership(objGroupOwnership);
        }

        /// <summary>
        /// To Delete Group Ownership By GMID
        /// </summary>
        /// <param name="GMID"></param>
        public void DeleteGroupOwnershipByGMID(int GMID)
        {
            (new PAP_GroupOwnershipDAL()).DeleteGroupOwnershipByGMID(GMID);
        }

        /// <summary>
        /// To Get Group Ownership By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PAP_GroupOwnershipList GetGroupOwnershipByHHID(int HHID)
        {
            PAP_GroupOwnershipDAL objGroupOwnershipDAL = new PAP_GroupOwnershipDAL();
            return objGroupOwnershipDAL.GetGroupOwnershipByHHID(HHID);
        }
    }
}
