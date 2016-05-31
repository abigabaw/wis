using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class ConsultantBLL
    {        
        /// <summary>
        /// To Get Consultant
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
            public ConsultantList  GetConsultant(int projectID)
            {
                ConsultantDAL  objConDAL = new ConsultantDAL();
                return objConDAL.GetConsultant(projectID);
            }

        /// <summary>
            /// To Add Consultant
        /// </summary>
        /// <param name="objCon"></param>
        /// <returns></returns>
            public string AddConsultant(ConsultantBO objCon)
            {
                {
                    ConsultantDAL objConDAL = new ConsultantDAL();
                    return objConDAL.AddConsultant(objCon);
                }
            }

        /// <summary>
            /// To Obsolete Consultant
        /// </summary>
        /// <param name="consultantID"></param>
        /// <param name="isObsolete"></param>
            public void ObsoleteConsultant(int consultantID, string isObsolete)
            {
                ConsultantDAL objConDAL = new ConsultantDAL();
                objConDAL.ObsoleteConsultant(consultantID, isObsolete);
            }

        /// <summary>
            /// To Delete Consultant
        /// </summary>
        /// <param name="ConID"></param>
            public void DeleteConsultant(int ConID)
            {
                ConsultantDAL objConDAL = new ConsultantDAL();
                objConDAL.DeleteConsultant(ConID);
            }

        /// <summary>
            /// To Update Consultant
        /// </summary>
        /// <param name="objCon"></param>
            public void UpdateConsultant(ConsultantBO objCon)
            {
                ConsultantDAL objConDAL = new ConsultantDAL();
                objConDAL.UpdateConsultant(objCon);
            }

        /// <summary>
            /// To Get Consultant By ID
        /// </summary>
        /// <param name="ConID"></param>
        /// <returns></returns>
            public ConsultantBO GetConsultantByID(int ConID)
            {
                ConsultantDAL objConDAL = new ConsultantDAL();
                return objConDAL.GetConsultantByID(ConID);
            }

        }
    }
