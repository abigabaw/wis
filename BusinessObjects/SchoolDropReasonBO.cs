using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class SchoolDropReasonBO
    {
        private int schooldropreasonID;
        private string schooldropreason=string.Empty;
        private string description = string.Empty;
        private bool isdeleted;
        private int createdby ;
        private DateTime createddate;

        public DateTime Createddate
        {
            get { return createddate; }
            set { createddate = value; }
        }

        public int Createdby
        {
            get { return createdby; }
            set { createdby = value; }
        }
       

        public bool Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Schooldropreason
        {
            get { return schooldropreason; }
            set { schooldropreason = value; }
        }

        public int SchooldropreasonID
        {
            get { return schooldropreasonID; }
            set { schooldropreasonID = value; }
        }

      

    }
}