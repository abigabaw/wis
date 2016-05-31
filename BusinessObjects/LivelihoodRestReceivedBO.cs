using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    //Table Name : PAP_LIV_REST_RECD
    public class LivelihoodRestReceivedBO
    {
        public int Liv_Rest_RecdID { get; set; }

        public int Liv_Rest_ItemID { get; set; }

        public int Received { get; set; }

        public string DateReceived { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion    
    }
}
