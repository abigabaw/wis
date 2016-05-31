using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class FinalValuationBLL
    {
        FinalValuationDAL oFinalValuationDAL;
        /// <summary>
        /// To get Final Valuatin
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public FinalValuationBO getFinalValuatin(int HHID)
        {
            oFinalValuationDAL = new FinalValuationDAL();
            return oFinalValuationDAL.getFinalValuation(HHID);
        }

        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="Finalvaluationobj"></param>
        /// <returns></returns>
        public int Insert(FinalValuationBO Finalvaluationobj)
        {
            FinalValuationDAL FinalValuationDALobj = new FinalValuationDAL(); //Data pass -to Database Layer

            try
            {
                return FinalValuationDALobj.Insert(Finalvaluationobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                FinalValuationDALobj = null;
            }
        }

        /// <summary>
        /// To Approval Change request Status
        /// </summary>
        /// <param name="objFinalValuationBO"></param>
        /// <returns></returns>
        public FinalValuationBO ApprovalChangerequestStatus(FinalValuationBO objFinalValuationBO)
        {
            FinalValuationDAL FinalValuationDALobj = new FinalValuationDAL(); //Data pass -to Database Layer
            return FinalValuationDALobj.ApprovalChangerequestStatus(objFinalValuationBO);
        }

        /// <summary>
        /// To Save Negotiated Amount
        /// </summary>
        /// <param name="objFinalValuationBO"></param>
        /// <returns></returns>
        public int SaveNogotiatedAmount(FinalValuationBO objFinalValuationBO)
        {
            FinalValuationDAL FinalValuationDALobj = new FinalValuationDAL(); //Data pass -to Database Layer
            return FinalValuationDALobj.SaveNogotiatedAmount(objFinalValuationBO);
          
        }

        /// <summary>
        /// To Save Negotiated Amount Individual
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="NegotiatedAmount"></param>
        /// <param name="NegType"></param>
        /// <returns></returns>
        public int SaveNogotiatedAmountIndividual(int HHID, decimal NegotiatedAmount, string NegType)
        {
            FinalValuationDAL FinalValuationDALobj = new FinalValuationDAL(); //Data pass -to Database Layer
            return FinalValuationDALobj.SaveNogotiatedAmountIndividual(HHID, NegotiatedAmount, NegType);
        }

        /// <summary>
        /// To get Neg Ind Valuation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public FinalValuationBO getNegIndValuation(int HHID)
        {
            oFinalValuationDAL = new FinalValuationDAL();
            return oFinalValuationDAL.getNegIndValuation(HHID);
        }
    }
}
