using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PublicLandInfoBO
    {

        private int hID = -1;      
        private int lnd_TENUREID = -1;
        private string hASTITLEDETAILS = String.Empty;
        private string yEAROFACQUISITION = String.Empty;
        private string fROMWHOM = String.Empty;
        private string cOMMENTS = String.Empty;
        private string wHOCLAIMSLAND = String.Empty;
        private string lIVEDSINCEBIRTH = String.Empty;
        private string mOVEDYEAR = String.Empty;
        private string wHERELIVEDBEFORE = String.Empty;
        private string iSMORTGAGED = String.Empty;
        private string mORTGAGEDETAILS = String.Empty;
        public int updatedBy = -1;
        public string isDeleted = "False";
        public int createdBy = -1;
        public int landRecivedfromid = -1;
        public int userid = 85;
         //MCBO.CreatedBy = Convert.ToInt32(Session["USER_ID"].ToString());

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

        public int LandRecivedfromid
        {
            get
            {
                return landRecivedfromid;
            }
            set
            {
                landRecivedfromid = value;
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

        public int LND_TENUREID
        {
            get
            {
                return lnd_TENUREID;
            }
            set
            {
                lnd_TENUREID = value;
            }
        }

        public string HASTITLEDETAILS
        {
            get
            {
                return hASTITLEDETAILS;
            }
            set
            {
                hASTITLEDETAILS = value;
            }
        }


        public string YEAROFACQUISITION
        {
            get
            {
                return yEAROFACQUISITION;
            }
            set
            {
                yEAROFACQUISITION = value;
            }
        }



        public string FROMWHOM
        {
            get
            {
                return fROMWHOM;
            }
            set
            {
                fROMWHOM = value;
            }
        }


        public string COMMENTS
        {
            get
            {
                return cOMMENTS;
            }
            set
            {
                cOMMENTS = value;
            }
        }

        public string WHOCLAIMSLAND
        {
            get
            {
                return wHOCLAIMSLAND;
            }
            set
            {
                wHOCLAIMSLAND = value;
            }
        }

        public string LIVEDSINCEBIRTH
        {
            get
            {
                return lIVEDSINCEBIRTH;
            }
            set
            {
                lIVEDSINCEBIRTH = value;
            }
        }

        public string MOVEDYEAR
        {
            get
            {
                return mOVEDYEAR;
            }
            set
            {
                mOVEDYEAR = value;
            }
        }


        public string WHERELIVEDBEFORE
        {
            get
            {
                return wHERELIVEDBEFORE;
            }
            set
            {
                wHERELIVEDBEFORE = value;
            }
        }

        public string ISMORTGAGED
        {
            get
            {
                return iSMORTGAGED;
            }
            set
            {
                iSMORTGAGED = value;
            }
        }
        public string MORTGAGEDETAILS
        {
            get
            {
                return mORTGAGEDETAILS;
            }
            set
            {
                mORTGAGEDETAILS = value;
            }
        }



        internal LandInfoList GetLandInfo()
        {
            throw new NotImplementedException();
        }
    }
}