using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public  class BranchBLL
    {
       /// <summary>
        /// To Get Active Branches from database
       /// </summary>
       /// <param name="bankID"></param>
       /// <returns></returns>
       public BankBranchList GetActiveBranches(int bankID)
        {
            return (new BranchDAL()).GetActiveBranches(bankID);
        }
       /// <summary>
       /// To Get All Branches from database
       /// </summary>
       /// <param name="bankID"></param>
       /// <returns></returns>
       public BankBranchList GetAllBranches(int bankID)
        {
            return (new BranchDAL()).GetAllBranches(bankID);
        }
       /// <summary> 
       /// To Get Branch By Id from database
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <returns></returns>
        public BranchBO GetBranchById(int BankBranchId)
        {
            return (new BranchDAL()).GetBranchByID(BankBranchId);
        }
       /// <summary>
       /// To add branch to database
       /// </summary>
       /// <param name="objBranchBO"></param>
       /// <returns></returns>
        public string AddBranch(BranchBO objBranchBO)
        {
            return (new BranchDAL()).AddBranch(objBranchBO);
        }
       /// <summary>
       /// To update branch to database
       /// </summary>
       /// <param name="objBranchBO"></param>
       /// <returns></returns>
        public string UpdateBranch(BranchBO objBranchBO)
        {
            return (new BranchDAL()).UpdateBranch(objBranchBO);
 
        }
       /// <summary>
        /// To Delete Branch from database
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <returns></returns>
        public string DeleteBranch(int BankBranchId)
        {
            return (new BranchDAL()).DeleteBranch(BankBranchId);
        }
       /// <summary>
       /// To make branch details obsolete
       /// </summary>
       /// <param name="BankBranchId"></param>
       /// <param name="IsDeleted"></param>
       /// <returns></returns>
        public string ObsoleteBranch(int BankBranchId, string IsDeleted)
        {
            return (new BranchDAL()).ObsoleteBranch(BankBranchId, IsDeleted);
        }
    }
}
