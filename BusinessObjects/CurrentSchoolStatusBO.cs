using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CurrentSchoolStatusBO
    {
        private int currentSchoolStatusID;
        private string currentSchoolStatus = string.Empty;
        private string description = string.Empty;
        private string isdeleted = string.Empty;
        private int createdby;
        private DateTime createddate = System.DateTime.Now;
       

      
        public int CurrentSchoolStatusID
        {
            get { return currentSchoolStatusID; }
            set { currentSchoolStatusID = value; }
        }
              
        public string CurrentSchoolStatus
        {
            get { return currentSchoolStatus; }
            set { currentSchoolStatus = value; }
        }
               
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }

        public DateTime Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }       
    }
}