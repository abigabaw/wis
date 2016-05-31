using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class OtherFenceBO
    {
        private string otherfencedescription;

        public string Otherfencedescription
        {
            get { return otherfencedescription; }
            set { otherfencedescription = value; }
        }


        private int pap_otherfenceid = -1;
        private int householdID = -1;
        private int otherfenceid = -1;
        private Decimal depreciatedvalue = -1;
        private Decimal dIMEN_LENGTH = -1;
        private Decimal dIMEN_WIDTH = -1;
        private int createdBy = -1;
        private int updatedBy = -1;
        private DateTime updatedDate;
        private string isDeleted = "False";

        private byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }


        public int Pap_otherfenceid
        {
            get { return pap_otherfenceid; }
            set { pap_otherfenceid = value; }
        }

        public int HouseholdID
        {
            get { return householdID; }
            set { householdID = value; }
        }
        public int Otherfenceid
        {
            get { return otherfenceid; }
            set { otherfenceid = value; }
        }
        public Decimal Depreciatedvalue
        {
            get { return depreciatedvalue; }
            set { depreciatedvalue = value; }
        }
        public Decimal DIMEN_LENGTH
        {
            get { return dIMEN_LENGTH; }
            set { dIMEN_LENGTH = value; }
        }
        public Decimal DIMEN_WIDTH
        {
            get { return dIMEN_WIDTH; }
            set { dIMEN_WIDTH = value; }
        }
        //public DateTime CreatedDate
        //{
        //    get { return createdDate; }
        //    set { createdDate = value; }
        //}
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}
