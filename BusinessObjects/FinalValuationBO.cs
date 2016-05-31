using System;

namespace WIS_BusinessObjects
{
    public class FinalValuationBO
    {
        private int val_SummaryID = 0;
        private int householdID = 0;

        private decimal cropValue = 0;
        private decimal landValue = 0;
        private decimal fixtureValue = 0;
        private decimal houseValue = 0;
        private decimal replacementValue = 0;
        private decimal damagedcropValue = 0;
        private decimal culturalpropertyValue = 0;
        private decimal grandtotalValue = 0;
        private string valsummaryComments = string.Empty;

        private DateTime createdDate;

        public decimal ResLabourCost { get; set; }

        public decimal GOUAllowance { get; set; }

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
        private string isDeleted = "False";

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string ValsummaryComments
        {
            get { return valsummaryComments; }
            set { valsummaryComments = value; }
        }

        public decimal GrandtotalValue
        {
            get { return grandtotalValue; }
            set { grandtotalValue = value; }
        }

        public decimal CulturalpropertyValue
        {
            get { return culturalpropertyValue; }
            set { culturalpropertyValue = value; }
        }

        public decimal DamagedcropValue
        {
            get { return damagedcropValue; }
            set { damagedcropValue = value; }
        }

        public decimal ReplacementValue
        {
            get { return replacementValue; }
            set { replacementValue = value; }
        }

        public decimal HouseValue
        {
            get { return houseValue; }
            set { houseValue = value; }
        }

        public decimal FixtureValue
        {
            get { return fixtureValue; }
            set { fixtureValue = value; }
        }

        public decimal LandValue
        {
            get { return landValue; }
            set { landValue = value; }
        }

        public decimal CropValue
        {
            get { return cropValue; }
            set { cropValue = value; }
        }

        public decimal NegotiatedAmount { get; set; }

        public int HouseholdID
        {
            get { return householdID; }
            set { householdID = value; }
        }

        public int Val_SummaryID
        {
            get { return val_SummaryID; }
            set { val_SummaryID = value; }
        }
        //public string tmp
        //{ get; set; }

        public int ProjectedId { get; set; }
        public int HhId { get; set; }
        public string PageCode { get; set; }
        public string Workflowcode { get; set; }
        public int ApproverStatus { get; set; }
        public string IsFinal { get; set; }
        public string Comments { get; set; }

        private decimal cropNegValue = 0;
        private decimal landNegValue = 0;
        private decimal fixtureNegValue = 0;
        private decimal replacementNegValue = 0;
        private decimal damagedcropNegValue = 0;
        private decimal culturalpropertyNegValue = 0;
        public decimal CulturalpropertyNegValue
        {
            get { return culturalpropertyNegValue; }
            set { culturalpropertyNegValue = value; }
        }

        public decimal DamagedcropNegValue
        {
            get { return damagedcropNegValue; }
            set { damagedcropNegValue = value; }
        }

        public decimal ReplacementNegValue
        {
            get { return replacementNegValue; }
            set { replacementNegValue = value; }
        }

        public decimal FixtureNegValue
        {
            get { return fixtureNegValue; }
            set { fixtureNegValue = value; }
        }

        public decimal LandNegValue
        {
            get { return landNegValue; }
            set { landNegValue = value; }
        }

        public decimal CropNegValue
        {
            get { return cropNegValue; }
            set { cropNegValue = value; }
        }

        public string CropNegValueAppStatus { get; set; }
        public string LandNegValueAppStatus { get; set; }
        public string FixtureNegValueAppStatus { get; set; }
        public string ReplacementNegValueAppStatus { get; set; }
        public string DamagedcropNegValueAppStatus { get; set; }
        public string CulturalpropertyNegValueAppStatus { get; set; }

        public string Crop_Max_Cap_Case { get; set; }

        public decimal Crop_Val_Aft_Max_Cap { get; set; }
    }
}
