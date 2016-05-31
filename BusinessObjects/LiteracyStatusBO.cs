using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class LiteracyStatusBO
    {
        private int LiteracystatusID = -1;
        private string Literacystatus = string.Empty;
        private string description = string.Empty;
        private string isdeleted = string.Empty;
        private int createdby;
        private  DateTime createddate ;
        private int updatedby;
        private DateTime updateddate;

        public int LTR_STATUSID
        {
            get
            {
                return LiteracystatusID;
            }
            set
            {
                LiteracystatusID = value;
            }
        }

        public string LTR_STATUS
        {
            get
            {
                return Literacystatus;
            }
            set
            {
                Literacystatus = value;
            }
        }

        public string DESCRIPTION
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string ISDELETED
        {
            get
            {
                return isdeleted;
            }
            set
            {
                isdeleted = value;
            }
        }

        public int CREATEDBY
        {
            get
            {
                return createdby;
            }
            set
            {
                createdby = value;
            }
        }
        public DateTime CREATEDDATE
        {
            get
            {
                return createddate;
            }
            set
            {
                createddate = value;
            }
        }

        public int UPDATEDBY
        {
            get
            {
                return updatedby;
            }
            set
            {
                updatedby = value;
            }
        }

        public DateTime UPDATEDDATE
        {
            get
            {
                return updateddate;
            }
            set
            {
                updateddate = value;
            }
        }

       
    }
}