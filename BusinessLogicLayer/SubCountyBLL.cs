using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public class SubCountyBLL
    {
      /// <summary>
        /// To Get Sub County
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public SubCountyList GetSubCounty(string countyID)
      {
          SubCountyDAL objDisDAL = new SubCountyDAL();
          return objDisDAL.GetSubCounty(countyID);
      }

      /// <summary>
      /// To Get All Sub Counties
      /// </summary>
      /// <param name="COUNTYID"></param>
      /// <returns></returns>
      public SubCountyList GetAllSubCounties(int COUNTYID)
      {
          SubCountyDAL SubCountyDALobj = new SubCountyDAL();
          return SubCountyDALobj.GetAllSubCounties(COUNTYID);
      }

      /// <summary>
      /// To Add Sub County
      /// </summary>
      /// <param name="objSubCountyBO"></param>
      /// <returns></returns>
      public string AddSubCounty(SubCountyBO objSubCountyBO)
      {
          return (new SubCountyDAL()).AddSubCounty(objSubCountyBO);
      }

      /// <summary>
      /// To Update Sub County
      /// </summary>
      /// <param name="objSubCountyBO"></param>
      /// <returns></returns>
      public string UpdateSubCounty(SubCountyBO objSubCountyBO)
      {
          return (new SubCountyDAL()).UpdateSubCounty(objSubCountyBO);
      }

      /// <summary>
      /// To Delete Sub County
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public string DeleteSubCounty(int SUBCOUNTYID)
      {
          return (new SubCountyDAL()).DeleteSubCounty(SUBCOUNTYID);
      }

      /// <summary>
      /// To Obsolete Sub County
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <param name="ISDELETED"></param>
      /// <param name="UPDATEDBY"></param>
      /// <returns></returns>
      public string ObsoleteSubCounty(int SUBCOUNTYID, string ISDELETED, int UPDATEDBY)
      {
          return (new SubCountyDAL()).ObsoleteSubCounty(SUBCOUNTYID, ISDELETED, UPDATEDBY);
      }

      /// <summary>
      /// To Get Sub County By Id
      /// </summary>
      /// <param name="SUBCOUNTYID"></param>
      /// <returns></returns>
      public SubCountyBO GetSubCountyById(int SUBCOUNTYID)
      {
          return (new SubCountyDAL()).GetSubCountyById(SUBCOUNTYID);
      }

      /// <summary>
      /// To Get Sub Counties
      /// </summary>
      /// <param name="subCounty"></param>
      /// <returns></returns>
      public SubCountyList GetSubCounties(string subCounty)
      {
          return (new SubCountyDAL()).GetSubCounties(subCounty);
      }

    }
}
