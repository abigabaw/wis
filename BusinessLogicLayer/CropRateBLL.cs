using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CropRateBLL
    {
        /// <summary>
        /// To Get Crop Rate
        /// </summary>
        /// <param name="cropid"></param>
        /// <returns></returns>
        public CropRateList GetCropRate(int cropid)
        {
            CropRateDAL objCRDAL = new CropRateDAL();
            return objCRDAL.GetCropRate(cropid);
        } 

        /// <summary>
        /// To Add Crop Rate
        /// </summary>
        /// <param name="objCRBO"></param>
        /// <returns></returns>
         public string AddCropRate(CropRateBO   objCRBO)
        
            {
                CropRateDAL objCRDAL = new CropRateDAL();
                return objCRDAL.AddCropRate(objCRBO);
            }
               
        /// <summary>
         /// To Delete Crop Rate
        /// </summary>
        /// <param name="cropID"></param>
        /// <returns></returns>
        public string DeleteCropRate(int cropID)
        {
            CropRateDAL objCRDAL = new CropRateDAL();
            return objCRDAL.DeleteCropRate(cropID);
        }

        /// <summary>
        /// To Obsolete Crop Rate
        /// </summary>
        /// <param name="cropID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropRate(int cropID, string IsDeleted)
        {
            CropRateDAL objCRDAL = new CropRateDAL();
            return objCRDAL.ObsoleteCropRate(cropID,IsDeleted);
        }

        /// <summary>
        /// To Update Crop Rate
        /// </summary>
        /// <param name="objCRBO"></param>
        /// <returns></returns>
        public string UpdateCropRate(CropRateBO objCRBO)
        {
            CropRateDAL objCRDAL = new CropRateDAL();
            return objCRDAL.UpdateCropRate(objCRBO);
        }

        /// <summary>
        /// To Get Crop Rate By ID
        /// </summary>
        /// <param name="cropID"></param>
        /// <returns></returns>
        public CropRateBO GetCropRateByID(int cropID)
        {
            CropRateDAL objCRDAL = new CropRateDAL();
            return objCRDAL.GetCropRateByID(cropID);
        }

        /// <summary>
        /// To Get Crop Rate By District
        /// </summary>
        /// <param name="cropID"></param>
        /// <param name="CropDesID"></param>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CropRateBO GetCropRateByDistrict(int cropID, int CropDesID, int householdID)
        {
            return (new CropRateDAL()).GetCropRateByDistrict(cropID, CropDesID, householdID);
        }
    }       
}
