using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class DistrictBLL
    {
        /// <summary>
        /// To Get District
        /// </summary>
        /// <returns></returns>
        public DistrictList GetDistrict()
        {
            DistrictDAL objDisDAL = new DistrictDAL();
            return objDisDAL.GetDistrict();
        }

        /// <summary>
        /// To Get All Districts
        /// </summary>
        /// <returns></returns>
        public DistrictList GetAllDistricts()
        {
            DistrictDAL objDisDAL = new DistrictDAL();
            return objDisDAL.GetAllDistricts();
        }

        /// <summary>
        /// To Add District
        /// </summary>
        /// <param name="objDistrictBO"></param>
        /// <returns></returns>
        public string AddDistrict(DistrictBO objDistrictBO)
        {
            return (new DistrictDAL()).AddDistrict(objDistrictBO);
        }

        /// <summary>
        /// To Update District
        /// </summary>
        /// <param name="objDistrictBO"></param>
        /// <returns></returns>
        public string UpdateDistrict(DistrictBO objDistrictBO)
        {
            return (new DistrictDAL()).UpdateDistrict(objDistrictBO);
        }

        /// <summary>
        /// To Delete District
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public string DeleteDistrict(int districtID)
        {
            return (new DistrictDAL()).DeleteDistrict(districtID);
        }

        /// <summary>
        /// To Obsolete District
        /// </summary>
        /// <param name="districtID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteDistrict(int districtID, string isDeleted, int updatedBy)
        {
            return (new DistrictDAL()).ObsoleteDistrict(districtID, isDeleted, updatedBy);
        }

        /// <summary>
        /// To Get District By Id
        /// </summary>
        /// <param name="DistrictId"></param>
        /// <returns></returns>
        public DistrictBO GetDistrictById(int DistrictId)
        {
            return (new DistrictDAL()).GetDistrictById(DistrictId);
        }

        /// <summary>
        /// To Search District
        /// </summary>
        /// <param name="districtname"></param>
        /// <returns></returns>
        public DistrictList SearchDistrict(string districtname)
        {
            return (new DistrictDAL()).SearchDistrict(districtname);

        }


    }



}
