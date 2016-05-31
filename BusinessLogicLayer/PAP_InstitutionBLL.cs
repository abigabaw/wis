using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;
using WIS_BusinessObjects.Collections;

namespace WIS_BusinessLogic
{
    public class PAP_InstitutionBLL
    {
        /// <summary>
        /// To Update Institution Details
        /// </summary>
        /// <param name="objInstitution"></param>
        /// <returns></returns>
        public string UpdateInstitutionDetails(PAP_InstitutionBO objInstitution)
        {
            return (new PAP_InstitutionDAL()).UpdateInstitutionDetails(objInstitution);
        }

        /// <summary>
        /// To Get Institution Contact By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PAP_InstitutionList GetInstContactByHHID(int HHID)
        {
            PAP_InstitutionDAL objInstitutionDAL = new PAP_InstitutionDAL();
            return objInstitutionDAL.GetInstContactByHHID(HHID);
        }
    }
}
