using System;
using System.Collections.Generic;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class NeverAttendedSchoolBLL
    {
        /// <summary>
        /// To Insert Into Never Attended School
        /// </summary>
        /// <param name="NeverAttendedSchoolBOObj"></param>
        /// <returns></returns>
        public string InsertIntoNeverAttendedSchool(NeverAttendedSchoolBO NeverAttendedSchoolBOObj)
        {
           
            NeverAttendedSchoolDAL objNeverAttendedSchoolDAL = new NeverAttendedSchoolDAL();
            try
            {
                return objNeverAttendedSchoolDAL.InsertIntoNeverAttendedSchool(NeverAttendedSchoolBOObj);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objNeverAttendedSchoolDAL = null;
            }
        }


        //public List<NeverAttendedSchoolBO> FetchGridViewDetails()
        //{
        //    NeverAttendedSchoolDAL objNeverAttendedSchoolDAL = new NeverAttendedSchoolDAL();
        //    try
        //    {
        //        return objNeverAttendedSchoolDAL.FetchGridViewDetails();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //objNeverAttendedSchoolDAL = null;
        //    }
        //}

        /// <summary>
        /// To Get All Never Attended School
        /// </summary>
        /// <returns></returns>
        public NeverAttendedSchoolList GetAllNeverAttendedSchool()
        {
            return (new NeverAttendedSchoolDAL()).GetAllNeverAttendedSchool();
        }

        /// <summary>
        /// To Fetch Never Attended School
        /// </summary>
        /// <returns></returns>
        public NeverAttendedSchoolList FetchNeverAttendedSchool()
        {
            NeverAttendedSchoolDAL NeverAttendedSchoolDALObj = new NeverAttendedSchoolDAL();
            return NeverAttendedSchoolDALObj.FetchNeverAttendedSchool();
        }

        /// <summary>
        /// To Get NA School By Id
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <returns></returns>
        public NeverAttendedSchoolBO GetNASchoolById(int NASchoolID)
        {
            NeverAttendedSchoolDAL NeverAttendedSchoolDALObj = new NeverAttendedSchoolDAL();
            return NeverAttendedSchoolDALObj.GetNASchoolById(NASchoolID);
        }

        //public int DeleteNASchoolById(int NASchoolID)
        //{
        //    NeverAttendedSchoolDAL NeverAttendedSchoolDALObj = new NeverAttendedSchoolDAL();
        //    return NeverAttendedSchoolDALObj.DeleteNASchoolById(NASchoolID);
        //}

        /// <summary>
        /// To Delete NA School By Id
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <returns></returns>
        public string DeleteNASchoolById(int NASchoolID)
        {
            NeverAttendedSchoolDAL NeverAttendedSchoolDALObj = new NeverAttendedSchoolDAL();
            return NeverAttendedSchoolDALObj.DeleteNASchoolById(NASchoolID);
        }

        /// <summary>
        /// To Obsolete NA School
        /// </summary>
        /// <param name="NASchoolID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteNASchool(int NASchoolID, string IsDeleted)
        {
            return (new NeverAttendedSchoolDAL()).ObsoleteNASchool(NASchoolID, IsDeleted);
        }

        /// <summary>
        /// To EDIT NA SCHOOL
        /// </summary>
        /// <param name="NeverAttendedSchoolBOObj"></param>
        /// <returns></returns>
        public string EDITNASCHOOL(NeverAttendedSchoolBO NeverAttendedSchoolBOObj)
        {
            NeverAttendedSchoolDAL NeverAttendedSchoolDALObj = new NeverAttendedSchoolDAL(); 

            try
            {
                return NeverAttendedSchoolDALObj.EDITNASCHOOL(NeverAttendedSchoolBOObj);
            }
            catch
            {
                throw;
            }
            finally
            {
                NeverAttendedSchoolDALObj = null;
            }
        }


    }
}