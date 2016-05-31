using WIS_BusinessObjects;
using WIS_DataAccess;
using System;

namespace WIS_BusinessLogic
{
   public class FinanceConditionBLL
    {
       /// <summary>
        /// To Insert
       /// </summary>
       /// <param name="FinanceConditionBOobj"></param>
       /// <returns></returns>
       public string Insert(FinanceConditionBO FinanceConditionBOobj)
       {
           FinanceConditionDAL FinanceConditionDALobj = new FinanceConditionDAL(); //Data pass -to Database Layer

           try
           {
               return FinanceConditionDALobj.Insert(FinanceConditionBOobj);
           }
           catch(Exception ex)
           {
               throw ex;
           }
           finally
           {
               FinanceConditionDALobj = null;
           }
       }

       /// <summary>
       /// To Obsolete Finance Condition
       /// </summary>
       /// <param name="FcondId"></param>
       /// <param name="ISDELETED"></param>
       /// <returns></returns>
       public string ObsoleteFcond(int FcondId, string ISDELETED)
       {
           FinanceConditionDAL DALobj = new FinanceConditionDAL();
           return DALobj.ObsoleteFcond(FcondId, ISDELETED);
       }

       /// <summary>
       /// To Get All Finance Conditions
       /// </summary>
       /// <returns></returns>
       public FinanceConditionList GetAllFinanceConditions()
       {
           FinanceConditionDAL FinanceConditionDALobj = new FinanceConditionDAL();
           return FinanceConditionDALobj.GetAllFinanceConditions();
       }

       /// <summary>
       /// To Get Finance Condition
       /// </summary>
       /// <returns></returns>
       public FinanceConditionList GetFinanceCondition()
       {
           FinanceConditionDAL FinanceConditionDALobj = new FinanceConditionDAL();
           return FinanceConditionDALobj.GetFinanceCondition();
       }

       /// <summary>
       /// To Get finance Condition ID
       /// </summary>
       /// <param name="financeConditionID"></param>
       /// <returns></returns>
       public FinanceConditionBO GetfinanceConditionID(int financeConditionID)
       {
           FinanceConditionDAL DALobj = new FinanceConditionDAL();
           return DALobj.GetfinanceConditionID(financeConditionID);
       }

       /// <summary>
       /// To Delete Finance Condition
       /// </summary>
       /// <param name="financeConditionId"></param>
       /// <returns></returns>
       public string DeleteFinanceCondition(int financeConditionId)
       {
           FinanceConditionDAL DALobj = new FinanceConditionDAL();
           return DALobj.DeleteFinanceCondition(financeConditionId);
       }

       public string Update(FinanceConditionBO FinanceConditionBOobj)
       {
           FinanceConditionDAL DALobj = new FinanceConditionDAL(); //Data pass -to Database Layer

           try
           {
               return DALobj.Update(FinanceConditionBOobj);
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
