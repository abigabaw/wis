using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class CropDescriptionBLL
    {
        /// <summary>
        /// To Insert Crop Description
        /// </summary>
        /// <param name="objCropDescription"></param>
        /// <returns></returns>
        public string InsertCropDescription(CropDescriptionBO objCropDescription)
        {
            CropDescriptionDAL CropDescriptionDAL = new CropDescriptionDAL(); //Data pass -to Database Layer

            try
            {
                return CropDescriptionDAL.InsertCropDescription(objCropDescription);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropDescriptionDAL = null;
            }
        }

        /// <summary>
        /// To Get All Crop Description
        /// </summary>
        /// <returns></returns>
        public CropDescriptionList GetAllCropDescription()
        {
            CropDescriptionDAL CropDescriptionDALObj = new CropDescriptionDAL();
            return CropDescriptionDALObj.GetAllCropDescription();
        }

        /// <summary>
        /// To Get Crop Description
        /// </summary>
        /// <returns></returns>
        public CropDescriptionList GetCropDescription()
        {
            CropDescriptionDAL CropDescriptionDALObj = new CropDescriptionDAL();
            return CropDescriptionDALObj.GetCropDescription();
        }

        public string DeleteCropDESC(int CROPDESCRIPTIONID)
        {
            CropDescriptionDAL CropDescriptionDALObj = new CropDescriptionDAL();
            return CropDescriptionDALObj.DeleteCropDESC(CROPDESCRIPTIONID);
        }

        /// <summary>
        /// To Obsolete Crop DESC
        /// </summary>
        /// <param name="CROPDESCRIPTIONID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropDESC(int CROPDESCRIPTIONID,string IsDeleted)
        {
            CropDescriptionDAL CropDescriptionDALObj = new CropDescriptionDAL();
            return CropDescriptionDALObj.ObsoleteCropDESC(CROPDESCRIPTIONID, IsDeleted);
        }

        /// <summary>
        /// To Get Crop Description Id
        /// </summary>
        /// <param name="CROPDESCRIPTIONID"></param>
        /// <returns></returns>
        public CropDescriptionBO GetCropDescriptionId(int CROPDESCRIPTIONID)
        {
            CropDescriptionDAL CropDescriptionDALObj = new CropDescriptionDAL();
            return CropDescriptionDALObj.GetCropDescriptionId(CROPDESCRIPTIONID);
        }

        /// <summary>
        /// To EDIT Crop Descr
        /// </summary>
        /// <param name="objCropDesc"></param>
        /// <returns></returns>
        public string EDITCropDescr(CropDescriptionBO objCropDesc)
        {
            CropDescriptionDAL CropDescriptionDAL = new CropDescriptionDAL(); //Data pass -to Database Layer

            try
            {
                return CropDescriptionDAL.EDITCropDescr(objCropDesc);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropDescriptionDAL = null;
            }
        }
    }
}