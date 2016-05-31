using System.Data;
using WIS_BusinessObjects;
using WIS_DataAccess;

namespace WIS_BusinessLogic
{
    public class SocioConcernBLL
    {
        /// <summary>
        /// To Insert Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string InsertSocioConcern(SocioConcernBO objSocioConcern)
        {
                       
            ConcernDAL ConcernDALobj = new ConcernDAL(); //Data pass -to Database Layer

            try
            {
                return ConcernDALobj.InsertSocioConcern(objSocioConcern);
            }
            catch
            {
                throw;
            }
            finally
            {
                ConcernDALobj = null;
            }
        }

        /// <summary>
        /// To get Socio Concern
        /// </summary>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernList getSocioConcern(int HHID)
        {
            ConcernDAL ConcernDALobj = new ConcernDAL();
            return ConcernDALobj.getSocioConcern(HHID);
        }

        /// <summary>
        /// To Get Socio Concern By Id
        /// </summary>
        /// <param name="PapConcernID"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public SocioConcernBO GetSocioConcernById(int PapConcernID, int HHID)
        {
            ConcernDAL ConcernDALobj = new ConcernDAL();
            return ConcernDALobj.GetSocioConcernById(PapConcernID, HHID);
        }

        /// <summary>
        /// To Edit Socio Concern
        /// </summary>
        /// <param name="objSocioConcern"></param>
        /// <returns></returns>
        public string EditSocioConcern(SocioConcernBO objSocioConcern)
        {
            ConcernDAL ConcernDALobj = new ConcernDAL();
            return ConcernDALobj.EditSocioConcern(objSocioConcern);
        }

        /// <summary>
        /// To Delete Social Concern
        /// </summary>
        /// <param name="PapConcernID"></param>
        /// <param name="HHID"></param>
        /// <returns></returns>
        public string DeleteSocialConcern(int PapConcernID, int HHID)
        {
            ConcernDAL ConcernDALobj = new ConcernDAL();
            return ConcernDALobj.DeleteSocialConcern(PapConcernID, HHID);
        }

    }
}