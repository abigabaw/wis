using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class ModeofPaymentBO
    {
        private int userID = -1;
        private int modeofPaymentID;
        private string modeofPayment = String.Empty;
        private string isDeleted = String.Empty;

        public int ModeofPaymentID
        {
            get { return modeofPaymentID; }
            set { modeofPaymentID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string PaymentType { get; set; }

        public string ModeofPayment
        {
            get { return modeofPayment; }
            set { modeofPayment = value; }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}
