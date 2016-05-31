using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class DashboardBLL
    {
        /// <summary>
        /// To Get Recent PAPS By User
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_RecentPAPSList GetRecentPAPSByUser(int userID)
        {
            return (new DashboardDAL()).GetRecentPAPSByUser(userID);
        }

        /// <summary>
        /// To Get Recent Project
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetRecentProject(int userID)
        {
            return (new DashboardDAL()).GetRecentProject(userID);
        }

        /// <summary>
        /// To Get Projects
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjects(int userID)
        {
            return (new DashboardDAL()).GetProjects(userID);
        }

        /// <summary>
        /// To Get Project wise PAP Status
        /// </summary>
        /// <param name="PROJECTID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPStatus(int PROJECTID)
        {
            return (new DashboardDAL()).GetProjectwisePAPStatus(PROJECTID);
        }


        /// <summary>
        /// To Get Project wise PAP Status For Pie
        /// </summary>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPStatusForPie()
        {
            return (new DashboardDAL()).GetProjectwisePAPStatusForPie();
        }

        /// <summary>
        /// To Get Project wise PAP Budget For Spline
        /// </summary>
        /// <param name="PROJECTID"></param>
        /// <returns></returns>
        public DSH_PAPStatusList GetProjectwisePAPBudgetForSpline(int PROJECTID)
        {
            return (new DashboardDAL()).GetProjectwisePAPBudgetForSpline(PROJECTID);
        }
    }
}
