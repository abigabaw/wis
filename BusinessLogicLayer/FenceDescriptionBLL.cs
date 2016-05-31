using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class FenceDescriptionBLL
    {
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="FenceDescriptionBOobj"></param>
        /// <returns></returns>
        public string insert(FenceDescriptionBO FenceDescriptionBOobj)
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            try
            {
                return FenceDescriptionDALObj.insert(FenceDescriptionBOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FenceDescriptionDALObj = null;
            }
        }

        /// <summary>
        /// To Fence Description List
        /// </summary>
        /// <returns></returns>
        public FenceDescriptionList GetAllFence()
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            return FenceDescriptionDALObj.GetAllFence();
        }

        /// <summary>
        /// To Get Fence
        /// </summary>
        /// <returns></returns>
        public FenceDescriptionList GetFence()
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            return FenceDescriptionDALObj.GetFence();
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="FenceID"></param>
        /// <returns></returns>
        public string Delete(int FenceID)
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL(); //Data pass -to Database Layer
            try
            {
                return FenceDescriptionDALObj.Delete(FenceID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FenceDescriptionDALObj = null;
            }
        }

        /// <summary>
        /// To Obsolete Fence Description
        /// </summary> 
        /// <param name="FenceID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteFenceDescription(int FenceID, string IsDeleted)
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            return FenceDescriptionDALObj.ObsoleteFenceDescriptionDAL(FenceID, IsDeleted);
        }


        public FenceDescriptionBO GetFencebyID(int FenceID)
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            return FenceDescriptionDALObj.GetFencebyID(FenceID);
        }


        public string Update(FenceDescriptionBO FenceDescriptionBOobj, int FenceID)
        {
            FenceDescriptionDAL FenceDescriptionDALObj = new FenceDescriptionDAL();
            try
            {
                return FenceDescriptionDALObj.Update(FenceDescriptionBOobj, FenceID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                FenceDescriptionDALObj = null;
            }

        }
    }
}