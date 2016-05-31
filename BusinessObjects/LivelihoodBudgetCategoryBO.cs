﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name : MST_LIV_BDG_CATEG
    public class LivelihoodBudgetCategoryBO
    {
        public int Liv_Bud_CategID { get; set; }

        public string Liv_Bud_CategoryName { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}