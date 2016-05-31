using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ConcernBO
    {
        private int userID = -1;
        private int concernID;
        private string concernName = String.Empty;
        private string concernIsDeleted = String.Empty;
        private string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }


        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public int ConcernID
        {
            get
            {
                return concernID;
            }
            set
            {
                concernID = value;
            }
        }

        public string ConcernName
        {
            get
            {
                return concernName;
            }
            set
            {
                concernName = value;
            }
        }
        public string ConcernIsDeleted
        {
            get
            {
                return concernIsDeleted;
            }
            set
            {
                concernIsDeleted = value;
            }
        }

    }
}