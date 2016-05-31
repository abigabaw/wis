using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class VillageBLL
    {
      /// <summary>
        /// To Get Village
      /// </summary>
      /// <param name="subCountyID"></param>
      /// <returns></returns>
      public VillageList GetVillage(string subCountyID)
      {
          VillageDAL objVilDAL = new VillageDAL();
          return objVilDAL.GetVillage(subCountyID);
      }

      /// <summary>
      /// To Search Village
      /// </summary>
      /// <param name="val"></param>
      /// <returns></returns>
      public VillageList SearchVillage(string val)
      {
          VillageDAL objVilDAL = new VillageDAL();
          return objVilDAL.SearchVillage(val);
      }

      /// <summary>
      /// To Get All Village
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public VillageList GetAllVillage(int SUBCOUNTYID)
      {
          VillageDAL objVilDAL = new VillageDAL();
          return objVilDAL.GetAllVillage(SUBCOUNTYID);
      }

      /// <summary>
      /// To Add Village
      /// </summary>
      /// <param name="objVillageBO"></param>
      /// <returns></returns>
      public string AddVillage(VillageBO objVillageBO)
      {
          return (new VillageDAL()).AddVillage(objVillageBO);
      }

      /// <summary>
      /// To Update Village
      /// </summary>
      /// <param name="objVillageBO"></param>
      /// <returns></returns>
      public string UpdateVillage(VillageBO objVillageBO)
      {
          return (new VillageDAL()).UpdateVillage(objVillageBO);
      }

      /// <summary>
      /// To Delete Village
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <returns></returns>
      public string DeleteVillage(int VILLAGEID)
      {
          return (new VillageDAL()).DeleteVillage(VILLAGEID);
      }

      /// <summary>
      /// To Obsolete Village
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <param name="ISDELETED"></param>
      /// <param name="UPDATEDBY"></param>
      /// <returns></returns>
      public string ObsoleteVillage(int VILLAGEID, string ISDELETED, int UPDATEDBY)
      {
          return (new VillageDAL()).ObsoleteVillage(VILLAGEID, ISDELETED, UPDATEDBY);
      }

      /// <summary>
      /// To Get Village By Id
      /// </summary>
      /// <param name="VILLAGEID"></param>
      /// <returns></returns>
      public VillageBO GetVillageById(int VILLAGEID)
      {
          return (new VillageDAL()).GetVillageById(VILLAGEID);
      }

    }
}
