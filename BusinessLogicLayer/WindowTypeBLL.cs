using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class WindowTypeBLL
    {
        #region Declaration Scetion
        WindowTypeDAL objWindowTypeDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Window Type()
        /// </summary>
        /// <returns></returns>
        public WindowTypeList GetAllWindowType()
        {
            objWindowTypeDAL = new WindowTypeDAL();
            return objWindowTypeDAL.GetAllWindowType();
        }

        /// <summary>
        /// To Get Window Type
        /// </summary>
        /// <returns></returns>
        public WindowTypeList GetWindowType()
        {
            objWindowTypeDAL = new WindowTypeDAL();
            return objWindowTypeDAL.GetWindowType();
        }

        /// <summary>
        /// To Get Window Type By Id
        /// </summary>
        /// <param name="WindowTypeID"></param>
        /// <returns></returns>
        public WindowTypeBO GetWindowTypeById(int WindowTypeID)
        {
            objWindowTypeDAL = new WindowTypeDAL();
            return objWindowTypeDAL.GetWindowTypeById(WindowTypeID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Save Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string AddWindowType(WindowTypeBO oWindowType)
        {
            objWindowTypeDAL = new WindowTypeDAL();

            return objWindowTypeDAL.SaveWindowType(oWindowType);
        }

        /// <summary>
        /// To Update Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string UpdateWindowType(WindowTypeBO oWindowType)
        {
            objWindowTypeDAL = new WindowTypeDAL();

            return objWindowTypeDAL.UpdateWindowType(oWindowType);
        }

        /// <summary>
        /// To Delete Window Type
        /// </summary>
        /// <param name="oWindowType"></param>
        /// <returns></returns>
        public string DeleteWindowType(WindowTypeBO oWindowType)
        {
            objWindowTypeDAL = new WindowTypeDAL();
            return objWindowTypeDAL.DeleteWindowType(oWindowType);
        }

        /// <summary>
        /// To Get Obsolete Floor Type DAL
        /// </summary>
        /// <param name="WindowTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteWindowType(int WindowTypeID, string IsDeleted)
        {
            objWindowTypeDAL = new WindowTypeDAL();
            return objWindowTypeDAL.ObsoleteFloorTypeDAL(WindowTypeID,IsDeleted);
        }
        #endregion
    }
}