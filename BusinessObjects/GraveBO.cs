using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class GraveBO
    {
        private string grv_finishtype;

        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }


        public string Grv_finishtype
        {
            get { return grv_finishtype; }
            set { grv_finishtype = value; }
        }
        //private string gRV_FINISH;

        //public string GRV_FINISH
        //{
        //    get { return gRV_FINISH; }
        //    set { gRV_FINISH = value; }
        //}

        private int pap_graveid = -1;
        private int householdID = -1;
        private int grv_finishid = -1;

        private string othergravefinish = String.Empty;
        private Decimal depreciatedvalue = -1;

        private Decimal grv_dimen_length = -1;
        private Decimal grv_dimen_width = -1;

        private DateTime createdDate;

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        private int createdBy = -1;

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        private DateTime updatedDate;

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
        private int updatedBy = -1;

        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        private string isDeleted = "FALSE";

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }


        public Decimal Depreciatedvalue
        {
            get { return depreciatedvalue; }
            set { depreciatedvalue = value; }
        }



        public Decimal Grv_dimen_width
        {
            get { return grv_dimen_width; }
            set { grv_dimen_width = value; }
        }

        public Decimal Grv_dimen_length
        {
            get { return grv_dimen_length; }
            set { grv_dimen_length = value; }
        }



        public int Grv_finishid
        {
            get { return grv_finishid; }
            set { grv_finishid = value; }
        }



        public string Othergravefinish
        {
            get { return othergravefinish; }
            set { othergravefinish = value; }
        }

        public int HouseholdID
        {
            get { return householdID; }
            set { householdID = value; }
        }


        public int Pap_graveid
        {
            get { return pap_graveid; }
            set { pap_graveid = value; }
        }





    }
}
