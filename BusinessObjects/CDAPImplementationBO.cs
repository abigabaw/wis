using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CDAPImplementationBO
    {
        private decimal cdap_phaseid = -1;
        private int cdap_phaseno = -1;
        private int cdap_phaseactivityid = -1;
        private DateTime periodFrom;
        private DateTime periodTo;
        private int cdap_activityid = -1;
        private int projectId = -1;
        private string cdap_activityname = string.Empty;
        private string district = String.Empty;
        private string county = String.Empty;
        private string subCounty = String.Empty;
        private int villageid = -1;
        private string village = String.Empty;
        private string activitydetails = String.Empty;
        private string modeofimplementation = String.Empty;
        private string challenges = String.Empty;
        private string comments = String.Empty;
        private DateTime activitydatefrom;
        private DateTime activitydateto;
        private int hhId = -1;
        private int updatedby = -1;
        private decimal eXPENDITURE;

        public decimal EXPENDITURE
        {
            get { return eXPENDITURE; }
            set { eXPENDITURE = value; }
        }

        public int Villageid
        {
            get { return villageid; }
            set { villageid = value; }
        }
    
        public string PapNames { get; set; }

        public int Cdap_phaseactivityid
        {
            get { return cdap_phaseactivityid; }
            set { cdap_phaseactivityid = value; }
        }
        public int ProjectedId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }
        public decimal Cdap_phaseid
        {
            get { return cdap_phaseid; }
            set { cdap_phaseid = value; }
        }
        public int Cdap_phaseno
        {
            get { return cdap_phaseno; }
            set { cdap_phaseno = value; }
        }
        public DateTime PeriodFrom
        {
            get { return periodFrom; }
            set { periodFrom = value; }
        }
        public DateTime PeriodTo
        {
            get { return periodTo; }
            set { periodTo = value; }
        }
        public int Cdap_activityid
        {
            get { return cdap_activityid; }
            set { cdap_activityid = value; }
        }
        public string Cdap_activityname
        {
            get { return cdap_activityname; }
            set { cdap_activityname = value; }
        }
        public string Activitydetails
        {
            get { return activitydetails; }
            set { activitydetails = value; }
        }
        public string Modeofimplementation
        {
            get { return modeofimplementation; }
            set { modeofimplementation = value; }
        }
        public string Challenges
        {
            get { return challenges; }
            set { challenges = value; }
        }
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
        public DateTime Activitydatefrom
        {
            get { return activitydatefrom; }
            set { activitydatefrom = value; }
        }
        public DateTime Activitydateto
        {
            get { return activitydateto; }
            set { activitydateto = value; }
        }
        public int Updatedby
        {
            get { return updatedby; }
            set { updatedby = value; }
        }
        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }

        public string County
        {
            get
            {
                return county;
            }
            set
            {
                county = value;
            }
        }


        public string SubCounty
        {
            get
            {
                return subCounty;
            }
            set
            {
                subCounty = value;
            }
        }

        public string Village
        {
            get
            {
                return village;
            }
            set
            {
                village = value;
            }
        }

        public int HhId
        {
            get { return hhId; }
            set { hhId = value; }
        }
    }
}
