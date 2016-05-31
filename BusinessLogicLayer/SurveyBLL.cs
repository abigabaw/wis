using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class SurveyBLL
    {
        #region LandLivingON
        /// <summary>
        /// To Add Land Living On
        /// </summary>
        /// <param name="objSurvey"></param>
        /// <returns></returns>
        public string AddLandLivingOn(LandLivingOnBO objSurvey)
        {
            SurveyDAL objAddLandLivingDAL = new SurveyDAL();
            return objAddLandLivingDAL.AddLandLivingOn(objSurvey);
        }

        /// <summary>
        /// To Get Land Living On By HHID
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandLivingOnBO GetLandLivingOnByHHID(int HHID)
        {
            SurveyDAL objAddLandLivingDAL = new SurveyDAL();
            return objAddLandLivingDAL.GetLandLivingOnByHHID(HHID);
        }
        #endregion

        #region LandLivingOFF
        /// <summary>
        /// To Add Land Living OFF
        /// </summary>
        /// <param name="objSurveyLandLivingOff"></param>
        /// <returns></returns>
        public int AddLandLivingOFF(LandLivingOffBO objSurveyLandLivingOff)
        {
            SurveyDAL objAddLandLivingOFFDAL = new SurveyDAL();
            return objAddLandLivingOFFDAL.AddLandLivingOFF(objSurveyLandLivingOff);
        }

        /// <summary>
        /// To Get Living OFF
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LandLivingOffList GetLivingOFF(int HHID)
        {
            return (new SurveyDAL().GetLivingOFF(HHID));
        }

        /// <summary>
        /// To Get Living OFF By ID
        /// </summary>
        /// <param name="LivingOffID"></param>
        /// <returns></returns>
        public LandLivingOffBO GetLivingOFFByID(int LivingOffID)
        {
            return (new SurveyDAL().GetLivingOFFByID(LivingOffID));
        }

        /// <summary>
        /// To Delete Land Living Off
        /// </summary>
        /// <param name="LivingOffID"></param>
        public void DeleteLivingOFF(int LivingOffID)
        {
            SurveyDAL objAddLandLivingOFFDAL = new SurveyDAL();
            objAddLandLivingOFFDAL.DeleteLandLivingOff(LivingOffID);
        }  

        #endregion

        #region AffectedAcreageValuation
        /// <summary>
        /// To Add Affected Acreage Valuation
        /// </summary>
        /// <param name="objAffectedAcreageValuation"></param>
        /// <returns></returns>
        public string AddAffectedAcreageValuation(AffectedAcreageValuationBO objAffectedAcreageValuation)
        {
            SurveyDAL objAddAffectedAcreageValuationDAL = new SurveyDAL();
            return objAddAffectedAcreageValuationDAL.AddAffectedAcreageValuation(objAffectedAcreageValuation);
        }

        /// <summary>
        /// To Get Affected Acreage Valuation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public AffectedAcreageValuationBO GetAffectedAcreageValuation(int HHID)
        {
            SurveyDAL objGetAffectedAcreageValuation = new SurveyDAL();
            return objGetAffectedAcreageValuation.GetAffectedAcreageValuation(HHID);
        }
        #endregion
    }
}