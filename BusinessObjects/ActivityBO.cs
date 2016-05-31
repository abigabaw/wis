using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class ActivityBO
    {
        private int cdap_activityid = -1;
        private string cdap_activityname = string.Empty;

        public int Cdap_activityid
        {
            get
            {
                return cdap_activityid;
            }
            set
            {
                cdap_activityid = value;
            }
        }

        public string Cdap_activityname
        {
            get
            {
                return cdap_activityname;
            }
            set
            {
                cdap_activityname = value;
            }
        }
    }
}
