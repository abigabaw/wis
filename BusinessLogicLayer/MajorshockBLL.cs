using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
   public class MajorshockBLL
    {
       /// <summary>
        /// To Get Type of shock
       /// </summary>
       /// <param name="SHOCKID"></param>
       /// <returns></returns>
        public DataSet GetTypeofshock(string SHOCKID)
        {
            MajorshockDAL Typeofshock_DAL = new MajorshockDAL();
            try
            {
                return Typeofshock_DAL.GetTypeofshock(SHOCKID);
            }
            catch
            {
                throw;
            }
            finally
            {
                Typeofshock_DAL = null;
            }
        }

       /// <summary>
        /// To Get Cop Mech
       /// </summary>
       /// <param name="COP_MECHANISMID"></param>
       /// <returns></returns>
        public DataSet GetCopMech(string COP_MECHANISMID)
        {
            MajorshockDAL copmech_DAL = new MajorshockDAL();
            try
            {
                return copmech_DAL.GetCopMech(COP_MECHANISMID);
            }
            catch
            {
                throw;
            }
            finally
            {
                copmech_DAL = null;
            }
        }

       /// <summary>
        /// To Get help
       /// </summary>
       /// <param name="SUPPORTID"></param>
       /// <returns></returns>
        public DataSet Gethelp(string SUPPORTID)
        {
            MajorshockDAL help_DAL = new MajorshockDAL();
            try
            {
                return help_DAL.Gethelp(SUPPORTID);
            }
            catch
            {
                throw;
            }
            finally
            {
                help_DAL = null;
            }
        }

       /// <summary>
        /// To Insert into Database
       /// </summary>
       /// <param name="Majorshockobj"></param>
       /// <returns></returns>
        public string Insert(MajorshockBO Majorshockobj)
        {
            MajorshockDAL MajorshockDALobj = new MajorshockDAL(); //Data pass -to Database Layer
            return MajorshockDALobj.Insert(Majorshockobj);
        }

       /// <summary>
        /// To Get Major shock
       /// </summary>
       /// <param name="householdID"></param>
       /// <returns></returns>
        public major_shockList GetMshock(int householdID)
        {
            MajorshockDAL MajorshockDALobj = new MajorshockDAL();
            return MajorshockDALobj.GetMshock(householdID);
        }

       /// <summary>
        /// To Get Pap Shoch Id
       /// </summary>
       /// <param name="PAP_SHOCKID1"></param>
       /// <returns></returns>
        public MajorshockBO GetPapShochId(int PAP_SHOCKID1)
        {
            MajorshockDAL MajorshockDALobj = new MajorshockDAL();
            return MajorshockDALobj.GetPapShochId(PAP_SHOCKID1);
        }

       /// <summary>
        /// To EDIT Major shock
       /// </summary>
       /// <param name="Majorshockobj"></param>
       /// <returns></returns>
        public string EDITMshock(MajorshockBO Majorshockobj)
        {
            MajorshockDAL MajorshockDALobj = new MajorshockDAL(); //Data pass -to Database Layer
            return MajorshockDALobj.EDITMshock(Majorshockobj);
        }

       /// <summary>
        /// To Delete from Database
       /// </summary>
       /// <param name="PAP_SHOCKID1"></param>
       /// <returns></returns>
        public int Delete(int PAP_SHOCKID1)
        {
            MajorshockDAL MajorshockDALobj = new MajorshockDAL(); //Data pass -to Database Layer
            try
            {
                return MajorshockDALobj.Delete(PAP_SHOCKID1);
            }
            catch 
            {

                throw ;
            }
            finally
            {
                MajorshockDALobj = null;
            }
        }
    }
}
