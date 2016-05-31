using WIS_BusinessObjects;
using WIS_DataAccess;
using System.Data;
using System;

namespace WIS_BusinessLogic
{
    public class GrievancesBLL
    {
        /// <summary>
        /// To get screen Intialization
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public GrievancesBO getscreenIntialization(int hhid)
        {
            GrievancesDAL GrievancesDAL = new GrievancesDAL();
            return GrievancesDAL.getscreenIntialization(hhid);
        }

        /// <summary>
        /// To Get Grievance Closure
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
        public GrievancesBO GetGrievanceClosure(int GrievanceID)
        {
             GrievancesDAL GrievancesDAL = new GrievancesDAL();
             return GrievancesDAL.GetGrievanceClosure(GrievanceID);
        }

        /// <summary>
        /// To get Grievance Over All Status
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public GrievancesBO getGrievanceOverAllStatus(int HHID)
        {
            GrievancesDAL GrievancesDAL = new GrievancesDAL();
            return GrievancesDAL.getGrievanceOverAllStatus(HHID);
        }

        /// <summary>
        /// To Get category
        /// </summary>
        /// <returns></returns>
        public GrievanceList Getcategory()
        {
            GrievancesDAL GrievancesDAL = new GrievancesDAL();
            try
            {
                return GrievancesDAL.Getcategory();
            }
            catch
            {
                throw;
            }
            finally
            {
                GrievancesDAL = null;
            }
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="GrievancesBOobj"></param>
        /// <returns></returns>
       public int Insert(GrievancesBO GrievancesBOobj)
        {
            GrievancesDAL GrievancesDALobj = new GrievancesDAL(); //Data pass -to Database Layer

            try
            {
                return GrievancesDALobj.Insert(GrievancesBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                GrievancesDALobj = null;
            }
        }

        /// <summary>
       /// To Get grievance data
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
       public GrievanceList Getgrievancedata(int householdID)
       {
           GrievancesDAL GrievancesDALobj = new GrievancesDAL(); //Data pass -to Database Layer
           return GrievancesDALobj.Getgrievancedata(householdID);
       }

        /// <summary>
        /// To Delete from Database
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
       public int Delete(int GrievanceID)
       {
           GrievancesDAL GrievancesDALobj = new GrievancesDAL(); //Data pass -to Database Layer
           try
           {
               return Convert.ToInt16(GrievancesDALobj.Delete(GrievanceID));
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally
           {
               GrievancesDALobj = null;

           }
       }

        /// <summary>
       /// To Get Grievance data row
        /// </summary>
        /// <param name="GrievanceID"></param>
        /// <returns></returns>
       public GrievancesBO GetGrievancedatarow(int GrievanceID)
       {
           GrievancesDAL GrievancesDALobj = new GrievancesDAL(); //Data pass -to Database Layer
           return GrievancesDALobj.GetGrievancedatarow(GrievanceID);
       }

        /// <summary>
       /// To Edit GRIEVANCE
        /// </summary>
        /// <param name="GrievancesBOobj"></param>
        /// <returns></returns>
       public int EditGRIEVANCE(GrievancesBO GrievancesBOobj)
       {
           GrievancesDAL GrievancesDALobj = new GrievancesDAL(); //Data pass -to Database Layer

           try
           {
               return GrievancesDALobj.EditGRIEVANCE(GrievancesBOobj);
           }
           catch
           {
               throw;
           }
           finally
           {
               GrievancesDALobj = null;
           }
       }
    }
}