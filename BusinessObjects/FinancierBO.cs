using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class FinancierBO
    {

        private int financierID = -1;
        private int projectID = -1;
        private string financierName = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public string isDeleted = "False";
        private string financereason;

        public string Financereason
        {
            get { return financereason; }
            set { financereason = value; }
        }
        private string finacecondition;

        public string Finacecondition
        {
            get { return finacecondition; }
            set { finacecondition = value; }
        }
        private string financenature;

        public string Financenature
        {
            get { return financenature; }
            set { financenature = value; }
        }

        private int fINANCECONDITIONID = -1;

        public int FINANCECONDITIONID
        {
            get { return fINANCECONDITIONID; }
            set { fINANCECONDITIONID = value; }
        }
        private int fINANCENATUREID = -1;

        public int FINANCENATUREID
        {
            get { return fINANCENATUREID; }
            set { fINANCENATUREID = value; }
        }
        private int fINANCEREASONID = -1;

        public int FINANCEREASONID
        {
            get { return fINANCEREASONID; }
            set { fINANCEREASONID = value; }
        }

        public int FinancierID
        {
            get
            {
                return financierID;
            }
            set
            {
                financierID = value;
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

        public string FinancierName
        {
            get
            {
                return financierName;
            }
            set
            {
                financierName = value;
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
                isDeleted = value;
            }
        }
    }
}
