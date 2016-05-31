using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class TemporaryAuthorizationBO
    {
        private int approvaltempauthoriserid_ = -1;

        private int authoriserid_ = -1;

        private int assigntoid_ = -1;

        private DateTime fromdate_;

        private DateTime todate_;

        private string isdeleted_ = string.Empty;

        private string remarks_ = string.Empty;

        private int createdby_ = -1;

        private int projectID = -1;

        private int updateby_ = -1;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public int Approvaltempauthoriserid
        {
            get { return approvaltempauthoriserid_; }
            set { approvaltempauthoriserid_ = value; }
        }

        public int Authoriserid
        {
            get { return authoriserid_; }
            set { authoriserid_ = value; }
        }

        public string AuthoriserName { get; set; }

        public string AssignedTo { get; set; }

        public int Assigntoid
        {
            get { return assigntoid_; }
            set { assigntoid_ = value; }
        }

        public DateTime Fromdate
        {
            get { return fromdate_; }
            set { fromdate_ = value; }
        }

        public DateTime Todate
        {
            get { return todate_; }
            set { todate_ = value; }
        }

        public string Isdeleted
        {
            get { return isdeleted_; }
            set { isdeleted_ = value; }
        }

        public string Remarks
        {
            get { return remarks_; }
            set { remarks_ = value; }
        }

        public int Createdby
        {
            get { return createdby_; }
            set { createdby_ = value; }
        }

        public int Updateby
        {
            get { return updateby_; }
            set { updateby_ = value; }
        }
    }
}
