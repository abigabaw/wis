using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class GrievancesMasterBLL
    {
        /// <summary>
        /// To Get ALL Grievances Category
        /// </summary>
        /// <returns></returns>
        public GrievancesMasterList GetALLGrievancesCategory()
        {
            return (new GrievancesMasterDAL()).GetALLGrievancesCategory();
        }

        /// <summary>
        /// To Insert into database
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string insert(GrievancesMasterBO objconsultantType)
        {
            GrievancesMasterDAL GrievancesMasterDALobj = new GrievancesMasterDAL(); //Data pass -to Database Layer

            try
            {
                return GrievancesMasterDALobj.insert(objconsultantType);
            }
            catch
            {
                throw;
            }
            finally
            {
                GrievancesMasterDALobj = null;
            }

        }

        /// <summary>
        /// To EDIT Grievances Category
        /// </summary>
        /// <param name="objconsultantType"></param>
        /// <returns></returns>
        public string EDITGrievancesCategory(GrievancesMasterBO objconsultantType)
        {
            GrievancesMasterDAL GrievancesMasterDALobj = new GrievancesMasterDAL(); //Data pass -to Database Layer

            try
            {
                return GrievancesMasterDALobj.EDITGrievancesCategory(objconsultantType);
            }
            catch
            {
                throw;
            }
            finally
            {
                GrievancesMasterDALobj = null;
            }
        }

        /// <summary>
        /// To Get Grievances Category Id
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <returns></returns>
        public GrievancesMasterBO GetGrievancesCategoryId(int consultantTypeID)
        {
            GrievancesMasterDAL GrievancesMasterDALobj = new GrievancesMasterDAL(); //Data pass -to Database Layer
            return GrievancesMasterDALobj.GetGrievancesCategoryId(consultantTypeID);
        }

        /// <summary>
        /// To Delete Grievances Category
        /// </summary>
        /// <param name="consultantTypeID"></param>
        /// <returns></returns>
        public string DeleteGrievancesCategory(int consultantTypeID)
        {
            GrievancesMasterDAL GrievancesMasterDALobj = new GrievancesMasterDAL(); //Data pass -to Database Layer
            return GrievancesMasterDALobj.DeleteGrievancesCategory(consultantTypeID);
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
            return (new GrievancesMasterDAL()).ObsoleteconsultantType(consultantTypeID, IsDeleted, updatedBy);
        }
    }
}
