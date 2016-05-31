using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class WelfareVoluntaryBO
    {
        private int voluntaryID = -1;
        private int householdID = -1;
        private string whereGetDrinkingWater = String.Empty;
        private string waterSourceDistance = String.Empty;
        private string doYouFish = String.Empty;
        private string whereDoYouFish = String.Empty;
        private string howOftenFish = String.Empty;
        private string doYouHunt = String.Empty;
        private string whereHunt = String.Empty;
        private string firewood = String.Empty;
        private string charcoal = String.Empty;
        private string paraffin = String.Empty;
        private string electricity = String.Empty;
        private string gas = String.Empty;
        private string solar = String.Empty;
        private string biogas = String.Empty;
        private string otherFuel = String.Empty;
        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;
        private string isDeleted = "FALSE";

        public int VoluntaryID
        {
            get { return voluntaryID; }
            set { voluntaryID = value; }
        }

        public int HouseholdID
        {
            get
            {
                return householdID;
            }
            set
            {
                householdID = value;
            }
        }

        public string WhereGetDrinkingWater
        {
            get { return whereGetDrinkingWater; }
            set { whereGetDrinkingWater = value; }
        }

        public string WaterSourceDistance
        {
            get { return waterSourceDistance; }
            set { waterSourceDistance = value; }
        }

        public string DoYouFish
        {
            get { return doYouFish; }
            set { doYouFish = value; }
        }

        public string WhereDoYouFish
        {
            get { return whereDoYouFish; }
            set { whereDoYouFish = value; }
        }

        public string HowOftenFish
        {
            get { return howOftenFish; }
            set { howOftenFish = value; }
        }

        public string DoYouHunt
        {
            get { return doYouHunt; }
            set { doYouHunt = value; }
        }

        public string WhereHunt
        {
            get { return whereHunt; }
            set { whereHunt = value; }
        }

        public string Firewood
        {
            get { return firewood; }
            set { firewood = value; }
        }

        public string Charcoal
        {
            get { return charcoal; }
            set { charcoal = value; }
        }

        public string Paraffin
        {
            get { return paraffin; }
            set { paraffin = value; }
        }

        public string Electricity
        {
            get { return electricity; }
            set { electricity = value; }
        }

        public string Gas
        {
            get { return gas; }
            set { gas = value; }
        }

        public string Solar
        {
            get { return solar; }
            set { solar = value; }
        }

        public string Biogas
        {
            get { return biogas; }
            set { biogas = value; }
        }

        public string OtherFuel
        {
            get { return otherFuel; }
            set { otherFuel = value; }
        }

        public string Comments { get; set; }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedBy;
            }
            set
            {
                updatedBy = value;
            }
        }

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
            }
        }
    }
}
