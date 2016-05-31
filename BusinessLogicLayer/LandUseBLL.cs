using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
  public  class LandUseBLL
    {
      /// <summary>
        /// To Get Land Use
      /// </summary>
      /// <returns></returns>
      public LandUseList GetLandUse()
      {
          LandUseDAL objLandUseDAL = new LandUseDAL();
          return objLandUseDAL.GetLandUse();
      }

    }
}
