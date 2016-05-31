using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WIS_BusinessObjects
{
    public class GrievancesBO
    {
        private int hhid = -1;
        private int grievanceID = -1;

       
        private string papName = string.Empty;
        private string plotReference = string.Empty;
        private string district = string.Empty;
        private string county = string.Empty;
        private string subCounty = string.Empty;
        private string parish = string.Empty;
        private string village = string.Empty;
        private int grievCategoryID = -1;
        private string description = string.Empty;
        private string grievCategory = string.Empty;
        private string resolutionStatus = string.Empty;
        private string closureComments = string.Empty;
        private int userID;
        private string userName;

        public string ResolutionStatus
        {
            get { return resolutionStatus; }
            set { resolutionStatus = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        

        public string GrievCategory
        {
            get { return grievCategory; }
            set { grievCategory = value; }
        }



        private string actionTaken = string.Empty;
        private DateTime actionTakenDate;
        private int actionTakenBy = -1;

      
        private string basicFacts = string.Empty;
        private string resolution = string.Empty;
        private DateTime resolutionDate;
        private int resolvedBy = -1;



        private DateTime createdDate;
        private int createdBy = -1;
        private DateTime updatedDate;
        private int updatedBy = -1;


        public int GrievanceID
        {
            get { return grievanceID; }
            set { grievanceID = value; }
        }

        public int ResolvedBy
        {
            get { return resolvedBy; }
            set { resolvedBy = value; }
        }

        public int ActionTakenBy
        {
            get { return actionTakenBy; }
            set { actionTakenBy = value; }
        }


        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
       

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
      

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
      

        public int UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        //public string ResolvedBy
        //{
        //    get { return resolvedBy; }
        //    set { resolvedBy = value; }
        //}

         public DateTime ResolutionDate
        {
            get { return resolutionDate; }
            set { resolutionDate = value; }
        }

        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        public string BasicFacts
        {
            get { return basicFacts; }
            set { basicFacts = value; }
        }

        //public string ActionTakenBy
        //{
        //    get { return actionTakenBy; }
        //    set { actionTakenBy = value; }
        //}

        public DateTime ActionTakenDate
        {
            get { return actionTakenDate; }
            set { actionTakenDate = value; }
        }

        public string ActionTaken
        {
            get { return actionTaken; }
            set { actionTaken = value; }
        }

        private string status = "";
        public string Status { get { return status; } set { status = value; } }
        
        private string isDeleted = "False";

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int GrievCategoryID
        {
            get { return grievCategoryID; }
            set { grievCategoryID = value; }
        }

        public string IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string Village
        {
            get { return village; }
            set { village = value; }
        }

        public string Parish
        {
            get { return parish; }
            set { parish = value; }
        }

        public string SubCounty
        {
            get { return subCounty; }
            set { subCounty = value; }
        }

        public string County
        {
            get { return county; }
            set { county = value; }
        }
       
        public string District
        {
            get { return district; }
            set { district = value; }
        }
       
        public string PlotReference
        {
            get { return plotReference; }
            set { plotReference = value; }
        }


        public string PapName
        {
            get { return papName; }
            set { papName = value; }
        }


        public int Hhid
        {
            get { return hhid; }
            set { hhid = value; }
        }

        public string ClosureComments
        {
            get { return closureComments; }
            set { closureComments = value; }
        }
    }
}