using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class WelfareIndicatorBO
    {

        private int wlf_indicatorID = -1;
        private string wlf_indicatorname = String.Empty;
        private string fieldtype = string.Empty;
        private string isDeleted = string.Empty;
        private int userID = -1;

        public int Wlf_indicatorID
        {
            get { return wlf_indicatorID; }
            set { wlf_indicatorID = value; }
        }

        public string Wlf_indicatorname
        {
            get { return wlf_indicatorname; }
            set { wlf_indicatorname = value; }
        }

        public string Fieldtype
        {
            get { return fieldtype; }
            set { fieldtype = value; }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int AssociatedWith { get; set; }
    }
}
