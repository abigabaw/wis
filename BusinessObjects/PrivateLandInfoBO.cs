using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PrivateLandInfoBO
    {
        private int hIDpriv = -1;
        private int lnd_TENUREIDPriv = -1;
        private int updatedBy = -1;
        private int privatelandid = -1;
        private string lANDLORDNAME = String.Empty;
        private string cLAIMANTNAME = String.Empty;
        private string wHENFARMINGBEGAN = String.Empty;
        private string wHEREFARMEDBEFORE = String.Empty;
        private string dOSPOUSESFARM = String.Empty;
        private string dOCHILDRENFARM = String.Empty;
        private string aGREEMENTTYPE = String.Empty;
        private string pRODASSETOPPORTUNITIES = String.Empty;
        private int createby = -1;
        public int useridpriv = 85;

        public int HIDPriv
        {
            get
            {
                return hIDpriv;
            }
            set
            {
                hIDpriv = value;
            }
        }

        public int PRIVATELANDID
        {
            get
            {
                return privatelandid;
            }
            set
            {
                privatelandid = value;
            }
        }
        public int Useridpriv
        {
            get
            {
                return useridpriv;
            }
            set
            {
                useridpriv = value;
            }
        }


        public int Createby
        {
            get
            {
                return createby;
            }
            set
            {
                createby = value;
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

        public int Lnd_TENUREIDPriv
        {
            get
            {
                return lnd_TENUREIDPriv;
            }
            set
            {
                lnd_TENUREIDPriv = value;
            }
        }


        public string LANDLORDNAME
        {
            get
            {
                return lANDLORDNAME;
            }
            set
            {
                lANDLORDNAME = value;
            }
        }


        public string CLAIMANTNAME
        {
            get
            {
                return cLAIMANTNAME;
            }
            set
            {
                cLAIMANTNAME = value;
            }
        }
        

        public string WHENFARMINGBEGAN
        {
            get
            {
                return wHENFARMINGBEGAN;
            }
            set
            {
                wHENFARMINGBEGAN = value;
            }
        }


        public string WHEREFARMEDBEFORE
        {
            get
            {
                return wHEREFARMEDBEFORE;
            }
            set
            {
                wHEREFARMEDBEFORE = value;
            }
        }


        public string DOSPOUSESFARM
        {
            get
            {
                return dOSPOUSESFARM;
            }
            set
            {
                dOSPOUSESFARM = value;
            }
        }

        public string DOCHILDRENFARM
        {
            get
            {
                return dOCHILDRENFARM;
            }
            set
            {
                dOCHILDRENFARM = value;
            }
        }

        public string AGREEMENTTYPE
        {
            get
            {
                return aGREEMENTTYPE;
            }
            set
            {
                aGREEMENTTYPE = value;
            }
        }


        public string PRODASSETOPPORTUNITIES
        {
            get
            {
                return pRODASSETOPPORTUNITIES;
            }
            set
            {
                pRODASSETOPPORTUNITIES = value;
            }
        }

        
            }
        }



    
