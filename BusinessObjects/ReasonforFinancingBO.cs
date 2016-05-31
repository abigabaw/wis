using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
   public class ReasonforFinancingBO
    {
        private int fINANCEREASONID = -1;

        public int FINANCEREASONID
        {
            get { return fINANCEREASONID; }
            set { fINANCEREASONID = value; }
        }
        private string fINANCEREASON = string.Empty;

        public string FINANCEREASON
        {
            get { return fINANCEREASON; }
            set { fINANCEREASON = value; }
        }
        private string iSDELETED = string.Empty;

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
