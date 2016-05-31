using System;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CompensationFinancialBLL
    {
        /// <summary>
        /// To Add Compensation Financial
        /// </summary>
        /// <param name="oCompensationFinancial"></param>
        /// <returns></returns>
        public string AddCompensationFinancial(CompensationFinancialBO oCompensationFinancial)
        {

            CompensationFinancialDAL oCompensationFinancialDAL = new CompensationFinancialDAL();

            try
            {
                return oCompensationFinancialDAL.AddCompensationFinancial(oCompensationFinancial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCompensationFinancial = null;
            }
        }

        /// <summary>
        /// To Update Compensation Financial Closing Info
        /// </summary>
        /// <param name="oCompensationFinancial"></param>
        /// <returns></returns>
        public string UpdateCompFinancial_ClosingInfo(CompensationFinancialBO oCompensationFinancial)
        {
            //From Clsoing Info Summery:
            CompensationFinancialDAL oCompensationFinancialDAL = new CompensationFinancialDAL();

            try
            {
                return oCompensationFinancialDAL.UpdateCompFinancial_ClosingInfo(oCompensationFinancial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oCompensationFinancial = null;
            }
        }

        /// <summary>
        /// To Update Compensation Financial Payment
        /// </summary>
        /// <param name="objCompensationFinancialBO"></param>
        public void UpdateCompensationFinancialPayment(CompensationFinancialBO objCompensationFinancialBO)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            CompensationFinancialDALObj.UpdateCompensationFinancialPayment(objCompensationFinancialBO);
        }

        /// <summary>
        /// To Get Compensation Financial By Id
        /// </summary>
        /// <param name="CompPaymentId"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancialById(int CompPaymentId, int HHID)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            return CompensationFinancialDALObj.GetCompensationFinancialById(CompPaymentId, HHID);
        }

        /// <summary>
        /// To Get Compensation Financial
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancial(int HHID)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            return CompensationFinancialDALObj.GetCompensationFinancial(HHID);
        }

        /// <summary>
        /// To Get Compensation Financial By Id
        /// </summary>
        /// <param name="ClansID"></param>
        /// <returns></returns>
        public CompensationFinancialBO GetCompensationFinancialById(int ClansID)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            return CompensationFinancialDALObj.GetCompensationFinancialByID(ClansID);
        }

        /// <summary>
        /// To Get Compensation Financial List
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialList GetCompensationFinancialList(int HHID)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            return CompensationFinancialDALObj.GetCompensationFinancialList(HHID);
        }

        #region Package Delivery Info
        /// <summary>
        /// To Add Package Delivery Info
        /// </summary>
        /// <param name="oCompensationFinancial"></param>
        /// <returns></returns>
        public string AddPackageDeliveryInfo(CompensationFinancialBO oCompensationFinancial)
        {
            CompensationFinancialDAL oCompensationFinancialDAL = new CompensationFinancialDAL();
            return oCompensationFinancialDAL.AddPackageDeliveryInfo(oCompensationFinancial);
        }

        /// <summary>
        /// To get Package Delivery Info
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO getPackageDeliveryInfo(int HHID)
        {
            CompensationFinancialDAL oCompensationFinancialDAL = new CompensationFinancialDAL();
            return oCompensationFinancialDAL.getPackageDeliveryInfo(HHID);
        }
        #endregion Package Delivery Info
    }
}