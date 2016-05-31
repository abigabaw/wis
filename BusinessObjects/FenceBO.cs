using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class FenceBO
    {
        private string fencedescription;

        public string Fencedescription
        {
            get { return fencedescription; }
            set { fencedescription = value; }
        }


        private int pap_fenceid = -1;
        private int householdID = -1;
        private int fenceid = -1;
        private Decimal depreciatedvalue = -1;
        private Decimal fen_dimen_length = -1;
        private Decimal fen_dimen_height = -1;
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


        public int Pap_fenceid
        {
            get { return pap_fenceid; }
            set { pap_fenceid = value; }
        }

        public int HouseholdID
        {
            get { return householdID; }
            set { householdID = value; }
        }
        public int Fenceid
        {
            get { return fenceid; }
            set { fenceid = value; }
        }
        public Decimal Depreciatedvalue
        {
            get { return depreciatedvalue; }
            set { depreciatedvalue = value; }
        }
        public Decimal Fen_dimen_length
        {
            get { return fen_dimen_length; }
            set { fen_dimen_length = value; }
        }
        public Decimal Fen_dimen_height
        {
            get { return fen_dimen_height; }
            set { fen_dimen_height = value; }
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
