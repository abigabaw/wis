using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class GraveBLL
    {
        /// <summary>
        /// To Get Grave Finish
        /// </summary>
        /// <returns></returns>
        public GraveList GetGraveFinish()
        {
            GraveDAL graveDAL = new GraveDAL();
            try
            {
                return graveDAL.GetGraveFinish();
            }
            catch
            {
                throw;
            }
            finally
            {
                graveDAL = null;
            }
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="GraveBOobj"></param>
        /// <returns></returns>
        public int Insert(GraveBO GraveBOobj)
        {
            GraveDAL gravesaveDALobj = new GraveDAL(); //Data pass -to Database Layer

            try
            {
                return gravesaveDALobj.Insert(GraveBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                gravesaveDALobj = null;
            }
        }

        /// <summary>
        /// To Get Grave data
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public GraveList GetGravedata(int householdID)
        {
            GraveDAL gravedataDALobj = new GraveDAL(); //Data pass -to Database Layer
            return gravedataDALobj.GetGravedata(householdID);
        }

        /// <summary>
        /// To Get data row
        /// </summary>
        /// <param name="Pap_graveid"></param>
        /// <returns></returns>
        public GraveBO Getdatarow(int Pap_graveid)
        {
            GraveDAL gravedatarowDALobj = new GraveDAL(); //Data pass -to Database Layer
            return gravedatarowDALobj.Getdatarow(Pap_graveid);
        }

        /// <summary>
        /// To Edit GRAVE
        /// </summary>
        /// <param name="GraveBOobj"></param>
        /// <returns></returns>
        public int EditGRAVE(GraveBO GraveBOobj)
        {
            GraveDAL gravedataDALobj = new GraveDAL(); //Data pass -to Database Layer

            try
            {
                return gravedataDALobj.EditGRAVE(GraveBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                gravedataDALobj = null;
            }
        }

        /// <summary>
        /// To Delete from Database
        /// </summary>
        /// <param name="Pap_graveid"></param>
        /// <returns></returns>
        public int Delete(int Pap_graveid)
        {
            GraveDAL gravedelDALobj = new GraveDAL(); //Data pass -to Database Layer
            try
            {
                return Convert.ToInt16(gravedelDALobj.Delete(Pap_graveid));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                gravedelDALobj = null;
            }
        }

        /// <summary>
        /// To Show PAP GRAVE
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public GraveBO ShowPAPGRAVE(int householdID, int PermanentStructureID)
        {
            return (new GraveDAL()).ShowPAPGRAVE(householdID, PermanentStructureID);
        }
    }
}
    

