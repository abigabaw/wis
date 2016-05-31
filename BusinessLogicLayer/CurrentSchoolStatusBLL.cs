using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Collections.Generic;

namespace WIS_BusinessLogic
{
    public class CurrentSchoolStatusBLL
    {       
        /// <summary>
        /// To Insert School Status Details
        /// </summary>
        /// <param name="CurrentSchoolStatusBOObj"></param>
        /// <returns></returns>
        public string InsertSchoolStatusDetails(CurrentSchoolStatusBO CurrentSchoolStatusBOObj)
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.InsertSchoolStatusDetails(CurrentSchoolStatusBOObj);                 
        }

        /// <summary>
        /// To Get All School Details
        /// </summary>
        /// <returns></returns>
        public SchoolStatusList GetAllSchoolDetails()
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.GetAllSchoolDetails();
        }

        /// <summary>
        /// To Get School Details
        /// </summary>
        /// <returns></returns>
        public SchoolStatusList GetSchoolDetails()
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.GetSchoolDetails();
        }

        /// <summary>
        /// To Get Current School Status By Id
        /// </summary>
        /// <param name="CurrentSchoolStatusID"></param>
        /// <returns></returns>
        public CurrentSchoolStatusBO GetCurSchlStatusById(int CurrentSchoolStatusID)
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.GetCurSchlStatusById(CurrentSchoolStatusID);
        }       

        //public int DeleteCurSchlStatus(int CurrentSchoolStatusID)
        //{
        //    CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
        //    return CurrentSchoolStatusDALObj.DeleteCurSchlStatus(CurrentSchoolStatusID);
        //}
        public string DeleteCurSchlStatus(int CurrentSchoolStatusID)
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.DeleteCurSchlStatus(CurrentSchoolStatusID);
        }

        /// <summary>
        /// To Edit Current School Status
        /// </summary>
        /// <param name="CurrentSchoolStatusBOObj"></param>
        /// <param name="EditCurSchStatusID"></param>
        /// <returns></returns>
        public string EditCurSchStatus(CurrentSchoolStatusBO CurrentSchoolStatusBOObj, int EditCurSchStatusID)
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.EditCurSchStatus(CurrentSchoolStatusBOObj, EditCurSchStatusID);
        }

        /// <summary>
        /// To Search School Status
        /// </summary>
        /// <param name="CurSchoolStatus"></param>
        /// <returns></returns>
        public object SearchSchoolStatus(string CurSchoolStatus)
        {
            CurrentSchoolStatusDAL CurrentSchoolStatusDALObj = new CurrentSchoolStatusDAL();
            return CurrentSchoolStatusDALObj.SearchSchoolStatus(CurSchoolStatus);
        }

        /// <summary>
        /// To Obsolete School Status
        /// </summary>
        /// <param name="CurrentSchoolStatusID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteSchoolStatus(int CurrentSchoolStatusID, string IsDeleted)
        {
            return (new CurrentSchoolStatusDAL()).ObsoleteSchoolStatus(CurrentSchoolStatusID, IsDeleted);
        }
    }
}