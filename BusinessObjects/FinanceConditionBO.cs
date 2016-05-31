using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
   public  class FinanceConditionBO
    {
        private int fINANCECONDITIONID = -1;

        public int FINANCECONDITIONID
        {
            get { return fINANCECONDITIONID; }
            set { fINANCECONDITIONID = value; }
        }
        private string fINANCECONDITION = string.Empty;

        public string FINANCECONDITION
        {
            get { return fINANCECONDITION; }
            set { fINANCECONDITION = value; }
        }
        private string iSDELETED ;

        public string ISDELETED
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }
        private int cREATEDBY = -1;

        public int CREATEDBY
        {
            get { return cREATEDBY; }
            set { cREATEDBY = value; }
        }
        private DateTime cREATEDDATE;

        public DateTime CREATEDDATE
        {
            get { return cREATEDDATE; }
            set { cREATEDDATE = value; }
        }
        private int uPDATEDBY = -1;

        public int UPDATEDBY
        {
            get { return uPDATEDBY; }
            set { uPDATEDBY = value; }
        }
        private DateTime uPDATEDDATE;

        public DateTime UPDATEDDATE
        {
            get { return uPDATEDDATE; }
            set { uPDATEDDATE = value; }
        }
    }
}
  