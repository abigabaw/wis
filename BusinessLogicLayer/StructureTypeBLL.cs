using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class StructureTypeBLL
    {
        #region Declaration Scetion
        StructureTypeDAL objStructureTypeDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Type
        /// </summary>
        /// <returns></returns>
        public StructureTypeList GetAllStructureType()
        {
            objStructureTypeDAL = new StructureTypeDAL();
            return objStructureTypeDAL.GetAllStructureType();
        }

        /// <summary>
        /// To Get Structure Type
        /// </summary>
        /// <returns></returns>
        public StructureTypeList GetStructureType()
        {
            objStructureTypeDAL = new StructureTypeDAL();
            return objStructureTypeDAL.GetStructureType();
        }

        /// <summary>
        /// To Get Structure Type By Id
        /// </summary>
        /// <param name="StructureTypeID"></param>
        /// <returns></returns>
        public StructureTypeBO GetStructureTypeById(int StructureTypeID)
        {
            objStructureTypeDAL = new StructureTypeDAL();
            return objStructureTypeDAL.GetStructureTypeById(StructureTypeID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Structure Type
        /// </summary>
        /// <param name="oStructureType"></param>
        /// <returns></returns>
        public string AddStructureType(StructureTypeBO oStructureType)
        {
            objStructureTypeDAL = new StructureTypeDAL();

            return objStructureTypeDAL.SaveStructureType(oStructureType);
        }

        /// <summary>
        /// To Update Structure Type
        /// </summary>
        /// <param name="oStructureType"></param>
        /// <returns></returns>
        public string UpdateStructureType(StructureTypeBO oStructureType)
        {
            objStructureTypeDAL = new StructureTypeDAL();

            return objStructureTypeDAL.UpdateStructureType(oStructureType);
        }

        /// <summary>
        /// To Delete Structure Type
        /// </summary>
        /// <param name="StructureTypeID"></param>
        /// <returns></returns>
        public string DeleteStructureType(int StructureTypeID)
        {
            objStructureTypeDAL = new StructureTypeDAL();
            return objStructureTypeDAL.DeleteStructureType(StructureTypeID);
        }

        /// <summary>
        /// To Obsolete Structure Type
        /// </summary>
        /// <param name="StructureTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureType(int StructureTypeID, string IsDeleted)
        {
            objStructureTypeDAL = new StructureTypeDAL();
            return objStructureTypeDAL.ObsoleteStructureType(StructureTypeID, IsDeleted);
        }

        #endregion
    }
}