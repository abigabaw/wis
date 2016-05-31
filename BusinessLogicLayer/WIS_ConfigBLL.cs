using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class WIS_ConfigBLL
    {
        /// <summary>
        /// To Get Round Off Limit
        /// </summary>
        /// <returns></returns>
        public string getRoundOffLimit()
        {
            return (new WIS_ConfigDAL()).getRoundOffLimit("ROUND_OFF_LIMIT");
        }

        /// <summary>
        /// To Get Configuration
        /// </summary>
        /// <param name="SearchValue"></param>
        /// <returns></returns>
        public WIS_ConfigBO getConfiguration(string SearchValue)
        {
            return (new WIS_ConfigDAL()).getConfiguration(SearchValue);
        }

        /// <summary>
        /// To Get Serial Number
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        public WIS_ConfigBO GetSerialNumber(string configItem)
        {
            return (new WIS_ConfigDAL()).GetSerialNumber(configItem);
        }

        /// <summary>
        /// To Get Config SMS sending status
        /// </summary>
        /// <returns></returns>
        public WIS_ConfigBO GetConfigSMSsending()
        {
            return (new WIS_ConfigDAL()).GetConfigSMSsending();
        }

        /// <summary>
        /// To Get Build Version
        /// </summary>
        /// <returns></returns>
         public WIS_ConfigBO getBuildVersion()
        {
            return (new WIS_ConfigDAL()).getBuildVersion();
        }
    }
}