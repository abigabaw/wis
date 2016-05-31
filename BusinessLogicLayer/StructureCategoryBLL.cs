using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class StructureCategoryBLL
    {
        #region Declaration Scetion
        StructureCategoryDAL objStructureCategoryDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Category
        /// </summary>
        /// <returns></returns>
        public StructureCategoryList GetAllStructureCategory()
        {
            objStructureCategoryDAL = new StructureCategoryDAL();
            return objStructureCategoryDAL.GetAllStructureCategory();
        }

        /// <summary>
        /// To Get Structure Category
        /// </summary>
        /// <returns></returns>
        public StructureCategoryList GetStructureCategory()
        {
            objStructureCategoryDAL = new StructureCategoryDAL();
            return objStructureCategoryDAL.GetStructureCategory();
        }

        /// <summary>
        /// To Get Structure Category By Id
        /// </summary>
        /// <param name="StructureCategoryID"></param>
        /// <returns></returns>
        public StructureCategoryBO GetStructureCategoryById(int StructureCategoryID)
        {
            objStructureCategoryDAL = new StructureCategoryDAL();
            return objStructureCategoryDAL.GetStructureCategoryById(StructureCategoryID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Structure Category
        /// </summary> 
        /// <param name="oStructureCategory"></param>
        /// <returns></returns>
        public string AddStructureCategory(StructureCategoryBO oStructureCategory)
        {
            objStructureCategoryDAL = new StructureCategoryDAL();

            return objStructureCategoryDAL.SaveStructureCategory(oStructureCategory);
        }

        /// <summary>
        /// To Update Structure Category
        /// </summary>
        /// <param name="oStructureCategory"></param>
        /// <returns></returns>
        public string UpdateStructureCategory(StructureCategoryBO oStructureCategory)
        {
            objStructureCategoryDAL = new StructureCategoryDAL();

            return objStructureCategoryDAL.UpdateStructureCategory(oStructureCategory);
        }

        /// <summary>
        /// To Obsolete Structure Categoty
        /// </summary>
        /// <param name="StructureCategoryID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureCategoty(int StructureCategoryID, string IsDeleted)
        {
            objStructureCategoryDAL = new StructureCategoryDAL();
            return objStructureCategoryDAL.ObsoleteStructureCategory(StructureCategoryID, IsDeleted);
        }

        /// <summary>
        /// To Delete Structure Category
        /// </summary>
        /// <param name="structureCategoryID"></param>
        /// <returns></returns>
        public string DeleteStructureCategory(int structureCategoryID)
        {
            objStructureCategoryDAL = new StructureCategoryDAL();
            return objStructureCategoryDAL.DeleteStructureCategory(structureCategoryID);
        }
        #endregion
    }
}