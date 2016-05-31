using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class TypeBO
    
    {

        private int _LND_TYPEID;

        public int LND_TYPEID
        {
            get { return _LND_TYPEID; }
            set { _LND_TYPEID = value; }
        }

        private string _LandType;

        public string LandType
        {
            get { return _LandType; }
            set { _LandType = value; }
        }
    }
}
