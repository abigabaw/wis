using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CropTypeBLL
    {
        /// <summary>
        /// To Get Crop Details
        /// </summary>
        /// <returns></returns>
        public object GetCropDetails()
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.GetCropDetails();
        }

        /// <summary>
        /// To Get All Crop Details
        /// </summary>
        /// <returns></returns>
        public object GetAllCropDetails()
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.GetAllCropDetails();
        }

        /// <summary>
        /// To Insert Crop Type Details
        /// </summary>
        /// <param name="CropTypeBOObj"></param>
        /// <returns></returns>
        public string InsertCropTypeDetails(CropTypeBO CropTypeBOObj)
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.InsertCropTypeDetails(CropTypeBOObj);
        }

        /// <summary>
        /// To Edit Crop Type Details
        /// </summary>
        /// <param name="CropTypeBOObj"></param>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public string EditCropTypeDetails(CropTypeBO CropTypeBOObj, int CROPTYPEID)
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.EditCropTypeDetails(CropTypeBOObj, CROPTYPEID);
        }

        /// <summary>
        /// To Delete Crop Type Row
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public string DeleteCropTypeRow(int CROPTYPEID)
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.DeleteCropTypeRow(CROPTYPEID);
        }

        /// <summary>
        /// To Obsolete Crop Type
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropType(int CROPTYPEID,string IsDeleted)
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.ObsoleteCropTypeRow(CROPTYPEID, IsDeleted);
        }

        /// <summary>
        /// To Get Crop Type By Id
        /// </summary>
        /// <param name="CROPTYPEID"></param>
        /// <returns></returns>
        public CropTypeBO GetCropTypeById(int CROPTYPEID)
        {
            CropTypeDAL CropTypeDALObj = new CropTypeDAL();
            return CropTypeDALObj.GetCropTypeById(CROPTYPEID);
        }
    }
}