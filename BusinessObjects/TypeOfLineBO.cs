using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class TypeOfLineBO
    {
        private int _LineTypeId;

        public int LineTypeId
        {
            get { return _LineTypeId; }
            set { _LineTypeId = value; }
        }
        private string _TypeOfLine;

        public string TypeOfLine
        {
            get { return _TypeOfLine; }
            set { _TypeOfLine = value; }
        }
        private string _Wayleavemeasurement;

        public string Wayleavemeasurement
        {
            get { return _Wayleavemeasurement; }
            set { _Wayleavemeasurement = value; }
        }
        private string _Rightofwaymeasurement;

        public string Rightofwaymeasurement
        {
            get { return _Rightofwaymeasurement; }
            set { _Rightofwaymeasurement = value; }
        }
        private int _Createdby;

        public int Createdby
        {
            get { return _Createdby; }
            set { _Createdby = value; }
        }

        private string isDeleted;
        public string IsDeleted { get { return isDeleted; } set { isDeleted = value.ToUpper(); } }
    }
}