using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ModeofPaymentBLL
    {
        /// <summary>
        /// To Insert Mode of Payment
        /// </summary>
        /// <param name="ModeofPaymentBOObj"></param>
        /// <returns></returns>
        public string InsertModeofPayment(ModeofPaymentBO ModeofPaymentBOObj)
        {
            ModeofPaymentDAL ModeofPaymentDAL = new ModeofPaymentDAL(); //Data pass -to Database Layer

            try
            {
                return ModeofPaymentDAL.InsertModeofPayment(ModeofPaymentBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                ModeofPaymentDAL = null;
            }
        }

        // serach the data from the Database Mst_Concern
        /// <summary>
        /// To Get Mode of Payment
        /// </summary>
        /// <returns></returns>
        public ModeofPaymentList GetModeofPayment()
        {
            ModeofPaymentDAL ModeofPaymentDALObj = new ModeofPaymentDAL();
            return ModeofPaymentDALObj.GetModeofPayment();
        }

        /// <summary>
        /// To Obsolete Mode of Payment
        /// </summary>
        /// <param name="ModeofPaymentID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteModeofPayment(int ModeofPaymentID, string IsDeleted)
        {
            ModeofPaymentDAL ModeofPaymentDALObj = new ModeofPaymentDAL();
            return ModeofPaymentDALObj.ObsoleteModeofPayment(ModeofPaymentID, IsDeleted);
        }

        /// <summary>
        /// To Delete Mode of Payment
        /// </summary>
        /// <param name="ModeofPaymentID"></param>
        /// <returns></returns>
        public string DeleteModeofPayment(int ModeofPaymentID)
        {
            ModeofPaymentDAL ModeofPaymentDALObj = new ModeofPaymentDAL();
            return ModeofPaymentDALObj.DeleteModeofPayment(ModeofPaymentID);
        }

        //Search the Singal Data by passing ID
        /// <summary>
        /// To Get Mode of PaymentID
        /// </summary>
        /// <param name="ModeofPaymentID"></param>
        /// <returns></returns>
        public ModeofPaymentBO GetModeofPaymentID(int ModeofPaymentID)
        {
            ModeofPaymentDAL ModeofPaymentDALObj = new ModeofPaymentDAL();
            return ModeofPaymentDALObj.GetModeofPaymentID(ModeofPaymentID);
        }

        /// <summary>
        /// To EDIT MODE OF PAYMENT
        /// </summary>
        /// <param name="ModeofPaymentBOObj"></param>
        /// <returns></returns>
        public string EDITMODEOFPAYMENT(ModeofPaymentBO ModeofPaymentBOObj)
        {
            ModeofPaymentDAL ModeofPaymentDALObj = new ModeofPaymentDAL();//Data pass -to Database Layer

            try
            {
                return ModeofPaymentDALObj.EDITMODEOFPAYMENT(ModeofPaymentBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                ModeofPaymentDALObj = null;
            }
        }
    }
}
