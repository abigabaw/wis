using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class NewLocationBLL
    {
        /// <summary>
        /// To Get New Location
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public NewLocationBO GetNewLocation(int HHID)
        {
            return (new NewLocationDAL()).GetNewLocation(HHID);
        }

        //public BankList GetBanks(string bankName, string city, string branchName, string swiftCode)
        //{
        //    return (new NewLocationDAL()).GetBanks(bankName, city, branchName, swiftCode);
        //}

        //public NewLocationBO GetBankByBankID(int bankID)
        //{
        //    return (new NewLocationDAL()).GetBankByBankID(bankID);
        //}

        /// <summary>
        /// To Add New Location
        /// </summary>
        /// <param name="oNewLocationBO"></param>
        /// <returns></returns>
        public string AddNewLocation(NewLocationBO oNewLocationBO)
        {
            return (new NewLocationDAL()).AddNewLocation(oNewLocationBO);
        }

        //public string UpdateBank(NewLocationBO oNewLocationBO)
        //{
        //    return (new NewLocationDAL()).UpdateBank(oNewLocationBO);
        //}

        //public void DeleteBank(int bankID)
        //{
        //    (new NewLocationDAL()).DeleteBank(bankID);
        //}  
    }
}