
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class PublicConsultationRptBLL
    {
        public DistrictList LoadDistrictData()
        {
            PublicConsultationRptDAL objPublicConsultationRptDAL = new PublicConsultationRptDAL();
            return objPublicConsultationRptDAL.LoadDistrictData();
        }

        public CountyList LoadCountyData(string pDisrtictId)
        {
            PublicConsultationRptDAL objPublicConsultationRptDAL = new PublicConsultationRptDAL();
            return objPublicConsultationRptDAL.LoadCountyData(pDisrtictId);
        }

        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            PublicConsultationRptDAL objPublicConsultationRptDAL = new PublicConsultationRptDAL();
            return objPublicConsultationRptDAL.LoadSubCountyData(pCountyId);
        }

        public VillageList LoadVillageData(string pSubcountyId)
        {
            PublicConsultationRptDAL objPublicConsultationRptDAL = new PublicConsultationRptDAL();
            return objPublicConsultationRptDAL.LoadVillageData(pSubcountyId);
        }

        public ParishList LoadParishData(string pSubcountyId)
        {
            PublicConsultationRptDAL objPublicConsultationRptDAL = new PublicConsultationRptDAL();
            return objPublicConsultationRptDAL.LoadParishData(pSubcountyId);
        }
    }
}
