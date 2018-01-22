using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WIS_BusinessObjects
{
    public class LoginBO
    {
        private int userID;
        private string UserName = String.Empty;
        private string Password = String.Empty;

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

        public string USERNAME
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }

        

        public string PASSWORD
        {
            get
            {
                return Password;
            }
            set
            {
                Password = value;
            }
        }

        public string DisplayName { get; set; }
    }
}