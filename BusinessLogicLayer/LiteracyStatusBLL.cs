using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class LiteracyStatusBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="LiteracyStatusBOobj"></param>
        /// <returns></returns>
        public string Insert(LiteracyStatusBO LiteracyStatusBOobj)
        {
            LiteracyStatusDAL LiteracyStatusDAL = new LiteracyStatusDAL(); //Data pass -to Database Layer

            try
            {

                return LiteracyStatusDAL.Insert(LiteracyStatusBOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LiteracyStatusDAL = null;
            }
        }

        /// <summary>
        /// To Get All Literacy Status
        /// </summary>
        /// <returns></returns>
        public LiteracyStatusList GetAllLiteracyStatus()
        {
            LiteracyStatusDAL LiteracyStatusDALobj = new LiteracyStatusDAL();
            return LiteracyStatusDALobj.GetAllLiteracyStatus();
        }

        /// <summary>
        /// To Get Literacy Status
        /// </summary>
        /// <returns></returns>
        public LiteracyStatusList GetLiteracyStatus()
        {
            LiteracyStatusDAL LiteracyStatusDALobj = new LiteracyStatusDAL();
            return LiteracyStatusDALobj.GetLiteracyStatus();
        }

        /// <summary>
        /// To Delete from Database
        /// </summary>
        /// <param name="literacyStatusID"></param>
        /// <returns></returns>
        public string Delete(int literacyStatusID)
        {
            LiteracyStatusDAL LiteracyStatusDALobj = new LiteracyStatusDAL(); //Data pass -to Database Layer
            try
            {
                return LiteracyStatusDALobj.Delete(literacyStatusID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                LiteracyStatusDALobj = null;
            }
        }

        /// <summary>
        /// To Obsolete Literacy Status
        /// </summary>
        /// <param name="literacyStatusID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLiteracyStatus(int literacyStatusID, string IsDeleted)
        {
            return (new LiteracyStatusDAL()).ObsoleteLiteracyStatus(literacyStatusID, IsDeleted);
        }

        /// <summary>
        /// TO Update in Database
        /// </summary>
        /// <param name="LitStatusBoobj"></param>
        /// <param name="litStatusID"></param>
        /// <returns></returns>
        public string Update(LiteracyStatusBO LitStatusBoobj, int litStatusID)
        {
            LiteracyStatusDAL LiteracyStatusDALobj = new LiteracyStatusDAL(); //Data pass -to Database Layer
            try
            {
                return LiteracyStatusDALobj.Update(LitStatusBoobj, litStatusID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                LiteracyStatusDALobj = null;
            }
        }
    }
}