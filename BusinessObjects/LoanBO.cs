using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LoanBO
    {

        private int encumbranceId = -1;
        private string encumbrancepurpose = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int EncumbranceId
        {
            get
            {
                return encumbranceId;
            }
            set
            {
                encumbranceId = value;
            }
        }

        public string Encumbrancepurpose
        {
            get
            {
                return encumbrancepurpose;
            }
            set
            {
                encumbrancepurpose = value;
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

