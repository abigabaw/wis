using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CulturPropertiesBO
    {

        private int cULTURALPROPID = -1;
        private int hHID = -1;
        private int cULTUREPROPTYPEID;
        private string cULTUREPROPTYP = string.Empty;
        private string cULTUREPROPDESCRIPTION;
        private int cULT_DIMEN_LENGTH;
        private int cULT_DIMEN_WIDTH;
        private decimal cULT_DEPRECIATEDVALUE;
        private string iSDELETED = "False";
        private int cREATEDBY;
        private decimal cULT_VALUATIONAMOUNT;
        private byte[] photo;
        private decimal vALUATIONAMOUNT;

        public decimal VALUATIONAMOUNT
        {
            get { return vALUATIONAMOUNT; }
            set { vALUATIONAMOUNT = value; }
        }

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public int CULTURALPROPID
        {
            get { return cULTURALPROPID; }
            set { cULTURALPROPID = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }

        public string CULTUREPROPTYP
        {
            get { return cULTUREPROPTYP; }
            set { cULTUREPROPTYP = value; }
        }

        public int CULTUREPROPTYPEID
        {
            get { return cULTUREPROPTYPEID; }
            set { cULTUREPROPTYPEID = value; }
        }
       
        public string CULTUREPROPDESCRIPTION
        {
            get { return cULTUREPROPDESCRIPTION; }
            set { cULTUREPROPDESCRIPTION = value; }
        }

        public int CULT_DIMEN_LENGTH
        {
            get { return cULT_DIMEN_LENGTH; }
            set { cULT_DIMEN_LENGTH = value; }
        }
       
        public int CULT_DIMEN_WIDTH
        {
            get { return cULT_DIMEN_WIDTH; }
            set { cULT_DIMEN_WIDTH = value; }
        }

        public decimal CULT_DEPRECIATEDVALUE
        {
            get { return cULT_DEPRECIATEDVALUE; }
            set { cULT_DEPRECIATEDVALUE = value; }
        }


        public decimal CULT_VALUATIONAMOUNT
        {
            get { return cULT_VALUATIONAMOUNT; }
            set { cULT_VALUATIONAMOUNT = value; }
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

    }
}
