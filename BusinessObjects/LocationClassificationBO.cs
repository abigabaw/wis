using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class LocationClassificationBO
    {
        private string iSDELETED = string.Empty;
        private string lOCTNCLASFCTNNAME = string.Empty;
        private string lOCTNCODE = string.Empty;
        private int lOCTNCLASFCTNID;

        public int LOCTNCLASFCTNID
        {
            get { return lOCTNCLASFCTNID; }
            set { lOCTNCLASFCTNID = value; }
        }

        private int cREATEDBY;

        public int CREATEDBY
        {
            get { return cREATEDBY; }
            set { cREATEDBY = value; }
        }

        public string LOCTNCODE
        {
            get { return lOCTNCODE; }
            set { lOCTNCODE = value; }
        }

        public string LOCTNCLASFCTNNAME
        {
            get { return lOCTNCLASFCTNNAME; }
            set { lOCTNCLASFCTNNAME = value; }
        }

        public string ISDELETED
        {
            get { return iSDELETED; }
            set { iSDELETED = value; }
        }
    }
}
