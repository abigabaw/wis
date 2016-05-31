using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class DwellingBLL
    {
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="DwellingBOobj"></param>
        /// <returns></returns>
        public string Insert(DwellingBO DwellingBOobj)
        {

            DwellingDAL DwellingDALobj = new DwellingDAL(); //Data pass -to Database Layer

            try
            {
                return DwellingDALobj.Insert(DwellingBOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DwellingDALobj = null;
            }
        }

        /// <summary>
        /// To Get All Dwelling
        /// </summary>
        /// <returns></returns>
        public DwellingList GetAllDwelling()
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();
            return DwellingDALobj.GetAllDwelling();
        }

        /// <summary>
        /// To Get Dwelling
        /// </summary>
        /// <returns></returns>
        public DwellingList GetDwelling()
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();
            return DwellingDALobj.GetDwelling();
        }

        /// <summary>
        /// To Get Dwelling by ID
        /// </summary>
        /// <param name="DwellingID"></param>
        /// <returns></returns>
        public DwellingBO GetDwellingbyID(int DwellingID)
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();
            return DwellingDALobj.GetDwellingById(DwellingID);
        }

        /// <summary>
        /// To Delete Dwelling
        /// </summary>
        /// <param name="Dwellingid"></param>
        /// <returns></returns>
        public string DeleteDwelling(int Dwellingid)
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();//Data pass -to Database Layer
            try
            {
                return DwellingDALobj.DeleteDwelling(Dwellingid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                DwellingDALobj = null;
            }
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="DwellingBOobj"></param>
        /// <param name="DwellingID"></param>
        /// <returns></returns>
        public string Update(DwellingBO DwellingBOobj, int DwellingID)
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();
            try
            {
                return DwellingDALobj.Update(DwellingBOobj, DwellingID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DwellingDALobj = null;
            }

        }

        /// <summary>
        /// To Obsolete Dwelling
        /// </summary>
        /// <param name="DwellingID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteDwelling(int DwellingID, string IsDeleted)
        {
            DwellingDAL DwellingDALobj = new DwellingDAL();
            return DwellingDALobj.ObsoleteDwelling(DwellingID, IsDeleted);
        }

    }
}