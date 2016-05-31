using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;
namespace WIS_BusinessLogic
{
   public class HIVContractedBLL
    {
       /// <summary>
        /// To Get ALL HIV Contracted
       /// </summary>
       /// <returns></returns>
       public HIVContractedList GetALLHIVContracted()
       {
           return (new HIVContractedDAL()).GetALLHIVContracted();
           //return null; ;
       }

       /// <summary>
       /// To Get HIV Contracted
       /// </summary>
       /// <returns></returns>
       public HIVContractedList GetHIVContracted()
       {
           return (new HIVContractedDAL()).GetHIVContracted();
           //return null; ;
       }

       /// <summary>
       /// To Get Contracted ID
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <returns></returns>
       public HIVContractedBO GetContractedID(int ContractedID)
       {
           HIVContractedDAL HIVContractedDALobj = new HIVContractedDAL(); //Data pass -to Database Layer
           return HIVContractedDALobj.GetContractedID(ContractedID);
       }

       /// <summary>
       /// To Insert HIVC
       /// </summary>
       /// <param name="objHIVContracted"></param>
       /// <returns></returns>
       public string insertHIVC(HIVContractedBO objHIVContracted)
       {
           HIVContractedDAL HIVContractedDALobj = new HIVContractedDAL(); //Data pass -to Database Layer

           try
           {
               return HIVContractedDALobj.insertHIVC(objHIVContracted); 
              
           }
           catch
           {
               throw;
           }
           finally
           {
               HIVContractedDALobj = null;
           }

       }
       
       /// <summary>
       /// To EDIT HIVC
       /// </summary>
       /// <param name="objHIVContracted"></param>
       /// <returns></returns>
       public string EDITHIVC(HIVContractedBO objHIVContracted)
       {
           HIVContractedDAL HIVContractedDALobj = new HIVContractedDAL(); //Data pass -to Database Layer

           try
           {
               return HIVContractedDALobj.EDITHIVC(objHIVContracted);
           }
           catch
           {
               throw;
           }
           finally
           {
               HIVContractedDALobj = null;
           }
       }

       /// <summary>
       /// To Delete HIVC
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <returns></returns>
       public string DeleteHIVC(int ContractedID)
       {
           HIVContractedDAL HIVContractedDALobj = new HIVContractedDAL(); //Data pass -to Database Layer
           return HIVContractedDALobj.DeleteHIVC(ContractedID);
       }

       /// <summary>
       /// To Obsolete HIVC
       /// </summary>
       /// <param name="ContractedID"></param>
       /// <param name="IsDeleted"></param>
       /// <param name="updatedBy"></param>
       /// <returns></returns>
       public string ObsoleteHIVC(int ContractedID, string IsDeleted, int updatedBy)
       {
           return (new HIVContractedDAL()).ObsoleteHIVC(ContractedID, IsDeleted, updatedBy);
       }
    }
}
