using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class PAP_InstitutionBO
    {
        public int HHID { get; set; }

        #region Institution Fields
        public string Paptype { get; set; }

        public string DistrictIN { get; set; }

        public string CountyIN { get; set; }

        public string SubCountyIN { get; set; }

        public string ParishIN { get; set; }

        public string VillageIN { get; set; }

        public int OptionGroupIdIN { get; set; }

        public string InstitutionNameIN { get; set; }

        public int NoofplotsIN { get; set; }

        public string PlotReferenceIN { get; set; }

        public DateTime DateofBirthIN { get; set; }

        public string IsResidentIN { get; set; }

        public string SexIN { get; set; }

        public string SurnameIN { get; set; }

        public string FirstnameIN { get; set; }

        public string OthernameIN { get; set; }

        public int UpdatedbyIN { get; set; }

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
        #endregion

        #region Institution Contact Fields
        public int POSITIONID { get; set; }

        public string CONT_DISTRICT { get; set; }

        public string CONT_COUNTY { get; set; }

        public string CONT_SUBCOUNTY { get; set; }

        public string CONT_PARISH { get; set; }

        public string CONT_VILLAGE { get; set; }

        public string CONTACTPHONE1 { get; set; }

        public string CONTACTPHONE2 { get; set; }

        public byte[] PLOTPHOTO { get; set; }

        #region Common Fields
        public string IsDeleted { get; set; }

        public string CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }
        #endregion CF

        #endregion ICF

        private string papuid = string.Empty;
        public string Papuid { get { return papuid; } set { papuid = value; } }
    }
}
