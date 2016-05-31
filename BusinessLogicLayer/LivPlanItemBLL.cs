using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LivPlanItemBLL
    {
        /// <summary>
        /// To GET ALL LivPlanItem
        /// </summary>
        /// <returns></returns>
        public LivPlanItemList GETALLLivPlanItem()
        {
            LivPlanItemDAL LivPlanItemDALObj = new LivPlanItemDAL();
            return LivPlanItemDALObj.GETALLLivPlanItem();
        }

        // serach the data from the Database Mst_LivPlanItem
        public LivPlanItemList GetLivPlanItem()
        {
            LivPlanItemDAL LivPlanItemDALObj = new LivPlanItemDAL();
            return LivPlanItemDALObj.GetLivPlanItem();
        }
        //Insert the data into Database
        public string Insert(LivPlanItemBO objLivPlanItem)
        {
            LivPlanItemDAL LivPlanItemDAl = new LivPlanItemDAL(); //Data pass -to Database Layer

            try
            {
                return LivPlanItemDAl.Insert(objLivPlanItem);
            }
            catch
            {
                throw;
            }
            finally
            {
                LivPlanItemDAl = null;
            }
        }
        //Search the Singal Data by passing ID
        public LivPlanItemBO GetLivPlanItemById(int LivPlanItemID)
        {
            LivPlanItemDAL LivPlanItemDALObj = new LivPlanItemDAL();
            return LivPlanItemDALObj.GetLivPlanItemById(LivPlanItemID);
        }
        //Edit the data
        public string EDITLivPlanItem(LivPlanItemBO objLivPlanItem)
        {
            LivPlanItemDAL LivPlanItemDAl = new LivPlanItemDAL(); //Data pass -to Database Layer

            try
            {
                return LivPlanItemDAl.EDITLivPlanItem(objLivPlanItem);
            }
            catch
            {
                throw;
            }
            finally
            {
                LivPlanItemDAl = null;
            }
        }
        //Delete the data
        public string DeleteLivPlanItem(int LivPlanItemID)
        {
            LivPlanItemDAL LivPlanItemDALObj = new LivPlanItemDAL();
            return LivPlanItemDALObj.DeleteLivPlanItem(LivPlanItemID);
        }

        public string ObsoleteLivPlanItem(int LivPlanItemID, string Isdeleted)
        {
            LivPlanItemDAL LivPlanItemDALObj = new LivPlanItemDAL();
            return LivPlanItemDALObj.ObsoleteLivPlanItem(LivPlanItemID, Isdeleted);
        }
    }
}
