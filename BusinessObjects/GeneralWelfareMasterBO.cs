using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class GeneralWelfareMasterBO
    {
        private int welfareIndicatorID = -1;
        private string welfareIndicatorName = String.Empty;
        private string fieldType = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int WelfareIndicatorID { get { return welfareIndicatorID; } set { welfareIndicatorID = value; } }

        public string WelfareIndicatorName
        {
            get
            {
                return welfareIndicatorName;
            }
            set
            {
                welfareIndicatorName = value;
            }
        }

        public string FieldType
        {
            get
            {
                return fieldType;
            }
            set
            {
                fieldType = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
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

        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
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

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }

        public int AssociatedWith { get; set; }
    }
}
