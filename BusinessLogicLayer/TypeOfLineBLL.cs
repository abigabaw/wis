using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class TypeOfLineBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="TypeOfLineBOObj"></param>
        /// <returns></returns>
        public string insert(TypeOfLineBO TypeOfLineBOObj)
        {
            try
            {
                TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
                return TypeOfLineDALObj.Insert(TypeOfLineBOObj);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        /// <summary>
        /// To Get Line Type
        /// </summary>
        /// <returns></returns>
        public TypeOfLineLists GetLineType()
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
            return TypeOfLineDALObj.GetLineType();
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public string Delete(int LineTypeID)
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL(); //Data pass -to Database Layer
            try
            {
                return TypeOfLineDALObj.Delete(LineTypeID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                TypeOfLineDALObj = null;
            }
        }

        /// <summary>
        /// To Get Line Type by ID
        /// </summary>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public  TypeOfLineBO GetLineTypebyID(int LineTypeID)
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
            return TypeOfLineDALObj.GetLineTypebyID(LineTypeID);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="TypeOfLineBOObj"></param>
        /// <param name="LineTypeID"></param>
        /// <returns></returns>
        public string Update(TypeOfLineBO TypeOfLineBOObj, int LineTypeID)
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
            try
            {
                return TypeOfLineDALObj.Update(TypeOfLineBOObj, LineTypeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TypeOfLineDALObj = null;
            }

        }

        /// <summary>
        /// To Obsolete Line Type
        /// </summary>
        /// <param name="lineTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteLineType(int lineTypeID, string IsDeleted, int updatedBy)
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
            return TypeOfLineDALObj.ObsoleteLineType(lineTypeID, IsDeleted, updatedBy);
        }

        /// <summary>
        /// To Get All Line Types
        /// </summary>
        /// <returns></returns>
        public TypeOfLineLists GetAllLineTypes()
        {
            TypeOfLineDAL TypeOfLineDALObj = new TypeOfLineDAL();
            return TypeOfLineDALObj.GetAllLineTypes();
        }
    }
}