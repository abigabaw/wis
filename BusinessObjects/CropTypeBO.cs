using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CropTypeBO
    {
        private int cropTypeID;
        private string cropType = string.Empty;
        private string isDeleted = string.Empty;
        private DateTime createdDate = DateTime.Now;
        private int createdby = 1;
        private int unitMeasure;
        private string unitName = string.Empty;

        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }


        public int UnitMeasure
        {
            get { return unitMeasure; }
            set { unitMeasure = value; }
        }
       

        

        public int CROPTYPEID
        {
            get { return cropTypeID; }
            set { cropTypeID = value; }
        }       

        public string CropType
        {
            get { return cropType; }
            set { cropType = value; }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }      

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }      

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }
    }
}