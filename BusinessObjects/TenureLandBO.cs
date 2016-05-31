using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class TenureLandBO
    {
        private int lnd_tenureId = -1;        
        private string lnd_tenure = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int Lnd_TenureId
        {
            get
            {
                return lnd_tenureId;
            }
            set
            {
                lnd_tenureId = value;
            }
        }

        public string Lnd_Tenure
        {
            get
            {
                return lnd_tenure;
            }
            set
            {
                lnd_tenure = value;
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
    }
}

