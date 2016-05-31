using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class DamagedCropsBLL
    {
        //public DamagedCropsList GetCropName()
        //{
        //    DamagedCropsDAL DALobj = new DamagedCropsDAL();

        //    try
        //    {
        //        return DALobj.GetCropName();
        //    }
        //    catch (Exception erromsg)
        //    {
        //        throw (erromsg);
        //    }
        //    finally
        //    {
        //        DALobj = null;
        //    }
        //}

        //public DamagedCropsList GetCropType()
        //{
        //    DamagedCropsDAL DALobj = new DamagedCropsDAL();

        //    try
        //    {
        //        return DALobj.GetCropType();
        //    }
        //    catch (Exception erromsg)
        //    {
        //        throw (erromsg);
        //    }
        //    finally
        //    {
        //        DALobj = null;
        //    }
        //}

        //public DamagedCropsList GetCropDescription()
        //{
        //    DamagedCropsDAL DALobj = new DamagedCropsDAL();

        //    try
        //    {
        //        return DALobj.GetCropDescription();
        //    }
        //    catch (Exception erromsg)
        //    {
        //        throw (erromsg);
        //    }
        //    finally
        //    {
        //        DALobj = null;
        //    }
        //}

        /// <summary>
        /// To Get Damaged By
        /// </summary>
        /// <returns></returns>
        public DamagedCropsList GetDamagedBy()
        {
            DamagedCropsDAL DALobj = new DamagedCropsDAL();

            try
            {
                return DALobj.GetDamagedBy();
            }
            catch (Exception erromsg)
            {
                throw (erromsg);
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="DamagedCropsobj"></param>
        /// <returns></returns>
        public int Insert(DamagedCropsBO DamagedCropsobj)
        {
            DamagedCropsDAL DALobj = new DamagedCropsDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(DamagedCropsobj);
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
        /// To Get Damaged Crops
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public DamagedCropsList GetDamagedCrops(string hhid)
        {
            DamagedCropsDAL DamagedCropsDALobj = new DamagedCropsDAL();
            return DamagedCropsDALobj.GetDamagedCrops(hhid);
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="damageCropId"></param>
        /// <returns></returns>
        public DamagedCropsBO GetData(int damageCropId)
        {
            DamagedCropsDAL DALobj = new DamagedCropsDAL();
            return DALobj.GetData(damageCropId);   
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="DamagedCropsobj"></param>
        /// <returns></returns>
        public int Update(DamagedCropsBO DamagedCropsobj)
        {
            DamagedCropsDAL DALobj = new DamagedCropsDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(DamagedCropsobj);
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
        /// <param name="damageCropId"></param>
        /// <returns></returns>
        public int DeleteData(int damageCropId)
        {
            DamagedCropsDAL DALobj = new DamagedCropsDAL();
            return DALobj.DeleteData(damageCropId);
        }

        /// <summary>
        /// To Show DAMAGED CROPS Image
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public DamagedCropsBO ShowDAMAGEDCROPSImage(int householdID, int PermanentStructureID)
        {
            return (new DamagedCropsDAL()).ShowDAMAGEDCROPSImage(householdID, PermanentStructureID);
        }
    }
}