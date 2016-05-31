using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PaymentBLL
    {
        #region Declaration Scetion
        PaymentDAL oPaymentDAL;
        #endregion

        #region DEFINE CONSTANTS
       
        #endregion

        #region Get Payment Record(s)
        /// <summary>
        /// To Get Mode Of Payment
        /// </summary>
        /// <param name="PaymentType"></param>
        /// <returns></returns>
        public PaymentList GetModeOfPayment(string PaymentType)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.GetModeOfPayment(PaymentType);
        }
      
        #endregion

        #region Compnesation Payement
        /// <summary>
        /// To get Compnesation Payment
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationPayementList getCompnesationPayment(int HHID)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.getCompensationPayment(HHID);
        }

        /// <summary>
        /// To get Compensation Payment Export
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public PaymentExportList getCompensationPaymentExport(int projectID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.getCompensationPaymentExport(projectID, papName, plotReference, district, county, subCounty, parish, village);
        }

        /// <summary>
        /// TO get Compensation Payment By Id
        /// </summary>
        /// <param name="CompensationPaymentID"></param>
        /// <returns></returns>
        public PaymentBO.CompensationPayementBO getCompensationPaymentById(int CompensationPaymentID)
        {
            PaymentDAL oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.getCompensationPaymentById(CompensationPaymentID);
        }

        /// <summary>
        /// To Add Compnesation Payment
        /// </summary>
        /// <param name="oCompensationPayementBO"></param>
        /// <returns></returns>
        public string[] AddCompnesationPayment(PaymentBO.CompensationPayementBO oCompensationPayementBO)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.AddCompensationPayment(oCompensationPayementBO);
        }

        /// <summary>
        /// To Update Composition Payment
        /// </summary>
        /// <param name="oCompPayementBO"></param>
        /// <returns></returns>
        public string UpdateCompositionPayment(PaymentBO.CompensationPayementBO oCompPayementBO)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.UpdateCompositionPayment(oCompPayementBO);
        }

        #region Compensational Financial
        /// <summary>
        /// To Update Compensation Financial
        /// </summary>
        /// <param name="oCompensationFinancialBO"></param>
        /// <returns></returns>
        public string UpdateCompensationFinancial(CompensationFinancialBO oCompensationFinancialBO)//(int HHID,string ResStructInKindCompensation,decimal FacilitationAllowance,int UpdatedBy)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.UpdateCompensationFinancial(oCompensationFinancialBO);
        }

        /// <summary>
        /// To get Compnesation Financial
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialBO getCompnesationFinancial(int HHID)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.getCompnesationFinancial(HHID);
        }
        #endregion

        /// <summary>
        /// To Delete Composition Payment
        /// </summary>
        /// <param name="CompPaymentId"></param>
        /// <returns></returns>
        public int DeleteCompositionPayment(int CompPaymentId)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.DeleteCompositionPayment(CompPaymentId);
        }

        /// <summary>
        /// To Get Compensation Financial List
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationFinancialList GetCompensationFinancialList(int HHID)
        {
            CompensationFinancialDAL CompensationFinancialDALObj = new CompensationFinancialDAL();
            return CompensationFinancialDALObj.GetCompensationFinancialList(HHID);// (tribeID);
        }

        /// <summary>
        /// To Send for Approval
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public int SendforApproval(int HHID)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.SendforApproval(HHID);
        }

        /// <summary>
        /// To Upadate Status in Database
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="FUNDREQSTATUS"></param>
        /// <returns></returns>
        public int UpdateStatus(int HHID, string FUNDREQSTATUS)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.UpdateStatus(HHID, FUNDREQSTATUS);
        }
        #endregion

        #region PAP VALUATION SUMMERY (Table) 

        /// <summary>
        /// To Get Pap Valuation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PaymentBO getPapValuation(int HHID)
        {
            //reading Payement Status from the TRN_PAP_VALUATION_SUMMERY table
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.getPapValuation(HHID);
        }

        /// <summary>
        /// To Update Pap Valutaion
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="PaymentStatus"></param>
        /// <returns></returns>
        public string UpdatePapValutaion(int HHID, string PaymentStatus)
        {
            //Updating Payement Status from the TRN_PAP_VALUATION_SUMMERY table
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.UpdatePapValutaion( HHID, PaymentStatus);
        }
        #endregion

        /*
        public BO.BatchBO AddBatch(BO.BatchBO oBatchBO)
        {
            oBatchDAL = new DAL.BatchDAL();

            return oBatchDAL.AddBatch(oBatchBO);
        }

       

     */
        //new 

        /// <summary>
        /// To Get File closing Comments
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PaymentBO GetFileclosingComments(int HHID)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.GetFileclosingComments(HHID);
        }

        /// <summary>
        /// To Save File closing Comments
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="comments"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public string SaveFileclosingComments(int HHID, string comments, Boolean Status)
        {
            oPaymentDAL = new PaymentDAL();
            return oPaymentDAL.SaveFileclosingComments(HHID, comments, Status);
        }
    }
}