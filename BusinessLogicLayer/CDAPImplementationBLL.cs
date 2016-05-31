using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class CDAPImplementationBLL
    {
        /// <summary>
        /// To Add CDAP Phase to database
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public string AddCDAPPhase(CDAPImplementationBO objCDAPImplementationBO)
        {
            return (new CDAPImplementationDAL()).AddCDAPPhase(objCDAPImplementationBO);
        }
        /// <summary>
        /// To Get CDAP Phase Details By ID
        /// </summary>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseDetailsByID(int PhaseID)
        {
            return (new CDAPImplementationDAL()).GetCDAPPhaseDetailsByID(PhaseID);
        }
        /// <summary>
        /// To Get CDAP Phase Details
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseDetails(int ProjectID)
        {
            return (new CDAPImplementationDAL()).GetCDAPPhaseDetails(ProjectID);
        }
        /// <summary>
        /// To Delete Phase details By ID
        /// </summary>
        /// <param name="PhaseID"></param>
        public void DeletePhasedetailsByID(int PhaseID)
        {
            (new CDAPImplementationDAL()).DeletePhasedetailsByID(PhaseID);
        }
        /// <summary>
        /// To Get CDAP Phase ID
        /// </summary>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseID()
        {
            return (new CDAPImplementationDAL()).GetCDAPPhaseID();
        }
        /// <summary>
        /// To Add CDAP Phase Activity to database
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public int AddCDAPPhaseActivity(CDAPImplementationBO objCDAPImplementationBO)
        {
            return (new CDAPImplementationDAL()).AddCDAPPhaseActivity(objCDAPImplementationBO);
        }
        /// <summary>
        /// To Get CDAP Phase Activity by ID
        /// </summary>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseActivityID()
        {
            return (new CDAPImplementationDAL()).GetCDAPPhaseActivityID();
        }
        /// <summary>
        /// To Add CDAP Activity PAPS details
        /// </summary>
        /// <param name="objCDAPImplementationBO"></param>
        /// <returns></returns>
        public int AddCDAPActivityPAPS(CDAPImplementationBO objCDAPImplementationBO)
        {
            return (new CDAPImplementationDAL()).AddCDAPActivityPAPS(objCDAPImplementationBO);
        }
        /// <summary>
        /// To Get CDAP Phase Activity Details
        /// </summary>
        /// <param name="prjctID"></param>
        /// <param name="PhaseID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPhaseActivityDetails(int prjctID, int PhaseID)
        {
            return (new CDAPImplementationDAL()).GetCDAPPhaseActivityDetails(prjctID, PhaseID);
        }
        /// <summary>
        /// To Get CDAP Phase Activity Details
        /// </summary>
        /// <param name="PhaseactivityID"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPPAPDetails(int PhaseactivityID)
        {
            return (new CDAPImplementationDAL()).GetCDAPPAPDetails(PhaseactivityID);
        }

        /// <summary>
        /// To Get CDAP Village ID
        /// </summary>
        /// <param name="village"></param>
        /// <returns></returns>
        public CDAPImplementationList GetCDAPVillageID(string village)
        {
            return (new CDAPImplementationDAL()).GetCDAPVillageID(village);
        }
    }
}
