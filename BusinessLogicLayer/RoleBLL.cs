using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class RoleBLL
    {
        /// <summary>
        /// To Get Role
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public RoleList  GetRoles(string RoleName)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.GetRole(RoleName);           
        }

        /// <summary>
        /// To Get All Role
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public RoleList GetAllRoles(string RoleName)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.GetAllRole(RoleName);
        }

        /// <summary>
        /// To Add Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public string AddRole(RoleBO objRole)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.AddRole(objRole);
        }

        /// <summary>
        /// To Delete Role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string DeleteRole(int roleID)
        {
            RoleDAL objRoleDAL = new RoleDAL();
           return objRoleDAL.DeleteRole(roleID);
        }

        /// <summary>
        /// To Obsolete Role
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteRole(int roleID, string IsDeleted)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.ObsoleteRole(roleID, IsDeleted);
        }

        /// <summary>
        /// To Update Role
        /// </summary>
        /// <param name="objRole"></param>
        /// <returns></returns>
        public string UpdateRole(RoleBO objRole)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.UpdateRole(objRole);
        }

        /// <summary>
        /// To Get Role By Role ID
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public RoleBO GetRoleByRoleID(int roleID)
        {
            RoleDAL objRoleDAL = new RoleDAL();
            return objRoleDAL.GetRoleByRoleID(roleID);
        }    
        
    }
}