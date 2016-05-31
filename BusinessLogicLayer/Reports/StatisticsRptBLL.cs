using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class StatisticsRptBLL
    {
        public DistrictList LoadDistrictData()
        {
            StatisticsRptDAL objStatisticsRptDAL = new StatisticsRptDAL();
            return objStatisticsRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            StatisticsRptDAL objStatisticsRptDAL = new StatisticsRptDAL();
            return objStatisticsRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            StatisticsRptDAL objStatisticsRptDAL = new StatisticsRptDAL();
            return objStatisticsRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            StatisticsRptDAL objStatisticsRptDAL = new StatisticsRptDAL();
            return objStatisticsRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            StatisticsRptDAL objStatisticsRptDAL = new StatisticsRptDAL();
            return objStatisticsRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
