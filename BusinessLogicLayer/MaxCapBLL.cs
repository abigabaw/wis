using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public   class MaxCapBLL
  {
      /// <summary>
      /// To Get County
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public MaxCapList GetMaxCapByDist(string ProjectId)
      {
          MaxCapDAL objMaxCapDAL = new MaxCapDAL();
          return objMaxCapDAL.GetMaxCapByDist(ProjectId);
      }

      /// <summary>
      /// To Get All Counties
      /// </summary>
      /// <param name="districtID"></param>
      /// <returns></returns>
      public MaxCapList GetAllMaxCap(int ProjectId)
      {
          MaxCapDAL CountyDALobj = new MaxCapDAL();
          return CountyDALobj.GetAllMaxCap(ProjectId);
      }

      /// <summary>
      /// To Get Counties
      /// </summary>
      /// <param name="County"></param>
      /// <returns></returns>
      public MaxCapList GetMaxCap(string ProjectId)
      {
          MaxCapDAL CountyDALobj = new MaxCapDAL();
          return CountyDALobj.GetMaxCap(ProjectId);
      }

      /// <summary>
      /// To Add County
      /// </summary>
      /// <param name="CountyBOobj"></param>
      /// <returns></returns>
      public string AddMaxCap(MaxCapBO CountyBOobj)
      {
          return (new MaxCapDAL()).AddMaxCap(CountyBOobj);
      }

      /// <summary>
      /// To Update County
      /// </summary>
      /// <param name="CountyBOobj"></param>
      /// <returns></returns>
      public string UpdateMaxCap(MaxCapBO CountyBOobj)
      {
          return (new MaxCapDAL()).UpdateMaxCap(CountyBOobj);
      }

      /// <summary>
      /// To Delete County
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public string DeleteMaxCap(int countyID)
      {
          return (new MaxCapDAL()).DeleteMaxCap(countyID);
      }

      /// <summary>
      /// To Obsolete County
      /// </summary>
      /// <param name="countyID"></param>
      /// <param name="isDeleted"></param>
      /// <param name="updatedBy"></param>
      /// <returns></returns>
      public string ObsoleteMaxCap(int countyID, string isDeleted, int updatedBy)
      {
          return (new MaxCapDAL()).ObsoleteMaxCap(countyID, isDeleted, updatedBy);
      }

      /// <summary>
      /// To Get County By Id
      /// </summary>
      /// <param name="countyID"></param>
      /// <returns></returns>
      public MaxCapBO GetMaxCapById(int countyID)
      {
          return (new MaxCapDAL()).GetMaxCapById(countyID);
      }

  }
}
