using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CropDiameterBO
    {
        private int userID = -1;
        private int CropDiaMeterID;
        private string CropDiaMeter = String.Empty;
        private string isDeleted = String.Empty;


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

        public int CROPDIAMETERID
        {
            get
            {
                return CropDiaMeterID;
            }
            set
            {
                CropDiaMeterID = value;
            }
        }

        public string CROPDIAMETER
        {
            get
            {
                return CropDiaMeter;
            }
            set
            {
                CropDiaMeter = value;
            }
        }
        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value;
            }
        }

    }
}