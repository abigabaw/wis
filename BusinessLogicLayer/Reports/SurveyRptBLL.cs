using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class SurveyRptBLL
    {
        public DistrictList LoadDistrictData()
        {
            SurveyRptDAL objSurveyRptDAL = new SurveyRptDAL();
            return objSurveyRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            SurveyRptDAL objSurveyRptDAL = new SurveyRptDAL();
            return objSurveyRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            SurveyRptDAL objSurveyRptDAL = new SurveyRptDAL();
            return objSurveyRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            SurveyRptDAL objSurveyRptDAL = new SurveyRptDAL();
            return objSurveyRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            SurveyRptDAL objSurveyRptDAL = new SurveyRptDAL();
            return objSurveyRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
