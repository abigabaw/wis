using System;

namespace WIS_BusinessObjects
{
    public class ProjectPersonalBO
    {
        private int userID = -1;
        private string username = String.Empty;
        private int projID = -1;
        private int createdBy = -1;
        private int updatedBy = -1;

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

        public int ProjID
        {
            get
            {
                return projID;
            }
            set
            {
                projID = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }
    }
}