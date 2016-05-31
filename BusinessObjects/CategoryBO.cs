using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class CategoryBO
    {
        private int bGT_CATEGORYID;

        public int BGT_CATEGORYID
        {
            get { return bGT_CATEGORYID; }
            set { bGT_CATEGORYID = value; }
        }
        private string bGT_CATEGORYNAME;

        public string BGT_CATEGORYNAME
        {
            get { return bGT_CATEGORYNAME; }
            set { bGT_CATEGORYNAME = value; }
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
