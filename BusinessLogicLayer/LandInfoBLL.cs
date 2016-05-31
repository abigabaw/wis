using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LandInfoBLL
    {
        /// <summary>
        /// To Get Land Info
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PublicLandInfoBO GetLandInfo(int householdID)
        {
            LandInfoDAL objLandInfoDAL = new LandInfoDAL();
            return objLandInfoDAL.GetLandInfo(householdID);
        }

        /// <summary>
        /// To Add Land Info
        /// </summary>
        /// <param name="objLF"></param>
        public void AddLandInfo(PublicLandInfoBO objLF)
        {
            LandInfoDAL objLandInfoDAL = new LandInfoDAL();
            objLandInfoDAL.AddLandInfo(objLF);
        }

        /// <summary>
        /// To Update Land Info
        /// </summary>
        /// <param name="objLF"></param>
        public void UpdateLandInfo(PublicLandInfoBO objLF)
        {
            LandInfoDAL objLandInfoDAL = new LandInfoDAL();
            objLandInfoDAL.UpdateLandInfo(objLF);
        }    
    }
}