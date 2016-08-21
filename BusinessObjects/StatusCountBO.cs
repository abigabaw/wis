using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class StatusCountBO
    {
        private int pendingApprovals;

        public int PendingApprovals
        {
            get { return pendingApprovals; }
            set { pendingApprovals = value; }
        }

        private int pendingClarify;

        public int PendingClarify
        {
            get { return pendingClarify; }
            set { pendingClarify = value; }
        }

        
    }
}
