using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class TribeClanBLL
    {
        /// <summary>
        /// To Load Tribe Clan Data
        /// </summary>
        /// <param name="pTribeId"></param>
        /// <returns></returns>
        public TribeClanList LoadTribeClanData(string pTribeId)
        {
            TribeClanDAL objMasterDAL = new TribeClanDAL();
            return objMasterDAL.LoadTribeClanData(pTribeId);
        }

    }
}