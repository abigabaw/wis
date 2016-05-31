using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LandInfoRespondentsBO
    {
        #region Declaration 
        private int holdingID = -1;
        private int hID = -1;
        private int lND_TYPEID = -1;
        private string landtype = String.Empty;
        private int lND_USEID = -1;
        private string landuse = String.Empty;
        private string dISTRICT = String.Empty;
        private string cOUNTY = String.Empty;
        private string sUBCOUNTY = String.Empty;
        private string vILLAGE = String.Empty;
        private string tENURE = String.Empty;
        private decimal tOTALSIZE = -1;
        private string iSPRIMARYRESIDENCE = String.Empty;
        private string iSAFFECTED = String.Empty;
        public int updatedBy = -1;
        public string isDeleted = "False";
        public int userid = 85;
        private string land_type = string.Empty;
        private string land_use = string.Empty;
        #endregion Declaration

        #region Properties Declaration
        public string Land_Type
        {
            get
            {
                return land_type;
            }
            set
            {
                land_type = value;
            }
        }

        public string Land_Use
        {
            get
            {
                return land_use;
            }
            set
            {
                land_use = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }

        public int Userid
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }

        public int HID
        {
            get
            {
                return hID;
            }
            set
            {
                hID = value;
            }
        }

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }

        public int HOLDINGID
        {
            get
            {
                return holdingID;
            }
            set
            {
                holdingID = value;
            }
        }

        public int LND_TYPEID
        {
            get
            {
                return lND_TYPEID;
            }
            set
            {
                lND_TYPEID = value;
            }
        }

        public string LandType
        {
            get
            {
                return landtype;
            }
            set
            {
                landtype = value;
            }
        }

        public int LND_USEID
        {
            get
            {
                return lND_USEID;
            }
            set
            {
                lND_USEID = value;
            }
        }

        public string LandUse
        {
            get
            {
                return landuse;
            }
            set
            {
                landuse = value;
            }
        }

        public string DISTRICT
        {
            get
            {
                return dISTRICT;
            }
            set
            {
                dISTRICT = value;
            }
        }

        public string COUNTY
        {
            get
            {
                return cOUNTY;
            }
            set
            {
                cOUNTY = value;
            }
        }

        public string SUBCOUNTY
        {
            get
            {
                return sUBCOUNTY;
            }
            set
            {
                sUBCOUNTY = value;
            }
        }

        public string VILLAGE
        {
            get
            {
                return vILLAGE;
            }
            set
            {
                vILLAGE = value;
            }
        }

        public string TENURE
        {
            get
            {
                return tENURE;
            }
            set
            {
                tENURE = value;
            }
        }

        public int TenureId { get; set; }

        public decimal TOTALSIZE
        {
            get
            {
                return tOTALSIZE;
            }
            set
            {
                tOTALSIZE = value;
            }
        }

        public string ISPRIMARYRESIDENCE
        {
            get
            {
                return iSPRIMARYRESIDENCE;
            }
            set
            {
                iSPRIMARYRESIDENCE = value;
            }
        }

        public string ISAFFECTED
        {
            get
            {
                return iSAFFECTED;
            }
            set
            {
                iSAFFECTED = value;
            }
        }
        #endregion Properties Declaration
    }
}