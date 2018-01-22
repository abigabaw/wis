using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class LoginBLL
    {
        /// <summary>
        /// To Authenticate username & password
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginBO Authentication(string username, string password)
        {
            LoginDAL LoginDALObj = new LoginDAL();
            return LoginDALObj.Authentication(username, password);
        }

        //Edwin: 22/01/2018 - Global Password
        public LoginBO DBAuth(string username, string password)
        {
            LoginDAL LoginDALObj = new LoginDAL();
            return LoginDALObj.Authentication(username, password);
        }
    }
}