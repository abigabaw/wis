using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class DamagedCropsBO
    {
        private int dAMAGED_CROPID;

        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }



        public int DAMAGED_CROPID
        {
            get { return dAMAGED_CROPID; }
            set { dAMAGED_CROPID = value; }
        }
        private int hHID;

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
        private string dMGCRPFORMREFNO = string.Empty;

        public string DMGCRPFORMREFNO
        {
            get { return dMGCRPFORMREFNO; }
            set { dMGCRPFORMREFNO = value; }
        }
        private int cROPID;

        public int CROPID
        {
            get { return cROPID; }
            set { cROPID = value; }
        }

        private string cropname;

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

        private string croptype;

        public string Croptype
        {
            get { return croptype; }
            set { croptype = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int cROPDESCRIPTIONID;

        public int CROPDESCRIPTIONID
        {
            get { return cROPDESCRIPTIONID; }
            set { cROPDESCRIPTIONID = value; }
        }
        private DateTime dATEDAMAGED;

        public DateTime DATEDAMAGED
        {
            get { return dATEDAMAGED; }
            set { dATEDAMAGED = value; }
        }
        private int cROPDAMAGEDBYID;

        public int CROPDAMAGEDBYID
        {
            get { return cROPDAMAGEDBYID; }
            set { cROPDAMAGEDBYID = value; }
        }
        private string cROPDAMAGEDBYOTHER;

        public string CROPDAMAGEDBYOTHER
        {
            get { return cROPDAMAGEDBYOTHER; }
            set { cROPDAMAGEDBYOTHER = value; }
        }

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
        private Decimal aMOUNTPAID;

        public Decimal AMOUNTPAID
        {
            get { return aMOUNTPAID; }
            set { aMOUNTPAID = value; }
        }
        private string cOMMENTS = string.Empty;

        public string COMMENTS
        {
            get { return cOMMENTS; }
            set { cOMMENTS = value; }
        }
        private string iSDELETED = string.Empty;

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
