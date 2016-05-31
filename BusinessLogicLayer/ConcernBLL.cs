using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class ConcernBLL
    {
        /// <summary>
        /// To GET ALL CONCERN
        /// </summary>
        /// <returns></returns>
        public ConcernList GETALLCONCERN()
        {
            ConcerDAL concernDALObj = new ConcerDAL();
            return concernDALObj.GETALLCONCERN();
        }

        // serach the data from the Database Mst_Concern
        public ConcernList GetConcern()
        {
            ConcerDAL concernDALObj = new ConcerDAL();
            return concernDALObj.GetConcern();
        }
        //Insert the data into Database
        public string Insert(ConcernBO objConcern)
        {
            ConcerDAL concernDAl = new ConcerDAL(); //Data pass -to Database Layer

            try
            {
                return concernDAl.Insert(objConcern);
            }
            catch
            {
                throw;
            }
            finally
            {
                concernDAl = null;
            }
        }
        //Search the Singal Data by passing ID
        public ConcernBO GetConcernById(int ConcernID)
        {
            ConcerDAL concernDALObj = new ConcerDAL();
            return concernDALObj.GetConcernById(ConcernID);
        }
        //Edit the data
        public string EDITCONCERN(ConcernBO objConcern)
        {
            ConcerDAL concernDAl = new ConcerDAL(); //Data pass -to Database Layer

            try
            {
                return concernDAl.EDITCONCERN(objConcern);
            }
            catch
            {
                throw;
            }
            finally
            {
                concernDAl = null;
            }
        }
        //Delete the data
        public string DeleteConcern(int ConcernID)
        {
            ConcerDAL concernDALObj = new ConcerDAL();
            return concernDALObj.DeleteConcern(ConcernID);
        }

        public string ObsoleteConcern(int ConcernID, string Isdeleted)
        {
            ConcerDAL concernDALObj = new ConcerDAL();
            return concernDALObj.ObsoleteConcern(ConcernID, Isdeleted);
        }
    }
}