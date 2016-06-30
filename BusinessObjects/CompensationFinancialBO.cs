using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class CompensationFinancialBO
    {
        private string roleName = String.Empty;
        private string roledescription = String.Empty;

        public int Cmp_FinancialID { get; set; }
        public int HHID { get; set; }

        #region Land Section
        public decimal LandValuation { get; set; }
        public decimal LandPaidValuation { get; set; }
        public decimal LandDA { get; set; }
        public decimal LandTotalValuation { get; set; }
        public decimal LandInKindCompensation { get; set; }
        public decimal LandDiffPayment { get; set; }
        public string LandValComments { get; set; }
        #endregion

        #region Residential Structure Section
        public decimal ResDepreciatedValue { get; set; }
        public decimal ResReplacementValue { get; set; }
        public decimal ResDA { get; set; }
        public decimal ResMovingAllowance { get; set; }
        public decimal ResLabourCost { get; set; }
        public decimal ResPayment { get; set; }
        public decimal ResTotalValuation { get; set; }
        public decimal ResPaidValuation { get; set; }
        public string ResInKindCompensation { get; set; }
        public string ResComments { get; set; }

        #endregion

        #region Fixtures Section
        public decimal FixtureValuation { get; set; }
        public decimal FixtureDA { get; set; }
        public decimal FixtureTotalValuation { get; set; }
        public decimal FixturePaidValuation { get; set; }
        public string FixtureComments { get; set; }
        //public decimal FixtureTotalValuation { get; set; }
        #endregion

        #region Crops Section
        public decimal CropValuation { get; set; }
        public decimal CropPaidValuation { get; set; }
        public string CropMaxCapCase { get; set; }
        public decimal CropValAftMaxCap { get; set; }
        public decimal CropDA { get; set; }
        public decimal CropTotalValuation { get; set; }
        public string CropComments { get; set; }

        #endregion

        #region Others
        public decimal CulturePropPaidValuation { get; set; }
        public decimal DamagedCropPaidValuation { get; set; }
        public decimal CulturePropValuation {get;set;}
        public decimal DamagedCropValuation {get;set;}
        public decimal TotalOtherValuation { get; set; }

        #endregion

        #region Summery Section
        public decimal TotalValuation { get; set; }
        public decimal FacilitationAllowance { get; set; }
        public decimal FacilitationAllowancePaid { get; set; }
        public decimal NegotiatedAmount { get; set; }
        public decimal NegotiatedAmountPaid { get; set; }
        public int CompPaymentId { get; set; }
        #endregion

        #region Package Delivery Info
        public int Cmp_DeliveryId { get; set; }
        public DateTime DeliveryDate { get; set; }

        // Edwin: 30JUN2016 - Fix wrong delivery date issue
        public DateTime DeliveryCreatedDate { get; set; }

        public int DeliveredBy { get; set; }
        public string PAPAction { get; set; }
        public string DeliveryComments { get; set; }
        #endregion Package Delivery Info

        #region Common Section
        public string IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        #endregion      

        public string Land_Approval_Status { get; set; }

        public string Replacment_Approval_Status { get; set; }

        public string Fixture_Approval_Status { get; set; }

        public string Crop_Approval_Status { get; set; }

        public string Culture_Approval_Status { get; set; }

        public string Damaged_Approval_Status { get; set; }

        public string Facilitation_Approval_Status { get; set; }

        public string Final_Approval_Status { get; set; }

        public string Nego_Amount_Approval_Status { get; set; }

       

        #region Batch Objects
        public BatchBO oBatchBO { get; set; }
        #endregion Batch Objects
    }
}