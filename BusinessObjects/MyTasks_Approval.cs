using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
 {
    public class MyTasks_Approval
    {
        private string projectName = String.Empty;
        private string moduleName = String.Empty;
        private int approverlevel;
        private int approvedCount;
        private int pendingCount;
        private int declinedCount;
        private int projectID;
        private int moduleID;       

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

        public int ApproverLevel
        {
            get
            {
                return approverlevel;
            }
            set
            {
                approverlevel = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }

        public int ApprovedCount
        {
            get
            {
                return approvedCount;
            }
            set
            {
                approvedCount = value;
            }
        }

        public int PendingCount
        {
            get
            {
                return pendingCount;
            }
            set
            {
                pendingCount = value;
            }
        }

        public int DeclinedCount
        {
            get
            {
                return declinedCount;
            }
            set
            {
                declinedCount = value;
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

        public int ModuleID
        {
            get
            {
                return moduleID;
            }
            set
            {
                moduleID = value;
            }
        }
        public string WCode
        {
            get;
            set;
        }
    }
}