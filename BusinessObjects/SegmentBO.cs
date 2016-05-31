using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class SegmentBO
    {
        private int projectSegmentID = -1;
        private int projectID = -1;
        private string segmentName = String.Empty;
        private int lineTypeID = -1;
        private Decimal estBudget = -1;
        private string implementationPeriod = String.Empty;
        private DateTime constrStartDate;
        private DateTime constrEndDate;
        private string funder;
        private int bankid;
        private string bankname;

        public string Bankname
        {
            get { return bankname; }
            set { bankname = value; }
        }

        public int Bankid
        {
            get { return bankid; }
            set { bankid = value; }
        }

        public string Funder
        {
            get { return funder; }
            set { funder = value; }
        }

        public int ProjectSegmentID
        {
            get
            {
                return projectSegmentID;
            }
            set
            {
                projectSegmentID = value;
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

        public string SegmentName
        {
            get
            {
                return segmentName;
            }
            set
            {
                segmentName = value;
            }
        }

        public string RouteLength
        {
            //get
            //{
            //    return routeLength;
            //}
            //set
            //{
            //    routeLength = value;
            //}
            get;
            set;
        }
        public string RightOfWay
        {
            get;
            set;
        }
        public string WayLeave
        {
            get;
            set;
        }
        public string TypeofLine
        {
            get;
            set;
        }
        public int LineTypeID
        {
            get
            {
                return lineTypeID;
            }
            set
            {
                lineTypeID = value;
            }
        }

        public Decimal EstBudget
        {
            get
            {
                return estBudget;
            }
            set
            {
                estBudget = value;
            }
        }

        public string ImplementationPeriod
        {
            get
            {
                return implementationPeriod;
            }
            set
            {
                implementationPeriod = value;
            }
        }

        public DateTime ConstrStartDate
        {
            get
            {
                return constrStartDate;
            }
            set
            {
                constrStartDate = value;
            }
        }

        public DateTime ConstrEndDate
        {
            get
            {
                return constrEndDate;
            }
            set
            {
                constrEndDate = value;
            }
        }

        public int CreatedBy
        {
            get;
            set;
        }

        public DateTime UpdatedDate
        {
            get;
            set;
        }

        public int UpdatedBy
        {
            get;
            set;
        }

        public string IsDeleted
        {
            get;
            set;
        }

        public Decimal Valueofhouse { get; set; }
    }
}
