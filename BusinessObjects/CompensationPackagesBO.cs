using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CompensationPackagesBO
    {
        private int userID = -1;
        private int catpkgdoccatID;
        private int itempkgdocitemID;
        private int pkgdocItemID; //primay Key value for MST_PKGDOC_CATGITEM
        private string pkgdoccatName; //PKGDOCCATNAME;
        private string pkgdocitemName; //PKGDOCITEMNAME
        private int approvalLevel = -1;
        private string approvalComents = string.Empty;
        private int projectID = -1;
        private string status = string.Empty;
      

        #region for PrintDocument
        private string documentCode = string.Empty;
        private int hHID;
        #endregion


        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string ApprovalComents
        {
            get { return approvalComents; }
            set { approvalComents = value; }
        }
        public int ApprovalLevel
        {
            get { return approvalLevel; }
            set { approvalLevel = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }
        public string DocumentCode
        {
            get { return documentCode; }
            set { documentCode = value; }
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

        public int CATpkgdoccatID
        {
            get
            {
                return catpkgdoccatID;
            }
            set
            {
                catpkgdoccatID = value;
            }
        }
        public int ITEMpkgdocitemID
        {
            get
            {
                return itempkgdocitemID;
            }
            set
            {
                itempkgdocitemID = value;
            }
        }
        public int PKGdocItemID
        {
            get
            {
                return pkgdocItemID;
            }
            set
            {
                pkgdocItemID = value;
            }
        }

        public string PKGDoccatName
        {
            get
            {
                return pkgdoccatName;
            }
            set
            {
                pkgdoccatName = value;
            }
        }

        public string PKGDOCitemName
        {
            get
            {
                return pkgdocitemName;
            }
            set
            {
                pkgdocitemName = value;
            }
        }

        public string PKGDocumentCode { get; set; }

        public string ApprovedDate { get; set; }
        public string UserName { get; set; }

        public int PKGdocCount { get; set; }
    }
}