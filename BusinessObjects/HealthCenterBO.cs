using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class HealthCenterBO
    {
        private int hEALTHCENTERID;
        private string hEALTHCENTERNAME;
        private string iSDELETED;
        private int cREATEDBY;
        private DateTime cREATEDDATE;
        private int uPDATEDBY;
        private DateTime uPDATEDDATE;

        public string HEALTHCENTERNAME
        {
            get { return hEALTHCENTERNAME; }
            set { hEALTHCENTERNAME = value; }
        }

        public string ISDELETED
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }

        public int CREATEDBY
        {
            get { return cREATEDBY; }
            set { cREATEDBY = value; }
        }

        public DateTime CREATEDDATE
        {
            get { return cREATEDDATE; }
            set { cREATEDDATE = value; }
        }

        public int UPDATEDBY
        {
            get { return uPDATEDBY; }
            set { uPDATEDBY = value; }
        }     

        public DateTime UPDATEDDATE
        {
            get { return uPDATEDDATE; }
            set { uPDATEDDATE = value; }
        }

        public int HEALTHCENTERID
        {
            get { return hEALTHCENTERID; }
            set { hEALTHCENTERID = value; }
        }
    }
}