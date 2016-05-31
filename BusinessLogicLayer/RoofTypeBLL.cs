using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class RoofTypeBLL
    {
        #region Declaration Scetion
        RoofTypeDAL objRoofTypeDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Roof Type
        /// </summary>
        /// <returns></returns>
        public RoofTypeList GetAllRoofType()
        {
            objRoofTypeDAL = new RoofTypeDAL();
            return objRoofTypeDAL.GetAllRoofType();
        }

        /// <summary>
        /// To Get Roof Type
        /// </summary>
        /// <returns></returns>
        public RoofTypeList GetRoofType()
        {
            objRoofTypeDAL = new RoofTypeDAL();
            return objRoofTypeDAL.GetRoofType();
        }

        /// <summary>
        /// To Get Roof Type By Id
        /// </summary>
        /// <param name="RoofTypeID"></param>
        /// <returns></returns>
        public RoofTypeBO GetRoofTypeById(int RoofTypeID)
        {
            objRoofTypeDAL = new RoofTypeDAL();
            return objRoofTypeDAL.GetRoofTypeById(RoofTypeID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Roof Type
        /// </summary>
        /// <param name="oRoofType"></param>
        /// <returns></returns>
        public string AddRoofType(RoofTypeBO oRoofType)
        {
            objRoofTypeDAL = new RoofTypeDAL();

            return objRoofTypeDAL.SaveRoofType(oRoofType);
        }

        /// <summary>
        /// To Update Roof Type
        /// </summary>
        /// <param name="oRoofType"></param>
        /// <returns></returns>
        public string UpdateRoofType(RoofTypeBO oRoofType)
        {
            objRoofTypeDAL = new RoofTypeDAL();

            return objRoofTypeDAL.UpdateRoofType(oRoofType);
        }

        /// <summary>
        /// To Delete Roof Type
        /// </summary>
        /// <param name="RoofTypeID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string DeleteRoofType(int RoofTypeID,int UserID)
        {
            objRoofTypeDAL = new RoofTypeDAL();
            return objRoofTypeDAL.DeleteRoofType(RoofTypeID,UserID);
        }

        /// <summary>
        /// To Obsolete Roof Type
        /// </summary>
        /// <param name="RoofTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRoofType(int RoofTypeID, string IsDeleted)
        {
            objRoofTypeDAL = new RoofTypeDAL();
            return objRoofTypeDAL.ObsoleteFloorTypeDAL(RoofTypeID, IsDeleted);
        }
        #endregion
    }
}