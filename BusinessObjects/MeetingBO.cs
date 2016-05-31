using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class Meeting
    {
        private int cULTURALMEETID = -1;

        public int CULTURALMEETID
        {
            get { return cULTURALMEETID; }
            set { cULTURALMEETID = value; }
        }
        private int hHID = -1;

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
        private int cULTURALPROPID = -1;

        public int CULTURALPROPID
        {
            get { return cULTURALPROPID; }
            set { cULTURALPROPID = value; }
        }
        private DateTime mEETINGDATE;

        public DateTime MEETINGDATE
        {
            get { return mEETINGDATE; }
            set { mEETINGDATE = value; }
        }
        private string mEETINGLOCATION;

        public string MEETINGLOCATION
        {
            get { return mEETINGLOCATION; }
            set { mEETINGLOCATION = value; }
        }
        private int mEETINGPURPOSEID;
        private string meetingpurpose;

        public string Meetingpurpose
        {
            get { return meetingpurpose; }
            set { meetingpurpose = value; }
        }

        public int MEETINGPURPOSEID
        {
            get { return mEETINGPURPOSEID; }
            set { mEETINGPURPOSEID = value; }
        }
        private string wITNESSNGO;

        public string WITNESSNGO
        {
            get { return wITNESSNGO; }
            set { wITNESSNGO = value; }
        }
        private string oPINIONLEADER;

        public string OPINIONLEADER
        {
            get { return oPINIONLEADER; }
            set { oPINIONLEADER = value; }
        }
        private string mINISTRYOFGLSD;

        public string MINISTRYOFGLSD
        {
            get { return mINISTRYOFGLSD; }
            set { mINISTRYOFGLSD = value; }
        }
        private string aESREP;

        public string AESREP
        {
            get { return aESREP; }
            set { aESREP = value; }
        }
        private string mOUSIGNED;

        public string MOUSIGNED
        {
            get { return mOUSIGNED; }
            set { mOUSIGNED = value; }
        }
        private string mEETINGCOMMENTS;

        public string MEETINGCOMMENTS
        {
            get { return mEETINGCOMMENTS; }
            set { mEETINGCOMMENTS = value; }
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
