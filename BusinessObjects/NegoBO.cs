using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class Nego
    {
        private int cULTURALNEGOID = -1;
        private int hHID = -1;
        private int cULTURALPROPID = -1;
        private DateTime nEGO_APPOINTMENTDATE;
        private string nEGO_VENUE = string.Empty;
        private DateTime nEGO_DATE;
        private string nEGO_PROBLEMDESC = string.Empty;
        private string iSDELETED = string.Empty;
        private int cREATEDBY = -1;
        private DateTime cREATEDDATE;
        private int uPDATEDBY = -1;
        private DateTime uPDATEDDATE;

        public int CULTURALNEGOID
        {
            get { return cULTURALNEGOID; }
            set { cULTURALNEGOID = value; }
        }
        

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
      

        public int CULTURALPROPID
        {
            get { return cULTURALPROPID; }
            set { cULTURALPROPID = value; }
        }
      

        public DateTime NEGO_APPOINTMENTDATE
        {
            get { return nEGO_APPOINTMENTDATE; }
            set { nEGO_APPOINTMENTDATE = value; }
        }
      

        public string NEGO_VENUE
        {
            get { return nEGO_VENUE; }
            set { nEGO_VENUE = value; }
        }
      

        public DateTime NEGO_DATE
        {
            get { return nEGO_DATE; }
            set { nEGO_DATE = value; }
        }
    

        public string NEGO_PROBLEMDESC
        {
            get { return nEGO_PROBLEMDESC; }
            set { nEGO_PROBLEMDESC = value; }
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
    }
}
