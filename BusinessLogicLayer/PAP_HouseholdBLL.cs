using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PAP_HouseholdBLL
    {
        /// <summary>
        /// To Get HouseHold Data
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
     
        public PAP_HouseholdBO GetHouseHoldData(int ID)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.GetHouseHoldData(ID);
        }
        public PAP_HouseholdBO getCommentsData(int id)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.getCommentsData(id);

        }
        public int GetHousaeHold(PAP_HouseholdBO objPAPhh, string landStatus)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.GetHousaeHold(objPAPhh, landStatus);
 
        }

        /// <summary>
        /// To Update HouseHold Details
        /// </summary>
        /// <param name="objHousehold"></param>
        /// <returns></returns>
        public string UpdateHouseHoldDetails(PAP_HouseholdBO objHousehold)
        {
            PAP_HouseholdDAL objHouseHoldDAL = new PAP_HouseholdDAL();
            return objHouseHoldDAL.UpdateHouseHoldDetails(objHousehold);            
        }

        /// <summary>
        /// To Get PAP Photo
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public PAP_HouseholdBO GetPAPPhoto(int householdID)
        {
            return (new PAP_HouseholdDAL()).GetPAPPhoto(householdID);
        }

        /// <summary>
        /// To Search PAP For Pap Export
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPForPapExport(bool recentRecords, int projectID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            return (new PAP_HouseholdDAL()).SearchPAPForPapExport(recentRecords, projectID, papName, plotReference, district, county, subCounty, parish, village);
        }

        /// <summary>
        /// To Search PAP
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAP(bool recentRecords, int projectID, int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            return (new PAP_HouseholdDAL()).SearchPAP(recentRecords, projectID, HHID, PAPUID, papName, plotReference, district, county, subCounty, parish, village);
        }

        /// <summary>
        /// To Global Search PAP
        /// </summary>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GlobalSearchPAP(int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village, int UserId)
        {
            return (new PAP_HouseholdDAL()).GlobalSearchPAP(HHID, PAPUID, papName, plotReference, district, county, subCounty, parish, village, UserId);
        }

        /// <summary>
        /// To Search PAP For ALL
        /// </summary>
        /// <param name="recentRecords"></param>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <param name="papName"></param>
        /// <param name="plotReference"></param>
        /// <param name="district"></param>
        /// <param name="county"></param>
        /// <param name="subCounty"></param>
        /// <param name="parish"></param>
        /// <param name="village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPForALL(bool recentRecords, int projectID, int HHID, string PAPUID, string papName, string plotReference, string district, string county, string subCounty, string parish, string village)
        {
            return (new PAP_HouseholdDAL()).SearchPAPForALL(recentRecords, projectID, HHID, PAPUID, papName, plotReference, district, county, subCounty, parish, village);
        }
        
        /// <summary>
        /// To Get PAP Name By Village
        /// </summary>
        /// <param name="Village"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GetPAPNameByVillage(string Village)
        {
            return (new PAP_HouseholdDAL()).GetPAPNameByVillage(Village);
        }

        /// <summary>
        /// To Approval Change request Status
        /// </summary>
        /// <param name="objHouseHold"></param>
        /// <returns></returns>
        public PAP_HouseholdBO ApprovalChangerequestStatus(PAP_HouseholdBO objHouseHold)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.ApprovalChangerequestStatus(objHouseHold);
        }

        /// <summary>
        /// To Change Request Status
        /// </summary>
        /// <param name="objHouseHold"></param>
        /// <returns></returns>
        public int ChangeRequestStatus(PAP_HouseholdBO objHouseHold)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.ChangeRequestStatus(objHouseHold);
        }

        /// <summary>
        /// To Is PDP
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public string IsPDP(int householdID)
        {
            PAP_HouseholdDAL objHouseHOldDAL = new PAP_HouseholdDAL();
            return objHouseHOldDAL.IsPDP(householdID);
        }

        /// <summary>
        /// To Check Is Resident 
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public bool IsResident(int householdID)
        {
            bool resident = false;

            PAP_HouseholdBO objHouseholdBO = (new PAP_HouseholdDAL()).GetHouseHoldData(householdID);

            if (objHouseholdBO != null)
            {
                if (objHouseholdBO.Isresident.ToUpper() == "YES") resident = true;
            }
            objHouseholdBO = null;

            return resident;
        }

        /// <summary>
        /// To Get Plot Dependents
        /// </summary>
        /// <param name="HHID_"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList GetPlotDependents(int HHID_)
        {
            return (new PAP_HouseholdDAL()).GetPlotDependents(HHID_);
        }

        /// <summary>
        /// To Search PAP ON HHID
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="HHID"></param>
        /// <param name="PAPUID"></param>
        /// <returns></returns>
        public Trn_Pap_HouseHoldList SearchPAPONHHID(int projectID, string HHID, string PAPUID)
        {
            return (new PAP_HouseholdDAL()).SearchPAPONHHID(projectID, HHID, PAPUID);
        }


        public CompensationPackagesList getprintComments(int HHId_)
        {
            PAP_HouseholdDAL objOptionGroupCommentsDAL = new PAP_HouseholdDAL();
            return objOptionGroupCommentsDAL.getprintComments(HHId_);
        }
    }
}