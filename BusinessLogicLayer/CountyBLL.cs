using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class CountyBLL
    {
      /// <summary>
        /// To Get County
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public CountyList GetCounty(string districtID)
      {
          CountyDAL objcountyDAL = new CountyDAL();
          return objcountyDAL.GetCounty(districtID);
      }

      /// <summary>
      /// To Get All Counties
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public CountyList GetAllCounties(int districtID)
      {
          CountyDAL CountyDALobj = new CountyDAL();
          return CountyDALobj.GetAllCounties(districtID);
      }

      /// <summary>
      /// To Get Counties
      /// </summary>
      /// <param name="County"></param>
      /// <returns></returns>
      public CountyList GetCounties( string County)
      {
          CountyDAL CountyDALobj = new CountyDAL();
          return CountyDALobj.GetCounties(County);
      }

      /// <summary>
      /// To Add County
      /// </summary>
      /// <param name="CountyBOobj"></param>
      /// <returns></returns>
      public string AddCounty(CountyBO CountyBOobj)
      {
          return (new CountyDAL()).AddCounty(CountyBOobj);
      }

      /// <summary>
      /// To Update County
      /// </summary>
      /// <param name="CountyBOobj"></param>
      /// <returns></returns>
      public string UpdateCounty(CountyBO CountyBOobj)
      {
          return (new CountyDAL()).UpdateCounty(CountyBOobj);
      }

      /// <summary>
      /// To Delete County
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public string DeleteCounty(int countyID)
      {
          return (new CountyDAL()).DeleteCounty(countyID);
      }

      /// <summary>
      /// To Obsolete County
      /// </summary>
      /// <param name="countyID"></param>
      /// <param name="isDeleted"></param>
      /// <param name="updatedBy"></param>
      /// <returns></returns>
      public string ObsoleteCounty(int countyID, string isDeleted, int updatedBy)
      {
          return (new CountyDAL()).ObsoleteCounty(countyID, isDeleted, updatedBy);
      }

      /// <summary>
      /// To Get County By Id
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public CountyBO GetCountyById(int countyID)
      {
          return (new CountyDAL()).GetCountyById(countyID);
      }

    }
}
