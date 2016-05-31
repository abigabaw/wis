using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{


    public class PstatusBO
    {
        private int PAPDESIGNATIONID;
        private int CREATEDBY;

        public int CREATEDBY1
        {
            get { return CREATEDBY; }
            set { CREATEDBY = value; }
        }

        public int PAPDESIGNATIONID1
        {
            get { return PAPDESIGNATIONID; }
            set { PAPDESIGNATIONID = value; }
        }
        private string PAPDESIGNATION = string.Empty;

        public string PAPDESIGNATION1
        {
            get { return PAPDESIGNATION; }
            set { PAPDESIGNATION = value; }
        }
        private string ISDELETED = string.Empty;

        public string ISDELETED1
        {
            get { return ISDELETED; }
            set { ISDELETED = value; }
        }


        public string ConcernName { get; set; }
    }
}
