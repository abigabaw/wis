using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name: LIV_RES_BUD_VILLAGES
    public class LivelihoodRestBudVillagesBO
    {
        public int Liv_Res_BudgID { get; set; }

        public string VillageID { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}
