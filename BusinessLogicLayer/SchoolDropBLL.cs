using System;
using WIS_DataAccess;
using WIS_BusinessObjects;

namespace WIS_BusinessLogic
{
    public class SchoolDropBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="SchoolDropReasonBOobj"></param>
        /// <returns></returns>
        public string Insert(SchoolDropReasonBO SchoolDropReasonBOobj)
        {

            SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL(); //Data pass -to Database Layer

            try
            {

                return Schooldropobj.Insert(SchoolDropReasonBOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Schooldropobj = null;
            }
        }

        /// <summary>
        /// To Get school Drop Reason
        /// </summary>
        /// <returns></returns>
        public SchoolDropReasonList GetschoolDropReason()
        {
            SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL();
            return Schooldropobj.GetschoolDropReason();
        }

        /// <summary>
        /// To Get All School Drop Reason
        /// </summary>
        /// <returns></returns>
        public SchoolDropReasonList GetAllSchoolDropReason()
        {
            return (new SchoolDropReasonDAL()).GetAllSchoolDropReason();
        }

        // public int  Delete(int reasonid)
        //{
        //    SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL(); //Data pass -to Database Layer
        //    try
        //    {
        //        return Convert.ToInt16(Schooldropobj.Delete(reasonid));
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        Schooldropobj = null;
        //    }
        //}

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Delete(int reasonid)
        {
            SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL(); //Data pass -to Database Layer
            try
            {
                return Schooldropobj.Delete(reasonid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Schooldropobj = null;
            }
        }

        /// <summary>
        /// To Obsolete School Drop
        /// </summary>
        /// <param name="reasonid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteSchoolDrop(int reasonid, string IsDeleted)
        {
            return (new SchoolDropReasonDAL()).ObsoleteSchoolDrop(reasonid, IsDeleted);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="SchoolDropReasonBOobj"></param>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Update(SchoolDropReasonBO SchoolDropReasonBOobj, int reasonid)
        {
            SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL();
            try
            {
                return Schooldropobj.Update(SchoolDropReasonBOobj, reasonid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Schooldropobj = null;
            }

        }

        /// <summary>
        /// To Get school Drop Reason by ID
        /// </summary>
        /// <param name="SchooldropreasonID"></param>
        /// <returns></returns>
        public SchoolDropReasonBO GetschoolDropReasonbyID(int SchooldropreasonID)
        {
            try
            {
                SchoolDropReasonDAL Schooldropobj = new SchoolDropReasonDAL();
                return Schooldropobj.GetschoolDropReasonbyID(SchooldropreasonID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}