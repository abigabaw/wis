using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class UploadPAPCoordinatesBLL
    {
        /// <summary>
        /// To Excel Data Import into Grid
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Extension"></param>
        /// <param name="HHID"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public DataTable ExcelDataImportintoGrid(string FilePath, string Extension, int HHID, int createdBy)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.ExcelDataImportintoGrid(FilePath, Extension, HHID, createdBy);
        }

        /// <summary>
        /// To Save Excel Data
        /// </summary>
        /// <param name="List1"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelData(UploadPAPCoordinatesList List1, string uID)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.SaveExcelData(List1, uID);
        }

        /// <summary>
        /// To Upload PAP Coordinates List
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public UploadPAPCoordinatesList GetAllPapCoordinatesData(int HHID, int PID)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.GetAllPapCoordinatesData(HHID, PID);
        }

        /// <summary>
        /// To Save Excel Data For All Paps
        /// </summary>
        /// <param name="List1"></param>
        /// <param name="uID"></param>
        /// <returns></returns>
        public string SaveExcelDataForAllPaps(UploadPAPCoordinatesList List1, string uID)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.SaveExcelDataForAllPaps(List1, uID);
        }

        public string SavePAPCoordinates(UploadPAPCoordinatesBO oUploadPAPCoordinatesBO)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.SavePAPCoordinates(oUploadPAPCoordinatesBO);
        }

        public string UpdatePAPCoordinates(UploadPAPCoordinatesBO oUploadPAPCoordinatesBO)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.UpdatePAPCoordinates(oUploadPAPCoordinatesBO);
        }

        public void DeletePapCoordinates(int ID)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            objUploadPAPCoordinatesDAL.DeletePapCoordinates(ID);
        }

        public UploadPAPCoordinatesBO GetPapCoordinatesDataByID(int ID)
        {
            UploadPAPCoordinatesDAL objUploadPAPCoordinatesDAL = new UploadPAPCoordinatesDAL();
            return objUploadPAPCoordinatesDAL.GetPapCoordinatesDataByID(ID);
        }
    }
}
