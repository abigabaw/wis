using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class FixedCostCentreBO
    {
        private int fixedCostCentreID = -1;
        private string fixedCostCentre = String.Empty;
        private string fixedCostCentredescription = String.Empty;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string isDeleted = string.Empty;

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }


        public int FixedCostCentreID
        {
            get
            {
                return fixedCostCentreID;
            }
            set
            {
                fixedCostCentreID = value;
            }
        }

        public string FixedCostCentre
        {
            get
            {
                return fixedCostCentre;
            }
            set
            {
                fixedCostCentre = value;
            }
        }

        public string FixedCostCentreDescription
        {
            get
            {
                return fixedCostCentredescription;
            }
            set
            {
                fixedCostCentredescription = value;
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
    }
}
