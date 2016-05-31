using System;

namespace WIS_BusinessObjects
{
    public class ApprovalscoredtlBO
    {
        private int routeID = -1;
        private int projectID = -1;
        private string routeName = String.Empty;
        private string routeDetails = String.Empty;
        private int totalRouteScore = 0;
        private string emailID = string.Empty;
        private string cellNumber = string.Empty;
        private string projectCode = string.Empty;
        private string projectName = string.Empty;
        private string emailSubject = string.Empty;
        private string emailBody = string.Empty;
        private string workFlowCode = string.Empty;
        private int trackHeaderID;
        private string approverComments;
        private string isFinal;
        private int hHID;
        private int cDAPBudgetID;
        private string status = string.Empty;
        private int elementID;

        public int ElementID
        {
            get { return elementID; }
            set { elementID = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int CDAPBudgetID
        {
            get { return cDAPBudgetID; }
            set { cDAPBudgetID = value; }
        }

        public int HHID
        {
            get { return hHID; }
            set { hHID = value; }
        }



        public string IsFinal
        {
            get { return isFinal; }
            set { isFinal = value; }
        }

        public string ApproverComments
        {
            get { return approverComments; }
            set { approverComments = value; }
        }
        
        public int TrackHeaderID
        {
            get { return trackHeaderID; }
            set { trackHeaderID = value; }
        }

        public string WorkFlowCode
        {
            get { return workFlowCode; }
            set { workFlowCode = value; }
        }

       
        public string EmailBody
        {
            get { return emailBody; }
            set { emailBody = value; }
        }

        public string EmailSubject
        {
            get { return emailSubject; }
            set { emailSubject = value; }
        }
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }
        public string EmailID
        {
            get { return emailID; }
            set { emailID = value; }
        }

        public string CellNumber
        {
            get { return cellNumber; }
            set { cellNumber = value; }
        }

        public int RouteID
        {
            get
            {
                return routeID;
            }
            set
            {
                routeID = value;
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

        public string RouteName
        {
            get
            {
                return routeName;
            }
            set
            {
                routeName = value;
            }
        }

        public string RouteDetails
        {
            get
            {
                return routeDetails;
            }
            set
            {
                routeDetails = value;
            }
        }
        public int WorkFlowItemId { get; set; }
        public int TotalRouteScore
        {
            get
            {
                return totalRouteScore;
            }
            set
            {
                totalRouteScore = value;
            }
        }

        public int UpdatedBy { get; set; }

        public int TrackerHdrID { get; set; }

        public string RequesterName { get; set; }
    }
}