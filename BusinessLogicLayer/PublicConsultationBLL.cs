using WIS_BusinessObjects;
using WIS_DataAccess;
using System;

namespace WIS_BusinessLogic
{
    public class PublicConsultationBLL
    {
        /// <summary>
        /// To Insert
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
        public String Insert(PublicConsultationBO BOobj)
        {
            PublicConsultationDAL DALobj = new PublicConsultationDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Insert(BOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }

        /// <summary>
        /// To Get Public Consultation
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public PublicConsultationList GetPublucConsultation(int HHID)
        {
            PublicConsultationDAL DALobj = new PublicConsultationDAL();
            return DALobj.GetPublucConsultation(HHID);
        }

        /// <summary>
        /// To Get Data
        /// </summary>
        /// <param name="CONSULTATIONID"></param>
        /// <returns></returns>
        public  PublicConsultationBO GetData(int CONSULTATIONID)
        {
            PublicConsultationDAL DALobj = new PublicConsultationDAL();
            return DALobj.GetData(CONSULTATIONID);
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="BOobj"></param>
        /// <returns></returns>
        public String Update(PublicConsultationBO BOobj)
        {
            PublicConsultationDAL DALobj = new PublicConsultationDAL(); //Data pass -to Database Layer

            try
            {
                return DALobj.Update(BOobj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DALobj = null;
            }
        }
    }
}
