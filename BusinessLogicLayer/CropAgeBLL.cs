using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class CropAgeBLL
    {
        /// <summary>
        /// To Insert Crop Age
        /// </summary>
        /// <param name="objCropAge"></param>
        /// <returns></returns>
        public string InsertCropAge(CropAgeBO objCropAge)
        {
            CropAgeDAL CropAgeDAL = new CropAgeDAL(); //Data pass -to Database Layer

            try
            {
                return CropAgeDAL.InsertCropAge(objCropAge);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropAgeDAL = null;
            }
        }

        /// <summary>
        /// To Get Crop Age By Id
        /// </summary>
        /// <param name="CROPAGEID"></param>
        /// <returns></returns>
        public CropAgeBO GetCropAgeById(int CROPAGEID)
        {
            CropAgeDAL CropAgeDALObj = new CropAgeDAL();
            return CropAgeDALObj.GetCropAgeById(CROPAGEID);
        }

        /// <summary>
        /// To Delete Crop Age
        /// </summary>
        /// <param name="CROPAGEID"></param>
        /// <returns></returns>
        public string DeleteCropAge(int CROPAGEID)
        {
            CropAgeDAL CropAgeDALObj = new CropAgeDAL();
            return CropAgeDALObj.DeleteCropAge(CROPAGEID);
        }

        /// <summary>
        /// To Obsolete Crop Age
        /// </summary>
        /// <param name="CROPAGEID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteCropAge(int CROPAGEID,string IsDeleted)
        {
            CropAgeDAL CropAgeDALObj = new CropAgeDAL();
            return CropAgeDALObj.ObsoleteCropAge(CROPAGEID,IsDeleted);
        }

        /// <summary>
        /// To Get Crop Age
        /// </summary>
        /// <returns></returns>
        public CropAgeList GetCropAge()
        {
            CropAgeDAL CropAgeDALObj = new CropAgeDAL();
            return CropAgeDALObj.GetCropAge();
        }

        /// <summary>
        /// To EDIT CROP AGE
        /// </summary>
        /// <param name="objCropAge"></param>
        /// <returns></returns>
        public string EDITCROPAGE(CropAgeBO objCropAge)
        {
            CropAgeDAL CropAgeDAL = new CropAgeDAL(); //Data pass -to Database Layer

            try
            {
                return CropAgeDAL.EDITCROPAGE(objCropAge);
            }
            catch
            {
                throw;
            }
            finally
            {
                CropAgeDAL = null;
            }
        }
    }
}