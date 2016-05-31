using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class CDAPBudgetBO
    {
        private int cdap_budgid = -1;
        private int cdap_categoryid = -1;
        private int cdap_subcategoryid = -1;
        private decimal unit = 0;
        private decimal quantity = 0;
        private decimal rateperunit = -1;
        public int updatedBy = -1;
        public string unitname = string.Empty;
        public string cdap_categoryname = string.Empty;
        public string cdap_subcategoryname = string.Empty;

        private string fundReqStatus = string.Empty; // Add by Ramu.S
        private int projectID = -1;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
         public string FundReqStatus
        {
            get { return fundReqStatus; }
            set { fundReqStatus = value; }
        }
        
        public int Cdap_budgid
        {
            get
            {
                return cdap_budgid;
            }
            set
            {
                cdap_budgid = value;
            }
        }
        public int Cdap_categoryid
        {
            get
            {
                return cdap_categoryid;
            }
            set
            {
                cdap_categoryid = value;
            }
        }
        public string Cdap_categoryname
        {
            get
            {
                return cdap_categoryname;
            }
            set
            {
                cdap_categoryname = value;
            }
        }
        public int Cdap_subcategoryid
        {
            get
            {
                return cdap_subcategoryid;
            }
            set
            {
                cdap_subcategoryid = value;
            }
        }
        public string Cdap_subcategoryname
        {
            get
            {
                return cdap_subcategoryname;
            }
            set
            {
                cdap_subcategoryname = value;
            }
        }
        public decimal Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
            }
        }
        public string UnitName
        {
            get
            {
                return unitname;
            }
            set
            {
                unitname = value;
            }
        }
        public decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
        public decimal Rateperunit
        {
            get
            {
                return rateperunit;
            }
            set
            {
                rateperunit = value;
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
