using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
   public  class ExpenseBO
    {
        private int pROJECTEXPENSEID;

        public int PROJECTEXPENSEID
        {
            get { return pROJECTEXPENSEID; }
            set { pROJECTEXPENSEID = value; }
        }
        private int pROJECTID;

        public int PROJECTID
        {
            get { return pROJECTID; }
            set { pROJECTID = value; }
        }
        private string eXPENSETYPE;

        public string EXPENSETYPE
        {
            get { return eXPENSETYPE; }
            set { eXPENSETYPE = value; }
        }
        private string aCCOUNTCODE;

        public string ACCOUNTCODE
        {
            get { return aCCOUNTCODE; }
            set { aCCOUNTCODE = value; }
        }
        private decimal eXPENSEAMOUNT;

        public decimal EXPENSEAMOUNT
        {
            get { return eXPENSEAMOUNT; }
            set { eXPENSEAMOUNT = value; }
        }
        private DateTime dATEOFEXPENSE;

        public DateTime DATEOFEXPENSE
        {
            get { return dATEOFEXPENSE; }
            set { dATEOFEXPENSE = value; }
        }
        private string iSDELETED;

        public string ISDELETED
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }
        private int cREATEDBY;

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
        private int uPDATEDBY;

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
