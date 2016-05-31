using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class LCTRptBLL
    {
        public DistrictList LoadDistrictData()
        {
            LCTRptDAL objLCTRptDAL = new LCTRptDAL();
            return objLCTRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            LCTRptDAL objLCTRptDAL = new LCTRptDAL();
            return objLCTRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            LCTRptDAL objLCTRptDAL = new LCTRptDAL();
            return objLCTRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            LCTRptDAL objLCTRptDAL = new LCTRptDAL();
            return objLCTRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            LCTRptDAL objLCTRptDAL = new LCTRptDAL();
            return objLCTRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
