using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class EmailTemplateBO
    {
        private string approverUserName = string.Empty;
        private string description = string.Empty;
        private string emailID = string.Empty;
        private string projectCode = string.Empty;
        private string projectName = string.Empty;
        private string emailSubject = string.Empty;
        private string emailBody = string.Empty;
        private string requestdate = string.Empty;
        private string duedate = string.Empty;
        private string workflowcode = string.Empty;
        private string triggertype = string.Empty;
        private string cellnumber = string.Empty;
        private string smstext = string.Empty;


        public string Smstext
        {
            get { return smstext; }
            set { smstext = value; }
        }

        public string Triggertype
        {
            get { return triggertype; }
            set { triggertype = value; }
        }

        public string Cellnumber
        {
            get { return cellnumber; }
            set { cellnumber = value; }
        }

        public string Requestdate
        {
            get { return requestdate; }
            set { requestdate = value; }
        }

        public string Workflowcode
        {
            get { return workflowcode; }
            set { workflowcode = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string EmailID
        {
            get { return emailID; }
            set { emailID = value; }
        }

        public string Duedate
        {
            get { return duedate; }
            set { duedate = value; }
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

        public string ApproverUserName
        {
            get { return approverUserName; }
            set { approverUserName = value; }
        }
    }
}
