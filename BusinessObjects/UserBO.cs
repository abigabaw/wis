using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class UserBO
    {
        private int userID;
        private string userName = String.Empty;
        private string emailID = String.Empty;
        private int roleID;
        //public Role Role { get; set; }

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

        public string Pwd
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string EmailID
        {
            get
            {
                return emailID;
            }
            set
            {
                emailID = value;
            }
        }

        public int RoleID
        {
            get
            {
                return roleID;
            }
            set
            {
                roleID = value;
            }
        }

        public string RoleName
        {
            get;
            set;
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public string CellNumber
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string IsDeleted
        {
            get;
            set;
        }
        public string ErrorMessage
        {
            get;
            set;
        }
    }
}