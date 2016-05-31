using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LoanBLL
    {
        /// <summary>
        /// To Get Loan
        /// </summary>
        /// <param name="LoanName"></param>
        /// <returns></returns>
        public LoanList GetLoan(string LoanName)
        {
            LoanDAL objLoanDAL = new LoanDAL();
            return objLoanDAL.GetLoan(LoanName);
        }

        /// <summary>
        /// To Add Loan
        /// </summary>
        /// <param name="objLoan"></param>
        /// <returns></returns>
        public string AddLoan(LoanBO objLoan)
        {
            LoanDAL objLoanDAL = new LoanDAL();
            return objLoanDAL.AddLoan(objLoan);
        }

        /// <summary>
        /// To Delete Loan
        /// </summary>
        /// <param name="EncumbranceId"></param>
        /// <returns></returns>
        public string DeleteLoan(int EncumbranceId)
        {
            LoanDAL objLoanDAL = new LoanDAL();
            return objLoanDAL.DeleteLoan(EncumbranceId);
        }

        /// <summary>
        /// To Update Loan
        /// </summary>
        /// <param name="objLoan"></param>
        /// <returns></returns>
        public string UpdateLoan(LoanBO objLoan)
        {
            LoanDAL objLoanDAL = new LoanDAL();
            return objLoanDAL.UpdateLoan(objLoan);
        }

        /// <summary>
        /// To Get Loan By Loan ID
        /// </summary>
        /// <param name="EncumbranceID"></param>
        /// <returns></returns>
        public LoanBO GetLoanByLoanID(int EncumbranceID)
        {
            LoanDAL objLoanDAL = new LoanDAL();
            return objLoanDAL.GetLoanByLoanID(EncumbranceID);
        }

        /// <summary>
        /// To Obsolete Loan
        /// </summary>
        /// <param name="EncumbranceID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteLoan(int EncumbranceID, string IsDeleted)
        {
            return (new LoanDAL()).ObsoleteLoan(EncumbranceID, IsDeleted);
        }
    }
}

