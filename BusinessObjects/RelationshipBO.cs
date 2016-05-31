using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class RelationshipBO
    {
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private int rELATIONSHIPID = -1;
        private string rELATIONSHIP = String.Empty;
        public string IsDeleted { get; set; }
        public int RELATIONSHIPID
        {
            get
            {
                return rELATIONSHIPID;
            }
            set
            {
                rELATIONSHIPID = value;
            }
        }

        public string RELATIONSHIP
        {
            get
            {
                return rELATIONSHIP;
            }
            set
            {
                rELATIONSHIP = value;
            }
        }

      
    }
}