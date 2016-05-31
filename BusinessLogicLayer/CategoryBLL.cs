using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class CategoryBLL
    {
       /// <summary>
       /// To insert category details to database
       /// </summary>
       /// <param name="CategoryBOobj"></param>
       /// <returns></returns>
       public string Insert(CategoryBO CategoryBOobj)
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer

            try
            {
                return CategoryDALobj.Insert(CategoryBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                CategoryDALobj = null;
            }
        }
       /// <summary>
       /// To update category details into database
       /// </summary>
       /// <param name="CategoryBOobj"></param>
       /// <returns></returns>
       public string Edit(CategoryBO CategoryBOobj)
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer

            try
            {
                return CategoryDALobj.Edit(CategoryBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                CategoryDALobj = null;
            }
        }
       /// <summary>
       /// To Get ALL Category details
       /// </summary>
       /// <returns></returns>
       public CategoryList GetALLCategory()
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer
            return CategoryDALobj.GetALLCategory();
        }
       /// <summary>
       /// To GetCategory details from database
       /// </summary>
       /// <returns></returns>
       public CategoryList GetCategory()
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer
            return CategoryDALobj.GetCategory();
        }
       /// <summary>
       /// To get category details based on ID from database
       /// </summary>
       /// <param name="CATEGORYID"></param>
       /// <returns></returns>
       public CategoryBO GetCategoryById(int CATEGORYID)
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer
            return CategoryDALobj.GetCategoryById(CATEGORYID);
        }
       /// <summary>
       /// To delete Category details
       /// </summary>
       /// <param name="CATEGORYID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
       public string DeleteCategory(int CATEGORYID)
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer
            return CategoryDALobj.DeleteCategory(CATEGORYID);
        }
       /// <summary>
       /// To make category details obsolete
       /// </summary>
       /// <param name="CATEGORYID"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
       public string ObsoleteCategory(int CATEGORYID, string IsDeleted)
        {
            CategoryDAL CategoryDALobj = new CategoryDAL(); //Data pass -to Database Layer
            return CategoryDALobj.ObsoleteCategory(CATEGORYID, IsDeleted);
        }
    }
}
