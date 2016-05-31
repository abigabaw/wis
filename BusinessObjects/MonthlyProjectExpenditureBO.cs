using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class MonthlyProjectExpenditureBO
    {
        private int _ProjectCodeID;

        public int ProjectCodeID
        {
            get { return _ProjectCodeID; }
            set { _ProjectCodeID = value; }
        }
        private string _ProjectCode;

        public string ProjectCode
        {
            get { return _ProjectCode; }
            set { _ProjectCode = value; }
        }
    }
}
