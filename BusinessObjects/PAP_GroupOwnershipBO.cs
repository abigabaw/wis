using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PAP_GroupOwnershipBO
    {
        public int HHID { get; set; }
        public string Paptype { get; set; }
        public int Groupmemberid { get; set; }
        public int Createdby { get; set; }
        public string DistrictIN { get; set; }
        public string CountyIN { get; set; }
        public string SubCountyIN { get; set; }
        public string ParishIN { get; set; }
        public string VillageIN { get; set; }
        public string PlotReferenceIN { get; set; }
        public DateTime DateofBirthIN { get; set; }
        public string IsResidentIN { get; set; }
        public string SexIN { get; set; }
        public string SurnameIN { get; set; }
        public string FirstnameIN { get; set; }
        public string OthernameIN { get; set; }
        public int PositionidIN { get; set; }
        public string Contactphone1IN { get; set; }
        public string Contactphone2IN { get; set; }

        public int OptionGroupIdIN { get; set; }

        private string papuid = string.Empty;
        public string Papuid { get { return papuid; } set { papuid = value; } }

        private string capturedBy = String.Empty;
        private string capturedDate = String.Empty;
        private string gouallowance;
        private string undertakingperiod;

        public string Undertakingperiod
        {
            get { return undertakingperiod; }
            set { undertakingperiod = value; }
        }

        public string Gouallowance
        {
            get { return gouallowance; }
            set { gouallowance = value; }
        }

        public string CapturedBy
        {
            get
            {
                return capturedBy;
            }
            set
            {
                capturedBy = value;
            }
        }

        public string CapturedDate
        {
            get
            {
                return capturedDate;
            }
            set
            {
                capturedDate = value;
            }
        }
    }
}
