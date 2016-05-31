using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
 public   class TypeBLL
    {
     /// <summary>
     /// To Get Land Type
     /// </summary>
     /// <returns></returns>
        public TypeList GetLandType()
        {
            TypeDAL objTypeDAL = new TypeDAL();
            return objTypeDAL.GetLandType();
        }
    }
}
