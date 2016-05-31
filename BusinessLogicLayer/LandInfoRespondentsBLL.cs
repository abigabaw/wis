using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LandInfoRespondentsBLL
    {
        /// <summary>
        /// To Land Info Respondents List
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandInfoRespondentsList GetLandInfoRespondents(int HHID)
        {
            LandInfoRespondentsDAL objGraveDAL = new LandInfoRespondentsDAL();
            return objGraveDAL.GetLandInfoRespondents(HHID);
        }

        /// <summary>
        /// To Add Land Info Respondents
        /// </summary>
        /// <param name="objLIR"></param>
        public void AddLandInfoRespondents(LandInfoRespondentsBO objLIR)
        {
            LandInfoRespondentsDAL objLIRDAL = new LandInfoRespondentsDAL();
            objLIRDAL.AddLandInfoRespondents(objLIR);
        }

        /// <summary>
        /// To Delete Land Info Respondents
        /// </summary>
        /// <param name="hID"></param>
        public void DeleteLandInfoRespondents(int hID)
        {
            LandInfoRespondentsDAL objLIRDAL = new LandInfoRespondentsDAL();
            objLIRDAL.DeleteLandInfoRespondents(hID);
        }

        /// <summary>
        /// To Update Land Info Respondents
        /// </summary>
        /// <param name="objLIR"></param>
        public void UpdateLandInfoRespondents(LandInfoRespondentsBO objLIR)
        {
            LandInfoRespondentsDAL objLIRDAL = new LandInfoRespondentsDAL();
            objLIRDAL.UpdateLandInfoRespondents(objLIR);
        }

        /// <summary>
        /// To Get Land Info Respondents By ID
        /// </summary>
        /// <param name="hID"></param>
        /// <returns></returns>
        public LandInfoRespondentsBO GetLandInfoRespondentsByID(int hID)
        {
            LandInfoRespondentsDAL objLIRDAL = new LandInfoRespondentsDAL();
            return objLIRDAL.GetLandInfoRespondentsByID(hID);
        }
    }
}