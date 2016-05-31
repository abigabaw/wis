using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class UserBLL
    {
        UserDAL objUserDAL;

        //public UserList Get_All_Users()
        //{
        //    objUserDAL = new UserDAL();
        //    return objUserDAL.Get_All_Users();
        //}

        /// <summary>
        /// To Get All Users
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public UserList GetAllUsers(UserBO oUser)
        {
            objUserDAL = new UserDAL();
            return objUserDAL.GetAllUsers(oUser);
        }

        /// <summary>
        /// To Get Users
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public UserList GetUsers(UserBO oUser)
        {
            objUserDAL = new UserDAL();
            return objUserDAL.GetUsers(oUser);
        }

        /// <summary>
        /// To Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string AddUser(UserBO user)
        {
            objUserDAL = new UserDAL();

            return objUserDAL.SaveUser(user);
        }

        /// <summary>
        /// To Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string UpdateUser(UserBO user)
        {
            objUserDAL = new UserDAL();

            return objUserDAL.UpdateUser(user);
        }

        /// <summary>
        /// To Get User By Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserBO GetUserById(int UserId)
        {
            objUserDAL = new UserDAL();
            return objUserDAL.GetUserById(UserId);
        }

        /// <summary>
        /// To Delete User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string DeleteUser(int UserId)
        {
            objUserDAL = new UserDAL();
            return objUserDAL.DeleteUser(UserId);
        }

        /// <summary>
        /// To Obsolete User
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteUser(int UserId,string IsDeleted)
        {
            objUserDAL=new UserDAL();
            return objUserDAL.ObsoleteUser(UserId,IsDeleted);
            
        }
    }
}