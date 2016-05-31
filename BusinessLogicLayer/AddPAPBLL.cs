using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class AddPAPBLL
    {
        /// <summary>
        /// To import excel data to grid
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension"></param>
        /// <param name="projectID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string fileName,string fileExtension, int projectID, int createdBy)
        {   
            AddPAPDAL objAddPAPDAL = new AddPAPDAL();
            return objAddPAPDAL.ExcelDataImportintoGrid(fileName, fileExtension, projectID, createdBy);            
        }
        /// <summary>
        /// To save excel data to database
        /// </summary>
        /// <param name="dtPap"></param>
        /// <param name="ProjectID"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(DataTable dtPap, int ProjectID, string uID)
        {
            AddPAPDAL objAddPAPDAL = new AddPAPDAL();
            return objAddPAPDAL.SaveExcelData(dtPap, ProjectID, uID);
        }
        /// <summary>
        /// To add pap data into database
        /// </summary>
        /// <param name="objAddPAP"></param>
        /// <returns></returns>
        public string AddPAP(AddPAPBO objAddPAP)
        {
            return (new AddPAPDAL()).AddPAP(objAddPAP);
        }
        /// <summary>
        /// TO update PAP data to database
        /// </summary>
        /// <param name="objHouseholdPAP"></param>
        /// <returns></returns>
        public string UpdatePAP(PAP_HouseholdBO objHouseholdPAP)
        {
            return (new AddPAPDAL()).UpdatePAP(objHouseholdPAP);
        }
        /// <summary>
        /// To Obsolete PAP data in database
        /// </summary>
        /// <param name="PAPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoletePAP(int PAPID, string IsDeleted)
        {
            return (new AddPAPDAL()).ObsoletePAP(PAPID, IsDeleted);
        }
        /// <summary>
        /// To Delete PAP data from database
        /// </summary>
        /// <param name="PAPID"></param>
        /// <returns></returns>
        public string DeletePAP(int PAPID)
        {
            return (new AddPAPDAL()).DeletePAP(PAPID);
        }
    }
}