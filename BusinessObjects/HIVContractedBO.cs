using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
   public class HIVContractedBO
    {
        private int contractedID = -1;
        private string contractedThrough = String.Empty;
        private int createdBy;
        private string isdeleted;

        public string Isdeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public string ContractedThrough
        {
            get { return contractedThrough; }
            set { contractedThrough = value; }
        }
        public int ContractedID
        {
            get { return contractedID; }
            set { contractedID = value; }
        }
       
       
        
    }
}
