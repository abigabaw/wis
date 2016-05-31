using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class FloorTypeBLL
    {
        #region Declaration Scetion
        FloorTypeDAL objFloorTypeDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Floor Type
        /// </summary>
        /// <returns></returns>
        public FloorTypeList GetAllFloorType()//(BO.FloorType oFloorType)
        {
            objFloorTypeDAL = new FloorTypeDAL();
            return objFloorTypeDAL.GetAllFloorType();// (oFloorType);
        }

        /// <summary>
        /// To Get Floor Type
        /// </summary>
        /// <returns></returns>
        public FloorTypeList GetFloorType()//(BO.FloorType oFloorType)
        {
            objFloorTypeDAL = new FloorTypeDAL();
            return objFloorTypeDAL.GetFloorType();// (oFloorType);
        }

        /// <summary>
        /// To Get Floor Type By Id
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <returns></returns>
        public FloorTypeBO GetFloorTypeById(int FloorTypeID)
        {
            objFloorTypeDAL = new FloorTypeDAL();
            return objFloorTypeDAL.GetFloorTypeById(FloorTypeID);
        }
        #endregion


        #region Add, Update & Delete Record(s)
        /// <summary>
        ///  To Add Floor Type
        /// </summary>
        /// <param name="oFloorType"></param>
        /// <returns></returns>
        public string AddFloorType(FloorTypeBO oFloorType)
        {
            objFloorTypeDAL = new FloorTypeDAL();

            return objFloorTypeDAL.SaveFloorType(oFloorType);
        }

        /// <summary>
        /// To Update Floor Type
        /// </summary>
        /// <param name="oFloorType"></param>
        /// <returns></returns>
        public string UpdateFloorType(FloorTypeBO oFloorType)
        {
            objFloorTypeDAL = new FloorTypeDAL();

            return objFloorTypeDAL.UpdateFloorType(oFloorType);
        }

        /// <summary>
        /// To Delete Floor Type
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <param name="UserID_"></param>
        /// <returns></returns>
        public string DeleteFloorType(int FloorTypeID,int UserID_)
        {
            objFloorTypeDAL = new FloorTypeDAL();
            return objFloorTypeDAL.DeleteFloorType(FloorTypeID,UserID_);
        }

        /// <summary>
        /// To Obsolete Floor Type
        /// </summary>
        /// <param name="FloorTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFloorType(int FloorTypeID, string IsDeleted)
        {
            objFloorTypeDAL = new FloorTypeDAL();
            return objFloorTypeDAL.ObsoleteFloorTypeDAL(FloorTypeID, IsDeleted);
        }
        #endregion
    }
}