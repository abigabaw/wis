using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class SubCategoryBLL
    {
       /// <summary>
        /// To Insert into database
       /// </summary>
       /// <param name="SubCategoryBOobj"></param>
       /// <returns></returns>
       public string Insert(SubCategoryBO SubCategoryBOobj)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer

            try
            {
                return SubCategoryDALobj.Insert(SubCategoryBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                SubCategoryDALobj = null;
            }
        }

       /// <summary>
       /// To Edit database
       /// </summary>
       /// <param name="SubCategoryBOobj"></param>
       /// <returns></returns>
       public string Edit(SubCategoryBO SubCategoryBOobj)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer

            try
            {
                return SubCategoryDALobj.Edit(SubCategoryBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                SubCategoryDALobj = null;
            }
        }

       /// <summary>
       /// To Get ALL Sub Category
       /// </summary>
       /// <param name="categoryID"></param>
       /// <returns></returns>
        public SubCategoryList GetALLSubCategory(int categoryID)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer
            return SubCategoryDALobj.GetALLSubCategory(categoryID);
        }

       /// <summary>
        /// To Get Sub Category
       /// </summary>
       /// <returns></returns>
        public SubCategoryList GetSubCategory()
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer
            return SubCategoryDALobj.GetSubCategory();
        }

       /// <summary>
        /// To To Get Sub Category By Id
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <returns></returns>
        public SubCategoryBO GetSubCategoryById(int SubCATEGORYID)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer
            return SubCategoryDALobj.GetSubCategoryById(SubCATEGORYID);
        }

       /// <summary>
        /// To Delete Sub Category
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <returns></returns>
        public string DeleteSubCategory(int SubCATEGORYID)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer
            return SubCategoryDALobj.DeleteSubCategory(SubCATEGORYID);
        }

       /// <summary>
        /// To Obsolete Sub Category
       /// </summary>
       /// <param name="SubCATEGORYID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteSubCategory(int SubCATEGORYID, string IsDeleted)
        {
            SubCategoryDAL SubCategoryDALobj = new SubCategoryDAL(); //Data pass -to Database Layer
            return SubCategoryDALobj.ObsoleteSubCategory(SubCATEGORYID, IsDeleted);
        }

    }
}
