using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class RouteBO
    {
        private int routeID = -1;
        private int projectID = -1;
        private string routeName = String.Empty;
        private string routeDetails = String.Empty;
        private bool isFinal = false;
        private int approvedBy = -1;
        private string approvedDate = String.Empty;
        private string comments = String.Empty;
        private int totalRouteScore = -1;

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

        public bool IsFinal
        {
            get
            {
                return isFinal;
            }
            set
            {
                isFinal = value;
            }
        }

        public int ApprovedBy
        {
            get
            {
                return approvedBy;
            }
            set
            {
                approvedBy = value;
            }
        }

        public string ApprovedDate
        {
            get
            {
                return approvedDate;
            }
            set
            {
                approvedDate = value;
            }
        }

        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

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

    }
}
