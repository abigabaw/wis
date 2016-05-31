using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class MasterBLL
    {
        /// <summary>
        /// To Load District Data
        /// </summary>
        /// <returns></returns>
        public DistrictList LoadDistrictData()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadDistrictData();
        }

        /// <summary>
        /// To Load County Data
        /// </summary>
        /// <param name="pDisrtictId"></param>
        /// <returns></returns>
        public CountyList LoadCountyData(string pDisrtictId)
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadCountyData(pDisrtictId);
        }

        /// <summary>
        /// To Load Sub County Data
        /// </summary>
        /// <param name="pCountyId"></param>
        /// <returns></returns>
        public SubCountyList LoadSubCountyData(string pCountyId)
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadSubCountyData(pCountyId);
        }

        /// <summary>
        /// To Load Village Data
        /// </summary>
        /// <param name="pSubcountyId"></param>
        /// <returns></returns>
        public VillageList LoadVillageData(string pSubcountyId)
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadVillageData(pSubcountyId);
        }

        /// <summary>
        /// To Load Parish Data
        /// </summary>
        /// <param name="pSubcountyId"></param>
        /// <returns></returns>
        public ParishList LoadParishData(string pSubcountyId)
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadParishData(pSubcountyId);
        }

        /// <summary>
        /// To Load Religion Data
        /// </summary>
        /// <returns></returns>
        public ReligionList LoadReligionData()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadReligionData();
        }

        /// <summary>
        /// To Load Option Group Data
        /// </summary>
        /// <returns></returns>
        public OptionGroupList LoadOptionGroupData()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadOptionGroupData();
        }

        /// <summary>
        /// To Load Proprietor Data
        /// </summary>
        /// <returns></returns>
        public ProprietorList LoadProprietorData()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadProprieterData();
        }

        /// <summary>
        /// TO Load Tenure Land
        /// </summary>
        /// <returns></returns>
        public TenureLandList LoadTenureLand()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadTenureLand();
        }

        /// <summary>
        /// To Load Currency
        /// </summary>
        /// <returns></returns>
        public CurrencyList LoadCurrency()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadCurrency();
        }

        /// <summary>
        /// To Load Representation Data
        /// </summary>
        /// <returns></returns>
        public RepresentationList LoadRepresentationData()
        {
            MasterDAL objMasterDAL = new MasterDAL();
            return objMasterDAL.LoadRepresentationData();
        }
    }
}