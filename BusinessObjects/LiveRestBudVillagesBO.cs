using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WIS_BusinessObjects
{
    public class LiveRestBudVillagesBO
    {
        //TRN_LIV_RES_BUD_VILLAGES
        public int Liv_Res_BudgId { get; set; }

        public string Village { get; set; }

       
        //Common Fields
        public string IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }
    }
}