using WIS_BusinessObjects;
using WIS_DataAccess;
using System;

namespace WIS_BusinessLogic
{
   public class NatureofFinancingBLL
    {
       /// <summary>
        /// To Insert into Database
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Insert(NatureofFinancingBO BOobj)
       {
           NatureofFinancingDAL NatureofFinancingDALobj = new NatureofFinancingDAL(); //Data pass -to Database Layer

           try 
           {
               return NatureofFinancingDALobj.Insert(BOobj);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               NatureofFinancingDALobj = null;
           }
       }

       /// <summary>
       /// To Get All Nature Finance
       /// </summary>
       /// <returns></returns>
       public NatureofFinancingList GetAllNatureFinance()
       {
           NatureofFinancingDAL NatureofFinancingDALobj = new NatureofFinancingDAL(); //Data pass -to Database Layer
           return NatureofFinancingDALobj.GetAllNatureFinance();
       }

       /// <summary>
       /// To Get Nature All finance
       /// </summary>
       /// <param name="financeNature"></param>
       /// <returns></returns>
       public NatureofFinancingList GetnatureAllfinance(string financeNature)
       {
           NatureofFinancingDAL NatureofFinancingDALobj = new NatureofFinancingDAL();
           return NatureofFinancingDALobj.GetnatureAllfinance(financeNature);
       }

       /// <summary>
       /// To Get Nature Of Finance
       /// </summary>
       /// <returns></returns>
       public NatureofFinancingList GetNatureOfFinance()
       {
           NatureofFinancingDAL NatureofFinancingDALobj = new NatureofFinancingDAL(); //Data pass -to Database Layer
           return NatureofFinancingDALobj.GetNatureOfFinance();
       }

       /// <summary>
       /// To Obsolete Fcond
       /// </summary>
       /// <param name="FNatureId"></param>
       /// <param name="ISDELETED"></param>
       /// <returns></returns>
       public string ObsoleteFcond(int FNatureId, string ISDELETED)
       {
           NatureofFinancingDAL NatureofFinancingDALobj = new NatureofFinancingDAL(); //Data pass -to Database Layer
           return NatureofFinancingDALobj.ObsoleteFcond(FNatureId, ISDELETED);
       }

       /// <summary>
       /// To Update
       /// </summary>
       /// <param name="BOobj"></param>
       /// <returns></returns>
       public string Update(NatureofFinancingBO BOobj)
       {
           NatureofFinancingDAL DALobj = new NatureofFinancingDAL(); //Data pass -to Database Layer

           try
           {
               return DALobj.Update(BOobj);
           }
           catch(Exception ex)
           {
               throw ex;
           }
           finally
           {
               DALobj = null;
           }
       }

       /// <summary>
       /// To Get Nature Finance ID
       /// </summary>
       /// <param name="NatureFinanceID"></param>
       /// <returns></returns>
       public NatureofFinancingBO GetNatureFinanceID(int NatureFinanceID)
       {
           NatureofFinancingDAL DALobj = new NatureofFinancingDAL();
           return DALobj.GetNatureFinanceID(NatureFinanceID);
       }

       /// <summary>
       /// To Delete Nature Finance
       /// </summary>
       /// <param name="NatureFinanceID"></param>
       /// <returns></returns>
       public string DeleteNatureFinance(int NatureFinanceID)
       {
           NatureofFinancingDAL DALobj = new NatureofFinancingDAL();
           return DALobj.DeleteNatureFinance(NatureFinanceID);
       }

    }
}
