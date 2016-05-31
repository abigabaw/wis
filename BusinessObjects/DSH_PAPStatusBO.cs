using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class DSH_PAPStatusBO
    {
        public string ProjectCode { get; set; }

        private int projectId = -1;

        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }

        public string ProjectName { get; set; }

        public int PAPCount { get; set; }

        public int PAPPaidCount { get; set; }

        public int PAPPendingPayCount { get; set; }

        public string ProjectStatus { set; get; }

        public double StatuCount { get; set; }
        decimal eamount = 0.00M;
        decimal estamount = 0.00M;
        public decimal expenseamount 
        { 
            get
            {
                return eamount;
            }
            set
            {
                eamount = value;
            }
        }

        public decimal est_value 
        {
            get
            {
                return estamount;
            }
            set
            {
                estamount = value;
            }
        }

        public int accountcode { get; set; }

        public string BudDate { get; set; }
    }
}
