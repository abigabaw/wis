using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class OccupationBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="objOccupation"></param>
        /// <returns></returns>
       public string Insert(OccupationBO objOccupation)
        {
            OccupationDAL OccupationDAL = new OccupationDAL(); //Data pass -to Database Layer

            try
            {
                return OccupationDAL.InsertOccupation(objOccupation);
            }
            catch
            {
                throw;
            }
            finally
            {
                OccupationDAL = null;
            }
        }

        /// <summary>
       /// To Get ALL Occupation
        /// </summary>
        /// <returns></returns>
       public OccupationList GetALLOccupation()
       {
           OccupationDAL OccupationDALObj = new OccupationDAL();
           return OccupationDALObj.GetALLOccupation();
       }

        /// <summary>
       /// To Get Occupation
        /// </summary>
        /// <returns></returns>
       public OccupationList GetOccupation()
       {
           OccupationDAL OccupationDALObj = new OccupationDAL();
           return OccupationDALObj.GetOccupation();
       }

        /// <summary>
       /// To Delete Occupation
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <returns></returns>
       public string DeleteOccupation(int OCCUPATIONID)
       {
           OccupationDAL OccupationDALObj = new OccupationDAL();
           return OccupationDALObj.DeleteOccupation(OCCUPATIONID);
       }

        /// <summary>
       /// To Get Occupation Id
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <returns></returns>
       public OccupationBO GetOccupationId(int OCCUPATIONID)
       {
           OccupationDAL OccupationDALObj = new OccupationDAL();
           return OccupationDALObj.GetOccupationId(OCCUPATIONID);
       }

        /// <summary>
       /// To EDIT Occupation
        /// </summary>
        /// <param name="objOccupation"></param>
        /// <returns></returns>
       public string EDITOccupation(OccupationBO objOccupation)
       {
           OccupationDAL OccupationDAL = new OccupationDAL(); //Data pass -to Database Layer

           try
           {
               return OccupationDAL.EDITOccupation(objOccupation);
           }
           catch
           {
               throw;
           }
           finally
           {
               OccupationDAL = null;
           }
       }

        /// <summary>
       /// To Obsolete Occupation
        /// </summary>
        /// <param name="OCCUPATIONID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
       public string ObsoleteOccupation(int OCCUPATIONID, string IsDeleted)
       {
           OccupationDAL OccupationDALObj = new OccupationDAL();
           return OccupationDALObj.ObsoleteOccupation(OCCUPATIONID, IsDeleted);
       }
    }
}