using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class ProprietorBO
    {
        private int proprietorID = -1;
        private string proprietorName = String.Empty;

        public int ProprietorID
        {
            get
            {
                return proprietorID;
            }
            set
            {
                proprietorID = value;
            }
        }

        public string ProprietorName
        {
            get
            {
                return proprietorName;
            }
            set
            {
                proprietorName = value;
            }
        }

    }
}
