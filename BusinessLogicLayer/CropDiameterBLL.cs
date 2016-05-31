using WIS_BusinessObjects;
using WIS_DataAccess;



namespace WIS_BusinessLogic
{
    public class CropDiameterBLL
    {
        /// <summary>
        /// To Insert Crop Diameter
        /// </summary>
        /// <param name="objCropDiameter"></param>
        /// <returns></returns>
        public string InsertCropDiameter(CropDiameterBO objCropDiameter)
        {
            CropDiameterDAL CropDiameterDAL = new CropDiameterDAL(); //Data pass -to Database Layer

            try
            {
                return CropDiameterDAL.InsertCropDiameter(objCropDiameter);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropDiameterDAL = null;
            }
        }

        /// <summary>
        /// To Get Crop Diameter By Id
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <returns></returns>
        public CropDiameterBO GetCropDiameterById(int CROPDIAMETERID)
        {
            CropDiameterDAL CropDiameterDALObj = new CropDiameterDAL();
            return CropDiameterDALObj.GetCropDiameterById(CROPDIAMETERID);
        }

        /// <summary>
        /// To Delete Crop Diameter
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <returns></returns>
        public string DeleteCropDiameter(int CROPDIAMETERID)
        {
            CropDiameterDAL CropDiameterDALObj = new CropDiameterDAL();
            return CropDiameterDALObj.DeleteCropDiameter(CROPDIAMETERID);
        }

        /// <summary>
        /// To Obsolete Crop Diameter
        /// </summary>
        /// <param name="CROPDIAMETERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropDiameter(int CROPDIAMETERID,string IsDeleted)
        {
            CropDiameterDAL CropDiameterDALObj = new CropDiameterDAL();
            return CropDiameterDALObj.ObsoleteCropDiameter(CROPDIAMETERID, IsDeleted);
        }

        /// <summary>
        /// To Get Crop Diameter
        /// </summary>
        /// <returns></returns>
        public CropDiameterList GetCropDiameter()
        {
            CropDiameterDAL CropDiameterDALObj = new CropDiameterDAL();
            return CropDiameterDALObj.GetCropDiameter();
        }

        /// <summary>
        /// To EDIT Crop Diameter
        /// </summary>
        /// <param name="objCropDiameter"></param>
        /// <returns></returns>
        public string EDITCropDiameter(CropDiameterBO objCropDiameter)
        {
            CropDiameterDAL CropDiameterDAL = new CropDiameterDAL(); //Data pass -to Database Layer

            try
            {
                return CropDiameterDAL.EDITCropDiameter(objCropDiameter);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropDiameterDAL = null;
            }
        }
    }
}