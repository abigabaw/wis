using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ParishBLL
    {
        //public Parish_List GetParish(string subcountyid)
        //{
        //    ParishDAL ParishDALobj = new ParishDAL();
        //    return ParishDALobj.GetParish(subcountyid);
        //}

        /// <summary>
        /// To Get All Parish
        /// </summary>
        /// <param name="subcountyid"></param>
        /// <param name="countyid"></param>
        /// <param name="districtid"></param>
        /// <returns></returns>
        public Parish_List GetAllParish(int subcountyid ,int countyid,int districtid)
        {
            ParishDAL ParishDALobj = new ParishDAL();
            return ParishDALobj.GetAllParish(subcountyid, countyid, districtid);
        }

        /// <summary>
        /// To Search Parish
        /// </summary>
        /// <param name="SearchParish"></param>
        /// <returns></returns>
        public Parish_List SearchParish(string SearchParish)
        {
            ParishDAL ParishDALobj = new ParishDAL();
            return ParishDALobj.SearchParish(SearchParish);
        }

        /// <summary>
        /// To Add Parish
        /// </summary>
        /// <param name="ParishBOobj"></param>
        /// <returns></returns>
        public string AddParish(ParishBO ParishBOobj)
        {
            return (new ParishDAL()).AddParish(ParishBOobj);
        }

        /// <summary>
        /// To Update Parish
        /// </summary>
        /// <param name="ParishBOobj"></param>
        /// <returns></returns>
        public string UpdateParish(ParishBO ParishBOobj)
        {
            return (new ParishDAL()).UpdateParish(ParishBOobj);
        }

        /// <summary>
        /// To Delete Parish
        /// </summary>
        /// <param name="ParishId"></param>
        /// <returns></returns>
        public string DeleteParish(int ParishId)
        {
            return (new ParishDAL()).DeleteParish(ParishId);
        }

        /// <summary>
        /// To Obsolete Parish
        /// </summary>
        /// <param name="countyID"></param>
        /// <param name="isDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoleteParish(int countyID, string isDeleted, int updatedBy)
        {
            return (new ParishDAL()).ObsoleteParish(countyID, isDeleted, updatedBy);
        }

        /// <summary>
        /// To Get Parish By Id
        /// </summary>
        /// <param name="countyID"></param>
        /// <returns></returns>
        public ParishBO GetParishById(int countyID)
        {
            return (new ParishDAL()).GetParishById(countyID);
        }
    }
}
