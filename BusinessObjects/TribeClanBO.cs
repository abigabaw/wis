using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class TribeClanBO
    {
        private int masterID = -1;
        private string masterName = String.Empty;

        public int MasterID
        {
            get
            {
                return masterID;
            }
            set
            {
                masterID = value;
            }
        }

        public string MasterName
        {
            get
            {
                return masterName;
            }
            set
            {
                masterName = value;
            }
        }
    }
}