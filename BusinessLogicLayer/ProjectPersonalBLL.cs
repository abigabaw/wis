using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ProjectPersonalBLL
    {
        /// <summary>
        /// To Get Users
        /// </summary>
        /// <returns></returns>
        public ProjectPersonalList GetUsers()
        {
            ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
            return objPPDAL.GetUsers();
        }

        /// <summary>
        /// To Get Project Personnel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectPersonalList GetProjectPersonnel(int projectID)
        {
            ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
            return objPPDAL.GetProjectPersonnel(projectID);
        }

        //Edwin: 30MAY2016 To retrieve all users
        public ProjectPersonalList GetAllPersonnel(int projectID)
        {
            ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
            return objPPDAL.GetAllPersonnel(projectID);
        }

        /// <summary>
        /// To Add Personal
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="objPPList"></param>
        public void AddPersonal(int projectID, ProjectPersonalList objPPList)
        {
            {
                ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
                objPPDAL.AddPersonal(projectID, objPPList);
            }
        }

        /// <summary>
        /// To Check User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string CheckUser(int UserID, int ProjectId)
        {
            ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
            return objPPDAL.CheckUser(UserID, ProjectId);
        }

        /// <summary>
        /// To Get Project Personnel
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ProjectPersonalList GetProjectOtherPersonnel(int projectID, int UserId)
        {
            ProjectpersonalDAL objPPDAL = new ProjectpersonalDAL();
            return objPPDAL.GetProjectOtherPersonnel(projectID,UserId);
        }
    }
}