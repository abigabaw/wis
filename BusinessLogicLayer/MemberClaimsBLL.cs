using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_DataAccess;
using WIS_BusinessObjects;
namespace WIS_BusinessLogic
{
    public class MemberClaimsBLL
    {
        /// <summary>
        /// To Add Member
        /// </summary>
        /// <param name="MCBO"></param>
        /// <returns></returns>
        public string AddMember(MemberClaimsBO MCBO)
        {
             MemberClaimsDAL objMCDAL = new MemberClaimsDAL();
             return objMCDAL.AddMember(MCBO);
        }

        /// <summary>
        /// To get Member Claim
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public MemberClaimsBO getMemberClaim(int HHID)
        {
            MemberClaimsDAL objMCDAL = new MemberClaimsDAL();
            return objMCDAL.getMemberClaim(HHID);
        }
    }
}