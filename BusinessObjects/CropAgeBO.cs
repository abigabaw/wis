using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CropAgeBO
    {
        private int userID = -1;
        private int CropAgeID;
        private string CropAgeName = String.Empty;
        private string CropIsDeleted = String.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        private string isDeleted = string.Empty;

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
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

        public int CROPAGEID
        {
            get
            {
                return CropAgeID;
            }
            set
            {
                CropAgeID = value;
            }
        }

        public string CROPAGE
        {
            get
            {
                return CropAgeName;
            }
            set
            {
                CropAgeName = value;
            }
        }
        public string CROPIsDeleted
        {
            get
            {
                return CropIsDeleted;
            }
            set
            {
                CropIsDeleted = value;
            }
        }

    }
}