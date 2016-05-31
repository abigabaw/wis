using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CardTypeBO
    {
        private int userID = -1;
        private int cardTypeID;
        private string cardTypeName = String.Empty;
        private string cardTypeIsDeleted = String.Empty;
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

        public int CardTypeID
        {
            get
            {
                return cardTypeID;
            }
            set
            {
                cardTypeID = value;
            }
        }

        public string CardTypeName
        {
            get
            {
                return cardTypeName;
            }
            set
            {
                cardTypeName = value;
            }
        }
        public string CardTypeIsDeleted
        {
            get
            {
                return cardTypeIsDeleted;
            }
            set
            {
                cardTypeIsDeleted = value;
            }
        }

    }
}