using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using System.Data;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
   public class MonthlyProjectExpenditureBLL
    {
       /// <summary>
        /// To Load Project Code
       /// </summary>
       /// <returns></returns>
        public List<MonthlyProjectExpenditureBO> LoadProjectCode()
        {
            MonthlyProjectExpenditureDAL MonthlyProjectExpenditureDALObj=new MonthlyProjectExpenditureDAL();
            return MonthlyProjectExpenditureDALObj.LoadProjectCode();
        }
    }
}
