using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CropNameBO
    {
        private int cropNameID;
        private string unitName = string.Empty;
        private int unitMeasure;

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
       
        public int CROPID
        {
            get { return cropNameID; }
            set { cropNameID = value; }
        }
        private string cropName = string.Empty;

        public string CropName
        {
            get { return cropName; }
            set { cropName = value; }
        }
        private string isDeletedBy = string.Empty;

        public string IsDeletedBy
        {
            get { return isDeletedBy; }
            set { isDeletedBy = value; }
        }
        private DateTime createdDate = DateTime.Now;

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        private int createdBy = 1;

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

    }
}