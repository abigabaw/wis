using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class SubCategoryBO
    {
        private int bGT_SUBCATEGORYID;

        public int BGT_SUBCATEGORYID
        {
            get { return bGT_SUBCATEGORYID; }
            set { bGT_SUBCATEGORYID = value; }
        }
        private int bGT_CATEGORYID;

        public int BGT_CATEGORYID
        {
            get { return bGT_CATEGORYID; }
            set { bGT_CATEGORYID = value; }
        }
        private string bGT_SUBCATEGORYNUM;

        public string BGT_SUBCATEGORYNUM
        {
            get { return bGT_SUBCATEGORYNUM; }
            set { bGT_SUBCATEGORYNUM = value; }
        }
        private string bGT_SUBCATEGORYNAME;

        public string BGT_SUBCATEGORYNAME
        {
            get { return bGT_SUBCATEGORYNAME; }
            set { bGT_SUBCATEGORYNAME = value; }
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
        private string aCCOUNTCODE;

        public string ACCOUNTCODE
        {
            get { return aCCOUNTCODE; }
            set { aCCOUNTCODE = value; }
        }
    }
}
