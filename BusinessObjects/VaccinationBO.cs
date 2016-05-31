using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class VaccinationBO
    {
        private int _Createdby;

        public int Createdby
        {
            get { return _Createdby; }
            set { _Createdby = value; }
        }
        private int vaccinationId;
        private string vaccinationName = String.Empty;
        private string vaccinationIsDeleted = String.Empty;
 


        public int VACCINATIONID
        {
            get
            {
                return vaccinationId;
            }
            set
            {
                vaccinationId = value;
            }
        }
        public string VACCINATIONNAME
        {
            get
            {
                return vaccinationName;
            }
            set
            {
                vaccinationName = value;
            }
        }
        public string ISDELETED
        {
            get
            {
                return vaccinationIsDeleted;
            }
            set
            {
                vaccinationIsDeleted = value;
            }
        }

        

    }
}