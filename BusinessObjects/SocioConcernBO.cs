using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class SocioConcernBO
    {
        private int userID = -1;
        private int concernID;
        private string concernName;
        private string otherConcern = String.Empty;
        private string isdeleted;
        private int hHID;
        private int papConcernID;


        public int PapConcernID
        {
            get { return papConcernID; }
            set { papConcernID = value; }
        }
        public string ConcernName
        {
            get { return concernName; }
            set { concernName = value; }
        }
        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int ConcernID
        {
            get { return concernID; }
            set { concernID = value; }
        }

        public string OtherConcern
        {
            get { return otherConcern; }
            set { otherConcern = value; }
        }

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }
    }
}