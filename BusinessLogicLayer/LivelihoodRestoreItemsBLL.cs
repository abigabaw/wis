using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LivelihoodRestoreItemsBLL
    {
        #region GET Restoration Items 
        /// <summary>
        /// To Get Live Rest Items
        /// </summary>
        /// <returns></returns>
        public LivelihoodRestoreItemsList GetLiveRestItems()
        {
            return (new LivelihoodRestoreItemsDAL()).GetLiveRestItems();
        }

        /// <summary>
        /// To Get Live Rest Items All
        /// </summary>
        /// <returns></returns>
        public LivelihoodRestoreItemsList GetLiveRestItems_All()
        {
            return (new LivelihoodRestoreItemsDAL()).GetLiveRestItems_All();
        }

        /// <summary>
        /// To Get Live Rest Items By Id
        /// </summary>
        /// <param name="RestorationItemID"></param>
        /// <returns></returns>
        public LivelihoodRestoreItemsBO GetLiveRestItemsById(int RestorationItemID)
        {
            return (new LivelihoodRestoreItemsDAL()).GetLiveRestItemsById(RestorationItemID);
        }
        #endregion GET Restoration Items

        #region Modify Restoration Items
        /// <summary>
        /// To Add Live Rest Item
        /// </summary>
        /// <param name="oLiveRestItemsBO"></param>
        /// <returns></returns>
        public string AddLiveRestItem(LivelihoodRestoreItemsBO oLiveRestItemsBO)
        {
            return (new LivelihoodRestoreItemsDAL()).AddLiveRestItem(oLiveRestItemsBO);
        }

        /// <summary>
        /// To Update Live Rest Item
        /// </summary>
        /// <param name="oLiveRestItemsBO"></param>
        /// <returns></returns>
        public string UpdateLiveRestItem(LivelihoodRestoreItemsBO oLiveRestItemsBO)
        {
            return (new LivelihoodRestoreItemsDAL()).UpdateLiveRestItem(oLiveRestItemsBO);
        }

        /// <summary>
        /// To Delete Live Rest Item
        /// </summary>
        /// <param name="LiveRestItemID"></param>
        /// <returns></returns>
        public string DeleteLiveRestItem(int LiveRestItemID)
        {
            return (new LivelihoodRestoreItemsDAL()).DeleteLiveRestItem(LiveRestItemID);
        }

        /// <summary>
        /// To Obsolete Live Rest Item
        /// </summary>
        /// <param name="LiveRestItemID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLiveRestItem(int LiveRestItemID, string IsDeleted)
        {
            return (new LivelihoodRestoreItemsDAL()).ObsoleteLiveRestItem(LiveRestItemID, IsDeleted);
        }
        #endregion Modify Restoration Items
    }
}