using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class StructureConditionBLL
    {
        #region Declaration Scetion
        StructureConditionDAL objStructureConditionDAL;
        #endregion

        #region Get Record(s)
        /// <summary>
        /// To Get All Structure Condition
        /// </summary>
        /// <returns></returns>
        public StructureConditionList GetAllStructureCondition()//(StructureCondition oStructureCondition)
        {
            objStructureConditionDAL = new StructureConditionDAL();
            return objStructureConditionDAL.GetAllStructureCondition();// (oStructureCondition);
        }

        /// <summary>
        /// To Get Structure Condition
        /// </summary>
        /// <returns></returns>
        public StructureConditionList GetStructureCondition()//(StructureCondition oStructureCondition)
        {
            objStructureConditionDAL = new StructureConditionDAL();
            return objStructureConditionDAL.GetStructureCondition();// (oStructureCondition);
        }

        /// <summary>
        /// To Get Structure Condition By Id
        /// </summary>
        /// <param name="StructureConditionID"></param>
        /// <returns></returns>
        public StructureConditionBO GetStructureConditionById(int StructureConditionID)
        {
            objStructureConditionDAL = new StructureConditionDAL();
            return objStructureConditionDAL.GetStructureConditionById(StructureConditionID);
        }
        #endregion

        #region Add, Update & Delete Record(s)
        /// <summary>
        /// To Add Structure Condition
        /// </summary>
        /// <param name="oStructureCondition"></param>
        /// <returns></returns>
        public string AddStructureCondition(StructureConditionBO oStructureCondition)
        {
            objStructureConditionDAL = new StructureConditionDAL();

            return objStructureConditionDAL.SaveStructureCondition(oStructureCondition);
        }

        /// <summary>
        /// To Update Structure Condition
        /// </summary>
        /// <param name="oStructureCondition"></param>
        /// <returns></returns>
        public string UpdateStructureCondition(StructureConditionBO oStructureCondition)
        {
            objStructureConditionDAL = new StructureConditionDAL();

            return objStructureConditionDAL.UpdateStructureCondition(oStructureCondition);
        }

        /// <summary>
        /// To Obsolete Structure Condition
        /// </summary>
        /// <param name="StructureConditionID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteStructureCondition(int StructureConditionID, string IsDeleted)
        {
            objStructureConditionDAL = new StructureConditionDAL();
            return objStructureConditionDAL.ObsoleteStructureCondition(StructureConditionID, IsDeleted);
        }

        /// <summary>
        /// To Delete Structure Condition
        /// </summary>
        /// <param name="StructureConditionID"></param>
        /// <returns></returns>
        public string DeleteStructureCondition(int StructureConditionID)
        {
            objStructureConditionDAL = new StructureConditionDAL();
            return objStructureConditionDAL.DeleteStructureCondition(StructureConditionID);
        }
        #endregion
    }
}