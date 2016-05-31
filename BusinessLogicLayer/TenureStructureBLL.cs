using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class TenureStructureBLL
    {
        /// <summary>
        /// To Get Tenure Structures
        /// </summary>
        /// <param name="TenureStructureName"></param>
        /// <returns></returns>
        public TenureStructureList GetTenureStructures(string TenureStructureName)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.GetTenureStructures(TenureStructureName);
        }

        /// <summary>
        /// To Get All Tenure Structures
        /// </summary>
        /// <param name="TenureStructureName"></param>
        /// <returns></returns>
        public TenureStructureList GetAllTenureStructures(string TenureStructureName)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.GetAllTenureStructures(TenureStructureName);
        }

        /// <summary>
        /// To Add Tenure Structure
        /// </summary>
        /// <param name="objTenureStructure"></param>
        /// <returns></returns>
        public string AddTenureStructure(TenureStructureBO objTenureStructure)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.AddTenureStructure(objTenureStructure);
        }

        /// <summary>
        /// To Delete Tenure Structure
        /// </summary>
        /// <param name="TenureStructureID"></param>
        /// <returns></returns>
        public string DeleteTenureStructure(int TenureStructureID)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.DeleteTenureStructure(TenureStructureID);
        }

        /// <summary>
        /// To Update Tenure Structure
        /// </summary>
        /// <param name="objTenureStructure"></param>
        /// <returns></returns>
        public string UpdateTenureStructure(TenureStructureBO objTenureStructure)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.UpdateTenureStructure(objTenureStructure);
        }

        /// <summary>
        /// To Get Tenure Structure Item
        /// </summary>
        /// <param name="TenureStructureID"></param>
        /// <returns></returns>
        public TenureStructureBO GetTenureStructureItem(int TenureStructureID)
        {
            TenureStructureDAL objTenureStructureDAL = new TenureStructureDAL();
            return objTenureStructureDAL.GetTenureStructureItem(TenureStructureID);
        }

        /// <summary>
        /// To Obsolete Tenure Structure
        /// </summary>
        /// <param name="TenureStructureID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteTenureStructure(int TenureStructureID, string IsDeleted)
        {
            return (new TenureStructureDAL()).ObsoleteTenureStructure(TenureStructureID, IsDeleted);
        }
    }
}

