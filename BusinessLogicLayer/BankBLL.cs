using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class BankBLL
    {
        /// <summary>
        /// To fetch bank details from database
        /// </summary>
        /// <returns></returns>
        public BankList GetBanks()
        {
            return (new BankDAL()).GetBanks();
        }
        /// <summary>
        /// To fetch bank details from database based on bankname
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public BankList GetAllBanks(string bankName)
        {
            return (new BankDAL()).GetAllBanks(bankName);
        }
        /// <summary>
        /// To Get Bank details  By BankID
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public BankBO GetBankByBankID(int bankID)
        {
            return (new BankDAL()).GetBankByBankID(bankID);
        }
        /// <summary>
        /// To save bank data to database
        /// </summary>
        /// <param name="objBank"></param>
        /// <returns></returns>
        public string AddBank(BankBO objBank)
        {
            return (new BankDAL()).AddBank(objBank);
        }
        /// <summary>
        /// To update bank details into database
        /// </summary>
        /// <param name="objBank"></param>
        /// <returns></returns>
        public string UpdateBank(BankBO objBank)
        {
            return (new BankDAL()).UpdateBank(objBank);
        }
        /// <summary>
        /// To delete bank details from database
        /// </summary>
        /// <param name="bankID"></param>
        /// <returns></returns>
        public string DeleteBank(int bankID)
        {
            return (new BankDAL()).DeleteBank(bankID);
        }
        /// <summary>
        /// To obsolete bank details
        /// </summary>
        /// <param name="bankID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteBank(int bankID, string IsDeleted)
        {
            return (new BankDAL()).ObsoleteBank(bankID, IsDeleted);
        }
    }
}