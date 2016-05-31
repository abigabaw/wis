using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class GrievancesMasterBO
    {
        private int gRIEVANCECATEGID = -1;
        private string grievancesCategory = String.Empty;
        private int createdBy;
        private string isdeleted = string.Empty;

        public string IsDeleted
        {
            get { return isdeleted; }
            set { isdeleted = value; }
        }



        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public int GRIEVANCECATEGID
        {
            get
            {
                return gRIEVANCECATEGID;
            }
            set
            {
                gRIEVANCECATEGID = value;
            }
        }

        public string GrievancesCategory
        {
            get
            {
                return grievancesCategory;
            }
            set
            {
                grievancesCategory = value;
            }
        }
    }
}
