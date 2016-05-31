using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
 public  class ParameterBO
    {
        private int parameterID = -1;
        private string parameterName = string.Empty;
        private string availableOptions = string.Empty;
        public int AvailableOptionsID { get; set; }

        public string AvailableOptions
        {
            get { return availableOptions; }
            set { availableOptions = value; }
        }

        public int ParameterID
        {
            get
            {
                return parameterID;
            }
            set
            {
                parameterID = value;
            }
        }

        public string ParameterName
        {
            get
            {
                return parameterName;
            }
            set
            {
                parameterName = value;
            }
        }

        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

    }
}
