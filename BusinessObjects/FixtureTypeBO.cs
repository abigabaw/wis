using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public  class FixtureTypeBO
    {
        private int fixtureID = -1;

        public int FixtureID
        {
            get { return fixtureID; }
            set { fixtureID = value; }
        }
        private string fixtureType = String.Empty;

        public string FixtureType
        {
            get { return fixtureType; }
            set { fixtureType = value; }
        }


        private string swiftCode = String.Empty;

        public string SwiftCode
        {
            get { return swiftCode; }
            set { swiftCode = value; }
        }
        private DateTime createdDate;

        public DateTime CreatedDate1
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

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
    }
}
