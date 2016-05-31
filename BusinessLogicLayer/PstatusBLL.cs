using WIS_BusinessObjects;
using WIS_DataAccess;


namespace WIS_BusinessLogic
{
    public class PstatusBLL
    {
        //Search the data from the Database Mst_PAPDESIGNATION
        /// <summary>
        /// To Get Pap's status
        /// </summary>
        /// <returns></returns>
        public object GetPstatus()
        {
            PstatusDAL PstatusDALObj = new PstatusDAL();
            return PstatusDALObj.GetPstatus();
        }

        /// <summary>
        /// To Get All Pap's status
        /// </summary>
        /// <param name="PapStatus"></param>
        /// <returns></returns>
        public object GetAllPstatus(string PapStatus)
        {
            PstatusDAL PstatusDALObj = new PstatusDAL();
            return PstatusDALObj.GetAllPstatus(PapStatus);
        }

        /// <summary>
        /// To Delete PAP Status
        /// </summary>
        /// <param name="PAPDESIGNATIONID"></param>
        /// <returns></returns>
        public string DeletePstatus(int PAPDESIGNATIONID)
        {
            PstatusDAL PstatusDALObj = new PstatusDAL();
            return PstatusDALObj.DeletePstatus(PAPDESIGNATIONID);
        }

        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="objPstatus"></param>
        /// <returns></returns>
        public string insert(PstatusBO objPstatus)
        {
            PstatusDAL PstatusDAlobj = new PstatusDAL(); //Data pass -to Database Layer

            try
            {
                return PstatusDAlobj.Insert(objPstatus);
            }
            catch
            {
                throw;
            }
            finally
            {
                PstatusDAlobj = null;
            }
        }

        /// <summary>
        /// To EDIT Pap status
        /// </summary>
        /// <param name="objPstatus"></param>
        /// <returns></returns>
        public string EDITPstatus(PstatusBO objPstatus)
        {
            PstatusDAL PstatusDAlobj = new PstatusDAL(); //Data pass -to Database Layer

            try
            {
                return PstatusDAlobj.EDITPstatus(objPstatus);
            }
            catch
            {
                throw;
            }
            finally
            {
                PstatusDAlobj = null;
            }
        }

        /// <summary>
        /// To Get Pap status By Id
        /// </summary>
        /// <param name="PAPDESIGNATIONID"></param>
        /// <returns></returns>
        public PstatusBO GetPstatusById(int PAPDESIGNATIONID)
        {
            PstatusDAL PstatusDAlobj = new PstatusDAL();
            return PstatusDAlobj.GetPstatusById(PAPDESIGNATIONID);
        }

        /// <summary>
        /// To Obsolete PAP Status
        /// </summary>
        /// <param name="papStatusID"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        public string ObsoletePAPStatus(int papStatusID, string IsDeleted, int updatedBy)
        {
            return (new PstatusDAL()).ObsoletePAPStatus(papStatusID, IsDeleted, updatedBy);
        }
    }
}
