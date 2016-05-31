using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class neighbourBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="Neighbourobj"></param>
        /// <returns></returns>
        public string Insert(NeighbourBO Neighbourobj)
        {
            NeighbourDAL NeighbourDALobj = new NeighbourDAL(); //Data pass -to Database Layer

            try
            {
                return NeighbourDALobj.Insert(Neighbourobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                NeighbourDALobj = null;
            }
        }

        /// <summary>
        /// To Get Neighbour Details
        /// </summary>
        /// <param name="householdID"></param>
        /// <returns></returns>
        public NeighbourList  GetneigbrDetails(int householdID)
        {
            NeighbourDAL NeighbourDALobj = new NeighbourDAL();
            return NeighbourDALobj.GetneigbrDetails(householdID);
        }

        /// <summary>
        /// To Get Neighbour By Id
        /// </summary>
        /// <param name="Pap_NeighbrID"></param>
        /// <returns></returns>
        public NeighbourBO GetNeighbrById(int Pap_NeighbrID)
        {
            NeighbourDAL NeighbourDALobj = new NeighbourDAL();
            return NeighbourDALobj.GetNeighbrById(Pap_NeighbrID);
        }

        /// <summary>
        /// To Edit Neighbour
        /// </summary>
        /// <param name="Neighbourobj"></param>
        /// <returns></returns>
        public string EditNeighbr(NeighbourBO Neighbourobj)
        {
            NeighbourDAL NeighbourDALobj = new NeighbourDAL(); //Data pass -to Database Layer

            try
            {
                return NeighbourDALobj.EditNeighbr(Neighbourobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                NeighbourDALobj = null;
            }
        }

        /// <summary>
        /// To Delete
        /// </summary>
        /// <param name="Pap_NeighbrID"></param>
        /// <returns></returns>
        public int Delete(int Pap_NeighbrID)
        {
            NeighbourDAL NeighbourDALobj = new NeighbourDAL(); //Data pass -to Database Layer
            try
            {
                return NeighbourDALobj.Delete(Pap_NeighbrID);
            }
            catch
            {

                throw;
            }
            finally
            {
                NeighbourDALobj = null;
            }
        }
    }
}