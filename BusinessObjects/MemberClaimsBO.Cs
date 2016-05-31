using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class MemberClaimsBO
    {
        private int lND_RESPONDENTID = -1;
        private string hASCLAIM = String.Empty;
        private string cLAIMDETAILS = String.Empty;
        private string oTHPEOPLEPICK = String.Empty;
        private string pICKFROMOTHPEOPLELAND = String.Empty;
        private string oTHPEOPLEACCESSWATER = String.Empty;
        private string aCCESSWATERFRMOTHPEOPLE = String.Empty;
        private string oTHEREASEMENT = String.Empty;
        private string oTHEREASEMENTDETAILS = String.Empty;

        public int createdBy = -1;
        public int updatedBy = -1;
        public string isDeleted = "False";

        public int LND_RESPONDENTID
        {
            get
            {
                return lND_RESPONDENTID;
            }
            set
            {
                lND_RESPONDENTID = value;
            }
        }

        public int HHID { get; set; }

        public string HASCLAIM
        {
            get
            {
                return hASCLAIM;
            }
            set
            {
                hASCLAIM = value;
            }
        }

        public string CLAIMDETAILS
        {
            get
            {
                return cLAIMDETAILS;
            }
            set
            {
                cLAIMDETAILS = value;
            }
        }
             
        public string PICKFROMOTHPEOPLELAND
        {
            get
            {
                return pICKFROMOTHPEOPLELAND;
            }
            set
            {
                pICKFROMOTHPEOPLELAND = value;
            }
        }

        public string OTHPEOPLEACCESSWATER
        {
            get
            {
                return oTHPEOPLEACCESSWATER;
            }
            set
            {
                oTHPEOPLEACCESSWATER = value;
            }
        }

        public string OTHPEOPLEPICK
        {
            get
            {
                return oTHPEOPLEPICK;
            }
            set
            {
                oTHPEOPLEPICK = value;
            }
        }

        public string OTHEREASEMENT
        {
            get
            {
                return oTHEREASEMENT;
            }
            set
            {
                oTHEREASEMENT = value;
            }
        }

        public string OTHEREASEMENTDETAILS
        {
            get
            {
                return oTHEREASEMENTDETAILS;
            }
            set
            {
                oTHEREASEMENTDETAILS = value;
            }
        }

        public string ACCESSWATERFRMOTHPEOPLE
        {
            get
            {
                return aCCESSWATERFRMOTHPEOPLE;
            }
            set
            {
                aCCESSWATERFRMOTHPEOPLE = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
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
                isDeleted = value;
            }
        }
        
    }
}