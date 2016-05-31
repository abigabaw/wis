using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name: LIV_RES_BUDG
    public class LivelihoodRestBudgetBO
    {
        public int Liv_Res_BudgID { get; set; }

        public int Liv_Bud_CategID { get; set; }

        public int Liv_Bud_ItemID { get; set; }

        public string ImplCost { get; set; }

        public string OperCost { get; set; }

        public string ExternalMonitory { get; set; }

        public decimal NoOfBeneficial { get; set; }

        public decimal ItemQuantity { get; set; }

        public decimal CostPerUnit { get; set; }

        public decimal TotalAmount { get; set; }

        public string Comments { get; set; }

        public string District { get; set; }

        public string County { get; set; }

        public string SubCounty { get; set; }

        public string Parish { get; set; }

        public string Liv_Bud_CategoryName { get; set; }

        public string Liv_Bud_ItemName { get; set; }

        public int ProjectID { get; set; }


        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}
