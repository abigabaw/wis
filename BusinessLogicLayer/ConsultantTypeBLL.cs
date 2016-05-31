using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ConsultantTypeBLL
    {
        /// <summary>
        /// To Get ALL Consultant Type
        /// </summary>
        /// <returns></returns>
        public ConsultantTypeList GetALLConsultantType()
        {
            return (new ConsultantTypeDAL()).GetALLConsultantType();
        }

        /// <summary>
        /// To Get Consultant Type
        /// </summary>
        /// <returns></returns>
        public ConsultantTypeList GetConsultantType()
        {
            return (new ConsultantTypeDAL()).GetConsultantType();
        }

        /// <summary>
        /// To insert into database
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string insert(ConsultantTypeBO objconsultantType)
        {
            ConsultantTypeDAL ConsultantTypeDALobj = new ConsultantTypeDAL(); //Data pass -to Database Layer

            try
            {
                return ConsultantTypeDALobj.insert(objconsultantType);
            }
            catch
            {
                throw;
            }
            finally
            {
                ConsultantTypeDALobj = null;
            }

        }

        /// <summary>
        /// To EDIT Consultant Type
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string EDITConsultantType(ConsultantTypeBO objconsultantType)
        {
            ConsultantTypeDAL ConsultantTypeDALobj = new ConsultantTypeDAL(); //Data pass -to Database Layer

            try
            {
                return ConsultantTypeDALobj.EDITConsultantType(objconsultantType);
            }
            catch
            {
                throw;
            }
            finally
            {
                ConsultantTypeDALobj = null;
            }
        }
        
        /// <summary>
        /// To Get Consultant Type Id
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <returns></returns>
        public ConsultantTypeBO GetConsultantTypeId(int consultantTypeID)
        {
            ConsultantTypeDAL ConsultantTypeDALobj = new ConsultantTypeDAL(); //Data pass -to Database Layer
            return ConsultantTypeDALobj.GetConsultantTypeId(consultantTypeID);
        }

        /// <summary>
        /// To Delete Consultant Type
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <returns></returns>
        public string DeleteConsultantType(int consultantTypeID)
        {
            ConsultantTypeDAL ConsultantTypeDALobj = new ConsultantTypeDAL(); //Data pass -to Database Layer
            return ConsultantTypeDALobj.DeleteConsultantType(consultantTypeID);
        }

        /// <summary>
        /// To Obsolete consultant Type
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteconsultantType(int consultantTypeID, string IsDeleted, int updatedBy)
        {
            return (new ConsultantTypeDAL()).ObsoleteconsultantType(consultantTypeID, IsDeleted, updatedBy);
        }
    }
}
