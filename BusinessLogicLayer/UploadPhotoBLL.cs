using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class UploadPhotoBLL
    {
        /// <summary>
        /// To Insert Upload Photo
        /// </summary>
        /// <param name="objUploadPhotoBO"></param>
        /// <returns></returns>
        public string InsertUploadPhoto(UploadPhotoBO objUploadPhotoBO)
        {
            UploadPhotoDAL UploadPhotoDAL = new UploadPhotoDAL(); //Data pass -to Database Layer

            try
            {
                return UploadPhotoDAL.InsertUploadPhoto(objUploadPhotoBO);
            }
            catch
            {
                throw;
            }
            finally
            {
                UploadPhotoDAL = null;
            }
        }
    }
}