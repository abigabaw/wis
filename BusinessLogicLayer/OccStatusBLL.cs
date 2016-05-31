using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class OccStatusBLL
    {
        /// <summary>
        /// To Load Status Data
        /// </summary>
        /// <returns></returns>
        public OccStatusList LoadStatusData()
        {
            
            OccStatusDAL objMasterDAL = new OccStatusDAL();
            return objMasterDAL.LoadStatusData();
        }
    }
}