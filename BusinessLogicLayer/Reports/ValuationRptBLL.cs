
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
   public class ValuationRptBLL
    {
        public DistrictList LoadDistrictData()
        {
            ValuationRptDAL objValuationRptDAL = new ValuationRptDAL();
            return objValuationRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            ValuationRptDAL objValuationRptDAL = new ValuationRptDAL();
            return objValuationRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            ValuationRptDAL objValuationRptDAL = new ValuationRptDAL();
            return objValuationRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            ValuationRptDAL objValuationRptDAL = new ValuationRptDAL();
            return objValuationRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            ValuationRptDAL objValuationRptDAL = new ValuationRptDAL();
            return objValuationRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
