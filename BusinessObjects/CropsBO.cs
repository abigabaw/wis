using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CropsBO
    {
        private int pAP_CROPID;
        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public int PAP_CROPID
        {
            get { return pAP_CROPID; }
            set { pAP_CROPID = value; }
        }

        private int hHID;

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }

        private int cROPID;

        public int CROPID
        {
            get { return cROPID; }
            set { cROPID = value; }
        }

        private string cropname = string.Empty;

        public string Cropname
        {
            get { return cropname; }
            set { cropname = value; }
        }

        private int cROPTYPEID;

        public int CROPTYPEID
        {
            get { return cROPTYPEID; }
            set { cROPTYPEID = value; }
        }

        private string croptype = string.Empty;

        public string Croptype
        {
            get { return croptype; }
            set { croptype = value; }
        }

        private int cROPDESCRIPTIONID;

        public int CROPDESCRIPTIONID
        {
            get { return cROPDESCRIPTIONID; }
            set { cROPDESCRIPTIONID = value; }
        }

        private string cropdescription = string.Empty;

        public string Cropdescription
        {
            get { return cropdescription; }
            set { cropdescription = value; }
        }

        private int uNITOFMEASURE;

        public int UNITOFMEASURE
        {
            get { return uNITOFMEASURE; }
            set { uNITOFMEASURE = value; }
        }

        private string unitofmeasure = string.Empty;

        public string UnitName { get; set; }

        private Decimal qUANTITY;

        public Decimal QUANTITY
        {
            get { return qUANTITY; }
            set { qUANTITY = value; }
        }

        private Decimal cROPRATE;

        public Decimal CROPRATE
        {
            get { return cROPRATE; }
            set { cROPRATE = value; }
        }

        private string cOMMENTS;

        public string COMMENTS
        {
            get { return cOMMENTS; }
            set { cOMMENTS = value; }
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

        private int cREATEDDATE;

        public int CREATEDDATE
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

        private Decimal cROPVALUATION;

        public Decimal CROPVALUATION
        {
            get { return cROPVALUATION; }
            set { cROPVALUATION = value; }
        }
    }
}
