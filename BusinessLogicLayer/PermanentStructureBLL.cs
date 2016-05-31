using System;
using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class PermanentStructureBLL
    {
        /// <summary>
        /// To Get Occupant status
        /// </summary>
        /// <returns></returns>
        public PermanentStructureList GetOccupantstatus()
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL();

            try
            {
                return DALobj.GetOccupantstatus();
            }
            catch (Exception erromsg)
            {
                throw (erromsg);
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Insert to Database
        /// </summary>
        /// <param name="PermanentStructureobj"></param>
        /// <returns></returns>
        public int Insert(PermanentStructureBO PermanentStructureobj)
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(PermanentStructureobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                PermanentStructureobj = null;
            }
        }


        /// <summary>
        /// To Get Permanent Structure
        /// </summary>
        /// <param name="hhid"></param>
        /// <returns></returns>
        public PermanentStructureList GetPermanentStructure(string hhid)
        {
            PermanentStructureDAL PermanentStructureDALobj = new PermanentStructureDAL();
            return PermanentStructureDALobj.GetPermanentStructure(hhid);
        }

        public void Delete(int permaneStructID)
        {
            //PermanentStructureDAL LiteracyStatusDALobj = new LiteracyStatusDAL(); //Data pass -to Database Layer
            //try
            //{
            //    return Convert.ToInt16(LiteracyStatusDALobj.Delete(literacyStatusID));
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    LiteracyStatusDALobj = null;
            //}
        }

       
        /// <summary>
        /// To GET STRUCTURE ID
        /// </summary>
        /// <param name="STRUCTUREID"></param>
        /// <returns></returns>
        public PermanentStructureBO GetSTRUCTUREID(int STRUCTUREID)
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL();
            return DALobj.GetSTRUCTUREID(STRUCTUREID);
        }

        /// <summary>
        /// To Delete Permanent Structure
        /// </summary>
        /// <param name="structurId"></param>
        /// <returns></returns>
        public int DeletePermanentStruct(string structurId)
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL();
            return DALobj.DeletePermanentStruct(structurId);
        }

        /// <summary>
        /// To Update in Database
        /// </summary>
        /// <param name="PermanentStructureobj"></param>
        /// <returns></returns>
        public int Update(PermanentStructureBO PermanentStructureobj)
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(PermanentStructureobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                PermanentStructureobj = null;
            }
        }

        /// <summary>
        /// To Get PAPPS Photo
        /// </summary>
        /// <param name="householdID"></param>
        /// <param name="PermanentStructureID"></param>
        /// <returns></returns>
        public PermanentStructureBO GetPAPPSPhoto(int householdID, int PermanentStructureID)
        {
            return (new PermanentStructureDAL()).GetPAPPSPhoto(householdID, PermanentStructureID);
        }

        /// <summary>
        /// To Update Photo
        /// </summary>
        /// <param name="PermanentStructureobj1"></param>
        /// <returns></returns>
        public int Updatephoto(PermanentStructureBO PermanentStructureobj1)
        {
            PermanentStructureDAL DALobj = new PermanentStructureDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Updatephoto(PermanentStructureobj1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                PermanentStructureobj1 = null;
            }
        }
    }
}