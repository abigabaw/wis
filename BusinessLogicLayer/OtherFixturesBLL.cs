using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class OtherFixturesBLL
    {
        /// <summary>
        /// To Edit Fence
        /// </summary>
        /// <param name="FenceBOobj"></param>
        /// <returns></returns>
        public int EditFence(OtherFenceBO FenceBOobj)
        {
            OtherFenceDAL FenceupdateDALobj = new OtherFenceDAL(); //Data pass -to Database Layer

            try
            {
                return FenceupdateDALobj.EditFence(FenceBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                FenceupdateDALobj = null;
            }
        }

        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="FenceBOobj"></param>
        /// <returns></returns>
        public int Insert(OtherFenceBO FenceBOobj)
        {
            OtherFenceDAL fencesaveDALobj = new OtherFenceDAL(); //Data pass -to Database Layer

            try
            {
                return fencesaveDALobj.Insert(FenceBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                fencesaveDALobj = null;
            }
        }

        /// <summary>
        /// To Get Fence Data
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public OtherFenceList GetFencedata(int householdID)
        {
            OtherFenceDAL fencedataDALobj = new OtherFenceDAL();
            return fencedataDALobj.GetFencedata(householdID);
        }

        /// <summary>
        /// To Delete from Database
        /// </summary>
        /// <param name="Pap_fenceid"></param>
        /// <returns></returns>
        public int Delete(int Pap_fenceid)
        {
            OtherFenceDAL fencedelDALobj = new OtherFenceDAL();
            try
            {
                return Convert.ToInt16(fencedelDALobj.Delete(Pap_fenceid));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                fencedelDALobj = null;

            }
        }

        /// <summary>
        /// To Get fence data row
        /// </summary>
        /// <param name="Pap_fenceid"></param>
        /// <returns></returns>
        public OtherFenceBO Getfencedatarow(int Pap_fenceid)
        {
            OtherFenceDAL fencedatarowDALobj = new OtherFenceDAL();//Data pass -to Database Layer
            return fencedatarowDALobj.Getfencedatarow(Pap_fenceid);
        }

        /// <summary>
        /// To Show PAPOHFIX Image
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public OtherFenceBO ShowPAPOHFIXImage(int householdID, int PermanentStructureID)
        {
            return (new OtherFenceDAL()).ShowPAPOHFIXImage(householdID, PermanentStructureID);
        }
    }
}
