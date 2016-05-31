using System;

namespace WIS_BusinessObjects
{
    public class ConsultantTypeBO
    {
        private int consultantTypeID = -1;
        private string consultantType = String.Empty;
        private int createdBy;
        private string isdeleted = string.Empty;

        public string IsDeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

     

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public int ConsultantTypeID
        {
            get
            {
                return consultantTypeID;
            }
            set
            {
                consultantTypeID = value;
            }
        }

        public string ConsultantType
        {
            get
            {
                return consultantType;
            }
            set
            {
                consultantType = value;
            }
        }
    }

}
