using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class ConsultantBO
    {
        private int consultID = -1;
        private int projectID = -1;
        private string consultName = String.Empty;
        private string consultType = String.Empty;
        private string address = String.Empty;
        private string conNumber = String.Empty;
        private string conPerson = String.Empty;
        private string emailAddress = String.Empty;
        private int createdBy = -1;
        private int updatedBy = -1;
        public string isDeleted = "False";

        public int ConsultID
        {
            get
            {
                return consultID;
            }
            set
            {
                consultID = value;
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

        public string ConsultName
        {
            get
            {
                return consultName;
            }
            set
            {
                consultName = value;
            }
        }

        public string ConsultType
        {
            get
            {
                return consultType;
            }
            set
            {
                consultType = value;
            }
        }


        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string ConNumber
        {
            get
            {
                return conNumber;
            }
            set
            {
                conNumber = value;
            }
        }

        public string ConPerson
        {
            get
            {
                return conPerson;
            }
            set
            {
                conPerson = value;
            }
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                emailAddress = value;
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

        public string IsDeleted
        {
            get
            {
                return isDeleted.ToUpper();
            }
            set
            {
                isDeleted = value;
            }
        }
    }
}