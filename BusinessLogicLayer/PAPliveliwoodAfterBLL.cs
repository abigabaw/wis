using WIS_BusinessObjects;
using System;
using System.Data;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class PAPliveliwoodAfterBLL
    {
        
        /// <summary>
        /// To Get Livelihood Items By ID
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
      public PAPliveliwoodAfterList GetLivelihoodItemsByID(int householdID)
        {
            return (new PAPliveliwoodAfterDAL()).GetLivelihoodItemsByID(householdID);
        }

        /// <summary>
        /// To Update Livelihood
        /// </summary>
        /// <param name="LivelihoodItems"></param>
      public string UpdateLivelihood(PAPliveliwoodAfterList LivelihoodItems)
        {
            return  (new PAPliveliwoodAfterDAL()).UpdateLivelihood(LivelihoodItems);
        }

      /// <summary>
      /// To Get Livelihood Items 
      /// </summary>
      /// <param name="householdID"></param>
      /// <returns></returns>
      public PAPliveliwoodAfterList GetLivelihood()
      {
          return (new PAPliveliwoodAfterDAL()).GetLivelihood();
      }
      public PAPliveliwoodAfterList GetLivelihoodItemsByIDCD(int household, string CaptDate)
      {
          return (new PAPliveliwoodAfterDAL()).GetLivelihoodItemsByIDCD(household, CaptDate);
      }
      public string DeleteLiveliHood(int HHID, string CaptDate)
      {
          return (new PAPliveliwoodAfterDAL()).DeleteLiveliHood(HHID, CaptDate);
 
      }
  
    }
}
