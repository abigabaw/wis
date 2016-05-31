using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class NeighbourBO
    {
        private int PAP_NEIGHBOURID;

        public int PAP_NEIGHBOURID1
        {
            get { return PAP_NEIGHBOURID; }
            set { PAP_NEIGHBOURID = value; }
        }
        private string TRN_PAP_NEIGHBOURNAme = string.Empty;

        public string TRN_PAP_NEIGHBOURNAme1
        {
            get { return TRN_PAP_NEIGHBOURNAme; }
            set { TRN_PAP_NEIGHBOURNAme = value; }
        }

        private string BOUNDARY_DISPUTE1 = string.Empty;

        public string BOUNDARY_DISPUTE
        {
            get { return BOUNDARY_DISPUTE1; }
            set { BOUNDARY_DISPUTE1 = value; }
        }

        private string DISPUTE_DETAILS1 = string.Empty;
        public string DISPUTE_DETAILS
        {
            get { return DISPUTE_DETAILS1; }
            set { DISPUTE_DETAILS1 = value; }
        }

        private string DIRECTION = string.Empty;

        public string DIRECTION1
        {
            get { return DIRECTION; }
            set { DIRECTION = value; }
        }
        private string BOUNDARIESCONFIRMED = string.Empty;

        public string BOUNDARIESCONFIRMED1
        {
            get { return BOUNDARIESCONFIRMED; }
            set { BOUNDARIESCONFIRMED = value; }
        }
        private string ISDELETED = string.Empty;

        public string ISDELETED1
        {
            get { return ISDELETED; }
            set { ISDELETED = value; }
        }
        private int CREATEDBY;

        public int CREATEDBY1
        {
            get { return CREATEDBY; }
            set { CREATEDBY = value; }
        }
        private DateTime CREATEDDATE;

        public DateTime CREATEDDATE1
        {
            get { return CREATEDDATE; }
            set { CREATEDDATE = value; }
        }

        private int HHID;

        public int HHID1
        {
            get { return HHID; }
            set { HHID = value; }
        }

       
    }
}