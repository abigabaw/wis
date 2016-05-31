using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PositionBLL
    {
        /// <summary>
        /// To Insert Position
        /// </summary>
        /// <param name="PositionBOObj"></param>
        /// <returns></returns>
        public string InsertPosition(PositionBO PositionBOObj)
        {
            PositionDAL PositionDAL = new PositionDAL(); //Data pass -to Database Layer

            try
            {
                return PositionDAL.InsertPosition(PositionBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                PositionDAL = null;
            }
        }

        // serach the data from the Database Mst_Concern
        /// <summary>
        /// To Get Position
        /// </summary>
        /// <returns></returns>
        public PositionList GetPosition()
        {
            PositionDAL PositionDALObj = new PositionDAL();
            return PositionDALObj.GetPosition();
        }

        /// <summary>
        /// To Get All Positions
        /// </summary>
        /// <returns></returns>
        public PositionList GetAllPositions()
        {
            PositionDAL PositionDALObj = new PositionDAL();
            return PositionDALObj.GetAllPositions();
        }

        /// <summary>
        /// To Obsolete Position
        /// </summary>
        /// <param name="PositionID"></param>
        /// <param name="PositionIsDeleted"></param>
        /// <returns></returns>
        public string ObsoletePosition(int PositionID, string PositionIsDeleted)
        {
            PositionDAL PositionDALObj = new PositionDAL();
            return PositionDALObj.ObsoletePosition(PositionID, PositionIsDeleted);
        }

        /// <summary>
        /// To Delete Position
        /// </summary>
        /// <param name="PositionID"></param>
        /// <returns></returns>
        public string DeletePosition(int PositionID)
        {
            PositionDAL PositionDALObj = new PositionDAL();
            return PositionDALObj.DeletePosition(PositionID);
        }

        //Search the Singal Data by passing ID
        /// <summary>
        /// To Get Position By Id
        /// </summary>
        /// <param name="PositionID"></param>
        /// <returns></returns>
        public PositionBO GetPositionById(int PositionID)
        {
            PositionDAL PositionDALObj = new PositionDAL();
            return PositionDALObj.GetPositionById(PositionID);
        }

        /// <summary>
        /// To EDIT POSITION
        /// </summary>
        /// <param name="PositionBOObj"></param>
        /// <returns></returns>
        public string EDITPOSITION(PositionBO PositionBOObj)
        {
            PositionDAL PositionDAL = new PositionDAL(); //Data pass -to Database Layer

            try
            {
                return PositionDAL.EDITPOSITION(PositionBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                PositionDAL = null;
            }
        }
    }
}
