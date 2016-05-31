using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class HealthCenterBLL
    {
        /// <summary>
        /// To Insert into Database
        /// </summary>
        /// <param name="HealthCenterBOobj"></param>
        /// <returns></returns>
        public string Insert(HealthCenterBO HealthCenterBOobj)
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL(); //Data pass -to Database Layer

            try
            {
                return HealthCenterDALobj.Insert(HealthCenterBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                HealthCenterDALobj = null;
            }
        }

        /// <summary>
        /// To Edit in Database
        /// </summary>
        /// <param name="HealthCenterBOobj"></param>
        /// <returns></returns>
        public string Edit(HealthCenterBO HealthCenterBOobj)
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();//Data pass -to Database Layer

            try
            {
                return HealthCenterDALobj.Edit(HealthCenterBOobj);
            }
            catch
            {
                throw;
            }
            finally
            {
                HealthCenterDALobj = null;
            }
        }

        /// <summary>
        /// To Get ALL Health Center
        /// </summary>
        /// <returns></returns>
        public HealthCenterList GetALLHealthCenter()
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();
            return HealthCenterDALobj.GetALLHealthCenter();
        }

        /// <summary>
        /// To Get Health Center
        /// </summary>
        /// <returns></returns>
        public HealthCenterList GetHealthCenter()
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();
            return HealthCenterDALobj.GetHealthCenter();
        }

        /// <summary>
        /// To Get Health Center By Id
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <returns></returns>
        public HealthCenterBO GetHealthCenterById(int HEALTHCENTERID)
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();
            return HealthCenterDALobj.GetHealthCenterById(HEALTHCENTERID);
        }

        /// <summary>
        /// To Delete Health Center
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <returns></returns>
        public string DeleteHealthCenter(int HEALTHCENTERID)
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();
            return HealthCenterDALobj.DeleteHealthCenter(HEALTHCENTERID);
        }

        /// <summary>
        /// To Obsolete Health Center
        /// </summary>
        /// <param name="HEALTHCENTERID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteHealthCenter(int HEALTHCENTERID, string IsDeleted)
        {
            HealthCenterDAL HealthCenterDALobj = new HealthCenterDAL();
            return HealthCenterDALobj.ObsoleteHealthCenter(HEALTHCENTERID, IsDeleted);
        }
    }
}

