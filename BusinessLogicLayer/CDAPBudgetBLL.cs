using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class CDAPBudgetBLL
    {
        /// <summary>
        /// To Add CDAP Budget into database
        /// </summary>
        /// <param name="objCDAPBudgetBO"></param>
        /// <returns></returns>
        public string AddCDAPBudget(CDAPBudgetBO objCDAPBudgetBO)
        {
            return (new CDAPBudgetDAL()).AddCDAPBudget(objCDAPBudgetBO);
        }
        /// <summary>
        /// To Get CDAP Budget from database
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public CDAPBudgetList GetCDAPBudget(int ProjectID, string Status)
        {
            CDAPBudgetDAL CDAPBudgetDAL = new CDAPBudgetDAL();
            return CDAPBudgetDAL.GetCDAPBudget(ProjectID, Status);
        }
        /// <summary>
        /// To Get CDAP Budget Item from database
        /// </summary>
        /// <param name="cdap_budgid"></param>
        /// <returns></returns>
        public CDAPBudgetBO GetCDAPBudgetItem(int cdap_budgid)
        {
            return (new CDAPBudgetDAL()).GetCDAPBudgetItem(cdap_budgid);
        }
        /// <summary>
        /// To Delete CDAP Budget details
        /// </summary>
        /// <param name="cdap_budgid"></param>
        /// <returns></returns>
        public int DeleteCDAPBudget(int cdap_budgid)
        {
            return (new CDAPBudgetDAL()).DeleteCDAPBudget(cdap_budgid);
        }
        /// <summary>
        /// To Send for Approval
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int SendforApproval(int projectID)
        {
            CDAPBudgetDAL CDAPBudgetDAL = new CDAPBudgetDAL();
            return CDAPBudgetDAL.SendforApproval(projectID);
       }
    }
}
