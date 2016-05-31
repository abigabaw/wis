using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class ReligionBO
    {
        private int religionID = -1;
        private string religionName = String.Empty;

        public int ReligionID
        {
            get
            {
                return religionID;
            }
            set
            {
                religionID = value;
            }
        }

        public string ReligionName
        {
            get
            {
                return religionName;
            }
            set
            {
                religionName = value;
            }
        }
    }
}
