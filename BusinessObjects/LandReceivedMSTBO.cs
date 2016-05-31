using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class LandReceivedMSTBO
    {
        private int userID = -1;
        private int landReceivedID;
        private string landReceived = String.Empty;
        private string isDeleted = String.Empty;

        public int LandReceivedID
        {
            get { return landReceivedID; }
            set { landReceivedID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string LandReceived
        {
            get { return landReceived; }
            set { landReceived = value; }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}
