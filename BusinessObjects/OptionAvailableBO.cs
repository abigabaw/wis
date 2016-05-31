using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class OptionAvailableBO
    {
        private int userID = -1;
        private int id;
        private string optionAvailable = String.Empty;
        private string isdeleted;
        private string createdby;
        private DateTime createddate;

        public DateTime Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }

        public string Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }

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

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string OptionAvailable
        {
            get
            {
                return optionAvailable;
            }
            set
            {
                optionAvailable = value;
            }
        }


    }
}

