using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WIS_BusinessObjects
{
    public class UploadDocumentBO
    {
        private int userID = -1;
        private int projectID = -1;
        private int hhid = -1;
        private decimal recordCount = -1;
        private string userName;
        private string projectcode;

        private int documentTypeID;
        private string documentCode;
        private string documentType;

        private string documnetPath;

        private string date;
        private int pAPDOCUMENTID;
        private string keyWord = string.Empty;
        private string description = string.Empty;


        public decimal RecordCount
        {
            get
            {
                return recordCount;
            }
            set
            {
                recordCount = value;
            }
        }

        public string Projectcode
        {
            get { return projectcode; }
            set { projectcode = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string KeyWord
        {
            get { return keyWord; }
            set { keyWord = value; }
        }

        public int PAPDOCUMENTID
        {
            get { return pAPDOCUMENTID; }
            set { pAPDOCUMENTID = value; }
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

        public int DOCSERVICEID { set; get; }

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

        public int HHID
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

        public int DocumentTypeID
        {
            get
            {
                return documentTypeID;
            }
            set
            {
                documentTypeID = value;
            }
        }
        public string DocumentCode
        {
            get
            {
                return documentCode;
            }
            set
            {
                documentCode = value;
            }
        }
        public string DocumentType
        {
            get
            {
                return documentType;
            }
            set
            {
                documentType = value;
            }
        }
        public string DocumentPath
        {
            get
            {
                return documnetPath;
            }
            set
            {
                documnetPath = value;
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

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

    }
}