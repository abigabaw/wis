using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class TenureStructureBO
    {

        private int str_tenureId = -1;
        private string str_tenure = String.Empty;
        private string isDeleted = "FALSE";
        public int createdBy = -1;
        public int updatedBy = -1;

        public int Str_TenureId
        {
            get
            {
                return str_tenureId;
            }
            set
            {
                str_tenureId = value;
            }
        }

        public string Str_Tenure
        {
            get
            {
                return str_tenure;
            }
            set
            {
                str_tenure = value;
            }
        }

        public string IsDeleted
        {
            get
            {
                return isDeleted;
            }
            set
            {
                isDeleted = value.ToUpper();
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

