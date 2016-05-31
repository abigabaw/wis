using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class UploadPhotoBO
    {
        private int userID = -1;
        private int projectID = -1;
        private int hhid = -1;
        private string userName;
        private byte[] photo;
        private string photoModule = string.Empty;
        private int pKID = -1;

        public int PKID
        {
            get { return pKID; }
            set { pKID = value; }
        }

        public string PhotoModule
        {
            get { return photoModule; }
            set { photoModule = value; }
        }

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
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

        public int ProjectID
        {
            get
            {
                return projectID;
            }
            set
            {
                projectID = value;
            }
        }

        public int Hhid
        {
            get
            {
                return hhid;
            }
            set
            {
                hhid = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }


    }
}