using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class CompensationPackagesBLL
    {
        #region Declaration Scetion
        CompensationPackagesDAL objCOMPACKDAL;
        #endregion

        /// <summary>
        /// To Get Componestion by Id
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public CompensationPackagesList GetComponestionbyId(int HHID)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.GetComponestionbyId(HHID);
        }

        /// <summary>
        /// To get Componestion
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PackageCat"></param>
        /// <param name="USERID"></param>
        /// <returns></returns>
        public CompensationPackagesList getComponestion(int householdID, int PackageCat, int USERID)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.GetComponestion(householdID, PackageCat, USERID);
        }

        /// <summary>
        /// To Update Approval Status
        /// </summary>
        /// <param name="objCompensationPackagesBO"></param>
        /// <returns></returns>
        public int UpdateApprovalStatus(CompensationPackagesBO objCompensationPackagesBO)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.UpdateApprovalStatus(objCompensationPackagesBO);
        }

        //used in Approval screen Popup window
        /// <summary>
        /// To Get Componestion by HHId
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public CompensationPackagesList GetComponestionbyHHId(int householdID)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.GetComponestionbyHHId(householdID);
        }

        /// <summary>
        /// To Save Approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public int SaveApprovalComments(CompensationPackagesBO objCOMPPACKBO)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.SaveApprovalComments(objCOMPPACKBO);
        }

        /// <summary>
        /// To get approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public CompensationPackagesList getApproverReviewComments(CompensationPackagesBO pCompensationPackagesBO)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.getApproverReviewComments(pCompensationPackagesBO);
        }

        /// <summary>
        /// To get approval Comments
        /// </summary>
        /// <param name="objCOMPPACKBO"></param>
        /// <returns></returns>
        public CompensationPackagesBO getapprovalComments(CompensationPackagesBO objCOMPPACKBO)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.getapprovalComments(objCOMPPACKBO);
        }

        /// <summary>
        /// To get pre Comments
        /// </summary>
        /// <param name="cmppkgBo"></param>
        /// <returns></returns>
        public CompensationPackagesBO getpreComments(CompensationPackagesBO cmppkgBo)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.getpreComments(cmppkgBo);
        }

        /// <summary>
        /// To Save reprint Comments
        /// </summary>
        /// <param name="cmppkgBo"></param>
        /// <returns></returns>
        public int SavereprintComments(CompensationPackagesBO cmppkgBo)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.SavereprintComments(cmppkgBo);
        }

        /// <summary>
        /// To Update Doc Read Status
        /// </summary>
        /// <param name="DocItemId"></param>
        /// <param name="Status"></param>
        /// <param name="UID"></param>
        /// <param name="HHID"></param>
        public void UpdateDocReadStatus(int DocItemId, string Status, int UID, int HHID)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            objCOMPACKDAL.UpdateDocReadStatus(DocItemId, Status, UID, HHID);
        }
        public CompensationPackagesList getprintComments(int HHId_)
        {
            objCOMPACKDAL = new CompensationPackagesDAL();
            return objCOMPACKDAL.getprintComments(HHId_);


        }
    }
}