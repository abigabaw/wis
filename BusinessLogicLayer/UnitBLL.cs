using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class UnitBLL
    {
        #region Declaration Scetion
        UnitDAL objUnitDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Unit
        /// </summary>
        /// <returns></returns>
        public UnitList GetAllUnit()
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.GetAllUnit();
        }

        /// <summary>
        /// To Get Unit
        /// </summary>
        /// <returns></returns>
        public UnitList GetUnit()
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.GetUnit();
        }

        /// <summary>
        /// To Get Unit By Id
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public UnitBO GetUnitById(int UnitID)
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.GetUnitById(UnitID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Save Unit
        /// </summary>
        /// <param name="oUnit"></param>
        /// <returns></returns>
        public string AddUnit(UnitBO oUnit)
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.SaveUnit(oUnit);
        }

        /// <summary>
        /// To Update Unit
        /// </summary>
        /// <param name="oUnit"></param>
        /// <returns></returns>
        public string UpdateUnit(UnitBO oUnit)
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.UpdateUnit(oUnit);
        }

        /// <summary>
        /// To Obsolete Unit
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteUnit(int UnitID, string IsDeleted)
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.ObsoleteUnit(UnitID, IsDeleted);
        }

        /// <summary>
        /// To Delete Unit
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public string DeleteUnit(int UnitID)
        {
            objUnitDAL = new UnitDAL();
            return objUnitDAL.DeleteUnit(UnitID);
        }
        #endregion
    }
}