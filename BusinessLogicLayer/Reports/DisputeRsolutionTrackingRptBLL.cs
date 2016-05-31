
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class DisputeRsolutionTrackingRptBLL
    {
       public DistrictList LoadDistrictData()
        {
            DisputeRsolutionTrackingRptDAL objDisputeRsolutionTrackingRptDAL = new DisputeRsolutionTrackingRptDAL();
            return objDisputeRsolutionTrackingRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            DisputeRsolutionTrackingRptDAL objDisputeRsolutionTrackingRptDAL = new DisputeRsolutionTrackingRptDAL();
            return objDisputeRsolutionTrackingRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            DisputeRsolutionTrackingRptDAL objDisputeRsolutionTrackingRptDAL = new DisputeRsolutionTrackingRptDAL();
            return objDisputeRsolutionTrackingRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            DisputeRsolutionTrackingRptDAL objDisputeRsolutionTrackingRptDAL = new DisputeRsolutionTrackingRptDAL();
            return objDisputeRsolutionTrackingRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            DisputeRsolutionTrackingRptDAL objDisputeRsolutionTrackingRptDAL = new DisputeRsolutionTrackingRptDAL();
            return objDisputeRsolutionTrackingRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
