using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIS_BusinessObjects
{
    public class Trn_Pap_HouseHoldBO
    {
        private int hhId = -1;
        //private int pap_uid = -1;
        private int projectId = -1;
        private int optiongroupid = -1;
        private int religionId = -1;
        private int occupationId = -1;
        private int papstatusId = -1;
        private int literacycycleid = -1;
        private int updatedby = -1;

        private string papName = String.Empty;
        private string papType = String.Empty;
        private string plotreference = String.Empty;
        private string optiongroup = String.Empty;

        public string Optiongroup
        {
            get { return optiongroup; }
            set { optiongroup = value; }
        }
        private string district =  String.Empty;
        private string county =  String.Empty;
        private string subCounty = String.Empty;
        private string parish = String.Empty;
        private string village =String.Empty;
        private string rightofway = String.Empty;
        private string wayleaves = String.Empty;        
        private string isresident = String.Empty;
        private string sex = String.Empty;
        private string dateofbirth = String.Empty;
        private string yearmoveon = String.Empty;
        private string parentslive = String.Empty;
        private string whereparentslive = String.Empty;
        private string isidentificationcard = String.Empty;
        private string cardtype = String.Empty;
        private string cardno = String.Empty;
        private string nameoncard = String.Empty;
        private string addressoncard = String.Empty;
        private string maritalstatus = String.Empty;
        private string noofspouse = String.Empty;
        private string tribe = String.Empty;
        private string clan = String.Empty;      
        private string photo = String.Empty;
        private string isdeleted = String.Empty;
        private string plotlatitude = String.Empty;
        private string plotlongitude = String.Empty;
        // for ADD PAP page
        private string surname = string.Empty;
        private string firstname = string.Empty;
        private string otherName = string.Empty;
        private string landtenure = string.Empty;

        private string paptype = string.Empty;
        private string insti = string.Empty;
        private string projectName = string.Empty;
        private string viewstatus;

        public string Viewstatus
        {
            get { return viewstatus; }
            set { viewstatus = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        private string projectCode = string.Empty;

        public string ProjectCode
        {
            get { return projectCode; }
            set { projectCode = value; }
        }

    

        public double Cropsvalue { set; get; }
        public double Housevalue { set; get; }
        public double Disturbance { set; get; }
        public double TotalSQM { set; get; }
        public double TotalHa { set; get; }
        public double TotalAcres { set; get; }
        public double SubTotal { set; get; }
        public double Total { set; get; }

        public string ROW_X { set; get; }
        public string ROW_Y { set; get; }
        public string WL_X { set; get; }
        public string WL_Y { set; get; }

        private string papuid = string.Empty;
        public string Papuid { get { return papuid; } set { papuid = value; } }

        private string designation = string.Empty;
        public string Designation { get { return designation; } set { designation = value; } }

        public string Institution
        {
            get
            {
                return insti;
            }
            set
            {
                insti = value;
            }
        }
        public string Landtenure
        {
            get
            {
                return landtenure;
            }
            set
            {
                landtenure = value;
            }
        }
        //end

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

        //public int Pap_UId
        //{
        //    get
        //    {
        //        return pap_uid;
        //    }
        //    set
        //    {
        //        pap_uid = value;
        //    }
        //}

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

        public string PapType
        {
            get
            {
                return papType;
            }
            set
            {
                papType = value;
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

        public string NoofSpouse
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

        public string Photo
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

        public string PAPIDPOP
        {
            get;
            set;
        }

        public string PAPDesignationPOP
        {
            get;
            set;
        }
    }
}