using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CropNameBLL
    {
        /// <summary>
        /// To Get All Crop Name Details
        /// </summary>
        /// <returns></returns>
        public object GetAllCropNameDetails()
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.GetAllCropNameDetails();
        }

        /// <summary>
        /// To Get Crop Name Details
        /// </summary>
        /// <returns></returns>
        public object GetCropNameDetails()
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.GetCropNameDetails();
        }

        /// <summary>
        /// To Insert Crop Name Details
        /// </summary>
        /// <param name="CropNameBOObj"></param>
        /// <returns></returns>
        public string InsertCropNameDetails(CropNameBO CropNameBOObj)
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.InsertCropNameDetails(CropNameBOObj);
        }

        /// <summary>
        /// To Update Crop Name Details
        /// </summary>
        /// <param name="CropNameBOObj"></param>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public string UpdateCropNameDetails(CropNameBO CropNameBOObj, int CROPID)
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.UpdateCropNameDetails(CropNameBOObj, CROPID);
        }

        /// <summary>
        /// To Get Crop Name By Id
        /// </summary>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public CropNameBO GetCropNameById(int CROPID)
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.GetCropNameById(CROPID);
        }

        /// <summary>
        /// To Delete Crop Type Row
        /// </summary>
        /// <param name="CROPID"></param>
        /// <returns></returns>
        public string DeleteCropTypeRow(int CROPID)
        {
            CropNameDAL CropNameDALObj = new CropNameDAL();
            return CropNameDALObj.DeleteCropTypeRow(CROPID);
        }

        /// <summary>
        /// To Obsolete Crop Name
        /// </summary>
        /// <param name="CROPID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropName(int CROPID, string IsDeleted)
        {
            return (new CropNameDAL()).ObsoleteCropName(CROPID, IsDeleted);
        }
    }
}