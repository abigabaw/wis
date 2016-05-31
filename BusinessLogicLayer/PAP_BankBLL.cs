using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_BankBLL
    {
        /// <summary>
        /// To Get PAP Bank By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_BankBO GetPAPBankByID(int householdID)
        {
            return (new PAP_BankDAL()).GetPAPBankByID(householdID);
        }

        /// <summary>
        /// To Update PAP Bank
        /// </summary>
        /// <param name="objPAPBank"></param>
        public void UpdatePAPBank(PAP_BankBO objPAPBank)
        {
            (new PAP_BankDAL()).UpdatePAPBank(objPAPBank);
        }

        /// <summary>
        /// To Delete PAP Bank
        /// </summary>
        /// <param name="HHID"></param>
        public void DeletePAPBank(int HHID)
        {
            (new PAP_BankDAL()).DeletePAPBank(HHID);
        }
    }
}