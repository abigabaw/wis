using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class CropsBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Cropsobj"></param>
        /// <returns></returns>
        public int Insert(CropsBO Cropsobj)
        {
            CropsDAL DALobj = new CropsDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(Cropsobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }  
        }

        /// <summary>
        /// To Get Crops
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CropsList GetCrops(int householdID)
        {
            CropsDAL CropsDALobj = new CropsDAL();
            return CropsDALobj.GetCrops(householdID);
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="CropId"></param>
        /// <returns></returns>
        public CropsBO GetData(int CropId)
        {
            CropsDAL DALobj = new CropsDAL();
            return DALobj.GetData(CropId);  
        }

        /// <summary>
        /// To Update Data
        /// </summary>
        /// <param name="Cropsobj"></param>
        /// <returns></returns>
        public int UpdateData(CropsBO Cropsobj)
        {
            CropsDAL DALobj = new CropsDAL();

            try
            {
                return DALobj.Update(Cropsobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Delete Data
        /// </summary>
        /// <param name="cropid"></param>
        /// <returns></returns>
        public int DeleteData(string cropid)
        {
            CropsDAL DALobj = new CropsDAL();
            return DALobj.DeleteData(cropid);
        }

        public CropsBO ShowPAPCROPImage(int householdID, int PermanentStructureID)
        {
            return (new CropsDAL()).ShowPAPCROPImage(householdID, PermanentStructureID);
        }
    }
}