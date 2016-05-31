using System;
using WIS_DataAccess;
using WIS_BusinessObjects;
using WIS_BusinessObjects.Collections;

namespace WIS_BusinessLogic
{
    public class GOUAllowanceBLL
    {
        /// <summary>
        /// To Insert into Databse
        /// </summary>
        /// <param name="GOUAllowanceBOobj"></param>
        /// <returns></returns>
        public string Insert(GOUAllowanceBO GOUAllowanceBOobj)
        {


            GOUAllowanceDAL GOUAllowanceDALobj = new GOUAllowanceDAL(); //Data pass -to Database Layer

            try
            {

                return GOUAllowanceDALobj.Insert(GOUAllowanceBOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GOUAllowanceDALobj = null;
            }
        }

        /// <summary>
        /// To Get Gou Allowance
        /// </summary>
        /// <returns></returns>
        public GOUAllowanceList GetGouAllowance()
        {
            GOUAllowanceDAL GOUAllowanceDALobj = new GOUAllowanceDAL();
            return GOUAllowanceDALobj.GetGOUAllowance();
        }

        /// <summary>
        /// To Get All Gou Allowance
        /// </summary>
        /// <returns></returns>
        public GOUAllowanceList GetAllGouAllowance()
        {
            return (new GOUAllowanceDAL()).GetAllSchoolGOUAllowance();
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Delete(int reasonid)
        {
            GOUAllowanceDAL GOUAllowanceDALobj = new GOUAllowanceDAL(); //Data pass -to Database Layer
            try
            {
                return GOUAllowanceDALobj.Delete(reasonid);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                GOUAllowanceDALobj = null;
            }
        }

        /// <summary>
        /// To Obsolete
        /// </summary>
        /// <param name="reasonid"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string Obsolete(int reasonid, string IsDeleted)
        {
            return (new GOUAllowanceDAL()).Obsolete(reasonid, IsDeleted);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="GOUAllowanceBOobj"></param>
        /// <param name="reasonid"></param>
        /// <returns></returns>
        public string Update(GOUAllowanceBO GOUAllowanceBOobj, int reasonid)
        {
            GOUAllowanceDAL GOUAllowanceDALobj = new GOUAllowanceDAL();
            try
            {
                return GOUAllowanceDALobj.Update(GOUAllowanceBOobj, reasonid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                GOUAllowanceDALobj = null;
            }

        }

        /// <summary>
        /// To Get Gou Allowance by ID
        /// </summary>
        /// <param name="GouAllowanceID"></param>
        /// <returns></returns>
        public GOUAllowanceBO GetGouAllowancebyID(int GouAllowanceID)
        {
            try
            {

                GOUAllowanceDAL GOUAllowanceDALobj = new GOUAllowanceDAL();
                return GOUAllowanceDALobj.GetGOUAllowancebyID(GouAllowanceID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
