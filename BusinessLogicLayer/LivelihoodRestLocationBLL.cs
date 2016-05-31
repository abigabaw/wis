using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LivelihoodRestLocationBLL
    {
        /// <summary>
        /// To Get New Location
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public LivelihoodRestLocationBO GetNewLocation(int HHID)
        {
            return (new LivelihoodRestLocationDAL()).GetNewLocation(HHID);
        }

        /// <summary>
        /// To Add New Location
        /// </summary>
        /// <param name="oNewLocationBO"></param>
        /// <returns></returns>
        public string AddNewLocation(LivelihoodRestLocationBO oNewLocationBO)
        {
            return (new LivelihoodRestLocationDAL()).AddNewLocation(oNewLocationBO);
        }
    }
}