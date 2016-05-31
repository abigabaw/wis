using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
  public   class BranchBO
    {
        private int bankID = -1;
        private int bankBranchId = -1;

        public int BankBranchId
        {
            get { return bankBranchId; }
            set { bankBranchId = value; }
        }

        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
        }
       // private string bankName = String.Empty;
        private string city = String.Empty;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string branchName = String.Empty;

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }
        private string swiftCode = String.Empty;

        public string SwiftCode
        {
            get { return swiftCode; }
            set { swiftCode = value; }
        }

        private string bANKCODE = String.Empty;
        public string BANKCODE
        {
            get
            {
                return bANKCODE;
            }
            set
            {
                bANKCODE = value;
            }
        }

        private DateTime createdDate;

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        private int createdBy = -1;

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private DateTime updatedDate;

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
        private int updatedBy = -1;

        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        private string isDeleted = "FALSE";

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}
