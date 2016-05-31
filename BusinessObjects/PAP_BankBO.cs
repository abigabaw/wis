using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PAP_BankBO
    {
        private int bankDetailID = -1;
        private int houseHoldID = -1;
        private int bankID = -1;
        private int branchID = -1;
        private string accountNo = String.Empty;
        private string accountHolderName = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "FALSE";

        public int BankDetailID
        {
            get
            {
                return bankDetailID;
            }
            set
            {
                bankDetailID = value;
            }
        }

        public int HouseHoldID
        {
            get
            {
                return houseHoldID;
            }
            set
            {
                houseHoldID = value;
            }
        }

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

        public int BranchID
        {
            get
            {
                return branchID;
            }
            set
            {
                branchID = value;
            }
        }

        public string AccountNo
        {
            get
            {
                return accountNo;
            }
            set
            {
                accountNo = value;
            }
        }

        public string AccountHolderName
        {
            get
            {
                return accountHolderName;
            }
            set
            {
                accountHolderName = value;
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