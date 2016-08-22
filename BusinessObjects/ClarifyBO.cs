using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class ClarifyBO
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int hhid;

        public int HHID {
            get { return hhid;}
            set {hhid = value;}
        }

        private string papName = string.Empty;

        public string PapName {
            get { return papName; }
            set { papName = value; }
        }

        private string requester = string.Empty;

        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private int respondentID;

        public int RespondentID
        {
            get { return respondentID; }
            set { respondentID = value; }
        }

        public string Requester
        {
            get { return requester; }
            set { requester = value; }
        }

        private DateTime requestDate;

        public DateTime RequestDate{
            get { return requestDate; }
            set { requestDate = value; }
        }

        private string requestDetails = string.Empty;

        public string RequestDetails{
            get{return requestDetails;}
            set { requestDetails = value; }
        }

        private string respondent = string.Empty;

        public string Respondent
        {
            get { return respondent; }
            set { respondent = value;                }
        }

        private DateTime responseDate;

        public string ResponseDate
        {
            get { if (responseDate == DateTime.MinValue) { return ""; } else { return responseDate.ToString(); } }
            set { responseDate = Convert.ToDateTime(value); }
        }

        private string responseDetails = string.Empty;

        public string ResponseDetails
        {
            get { return responseDetails; }
            set { responseDetails = value; }
        }

        private string status = string.Empty;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string statusMessage = string.Empty;

        public string StatusMessage
        {
            get { return statusMessage; }
            set { statusMessage = value; }
        }

        private int trackHeader;

        public int TrackHeader
        {
            get { return trackHeader; }
            set { trackHeader = value; }
        }


   
    }

    


}
