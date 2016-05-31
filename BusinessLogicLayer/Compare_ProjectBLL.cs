using System;
using WIS_DataAccess;
using WIS_BusinessObjects;



namespace WIS_BusinessLogic
{
   public class Compare_ProjectBLL
    {
        /// <summary>
        /// To fetch Project name from database
        /// </summary>
        /// <param name="PROJECTID"></param>
        /// <returns></returns>
       public Compare_projectList Getprojectname(string PROJECTID)
       {
           Compare_ProjectDAL Compare_ProjectDAL = new Compare_ProjectDAL();
           try
           {
               return Compare_ProjectDAL.Getprojectname(PROJECTID);
           }
           catch
           {
               throw;
           }
           finally
           {
               Compare_ProjectDAL = null;
           }
       }

       /// <summary>
       /// To Get data from database
       /// </summary>
       /// <param name="Compare_projectBOObj"></param>
       /// <returns></returns>

       public Compare_projectList Getdata(Compare_projectBO Compare_projectBOObj)
       {
           Compare_ProjectDAL Compare_ProjectDAL = new Compare_ProjectDAL();
           try
           {
               return Compare_ProjectDAL.Getdata(Compare_projectBOObj);
           }
           catch
           {
               throw;
           }
           finally
           {
               Compare_ProjectDAL = null;
           }
       }
    }
}
