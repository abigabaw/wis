using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LandReceivedMSTBLL
    {
        /// <summary>
        /// To Insert Land Received
        /// </summary>
        /// <param name="LandReceivedMSTBOObj"></param>
        /// <returns></returns>
        public string InsertLandReceived(LandReceivedMSTBO LandReceivedMSTBOObj)
        {
            LandReceivedMSTDAL LandReceivedMSTDAL = new LandReceivedMSTDAL(); //Data pass -to Database Layer

            try
            {
                return LandReceivedMSTDAL.InsertLandReceived(LandReceivedMSTBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                LandReceivedMSTDAL = null;
            }
        }

        // serach the data from the Database Mst_Concern
        /// <summary>
        /// To Get Land Received 
        /// </summary>
        /// <returns></returns>
        public LandReceivedMSTList GetLandReceived()
        {
            LandReceivedMSTDAL LandReceivedMSTDALOBJ = new LandReceivedMSTDAL(); //Data pass -to Database Layer
            return LandReceivedMSTDALOBJ.GetLandReceived();
        }

        /// <summary>
        /// To Get All Land Received
        /// </summary>
        /// <returns></returns>
        public LandReceivedMSTList GetAllLandReceived()
        {
            return (new LandReceivedMSTDAL()).GetAllLandReceived();
        }

        /// <summary>
        /// To Obsolete Land Received
        /// </summary>
        /// <param name="LandReceivedID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLandReceived(int LandReceivedID, string IsDeleted)
        {
            LandReceivedMSTDAL LandReceivedMSTDALOBJ = new LandReceivedMSTDAL(); //Data pass -to Database Layer
            return LandReceivedMSTDALOBJ.ObsoleteLandReceived(LandReceivedID, IsDeleted);
        }

        /// <summary>
        /// To Delete Land Received
        /// </summary>
        /// <param name="LandReceivedID"></param>
        /// <returns></returns>
        public string DeleteLandReceived(int LandReceivedID)
        {
            LandReceivedMSTDAL LandReceivedMSTDALOBJ = new LandReceivedMSTDAL(); //Data pass -to Database Layer
            return LandReceivedMSTDALOBJ.DeleteLandReceived(LandReceivedID);
        }

        //Search the Singal Data by passing ID
        public LandReceivedMSTBO GetLandReceivedByID(int LandReceivedID)
        {
            LandReceivedMSTDAL LandReceivedMSTDALOBJ = new LandReceivedMSTDAL(); //Data pass -to Database Layer
            return LandReceivedMSTDALOBJ.GetLandReceivedByID(LandReceivedID);
        }

        /// <summary>
        /// To EDIT LAND RECEIVED
        /// </summary>
        /// <param name="LandReceivedMSTBOObj"></param>
        /// <returns></returns>
        public string EDITLANDRECEIVED(LandReceivedMSTBO LandReceivedMSTBOObj)
        {
            LandReceivedMSTDAL LandReceivedMSTDALOBJ = new LandReceivedMSTDAL(); //Data pass -to Database Layer

            try
            {
                return LandReceivedMSTDALOBJ.EDITLANDRECEIVED(LandReceivedMSTBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                LandReceivedMSTDALOBJ = null;
            }
        }

    }
}
