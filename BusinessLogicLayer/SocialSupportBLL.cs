using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class SocialSupportBLL
    {
        /// <summary>
        /// To Get ALL School Details
        /// </summary>
        /// <returns></returns>
        public object GetALLSchoolDetails()
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.GetALLSchoolDetails();
        }

        /// <summary>
        /// To Get School Details
        /// </summary>
        /// <returns></returns>
        public object GetSchoolDetails()
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.GetSchoolDetails();
        }

        /// <summary>
        /// To Insert Support Details
        /// </summary>
        /// <param name="SocialSupportBOObj"></param>
        /// <returns></returns>
        public string InsertSupportDetails(SocialSupportBO SocialSupportBOObj)
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.InsertSupportDetails(SocialSupportBOObj);
        }

        /// <summary>
        /// To Edit Support Details
        /// </summary>
        /// <param name="SocialSupportBOObj"></param>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public string EditSupportDetails(SocialSupportBO SocialSupportBOObj, int SUPPORTEDBYID)
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.EditSupportDetails(SocialSupportBOObj, SUPPORTEDBYID);
        }

        /// <summary>
        /// To Get Support By Id
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public SocialSupportBO GetSupportById(int SUPPORTEDBYID)
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.GetSupportById(SUPPORTEDBYID);
        }

        /// <summary>
        /// To Delete Support Row
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <returns></returns>
        public string DeleteSupportRow(int SUPPORTEDBYID)
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.DeleteSupportRow(SUPPORTEDBYID);
        }

        /// <summary>
        /// To Obsolete Social Support
        /// </summary>
        /// <param name="SUPPORTEDBYID"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        public string ObsoleteSocialSupport(int SUPPORTEDBYID, string IsDeleted)
        {
            SocialSupportDAL SocialSupportDALObj = new SocialSupportDAL();
            return SocialSupportDALObj.ObsoleteSocialSupport(SUPPORTEDBYID, IsDeleted);
        }
    }
}