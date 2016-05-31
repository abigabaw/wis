using System;

namespace WIS_BusinessObjects
{
    public class ProjectBO
    {
        private Decimal percentageofPAP;

        public Decimal PercentageofPAP
        {
            get { return percentageofPAP; }
            set { percentageofPAP = value; }
        }
        private int projectID = -1;
        private string projectCode = String.Empty;
        private string projectName = String.Empty;
        private string objective = String.Empty;
        public DateTime projectStartDate;
        public DateTime projectEndDate;
        public string projectStatus = String.Empty;
        public DateTime createdDate;
        public int createdBy = -1;
        public DateTime updatedDate;
        public int updatedBy = -1;
        public bool isDeleted = false;
        private string frozen = string.Empty;
        private int budgetcurrency = -1;
        private decimal labourcost;
        private decimal bUILDINGMATCOST;
        private decimal dollervalue;
        private string includewayleavelandvalue;

        //Allan.Start
        private int reportID;
        private string reportCode;
        private string reportName;
        private string reportFile;
        //Allan.Start

        public string Includewayleavelandvalue
        {
            get { return includewayleavelandvalue; }
            set { includewayleavelandvalue = value; }
        }
        private string includerecipients;

        public string Includerecipients
        {
            get { return includerecipients; }
            set { includerecipients = value; }
        }

        public decimal Dollervalue
        {
            get { return dollervalue; }
            set { dollervalue = value; }
        }

        public decimal Labourcost
        {
            get { return labourcost; }
            set { labourcost = value; }
        }

        public decimal BUILDINGMATCOST
        {
            get { return bUILDINGMATCOST; }
            set { bUILDINGMATCOST = value; }
        }

        public int BudgetCurrency
        {
            get
            {
                return budgetcurrency;
            }
            set
            {
                budgetcurrency = value;
            }
        }
        public string Frozen
        {
            get { return frozen; }
            set { frozen = value; }
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

        public string ProjectCode
        {
            get
            {
                return projectCode;
            }
            set
            {
                projectCode = value;
            }
        }

        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                projectName = value;
            }
        }

        // Edwin Baguma: Start
        public int ReportID
        {
            get { return reportID; }
            set { reportID = value; }
        }

        public string ReportCode
        {
            get { return reportCode; }
            set { reportCode = value; }
        }

        public string ReportName
        {
            get { return reportName; }
            set { reportName = value; }
        }

        public string ReportFile
        {
            get { return reportFile; }
            set { reportFile = value; }
        }
        // Edwin Baguma: End

        public string Objective
        {
            get
            {
                return objective;
            }
            set
            {
                objective = value;
            }
        }

        public DateTime ProjectStartDate
        {
            get
            {
                return projectStartDate;
            }
            set
            {
                projectStartDate = value;
            }
        }

        public DateTime ProjectEndDate
        {
            get
            {
                return projectEndDate;
            }
            set
            {
                projectEndDate = value;
            }
        }

        public Decimal TotalEstBudget { get; set; }

        public string ProjectStatus
        {
            get
            {
                return projectStatus;
            }
            set
            {
                projectStatus = value;
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

        public bool IsDeleted
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

        #region Unfreeze Section
        public DateTime UnfreezeDate { get; set; }

        public int UnfreezeBy { get; set; }

        public string UnfreezeComments { get; set; }
        #endregion Unfreeze Section

        public Decimal RouteCount { get; set; }      
    }
}