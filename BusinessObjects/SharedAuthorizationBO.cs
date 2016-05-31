using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class SharedAuthorizationBO
    {
        private int approvaltempauthoriserid_ = -1;

        private int authoriserid_ = -1;

        private int assigntoid_ = -1;

        private string remarks_ = string.Empty;

        private int createdby_ = -1;

        private int projectID = -1;

        private int updateby_ = -1;

        //public int ProjectID
        //{
        //    get { return projectID; }
        //    set { projectID = value; }
        //}

        public int WorkFlowSharedId { get; set; }

        public int AssignedToId
        {
            get;
            set;
        }

        public int AuthoriserId
        {
            get;
            set;
        }

        public int ModuleId
        {
            get;
            set;
        }

        public int WorkFlowId
        {
            get;
            set;
        }

        public int ProjectId
        {
            get;
            set;
        }

        public string ModuleName { get; set; }

        public string WorkFlow { get; set; }

        public string AuthoriserName { get; set; }

        public string AssignedTo { get; set; }

        public string Remarks
        {
            get { return remarks_; }
            set { remarks_ = value; }
        }

        public int CreatedBy
        {
            get { return createdby_; }
            set { createdby_ = value; }
        }

        public int UpdateBy
        {
            get { return updateby_; }
            set { updateby_ = value; }
        }

        public int TRACKERHEADERID
        {
            get;
            set;
        }

        public string LockStatus
        {
            get;
            set;
        }

        public string LockedBy
        {
            get;
            set;
        }

    }
}
