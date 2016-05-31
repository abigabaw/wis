using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name : MST_LIV_REST_ITEMS
    public class LivelihoodRestoreItemsBO
    {
        public int Liv_Rest_ItemID { get; set; }
        public string Liv_Rest_ItemName { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion
    }
}
