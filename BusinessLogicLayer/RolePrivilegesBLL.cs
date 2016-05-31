using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class RolePrivilegesBLL
    {
        /// <summary>
        /// To Get Role Privileges
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetRolePrivileges()
        {
            RolePrivilegesDAL RolePrivilegesDALObj = new RolePrivilegesDAL();
            return RolePrivilegesDALObj.GetRolePrivileges();
        }

        /// <summary>
        /// To Insert Role Privilages
        /// </summary>
        /// <param name="RolePrivilegesList"></param>
        /// <returns></returns>
        public int InsertRolePrivilages(RolePrivilegesBO RolePrivilegesList)
        {
            RolePrivilegesDAL RolePrivilegesDAL = new RolePrivilegesDAL(); //Data pass -to Database Layer

            try
            {
                return RolePrivilegesDAL.InsertRolePrivilages(RolePrivilegesList);
            }
            catch
            {
                throw;
            }
            finally
            {
                RolePrivilegesDAL = null;
            }
        }

        /// <summary>
        /// To Get ROLE PRI Id
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public RolePrivilegesList GetROLEPRIId(int UserID)
        {
            RolePrivilegesDAL RolePrivilegesDAL = new RolePrivilegesDAL(); //Data pass -to Database Layer

            try
            {
                return RolePrivilegesDAL.GetROLEPRIId(UserID);
            }
            catch
            {
                throw;
            }
            finally
            {
                RolePrivilegesDAL = null;
            }
        }

        /// <summary>
        /// To Delete Role Privileges
        /// </summary>
        /// <param name="DeletedID"></param>
        /// <returns></returns>
        public int DeleteRolePrivileges(int DeletedID)
        {
            RolePrivilegesDAL RolePrivilegesDALObj = new RolePrivilegesDAL();
            return RolePrivilegesDALObj.DeleteRolePrivileges(DeletedID);
        }
    }
}
