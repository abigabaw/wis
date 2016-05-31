using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class PAP_HouseholdBO
    {
        Decimal percentageOccupied;

        public Decimal PercentageOccupied
        {
            get { return percentageOccupied; }
            set { percentageOccupied = value; }
        } 
        string overrideopt;

        public string Overrideopt
        {
            get { return overrideopt; }
            set { overrideopt = value; }
        }
        string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
        string landCompensation;

        public string LandCompensation
        {
            get { return landCompensation; }
            set { landCompensation = value; }
        }
        string houseCompensation;

        public string HouseCompensation
        {
            get { return houseCompensation; }
            set { houseCompensation = value; }
        }
        private int hhId = -1;
        private string pap_uid = String.Empty;
        private int projectId = -1;
        private int optiongroupid = -1;
        private int religionId = -1;
        private int occupationId = -1;
        private int papstatusId = -1;
        private int literacycycleid = -1;
        private int updatedby = -1;

        //NEW FIELDS
        private string institutionName = string.Empty;
        private int noofplots = 0;
        private string surname = string.Empty;
        private string firstname = string.Empty;
        private string otherName = string.Empty;
        private int positionId = -1;
        private string contactphone1 = string.Empty;
        private string contactphone2 = string.Empty;
        private string paptype = string.Empty;
        private string optionGroup = string.Empty;
        private string locClassification = string.Empty;
        //END
        private string papName = String.Empty;
        private string plotreference = String.Empty;

        private string gouStatus = String.Empty;
        private string underTakingPeriod = String.Empty;


        public string LocClassification
        {
            get { return locClassification; }
            set { locClassification = value; }
        }

        public string GouStatus
        {
            get { return gouStatus; }
            set { gouStatus = value; }
        }

        public string UnderTakingPeriod
        {
            get { return underTakingPeriod; }
            set { underTakingPeriod = value; }
        }

        public string OptionGroup
        {
            get { return optionGroup; }
            set { optionGroup = value; }
        }
        private string district = String.Empty;
        private string county = String.Empty;
        private string subCounty = String.Empty;
        private string parish = String.Empty;
        private string village = String.Empty;
        private string rightofway = String.Empty;
        private string wayleaves = String.Empty;
        private string isresident = String.Empty;
        private string sex = String.Empty;
        private string dateofbirth = String.Empty;
        private string yearmoveon = String.Empty;
        private string parentslive = String.Empty;
        private string whichparentalive = String.Empty;
        private string whereparentslive = String.Empty;
        private string isidentificationcard = String.Empty;
        private string cardtype = String.Empty;
        private string cardno = String.Empty;
        private string nameoncard = String.Empty;
        private string addressoncard = String.Empty;
        private string maritalstatus = String.Empty;
        private int noofspouse = 0;
        private string tribe = String.Empty;
        private string clan = String.Empty;
        private byte[] photo = null;
        private string isdeleted = String.Empty;
        private string otherreligion = String.Empty;
        private string plotlatitude = String.Empty;
        private string plotlongitude = String.Empty;
        private string capturedBy = String.Empty;
        private string capturedDate = String.Empty;


        private string papuid = string.Empty;
        public string Papuid { get { return papuid; } set { papuid = value; } }

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

        public int ReligionId
        {
            get
            {
                return religionId;
            }
            set
            {
                religionId = value;
            }
        }


        public string Plotlatitude
        {
            get
            {
                return plotlatitude;
            }
            set
            {
                plotlatitude = value;
            }
        }

        public string Plotlongitude
        {
            get
            {
                return plotlongitude;
            }
            set
            {
                plotlongitude = value;
            }
        }

        public string Otherreligion
        {
            get
            {
                return otherreligion;
            }
            set
            {
                otherreligion = value;
            }
        }

        public int HhId
        {
            get
            {
                return hhId;
            }
            set
            {
                hhId = value;
            }
        }

        public string Pap_UId
        {
            get
            {
                return pap_uid;
            }
            set
            {
                pap_uid = value;
            }
        }

        public int ProjectedId
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

        public int OptiongroupId
        {
            get
            {
                return optiongroupid;
            }
            set
            {
                optiongroupid = value;
            }
        }

        public int OccupationId
        {
            get
            {
                return occupationId;
            }
            set
            {
                occupationId = value;
            }
        }

        public int PapstatusId
        {
            get
            {
                return papstatusId;
            }
            set
            {
                papstatusId = value;
            }
        }

        public int LiteracyCycleId
        {
            get
            {
                return literacycycleid;
            }
            set
            {
                literacycycleid = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return updatedby;
            }
            set
            {
                updatedby = value;
            }
        }

        // new 

        public string InstitutionName
        {
            get
            {
                return institutionName;
            }
            set
            {
                institutionName = value;
            }
        }

        public int Noofplots
        {
            get
            {
                return noofplots;
            }
            set
            {
                noofplots = value;
            }
        }

        public int PositionId
        {
            get
            {
                return positionId;
            }
            set
            {
                positionId = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string Othername
        {
            get
            {
                return otherName;
            }
            set
            {
                otherName = value;
            }
        }

        public string Contactphone1
        {
            get
            {
                return contactphone1;
            }
            set
            {
                contactphone1 = value;
            }
        }

        public string Contactphone2
        {
            get
            {
                return contactphone2;
            }
            set
            {
                contactphone2 = value;
            }
        }

        public string Paptype
        {
            get
            {
                return paptype;
            }
            set
            {
                paptype = value;
            }
        }

        public string PapName
        {
            get
            {
                return papName;
            }
            set
            {
                papName = value;
            }
        }

        public string PlotReference
        {
            get
            {
                return plotreference;
            }
            set
            {
                plotreference = value;
            }
        }

        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }


        public string County
        {
            get
            {
                return county;
            }
            set
            {
                county = value;
            }
        }

        public string SubCounty
        {
            get
            {
                return subCounty;
            }
            set
            {
                subCounty = value;
            }
        }

        public string Parish
        {
            get
            {
                return parish;
            }
            set
            {
                parish = value;
            }
        }


        public string Village
        {
            get
            {
                return village;
            }
            set
            {
                village = value;
            }
        }

        public string Rightofway
        {
            get
            {
                return rightofway;
            }
            set
            {
                rightofway = value;
            }
        }


        public string Wayleaves
        {
            get
            {
                return wayleaves;
            }
            set
            {
                wayleaves = value;
            }
        }


        public string Isresident
        {
            get
            {
                return isresident;
            }
            set
            {
                isresident = value;
            }
        }


        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
            }
        }

        public string PlaceofBirth { get; set; }

        public string DateofBirth
        {
            get
            {
                return dateofbirth;
            }
            set
            {
                dateofbirth = value;
            }
        }

        public string Yearmoveon
        {
            get
            {
                return yearmoveon;
            }
            set
            {
                yearmoveon = value;
            }
        }

        public string Parentslive
        {
            get
            {
                return parentslive;
            }
            set
            {
                parentslive = value;
            }
        }

        public string Whichparentalive
        {
            get
            {
                return whichparentalive;
            }
            set
            {
                whichparentalive = value;
            }
        }

        public string Whereparentslive
        {
            get
            {
                return whereparentslive;
            }
            set
            {
                whereparentslive = value;
            }
        }

        public string Isidentificationcard
        {
            get
            {
                return isidentificationcard;
            }
            set
            {
                isidentificationcard = value;
            }
        }

        public string Cardtype
        {
            get
            {
                return cardtype;
            }
            set
            {
                cardtype = value;
            }
        }

        public string CardNo
        {
            get
            {
                return cardno;
            }
            set
            {
                cardno = value;
            }
        }

        public string NameonCard
        {
            get
            {
                return nameoncard;
            }
            set
            {
                nameoncard = value;
            }
        }

        public string AddressonCard
        {
            get
            {
                return addressoncard;
            }
            set
            {
                addressoncard = value;
            }
        }

        public string MaritalStatus
        {
            get
            {
                return maritalstatus;
            }
            set
            {
                maritalstatus = value;
            }
        }

        public int NoofSpouse
        {
            get
            {
                return noofspouse;
            }
            set
            {
                noofspouse = value;
            }
        }

        public string Tribe
        {
            get
            {
                return tribe;
            }
            set
            {
                tribe = value;
            }
        }

        public string Clan
        {
            get
            {
                return clan;
            }
            set
            {
                clan = value;
            }
        }

        public byte[] Photo
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }


        public string Isdeleted
        {
            get
            {
                return isdeleted;
            }
            set
            {
                isdeleted = value;
            }
        }

        private string designation = string.Empty;
        public string Designation { get { return designation; } set { designation = value; } }

        //Add By ramu For freeze the data change

        private string workflowcodeID = string.Empty;
        private int approverStatusID = -1;
        private string pageCode = string.Empty;

        public string PageCode
        {
            get { return pageCode; }
            set { pageCode = value; }
        }

        public int ApproverStatus
        {
            get { return approverStatusID; }
            set { approverStatusID = value; }
        }

        public string Workflowcode
        {
            get { return workflowcodeID; }
            set { workflowcodeID = value; }
        }

        public int CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedByUser { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}