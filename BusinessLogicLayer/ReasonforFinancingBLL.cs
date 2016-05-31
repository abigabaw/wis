using WIS_BusinessObjects;
using WIS_DataAccess;
using System;


namespace WIS_BusinessLogic
{
   public class ReasonforFinancingBLL
    {
       /// <summary>
        /// To Insert
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Insert(ReasonforFinancingBO BOobj)
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(BOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

       /// <summary>
       /// To Obsolete Reason For Fin
       /// </summary>
       /// <param name="ReasonForFinanceId"></param>
       /// <param name="ISDELETED"></param>
       /// <returns></returns>
       public string ObsoleteReasonForFin(int ReasonForFinanceId, string ISDELETED)
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer
            return DALobj.ObsoleteReasonForFin(ReasonForFinanceId, ISDELETED);
        }

       /// <summary>
       /// To Get All Reason For Finance
       /// </summary>
       /// <param name="financereason"></param>
       /// <returns></returns>
       public ReasonforFinancingList GetAllReasonForFinance(string financereason)
       {
              ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL();
              return DALobj.GetAllReasonForFinance(financereason);
       }

       /// <summary>
       /// To Get Reason For Finance
       /// </summary>
       /// <returns></returns>
       public ReasonforFinancingList GetReasonForFinance()
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer
            return DALobj.GetReasonForFinance();
        }

       /// <summary>
       /// To Get Reason For Finance ID
       /// </summary>
       /// <param name="ReasonForFinanceID"></param>
       /// <returns></returns>
       public ReasonforFinancingBO GetReasonForFinanceID(int ReasonForFinanceID)
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer
            return DALobj.GetReasonForFinanceID(ReasonForFinanceID);
        }

       /// <summary>
       /// To Delete For Finance ID
       /// </summary>
       /// <param name="ReasonForFinanceID"></param>
       /// <returns></returns>
       public string DeleteReasonForFinance(int ReasonForFinanceID)
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer
            return DALobj.ReasonForFinanceID(ReasonForFinanceID);
        }

       /// <summary>
       /// To Update
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Update(ReasonforFinancingBO BOobj)
        {
            ReasonforFinancingDAL DALobj = new ReasonforFinancingDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(BOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                DALobj = null;
            }
        }
    }
}
