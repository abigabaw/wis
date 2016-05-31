using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class WallTypeBLL
    {
        #region Declaration Scetion
        WallTypeDAL objWallTypeDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Wall Type
        /// </summary>
        /// <returns></returns>
        public WallTypeList GetAllWallType()
        {
            objWallTypeDAL = new WallTypeDAL();
            return objWallTypeDAL.GetAllWallType();
        }

        /// <summary>
        /// To Get Wall Type
        /// </summary>
        /// <returns></returns>
        public WallTypeList GetWallType()
        {
            objWallTypeDAL = new WallTypeDAL();
            return objWallTypeDAL.GetWallType();
        }

        /// <summary>
        /// To Get Wall Type By Id
        /// </summary>
        /// <param name="WallTypeID"></param>
        /// <returns></returns>
        public WallTypeBO GetWallTypeById(int WallTypeID)
        {
            objWallTypeDAL = new WallTypeDAL();
            return objWallTypeDAL.GetWallTypeById(WallTypeID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string AddWallType(WallTypeBO oWallType)
        {
            objWallTypeDAL = new WallTypeDAL();

            return objWallTypeDAL.SaveWallType(oWallType);
        }

        /// <summary>
        /// To Update Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string UpdateWallType(WallTypeBO oWallType)
        {
            objWallTypeDAL = new WallTypeDAL();

            return objWallTypeDAL.UpdateWallType(oWallType);
        }

        /// <summary>
        /// To Delete Wall Type
        /// </summary>
        /// <param name="oWallType"></param>
        /// <returns></returns>
        public string DeleteWallType(WallTypeBO oWallType)
        {
            objWallTypeDAL = new WallTypeDAL();
            return objWallTypeDAL.DeleteWallType(oWallType);
        }

        /// <summary>
        /// To Obsolete Wall Type
        /// </summary>
        /// <param name="WallTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteWallType(int WallTypeID, string IsDeleted)
        {
            objWallTypeDAL = new WallTypeDAL();
            return objWallTypeDAL.ObsoleteFloorTypeDAL(WallTypeID,IsDeleted);
        }
        #endregion
    }
}