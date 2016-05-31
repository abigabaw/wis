using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class AffectedAcreageValuationBO
    {
        private int livingOffID = -1;
        private int householdID = -1;
        private string landowner = string.Empty;
        private string landblock = string.Empty;
        private string landplot = string.Empty;
        private int proprietorid = 0;
        private string wholeacreageacres = string.Empty;
        private decimal rowacres;
        private decimal rowlandvalueshare;
        private decimal rowrateperacre;
        private decimal rowlandvalue;
        private decimal wlacres;
        private decimal dimunitionlevel;
        private decimal wlrateperacre;
        private decimal wllandvalueshare;
        private decimal wllandvalue;
        public int createdBy = -1;
        public int updatedBy = -1;
        private string locClassification = string.Empty;

        public string LocClassification
        {
            get { return locClassification; }
            set { locClassification = value; }
        }

        public int LivingOffID
        {
            get
            {
                return livingOffID;
            }
            set
            {
                livingOffID = value;
            }
        }

        public int HouseholdID
        {
            get
            {
                return householdID;
            }
            set
            {
                householdID = value;
            }
        }

        public string Landowner
        {
            get
            {
                return landowner;
            }
            set
            {
                landowner = value;
            }
        }
        public string Landblock
        {
            get
            {
                return landblock;
            }
            set
            {
                landblock = value;
            }
        }
        public string Landplot
        {
            get
            {
                return landplot;
            }
            set
            {
                landplot = value;
            }
        }

        public string Wholeacreageacres
        {
            get
            {
                return wholeacreageacres;
            }
            set
            {
                wholeacreageacres = value;
            }
        }

        public int Proprietorid
        {
            get
            {
                return proprietorid;
            }
            set
            {
                proprietorid = value;
            }
        }

        public decimal Rowacres
        {
            get
            {
                return rowacres;
            }
            set
            {
                rowacres = value;
            }
        }

        public decimal Rowlandvalueshare
        {
            get
            {
                return rowlandvalueshare;
            }
            set
            {
                rowlandvalueshare = value;
            }
        }

        public decimal Rowrateperacre
        {
            get
            {
                return rowrateperacre;
            }
            set
            {
                rowrateperacre = value;
            }
        }

        public decimal Rowlandvalue
        {
            get
            {
                return rowlandvalue;
            }
            set
            {
                rowlandvalue = value;
            }
        }

        public decimal Wlacres
        {
            get
            {
                return wlacres;
            }
            set
            {
                wlacres = value;
            }
        }

        public decimal Dimunitionlevel
        {
            get
            {
                return dimunitionlevel;
            }
            set
            {
                dimunitionlevel = value;
            }
        }

        public decimal Wlrateperacre
        {
            get
            {
                return wlrateperacre;
            }
            set
            {
                wlrateperacre = value;
            }
        }

        public decimal Wllandvalueshare
        {
            get
            {
                return wllandvalueshare;
            }
            set
            {
                wllandvalueshare = value;
            }
        }

        public decimal Wllandvalue
        {
            get
            {
                return wllandvalue;
            }
            set
            {
                wllandvalue = value;
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
