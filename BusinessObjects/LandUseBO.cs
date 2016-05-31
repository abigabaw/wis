using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
  public  class LandUseBO

    {

      private int _LND_USEID;
      private string _LANDUSE;

      public int LND_USEID
        {
            get { return _LND_USEID; }
            set { _LND_USEID = value; }
        }


      public string LANDUSE
        {
            get { return _LANDUSE; }
            set { _LANDUSE = value; }
        }

    }
}
