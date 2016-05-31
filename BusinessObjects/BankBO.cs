using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class BankBO
    {
        private int bankID = -1;
        private string bankName = String.Empty;
        private string city = String.Empty;
        private string branchName = String.Empty;
        private string swiftCode = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int BankID
        {
            get
            {
                return bankID;
            }
            set
            {
                bankID = value;
            }
        }

        public string BankName
        {
            get
            {
                return bankName;
            }
            set
            {
                bankName = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public string BranchName
        {
            get
            {
                return branchName;
            }
            set
            {
                branchName = value;
            }
        }

        public string SwiftCode
        {
            get
            {
                return swiftCode;
            }
            set
            {
                swiftCode = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
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

        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
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
                isDeleted = value.ToUpper();
            }
        }
    }
}