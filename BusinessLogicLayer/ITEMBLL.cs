using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class ITEMBLL
    {
        #region Declaration Scetion
        ITEMDAL objITEMDAL;
        #endregion

        #region GetItem
        /// <summary>
        /// To Get Item
        /// </summary>
        /// <returns></returns>
        public ItemList GetItem()
        {
            objITEMDAL = new ITEMDAL();
            return objITEMDAL.GetItem();
        }
        #endregion

        #region GetSubItem
        /// <summary>
        /// To Get Sub Item
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public ItemList GetSubItem(int CatID)
        {
            objITEMDAL = new ITEMDAL();
            return objITEMDAL.GetSubItem(CatID);
        }
        #endregion

        /// <summary>
        /// To Add CDAP Budget Master
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string AddCDAPBudgetMaster(CDAPBudgetMasterBO objCDAPBudgetMasterBO)
        {
            return (new ITEMDAL()).AddCDAPBudgetMaster(objCDAPBudgetMasterBO);
        }

        /// <summary>
        /// To Update CDAP Budget Master
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string UpdateCDAPBudgetMaster(CDAPBudgetMasterBO objCDAPBudgetMasterBO)
        {
            return (new ITEMDAL()).UpdateCDAPBudgetMaster(objCDAPBudgetMasterBO);
        }

        /// <summary>
        /// To Delete CDAP Budget Master
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string DeleteCDAPBudgetMaster(int categoryID)
        {
            return (new ITEMDAL()).DeleteCDAPBudgetMaster(categoryID);
        }

        /// <summary>
        /// To Obsolete CDAP Budget Master
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteCDAPBudgetMaster(int categoryID, string isDeleted, int updatedBy)
        {
            return (new ITEMDAL()).ObsoleteCDAPBudgetMaster(categoryID, isDeleted, updatedBy);
        }


        /// <summary>
        /// To Get All CDAP Budget Items
        /// </summary>
        /// <returns></returns>
        public List<CDAPBudgetMasterBO> GetAllCDAPBudgetItems()
        {
            return (new ITEMDAL()).GetAllCDAPBudgetItems();
        }

        /// <summary>
        /// To Get CDAP Budget Item By ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public CDAPBudgetMasterBO GetCDAPBudgetItemByID(int categoryID)
        {
            return (new ITEMDAL()).GetCDAPBudgetItemByID(categoryID);
        }

        /// <summary>
        /// To Add CDAP Budget Sub Item
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string AddCDAPBudgetSubItem(CDAPBudgetDescrMasterBO objCDAPBudgetMasterBO)
        {
            return (new ITEMDAL()).AddCDAPBudgetSubItem(objCDAPBudgetMasterBO);
        }

        /// <summary>
        /// To Update CDAP Budget Sub Item
        /// </summary>
        /// <param name="objCDAPBudgetMasterBO"></param>
        /// <returns></returns>
        public string UpdateCDAPBudgetSubItem(CDAPBudgetDescrMasterBO objCDAPBudgetMasterBO)
        {
            return (new ITEMDAL()).UpdateCDAPBudgetSubItem(objCDAPBudgetMasterBO);
        }

        /// <summary>
        /// To Delete CDAP Budget Sub Item
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public string DeleteCDAPBudgetSubItem(int categoryID)
        {
            return (new ITEMDAL()).DeleteCDAPBudgetSubItem(categoryID);
        }

        /// <summary>
        /// To Obsolete CDAP Budget Sub Item
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteCDAPBudgetSubItem(int categoryID, string isDeleted, int updatedBy)
        {
            return (new ITEMDAL()).ObsoleteCDAPBudgetSubItem(categoryID, isDeleted, updatedBy);
        }

        /// <summary>
        /// To Get CDAP Budget Sub Item By ID
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public CDAPBudgetDescrMasterBO GetCDAPBudgetSubItemByID(int categoryID)
        {
            return (new ITEMDAL()).GetCDAPBudgetSubItemByID(categoryID);
        }

        /// <summary>
        /// To Get All CDAP Budget SubItems
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<CDAPBudgetDescrMasterBO> GetAllCDAPBudgetSubItems(int categoryID)
        {
            return (new ITEMDAL()).GetAllCDAPBudgetSubItems(categoryID);
        }
    }
}
