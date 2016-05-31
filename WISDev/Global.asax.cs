using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;

namespace WIS
{
    public partial class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        /// <summary>
        /// Catch erros
        /// Write Errors in Error log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Error(object sender, EventArgs e)
       {
           // Code that runs when an unhandled error occurs
           Exception ex = Server.GetLastError();
           //Notifies technical team about the error
           // for get errors   

           ////if (Request.Url.PathAndQuery != "/Images/checkbox.png")
           ////{
           ////    string fileLoc = System.Configuration.ConfigurationManager.AppSettings.Get("ErrorFilePath");

           ////    FileStream fs = null;
           ////    if (!File.Exists(fileLoc))
           ////    {
           ////        using (fs = File.Create(fileLoc))
           ////        {

           ////        }
           ////    }
           ////    using (StreamWriter sw = new StreamWriter(fileLoc, true))
           ////    {
           ////        sw.WriteLine(Environment.NewLine + "[" + DateTime.Now.ToString() + "] - " + Path.GetFileName(Request.Url.PathAndQuery));
           ////        sw.WriteLine(ex.ToString());
           ////    }
           ////    //end
           ////    try
           ////    {
           ////        string spath = VirtualPathUtility.ToAbsolute("~/ErrorPage.aspx",
           ////                   HttpContext.Current.Request.ApplicationPath);
           ////        Server.Transfer(string.Format(spath + "?aspxerrorpath={0}", Request.Url.PathAndQuery));
           ////    }
           ////    catch
           ////    {
           ////        //try
           ////        //{
           ////        //    Server.Transfer(string.Format("../../ErrorPage.aspx?aspxerrorpath={0}", Request.Url.PathAndQuery));
           ////        //}
           ////        //catch {
           ////        //    try
           ////        //    {
           ////        //        Server.Transfer(string.Format("../ErrorPage.aspx?aspxerrorpath={0}", Request.Url.PathAndQuery));

           ////        //    }
           ////        //    catch
           ////        //    {
           ////        //        Server.Transfer(string.Format("../../../ErrorPage.aspx?aspxerrorpath={0}", Request.Url.PathAndQuery));
           ////        //    }
           ////        //}
           ////    }
           ////}
           ////Server.ClearError();

           //Response.Clear();
           //Response.Redirect(string.Format("ErrorPage.aspx?aspxerrorpath={0}", Request.Url.PathAndQuery));
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
